using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientLib {
    /// <summary>
    /// A concrete class of IClient, represents a client 
    /// who sends requests to the maze server and print 
    /// it's responses
    /// </summary>
    /// <seealso cref="ClientLib.IClient" />
    public class Client : IClient {
        private readonly static string SHUTDOWNCLIENTCOMMAND = "exit";

        /// <summary>
        /// boolean that indicates if client is online.
        /// </summary>
        public static Boolean isOnline = false;

        //Class members
        private string ip;
        private int port;
        private TcpClient client;
        private StreamWriter writer;
        private StreamReader reader;
        private Task handleServerCommsTask;

        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        /// <param name="ip">The ip.</param>
        /// <param name="port">The port.</param>
        public Client(string ip, int port) {
            this.ip = ip;
            this.port = port;
            client = null;
            writer = null;
            reader = null;
            handleServerCommsTask = new Task(() => { this.ListenThread(); });
        }

        /// <summary>
        /// Listens the thread.
        /// </summary>
        private void ListenThread() {
            while (true) {
                try {
                    string data = reader.ReadLine();
                    if (data != null) {
                        Console.WriteLine(data);
                    }
                    else {
                        Client.isOnline = false;
                        client.Close();
                        break;
                    }
                }
                catch (Exception e) {
                    Console.WriteLine("I got exceptioned ," + e.Message);
                    Client.isOnline = false;
                    client.Close();
                    break;
                }
            }
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start() {
            //Creates an end point 
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(this.ip), this.port);

            while (true) {
                try {
                    Console.WriteLine("Enter a command (type exit to quit):");
                    string s = Console.ReadLine();
                    if (s.ToLower() == SHUTDOWNCLIENTCOMMAND) {
                        break;
                    }
                    //Start a new client and intialize streams.
                    if (!isOnline) {
                        this.client = new TcpClient();
                        this.client.Connect(ep);
                        this.reader = new StreamReader(client.GetStream());
                        this.writer = new StreamWriter(client.GetStream());
                        //Console.WriteLine("Made new connection");
                        writer.AutoFlush = true;
                        isOnline = true;
                        //Run a listener thread.
                        handleServerCommsTask = new Task(() => { this.ListenThread(); });
                        handleServerCommsTask.Start();
                    }
                    writer.WriteLine(s);
                }
                catch (SocketException) {
                    Client.isOnline = false;
                    client.Close();
                }
            }
        }
    }
}