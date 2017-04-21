using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace MazeGUI {
    /// <summary>
    /// Interaction logic for SinglePlayerSettingsForm.xaml
    /// </summary>
    public partial class SinglePlayerSettingsForm : Window {
        public SinglePlayerSettingsForm() {
            InitializeComponent();
            this.txtbxMazeCols.Text = ConfigurationManager.AppSettings["cols"];
            this.txtbxMazeRows.Text = ConfigurationManager.AppSettings["rows"];
        }

        private void btnMainMenu_Click(object sender, RoutedEventArgs e) {
            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void txtbxMazeRows_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }
    }
}