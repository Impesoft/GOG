﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace GameOfGoose
{
    public class PlayerPawn
    {
        public Image Pawn { get; set; } //= new Image();
        public int OffsetX { get; set; }
        public int OffsetY { get; set; }
        public Location PlayerLocation { get; set; } = new Location { X = 0, Y = 0 };
        private readonly TranslateTransform _interactiveTranslateTransform;

        public PlayerPawn(Image pawn)
        {
            Pawn = pawn;
            OffsetX = 0;
            OffsetY = 0;
            _interactiveTranslateTransform = new TranslateTransform();
            Pawn.RenderTransform =
                _interactiveTranslateTransform;
        }

        public void Move(int endPosition)
        {
            // Set the target point so the center of the ellipse
            // ends up at the clicked point.
            var targetPoint = new Point
            {
                X = Locations.List[endPosition].X - Pawn.Width / 2,
                Y = Locations.List[endPosition].Y + OffsetY - Pawn.Height
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