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
        public static List<Image> DiceFaces = new List<Image>();

        public static ObservableCollection<IPlayer> Players = new ObservableCollection<IPlayer>();
        public static List<Image> PawnList = new List<Image>();
        public static ObservableCollection<IPawn> AvailablePawnList = new ObservableCollection<IPawn>();
    }
}