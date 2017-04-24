using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MazeGUI.Annotations;
using MazeGUI.DataSources;
using MazeGUI.Models;
using MazeLib;

namespace MazeGUI.ViewModels
{
    class SinglePlayerSettingsViewModel : INotifyPropertyChanged {
        #region DataMembers
        private IDataSource dataSource;
        private string gameName;
        #endregion DataMembers

        public SinglePlayerSettingsViewModel() {
            dataSource = new SettingsModel();
        }

        public uint Rows {
            get { return this.dataSource.Rows; }
            set { this.dataSource.Rows = value; }
        }

        public uint Cols {
            get { return this.dataSource.Cols; }
            set { this.dataSource.Cols = value; }
        }

        public Maze GenerateMaze() {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(this.dataSource.ServerIP), Convert.ToInt32(this.dataSource.ServerPort));
            TcpClient client = new TcpClient();
            Maze maze;
            client.Connect(ep);
            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream);
            using (stream)
            using (reader)
            using (writer) {
                writer.AutoFlush = true;
                writer.WriteLine(string.Format("Generate {0} {1} {2}", this.gameName, this.Rows, this.Cols));
                string answer = reader.ReadLine();
                maze = Maze.FromJSON(answer);
            }
            maze.Name = this.gameName;
            return maze;
        }

        public string GameName {
            get { return this.gameName; }
            set {
                this.gameName = value;
                NotifyPropertyChanged("GameName");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
