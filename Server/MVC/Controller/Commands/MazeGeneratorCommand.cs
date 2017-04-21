using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MazeGeneratorLib;
using MazeLib;
using System.IO;
using Server;
using Server.Exceptions;


/// <summary>
/// 
/// </summary>
namespace ServerLib {
    /// <summary>
    /// Class describes a generator of maze.
    /// </summary>
    /// <seealso cref="ServerLib.Command" />
    class MazeGeneratorCommand : Command {
        /// <summary>
        /// Initializes a new instance of the <see cref="MazeGeneratorCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public MazeGeneratorCommand(IModel model) : base(model) {
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="player"></param>
        /// <returns>
        /// JSON result of the command
        /// </returns>
        /// <exception cref="GameException">
        /// Failed converting args , please retry - true
        /// or
        /// true
        /// </exception>
        public override string ExecuteCommand(string[] args, IPlayer player = null) {
            lock (this.lockRaceCondition) {
                string name = "";
                int rows = -1;
                int col = -1;
                try {
                    name = args[0];
                    rows = int.Parse(args[1]);
                    col = int.Parse(args[2]);
                }
                catch (Exception e) {
                    throw new GameException("Failed converting args , please retry", true);
                }
                Maze maze = this.model.GenerateMaze(name, rows, col);
                if (maze != null) {
                    player.SendMessage(maze.ToJSON());
                    player.CloseConnection();
                    return "shutdown";
                }
                else {
                    throw new GameException(
                        String.Format("Failed generating maze {0} either already exists or internal error!", name),
                        true);
                }
            }
        }
    }
}