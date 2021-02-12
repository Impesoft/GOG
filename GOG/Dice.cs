using System;
using System.ComponentModel;

namespace GameOfGoose
{
    public class Dice
    {
        public int Dice1, Dice2;

        public int[] Roll()
        {
            Dice1 = new Random().Next(1, 7);
            Dice2 = new Random().Next(1, 7);

            return new int[2] { Dice1, Dice2 };
        }
    }
}