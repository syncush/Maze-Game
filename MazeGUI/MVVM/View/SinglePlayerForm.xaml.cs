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
    /// Interaction logic for SinglePlayerForm.xaml
    /// </summary>
    public partial class SinglePlayerForm : Window {
        private SinglePlayerGameViewModel spGameVM;

        /// <summary>
        /// Initializes a new instance of the <see cref="SinglePlayerForm"/> class.
        /// </summary>
        /// <param name="rows">The rows.</param>
        /// <param name="cols">The cols.</param>
        /// <param name="gameName">Name of the game.</param>
        public SinglePlayerForm(int rows, int cols, string gameName) {
            InitializeComponent();
            this.spGameVM = new SinglePlayerGameViewModel(rows, cols, gameName);
            this.DataContext = spGameVM;
            this.mazeBoard.DataContext = spGameVM;
            this.spGameVM.GameFinishedEvent += this.GameFinished;
            this.spGameVM.AnimationStartedEvent += () => {
                this.Dispatcher.Invoke(() => {
                    this.IsEnabled = false;
                });

            };
            this.spGameVM.AnimationFinishedEvent += () => {
                this.Dispatcher.Invoke(() => {
                    this.IsEnabled = true;
                });
            };
            this.spGameVM.ConnectionToServerFailedEvent += HandleConnectionLost;
        }
        private void HandleConnectionLost(string message)
        {
            MessageBoxResult mBox = MessageBox.Show(message + " Form will now shutdown", "Error", MessageBoxButton.OK,
               MessageBoxImage.Error);
            if (mBox == MessageBoxResult.OK)
            {
                this.Close();
            }
        }
        /// <summary>
        /// Handles the KeyUp event of the window control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void window_KeyUp(object sender, KeyEventArgs e) {
            if (e.Key == Key.Up || e.Key == Key.W || e.Key == Key.NumPad8) {
                this.spGameVM.MovePlayer("up");
            }
            if (e.Key == Key.Left || e.Key == Key.A || e.Key == Key.NumPad4) {
                this.spGameVM.MovePlayer("left");
            }
            if (e.Key == Key.Right || e.Key == Key.D || e.Key == Key.NumPad6) {
                this.spGameVM.MovePlayer("right");
            }
            if (e.Key == Key.Down || e.Key == Key.S || e.Key == Key.NumPad3) {
                this.spGameVM.MovePlayer("down");
            }
            //this.mazeBoard.Maze = this.spGameVM.MazeOBJ;
        }

        /// <summary>
        /// Handles the Click event of the btnRestart control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnRestart_Click(object sender, RoutedEventArgs e) {
            MessageBoxResult mBox = MessageBox.Show("Restart Game ?", "Confirmation", MessageBoxButton.OK,
                MessageBoxImage.Information);
            if (mBox == MessageBoxResult.OK)
            {
                this.spGameVM.RestartGame();
            }

        }

        /// <summary>
        /// Games the finished.
        /// </summary>
        private void GameFinished() {
            this.Dispatcher.Invoke(() => {
                MessageBoxResult result = MessageBox.Show("You Finished The Maze!", "Confirmation", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                if (result == MessageBoxResult.OK) {
                }
            });
        }

        /// <summary>
        /// Handles the Click event of the btnSolve control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnSolve_Click(object sender, RoutedEventArgs e) {
            MessageBoxResult mBox = MessageBox.Show("Solve Maze ?", "Confirmation", MessageBoxButton.OK,
                MessageBoxImage.Information);
            if (mBox == MessageBoxResult.OK) {
                this.spGameVM.SolveMaze();
            }
        }


        /// <summary>
        /// Handles the Closed event of the window control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void window_Closed(object sender, EventArgs e) {
            MainMenu form = new MainMenu();
            form.Show();
        }

        /// <summary>
        /// Handles the Click event of the btnMainMenu control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnMainMenu_Click(object sender, RoutedEventArgs e) {
            MessageBoxResult mBox = MessageBox.Show("Go Back To MainMenu ?", "Confirmation", MessageBoxButton.OK,
                MessageBoxImage.Information);
            if (mBox == MessageBoxResult.OK) {
                this.Close();
            }
        }

        private void window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult mBox = MessageBox.Show("Windows will now close ", "Confirmation", MessageBoxButton.OK,
               MessageBoxImage.Question);
            if (mBox == MessageBoxResult.OK)
            {
                this.Close();
            }
        }
    }
}