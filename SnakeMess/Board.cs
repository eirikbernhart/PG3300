using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnakeMess
{
    class Board
    {
        int boardX;
        int boardY;

        Board()
        {
            boardX = Console.WindowWidth;
            boardY = Console.WindowHeight;
        }

        int getWidth()
        {
            return boardX;
        }

        int getHeight()
        {
            return boardY;
        }
    }
}
