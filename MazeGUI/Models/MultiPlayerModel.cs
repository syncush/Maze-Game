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
using MazeGUI.ViewModels;
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

        #region CommunicationServer

        private TcpClient client;
        private StreamWriter writer;
        private StreamReader reader;
        

        #endregion
        #region Events

        public event RivalMoved RivalMovedEvent;
        public delegate void RivalMoved();
        #endregion

        private MultiPlayerModel() {
            this.dataSource = new AppConfigDataSource();
            client.Connect(this.EndPoint);
            writer = new StreamWriter(client.GetStream());
            reader = new StreamReader(client.GetStream());
            writer.AutoFlush = true;
           
        }
        public MultiPlayerModel(string gameName, int cols, int rows) {
            writer.WriteLine(string.Format("Start {0} {1} {2}", gameName, rows, cols));
            string answer = reader.ReadLine();
            Maze maze = Maze.FromJSON(answer);
            this.gameMaze = maze;
            this.clientPosition = maze.InitialPos;
            this.rivalPosition = maze.InitialPos;
            Task t = new Task(()=> {
                this.ListenToRivalMovement();
            });
        
        }

        public MultiPlayerModel(string gameName):this() {
            writer.WriteLine(string.Format("join {0}", gameName));
            string answer = reader.ReadLine();
            Maze maze = Maze.FromJSON(answer);
            this.gameMaze = maze;
            this.rivalPosition = maze.InitialPos;
            this.clientPosition = maze.InitialPos;
            Task t = new Task(() => {
                this.ListenToRivalMovement();
            });

        }

        #region Properties
        public IPEndPoint EndPoint
        {
            get
            {
                return new IPEndPoint(IPAddress.Parse(this.dataSource.ServerIP),
                    Convert.ToInt32(this.dataSource.ServerPort));
            }
        }

        public Maze Maze
        {
            get { return this.gameMaze; }
        }
        public Position RivalPosition
        {
            get { return this.rivalPosition; }
            set { this.rivalPosition = value; }
        }

        public Position ClientPosition
        {
            get { return this.clientPosition; }
            set { this.clientPosition = value; }
        }



        #endregion

        private void ListenToRivalMovement() {
            try {
                using (client.GetStream())
                using (reader)
                using (writer)
                {
                    while (true)
                    {
                        string answer = reader.ReadLine();
                        JObject jobj = JObject.Parse(answer);
                        this.rivalPosition = Converter.FromDirectionToNewPosition(this.rivalPosition,
                            Converter.StringToDirection(jobj["Direction"].Value<string>()));
                        this.RivalMovedEvent.Invoke();
                    }

                }
            }
            catch (Exception) {
                
            }
            
        }

        private void ClientMoved(Direction direct) {
            try {
                writer.WriteLine(string.Format("Play {1}", direct.ToString()));
            }
            catch (Exception) {
                
            }
            
        }
    }
}