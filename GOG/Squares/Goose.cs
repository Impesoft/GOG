using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace GameOfGoose.Squares
{
    internal class Goose : Square
    {
        public override string Name { get; set; } = "a Goose just took you further";

        public override void Move(Player player)
        {
            //fatal error!
            MessageBox.Show("You can't Stop here!");
            // player.Position = 0;
        }

        public override string ToString()
        {
            return "\na Goose just took you further";
        }
    }
}