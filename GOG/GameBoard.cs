using System;
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
        private Dice _dice;
        private Player WellPlayer;

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
            if (Geese.Contains(player.Position))
            {
                return true;
            }
            return false;
        }

        private void Game()
        {
            //bool doWeHaveAWinner = WeHaveAWinner();
            //do
            //{
            PlayerTurn(_settings.Turn % Players.Count);
            _settings.Turn++;
            //} while (!doWeHaveAWinner);
            if (WeHaveAWinner())
            {
                MessageBox.Show($"Congratulations ({ Players.SingleOrDefault(player => player.Position == 63).Name})\nYou Won!");
                Application.Current.Shutdown(); // or whatever we do to stop the game
            }
        }

        private void PlayerTurn(int playerId)
        {
            while (CanPlay(playerId))
            {
                int[] diceRoll = _dice.Roll();
                if (IsFirstThrow())
                {
                    if (FirsThrowExceptionCheck(playerId, diceRoll)) { return; };
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
                case SpecialPositions.Inn://to do
                    return false;
                case SpecialPositions.Well://to do
                    return false;
                case SpecialPositions.Prison://to do
                    return false;
                default:MessageBox.Show("fatal exception");
                    return false;
            }
        }

        private void Move(int playerId, int[] diceRoll)
        {
            Players[playerId].Move(diceRoll);
            OnGoose(playerId, diceRoll);
            int position = Players[playerId].Position;
            SpecialPositions currentPosition = (SpecialPositions)position;
            switch (currentPosition)
            {   
                case SpecialPositions.NotSpecialPosition:
                    break;
                case SpecialPositions.Bridge:
                    onBridge(playerId);
                    break;
                case SpecialPositions.Inn:
                    inInn(playerId);
                    break;
                case SpecialPositions.Well:
                    inWell(playerId);
                    break;
                case SpecialPositions.Maze:
                    isMaze(playerId);
                    break;
                case SpecialPositions.Prison:
                    inPrison(playerId);
                    break;
                case SpecialPositions.Death:
                    isDead(playerId);
                    break;
                case SpecialPositions.End:
                    //Winner
                    break;
                default:
                    break;
            }
        }

        private void onBridge(int playerId)
        {
            Players[playerId].Position = 12;
        }

        private void isMaze(int playerId)
        {
            Players[playerId].Position = 39;
        }

        private void isDead(int playerId)
        {
            Players[playerId].Position = 0;
        }

        private void inPrison(int playerId)
        {
            if (Players[playerId].ToSkipTurns != 0)
            {
                Players[playerId].ToSkipTurns = 3;
            }
            else
            {
                Players[playerId].ToSkipTurns--;
            }
        }

        private void inWell(int playerId)
        {
            if (WellPlayer == null)
            {
                WellPlayer = Players[playerId];
            }
            else
            {
                if (WellPlayer != Players[playerId])
                {
                    WellPlayer = Players[playerId];
                }
            }
        }

        private void inInn(int playerId)
        {
            if (Players[playerId].ToSkipTurns != 0)
            {
                Players[playerId].ToSkipTurns = 1;
            }
            else
            {
                Players[playerId].ToSkipTurns--;
            }
        }

        private void OnGoose(int playerId, int[] diceRoll)
        {
            while (IsPlayerOnGoose(Players[playerId]))
            {
                Players[playerId].Move(diceRoll);
            } 
        }

        private bool FirsThrowExceptionCheck(int playerId, int[] diceRoll)
        {
            if (diceRoll.Contains(5) && diceRoll.Contains(4))
            {
                Players[playerId].Position = 26;
                return true;
            }
            if (diceRoll.Contains(6) && diceRoll.Contains(3))
            {
                Players[playerId].Position = 53;
                return true;
            }
            return false;
        }

        private bool IsFirstThrow()
        {
            int _round = _settings.Turn / Players.Count;

            if (_round == 0)
            {
                return true;
            }
            return false;
        }

        private bool WeHaveAWinner()
        {
            var Winner = Players.SingleOrDefault(player => player.Position == 63);
            if (Winner == null) { return false; }
            return true;
        }
    }
}