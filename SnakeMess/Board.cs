using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnakeBeauty
{
    class Board
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Board()
        {
            Width = Console.WindowWidth;
            Height = Console.WindowHeight;
        }
    }
}
