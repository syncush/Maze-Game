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

        private void btnSaveSettings_Click(object sender, RoutedEventArgs e) {
            this.settingsVM.SaveSettings();
        }


        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void frmSettings_Closed(object sender, EventArgs e) {
            MessageBoxResult mBox = MessageBox.Show("Go Back To MainMenu ?", "Confirmation", MessageBoxButton.OK,
                MessageBoxImage.Information);
            if (mBox == MessageBoxResult.OK)
            {
                MainMenu main = new MainMenu();
                main.Show();
                this.Close();
            }
           
        }

        private void txtbxCols_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }

        private void txtbxServerIP_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1) && e.Text != ".")
                e.Handled = true;
        }
    }
}