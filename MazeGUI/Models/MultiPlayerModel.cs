using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MazeGUI.DataSources;
using MazeGUI.GameLogic;
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
        private IGameLogic gameLogic;

        #endregion

        #region CommunicationServer

        private TcpClient client;
        private StreamWriter writer;
        private StreamReader reader;
        private Task t;

        #endregion

        #region Events

        public event RivalMoved RivalMovedEvent;

        public delegate void RivalMoved();

        public delegate void GameStarted();

        public event GameStarted GameStartedEvent;

        #endregion

        private MultiPlayerModel() {
            this.dataSource = new AppConfigDataSource();
            this.client = new TcpClient();
            this.client.Connect(this.EndPoint);
            this.writer = new StreamWriter(client.GetStream());
            this.reader = new StreamReader(client.GetStream());
            this.writer.AutoFlush = true;

        }

        public MultiPlayerModel(string gameName, int cols, int rows) : this() {
            writer.WriteLine(string.Format("Start {0} {1} {2}", gameName, rows, cols));
            string answer = reader.ReadLine();
            Maze maze = Maze.FromJSON(answer);
            this.gameMaze = maze;
            this.clientPosition = maze.InitialPos;
            this.rivalPosition = maze.InitialPos;
            this.gameLogic = new MazeGameLogic(maze);
            Action listenerAction = new Action(() => this.ListenToRivalMovement());
            t = new Task(listenerAction);
            t.Start();
        }

        public MultiPlayerModel(string gameName) : this() {
            writer.WriteLine(string.Format("join {0}", gameName));
            string answer = reader.ReadLine();
            Maze maze = Maze.FromJSON(answer);
            this.gameMaze = maze;
            this.rivalPosition = maze.InitialPos;
            this.clientPosition = maze.InitialPos;
            this.gameLogic = new MazeGameLogic(maze);
            Action listenerAction = new Action(() =>
                this.ListenToRivalMovement());
            t = new Task(listenerAction);
            t.Start();
        }

        #region Properties

        public IPEndPoint EndPoint {
            get {
                return new IPEndPoint(IPAddress.Parse(this.dataSource.ServerIP),
                    Convert.ToInt32(this.dataSource.ServerPort));
            }
        }

        public Maze Maze {
            get { return this.gameMaze; }
        }

        public Position RivalPosition {
            get { return this.rivalPosition; }
            set { this.rivalPosition = value; }
        }

        public Position ClientPosition {
            get { return this.clientPosition; }
            set { this.clientPosition = value; }
        }

        #endregion

        private void ListenToRivalMovement() {
            try {
                while (true) {
                    string answer = reader.ReadLine();
                    if (answer != null) {
                        JObject jobj = JObject.Parse(answer);
                        this.rivalPosition = Converter.FromDirectionToNewPosition(this.rivalPosition,
                            Converter.StringToDirection(jobj["Direction"].Value<string>()));
                        this.RivalMovedEvent.Invoke();
                    }
                    else {
                        break;
                    }
                }
            }
            catch (Exception e) {
                throw e;
            }
        }

        public void ClientMoved(Direction direct) {
            try {
                if (gameLogic.IsLegitMove(this.clientPosition, direct)) {
                    this.clientPosition = this.gameLogic.Move(this.clientPosition, direct);
                    writer.WriteLine(string.Format("Play {0}", direct.ToString()));
                }
            }
            catch (Exception e) {
                throw e;
            }
        }
    }
}