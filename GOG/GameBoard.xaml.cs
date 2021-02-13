using GameOfGoose.Squares;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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
        public int dice1, dice2;
        public Image ActivePawn;
        public bool GameIsRunning;
        public Player ActivePlayer;

        public GameBoard()
        {
            InitializeComponent();
            CenterWindowOnScreen();
            StartOrContinueGame();
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
            }
            else
            {
                int activePlayerId = _settings.Turn % Players.Count;
                ActivePlayer = Players[activePlayerId];
                ActivePawn = ActivePlayer.Pawn;
                double x = Locations.List[ActivePlayer.Position].X - ActivePawn.Width;
                double y = Locations.List[ActivePlayer.Position].Y - ActivePawn.Height;
                Canvas.SetLeft(ActivePawn, x / 900 * Width);
                Canvas.SetTop(ActivePawn, y / 600 * Height);
                PlayerTurn(activePlayerId);
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
            _well.WellPlayer = new Player { Name = "not set" };
            var Player = Player1;
            Players = _settings.GetPlayers();
            PawnList = new List<Image>()
            {
                Player1,
                Player2,
                Player3,
                Player4,
            };
            foreach (Player player in Players)
            {
                player.Pawn = PawnList[Players.IndexOf(player)];
                player.Pawn.ToolTip = player.Name;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //GameStep();
            StartOrContinueGame();
        }

        public static void MoveTo(Image target, double newX, double newY)
        {
            Canvas.SetLeft(target, newX - target.Width / 2);
            Canvas.SetTop(target, newY - target.Height / 2);
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

        public void GameStep()
        {
        }

        private void PlayerTurn(int playerId)
        {
            ActivePawn = ActivePlayer.Pawn;
            if (!CanPlay()) return;
            int[] diceRoll = _dice.Roll();
            dice1 = diceRoll[0];
            dice2 = diceRoll[1];
            // Dice1 Dice2
            Throw.Text = $"{ActivePlayer.Name} threw {dice1},{dice2}\n";

            if (IsFirstThrow())
            {
                if (FirsThrowExceptionCheck(playerId, diceRoll)) return;
            }

            _direction = 1;
            Move(playerId, diceRoll);
        }

        private bool CanPlay()
        {
            if (ActivePlayer.ToSkipTurns > 0)
            {
                ActivePlayer.ToSkipTurns--;
                return false;
            }

            Throw.Text += $"\nin Well {_well.WellPlayer.Name}";
            return _well.WellPlayer != ActivePlayer;
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
            Throw.Text += SquarePathList[player.Position].ToString();
            MoveTo(ActivePawn, Locations.List[player.Position].X - ActivePlayer.OffsetX, Locations.List[player.Position].Y - ActivePlayer.OffsetY);
            //  Throw.Text += $"\nand should now be on position {player.Position} ({Locations.List[player.Position].X},{Locations.List[player.Position].Y})";
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