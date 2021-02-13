using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace GameOfGoose
{
    public class Settings
    {
        public int Turn { get; set; } = 0;
        public int NumberOfPlayers { get; set; }
        public List<Player> Players;

        public List<Location> GetLocations()
        {
            Locations locations = new Locations();

            return locations.GetLocations();
        }

        public List<Player> GetPlayers()
        {
            Players = new List<Player>
            {
                new Player(),
                new Player(),
                new Player(),
                new Player(),
            };
            for (int i = 0; i < Players.Count; i++)
            {
                Players[i].Name = $"Player {i} name";
                Players[i].OffsetX = (int)(5 * i - Players.Count * 2.5);
                Players[i].OffsetY = (int)(5 * i - Players.Count * 2.5);
            }

            return Players;
        }
    }
}