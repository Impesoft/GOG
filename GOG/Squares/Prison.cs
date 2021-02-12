using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfGoose.Squares
{
    internal class Prison : Square
    {
        public string Name { get; set; } = "Prison";

        public override void Move(Player player)
        {
            // Skip 3 Turns
            player.ToSkipTurns = 3;
        }
    }
}