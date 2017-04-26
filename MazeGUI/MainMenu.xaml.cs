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

namespace MazeGUI
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void btnSingle_Click(object sender, RoutedEventArgs e)
        {
            MoveForm(new SinglePlayerSettingsForm());
        }

        private void MoveForm(Window w) {
            w.Show();
            this.Close();
        }

        private void btnMultiplayer_Click(object sender, RoutedEventArgs e)
        {
            MoveForm(new MultiPlayerSettingsForm());
        }

        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            MoveForm(new SettingsForm());
        }

        private void windows_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
