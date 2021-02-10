using System;

namespace GameOfGoose
{
    public class Dice
    {
        public int[] Roll()
        {
            return new int[2] { new Random().Next(1, 7), new Random().Next(1, 7) };
        }
    }
}