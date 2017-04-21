using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLib {
    /// <summary>
    /// Describes an interface of a maze server.
    /// </summary>
    public interface IServer {
        /// <summary>
        /// Establish the server and from that point will be able to recieve clients
        /// </summary>
        void Start();
    }
}