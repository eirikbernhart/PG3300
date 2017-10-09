using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnakeBeauty
{
    // Defines the boundaries of the world
    class Board
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        //Creates a new board with a width and height fixed to the size of the window
        public Board()
        {
            Width = Console.WindowWidth;
            Height = Console.WindowHeight;
        }
    }
}
