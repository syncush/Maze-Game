using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MazeGUI.DataSources;
using MazeLib;

namespace MazeGUI.Models {
    class MultiPlayerModel {
        #region DataMembers

        private IDataSource dataSource;
        private Maze gameMaze;
        private Position clientPosition;
        private Position rivalPosition;

        #endregion

        public MultiPlayerModel(Maze game) {
            this.dataSource = new AppConfigDataSource();
            this.gameMaze = game;
            this.clientPosition = game.InitialPos;
            this.rivalPosition = game.InitialPos;
        }

        public IPEndPoint EndPoint {
            get {
                return new IPEndPoint(IPAddress.Parse(this.dataSource.ServerIP),
                    Convert.ToInt32(this.dataSource.ServerPort));
            }
        }

        public Position RivalPosition {
            get { return this.rivalPosition; }
            set { this.rivalPosition = value; }
        }

        public Position ClientPosition {
            get { return this.clientPosition; }
            set { this.clientPosition = value; }
        }
    }
}