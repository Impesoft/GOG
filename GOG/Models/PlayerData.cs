using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Printing;
using System.Runtime.CompilerServices;
using System.Text;
using GameOfGoose.Annotations;

namespace GameOfGoose
{
    public class PlayerData : INotifyPropertyChanged
    {
        public string Name { get; set; }

        private ObservableCollection<Player> _players;

        public ObservableCollection<Player> Players
        {
            get { return _players; }
            set
            {
                _players = value;
                OnPropertyChanged();
            }
        }

        public override string ToString()
        {
            return Name;
        }

        public PlayerData()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}