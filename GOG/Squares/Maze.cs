using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace GameOfGoose.Squares
{
    internal class Maze : Square
    {
        public override string Name { get; set; } = "Maze";

        public override void Move(Player player)
        {
            //move to start
            player.Position = 39;
            MessageBox.Show("You got lost in the maze go back to 39");
        }
    }
}