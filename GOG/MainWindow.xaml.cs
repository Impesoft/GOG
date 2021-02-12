using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GameOfGoose
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int IAmHere = 0;
        public GameBoard GameBoard;

        public MainWindow()
        {
            InitializeComponent();
            GameBoard = new GameBoard();
            GameBoard.GetSquares();
            double x = GameBoard.Locations[IAmHere].X - Player1.Width;
            double y = GameBoard.Locations[IAmHere].Y - Player1.Height;
            Canvas.SetLeft(Player1, x);
            Canvas.SetTop(Player1, y);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            IAmHere++;
            IAmHere %= 63;
            double x = GameBoard.Locations[IAmHere].X - Player1.Width / 2;
            double y = GameBoard.Locations[IAmHere].Y - Player1.Height;
            Canvas.SetLeft(Player1, x);
            Canvas.SetTop(Player1, y);
        }
    }
}