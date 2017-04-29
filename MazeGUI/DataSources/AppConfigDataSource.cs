using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MazeGUI.DataSources {
    /// <summary>
    /// Describes a app config data source.
    /// </summary>
    class AppConfigDataSource : IDataSource {
        /// <summary>
        /// Gets or sets the server ip.
        /// </summary>
        /// <value>
        /// The server ip.
        /// </value>
        public string ServerIP {
            get { return Properties.Settings.Default.ServerIP; }
            set { Properties.Settings.Default.ServerIP = value; }
        }

        /// <summary>
        /// Gets or sets the server port.
        /// </summary>
        /// <value>
        /// The server port.
        /// </value>
        public uint ServerPort {
            get { return (Properties.Settings.Default.ServerPort); }
            set { Properties.Settings.Default.ServerPort = value; }
        }

        /// <summary>
        /// Gets or sets the rows.
        /// </summary>
        /// <value>
        /// The rows.
        /// </value>
        public uint Rows {
            get { return Properties.Settings.Default.Rows; }
            set { Properties.Settings.Default.Rows = value; }
        }

        /// <summary>
        /// Gets or sets the cols.
        /// </summary>
        /// <value>
        /// The cols.
        /// </value>
        public uint Cols {
            get { return Properties.Settings.Default.Cols; }
            set { Properties.Settings.Default.Cols = value; }
        }

        /// <summary>
        /// Saves the settings.
        /// </summary>
        public void SaveSettings() {
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Gets or sets the algorithm.
        /// </summary>
        /// <value>
        /// The algorithm.
        /// </value>
        public uint Algorithm {
            get { return Properties.Settings.Default.Algorithm; }
            set { Properties.Settings.Default.Algorithm = value; }
        }
    }
}