using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace GameOfGoose.Squares
{
    internal class Bridge : Square
    {
        public override string Name { get; set; } = "Bridge";

        public override void Move(Player player)
        {
            //move to 12
            player.Position = 12;
            MessageBox.Show("bridge");
        }
    }
}