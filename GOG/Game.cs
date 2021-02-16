using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using GameOfGoose.Squares;

namespace GameOfGoose
{
    public class Game
    {
        private GameBoard _gameBoard;
        public ObservableCollection<Player> Players = Settings.Players;

        public List<int> Geese = new List<int> { 5, 9, 12, 14, 18, 23, 27, 32, 36, 41, 45, 50, 54, 59 };
        public Dice _dice;
        public int _direction = 1;
        public Well _well;
        public Image ActivePawn;
        public bool GameIsRunning;
        public Player ActivePlayer;

        public Game(GameBoard gameBoard)
        {
            _gameBoard = gameBoard;
        }

        public List<Square> SquarePathList { get; set; } = new List<Square>();

        public void StartOrContinueGame()
        {
            if (!GameIsRunning)
            {
                ReInitializeGame();
            }
            else
            {
                ContinueGame();
            }
        }

        public void ContinueGame()
        {
            DefineActivePlayer();

            PlayerTurn();
            Settings.Turn++;

            ActivePlayer.PlayerPawn.Move(ActivePlayer.Position);
            if (!WeHaveAWinner()) return;

            DisplayWinnerAndStopGame();
        }

        public void DisplayWinnerAndStopGame()
        {
            MessageBox.Show($"Congratulations {Players.SingleOrDefault(player => player.Position == 63)?.Name}\nYou Won!");
            GameIsRunning = false;
        }

        public void DefineActivePlayer()
        {
            int activePlayerId = Settings.Turn % Players.Count;
            ActivePlayer = Players[activePlayerId];
            ActivePawn = ActivePlayer.PlayerPawn.Pawn;
        }

        public void ReInitializeGame()
        {
            InitializeVariables();
            GameIsRunning = true;
            foreach (var player in Players)
            {
                player.PlayerPawn.Move(0);
            }
        }

        public void InitializeVariables()
        {
            GameIsRunning = false;
            CreatePawnListFromCanvasPawns();
            InitializeSquares();
            ShowInputWindow();
            _dice = new Dice(); //Create Dice

            CreatePlayersListOfPlayers();
        }

        public static void ShowInputWindow()
        {
            EnterPlayers _enterPlayers = new EnterPlayers();
            _enterPlayers.ShowDialog();
        }

        public void CreatePlayersListOfPlayers()
        {
            foreach (Player player in Players)
            {
                player.PlayerPawn.Pawn = Settings.PawnList[Players.IndexOf(player)];
                player.PlayerPawn.Pawn.ToolTip = player.Name;

                player.PlayerPawn.PlayerLocation.X =
                    Locations.List[0].X + player.PlayerPawn.OffsetX - 25;
                player.PlayerPawn.PlayerLocation.Y =
                    Locations.List[0].Y + player.PlayerPawn.OffsetY - 43;
            }
        }

        public void CreatePawnListFromCanvasPawns()
        {
            for (int i = 0; i < 4; i++)
            {
                //BitmapImage bmi = new BitmapImage(new Uri("pack://application:,,,/Images/Ted.jpg"));
                Image pawn = new Image();
                BitmapImage bmi = new BitmapImage(new Uri($"pack://application:,,,/Images/Pawn{i + 1}.png"));

                pawn.Source = bmi;
                pawn.Width = 50;
                pawn.Height = 43;
                Settings.PawnList.Add(pawn);
                _gameBoard.MyCanvas.Children.Add(pawn);
                Canvas.SetLeft(pawn, i * 10 - 20);
            }

            //foreach (Image pawn in _gameBoard.MyCanvas.Children)
            //{
            //    Settings.PawnList.Add(pawn);
            //}
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

        public void PlayerTurn()
        {
            ActivePawn = ActivePlayer.PlayerPawn.Pawn;
            if (!CanPlay()) return;
            var diceRoll = RollDice();
            if (IsFirstThrow())
            {
                if (FirsThrowIs9Exception_Move(diceRoll)) return;
            }
            _direction = 1; // set start direction forward
            Move(diceRoll);
            Settings.InfoText += $"\n{ActivePlayer.Name} is on field {ActivePlayer.Position}";
        }

        public int[] RollDice()
        {
            int[] diceRoll = _dice.Roll();
            Settings.InfoText = $"{ActivePlayer.Name} threw {diceRoll[0]},{diceRoll[1]}\n";
            return diceRoll;
        }

        public bool CanPlay()
        {
            if (ActivePlayer.ToSkipTurns > 0)
            {
                Settings.InfoText = $"{ActivePlayer.Name} Can't play right now still had to skip {ActivePlayer.ToSkipTurns}";

                ActivePlayer.ToSkipTurns--;

                if (ActivePlayer.ToSkipTurns > 0)
                {
                    Settings.InfoText += $"/{ActivePlayer.ToSkipTurns} remaining";
                }
                return false;
            }

            if (_well.WellPlayer == ActivePlayer)
            {
                Settings.InfoText += $"{ActivePlayer.Name}, You're in the Well!\n{_well.WellPlayer.Name} needs rescuing!";
            }
            else
            {
                if (_well.WellPlayer != null) Settings.InfoText += $"\nin Well {_well.WellPlayer.Name}";
            }

            return _well.WellPlayer != ActivePlayer;
        }

        public bool IsFirstThrow()
        {
            int round = Settings.Turn / Players.Count;

            return round == 0;
        }

        public void CheckIfReversed()
        {
            if (ActivePlayer.Position <= 63) return;
            ActivePlayer.Position = 63 - (ActivePlayer.Position % 63);
            _direction = -1;
            ActivePlayer.PlayerPawn.Move(ActivePlayer.Position);
        }

        public bool FirsThrowIs9Exception_Move(int[] diceRoll)
        {
            if (diceRoll.Contains(5) && diceRoll.Contains(4))
            {
                ActivePlayer.Position = 26;
                Settings.InfoText += "\nSpecial Throw! moving to 26";
                return true;
            }

            if (!diceRoll.Contains(6) || !diceRoll.Contains(3)) return false;
            ActivePlayer.Position = 53;
            Settings.InfoText += "\nAmazing Throw! moving to 53";

            return true;
        }

        public void Move(int[] diceRoll)
        {
            ActivePlayer.Move(_direction * (diceRoll[0] + diceRoll[1]));
            CheckIfReversed();

            //reflection
            if (IsPlayerOnGoose(ActivePlayer))
            {
                Move(diceRoll);
            }
            SquarePathList[ActivePlayer.Position].Move(ActivePlayer); //polymorphism activate 'current square positions'.move
            Settings.InfoText += SquarePathList[ActivePlayer.Position].ToString();
        }

        public bool IsPlayerOnGoose(Player player)
        {
            return Geese.Contains(player.Position);
        }

        public bool WeHaveAWinner()
        {
            var winner = Players.SingleOrDefault(player => player.Position == 63);

            return winner is not null;
        }
    }
}