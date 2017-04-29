using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MazeGUI.GameLogic;
using MazeLib;

namespace MazeGUI.Models {
    /// <summary>
    /// 
    /// </summary>
    class SinglePlayerGameModel {
        private Maze maze;
        private Position playerPosition;
        private IGameLogic gameLogic;
        private DataSources.IDataSource dataSource;

        /// <summary>
        /// Initializes a new instance of the <see cref="SinglePlayerGameModel"/> class.
        /// </summary>
        public SinglePlayerGameModel() {
           
            
            this.dataSource = new DataSources.AppConfigDataSource();
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
        public Position Move(Direction move) {
            Position p = this.gameLogic.Move(this.playerPosition, move);
            return p;
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
        /// Gets or sets the maze.
        /// </summary>
        /// <value>
        /// The maze.
        /// </value>
        public Maze Maze {
            set
            {
                playerPosition = value.InitialPos;
                this.maze = value;
                this.gameLogic = new MazeGameLogic(value);
            }
            get { return this.maze; }
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
    }
}