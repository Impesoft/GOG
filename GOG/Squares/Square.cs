using GameOfGoose.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfGoose.Squares
{
    public class Square : ISquare
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Square";

        public virtual void Move(IPlayer player)
        {
            //don't move
            player.Position = player.Position;
        }

        public override string ToString()
        {
            return $"\non a normal field...";
        }
    }
}