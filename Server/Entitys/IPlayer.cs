using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace ServerLib
{
    /// <summary>
    /// Class describes an interface of a maze player.
    /// </summary>
    interface IPlayer {
        /// <summary>
        /// Sends the message to the player.
        /// </summary>
        /// <param name="message">The message.</param>
        void SendMessage(string message);

        /// <summary>
        /// Closes the connection.
        /// </summary>
        void CloseConnection();

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="IPlayer"/> is busy.
        /// </summary>
        /// <value>
        ///   <c>true</c> if busy; otherwise, <c>false</c>.
        /// </value>
        Boolean Busy { get; set; }
    }
}
