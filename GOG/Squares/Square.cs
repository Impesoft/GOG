using GameOfGoose.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfGoose.Squares
{
    public class Square : ISquare
    {
        public int Id { get; set; }
        public virtual string Name { get; set; } = "Square";

        public virtual void Move(Player player)
        {
            //dont move
            player.Position = player.Position;
        }
    }
}