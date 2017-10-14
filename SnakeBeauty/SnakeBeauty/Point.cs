using System;

namespace SnakeBeauty
{
    internal class Point
    {
        public int X; public int Y;
        public Point(int x = 0, int y = 0) { X = x; Y = y; }
        public Point(Point input) { X = input.X; Y = input.Y; }

        public static Point GetRandomPoint(Board board)
        {
            var random = new Random();
            var x = random.Next(0, board.Width);
            var y = random.Next(0, board.Height);
            return new Point(x, y);
        }
    }
}
