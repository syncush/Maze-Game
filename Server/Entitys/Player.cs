using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Server;

namespace ServerLib {
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="IPlayer" />
    class Player : IPlayer {
        private static int idCounter = 0;
        protected int playerID;
        protected TcpClient clientComms;
        private StreamWriter writer;
        private int sessionCounter;

        public bool Busy {
            get { return sessionCounter > 0 ? true : false; }
            set {
                if (value) {
                    ++this.sessionCounter;
                }
                else {
                    --this.sessionCounter;
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="commsClient">The comms client.</param>
        public Player(TcpClient commsClient) {
            this.playerID = idCounter++;
            this.clientComms = commsClient;
            writer = new StreamWriter(commsClient.GetStream());
            writer.AutoFlush = true;
        }

        /// <summary>
        /// Sends the message to the player.
        /// </summary>
        /// <param name="message">The message.</param>
        public virtual void SendMessage(string message) {
            writer.WriteLine(message.Replace("\r\n", ""));
        }

        /// <summary>
        /// Equalses the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public bool Equals(Player obj) {
            return this.playerID == obj.playerID;
        }

        /// <summary>
        /// Closes the connection.
        /// </summary>
        public void CloseConnection() {
            //If the client is busy in other session don't close the connection.
            if (this.Busy) {
                return;
            }
            this.clientComms.Close();
        }
    }
}