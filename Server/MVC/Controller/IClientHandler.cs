using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerLib
{
    public interface IClientHandler
    {
        /// <summary>
        /// Handles the specified client.
        /// </summary>
        /// <param name="client">The client.</param>
        void Handle(TcpClient client);
    }
}
