using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using GameOfGoose.Interface;
using GameOfGoose.Squares;

namespace GameOfGoose
{
    public class Game : INotifyPropertyChanged
    {
        public ObservableCollection<IPlayer> Players = Settings.Players;

        private string _infoText;

        public string InfoText
        {
            get => _infoText;
            set
            {
                if (_infoText == value) return;
                _infoText = value;
                OnPropertyChanged();
            }
        }

        private List<ISquare> SquarePathList { get; set; } = new List<ISquare>();
        private List<int> _geese = new List<int> { 5, 9, 12, 14, 18, 23, 27, 32, 36, 41, 45, 50, 54, 59 };
        private Dice _dice;
        private int _direction = 1;
        private Well _well;
        private Image _activePawn;
        public bool GameIsRunning;
        private IPlayer _activePlayer;
        public Image Dice1 = new Image();
        public Image Dice2 = new Image();

        public Game()
        {
        }

        public void ContinueGame()
        {
            DefineActivePlayer();

            PlayerTurn();
            Settings.Turn++;

            _activePlayer.PlayerPawn.Move(_activePlayer.Position);
            if (!WeHaveAWinner()) return;

            DisplayWinnerAndStopGame();
        }

        public void DisplayWinnerAndStopGame()
        {
            MessageBox.Show($"Congratulations {Players.SingleOrDefault(player => player.Position == 63)?.Name}\nYou Won!");
            foreach (IPlayer player in Players)
            {
                player.PlayerPawn.Move(0);
                player.Position = 0;
                player.PlayerPawn.PlayerLocation = Locations.List[0];
            }
            GameIsRunning = false;
        }

        public void DefineActivePlayer()
        {
            int activePlayerId = Settings.Turn % Players.Count;
            _activePlayer = Players[activePlayerId];
            _activePawn = _activePlayer.PlayerPawn.PawnImage;
        }

        public void ReInitializeGame()
        {
            SquarePathList = InitializeSquares();
            if (Players.Count > 0)
            {
                foreach (IPlayer player in Players)
                {
                    player.PlayerPawn.Move(0);
                }
            }
            InitializeVariables();
            foreach (var player in Players)
            {
                player.PlayerPawn.Move(0);
            }
        }

        public void InitializeVariables()
        {
            GameIsRunning = false;
            ShowInputWindow();
            if (Players.Count > 1)
            {
                _dice = new Dice(); //Create Dice
                Players = CreatePlayersListOfPlayers();
                GameIsRunning = true;
            }
        }

        public static void ShowInputWindow()
        {
            EnterPlayers enterPlayers = new EnterPlayers();
            enterPlayers.ShowDialog();
        }

        public ObservableCollection<IPlayer> CreatePlayersListOfPlayers()
        {
            foreach (IPlayer player in Players)
            {
                player.PlayerPawn.PawnImage = Settings.PawnList[Players.IndexOf(player)];
                player.PlayerPawn.PawnImage.ToolTip = player.Name;

                player.PlayerPawn.PlayerLocation.X =
                    Locations.List[0].X + player.PlayerPawn.OffsetX;
                player.PlayerPawn.PlayerLocation.Y =
                    Locations.List[0].Y + player.PlayerPawn.OffsetY;
            }

            return Players;
        }

        public List<ISquare> InitializeSquares()
        {
            for (int i = 0; i <= 63; i++)
            {
                if (_geese.Contains(i))
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

            return SquarePathList;
        }

        public void PlayerTurn()
        {
            _activePawn = _activePlayer.PlayerPawn.PawnImage;
            if (!CanPlay()) return;
            var diceRoll = RollDice();
            if (IsFirstThrow())
            {
                if (FirsThrowIs9Exception_Move(diceRoll)) return;
            }
            _direction = 1; // set start direction forward
            Move(diceRoll);
            InfoText += $"\n{_activePlayer.Name} is on field {_activePlayer.Position}";
        }

        public int[] RollDice()
        {
            int[] diceRoll = _dice.Roll();
            Dice1.Source = Settings.DiceFaces[diceRoll[0] - 1].Source;
            Dice2.Source = Settings.DiceFaces[diceRoll[1] - 1].Source;
            InfoText = $"{_activePlayer.Name} threw {diceRoll[0] + diceRoll[1]}";
            return diceRoll;
        }

        public bool CanPlay()
        {
            if (_activePlayer.ToSkipTurns > 0)
            {
                InfoText = $"{_activePlayer.Name} Can't play right now still had to skip {_activePlayer.ToSkipTurns}";

                _activePlayer.ToSkipTurns--;

                if (_activePlayer.ToSkipTurns > 0)
                {
                    InfoText += $"/{_activePlayer.ToSkipTurns} remaining";
                }
                return false;
            }

            if (_well.WellPlayer == _activePlayer)
            {
                InfoText += $"{_activePlayer.Name}, You're in the Well!\n{_well.WellPlayer.Name} needs rescuing!";
            }
            else
            {
                if (_well.WellPlayer != null) InfoText += $"\nin Well {_well.WellPlayer.Name}";
            }

            return _well.WellPlayer != _activePlayer;
        }

        public bool IsFirstThrow()
        {
            int round = Settings.Turn / Players.Count;

            return round == 0;
        }

        public void CheckIfReversed()
        {
            if (_activePlayer.Position <= 63) return;
            _activePlayer.Position = 63 - (_activePlayer.Position % 63);
            _direction = -1;
            InfoText += "\nturning back";
            _activePlayer.PlayerPawn.Move(_activePlayer.Position);
        }

        public bool FirsThrowIs9Exception_Move(int[] diceRoll)
        {
            if (diceRoll.Contains(5) && diceRoll.Contains(4))
            {
                _activePlayer.Position = 26;
                InfoText += "\nSpecial Throw! moving to 26";
                return true;
            }

            if (!diceRoll.Contains(6) || !diceRoll.Contains(3)) return false;
            _activePlayer.Position = 53;
            InfoText += "\nAmazing Throw! moving to 53";

            return true;
        }

        public void Move(int[] diceRoll)
        {
            _activePlayer.Move(_direction * diceRoll.Sum());
            CheckIfReversed();

            //reflection
            if (IsPlayerOnGoose(_activePlayer))
            {
                InfoText += "\na Goose just took you further";
                Move(diceRoll);
            }

            if (!InfoText.Contains(SquarePathList[_activePlayer.Position].ToString()))
            {
                InfoText += SquarePathList[_activePlayer.Position].ToString();
            }
            SquarePathList[_activePlayer.Position].Move(_activePlayer); //polymorphism activate 'current square positions'.move
        }

        public bool IsPlayerOnGoose(IPlayer player)
        {
            return _geese.Contains(player.Position);
        }

        public bool WeHaveAWinner()
        {
            var winner = Players.SingleOrDefault(player => player.Position == 63);

            return winner is not null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}