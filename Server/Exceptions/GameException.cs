using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Exception" />
    class GameException : Exception {
        /// <summary>
        /// The should terminate connection
        /// </summary>
        private Boolean shouldTerminateConnection;
        public GameException(string mess, Boolean terminateConnection) : base(mess) {
            this.shouldTerminateConnection = terminateConnection;
        }

        /// <summary>
        /// Gets a value indicating whether [connection terminate].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [connection terminate]; otherwise, <c>false</c>.
        /// </value>
        public Boolean ConnectionTerminate {
            get { return this.shouldTerminateConnection; }
        }
    }
}
