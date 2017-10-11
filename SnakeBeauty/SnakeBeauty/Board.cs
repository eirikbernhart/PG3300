using System;

namespace SnakeBeauty
{
    // Defines the boundaries of the world (game board/console)
    class Board
    {
        public int Width { get; }
        public int Height { get; }

        //Creates a new board with a width and height fixed to the size of the window
        public Board()
        {
            Width = Console.WindowWidth;
            Height = Console.WindowHeight;
        }
    }
}
