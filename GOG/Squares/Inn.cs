using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace GameOfGoose.Squares
{
    internal class Inn : Square
    {
        public string Name { get; set; }

        public Inn()
        {
            Name = "Inn";
        }

        public override void Move(IPlayer player)
        {
            // Skip 1 Turn
            player.ToSkipTurns = 1;
        }

        public override string ToString()
        {
            return "\nWelcome have a drink and skip a turn";
        }
    }
}