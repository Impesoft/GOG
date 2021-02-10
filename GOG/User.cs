using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfGoose
{
    public class User
    {
        public static readonly List<int> Geese = new List<int> { 5, 9, 12, 14, 18, 23, 27, 32, 36, 41, 45, 50, 54, 59 };
        public String Name { get; set; }
        public int Position { get; set; }
        public string PlayerImage { get; set; }
        private int _skipCounter = 0;

        public User(string name, string playerImg)
        {
            PlayerImage = playerImg;
            Name = name;
            Position = 0;
            _skipCounter = 0;
        }

        public int[] Dice() //returns array of 2 random dice values
        {
            return new int[2] { new Random().Next(1, 7), new Random().Next(1, 7) };
        }

        public int moveto(int[] dice, int position) //moves de users position
        {
            position += dice[0] + dice[1];
            System.Windows.MessageBox.Show($"{position} = {Geese.Contains(position).ToString()}");
            if (Geese.Contains(position))
            {
                moveto(dice, position);
            }
            return position;
        }

        private bool CanPlay()
        {
            SpecialPositions position = (SpecialPositions)Position;
            bool canPlay = true;
            switch (position)
            {
                case SpecialPositions.Bridge:
                    return canPlay;

                case SpecialPositions.Death:
                    Position = 0;
                    return canPlay;

                default:
                    return canPlay;
            }
        }
    }
}