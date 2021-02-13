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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GameOfGoose.Squares;

namespace GameOfGoose
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class GameBoard : Window
    {
        public int IAmHere = 0;
        public List<Player> Players;
        private EnterPlayers _enterPlayers = new EnterPlayers();
        public List<Square> SquarePathList { get; private set; } = new List<Square>();
        public List<int> Geese = new List<int> { 5, 9, 12, 14, 18, 23, 27, 32, 36, 41, 45, 50, 54, 59 };
        private Settings _settings;
        private readonly Dice _dice;
        private int _direction = 1;
        public List<Location> Locations;
        private Well _well;
        public int dice1, dice2;
        public Image CurrentPlayer;
        public Player ActivePlayer;

        //  public GameBoard GameBoard;

        public GameBoard()
        {
            InitializeComponent();

            _settings = new Settings();
            Locations = _settings.GetLocations();
            _enterPlayers.ShowDialog();
            _dice = new Dice();
            _well = new Well();
            GetSquares();
            // MoveTo(Players[0], Locations[Players[0].Position].X, Locations[Players[0].Position].X);
            var Player = Player1;
            Players = _settings.GetPlayers();
            double x = Locations[0].X - Player1.Width;
            double y = Locations[0].Y - Player1.Height;
            Canvas.SetLeft(Player1, x);
            Canvas.SetTop(Player1, y);

            //double x = Locations[IAmHere].X - Player1.Width;
            //double y = Locations[IAmHere].Y - Player1.Height;

            //MoveTo(Player1, x, y);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GameStep();
            //IAmHere++;
            //IAmHere %= 64;
            //double x = GameBoard.Locations[IAmHere].X - Player1.Width / 2;
            //double y = GameBoard.Locations[IAmHere].Y - Player1.Height;
            //Canvas.SetLeft(Player1, x);
            //Canvas.SetTop(Player1, y);
            //  MoveTo(Player1, x, y);
        }

        public static void MoveTo(Image target, double newX, double newY)
        {
            Canvas.SetLeft(target, newX - target.Width / 2);
            Canvas.SetTop(target, newY - target.Height / 2);
            //// Return the offset vector for the TextBlock object.
            //Vector vector = VisualTreeHelper.GetOffset(target);

            //// Convert the vector to a x y values.
            //double x = vector.X;
            //double y = vector.Y;

            //TranslateTransform translation = new TranslateTransform();
            //target.RenderTransform = translation;
            //DoubleAnimation anim2 = new DoubleAnimation(450 - target.Width / 2, newX - target.Width / 2, TimeSpan.FromSeconds(1));
            //DoubleAnimation anim1 = new DoubleAnimation(300, newY, TimeSpan.FromSeconds(1));
            //translation.BeginAnimation(TranslateTransform.XProperty, anim2);
            //translation.BeginAnimation(TranslateTransform.YProperty, anim1);
        }

        public void GetSquares()
        {
            for (int i = 0; i <= 63; i++)
            {
                if (Geese.Contains(i))
                {
                    SquarePathList.Add(new Goose());
                }
                else if (((SpecialPositions)i == 0))
                {
                    SquarePathList.Add(new Square());
                }
                else
                {
                    SpecialPositions currentPosition = (SpecialPositions)i;
                    switch (currentPosition)
                    {
                        case SpecialPositions.Bridge:

                            SquarePathList.Add(new Bridge());
                            break;

                        case SpecialPositions.Inn:
                            SquarePathList.Add(new Inn());
                            break;

                        case SpecialPositions.Well:
                            SquarePathList.Add(new Well());
                            break;

                        case SpecialPositions.Maze:
                            SquarePathList.Add(new Maze());
                            break;

                        case SpecialPositions.Prison:
                            SquarePathList.Add(new Prison());
                            break;

                        case SpecialPositions.Death:
                            SquarePathList.Add(new Death());
                            break;

                        case SpecialPositions.End:
                            SquarePathList.Add(new End());
                            break;

                        default:
                            SquarePathList.Add(new Square());
                            break;
                    }
                }
            }
        }

        private bool IsPlayerOnGoose(Player player)
        {
            return Geese.Contains(player.Position);
        }

        public void GameStep()
        {
            int activePlayerId = _settings.Turn % Players.Count;
            ActivePlayer = Players[activePlayerId];
            PlayerTurn(activePlayerId);
            _settings.Turn++;

            if (!WeHaveAWinner()) return;
            MessageBox.Show($"Congratulations ({ Players.SingleOrDefault(player => player.Position == 63)?.Name})\nYou Won!");
            Application.Current.Shutdown(); // or whatever we do to stop the game
        }

        private void PlayerTurn(int playerId)
        {
            switch (playerId)
            {
                case 0:
                    CurrentPlayer = Player1;

                    break;

                case 1:
                    CurrentPlayer = Player2;
                    break;

                case 2:
                    CurrentPlayer = Player3;
                    break;

                case 3:
                    CurrentPlayer = Player4;
                    break;
            }
            if (!CanPlay(playerId)) return;
            int[] diceRoll = _dice.Roll();
            dice1 = diceRoll[0];
            dice2 = diceRoll[1];
            // Dice1 Dice2
            Throw.Text = $"player{playerId} threw {dice1},{dice2}\n";

            if (IsFirstThrow())
            {
                if (FirsThrowExceptionCheck(playerId, diceRoll)) return;
            }

            _direction = 1;
            Move(playerId, diceRoll);
        }

        private bool CanPlay(int playerId)
        {
            if (ActivePlayer.ToSkipTurns > 0)
            {
                ActivePlayer.ToSkipTurns--;
                return false;
            }

            return _well.WellPlayer != Players[playerId];
        }

        private void Move(int playerId, int[] diceRoll)
        {
            Player player = Players[playerId];
            //  _direction = 1;
            player.Move(diceRoll, _direction);
            CheckIfReversed(playerId, diceRoll);
            if (IsPlayerOnGoose(player))
            {
                Move(playerId, diceRoll);
            }
            SquarePathList[player.Position].Move(player); //polymorphism

            MoveTo(CurrentPlayer, Locations[player.Position].X - ActivePlayer.OffsetX, Locations[player.Position].Y - ActivePlayer.OffsetY);
            Throw.Text += $"and should now be on position {player.Position} ({Locations[player.Position].X},{Locations[player.Position].Y})";
        }

        private void CheckIfReversed(int playerId, int[] diceRoll)
        {
            if (Players[playerId].Position <= 63) return;
            Players[playerId].Position = 63 - (Players[playerId].Position % 63);
            _direction = -1;
            Move(playerId, diceRoll);
        }

        private bool FirsThrowExceptionCheck(int playerId, int[] diceRoll)
        {
            if (diceRoll.Contains(5) && diceRoll.Contains(4))
            {
                Players[playerId].Position = 26;
                return true;
            }

            if (!diceRoll.Contains(6) || !diceRoll.Contains(3)) return false;
            Players[playerId].Position = 53;
            return true;
        }

        private bool IsFirstThrow()
        {
            int round = _settings.Turn / Players.Count;

            return round == 0;
        }

        private bool WeHaveAWinner()
        {
            var winner = Players.SingleOrDefault(player => player.Position == 63);

            return winner is not null;
        }
    }
}