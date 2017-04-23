using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeGUI.DataSources;

namespace MazeGUI.ViewModels {
    class SettingsViewModel {
        private IDataSource model;

        public SettingsViewModel() {
            this.model = new AppConfigDataSource();
        }

        public string ServerAddress {
            get { return this.model.ServerIP; }
            set {
                try {
                    this.model.ServerIP = value;
                }
                catch (Exception e) {
                }
            }
        }

        public uint ServerPort {
            set { this.model.ServerPort = value; }
            get { return this.model.ServerPort; }
        }

        public uint MazeRows {
            set { this.model.Rows = value; }
            get { return this.model.Rows; }
        }
    }
}