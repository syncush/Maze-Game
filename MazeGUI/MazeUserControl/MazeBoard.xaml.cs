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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MazeLib;

namespace MazeGUI.MazeUserControl {
    /// <summary>
    /// Interaction logic for MazeBoard.xaml
    /// </summary>
    public partial class MazeBoard : UserControl {
        public MazeBoard(Maze maze) {
            InitializeComponent();
            this.canvas.Width = 20 * maze.Cols;
            this.canvas.Height = 20 * maze.Rows;
            for (int i = 0; i < maze.Cols; i++) {
                this.SetRow(i);

            }
        }

        public void SetRow(int rowNum) {
            int j = 20 * rowNum;
            for (int i = 0; i < this.canvas.Width; i+=20) {
                Rectangle rect = new Rectangle();
                rect.Width = 20;
                rect.Height = 20;
                this.canvas.Children.Add(rect);
                
            }
        }
       
    }
}