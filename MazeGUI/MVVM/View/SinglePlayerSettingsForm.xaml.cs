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
        Boolean isBackToMain;
        /// <summary>
        /// Initializes a new instance of the <see cref="SinglePlayerSettingsForm"/> class.
        /// </summary>
        public SinglePlayerSettingsForm()
        {
            InitializeComponent();
            settingsVM = new SinglePlayerSettingsViewModel();
            this.DataContext = settingsVM;
            this.txtbxMazeCols.DataContext = settingsVM;
            this.txtbxMazeRows.DataContext = settingsVM;
           
            
        }


        /// <summary>
        /// Handles the Click event of the btnStart control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnStart_Click(object sender, RoutedEventArgs e) {
            int rows = int.Parse(this.txtbxMazeRows.Text);
            int cols = int.Parse(this.txtbxMazeCols.Text);
            string gameName = this.txtbxMazeName.Text;
            SinglePlayerForm form = new SinglePlayerForm(rows, cols, gameName);
            form.Show();
            isBackToMain = false;
            this.Close();

        }

        /// <summary>
        /// Handles the Click event of the btnMainMenu control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnMainMenu_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mBox = MessageBox.Show("Go Back To MainMenu ?", "Confirmation", MessageBoxButton.OK,
                MessageBoxImage.Information);
            if (mBox == MessageBoxResult.OK) {
                this.isBackToMain = true;
                this.Close();
            }
        }

        /// <summary>
        /// Handles the Closed event of the Window control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Window_Closed(object sender, EventArgs e)
        {
            if(isBackToMain)
            {
                MainMenu main = new MainMenu();
                main.Show();
            }
            this.Close();
        }
        public void HandleConnectionLost()
        {
            MessageBoxResult mBox = MessageBox.Show("Connection to server has been lost !", "Connection lost", MessageBoxButton.OK,
               MessageBoxImage.Information);
            this.isBackToMain = true;
            this.Close();
        }
    }
}
