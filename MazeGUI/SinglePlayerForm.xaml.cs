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

namespace MazeGUI
{
    /// <summary>
    /// Interaction logic for SinglePlayerForm.xaml
    /// </summary>
    public partial class SinglePlayerForm : Window {
        private SinglePlayerGameViewModel spGameVM;
        public SinglePlayerForm(int rows, int cols, string gameName)
        {
            InitializeComponent();
            this.spGameVM = new SinglePlayerGameViewModel(rows, cols, gameName);
            this.DataContext = spGameVM;
            this.mazeBoard.Maze = spGameVM.MazeOBJ;
            this.spGameVM.GameFinishedEvent += this.GameFinished;
        }

        private void window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up || e.Key == Key.W || e.Key == Key.NumPad8) {
                this.spGameVM.MovePlayer("up");
            }
            if (e.Key == Key.Left || e.Key == Key.A || e.Key == Key.NumPad4) {
                this.spGameVM.MovePlayer("left");
            }
            if (e.Key == Key.Right || e.Key == Key.D || e.Key == Key.NumPad6)
            {
                this.spGameVM.MovePlayer("right");
            }
            if (e.Key == Key.Down || e.Key == Key.S || e.Key == Key.NumPad3)
            {
                this.spGameVM.MovePlayer("down");
            }
            this.mazeBoard.Maze = this.spGameVM.MazeOBJ;


        }

        private void btnRestart_Click(object sender, RoutedEventArgs e)
        {
            this.spGameVM.RestartGame();
        }

        private void GameFinished() {
            MessageBoxResult result = MessageBox.Show("You Finished The Maze!", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
            if (result == MessageBoxResult.OK)
            {
                MainMenu form = new MainMenu();
                form.Show();
                this.Close();
            }
        }
    }
}
