using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Server.Exceptions;

namespace ServerLib {
    /// <summary>
    /// Describes the close command .
    /// </summary>
    /// <seealso cref="ServerLib.Command" />
     class CloseCommand : Command {

        /// <summary>
        /// Initializes a new instance of the <see cref="CloseCommand"/> class.
        /// </summary>
        /// <param name="model">The model.</param>
        public CloseCommand(IModel model) : base(model) {
           
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="client">The client.</param>
        /// <returns>
        /// JSON result of the command
        /// </returns>
        public override string ExecuteCommand(string[] args, IPlayer client = null) {
            //Lock the resource , threadsafe
            lock (this.lockRaceCondition) {
                string name = args[0];
                //Get the enemy player of the current player.
                IPlayer enemy = this.model.GetEnemy(client);
                Boolean success = this.model.Close(name);
                //if opreation failed , throw exception
                if (!success) {
                    throw new GameException(String.Format("Game {0} could not close properly, please try again!", name), false);
                }
                else { //opreation succeeded
                    //Create Jobj with details and send it to the players, close their connection.
                    JObject emptyJObject = new JObject();
                    client.SendMessage(emptyJObject.ToString());
                    enemy.SendMessage(emptyJObject.ToString());
                    client.CloseConnection();
                    enemy.CloseConnection();
                    return "Shutdown";
                }
               
            }
            
        }
    }
}