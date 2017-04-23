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
using MazeGUI.Models;
using Newtonsoft.Json.Linq;

namespace MazeGUI.ViewModels {
    class MultiPlayerSettingsViewModel : INotifyPropertyChanged {
        private IDataSource dataSource;
        private ObservableCollection<String> avaiableGames;
        public event PropertyChangedEventHandler PropertyChanged;
        private bool stop;
        private Task t;

        public MultiPlayerSettingsViewModel() {
            this.dataSource = new SettingsModel();
            this.avaiableGames = new ObservableCollection<string>();
        }

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

        [NotifyPropertyChangedInvocator]
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        public Boolean Stop {
            set { this.stop = value; }
        }
        private string Read(StreamReader reader)
        {
            string arr = "";
            while (reader.Peek() > 0)
            {
                arr += reader.ReadLine() + Environment.NewLine;
            }
            return arr;

        }
        public void Intialize() {
            t = new Task(() => {
                IPEndPoint ep = new IPEndPoint(IPAddress.Parse(this.dataSource.ServerIP),
                    Convert.ToInt32(this.dataSource.ServerPort));
                TcpClient client = new TcpClient();
                client.Connect(ep);
                StreamReader reader = new StreamReader(client.GetStream());
                StreamWriter writer = new StreamWriter(client.GetStream());
                writer.AutoFlush = true;
                try {
                        while (!stop)
                        {
                            writer.WriteLine("List");
                            string answer = reader.ReadLine();
                            if (answer != "")
                            {
                                this.AvaiableGamesList = JArray.Parse(answer).ToObject<ObservableCollection<string>>();

                            }

                            Thread.Sleep(5000);
                        }
                    
        }
                catch (IOException) {
                    throw new Exception("Thread failed listening to games");
                }
            });
            this.t.Start();
        }

    }
}