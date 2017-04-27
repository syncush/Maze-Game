using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MazeGUI.DataSources;
using MazeGUI.Utilities;
using MazeLib;
using Newtonsoft.Json.Linq;

namespace MazeGUI.Models {
    class MultiPlayerModel {
        #region DataMembers

        private IDataSource dataSource;
        private Maze gameMaze;
        private Position clientPosition;
        private Position rivalPosition;


        #endregion

        #region Events

        public event RivalMoved RivalMovedEvent;
        public delegate void RivalMoved();
        #endregion
        public MultiPlayerModel(Maze game) {
            this.dataSource = new AppConfigDataSource();
            this.gameMaze = game;
            this.clientPosition = game.InitialPos;
            this.rivalPosition = game.InitialPos;
        }

        public IPEndPoint EndPoint {
            get {
                return new IPEndPoint(IPAddress.Parse(this.dataSource.ServerIP),
                    Convert.ToInt32(this.dataSource.ServerPort));
            }
        }

        public Position RivalPosition {
            get { return this.rivalPosition; }
            set { this.rivalPosition = value; }
        }

        public Position ClientPosition {
            get { return this.clientPosition; }
            set { this.clientPosition = value; }
        }

        private void ListenToRivalMovement() {
           TcpClient client = new TcpClient();
            client.Connect(this.EndPoint);
            StreamWriter writer = new StreamWriter(client.GetStream());
            StreamReader reader = new StreamReader(client.GetStream());
            using (client.GetStream())
            using (reader)
            using (writer) {
                while (true) {
                    string answer = reader.ReadLine();
                    JObject jobj = JObject.Parse(answer);
                    this.rivalPosition = Converter.FromDirectionToNewPosition(this.rivalPosition,
                        Converter.StringToDirection(jobj["Direction"].Value<string>()));
                    this.RivalMovedEvent.Invoke();
                }
  
            }
        }
    }
}