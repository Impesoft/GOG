using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfGoose
{
    public class Player
    {
        public List<int> Geese = new List<int> { 5, 9, 12, 14, 18, 23, 27, 32, 36, 41, 45, 50, 54, 59 };

        public String Name { get; set; }
        public int Position { get; set; }
        public string PlayerImage { get; set; }
        private int _skipCounter = 0;

        public Player(string name, string playerImg)
        {
            PlayerImage = playerImg;
            Name = name;
            Position = 0;
            _skipCounter = 0;
        }

        public int Move(int[] dice, int position) //moves de users position
        {
            position += dice[0] + dice[1];
            if (GogGame.Geese.Contains(position))
            {
                Move(dice, position);
            }
            return position;
        }
    }
}