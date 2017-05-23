using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    ///  
    public partial class MultiPlayerSettingsForm : Window {
        private Boolean backToMM;
        private MultiPlayerSettingsViewModel mpVP;
        private Task t;
        /// <summary>
        /// Initializes a new instance of the <see cref="MultiPlayerSettingsForm"/> class.
        /// </summary>
        public MultiPlayerSettingsForm() {
            InitializeComponent();
            mpVP = new MultiPlayerSettingsViewModel();
            this.DataContext = mpVP;
            this.mpVP.BadArgumentsEvent += this.BadArgsHandler;
            this.backToMM = true;
            this.imgPleaseWait.Source = new BitmapImage(
                new Uri(@"pack://application:,,,/MazeGUI;component/Resources/keepCalm.jpg"));
            this.imgPleaseWait.Visibility = Visibility.Hidden;



        }

        /// <summary>
        /// Numerics the text box.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="TextCompositionEventArgs"/> instance containing the event data.</param>
        private void NumericTxtBox(object sender, TextCompositionEventArgs e) {
            if (!char.IsDigit(e.Text, e.Text.Length - 1)) {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Creates the multi game.
        /// </summary>
        /// <param name="isStart">if set to <c>true</c> [is start].</param>
        private void CreateMultiGame(Boolean isStart) {
            if (isStart) {
                this.imgPleaseWait.Visibility = Visibility.Visible;
                MainMenu main = new MainMenu();
                main.Show();
                main.Close();
                this.mpVP.Stop = true;
                this.backToMM = false;
                MultiPlayerGameForm form;
                this.IsEnabled = false; 
                form = new MultiPlayerGameForm(Convert.ToInt32(this.mpVP.VM_Rows),
                            Convert.ToInt32(this.mpVP.VM_Cols), true, this.mpVP.VM_GameName);
                form.Show();
                this.Close();
            }
            else {

                MultiPlayerGameForm form = new MultiPlayerGameForm(this.cmbxGames.SelectedValue.ToString());
                form.Show();
                this.backToMM = false;
                this.mpVP.Stop = true;
                this.Close();
            }
        }

        /// <summary>
        /// Handles the Click event of the btnStartGame control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnStartGame_Click(object sender, RoutedEventArgs e)
        {
            this.CreateMultiGame(true);
        }

        /// <summary>
        /// Handles the Click event of the Button control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.CreateMultiGame(false);
        }

        /// <summary>
        /// Bads the arguments handler.
        /// </summary>
        /// <param name="message">The message.</param>
        private void BadArgsHandler(string message) {
            MessageBoxResult result = MessageBox.Show(message, "Bad Arguments",
                       MessageBoxButton.OK,
                       MessageBoxImage.Error);
        }

        /// <summary>
        /// Handles the Closed event of the Window control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Window_Closed(object sender, EventArgs e) {
            this.Dispatcher.Invoke(() => {
                if (backToMM)
                {
                    MainMenu main = new MainMenu();
                    main.Show();
                }
                
            });
           
           // this.Close();
        }

        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.backToMM = true;
            this.Close();
        }

        /// <summary>
        /// Handles the Closing event of the Window control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}
