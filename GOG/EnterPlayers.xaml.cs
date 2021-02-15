using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ObservableCollection<Player> Players = Settings.Players;

        public EnterPlayers()
        {
            InitializeComponent();
            PlayerList.ItemsSource = Players;

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

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^2-4]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (Players.Count <= 4)
            {
                if (Players.ToList().Find(x => x.Name == PlayerName.Text) == null)
                {
                    if (PlayerName.Text == "") return;
                    Players.Add(new Player()
                    {
                        Name = PlayerName.Text,
                        OffsetX = (int)(5 * Players.ToList().Count - 5),
                        OffsetY = (int)(5 * Players.ToList().Count - 5),
                        Pawn = Settings.PawnList[Players.ToList().Count],
                        PlayerLocation = new Location()
                        {
                            X = Locations.List[0].X + 10 * Players.ToList().Count,
                            Y = Locations.List[0].Y
                        }
                    });
                    PlayerName.Text = "";
                    if (Players.Count == 4)
                    {
                        AddPlayer.IsEnabled = false;
                    }
                    if (Players.Count > 1)
                    {
                        StartButton.IsEnabled = true;
                    }
                }
                else
                {
                    MessageBox.Show("Duplicate PlayerName");
                }
            }
        }
    }
}