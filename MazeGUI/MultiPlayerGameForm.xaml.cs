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
    /// Interaction logic for MultiPlayerGameForm.xaml
    /// </summary>
    public partial class MultiPlayerGameForm : Window {
        private MultiPlayerGameViewModel mpgVM;
        public MultiPlayerGameForm(int rows, int cols, Boolean isStart, string gameName) {
            InitializeComponent();
            this.mpgVM = new MultiPlayerGameViewModel(gameName, isStart, rows, cols);
            this.DataContext = this.mpgVM;

        }
    }
}