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

namespace MazeGUI {
    /// <summary>
    /// Interaction logic for MultiPlayerSettingsForm.xaml
    /// </summary>
    public partial class MultiPlayerSettingsForm : Window {
        private MultiPlayerSettingsViewModel mpVP;
        public MultiPlayerSettingsForm() {
            InitializeComponent();
            mpVP = new MultiPlayerSettingsViewModel();
            mpVP.Intialize();
            this.DataContext = mpVP;
            


        }

        private void NumericTxtBox(object sender, TextCompositionEventArgs e) {
            if (!char.IsDigit(e.Text, e.Text.Length - 1)) {
                e.Handled = true;
            }
        }

    }
}
