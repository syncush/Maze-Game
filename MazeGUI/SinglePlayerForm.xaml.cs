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
        }
    }
}
