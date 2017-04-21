using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using Server;
using Newtonsoft.Json.Linq;
using Server.Exceptions;

namespace ServerLib {
    /// <summary>
    /// Handles the "move" command of multiplayer game
    /// </summary>
    /// <seealso cref="ICommand" />
     class PlayCommand : Command {
        /// <summary>
        /// Initializes a new instance of the <see cref="MoveCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public PlayCommand(IModel model) : base(model) {
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="client">The client.</param>
        /// <returns>string status of command</returns>
        public override string ExecuteCommand(string[] args, IPlayer player) {
            lock (this.lockRaceCondition) {
                Direction direct = Direction.Unknown;
                switch (args[0])
                {
                    case "Up":
                    case "up":
                        direct = Direction.Up;
                        break;
                    case "Down":
                    case "down":
                        direct = Direction.Down;
                        break;
                    case "Left":
                    case "left":
                        direct = Direction.Left;
                        break;
                    case "Right":
                    case "right":
                        direct = Direction.Right;
                        break;
                    default:
                        direct = Direction.Unknown;
                        break;
                }
                MultiPlayerInfoPackage gameInfo = this.model.GetGame(player);
                if (direct == Direction.Unknown) {
                    throw new GameException("Bad play arguement", false);
                }
                if (gameInfo == null) {
                    throw new GameException("You are not in a game session, please try again!", false);
                }
                IPlayer enemy;
                if (player.Equals(gameInfo.Host)) {
                    enemy = gameInfo.Guest;
                }
                else {
                    enemy = gameInfo.Host;
                }
                //Creates the json and sends it to the enemy player.
                JObject jobj = new JObject();
                jobj["Name"] = gameInfo.Maze.Name;
                jobj["Direction"] = direct.ToString();
                enemy.SendMessage(jobj.ToString());
                return "keep";
            }
        }
    }
}