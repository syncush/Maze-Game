using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MazeLib;
using Server;
using Server.Exceptions;

namespace ServerLib {
    /// <summary>
    /// A class describes the join command , when a guest joins a host game.
    /// </summary>
    /// <seealso cref="ServerLib.Command" />
     class JoinCommand : Command {

        /// <summary>
        /// Initializes a new instance of the <see cref="JoinCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public JoinCommand(IModel model) : base(model) {
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="client">The player.</param>
        /// <returns></returns>
        /// <exception cref="GameException">true</exception>
        public override string ExecuteCommand(string[] args, IPlayer player = null) {
            lock (this.lockRaceCondition) {
                string name = args[0];
                //Assign the plaer to the game.
                MultiPlayerInfoPackage mpPack = this.model.Join(name, player);
                if (mpPack != null) { //Found an active game with the player
                    player.SendMessage(mpPack.Maze.ToJSON());
                    mpPack.Host.SendMessage(mpPack.Maze.ToJSON());
                    return "keep";
                }
                else { //Could not find an active game with the player.
                    throw new GameException(string.Format("Unable to join {0} game session", name), true);
                }
                
            }
        }
    }
}