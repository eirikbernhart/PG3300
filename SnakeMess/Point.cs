using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnakeBeauty
{
    //Represents a point on the board. Keeps track of the x and y coordinates
    class Point
    {
        public const string Ok = "Ok";

        public int X; public int Y;
        public Point(int x = 0, int y = 0) { X = x; Y = y; }
        public Point(Point input) { X = input.X; Y = input.Y; }

        public static Point GetRandomPoint(Board board)
        {
            Random rnd = new Random();
            int x = rnd.Next(0, board.Width);
            int y = rnd.Next(0, board.Height);
            return new Point(x, y);
        }
    }
}
