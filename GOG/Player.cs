using System;
using System.Collections.Generic;
using System.Windows;

namespace GameOfGoose
{
    public class Player : IPlayer
    {
        public int Position { get; set; }
        public string Name { get; set; }
        public int OffsetX { get; set; }
        public int OffsetY { get; set; }

        public Player()
        {
            OffsetX = 0;
            OffsetY = 0;
        }

        public Location PlayerLocation { get; set; } = new Location { Id = 0, X = 0, Y = 0 };

        public void Move(int[] dice, int direction)
        {
            Position += direction * (dice[0] + dice[1]);
        }

        public int ToSkipTurns { get; set; }
    }
}