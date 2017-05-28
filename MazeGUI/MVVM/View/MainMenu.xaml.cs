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
    public partial class MainMenu : Window {
        private Boolean shouldAsk = true;
        /// <summary>
        /// Initializes a new instance of the <see cref="MainMenu"/> class.
        /// </summary>
        public MainMenu()
        {
            InitializeComponent();
        }
        public MainMenu(bool a)
        {
            InitializeComponent();
            this.shouldAsk = a;
        }

        /// <summary>
        /// Handles the Click event of the btnSingle control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnSingle_Click(object sender, RoutedEventArgs e)
        {
            MoveForm(new SinglePlayerSettingsForm());
        }

        /// <summary>
        /// Moves the form.
        /// </summary>
        /// <param name="w">The w.</param>
        private void MoveForm(Window w) {
            shouldAsk = false;
            w.Show();
            this.Close();
        }

        private void btnMultiplayer_Click(object sender, RoutedEventArgs e)
        {
            MoveForm(new MultiPlayerSettingsForm());
        }

        /// <summary>
        /// Handles the Click event of the btnSettings control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            MoveForm(new SettingsForm());
        }

        /// <summary>
        /// Handles the Loaded event of the windows control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void windows_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void windows_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (shouldAsk) {
                MessageBoxResult mBox = MessageBox.Show("This will end the app!", "Confirmation", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        }
    }
}
