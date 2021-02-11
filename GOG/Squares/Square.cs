using GameOfGoose.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfGoose.Squares
{
    public class Square : ISquare
    {
        private Settings _settings;
        private List<Player> Players;
        public int Id { get; set; }
        public virtual string Name { get; set; } = "Square";

        public Square()
        {
            _settings = new Settings();
            Players = _settings.GetPlayers();
        }

        public void Move()
        {
            //dont move
        }
    }
}