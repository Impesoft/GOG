using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace GameOfGoose
{
    public class GameBoard
    {
        public List<Player> Players;
        public List<int> Geese = new List<int> { 5, 9, 12, 14, 18, 23, 27, 32, 36, 41, 45, 50, 54, 59 };
        private Settings _settings;
        private readonly Dice _dice;
        private Player _wellPlayer;
        private int _direction = 1;

        public GameBoard()
        {
            _settings = new Settings();
            _dice = new Dice();

            Players = _settings.GetPlayers();
            // GameStep();            // GameStep will be triggered by front end buttons
        }

        public GameBoard(List<Player> players)
        {
            Players = players;
        }

        private bool IsPlayerOnGoose(Player player)
        {
            return Geese.Contains(player.Position);
        }

        private void Game()
        {
            //bool doWeHaveAWinner = WeHaveAWinner();
            //do
            //{
            PlayerTurn(_settings.Turn % Players.Count);
            _settings.Turn++;
            //} while (!doWeHaveAWinner);
            if (!WeHaveAWinner()) return;
            MessageBox.Show($"Congratulations ({ Players.SingleOrDefault(player => player.Position == 63)?.Name})\nYou Won!");
            Application.Current.Shutdown(); // or whatever we do to stop the game
        }

        private void PlayerTurn(int playerId)
        {
            while (CanPlay(playerId))
            {
                int[] diceRoll = _dice.Roll();
                if (IsFirstThrow())
                {
                    if (FirsThrowExceptionCheck(playerId, diceRoll)) { return; }
                }
                Move(playerId, diceRoll);
            }
        }

        private bool CanPlay(int playerId)
        {
            int position = Players[playerId].Position;
            SpecialPositions currentPosition = (SpecialPositions)position;
            switch (currentPosition)
            {
                case SpecialPositions.NotSpecialPosition:
                    return true;

                case SpecialPositions.Inn:
                    if (Players[playerId].ToSkipTurns == 0) return true;
                    Players[playerId].ToSkipTurns--;
                    return false;

                case SpecialPositions.Well:
                    return _wellPlayer != Players[playerId];

                case SpecialPositions.Prison:
                    if (Players[playerId].ToSkipTurns == 0) return true;
                    Players[playerId].ToSkipTurns--;
                    return false;

                default:
                    MessageBox.Show("fatal exception");
                    return false;
            }
        }

        private void Move(int playerId, int[] diceRoll)
        {
            _direction = 1;
            Players[playerId].Move(diceRoll, _direction);
            OnGoose(playerId, diceRoll);
            SpecialPositionsMoves(playerId);

            if (Players[playerId].Position > 63)
            {
                Players[playerId].Position = 63 - (Players[playerId].Position % 63);
                _direction = -1;
                OnGoose(playerId, diceRoll);
            }
        }

        private void SpecialPositionsMoves(int playerId)
        {
            int position = Players[playerId].Position;
            SpecialPositions currentPosition = (SpecialPositions)position;
            switch (currentPosition)
            {
                case SpecialPositions.NotSpecialPosition:
                    break;

                case SpecialPositions.Bridge:
                    OnBridge(playerId);
                    break;

                case SpecialPositions.Inn:
                    InInn(playerId);
                    break;

                case SpecialPositions.Well:
                    InWell(playerId);
                    break;

                case SpecialPositions.Maze:
                    IsMaze(playerId);
                    break;

                case SpecialPositions.Prison:
                    InPrison(playerId);
                    break;

                case SpecialPositions.Death:
                    IsDead(playerId);
                    break;

                case SpecialPositions.End:
                    //Winner method when frontend exists
                    break;
            }
        }

        private void OnBridge(int playerId)
        {
            Players[playerId].Position = 12;
        }

        private void IsMaze(int playerId)
        {
            Players[playerId].Position = 39;
        }

        private void IsDead(int playerId)
        {
            Players[playerId].Position = 0;
        }

        private void InPrison(int playerId)
        {
            Players[playerId].ToSkipTurns = 3;
        }

        private void InWell(int playerId)
        {
            _wellPlayer = Players[playerId];
        }

        private void InInn(int playerId)
        {
            Players[playerId].ToSkipTurns = 1;
        }

        private void OnGoose(int playerId, int[] diceRoll)
        {
            while (IsPlayerOnGoose(Players[playerId]))
            {
                Players[playerId].Move(diceRoll, _direction);
            }
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

            return winner != null;
        }
    }
}