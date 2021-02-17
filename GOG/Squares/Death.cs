using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace GameOfGoose.Squares
{
    internal class Death : Square
    {
        public string Name { get; set; }

        public Death()
        {
            Name = "Death";
        }

        public override void Move(IPlayer player)
        {
            //move to start
            player.Position = 0;
        }

        public override string ToString()
        {
            return "\nAw... You're Dead...";
        }
    }
}