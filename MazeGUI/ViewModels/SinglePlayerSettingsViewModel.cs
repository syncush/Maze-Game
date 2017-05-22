using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MazeGUI.Annotations;
using MazeGUI.DataSources;
using MazeGUI.Models;
using MazeLib;

namespace MazeGUI.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    class SinglePlayerSettingsViewModel : ViewModel {
        #region DataMembers
        private IDataSource dataSource;
        private string gameName;
        #endregion DataMembers

        #region Events

        public event GameMovement GameClientMovement;

        public delegate void GameMovement(MazeLib.Direction p);


        public delegate void GameFinished(bool iWon);

        public event GameFinished GameFinishedEvent;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="SinglePlayerSettingsViewModel"/> class.
        /// </summary>
        public SinglePlayerSettingsViewModel() : base() {
            dataSource = new SettingsModel();
        }

        /// <summary>
        /// Gets or sets the rows.
        /// </summary>
        /// <value>
        /// The rows.
        /// </value>
        public uint Rows {
            get { return this.dataSource.Rows; }
            set { this.dataSource.Rows = value; }
        }

        /// <summary>
        /// Gets or sets the cols.
        /// </summary>
        /// <value>
        /// The cols.
        /// </value>
        public uint Cols {
            get { return this.dataSource.Cols; }
            set { this.dataSource.Cols = value; }
        }

       

        /// <summary>
        /// Gets or sets the name of the game.
        /// </summary>
        /// <value>
        /// The name of the game.
        /// </value>
        public string GameName {
            get { return this.gameName; }
            set {
                this.gameName = value;
                NotifyPropertyChanged("GameName");
            }
        }
    }
}
