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

namespace MazeGUI {
    /// <summary>
    /// Interaction logic for SettingsForm.xaml
    /// </summary>
    public partial class SettingsForm : Window {
        public SettingsForm() {
            InitializeComponent();
            this.txtbxRows.Text = ConfigurationManager.AppSettings["rows"];
            this.txtbxCols.Text = ConfigurationManager.AppSettings["cols"];
            this.txtbxServerIP.Text = ConfigurationManager.AppSettings["ip"];
            this.txtbxServerPort.Text = ConfigurationManager.AppSettings["port"];
            this.cmbAlgo.SelectedIndex = GetIndexOfAlgoValue(ConfigurationManager.AppSettings["algo"]);
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
            ApplySetting("rows", this.txtbxRows.Text);
            ApplySetting("cols", this.txtbxCols.Text);
            ApplySetting("algo", this.cmbAlgo.SelectionBoxItem.ToString());
            ApplySetting("ip", this.txtbxServerIP.Text);
            ApplySetting("port", this.txtbxServerPort.Text);
        }

        private void ApplySetting(string key, string value) {
            try {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null) {
                    settings.Add(key, value);
                }
                else {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException) {
                Console.WriteLine("Error writing app settings");
            }
        }
    }
}