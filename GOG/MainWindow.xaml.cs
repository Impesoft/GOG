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
using System.Windows.Media.Animation;
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
            IAmHere %= 64;
            double x = GameBoard.Locations[IAmHere].X - Player1.Width / 2;
            double y = GameBoard.Locations[IAmHere].Y - Player1.Height;
            //Canvas.SetLeft(Player1, x);
            //Canvas.SetTop(Player1, y);
            MoveTo(Player1, x, y);
        }

        public static void MoveTo(Image target, double newX, double newY)
        {
            Vector offset = VisualTreeHelper.GetOffset(target);
            var left = offset.X;
            var top = offset.Y;
            TranslateTransform trans = new TranslateTransform();
            target.RenderTransform = trans;
            DoubleAnimation anim1 = new DoubleAnimation(0, newY - top, TimeSpan.FromSeconds(1));
            DoubleAnimation anim2 = new DoubleAnimation(0, newX - left, TimeSpan.FromSeconds(1));
            trans.BeginAnimation(TranslateTransform.YProperty, anim1);
            trans.BeginAnimation(TranslateTransform.XProperty, anim2);
        }
    }
}