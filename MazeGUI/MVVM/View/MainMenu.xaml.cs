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
using System.Windows.Shapes;

namespace MazeGUI
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainMenu"/> class.
        /// </summary>
        public MainMenu()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Click event of the btnSingle control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnSingle_Click(object sender, RoutedEventArgs e)
        {
            MoveForm(new SinglePlayerSettingsForm());
        }

        /// <summary>
        /// Moves the form.
        /// </summary>
        /// <param name="w">The w.</param>
        private void MoveForm(Window w) {
            w.Show();
            this.Close();
        }

        private void btnMultiplayer_Click(object sender, RoutedEventArgs e)
        {
            MoveForm(new MultiPlayerSettingsForm());
        }

        /// <summary>
        /// Handles the Click event of the btnSettings control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            MoveForm(new SettingsForm());
        }

        private void windows_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}