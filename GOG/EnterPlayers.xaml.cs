﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GameOfGoose
{
    /// <summary>
    /// Interaction logic for EnterPlayers.xaml
    /// </summary>
    public partial class EnterPlayers : Window
    {
        private ObservableCollection<IPlayer> _players = Settings.Players;
        private List<Image> _localPawnList = new List<Image>();

        public EnterPlayers()
        {
            InitializeComponent();
            PlayerList.ItemsSource = _players;
            foreach (IPlayer player in _players)
            {
                player.PlayerPawn.PlayerLocation = Locations.List[0];
                player.Position = 0;
                player.PlayerPawn.Move(0);
            }
            _players.Clear();
            PlayerName.Focus();
            CenterWindowOnScreen();
        }

        private void CenterWindowOnScreen()
        {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            PlayerName.Focus();
            if (_players.Count > 1)
            {
                foreach (IPlayer player in Settings.Players)
                {
                    Settings.PawnList.Add(player.PlayerPawn.PawnImage);
                }
                this.Close();
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            _localPawnList = Settings.PawnList;
            if (_players.Count <= 4)
            {
                StartButton.IsDefault = false;
                AddPlayer.IsDefault = true;
                PlayerName.Focus();

                if (_players.ToList().Find(x => x.Name == PlayerName.Text) == null)
                {
                    if (PlayerName.Text == "") return;
                    _players.Add(new Player(PlayerName.Text, 0)
                    {
                        PlayerPawn = new PlayerPawn(_localPawnList[0])
                        {
                            OffsetX = (int)(2 * _players.ToList().Count),
                            OffsetY = (int)(2 * _players.ToList().Count - 615),

                            PlayerLocation = new Location()
                            {
                                X = Locations.List[0].X + 3 * _players.ToList().Count,
                                Y = Locations.List[0].Y
                            }
                        }
                    });

                    _localPawnList.RemoveAt(0);
                    PlayerName.Text = "";

                    if (_players.Count == 4)
                    {
                        AddPlayer.IsEnabled = false;
                    }
                    if (_players.Count > 1)
                    {
                        StartButton.IsEnabled = true;
                    }
                }
                else
                {
                    MessageBox.Show("Duplicate PlayerName");
                    PlayerName.Focus();
                }
            }
            else
            {
                StartButton.IsDefault = true;
                AddPlayer.IsDefault = false;
            }
        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            PlayerName.Focus();
            if (PlayerList.SelectedIndex != -1)
            {
                _localPawnList.Add(_players[PlayerList.SelectedIndex].PlayerPawn.PawnImage);
                _players.RemoveAt(PlayerList.SelectedIndex);
            }
            if (_players.Count < 4)
            {
                AddPlayer.IsEnabled = true;
            }
            if (_players.Count < 1)
            {
                StartButton.IsEnabled = false;
            }
        }

        private void onClose(object sender, EventArgs e)
        {
            if (_players.Count > 1)
            {
                foreach (IPlayer player in Settings.Players)
                {
                    Settings.PawnList.Add(player.PlayerPawn.PawnImage);
                }
                //   this.Close();
            }
        }
    }
}