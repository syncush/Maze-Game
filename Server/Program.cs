using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerLib;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Configuration;

namespace Server {
    /// <summary>
    /// Runs a server.
    /// </summary>
    class Program {
        /// <summary>
        /// Mains the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args) {
            int port = int.Parse(ConfigurationManager.AppSettings["port"]);
            string ip = ConfigurationManager.AppSettings["ip"];
            IServer server = new ServerLib.Server(port, ip, new ClientHandler());
            server.Start();
            
        }
    }
}