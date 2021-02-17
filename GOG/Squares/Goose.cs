using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace GameOfGoose.Squares
{
    internal class Goose : Square
    {
        public Goose()
        {
            Name = "Goose";
        }

        public override void Move(IPlayer player)
        {
            //fatal error!
            MessageBox.Show("You can't Stop here!");
            // player.Position = 0;
        }

        public override string ToString()
        {
            return "";
        }
    }
}