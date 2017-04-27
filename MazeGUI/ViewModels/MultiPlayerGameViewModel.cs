using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MazeLib;

namespace MazeGUI.ViewModels {
    class MultiPlayerGameViewModel {
        #region ClientCommunication

        private TcpClient    rivalMovementClient;
        private StreamReader reader;
        private StreamWriter writer;
        private Task         rivalMovementListener;
        public event RivalMoved RivalMovedEvent;
        public delegate void RivalMoved(Direction p);

        #endregion

        public MultiPlayerGameViewModel(string gameName) {
            
        }

    }
}