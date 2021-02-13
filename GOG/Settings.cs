using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Controls;

namespace GameOfGoose
{
    public class Settings
    {
        public int Turn { get; set; } = 0;
        public int NumberOfPlayers { get; set; }
        public List<Player> Players;

        public List<Player> GetPlayers()
        {
            List<string> names = new List<string>() { "Dries", "Nick", "Ward", "Someone's Els" };

            Players = new List<Player>();
            NumberOfPlayers = 4;
            for (int i = 0; i < NumberOfPlayers; i++)
            {
                Players.Add(new Player() { Name = names[i], OffsetX = (int)(5 * i - NumberOfPlayers * 2.5), OffsetY = (int)(5 * i - NumberOfPlayers * 2.5), Pawn = new Image() });
            }

            return Players;
        }
    }
}