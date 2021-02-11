using System;
using System.Collections.Generic;

namespace GameOfGoose
{
    public class Player : IPlayer
    {
        public int Position { get; set; }
        public string Name { get; set; }

        public Player()
        {
        }

        public void Move(int[] dice, int direction)
        {
            Position += direction * (dice[0] + dice[1]);
        }

        public int ToSkipTurns { get; set; }
    }
}