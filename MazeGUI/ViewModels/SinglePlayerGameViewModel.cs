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
    /// <seealso cref="MazeGUI.ViewModels.GameViewModel" />
    class SinglePlayerGameViewModel : ViewModel {
        #region DataMembers

        private string[,] mazeString;
        private SinglePlayerGameModel model;
        private List<Position> solutionPosList;
        private Boolean shouldDrawSolution;

        #endregion

        #region Events



        public delegate void GameFinished();
        public delegate void AnimationInvoke();
        public delegate void ConnectionFailed(string message);


        public event GameFinished GameFinishedEvent;
        public event AnimationInvoke AnimationFinishedEvent;
        public event AnimationInvoke AnimationStartedEvent;
        public event ConnectionFailed ConnectionToServerFailedEvent;

        #endregion


        /// <summary>
        /// Initializes a new instance of the <see cref="SinglePlayerGameViewModel"/> class.
        /// </summary>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        /// <param name="mazeName">Name of the maze.</param>
        public SinglePlayerGameViewModel(int rows, int cols, string mazeName) {
            this.model = new SinglePlayerGameModel(mazeName, rows, cols);
            this.model.GameFinishedEvent += AnnounceGameFinished;
            this.shouldDrawSolution = false;
            this.solutionPosList = new List<Position>();
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
               
                this.NotifyPropertyChanged("PlayerPos");
            }
        }

        #region Funcs



        /// <summary>
        /// Restarts the game.
        /// </summary>
        public void RestartGame() {
            this.model.Restart();
            this.solutionPosList = null;
            this.shouldDrawSolution = false;
            this.PlayerPos = "yolo";
            this.NotifyPropertyChanged("PlayerPos");

        }

        /// <summary>
        /// Moves the player.
        /// </summary>
        /// <param name="direct">The direct.</param>
        public void MovePlayer(string direct) {
            Direction parseDirection = Converter.StringToDirection(direct);
            this.model.Move(parseDirection);
            NotifyPropertyChanged("PlayerPos");
           
        }

        /// <summary>
        /// Solves the maze.
        /// </summary>
        public void SolveMaze() {
           
            try
            {
                this.DrawSolvedMaze(this.model.GenerateSolution());
            } catch(Exception e)
            {
                this.ConnectionToServerFailedEvent?.Invoke("Failed generating a solution , check connection to Server !");
            }
            
        }
        public void AnnounceGameFinished()
        {
            this.GameFinishedEvent?.Invoke();
        }
        /// <summary>
        /// Draws the solved maze.
        /// </summary>
        /// <param name="solution">The solution.</param>
        public void DrawSolvedMaze(string solution)
        {
            this.RestartGame();
            this.AnimationStartedEvent?.Invoke();
            Task t = new Task(() => {
                foreach (char direction in solution) {
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
                    Thread.Sleep(500);

                }
                this.AnimationFinishedEvent?.Invoke();
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