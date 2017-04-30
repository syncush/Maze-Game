﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;
using MazeGUI.Annotations;
using MazeGUI.Models;
using MazeGUI.Utilities;
using MazeLib;

namespace MazeGUI.ViewModels {
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    class MultiPlayerGameViewModel : INotifyPropertyChanged {
        #region Events

        public event GameMovement GameClientMovement;

        public delegate void GameMovement(Direction p);


        public delegate void GameFinished(bool iWon);

        public event GameFinished GameFinishedEvent;

        #endregion

        #region DataMembers

        private MultiPlayerModel mpModel;
        private Boolean isStart;
        private string[,] mazeRep;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiPlayerGameViewModel"/> class.
        /// </summary>
        /// <param name="gameName">Name of the game.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        public MultiPlayerGameViewModel(string gameName, int rows, int cols) {
            this.mpModel = new MultiPlayerModel(gameName, rows, cols);
            this.mpModel.RivalMovedEvent += this.RivalMoved;
            this.mpModel.GameFinishedEvent += this.GameFinishedFunc;
            this.mazeRep = new string[rows,cols];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiPlayerGameViewModel"/> class.
        /// </summary>
        /// <param name="joinGame">The join game.</param>
        public MultiPlayerGameViewModel(string joinGame) {
            this.mpModel = new MultiPlayerModel(joinGame);
            this.mpModel.RivalMovedEvent += this.RivalMoved;
            this.mpModel.GameFinishedEvent += this.GameFinishedFunc;
            this.mazeRep = new string[this.mpModel.Maze.Rows, this.mpModel.Maze.Cols];
        }


        /// <summary>
        /// Gets the client maze.
        /// </summary>
        /// <value>
        /// The client maze.
        /// </value>
        public string ClientMaze {
            get { return Converter.MazeToRepresentation(this.mazeRep, this.mpModel.Maze, null, this.mpModel.ClientPosition); }
        }

        /// <summary>
        /// Gets the rival maze.
        /// </summary>
        /// <value>
        /// The rival maze.
        /// </value>
        public string RivalMaze {
            get { return Converter.MazeToRepresentation(this.mazeRep, this.mpModel.Maze, null, this.mpModel.RivalPosition); }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string ClientPos {
            get { return this.mpModel.ClientPosition.Row+","+this.mpModel.ClientPosition.Col; }
            set {
                string[] args = value.Split(',');
                this.mpModel.ClientPosition = new Position(Convert.ToInt32(args[0]), Convert.ToInt32(args[1]));
                this.OnPropertyChanged("ClientPos");
            }
            
        }
        public string RivalPos
        {
            get { return this.mpModel.RivalPosition.Row + "," + this.mpModel.RivalPosition.Col; }
            set {
                string[] args = value.Split(',');
                this.mpModel.RivalPosition = new Position(Convert.ToInt32(args[0]), Convert.ToInt32(args[1]));
                this.OnPropertyChanged("RivalPos");
            }

        }

        /// <summary>
        /// Players the moved.
        /// </summary>
        /// <param name="direction">The direction.</param>
        public void PlayerMoved(string direction) {
            Direction direct = Converter.StringToDirection(direction);
            Position clientPos = this.mpModel.ClientMoved(direct);
            this.ClientPos = clientPos.Row + "," + clientPos.Col;

        }

        /// <summary>
        /// Rivals the moved.
        /// </summary>
        public void RivalMoved(Position movedTo) {
            this.RivalPos = movedTo.Row +","+ movedTo.Col ;
            
        }

        /// <summary>
        /// Games the started function.
        /// </summary>
        public void GameStartedFunc() {
          //this.MazeChangedEvent.Invoke();
        }

        /// <summary>
        /// Games the finished function.
        /// </summary>
        /// <param name="iWon">if set to <c>true</c> [i won].</param>
        public void GameFinishedFunc(bool iWon) {
            this.GameFinishedEvent.Invoke(iWon);
        }
    }
}