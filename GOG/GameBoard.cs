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
        private Player _player;

        public GameBoard()
        {
            _settings = new Settings();
            _dice = new Dice();
            _player = new Player();

            Players = _settings.GetPlayers();
            //Game();
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
            bool doWeHaveAWinner = WeHaveAWinner();
            do
            {
                PlayerTurn(_settings.Turn % Players.Count);
                _settings.Turn++;
            } while (!doWeHaveAWinner);
            MessageBox.Show($"Congratulations ({ Players.SingleOrDefault(player => player.Position == 63).Name})\nYou Won!");
            Application.Current.Shutdown();
        }

        private void PlayerTurn(int playerId)
        {
            int[] diceRoll = _dice.Roll();
            if (IsFirstThrow())
            {
                if (FirsThrowExceptionCheck(playerId, diceRoll)) { return; };
            }
            MoveAsLongAsOnGoose(playerId, diceRoll);
        }

        private void MoveAsLongAsOnGoose(int playerId, int[] diceRoll)
        {
            do
            {
                _player.Move(diceRoll);
            } while (IsPlayerOnGoose(Players[playerId]));
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