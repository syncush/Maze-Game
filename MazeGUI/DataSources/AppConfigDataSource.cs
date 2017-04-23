using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MazeGUI.DataSources {
    /// <summary>
    /// 
    /// </summary>
    class AppConfigDataSource : IDataSource {
        public string ServerIP {
            get { return Properties.Settings.Default.ServerIP; }
            set { Properties.Settings.Default.ServerIP = value; }
        }

        public uint ServerPort {
            get { return (Properties.Settings.Default.ServerPort); }
            set { Properties.Settings.Default.ServerPort = value; }
        }

        public uint Rows {
            get { return Properties.Settings.Default.Rows; }
            set { Properties.Settings.Default.Rows = value; }
        }

        public uint Cols {
            get { return Properties.Settings.Default.Cols; }
            set { Properties.Settings.Default.Cols = value; }
        }

        public void SaveSettings() {
            Properties.Settings.Default.Save();
        }

        public uint Algorithm {
            get { return Properties.Settings.Default.Algorithm; }
            set { Properties.Settings.Default.Algorithm = value; }
        }
    }
}