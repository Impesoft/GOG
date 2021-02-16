using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;

namespace GameOfGoose
{
    public static class Settings
    {
        public static int Turn { get; set; } = 0;
        public static int NumberOfPlayers { get; set; }
        public static ObservableCollection<Player> Players = new ObservableCollection<Player>();
        public static List<Image> PawnList = new List<Image>();
    }
}