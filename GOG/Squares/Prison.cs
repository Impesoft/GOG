using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace GameOfGoose.Squares
{
    internal class Prison : Square
    {
        public string Name { get; set; } = "Prison";

        public override void Move(Player player)
        {
            // Skip 3 Turns
            player.ToSkipTurns = 3;
            //   MessageBox.Show("Got Caught! You're in prison now (skip 3 turns)");
        }

        public override string ToString()
        {
            return $"\nGot Caught! You're in prison now (skip 3 turns)";
        }
    }
}