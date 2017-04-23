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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MazeGUI {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e) {
            SettingsForm form = new SettingsForm();
            form.Show();
            this.Close();
        }

        private void btnSingle_Click(object sender, RoutedEventArgs e) {
            SinglePlayerSettingsForm spsForm = new SinglePlayerSettingsForm();
            spsForm.Show();
            this.Close();
        }

        private void btnMultiplayer_Click(object sender, RoutedEventArgs e) {
            MultiPlayerSettingsForm mpFormSettings = new MultiPlayerSettingsForm();
            mpFormSettings.Show();
            this.Close();
        }
    }
}