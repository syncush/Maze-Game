﻿using System;
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
using Image = System.Drawing.Image;

namespace MazeGUI.MazeUserControl {
    /// <summary>
    /// Interaction logic for MazeBoard.xaml
    /// </summary>
    public partial class MazeBoard : UserControl {
        private int[,] maze;
        private Dictionary<string, Label> lblDictionary;
        private BitmapImage wallImg;
        private BitmapImage playerImg;
        private BitmapImage exitImg;
        private ImageBrush exitBrush;
        private ImageBrush playerBrush;
        private ImageBrush wallBrush;
        private SolidColorBrush freeSpace;

        public MazeBoard() {
            InitializeComponent();
            this.exitImg = new BitmapImage(new Uri(@"pack://application:,,,/MazeGUI;component/Resources/exit.png"));
            this.playerImg = new BitmapImage(
                new Uri(@"pack://application:,,,/MazeGUI;component/Resources/walterWhite.jpeg"));
            this.wallImg = new BitmapImage(new Uri(@"pack://application:,,,/MazeGUI;component/Resources/wall.png"));
            this.exitBrush = new ImageBrush(this.exitImg);
            this.playerBrush = new ImageBrush(this.playerImg);
            this.wallBrush = new ImageBrush(this.wallImg);
            this.freeSpace = new SolidColorBrush(Color.FromRgb(150, 213, 150));
            lblDictionary = new Dictionary<string, Label>();
        }

        public void Initialize(int[,] mazeB) {
            int rows = mazeB.GetLength(0);
            int cols = mazeB.GetLength(1);
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
        }

        public string PlayerImage {
            set { this.RefreshImage(value, this.playerBrush); }
        }

        public string WallImage {
            set { this.RefreshImage(value, this.wallBrush); }
        }

        public string ExitImage {
            set { this.RefreshImage(value, this.exitBrush); }
        }

        private void RefreshImage(string imgName, ImageBrush brush) {
            brush = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/MazeGUI;component/Resources/" +
                                                           imgName)));
            this.Maze = this.maze;
        }


        public int[,] Maze {
            get { return this.maze; }
            set {
                int rows = value.GetLength(0);
                int cols = value.GetLength(1);
                for (int i = 0; i < rows; i++) {
                    for (int j = 0; j < cols; j++) {
                        Label rect = lblDictionary[string.Format("({0},{1})", i, j)];
                        switch (value[i, j]) {
                            case 0: {
                                rect.Background = this.wallBrush;
                            }
                                break;
                            case 1: {
                                rect.Background = this.freeSpace;
                            }
                                break;
                            case 2: {
                                rect.Background = this.exitBrush;
                            }
                                break;
                            case 3: {
                                rect.Background = this.playerBrush;
                            }
                                break;
                        }
                    }
                }
            }
        }
    }
}