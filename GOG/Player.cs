using System;
using System.Collections.Generic;

namespace GameOfGoose
{
    public class Player
    {
        public int Position { get; set; }
        public string Name { get; set; }

        public Player()
        {
        }

        public void Move(int[] dice)
        {
            Position += dice[0] + dice[1];
        }
    }
}