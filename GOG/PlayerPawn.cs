using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace GameOfGoose
{
    public class PlayerPawn : Pawn
    {
        public int OffsetX { get; set; }
        public int OffsetY { get; set; }

        private readonly TranslateTransform _interactiveTranslateTransform;

        public PlayerPawn(Image pawnImage) : base(pawnImage: pawnImage)
        {
            PawnImage = pawnImage;
            OffsetX = 0;
            OffsetY = 0;
            _interactiveTranslateTransform = new TranslateTransform();
            PawnImage.RenderTransform =
                _interactiveTranslateTransform;
            Move(0);
        }

        public void Move(int endPosition)
        {
            // Set the target point so the center of the ellipse
            // ends up at the clicked point.
            var targetPoint = new Point
            {
                X = Locations.List[endPosition].X - 25,
                Y = Locations.List[endPosition].Y - 43 //-600?
            };
            // Animate to the target point.
            var xAnimation =
                new DoubleAnimation(targetPoint.X,
                    new Duration(TimeSpan.FromMilliseconds(500)));
            _interactiveTranslateTransform.BeginAnimation(
                TranslateTransform.XProperty, xAnimation, HandoffBehavior.SnapshotAndReplace);

            var yAnimation =
                new DoubleAnimation(targetPoint.Y,
                    new Duration(TimeSpan.FromMilliseconds(500)));
            _interactiveTranslateTransform.BeginAnimation(
                TranslateTransform.YProperty, yAnimation, HandoffBehavior.SnapshotAndReplace);
        }
    }
}