using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerLib
{
    /// <summary>
    /// An interface of a command , must have an execute func.
    /// </summary>
     interface ICommand
    {
        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="client">The player who requested the command.</param>
        /// <returns>JSON result of the command</returns>
        string ExecuteCommand(string[] args, IPlayer player);
    }
}
