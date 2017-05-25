using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MazeGUI.Annotations;
using MazeGUI.DataSources;
using MazeLib;
using MazeGUI.Models;
using Newtonsoft.Json.Linq;

/// <summary>
/// 
/// </summary>
namespace MazeGUI.ViewModels {
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    class MultiPlayerSettingsViewModel : ViewModel {

        private MultiPlayerSettingsModel model;
        public delegate void BadArguments(string message);
        public event BadArguments BadArgumentsEvent;

        public delegate void ConnectionLost(string mess);

        public event ConnectionLost ConnectionLostEvent;
        /// <summary>
        /// Initializes a new instance of the <see cref="MultiPlayerSettingsViewModel"/> class.
        /// </summary>
        public MultiPlayerSettingsViewModel() : base() {
            this.model = new MultiPlayerSettingsModel();
            this.model.ConnectionLostEvent += HandleConnection;
            this.model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                this.NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

        private void HandleConnection(string mess) {
            this.ConnectionLostEvent?.Invoke(mess);
        }
        /// <summary>
        /// Handles the bad arguments.
        /// </summary>
        /// <param name="mess">The mess.</param>
        public void HandleBadArguments(string mess)
        {
            this.BadArgumentsEvent?.Invoke(mess);
        }
        /// <summary>
        /// Gets or sets the vm rows.
        /// </summary>
        /// <value>
        /// The vm rows.
        /// </value>
        public uint VM_Rows
        {
            get
            {
                return this.model.Rows;
            }
            set
            {
                this.model.Rows = value;
            }
        }
        /// <summary>
        /// Sets a value indicating whether this <see cref="MultiPlayerSettingsViewModel"/> is stop.
        /// </summary>
        /// <value>
        ///   <c>true</c> if stop; otherwise, <c>false</c>.
        /// </value>
        public Boolean Stop
        {
            set { this.model.Stop = true; }
        }
        /// <summary>
        /// Gets or sets the vm cols.
        /// </summary>
        /// <value>
        /// The vm cols.
        /// </value>
        public uint VM_Cols
        {
            get
            {
                return this.model.Cols;
            }
            set
            {
                this.model.Cols = value;
            }
        }
        /// <summary>
        /// Gets or sets the name of the vm game.
        /// </summary>
        /// <value>
        /// The name of the vm game.
        /// </value>
        public String VM_GameName
        {
            get
            {
                return this.model.GameName;
            }
            set
            {
                this.model.GameName = value;
            }
        }
        /// <summary>
        /// Gets the vm avaiable games list.
        /// </summary>
        /// <value>
        /// The vm avaiable games list.
        /// </value>
        public ObservableCollection<string> VM_AvaiableGamesList
        {
            get
            {
                return this.model.AvaiableGamesList;
            }
            set
            {
                this.model.AvaiableGamesList = value;
            }
        }
        /// <summary>
        /// Gets or sets the vm index selected.
        /// </summary>
        /// <value>
        /// The vm index selected.
        /// </value>
        public int VM_IndexSelected
        {
            get
            {
                return this.model.IndexSelected;
            }
            set
            {
                this.model.IndexSelected = value;
            }
        }
    }
}