using GameOfGoose.Squares;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace GameOfGoose
{
    /// <summary>
    /// Interaction logic for GameBoard.xaml
    /// </summary>
    public partial class GameBoard : Window
    {
        public Game Game
        {
            get { return _game; }
        }

        //public Settings Settings;

        private readonly Game _game;

        public GameBoard()
        {
            InitializeComponent();
            CenterWindowOnScreen();
            _game = new Game(this);
        }

        public void CenterWindowOnScreen()
        {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            Game.StartOrContinueGame();
        }
    }
}