using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeGUI.DataSources;
using MazeGUI.Models;

namespace MazeGUI.ViewModels {
    /// <summary>
    /// 
    /// </summary>
    class SettingsViewModel {
        /// <summary>
        /// The model
        /// </summary>
        private SettingsModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsViewModel"/> class.
        /// </summary>
        public SettingsViewModel() {
            this.model = new SettingsModel();
        }

        #region Properties
        /// <summary>
        /// Gets or sets the server address.
        /// </summary>
        /// <value>
        /// The server address.
        /// </value>
        public string ServerAddress
        {
            get { return this.model.ServerIP; }
            set
            {
                try
                {
                    this.model.ServerIP = value;
                }
                catch (Exception e)
                {
                }
            }
        }

        /// <summary>
        /// Gets or sets the server port.
        /// </summary>
        /// <value>
        /// The server port.
        /// </value>
        public uint ServerPort
        {
            set { this.model.ServerPort = value; }
            get { return this.model.ServerPort; }
        }

        /// <summary>
        /// Gets or sets the maze rows.
        /// </summary>
        /// <value>
        /// The maze rows.
        /// </value>
        public uint MazeRows
        {
            set { this.model.Rows = value; }
            get { return this.model.Rows; }
        }

        /// <summary>
        /// Gets or sets the maze cols.
        /// </summary>
        /// <value>
        /// The maze cols.
        /// </value>
        public uint MazeCols
        {
            set { this.model.Cols = value; }
            get { return this.model.Cols; }
        }

        /// <summary>
        /// Gets or sets the algorithm.
        /// </summary>
        /// <value>
        /// The algorithm.
        /// </value>
        public uint Algorithm
        {
            get { return this.model.Algorithm; }
            set { this.model.Algorithm = value; }
        }
        /// <summary>
        /// Saves the settings.
        /// </summary>
        public void SaveSettings()
        {
            this.model.SaveSettings();
        }


        #endregion Properties

    }
}