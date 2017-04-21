using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ServerLib;
using MazeLib;

namespace ServerLib {
    /// <summary>
    /// Class desribes a info container for a Player game.
    /// </summary>
    class MultiPlayerInfoPackage {
        /// <summary>
        /// The host socket;
        /// The guest socket;
        /// The maze they host and guest are playing on.
        /// </summary>
        private IPlayer host;

        /// <summary>
        /// The guest
        /// </summary>
        private IPlayer guest;
        /// <summary>
        /// The maze
        /// </summary>
        private Maze maze;

        /// <summary>
        /// Gets or sets the host.
        /// </summary>
        /// <value>
        /// The host.
        /// </value>
        public IPlayer Host {
            get { return this.host; }
            set {
                this.host = value;
                this.host.Busy = true;

            }
        }

        /// <summary>
        /// Gets or sets the guest.
        /// </summary>
        /// <value>
        /// The guest.
        /// </value>
        public IPlayer Guest {
            get { return this.guest; }
            set {
                this.guest = value;
                this.guest.Busy = true;

            }
        }

        /// <summary>
        /// Gets or sets the maze.
        /// </summary>
        /// <value>
        /// The maze.
        /// </value>
        public Maze Maze {
            get { return this.maze; }
            set { this.maze = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiPlayerInfoPackage"/> class.
        /// </summary>
        /// <param name="host">The host. The host socket</param>
        /// <param name="maze">The maze.the maze the Player game is on </param>
        public MultiPlayerInfoPackage(IPlayer host, Maze maze) {
            this.Host = host;
            this.maze = maze;
        }

        /// <summary>
        /// Clears the game, notifiyes the player  their session has ended.
        /// </summary>
        public void Clear() {
            this.host.Busy = false;
            this.guest.Busy = false;
        }
    }
}