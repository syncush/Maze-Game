using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MazeGUI.Annotations;
using MazeGUI.DataSources;
using MazeLib;
using MazeGUI.Models;
using Newtonsoft.Json.Linq;

namespace MazeGUI.ViewModels {
    class MultiPlayerSettingsViewModel : INotifyPropertyChanged {
        private IDataSource dataSource;
        private IPEndPoint ep;
        private ObservableCollection<String> avaiableGames;
        private string gameName;
        public event PropertyChangedEventHandler PropertyChanged;
        private bool stop;
        private Task t;

        public MultiPlayerSettingsViewModel() {
            this.dataSource = new SettingsModel();
            this.avaiableGames = new ObservableCollection<string>();
            ep = new IPEndPoint(IPAddress.Parse(this.dataSource.ServerIP),
                Convert.ToInt32(this.dataSource.ServerPort));
        }

        #region Properties

        public uint Rows {
            get { return this.dataSource.Rows; }
            set { this.dataSource.Rows = value; }
        }

        public uint Cols {
            get { return this.dataSource.Cols; }
            set { this.dataSource.Cols = value; }
        }

        public ObservableCollection<string> AvaiableGamesList {
            get { return this.avaiableGames; }
            set {
                this.avaiableGames = value;
                this.NotifyPropertyChanged("AvaiableGamesList");
            }
        }

        public Boolean Stop {
            set { this.stop = value; }
        }


        public string GameName {
            get { return this.gameName; }
            set {
                this.gameName = value;
                NotifyPropertyChanged("GameName");
            }
        }

        #endregion Properties


        #region Funcs

        public Maze JoinMaze() {
            Maze maze;
            TcpClient joinClient = new TcpClient();
            joinClient.Connect(ep);
            StreamWriter writer = new StreamWriter(joinClient.GetStream());
            StreamReader reader = new StreamReader(joinClient.GetStream());
            writer.AutoFlush = true;
            using (writer)
            using (reader) {
                writer.WriteLine(string.Format("Start {0} {1} {2}", GameName, this.dataSource.Rows,
                    this.dataSource.Cols));
                string answer = reader.ReadLine();
                maze = Maze.FromJSON(answer);
            }
            return maze;
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Intialize() {
            t = new Task(() => {
                TcpClient client = new TcpClient();
                client.Connect(ep);
                StreamReader reader = new StreamReader(client.GetStream());
                StreamWriter writer = new StreamWriter(client.GetStream());
                writer.AutoFlush = true;
                try {
                    while (!stop) {
                        writer.WriteLine("List");
                        string answer = reader.ReadLine();
                        if (answer != "") {
                            this.AvaiableGamesList = JArray.Parse(answer).ToObject<ObservableCollection<string>>();
                        }

                        Thread.Sleep(10000);
                    }
                }
                catch (IOException e) {
                    client.GetStream().Dispose();
                    writer.Dispose();
                    reader.Dispose();
                }
            });
            this.t.Start();
        }
        #endregion
    }
}