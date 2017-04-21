using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerLib
{
    interface IController
    {
        /// <summary>
        /// Executes the command given by the client.
        /// </summary>
        /// <param name="arg">The argument.</param>
        /// <param name="client">The client.</param>
        /// <returns>result of the command</returns>
        string ExecuteCommand(string args, Player player, TcpClient client = null);
    }
}
