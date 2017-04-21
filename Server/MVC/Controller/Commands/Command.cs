using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ServerLib {
    /// <summary>
    /// An abstract class describes a request from client to server.
    /// </summary>
    /// <seealso cref="ServerLib.ICommand" />
     abstract class Command : ICommand {
        /// <summary>
        /// The model
        /// </summary>
        protected readonly IModel model;
        /// <summary>
        /// The lock race condition
        /// </summary>
        protected readonly object lockRaceCondition;

        /// <summary>
        /// Initializes a new instance of the <see cref="Command"/> class.
        /// </summary>
        /// <param name="mod">The mod.</param>
        public Command(IModel mod) {
            this.model = mod;
            lockRaceCondition = new object();
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="player"></param>
        /// <returns>
        /// JSON result of the command
        /// </returns>
        public abstract string ExecuteCommand(string[] args, IPlayer player = null);
    }
}