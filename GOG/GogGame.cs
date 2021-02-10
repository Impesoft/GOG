using System;
using System.Collections.Generic;

namespace GameOfGoose
{
    public class GogGame
    {
        public GogGame()
        {
        }

        private bool CanPlay()
        {
            SpecialPositions position = (SpecialPositions)Position;
            bool canPlay = true;
            switch (position)
            {
                case SpecialPositions.Bridge:
                    return canPlay;

                case SpecialPositions.Inn:

                case SpecialPositions.Death:
                    Position = 0;
                    return canPlay;

                default:
                    return canPlay;
            }
        }

        public int[] Dice() //returns array of 2 random dice values
        {
            return new int[2] { new Random().Next(1, 7), new Random().Next(1, 7) };
        }
    }
}