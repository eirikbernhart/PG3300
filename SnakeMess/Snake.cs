using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnakeMess
{
    class Snake
    {

        Direction moveDir = new Direction(2);
        List<Point> snake;
        public Snake()
        {
            snake = new List<Point>();
        }

        public int Length()
        {
            return snake.Count();
        }

        public Point AddTail(Point point)
        {
            snake.Add(point);
            return point;
        }

        public Point GetHead()
        {
            return snake.First();
        }

        public Point GetEnd()
        {
            return snake.Last();
        }

        public void RemoveLast()
        {
            snake.RemoveAt(Length());
        }

        public void RemoveTailAt(int i)
        {
            snake.RemoveAt(i);
        }

        public Direction GetDirection()
        {
            return moveDir;
        }
    }
}
