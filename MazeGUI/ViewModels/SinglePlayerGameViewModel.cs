using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;

namespace MazeGUI.ViewModels
{
    class SinglePlayerGameViewModel : INotifyPropertyChanged {
        #region DataMembers
        private Maze mazeGame;
        private Position playerPosition;

        public SinglePlayerGameViewModel(Maze maze) {
            this.mazeGame = maze;
            this.playerPosition = maze.InitialPos;
        }




        #endregion
        public event PropertyChangedEventHandler PropertyChanged;

        #region Funcs

        

       
        public void RestartGame() {
            this.playerPosition = mazeGame.InitialPos;
        }

        public void Move(string Direction) {
            
        }
        #endregion

        #region Properties

        public string GameName {
            set { }
            get { return this.GameName; }
        }

        public Maze Game {
            get { return this.mazeGame; }
        }
        

        #endregion



    }
}
