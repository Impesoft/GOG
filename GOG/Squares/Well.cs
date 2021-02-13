using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace GameOfGoose.Squares
{
    internal class Well : Square
    {
        public Player WellPlayer;

        public string Name { get; set; } = "Well";

        public override void Move(Player player)
        {
            // wait to be saved
            WellPlayer = player;
        }

        public override string ToString()
        {
            return $"\nWell... that shocks, gotta wait for someone to rescue you now...";
        }
    }
}