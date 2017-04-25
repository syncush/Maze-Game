using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeGUI.DataSources;
using MazeGUI.Models;

namespace MazeGUI.ViewModels {
    class SettingsViewModel {
        private SettingsModel model;

        public SettingsViewModel() {
            this.model = new SettingsModel();
        }

        #region Properties
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

        public uint ServerPort
        {
            set { this.model.ServerPort = value; }
            get { return this.model.ServerPort; }
        }

        public uint MazeRows
        {
            set { this.model.Rows = value; }
            get { return this.model.Rows; }
        }

        public uint MazeCols
        {
            set { this.model.Cols = value; }
            get { return this.model.Cols; }
        }

        public uint Algorithm
        {
            get { return this.model.Algorithm; }
            set { this.model.Algorithm = value; }
        }
        public void SaveSettings()
        {
            this.model.SaveSettings();
        }


        #endregion Properties

    }
}