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
            List<string> names = new List<string>() { "Dries", "Nick", "Ward", "Someone's Els" };
            Players = new List<Player>();
            //{
            //    new Player(){Name= "Dries"},
            //    new Player(){Name= "Nick"},
            //    new Player(){Name= "Ward"},
            //    new Player(){Name= "SomeOne's Els"},
            //};
            NumberOfPlayers = 4;
            for (int i = 0; i < NumberOfPlayers; i++)
            {
                Players.Add(new Player() { Name = names[i], OffsetX = (int)(5 * i - NumberOfPlayers * 2.5), OffsetY = (int)(5 * i - NumberOfPlayers * 2.5) });
                //Players[i].Name = $"Player {i} name";
                //Players[i].OffsetX = (int)(5 * i - Players.Count * 2.5);
                //Players[i].OffsetY = (int)(5 * i - Players.Count * 2.5);
            }

            return Players;
        }
    }
}