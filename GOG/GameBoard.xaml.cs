using GameOfGoose.Squares;
using System;
using System.Collections.Generic;
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
        public List<Player> Players;
        public List<Image> PawnList;
        public List<Square> SquarePathList { get; private set; } = new List<Square>();
        public List<int> Geese = new List<int> { 5, 9, 12, 14, 18, 23, 27, 32, 36, 41, 45, 50, 54, 59 };
        private Settings _settings;
        private Dice _dice;
        private int _direction = 1;

        private Well _well;

        //  public int dice1, dice2;
        public Image ActivePawn;

        public bool GameIsRunning;
        public Player ActivePlayer;
        private int[] StartPosition { get; set; } = { 0, 1, 2, 3 };

        public GameBoard()
        {
            InitializeComponent();
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

        private void StartOrContinueGame()
        {
            if (!GameIsRunning)
            {
                InitializeVariables();
                GameIsRunning = true;
                foreach (var player in Players)
                {
                    AnimatePawn(player.Pawn, 0);
                }
            }
            else
            {
                foreach (var player in Players)
                {
                    Canvas.SetLeft(player.Pawn, player.PlayerLocation.X);
                    Canvas.SetTop(player.Pawn, player.PlayerLocation.Y);
                }
                int activePlayerId = _settings.Turn % Players.Count;
                ActivePlayer = Players[activePlayerId];
                ActivePawn = ActivePlayer.Pawn;

                PlayerTurn();
                _settings.Turn++;
                if (!WeHaveAWinner()) return;

                MessageBox.Show($"Congratulations { Players.SingleOrDefault(player => player.Position == 63)?.Name}\nYou Won!");
                GameIsRunning = false;
            }
        }

        private void InitializeVariables()
        {
            GameIsRunning = false;
            InitializeSquares();
            _settings = new Settings();
            EnterPlayers _enterPlayers = new EnterPlayers();
            _enterPlayers.ShowDialog();
            _dice = new Dice();

            var Player = Player1;
            Players = _settings.GetPlayers();

            foreach (Player player in Players)
            {
                player.Pawn = (Image)MyCanvas.Children[Players.IndexOf(player)];
                player.Pawn.ToolTip = player.Name;

                Canvas.SetLeft(player.Pawn, Locations.List[0].X + player.OffsetX - player.Pawn.Width / 2);
                Canvas.SetTop(player.Pawn, Locations.List[0].Y + player.OffsetY - player.Pawn.Height);
                player.PlayerLocation.X = Locations.List[0].X;
                player.PlayerLocation.Y = Locations.List[0].Y;
                AnimatePawn(player.Pawn, 0);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StartOrContinueGame();
        }

        //public void MoveTo(Image target, double newX, double newY)
        //{
        //    Canvas.SetLeft(target, newX - target.Width / 2); //(MyCanvas.ActualWidth / 884) *
        //    Canvas.SetTop(target, newY - target.Height); // (MyCanvas.ActualHeight / 658.5) *
        //}

        public void AnimatePawn(Image pawn, int endPosition)
        {
            Canvas.SetLeft(pawn, Locations.List[endPosition].X);
            Canvas.SetTop(pawn, Locations.List[endPosition].Y);
            //double offsetX = Locations.List[endPosition].X - (Players.Find(player => player.Pawn == pawn).PlayerLocation.X); // (MyCanvas.ActualWidth / 884) *
            //double offsetY = Locations.List[endPosition].Y - (Players.Find(player => player.Pawn == pawn).PlayerLocation.Y); //(MyCanvas.ActualWidth / 884) *
            //Throw.Text += $"\ncurrent X,Y = {Players.Find(player => player.Pawn == pawn).PlayerLocation.X},{Players.Find(player => player.Pawn == pawn).PlayerLocation.Y}: relative move to {endPosition} = {offsetX},{offsetY} to {Locations.List[endPosition].X},{Locations.List[endPosition].Y}";
            //TranslateTransform offsetTransform = new TranslateTransform();
            //var translationName = "myTranslation" + offsetTransform.GetHashCode();
            //RegisterName(translationName, offsetTransform);

            //DoubleAnimation offsetXAnimation = new DoubleAnimation(offsetX, new Duration(TimeSpan.FromSeconds(1)))
            //{
            //    EasingFunction = new PowerEase { EasingMode = EasingMode.EaseOut }
            //};
            //DoubleAnimation offsetYAnimation = new DoubleAnimation(offsetY, new Duration(TimeSpan.FromSeconds(1)))
            //{
            //    EasingFunction = new PowerEase { EasingMode = EasingMode.EaseOut }
            //};

            //offsetTransform.BeginAnimation(TranslateTransform.XProperty, offsetXAnimation);
            //offsetTransform.BeginAnimation(TranslateTransform.YProperty, offsetYAnimation);

            //pawn.RenderTransform = offsetTransform;
            //var s = new Storyboard();
            //Storyboard.SetTargetName(s, "MoveToXY");
            //Storyboard.SetTargetProperty(s, new PropertyPath(TranslateTransform.YProperty));
            //var storyboardName = "s" + s.GetHashCode();
            //Resources.Add(storyboardName, s);

            //s.Completed +=
            //    (sndr, evtArgs) =>
            //    {
            //        Resources.Remove(storyboardName);
            //        UnregisterName(translationName);
            //        Players.Find(player => player.Pawn == pawn).PlayerLocation.X = Locations.List[endPosition].X;
            //        Players.Find(player => player.Pawn == pawn).PlayerLocation.Y = (Locations.List[endPosition].Y);
            //        //Canvas.SetLeft(pawn, Players.Find(player => player.Pawn == pawn).PlayerLocation.X);
            //        //Canvas.SetTop(pawn, Players.Find(player => player.Pawn == pawn).PlayerLocation.Y);
            //    };
            //s.Begin();
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

        private bool IsPlayerOnGoose(Player player)
        {
            return Geese.Contains(player.Position);
        }

        private void PlayerTurn()
        {
            ActivePawn = ActivePlayer.Pawn;
            if (!CanPlay()) return;

            int[] diceRoll = _dice.Roll();
            // Dice1 Dice2
            Throw.Text = $"{ActivePlayer.Name} threw {diceRoll[0]},{ diceRoll[1]}\n";

            if (IsFirstThrow())
            {
                if (FirsThrowIs9Exception_Move(diceRoll)) return;
            }

            _direction = 1;
            StartPosition[Players.IndexOf(ActivePlayer)] = ActivePlayer.Position;
            Move(diceRoll);
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

        private void Move(int[] diceRoll)
        {
            ActivePlayer.Move(diceRoll, _direction);
            CheckIfReversed(diceRoll);
            //reflection
            if (IsPlayerOnGoose(ActivePlayer))
            {
                Move(diceRoll);
            }
            SquarePathList[ActivePlayer.Position].Move(ActivePlayer); //polymorphism
            Throw.Text += SquarePathList[ActivePlayer.Position].ToString();
            AnimatePawn(ActivePawn, ActivePlayer.Position);
        }

        private void CheckIfReversed(int[] diceRoll)
        {
            if (ActivePlayer.Position <= 63) return;
            ActivePlayer.Position = 63 - (ActivePlayer.Position % 63);
            _direction = -1;
            Move(diceRoll);
        }

        private bool FirsThrowIs9Exception_Move(int[] diceRoll)
        {
            if (diceRoll.Contains(5) && diceRoll.Contains(4))
            {
                ActivePlayer.Position = 26;
                Throw.Text += "\nSpecial Throw! moving to 26";
                AnimatePawn(ActivePlayer.Pawn, 26);
                return true;
            }

            if (!diceRoll.Contains(6) || !diceRoll.Contains(3)) return false;
            ActivePlayer.Position = 53;
            Throw.Text += "\nAmazing Throw! moving to 53";
            AnimatePawn(ActivePlayer.Pawn, 26);

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