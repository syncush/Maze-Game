using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Server.Exceptions;

namespace ServerLib {
    /// <summary>
    /// Resposisble to handle client.
    /// </summary>
    /// <seealso cref="ServerLib.IClientHandler" />
    public class ClientHandler : IClientHandler {
        private readonly IController controller;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientHandler"/> class.
        /// </summary>
        /// <param name="cont">The cont.</param>
        public ClientHandler() {
            this.controller = new Controller();
        }

        /// <summary>
        /// Handles the specified client.
        /// </summary>
        /// <param name="client">The client.</param>
        public void Handle(TcpClient client) {
            //Create a new task for each client.
            new Task(() => {
                //Intialize player object.
                Player player = null;
                int counter = 0;
                try {
                    using (NetworkStream stream = client.GetStream())
                    using (StreamReader reader = new StreamReader(stream))
                    using (StreamWriter writer = new StreamWriter(stream)) {
                        while (true) {
                            try {
                                string commandLine = reader.ReadLine();
                                if (commandLine != null) {
                                    Console.WriteLine("Got command: {0}", commandLine);
                                    if (counter == 0) {
                                        player = new Player(client);
                                    }
                                    //Execute the command.
                                    string result = this.controller.ExecuteCommand(commandLine, player);
                                    counter++;
                                }
                            }
                            //If caught an exception check if connection should be close and send the error message.
                            catch (GameException gameException) {
                                player.SendMessage(gameException.Message);
                                if (gameException.ConnectionTerminate) {
                                    player.CloseConnection();
                                }
                            }
                        }
                    }
                }
                catch (Exception e) {
                }
            }).Start();
        }
    }
}