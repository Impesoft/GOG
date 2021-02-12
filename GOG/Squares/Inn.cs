﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfGoose.Squares
{
    internal class Inn : Square
    {
        public override string Name { get; set; } = "Inn";

        public override void Move(Player player)
        {
            // Skip 1 Turn
            player.ToSkipTurns = 1;
        }
    }
}