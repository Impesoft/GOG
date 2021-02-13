using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace GameOfGoose.Squares
{
    internal class Death : Square
    {
        public override string Name { get; set; } = "Death";

        public override void Move(Player player)
        {
            //move to start
            player.Position = 0;
            //      MessageBox.Show("dead");
        }

        public override string ToString()
        {
            return "\nAw... You're Dead...";
        }
    }
}