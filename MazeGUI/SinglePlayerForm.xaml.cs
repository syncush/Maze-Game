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
using MazeLib;

namespace MazeGUI {
    /// <summary>
    /// Interaction logic for SinglePlayerForm.xaml
    /// </summary>
    public partial class SinglePlayerForm : Window {
        public SinglePlayerForm(Maze maze) {
            InitializeComponent();
            this.
            grdMaze.Background = new SolidColorBrush(Colors.LightSteelBlue);
            for (int i = 0; i < maze.Cols; i++) {
                ColumnDefinition gridCol1 = new ColumnDefinition();
                grdMaze.ColumnDefinitions.Add(gridCol1);
            }
            for (int i = 0; i < maze.Rows; i++) {
                RowDefinition gridRow = new RowDefinition();
                grdMaze.RowDefinitions.Add(gridRow);
            }
        }
    }
}