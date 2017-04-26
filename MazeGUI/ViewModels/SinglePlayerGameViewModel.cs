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
using MazeGUI.Annotations;
using MazeGUI.Models;
using MazeLib;
using Newtonsoft.Json.Linq;

namespace MazeGUI.ViewModels {
    class SinglePlayerGameViewModel : INotifyPropertyChanged {
        #region DataMembers

        private int[,] mazeInts;
        private SinglePlayerGameModel model;
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        public SinglePlayerGameViewModel(int rows, int cols, string mazeName) {
            this.model = new SinglePlayerGameModel();
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
            this.PlayerPosition = maze.InitialPos;
            this.mazeInts = new int[rows, cols];
        }

        public int[,] MazeOBJ {
            get {
                for (int i = 0; i < this.model.Maze.Rows; i++) {
                    for (int j = 0; j < this.model.Maze.Cols; j++) {
                        mazeInts[i, j] = this.model.Maze[i, j] == CellType.Free ? 1 : 0;
                    }
                }
                mazeInts[this.model.PlayerPosition.Row, this.model.PlayerPosition.Col] = 3;
                mazeInts[this.model.Maze.GoalPos.Row, this.model.Maze.GoalPos.Col] = 2;
                return mazeInts;
                
            }
            set {
                this.mazeInts = value;
                this.NotifyPropertyChanged("MazeOBJ");
            }
        }

        public Position PlayerPosition {
            set {
                this.model.PlayerPosition = value;
                this.NotifyPropertyChanged("MazeOBJ");
                this.NotifyPropertyChanged("PlayerPosition");
            }
        }

        #region Funcs

        [NotifyPropertyChangedInvocator]
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public void RestartGame() {
            this.model.PlayerPosition = this.model.Maze.InitialPos;
        }

        public void MovePlayer(string direct) {
            Direction parseDirection;
            switch (direct) {
                case "Up":
                case "up": {
                    parseDirection = Direction.Up;
                }
                    break;
                case "Down":
                case "down": {
                    parseDirection = Direction.Down;
                }
                    break;
                case "Left":
                case "left": {
                    parseDirection = Direction.Left;
                }
                    break;
                case "Right":
                case "right": {
                    parseDirection = Direction.Right;
                }
                    break;
                default: {
                    parseDirection = Direction.Unknown;
                }
                    break;
            }
            this.PlayerPosition = this.model.Move(parseDirection);
            this.NotifyPropertyChanged("MazeOBJ");
        }

        #endregion

        #region Properties

        public string GameName {
            set { this.model.Maze.Name = value; }
            get { return this.model.Maze.Name; }
        }

        #endregion
    }
}