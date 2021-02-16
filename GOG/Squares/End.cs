using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;

namespace GameOfGoose.Squares
{
    internal class End : Square
    {
        public override string Name { get; set; } = "End";

        public override void Move(Player player)
        {
            //we have a winner
        }

        public override string ToString()
        {
            return "\nCongratulations You've Won!";
        }
    }
}