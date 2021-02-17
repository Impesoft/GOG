using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace GameOfGoose.Squares
{
    public class Well : Square
    {
        public IPlayer WellPlayer { get; set; }

        public string Name { get; set; }// to remove Name = 'name'

        public Well()
        {
            Name = "Well";
            WellPlayer = new Player(Name = "WellPlayer", 31);
        }

        public override void Move(IPlayer player)
        {
            // wait to be saved
            WellPlayer = player;
        }

        public override string ToString()
        {
            return $"\nWell... that bites, gotta wait for someone to rescue you now...";
        }
    }
}