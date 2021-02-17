using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace GameOfGoose.Squares
{
    internal class Maze : Square
    {
        public string Name { get; set; }

        public Maze()
        {
            Name = "Maze";
        }

        public override void Move(IPlayer player)
        {
            //move to start
            player.Position = 39;
        }

        public override string ToString()
        {
            return $"\nYou got lost in the maze go back to 39";
        }
    }
}