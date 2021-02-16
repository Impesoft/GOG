using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace GameOfGoose.Squares
{
    internal class Well : Square
    {
        public Player WellPlayer { get; set; }

        public override string Name { get; set; } = "Well";

        public override void Move(Player player)
        {
            // wait to be saved
            WellPlayer = player;
        }

        public override string ToString()
        {
            return $"\nWell... that bites, gotta wait for someone to rescue you now...";
        }
    }
}