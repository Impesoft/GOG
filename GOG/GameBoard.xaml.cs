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
        public ObservableCollection<Player> Players = Settings.Players;

        public List<Square> SquarePathList { get; private set; } = new List<Square>();

        public List<int> Geese = new List<int> { 5, 9, 12, 14, 18, 23, 27, 32, 36, 41, 45, 50, 54, 59 };

        //private Settings Settings;
        private Dice _dice;

        private int _direction = 1;

        private Well _well;

        public Image ActivePawn;

        public bool GameIsRunning;
        public Player ActivePlayer;
        private int[] StartPosition { get; set; } = { 0, 1, 2, 3 };

        public GameBoard()
        {
            InitializeComponent();

            foreach (Image pawn in MyCanvas.Children)
            {
                //pawn.RenderTransform = ;
                Settings.PawnList.Add(pawn);
            }
            CenterWindowOnScreen();
            //        StartOrContinueGame();
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StartOrContinueGame();
        }

        private void StartOrContinueGame()
        {
            if (!GameIsRunning)
            {
                InitializeVariables();
                GameIsRunning = true;
                foreach (var player in Players)
                {
                    player.PlayerPawn.Move(0);
                }
            }
            else
            {
                int activePlayerId = Settings.Turn % Players.Count;
                ActivePlayer = Players[activePlayerId];
                ActivePawn = ActivePlayer.PlayerPawn.Pawn;

                PlayerTurn();
                Settings.Turn++;

                ActivePlayer.PlayerPawn.Move(ActivePlayer.Position);
                if (!WeHaveAWinner()) return;

                MessageBox.Show($"Congratulations { Players.SingleOrDefault(player => player.Position == 63)?.Name}\nYou Won!");
                GameIsRunning = false;
            }
        }

        private void InitializeVariables()
        {
            GameIsRunning = false;
            InitializeSquares();
            EnterPlayers _enterPlayers = new EnterPlayers();
            _enterPlayers.ShowDialog();
            _dice = new Dice();

            foreach (Player player in Players)
            {
                player.PlayerPawn.Pawn = (Image)MyCanvas.Children[Players.IndexOf(player)];
                player.PlayerPawn.Pawn.ToolTip = player.Name;

                player.PlayerPawn.PlayerLocation.X = Locations.List[0].X + player.PlayerPawn.OffsetX - player.PlayerPawn.Pawn.Width / 2;
                player.PlayerPawn.PlayerLocation.Y = Locations.List[0].Y + player.PlayerPawn.OffsetY - player.PlayerPawn.Pawn.Height;
            }
        }

        public void InitializeSquares()
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
                            _well = (Well)SquarePathList.FirstOrDefault(square => square.Name == "Well");
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

        private void PlayerTurn()
        {
            ActivePawn = ActivePlayer.PlayerPawn.Pawn;
            if (!CanPlay()) return;

            int[] diceRoll = _dice.Roll();

            Throw.Text = $"{ActivePlayer.Name} threw {diceRoll[0]},{ diceRoll[1]}\n";

            if (IsFirstThrow())
            {
                if (FirsThrowIs9Exception_Move(diceRoll)) return;
            }
            _direction = 1;
            StartPosition[Players.IndexOf(ActivePlayer)] = ActivePlayer.Position;
            Move(diceRoll);
            Throw.Text += $"\n{ActivePlayer.Name} is on field {ActivePlayer.Position}";
        }

        private bool CanPlay()
        {
            if (ActivePlayer.ToSkipTurns > 0)
            {
                Throw.Text = $"{ActivePlayer.Name} Can't play right now still had to skip {ActivePlayer.ToSkipTurns}";

                ActivePlayer.ToSkipTurns--;

                if (ActivePlayer.ToSkipTurns > 0) { Throw.Text += $"/{ActivePlayer.ToSkipTurns} remaining"; }
                return false;
            }

            if (_well.WellPlayer == ActivePlayer)
            {
                Throw.Text += $"{ActivePlayer.Name}, You're in the Well!\n{_well.WellPlayer.Name} needs rescuing!";
            }
            else
            {
                if (_well.WellPlayer != null) Throw.Text += $"\nin Well {_well.WellPlayer.Name}";
            }

            return _well.WellPlayer != ActivePlayer;
        }

        private bool IsFirstThrow()
        {
            int round = Settings.Turn / Players.Count;

            return round == 0;
        }

        private void CheckIfReversed()
        {
            if (ActivePlayer.Position <= 63) return;
            ActivePlayer.Position = 63 - (ActivePlayer.Position % 63);
            _direction = -1;
            ActivePlayer.PlayerPawn.Move(ActivePlayer.Position);
        }

        private bool FirsThrowIs9Exception_Move(int[] diceRoll)
        {
            if (diceRoll.Contains(5) && diceRoll.Contains(4))
            {
                ActivePlayer.Position = 26;
                Throw.Text += "\nSpecial Throw! moving to 26";
                return true;
            }

            if (!diceRoll.Contains(6) || !diceRoll.Contains(3)) return false;
            ActivePlayer.Position = 53;
            Throw.Text += "\nAmazing Throw! moving to 53";

            return true;
        }

        private void Move(int[] diceRoll)
        {
            ActivePlayer.Move(_direction * (diceRoll[0] + diceRoll[1]));
            CheckIfReversed();

            //reflection
            if (IsPlayerOnGoose(ActivePlayer))
            {
                Move(diceRoll);
            }
            SquarePathList[ActivePlayer.Position].Move(ActivePlayer); //polymorphism activate 'current square positions'.move
            Throw.Text += SquarePathList[ActivePlayer.Position].ToString();
        }

        private bool IsPlayerOnGoose(Player player)
        {
            return Geese.Contains(player.Position);
        }

        public void AnimatePawn(Image pawn, int endPosition)
        {
            ActivePlayer = Players.ToList().Find(player => player.PlayerPawn.Pawn == pawn);
            Canvas.SetLeft(pawn, Locations.List[endPosition].X + ActivePlayer.PlayerPawn.OffsetX + pawn.Width / 2);
            Canvas.SetTop(pawn, Locations.List[endPosition].Y + ActivePlayer.PlayerPawn.OffsetY - pawn.Height);
        }

        private bool WeHaveAWinner()
        {
            var winner = Players.SingleOrDefault(player => player.Position == 63);

            return winner is not null;
        }
    }
}