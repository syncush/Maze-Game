using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeGUI.DataSources {
    public  enum Algorithm {
        DFS = 0,BFS = 1, NONE = -1
    }
    interface IDataSource {
        /// <summary>
        /// Gets or sets the server ip.
        /// </summary>
        /// <value>
        /// The server ip.
        /// </value>
        String ServerIP { get; set; }
        /// <summary>
        /// Gets or sets the server port.
        /// </summary>
        /// <value>
        /// The server port.
        /// </value>
        uint ServerPort { get; set; }
        /// <summary>
        /// Gets or sets the rows.
        /// </summary>
        /// <value>
        /// The rows.
        /// </value>
        uint Rows { get; set; }
        /// <summary>
        /// Gets or sets the cols.
        /// </summary>
        /// <value>
        /// The cols.
        /// </value>
        uint Cols { get; set; }

        /// <summary>
        /// Saves the settings.
        /// </summary>
        void SaveSettings();

        /// <summary>
        /// Gets or sets the algorithm.
        /// </summary>
        /// <value>
        /// The algorithm.
        /// </value>
        uint Algorithm { set; get; }
    }
}