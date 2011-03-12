﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Valker.PlayOnLan.Api.Game;

namespace Valker.TicTacToePlugin
{
    public class TicTacToeParameters : IGameParameters
    {
        public TicTacToeParameters(int stones, int width)
        {
            Stones = stones;
            Width = width;
        }

        public int Stones { get; set; }

        public int Width { get; set; }
    }
}
