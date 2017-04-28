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
    class MultiPlayerGameViewModel : INotifyPropertyChanged {
        #region Events

        public event GameMovement GameClientMovement;

        public delegate void GameMovement(Direction p);

        public delegate void MazeChanged();

        public delegate void GameFinished(bool iWon);

        public event GameFinished GameFinishedEvent;

        public event MazeChanged MazeChangedEvent;

        #endregion

        #region DataMembers

        private MultiPlayerModel mpModel;
        private Boolean isStart;

        #endregion

        public MultiPlayerGameViewModel(string gameName, int rows, int cols) {
            this.mpModel = new MultiPlayerModel(gameName, rows, cols);
            this.mpModel.RivalMovedEvent += this.RivalMoved;
            this.mpModel.GameFinishedEvent += this.GameFinishedFunc;
        }

        public MultiPlayerGameViewModel(string joinGame) {
            this.mpModel = new MultiPlayerModel(joinGame);
            this.mpModel.RivalMovedEvent += this.RivalMoved;
            this.mpModel.GameFinishedEvent += this.GameFinishedFunc;
        }


        public int[,] ClientMaze {
            get { return Converter.MazeToRepresentation(this.mpModel.Maze, this.mpModel.ClientPosition); }
        }

        public int[,] RivalMaze {
            get { return Converter.MazeToRepresentation(this.mpModel.Maze, this.mpModel.RivalPosition); }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void PlayerMoved(string direction) {
            Direction direct = Converter.StringToDirection(direction);
            this.mpModel.ClientMoved(direct);
            MazeChangedEvent.Invoke();
            this.OnPropertyChanged("ClientMaze");
        }

        public void RivalMoved() {
            this.MazeChangedEvent.Invoke();
            this.OnPropertyChanged("RivalMaze");
        }

        public void GameStartedFunc() {
           this.MazeChangedEvent.Invoke();
        }

        public void GameFinishedFunc(bool iWon) {
            this.GameFinishedEvent.Invoke(iWon);
        }
    }
}