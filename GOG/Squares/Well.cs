﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfGoose.Squares
{
    internal class Well : Square
    {
        public Player WellPlayer;

        public string Name { get; set; } = "Well";

        public override void Move(Player player)
        {
            // Skip 1 Turn
            WellPlayer = player;
        }
    }
}