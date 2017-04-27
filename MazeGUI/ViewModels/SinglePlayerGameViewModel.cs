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
    class SinglePlayerGameViewModel : INotifyPropertyChanged {
        #region DataMembers

        private int[,] mazeInts;
        private SinglePlayerGameModel model;
        private List<Position> solutionPosList;
        private Boolean shouldDrawSolution;
        public event PropertyChangedEventHandler PropertyChanged;
        public event GameFinishAction GameFinishedEvent;

        public delegate void GameFinishAction();

        #endregion

        public SinglePlayerGameViewModel(int rows, int cols, string mazeName) {
            this.model = new SinglePlayerGameModel();
            this.shouldDrawSolution = false;
            this.solutionPosList = new List<Position>();
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
                if (!shouldDrawSolution) {
                    return Converter.MazeToRepresentation(this.mazeInts, this.model.Maze, null,
                        this.model.PlayerPosition);
                }
                else {
                    return Converter.MazeToRepresentation(this.mazeInts, this.model.Maze, null,
                        this.model.PlayerPosition);
                }
                
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
            this.shouldDrawSolution = false;
        }

        public void MovePlayer(string direct) {
            Direction parseDirection = Converter.StringToDirection(direct);
            this.PlayerPosition = this.model.Move(parseDirection);
            if (this.model.PlayerPosition.Row == this.model.Maze.GoalPos.Row &&
                this.model.PlayerPosition.Col == this.model.Maze.GoalPos.Col) {
                GameFinishedEvent.Invoke();
            }
            this.NotifyPropertyChanged("MazeOBJ");
        }

        public void SolveMaze() {
            TcpClient client = new TcpClient();
            string answer;
            client.Connect(this.model.EndPoint);
            StreamWriter writer = new StreamWriter(client.GetStream());
            StreamReader reader = new StreamReader(client.GetStream());
            writer.AutoFlush = true;
            using (client.GetStream())
            using (writer)
            using (reader) {
                writer.WriteLine(string.Format("Solve {0} {1}", this.model.Maze.Name, this.model.Algorithm));
                answer = reader.ReadLine();
            }
            this.DrawSolvedMaze(answer);
        }

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