using System.ComponentModel;
using System.Windows.Controls;

namespace GameOfGoose
{
    public interface IPawn : INotifyPropertyChanged
    {
        Image PawnImage { get; set; }
    }
}