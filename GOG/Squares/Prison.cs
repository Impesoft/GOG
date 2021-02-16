using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace GameOfGoose.Squares
{
    internal class Prison : Square
    {
        public override string Name { get; set; } = "Prison";

        public override void Move(Player player)
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