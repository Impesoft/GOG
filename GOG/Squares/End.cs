using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;

namespace GameOfGoose.Squares
{
    internal class End : Square
    {
        public string Name { get; set; }

        public End()
        {
            Name = "End";
        }

        public override void Move(IPlayer player)
        {
            //we have a winner
        }

        public override string ToString()
        {
            return "\nCongratulations You've Won!";
        }
    }
}