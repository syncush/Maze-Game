using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MazeGUI.DataSources;
using MazeGUI.Models;
using Newtonsoft.Json.Linq;

namespace MazeGUI.ViewModels
{
    class MultiPlayerSettingsViewModel {
        private IDataSource dataSource;
        private List<String> avaiableGames;
        private bool stop;
        public MultiPlayerSettingsViewModel() {
            this.dataSource = new SettingsModel();
            Task t = new Task(() => {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(this.dataSource.ServerIP), Convert.ToInt32(this.dataSource.ServerPort));
                TcpClient client = new TcpClient();
                client.Connect(ep);
                StreamReader reader = new StreamReader(client.GetStream());
                StreamWriter writer = new StreamWriter(client.GetStream());

                while (!stop) {
                    writer.WriteLine("list");
                    string answer = reader.ReadLine();
                    this.avaiableGames = JArray.Parse(answer).ToObject<List<String>>();
                    Thread.Sleep(5000);
                }
            });
        }

        public uint Rows {
            get { return this.dataSource.Rows; }
            set { this.dataSource.Rows = value; }
        }
        public uint Cols {
            get { return this.dataSource.Cols; }
            set { this.dataSource.Cols = value; }
        }

        public List<String> AvaiableGamesList {
            get { return this.avaiableGames; }
        }
    }
}
