using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MazeGUI.DataSources;
using MazeGUI.GameLogic;
using MazeGUI.Utilities;
using MazeGUI.ViewModels;
using MazeLib;
using Newtonsoft.Json.Linq;

namespace MazeGUI.Models {
    /// <summary>
    /// 
    /// </summary>
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

        public delegate void RivalMoved(Position movedTo);

        public delegate void GameStarted();

        public delegate void GameFinished(bool iWon);

        public event GameFinished GameFinishedEvent;

        public event GameStarted GameStartedEvent;



        #endregion

        /// <summary>
        /// Prevents a default instance of the <see cref="MultiPlayerModel"/> class from being created.
        /// </summary>
        private MultiPlayerModel() {
            this.dataSource = new AppConfigDataSource();
            this.client = new TcpClient();
            this.client.Connect(this.EndPoint);
            this.writer = new StreamWriter(client.GetStream());
            this.reader = new StreamReader(client.GetStream());
            this.writer.AutoFlush = true;

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiPlayerModel"/> class.
        /// </summary>
        /// <param name="gameName">Name of the game.</param>
        /// <param name="cols">The cols.</param>
        /// <param name="rows">The rows.</param>
        public MultiPlayerModel(string gameName, int cols, int rows) : this() {
            try {
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
            catch (Exception) {
                
            }
           
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiPlayerModel"/> class.
        /// </summary>
        /// <param name="gameName">Name of the game.</param>
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

        /// <summary>
        /// Gets the end point.
        /// </summary>
        /// <value>
        /// The end point.
        /// </value>
        public IPEndPoint EndPoint {
            get {
                return new IPEndPoint(IPAddress.Parse(this.dataSource.ServerIP),
                    Convert.ToInt32(this.dataSource.ServerPort));
            }
        }

        /// <summary>
        /// Gets the maze.
        /// </summary>
        /// <value>
        /// The maze.
        /// </value>
        public Maze Maze {
            get { return this.gameMaze; }
        }

        /// <summary>
        /// Gets or sets the rival position.
        /// </summary>
        /// <value>
        /// The rival position.
        /// </value>
        public Position RivalPosition {
            get { return this.rivalPosition; }
            set { this.rivalPosition = value; }
        }

        /// <summary>
        /// Gets or sets the client position.
        /// </summary>
        /// <value>
        /// The client position.
        /// </value>
        public Position ClientPosition {
            get { return this.clientPosition; }
            set { this.clientPosition = value; }
        }

        #endregion

        /// <summary>
        /// Listens to rival movement.
        /// </summary>
        private void ListenToRivalMovement() {
            try {
                while (true) {
                    string answer = reader.ReadLine();
                    if (answer != null) {
                        if (answer.Contains("{}")) {
                            this.GameFinishedEvent.Invoke(false);
                        }
                        else {
                            JObject jobj = JObject.Parse(answer);
                            this.rivalPosition = Converter.FromDirectionToNewPosition(this.rivalPosition,
                                Converter.StringToDirection(jobj["Direction"].Value<string>()));
                                this.RivalMovedEvent.Invoke(this.rivalPosition);
                        
                        }
                        
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

        /// <summary>
        /// Clients the moved.
        /// </summary>
        /// <param name="direct">The direct.</param>
        public Position ClientMoved(Direction direct) {
            try {
                if (gameLogic.IsLegitMove(this.clientPosition, direct)) {
                    this.clientPosition = this.gameLogic.Move(this.clientPosition, direct);
                    if (this.gameMaze.GoalPos.Row == this.clientPosition.Row &&
                        this.gameMaze.GoalPos.Col == this.clientPosition.Col) {
                        writer.WriteLine(string.Format("Play {0}", direct.ToString()));
                        Thread.Sleep(5);
                        writer.WriteLine(string.Format("Close {0}", this.gameMaze.Name));
                        this.GameFinishedEvent.Invoke(true);
                    }
                    else {
                        writer.WriteLine(string.Format("Play {0}", direct.ToString()));
                    }
                }
                return this.ClientPosition;
            }
            catch (Exception e) {
                throw e;
            }
        }

        public event MultiPlayerSettingsViewModel.BadArguments BadArgumentsEvent;
    }
}