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
    /// Interaction logic for MultiPlayerSettingsForm.xaml
    /// </summary>
    public partial class MultiPlayerSettingsForm : Window {
        public MultiPlayerSettingsForm() {
            InitializeComponent();
            this.txtbxMazeRows.Text = ConfigurationManager.AppSettings["rows"];
            this.txtbxMazeCols.Text = ConfigurationManager.AppSettings["cols"];
        }

        private void NumericTxtBox(object sender, TextCompositionEventArgs e) {
            if (!char.IsDigit(e.Text, e.Text.Length - 1)) {
                e.Handled = true;
            }
        }
    }
}