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
    /// Interaction logic for MultiPlayerGameForm.xaml
    /// </summary>
    public partial class MultiPlayerGameForm : Window {
        private MultiPlayerGameViewModel mpgVM;
        private Boolean shouldAsk = true;
        public MultiPlayerGameForm(int rows, int cols, Boolean isStart, string gameName) {
            InitializeComponent();
            if (isStart) {
                this.mpgVM = new MultiPlayerGameViewModel(gameName, rows, cols);
            }
            else {
                this.mpgVM = new MultiPlayerGameViewModel(gameName);
            }
            this.DataContext = this.mpgVM;
            this.ClientBoard.DataContext = this.mpgVM;
            this.RivalBoard.DataContext = this.mpgVM;
            //this.mpgVM.MazeChangedEvent += this.MazeChangedFunc;
            this.mpgVM.GameFinishedEvent += this.GameFinishedHandler;
            this.mpgVM.SomethingWentWrongEvent += this.HandleCriticalError;
            //this.MazeChangedFunc();
        }

        public MultiPlayerGameForm(string gameName) {
            InitializeComponent();
            this.mpgVM = new MultiPlayerGameViewModel(gameName);
            this.DataContext = mpgVM;
            this.ClientBoard.DataContext = this.mpgVM;
            this.RivalBoard.DataContext = this.mpgVM;
            // this.mpgVM.MazeChangedEvent += this.MazeChangedFunc;
            this.mpgVM.GameFinishedEvent += this.GameFinishedHandler;
            this.mpgVM.SomethingWentWrongEvent += this.HandleCriticalError;
            //this.MazeChangedFunc();
        }

        private void window_KeyUp(object sender, KeyEventArgs e) {
            if (e.Key == Key.Up || e.Key == Key.W || e.Key == Key.NumPad8) {
                this.mpgVM.PlayerMoved("up");
            }
            if (e.Key == Key.Left || e.Key == Key.A || e.Key == Key.NumPad4) {
                this.mpgVM.PlayerMoved("left");
            }
            if (e.Key == Key.Right || e.Key == Key.D || e.Key == Key.NumPad6) {
                this.mpgVM.PlayerMoved("right");
            }
            if (e.Key == Key.Down || e.Key == Key.S || e.Key == Key.NumPad3) {
                this.mpgVM.PlayerMoved("down");
            }
        }

        private void GameFinishedHandler(string mess) {
            this.Dispatcher.Invoke(() => {
                MessageBoxResult result = MessageBox.Show("Game finished!", "Game is done , thank you for playing.!",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                if (result == MessageBoxResult.OK)
                {
                    MainMenu form = new MainMenu();
                    form.Show();
                    this.shouldAsk = false;
                    this.Close();
                }
            });
        }

        private void window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(shouldAsk)
            {
                MessageBoxResult result = MessageBox.Show("Exiting game!", "Sure you want to quit ?",
                    MessageBoxButton.OKCancel,
                    MessageBoxImage.Information);
                if (result == MessageBoxResult.OK)
                {
                    this.mpgVM.GameClosed();
                    MainMenu main = new MainMenu();
                    main.Show();
                }
            }
            
        }

        public void HandleCriticalError(string mess)
        {
            this.Dispatcher.Invoke(() =>
            {
                MessageBoxResult result = MessageBox.Show("Server Connection Failure!", "Server Failure !",
                  MessageBoxButton.OK,
                  MessageBoxImage.Information);
                if (result == MessageBoxResult.OK)
                {
                    MainMenu main = new MainMenu();
                    main.Show();
                    this.shouldAsk = false;
                    this.Close();
                }
            });
        }
    }
}