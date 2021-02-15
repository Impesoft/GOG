using System.Collections.Generic;
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
                //                 player.PlayerLocation.X = Locations.List[0].X;
                //                player.PlayerLocation.Y = Locations.List[0].Y;

                Players.Add(new Player() { Name = names[i], OffsetX = (int)(5 * i - NumberOfPlayers * 2.5), OffsetY = (int)(5 * i - NumberOfPlayers * 2.5), Pawn = new Image(), PlayerLocation = new Location() { X = Locations.List[0].X + 10 * i, Y = Locations.List[0].Y } });
            }

            return Players;
        }
    }
}