using System;
using System.Collections.Generic;

namespace GameOfGoose
{
    public class Player
    {
        private GameBoard _gameBoard;
        public int Position { get; set; }

        public Player()
        {
            _gameBoard = new GameBoard();
        }

        public void Move(int[] dice)
        {
            Position += dice[0] + dice[1];

            if (_gameBoard.Geese.Contains(Position))
            {
                Move(dice);
            }
        }
    }
}