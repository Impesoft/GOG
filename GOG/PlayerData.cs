using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace GameOfGoose
{
    internal class PlayerData
    {
        private ObservableCollection<Player> _players;

        public ObservableCollection<Player> Players
        {
            get { return _players; }
            set { _players = value; }
        }

        public int NumberOfPlayers { get; set; }

        public PlayerData()
        {
        }
    }
}