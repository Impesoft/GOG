using System;
using System.Collections.Generic;

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
            // Game();
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
            do
            {
                PlayerTurn(_settings.Turn % Players.Count);
                _settings.Turn++;
            } while (true);
        }

        private void PlayerTurn(int playerId)
        {
            int[] diceRoll = _dice.Roll();
            do
            {
                _player.Move(diceRoll);
            } while (IsPlayerOnGoose(Players[playerId]));
        }
    }
}