using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace GameOfGoose.Squares
{
    internal class Inn : Square
    {
        public override string Name { get; set; } = "Inn";

        public override void Move(Player player)
        {
            // Skip 1 Turn
            player.ToSkipTurns = 1;
            MessageBox.Show("skip " + player.ToSkipTurns + " turns");
        }
    }
}