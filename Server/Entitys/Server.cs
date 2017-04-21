using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ServerLib {
    /// <summary>
    /// Describes a server.
    /// </summary>
    /// <seealso cref="ServerLib.IServer" />
    public class Server : IServer {
        //Class members.
        private int port;
        private string ip;
        private IClientHandler ch;
        private TcpListener listener;

        /// <summary>
        /// Initializes a new instance of the <see cref="Server"/> class.
        /// </summary>
        /// <param name="port">The port.</param>
        /// <param name="ip">The ip.</param>
        /// <param name="ch">The ch.</param>
        public Server(int port, string ip, IClientHandler ch) {
            this.ip = ip;
            this.port = port;
            this.ch = ch;
        }

        /// <summary>
        /// Establish the server and from that point will be able to recieve clients
        /// </summary>
        public void Start() {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(this.ip), this.port);
            listener = new TcpListener(endPoint);
            listener.Start();
            Task task = new Task(() => {
                while (true) {
                    try {
                        TcpClient client = listener.AcceptTcpClient();
                        Console.WriteLine("Got new connection");
                        ch.Handle(client);
                    }
                    catch (SocketException) {
                        break;
                    }
                }
                Console.WriteLine("Server stopped");
            });
            task.Start();
            task.Wait();
        }
        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop() {
            listener.Stop();
        }
    }
}