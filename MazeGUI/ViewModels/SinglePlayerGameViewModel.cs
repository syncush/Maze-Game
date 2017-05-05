using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Documents;
using MazeGUI.Annotations;
using MazeGUI.Models;
using MazeGUI.Utilities;
using MazeLib;
using Newtonsoft.Json.Linq;

namespace MazeGUI.ViewModels {
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    class SinglePlayerGameViewModel : INotifyPropertyChanged {
        #region DataMembers

        private string[,] mazeString;
        private SinglePlayerGameModel model;
        private List<Position> solutionPosList;
        private Boolean shouldDrawSolution;
        public event PropertyChangedEventHandler PropertyChanged;
        public event GameFinishAction GameFinishedEvent;

        public delegate void GameFinishAction();

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="SinglePlayerGameViewModel"/> class.
        /// </summary>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        /// <param name="mazeName">Name of the maze.</param>
        public SinglePlayerGameViewModel(int rows, int cols, string mazeName) {
            this.model = new SinglePlayerGameModel();
            this.shouldDrawSolution = false;
            this.solutionPosList = new List<Position>();
            this.mazeString = new string[rows, cols];
            TcpClient client = new TcpClient();
            client.Connect(this.model.EndPoint);
            StreamWriter writer = new StreamWriter(client.GetStream());
            StreamReader reader = new StreamReader(client.GetStream());
            writer.AutoFlush = true;
            string answer;
            using (client.GetStream())
            using (writer)
            using (reader) {
                writer.WriteLine(string.Format("Generate {0} {1} {2}", mazeName, rows, cols));
                answer = reader.ReadLine();
            }
            Maze maze = Maze.FromJSON(answer);
            maze.Name = mazeName;
            this.model.Maze = maze;
            this.model.PlayerPosition = maze.InitialPos;
            this.mazeString = new String[rows, cols];
        }

        /// <summary>
        /// Gets or sets the maze object.
        /// </summary>
        /// <value>
        /// The maze object.
        /// </value>
        public string MazeOrder {
            get {
                if (shouldDrawSolution) {
                    return Converter.MazeToRepresentation(this.mazeString,this.model.Maze, this.solutionPosList,
                        this.model.PlayerPosition);
                }
                else {
                    return Converter.MazeToRepresentation(this.mazeString,this.model.Maze, null,
                        this.model.PlayerPosition);
                }
                
            }
   
        }


        /// <summary>
        /// Sets the player position.
        /// </summary>
        /// <value>
        /// The player position.
        /// </value>
        public string PlayerPos
        {
            get { return this.model.PlayerPosition.Row + "," + this.model.PlayerPosition.Col; }
            set {
                this.model.PlayerPosition = new Position(Convert.ToInt32(value.Split(',')[0]), Convert.ToInt32(value.Split(',')[1]));
                this.NotifyPropertyChanged("PlayerPos");
            }
        }

        #region Funcs

        /// <summary>
        /// Notifies the property changed.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        [NotifyPropertyChangedInvocator]
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        /// <summary>
        /// Restarts the game.
        /// </summary>
        public void RestartGame() {
            this.model.PlayerPosition = this.model.Maze.InitialPos;
            this.solutionPosList = null;
            this.shouldDrawSolution = false;
            this.NotifyPropertyChanged("PlayerPos");

        }

        /// <summary>
        /// Moves the player.
        /// </summary>
        /// <param name="direct">The direct.</param>
        public void MovePlayer(string direct) {
            Direction parseDirection = Converter.StringToDirection(direct);
            this.model.PlayerPosition = this.model.Move(parseDirection);
            if (this.model.PlayerPosition.Row == this.model.Maze.GoalPos.Row &&
                this.model.PlayerPosition.Col == this.model.Maze.GoalPos.Col) {
                GameFinishedEvent.Invoke();
            }
            this.PlayerPos = this.model.PlayerPosition.Row + "," + this.model.PlayerPosition.Col;
           
        }

        /// <summary>
        /// Solves the maze.
        /// </summary>
        public void SolveMaze() {
            TcpClient client = new TcpClient();
            string answer;
            client.Connect(this.model.EndPoint);
            StreamWriter writer = new StreamWriter(client.GetStream());
            StreamReader reader = new StreamReader(client.GetStream());
            writer.AutoFlush = true;
            writer.WriteLine(string.Format("Solve {0} {1}", this.model.Maze.Name, this.model.Algorithm));
            answer = reader.ReadLine();
            this.DrawSolvedMaze(answer);
        }
        /**
        /// <summary>
        /// Draws the solved maze.
        /// </summary>
        /// <param name="solution">The solution.</param>
        public void DrawSolvedMaze(string solution) {
            JObject obj = JObject.Parse(solution);
            string list = obj.GetValue("Solution").Value<String>();
            List<Position> posList = new List<Position>();
            posList.Add(this.model.Maze.InitialPos);
            foreach (char direction in list) {
                posList.Add(Converter.CharToPosition(posList[posList.Count - 1], direction));
            }
            this.solutionPosList = posList;
            this.shouldDrawSolution = true;
            this.NotifyPropertyChanged("MazeOrder");

        }
    **/
        /// <summary>
        /// Draws the solved maze.
        /// </summary>
        /// <param name="solution">The solution.</param>
        public void DrawSolvedMaze(string solution)
        {
            this.RestartGame();
            JObject obj = JObject.Parse(solution);
            string list = obj.GetValue("Solution").Value<String>();
            List<Position> posList = new List<Position>();
            posList.Add(this.model.Maze.InitialPos);
            Task t = new Task(() => {
                foreach (char direction in list) {
                    switch (direction)
                    {
                        case '0':
                        {
                            this.MovePlayer("left");
                            }
                            break;

                        case '1':
                        {
                            this.MovePlayer("right");
                            }
                            break;

                        case '2':
                        {
                            this.MovePlayer("up");
                            }
                            break;

                        case '3':
                        {
                            this.MovePlayer("down");
                            }
                            break;
                        default:
                        {
                        }
                            break;
                    }
                    Thread.Sleep(1000);

                }
            });
            t.Start();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name of the game.
        /// </summary>
        /// <value>
        /// The name of the game.
        /// </value>
        public string GameName {
            set { this.model.Maze.Name = value; }
            get { return this.model.Maze.Name; }
        }

        #endregion
    }
}