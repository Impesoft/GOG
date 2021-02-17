using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace GameOfGoose.Squares
{
    internal class Prison : Square
    {
        public string Name { get; set; }

        public Prison()
        {
            Name = "Prison";
        }

        public override void Move(IPlayer player)
        {
            // Skip 3 Turns
            player.ToSkipTurns = 3;
        }

        public override string ToString()
        {
            return $"\nGot Caught! You're in prison now (skip 3 turns)";
        }
    }
}