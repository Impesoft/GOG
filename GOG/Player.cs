using System;
using System.Collections.Generic;

namespace GameOfGoose
{
    public class Player
    {
        public int Position { get; set; }
        public List<int> Geese = new List<int> { 5, 9, 12, 14, 18, 23, 27, 32, 36, 41, 45, 50, 54, 59 };

        public void Move(int amountOfEyes)
        {
            Position += amountOfEyes;
        }
    }
}