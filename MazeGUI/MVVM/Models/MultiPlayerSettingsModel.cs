using MazeGUI.DataSources;
using MazeLib;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MazeGUI.Models
{
    class MultiPlayerSettingsModel : INotifyPropertyChanged
    {
        private IDataSource dataSource;
        private IPEndPoint endPoint;
        private Boolean shouldStop;
        private string gameName;
        private int selectedIndex;
        private ObservableCollection<string> avaiableGamesList;
        public delegate void ConnectionLost(string mess);
        public event ConnectionLost ConnectionLostEvent;
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiPlayerSettingsModel"/> class.
        /// </summary>
        public MultiPlayerSettingsModel()
        {
            this.dataSource = new AppConfigDataSource();
            this.shouldStop = false;
            endPoint = new IPEndPoint(IPAddress.Parse(this.dataSource.ServerIP), Convert.ToInt32(this.dataSource.ServerPort));
            this.ListenToGames();
        }
        /// <summary>
        /// Listens to games.
        /// </summary>
        private void ListenToGames()
        {
            Task t = new Task(() =>
            {
                try
                {
                    TcpClient client = new TcpClient();
                    client.Connect(this.endPoint);
                    StreamReader reader = new StreamReader(client.GetStream());
                    StreamWriter writer = new StreamWriter(client.GetStream());
                    writer.AutoFlush = true;
                    while(!this.shouldStop)
                    {
                        writer.WriteLine("List");
                        string answer = reader.ReadLine();
                        if (answer != "")
                        {
                            this.AvaiableGamesList = JArray.Parse(answer).ToObject<ObservableCollection<string>>();
                            if(this.avaiableGamesList.Count >= 0)
                            {
                                this.IndexSelected = 0;
                            } else
                            {
                                this.IndexSelected = -1;
                            }
                        }
                        System.Threading.Thread.Sleep(3000);
                    }
                    client.GetStream().Dispose();
                    writer.Dispose();
                    reader.Dispose();
                }
                catch (Exception e)
                {
                    this.ConnectionLostEvent?.Invoke("Failed asking server for games");
                }
            });
            t.Start();         
        }
        /// <summary>
        /// Gets or sets the name of the game.
        /// </summary>
        /// <value>
        /// The name of the game.
        /// </value>
        public string GameName
        {
            get { return this.gameName; }
            set
            {
                this.gameName = value;
                NotifyPropertyChanged("GameName");
            }
        }
        /// <summary>
        /// Gets or sets the rows.
        /// </summary>
        /// <value>
        /// The rows.
        /// </value>
        public uint Rows
        {
            get { return this.dataSource.Rows; }
            set { this.dataSource.Rows = value; }
        }

        /// <summary>
        /// Gets or sets the cols.
        /// </summary>
        /// <value>
        /// The cols.
        /// </value>
        public uint Cols
        {
            get { return this.dataSource.Cols; }
            set { this.dataSource.Cols = value; }
        }
        protected virtual void NotifyPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        /// <summary>
        /// Gets or sets the avaiable games list.
        /// </summary>
        /// <value>
        /// The avaiable games list.
        /// </value>
        public ObservableCollection<string> AvaiableGamesList
        {
            get { return this.avaiableGamesList; }
            set
            {
                this.avaiableGamesList = value;
                this.NotifyPropertyChanged("AvaiableGamesList");
            }
        }
        /// <summary>
        /// Sets a value indicating whether this <see cref="MultiPlayerSettingsModel"/> is stop.
        /// </summary>
        /// <value>
        ///   <c>true</c> if stop; otherwise, <c>false</c>.
        /// </value>
        public Boolean Stop
        {
            set { this.shouldStop = true; }
        }
        /// <summary>
        /// Joins the maze.
        /// </summary>
        /// <returns></returns>
        public Maze JoinMaze()
        {
            if (!string.IsNullOrEmpty(this.gameName))
            {
                Maze maze;
                TcpClient joinClient = new TcpClient();
                joinClient.Connect(this.endPoint);
                StreamWriter writer = new StreamWriter(joinClient.GetStream());
                StreamReader reader = new StreamReader(joinClient.GetStream());
                writer.AutoFlush = true;
                using (writer)
                using (reader)
                {
                    writer.WriteLine(String.Format("Start {0} {1} {2}", GameName, this.dataSource.Rows,
                        this.dataSource.Cols));
                    string answer = reader.ReadLine();
                    maze = Maze.FromJSON(answer);
                }
                return maze;
            }
            else
            {
                this.ConnectionLostEvent?.Invoke("No game name was given! Please enter a maze name.");
                return null;
            }

        }
        /// <summary>
        /// Joins the maze.
        /// </summary>
        /// <returns></returns>
        public int IndexSelected
        {
            get { return this.selectedIndex; }
            set
            {
                this.selectedIndex = value;
                this.NotifyPropertyChanged("IndexSelected");
            }
        }
    }
}
