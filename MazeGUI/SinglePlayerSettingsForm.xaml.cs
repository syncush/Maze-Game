using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MazeGUI.ViewModels;

namespace MazeGUI
{
    /// <summary>
    /// Interaction logic for SinglePlayerSettingsForm.xaml
    /// </summary>
    public partial class SinglePlayerSettingsForm : Window {
        private SinglePlayerSettingsViewModel settingsVM;
        public SinglePlayerSettingsForm()
        {
            InitializeComponent();
            settingsVM = new SinglePlayerSettingsViewModel();
            this.DataContext = settingsVM;
            this.txtbxMazeCols.DataContext = settingsVM;
            this.txtbxMazeRows.DataContext = settingsVM;
        }
    }
}
