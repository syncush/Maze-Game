using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using MazeLib;
using Server;
using Server.Exceptions;

namespace ServerLib {
    /// <summary>
    /// Describes start a multiplayer game.
    /// </summary>
    /// <seealso cref="ServerLib.ICommand" />
    class StartCommand : Command {
        /// <summary>
        /// Initializes a new instance of the <see cref="StartCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public StartCommand(IModel model) : base(model) {
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="client">The client.</param>
        /// <returns>
        /// JSON result of the command
        /// </returns>
        public override string ExecuteCommand(string[] args, IPlayer player) {
            try {
                lock (this.lockRaceCondition) {
                    //Convert arguemnts.
                    string name = args[0];
                    int rows = int.Parse(args[1]);
                    int cols = int.Parse(args[2]);
                    //Start a multiplayer session.
                    Maze maze = this.model.Start(name, rows, cols, player);
                    return "keep";
                }
            }
            catch (Exception e) { // failed to start a multiplayer game because of bad arguments.
                throw new GameException("Start command was given bad arguments!", true);
            }
            
        }
    }
}