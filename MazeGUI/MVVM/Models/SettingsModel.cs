using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using MazeGUI.Annotations;
using MazeGUI.DataSources;

namespace MazeGUI.Models {
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    /// <seealso cref="MazeGUI.DataSources.IDataSource" />
    class SettingsModel : INotifyPropertyChanged,IDataSource {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        private IDataSource dataSource;
        public ObservableCollection<String> algortihmsCollec;

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsModel"/> class.
        /// </summary>
        public SettingsModel () {
            dataSource = new AppConfigDataSource();

        }


        /// <summary>
        /// Notifies the property changed.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        [NotifyPropertyChangedInvocator]
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Gets or sets the rows.
        /// </summary>
        /// <value>
        /// The rows.
        /// </value>
        public uint Rows {
            get { return this.dataSource.Rows; }
            set {
                this.dataSource.Rows = value;
                NotifyPropertyChanged("Rows");
            }
        }

        /// <summary>
        /// Gets or sets the cols.
        /// </summary>
        /// <value>
        /// The cols.
        /// </value>
        public uint Cols {
            get { return this.dataSource.Cols; }
            set {
                this.dataSource.Cols = value;
                NotifyPropertyChanged("Cols");
            }
        }

        /// <summary>
        /// Gets or sets the server ip.
        /// </summary>
        /// <value>
        /// The server ip.
        /// </value>
        public string ServerIP {
            get { return this.dataSource.ServerIP; }
            set {
                this.dataSource.ServerIP = value;
                NotifyPropertyChanged("ServerIP");
            }
        }

        /// <summary>
        /// Gets or sets the server port.
        /// </summary>
        /// <value>
        /// The server port.
        /// </value>
        public uint ServerPort {
            get { return this.dataSource.ServerPort; }
            set {
                this.dataSource.ServerPort = value;
                NotifyPropertyChanged("ServerPort");
            }
        }

        /// <summary>
        /// Saves the settings.
        /// </summary>
        public void SaveSettings() {
            this.dataSource.SaveSettings();
        }

        /// <summary>
        /// Gets or sets the algorithm.
        /// </summary>
        /// <value>
        /// The algorithm.
        /// </value>
        public uint Algorithm {
            get { return this.dataSource.Algorithm; }
            set {
                this.dataSource.Algorithm = value;
                NotifyPropertyChanged("Algorithm");
            }
        }
    }
}