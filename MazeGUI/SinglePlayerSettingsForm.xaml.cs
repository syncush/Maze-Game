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

        private void btnStart_Click(object sender, RoutedEventArgs e) {
            int rows = int.Parse(this.txtbxMazeRows.Text);
            int cols = int.Parse(this.txtbxMazeCols.Text);
            string gameName = this.txtbxMazeName.Text;
            SinglePlayerForm form = new SinglePlayerForm(rows, cols, gameName);
            form.Show();
            this.Close();

        }

        private void btnMainMenu_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mBox = MessageBox.Show("Go Back To MainMenu ?", "Confirmation", MessageBoxButton.OK,
                MessageBoxImage.Information);
            if (mBox == MessageBoxResult.OK)
            {
                
                this.Close();
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MainMenu main = new MainMenu();
            main.Show();
            this.Close();
        }
    }
}
