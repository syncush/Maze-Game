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
using MazeGUI.Utilities;
using MazeLib;
using Image = System.Drawing.Image;

/// <summary>
/// 
/// </summary>
namespace MazeGUI.MazeUserControl {
    /// <summary>
    /// Interaction logic for MazeBoard.xaml
    /// </summary>
    public partial class MazeBoard : UserControl {
        #region StaticMembers

        public static int InitPosRep = 5;
        public static int GoalRep = 2;
        public static int PlayerRep = 3;
        public static int SolutionBrickRep = 4;
        public static int WallRep = 0;
        public static int FreeSpaceRep = 1;

        #endregion

        #region DataMembers

        private string[] maze;
        private Dictionary<string, Label> lblDictionary;
        private int rows;
        private int cols;
        private string pos;
        private string goalPos;
        private BitmapImage wallImg;
        private BitmapImage playerImg;
        private BitmapImage exitImg;
        private BitmapImage solImage;
        private BitmapImage initImg;
        private ImageBrush initBrush;
        private ImageBrush exitBrush;
        private ImageBrush playerBrush;
        private ImageBrush wallBrush;
        private ImageBrush solBrush;
        private SolidColorBrush freeSpace;
        private Boolean isInit;

        #endregion


        /// <summary>
        /// Initializes a new instance of the <see cref="MazeBoard"/> class.
        /// </summary>
        public MazeBoard() {
            InitializeComponent();
            this.exitImg = new BitmapImage(new Uri(@"pack://application:,,,/MazeGUI;component/Resources/exit.png"));
            this.playerImg = new BitmapImage(
                new Uri(@"pack://application:,,,/MazeGUI;component/Resources/walterWhite.jpeg"));
            this.wallImg = new BitmapImage(new Uri(@"pack://application:,,,/MazeGUI;component/Resources/wall.png"));
            this.initImg = new BitmapImage(new Uri(@"pack://application:,,,/MazeGUI;component/Resources/start.png"));
            this.solImage = new BitmapImage(new Uri(@"pack://application:,,,/MazeGUI;component/Resources/thisWay.png"));
            this.initBrush = new ImageBrush(this.initImg);
            this.solBrush = new ImageBrush(this.solImage);
            this.exitBrush = new ImageBrush(this.exitImg);
            this.playerBrush = new ImageBrush(this.playerImg);
            this.wallBrush = new ImageBrush(this.wallImg);
            this.freeSpace = new SolidColorBrush(Color.FromRgb(150, 213, 150));
            lblDictionary = new Dictionary<string, Label>();
            this.isInit = false;
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void Initialize(string b) {
            this.maze = b.Split(',');
             
            this.rows = this.maze.Length - 1;
            this.cols = this.maze[0].Length;
            for (int i = 0; i < rows; i++) {
                RowDefinition row = new RowDefinition();
                row.Height = GridLength.Auto;
                this.grid.RowDefinitions.Add(row);
            }
            for (int i = 0; i < cols; i++) {
                ColumnDefinition col = new ColumnDefinition();
                col.Width = GridLength.Auto;
                this.grid.ColumnDefinitions.Add(col);
            }
            for (int i = 0; i < rows; i++) {
                for (int j = 0; j < cols; j++) {
                    Label rect = new Label();
                    rect.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                    this.grid.Children.Add(rect);
                    Grid.SetRow(rect, i);
                    Grid.SetColumn(rect, j);
                    lblDictionary.Add(string.Format("({0},{1})", i, j), rect);
                }
            }
            isInit = true;
        }


        #region Properties

        /// <summary>
        /// Sets the player image.
        /// </summary>
        /// <value>
        /// The player image.
        /// </value>
        public string PlayerImage {
            set { this.RefreshImage(value, this.playerBrush); }
        }

        /// <summary>
        /// Sets the wall image.
        /// </summary>
        /// <value>
        /// The wall image.
        /// </value>
        public string WallImage {
            set { this.RefreshImage(value, this.wallBrush); }
        }

        /// <summary>
        /// Sets the exit image.
        /// </summary>
        /// <value>
        /// The exit image.
        /// </value>
        public string ExitImage {
            set { this.RefreshImage(value, this.exitBrush); }
        }

        /// <summary>
        /// Refreshes the image.
        /// </summary>
        /// <param name="imgName">Name of the img.</param>
        /// <param name="brush">The brush.</param>
        private void RefreshImage(string imgName, ImageBrush brush) {
            brush = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/MazeGUI;component/Resources/" +
                                                           imgName)));
            this.DrawMaze(this.maze.ToString());
        }

        /// <summary>
        /// Gets or sets the maze.
        /// </summary>
        /// <value>
        /// The maze.
        /// </value>
        public string Maze {
            get { return this.maze.ToString(); }
            set { this.maze = value.Split(','); }
        }

        #endregion


        #region MazeDrawing

        /// <summary>
        /// The maze property
        /// </summary>
        public static readonly DependencyProperty MazeProperty =
            DependencyProperty.Register("Maze", typeof(string), typeof(MazeBoard), new UIPropertyMetadata(MazeChanged));

        public static readonly DependencyProperty PlayerPositionProperty = DependencyProperty.Register("PlayerPosition",
            typeof(string), typeof(MazeBoard), new UIPropertyMetadata(PlayerPositionChanged));

        public static void PlayerPositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            MazeBoard board = (MazeBoard) d;
            board.PlayerPositionChangedFunc((string) e.NewValue, (string)e.OldValue);
        }

        /// <summary>
        /// Mazes the changed.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void MazeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            MazeBoard board = (MazeBoard) d;

            board.DrawMaze((string) e.NewValue);
        }

        public string PlayerPosition {
            get { return this.pos; }
            set { this.pos = value; }
        }

        public void PlayerPositionChangedFunc(string newLocation, string oldLocation) {
            Label newPos = lblDictionary[string.Format("({0})", newLocation)];
            newPos.Background = this.playerBrush;
            try {
                string str = string.Format("({0})", oldLocation);
                Label oldPos = lblDictionary[str];
                if (str == goalPos) {
                    oldPos.Background = exitBrush;
                }
                else {
                    oldPos.Background = this.freeSpace;
                }
            }
            catch (Exception) {
                
            }
           
        }

        /// <summary>
        /// Draws the maze.
        /// </summary>
        private void DrawMaze(string b) {
            if (!isInit) {
                this.Initialize(b);
            }
            this.maze = b.Split(',');
            for (int i = 0; i < this.rows; ++i) {
                for (int j = 0; j < this.cols; ++j) {
                    string str = string.Format("({0},{1})", i, j);
                    Label rect = lblDictionary[str];
                    switch ((this.maze[i])[j]) {
                        case '0': {
                            rect.Background = this.wallBrush;
                        }
                            break;
                        case '1': {
                            rect.Background = this.freeSpace;
                        }
                            break;
                        case '2': {
                            this.goalPos = str;
                            rect.Background = this.exitBrush;
                        }
                            break;
                        case '3': {
                            rect.Background = this.playerBrush;
                        }
                            break;
                        case '4': {
                            rect.Background = this.solBrush;
                        }
                            break;
                        case '5': {
                            rect.Background = this.initBrush;
                        }
                            break;
                    }
                }
            }
        }

        #endregion
    }
}