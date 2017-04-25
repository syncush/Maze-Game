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
    /// Interaction logic for Test.xaml
    /// </summary>
    public partial class Test : Window
    {
        public Test() {
            InitializeComponent();
            int[,] maze = { {0,0,0}, {1,0,3},{2,0,1} } ;
            this.mazeBoard.Initialize(maze);
            this.mazeBoard.Maze = maze;
        }
    }
}
