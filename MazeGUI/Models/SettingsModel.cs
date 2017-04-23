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
    class SettingsModel : INotifyPropertyChanged,IDataSource {
        public event PropertyChangedEventHandler PropertyChanged;
        private IDataSource dataSource;
        public ObservableCollection<String> algortihmsCollec;

        public SettingsModel () {
            dataSource = new AppConfigDataSource();

        }
        

        [NotifyPropertyChangedInvocator]
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public uint Rows {
            get { return this.dataSource.Rows; }
            set {
                this.dataSource.Rows = value;
                NotifyPropertyChanged("Rows");
            }
        }

        public uint Cols {
            get { return this.dataSource.Cols; }
            set {
                this.dataSource.Cols = value;
                NotifyPropertyChanged("Cols");
            }
        }

        public string ServerIP {
            get { return this.dataSource.ServerIP; }
            set {
                this.dataSource.ServerIP = value;
                NotifyPropertyChanged("ServerIP");
            }
        }

        public uint ServerPort {
            get { return this.dataSource.ServerPort; }
            set {
                this.dataSource.ServerPort = value;
                NotifyPropertyChanged("ServerPort");
            }
        }

        public void SaveSettings() {
            this.dataSource.SaveSettings();
        }

        public uint Algorithm {
            get { return this.dataSource.Algorithm; }
            set {
                this.dataSource.Algorithm = value;
                NotifyPropertyChanged("Algorithm");
            }
        }
    }
}