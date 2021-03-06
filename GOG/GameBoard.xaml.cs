﻿using GameOfGoose.Squares;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

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
            _game = new Game();
            MyStackPanel.DataContext = _game;
            Dice1.DataContext = _game.Dice1;
            Dice2.DataContext = _game.Dice2;
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
            if (!Game.GameIsRunning)
            {
                CreateCanvasPawns();
                CreateCanvasDice();
                Game.ReInitializeGame();
            }
            else
            {
                _game.InfoText = "";
                Game.ContinueGame();
            }
        }

        public void CreateCanvasPawns()
        {
            if (MyCanvas.Children.Count > 0) return;
            for (int i = 0; i < 4; i++)
            {
                Image pawn = new Image();
                BitmapImage bmi = new BitmapImage(new Uri($"pack://application:,,,/Images/Pawn{i + 1}.png"));

                pawn.Source = bmi;
                pawn.Width = 50;
                pawn.Height = 43;
                Settings.PawnList.Add(pawn);
                MyCanvas.Children.Add(pawn);
                Canvas.SetLeft(pawn, i * 3 - 6);
                Canvas.SetTop(pawn, 600);
            }
        }

        public void CreateCanvasDice()
        {
            for (int i = 1; i < 7; i++)
            {
                Image pawn = new Image();
                BitmapImage bmi = new BitmapImage(new Uri($"pack://application:,,,/Images/{i}.png"));

                pawn.Source = bmi;
                pawn.Width = 50;
                pawn.Height = 43;
                Settings.DiceFaces.Add(pawn);
            }
        }
    }
}