using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Sockets;
using System.Net;
using System.Configuration;
using System.Threading;
using ClientLib;

/// <summary>
/// 
/// </summary>
namespace Client {
    /// <summary>
    /// Client main
    /// </summary>
    class Program {
        static void Main(string[] args) {

            
            int port = int.Parse(ConfigurationManager.AppSettings["port"]);
            string ip = ConfigurationManager.AppSettings["ip"];
            Thread.Sleep(1000);
            IClient client = new ClientLib.Client(ip, port);
            client.Start();
        }
    }
}