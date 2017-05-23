using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MazeGUI.GameLogic;
using MazeLib;
using System.Net.Sockets;
using System.IO;
using Newtonsoft.Json.Linq;

namespace MazeGUI.Models {
    /// <summary>
    /// 
    /// </summary>
    class SinglePlayerGameModel {
        private Maze maze;
        private Position playerPosition;
        private IGameLogic gameLogic;
        private DataSources.IDataSource dataSource;


        public delegate void GameFinished();
        public event GameFinished GameFinishedEvent;

        public delegate void ConnectionFailure();

        public event ConnectionFailure ConnectionFailureEvent;


        /// <summary>
        /// Initializes a new instance of the <see cref="SinglePlayerGameModel"/> class.
        /// </summary>
        public SinglePlayerGameModel(string mazeName, int rows, int cols) { 
            this.dataSource = new DataSources.AppConfigDataSource();
            this.GenerateMaze(mazeName, rows, cols);
            this.gameLogic = new MazeGameLogic(maze);            
        }
        /// <summary>
        /// Generates the maze.
        /// </summary>
        /// <returns></returns>
        private void GenerateMaze(string mazeName, int rows, int cols)
        {
            TcpClient client = new TcpClient();
            client.Connect(this.EndPoint);
            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream);
            Maze maze = null;
            using (stream)
            using (reader)
            using (writer)
            {
                try
                {
                writer.AutoFlush = true;
                writer.WriteLine(string.Format("Generate {0} {1} {2}", mazeName, rows, cols));
                string answer = reader.ReadLine();
               
                    maze = Maze.FromJSON(answer);
                    this.maze = maze;
                    this.maze.Name = mazeName;
                    this.playerPosition = this.maze.InitialPos;
                }
                catch (Exception e) {
                   this.ConnectionFailureEvent?.Invoke();
                }
               
            }
            
        }

        /// <summary>
        /// Gets or sets the player position.
        /// </summary>
        /// <value>
        /// The player position.
        /// </value>
        public Position PlayerPosition {
            get { return this.playerPosition; }
            set { this.playerPosition = value; }
        }

        /// <summary>
        /// Moves the specified move.
        /// </summary>
        /// <param name="move">The move.</param>
        /// <returns></returns>
        public void Move(Direction move) {
            if(gameLogic.IsLegitMove(this.playerPosition, move))
            {
                this.playerPosition = this.gameLogic.Move(this.playerPosition, move);
            }
           
            
            if(this.playerPosition.Row == this.maze.GoalPos.Row && this.playerPosition.Col == this.maze.GoalPos.Col)
            {
                this.GameFinishedEvent?.Invoke();
            }
        }
        /// <summary>
        /// Gets the maze.
        /// </summary>
        /// <value>
        /// The maze.
        /// </value>
        public Maze Maze {
            get {
                return this.maze;
            }
        }
        /// <summary>
        /// Restarts this instance.
        /// </summary>
        public void Restart()
        {
            this.playerPosition = this.maze.InitialPos;
        }
        /// <summary>
        /// Gets the end point.
        /// </summary>
        /// <value>
        /// The end point.
        /// </value>
        public IPEndPoint EndPoint {
            get { return
                new IPEndPoint(IPAddress.Parse(this.dataSource.ServerIP), Convert.ToInt32(this.dataSource.ServerPort));
            }
        }

        /// <summary>
        /// Gets the algorithm.
        /// </summary>
        /// <value>
        /// The algorithm.
        /// </value>
        public int Algorithm {
           get { return Convert.ToInt32(this.dataSource.Algorithm); }
        }

        /// <summary>
        /// Generates the solution.
        /// </summary>
        /// <returns></returns>
        public string GenerateSolution()
        {
            try
            {
                TcpClient client = new TcpClient();
                string answer;
                client.Connect(this.EndPoint);
                StreamWriter writer = new StreamWriter(client.GetStream());
                StreamReader reader = new StreamReader(client.GetStream());
                writer.AutoFlush = true;
                writer.WriteLine(string.Format("Solve {0} {1}", this.Maze.Name, this.Algorithm));
                answer = reader.ReadLine();
                JObject obj = JObject.Parse(answer);
                string list = obj.GetValue("Solution").Value<String>();
                return list;

            } catch(Exception e)
            {
                throw e;
            }
           
        }
    }
}