using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfGoose
{
    public class Settings
    {
        public int Turn { get; set; } = 0;
        public int NumberOfPlayers { get; set; }
        public List<Player> Players;

        public List<Player> GetPlayers()
        {
            Players = new List<Player>
            {
                new Player(),
                new Player(),
                new Player(),
                new Player(),
            };

            return Players;
        }
    }
}