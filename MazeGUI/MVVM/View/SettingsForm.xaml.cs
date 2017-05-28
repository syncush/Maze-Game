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
using System.Configuration;
using MazeGUI.ViewModels;

namespace MazeGUI{
    /// <summary>
    /// Interaction logic for SettingsForm.xaml
    /// </summary>
    public partial class SettingsForm : Window {
        /// <summary>
        /// The settings vm
        /// </summary>
        private SettingsViewModel settingsVM;
        public SettingsForm() {
            InitializeComponent();
            this.settingsVM = new SettingsViewModel();
            this.DataContext = settingsVM;
        }

        private int GetIndexOfAlgoValue(string value) {
            try {
                switch (value.ToLower()) {
                    case "dfs": {
                        return 0;
                    }
                        break;
                    case "bfs": {
                        return 1;
                    }
                        break;
                }
            }
            catch (Exception) {
                return -1;
            }

            return -1;
        }

        /// <summary>
        /// Handles the Click event of the btnSaveSettings control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnSaveSettings_Click(object sender, RoutedEventArgs e) {
            this.settingsVM.SaveSettings();
            MessageBoxResult mBox = MessageBox.Show("Settings Saved ", "Confirmation", MessageBoxButton.OK,
                MessageBoxImage.Information);
        }


        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        /// <summary>
        /// Handles the Closed event of the frmSettings control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void frmSettings_Closed(object sender, EventArgs e) {
           
           
        }

        /// <summary>
        /// Handles the PreviewTextInput event of the txtbxCols control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TextCompositionEventArgs"/> instance containing the event data.</param>
        private void txtbxCols_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        /// <summary>
        /// Handles the PreviewTextInput event of the txtbxServerIP control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TextCompositionEventArgs"/> instance containing the event data.</param>
        private void txtbxServerIP_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1) && e.Text != ".")
                e.Handled = true;
        }

        private void frmSettings_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult mBox = MessageBox.Show("Go Back To MainMenu ?", "Confirmation", MessageBoxButton.OK,
                MessageBoxImage.Information);
                MainMenu main = new MainMenu();
                main.Show();
            
        }
    }
}