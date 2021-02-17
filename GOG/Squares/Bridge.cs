using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace GameOfGoose.Squares
{
    internal class Bridge : Square
    {
        public string Name { get; set; }

        public Bridge()
        {
            Name = "Bridge";
        }

        public override void Move(IPlayer player)
        {
            //move to 12
            player.Position = 12;
        }

        public override string ToString()
        {
            return "\nThe bridge over troubled water";
        }
    }
}