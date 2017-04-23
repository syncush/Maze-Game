using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeGUI.DataSources;
using MazeGUI.Models;

namespace MazeGUI.ViewModels
{
    class SinglePlayerSettingsViewModel {
        private IDataSource dataSource;

        public SinglePlayerSettingsViewModel() {
            dataSource = new SettingsModel();
        }

        public uint Rows {
            get { return this.dataSource.Rows; }
            set { this.dataSource.Rows = value; }
        }

        public uint Cols {
            get { return this.dataSource.Cols; }
            set { this.dataSource.Cols = value; }
        }
    }
}
