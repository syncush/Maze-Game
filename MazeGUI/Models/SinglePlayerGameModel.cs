using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MazeGUI.GameLogic;
using MazeLib;

namespace MazeGUI.Models {
    class SinglePlayerGameModel {
        private Maze maze;
        private Position playerPosition;
        private IGameLogic gameLogic;
        private DataSources.IDataSource dataSource;

        public SinglePlayerGameModel() {
           
            
            this.dataSource = new DataSources.AppConfigDataSource();
        }

        public Position PlayerPosition {
            get { return this.playerPosition; }
            set { }
        }

        public Position Move(Direction move) {
            Position p = this.gameLogic.Move(this.playerPosition, move);
            return p;
        }

        public IPEndPoint EndPoint {
            get { return
                new IPEndPoint(IPAddress.Parse(this.dataSource.ServerIP), Convert.ToInt32(this.dataSource.ServerPort));
            }
        }

        public Maze Maze {
            set
            {
                playerPosition = value.InitialPos;
                this.maze = value;
                this.gameLogic = new MazeGameLogic(value);
            }
            get { return this.maze; }
        }
    }
}