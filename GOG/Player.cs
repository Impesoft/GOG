using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Windows;
using System.Windows.Controls;

namespace GameOfGoose
{
    public class Player : IPlayer
    {
        public int Position { get; set; }
        public string Name { get; set; }
        public PlayerPawn PlayerPawn { get; set; }

        public Player(string name, int position)
        {
            Name = name;
            Position = position;
        }

        public void Move(int direction)
        {
            Position += direction;
        }

        public int ToSkipTurns { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}