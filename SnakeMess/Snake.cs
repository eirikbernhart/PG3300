using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnakeMess
{
    class Snake
    {

        class Direction
        {
            static int LEFT = 
        }

        List<Point> snake; 
        Snake()
        {
            snake = new List<Point>();
        }

        int Length()
        {
            return snake.Count();
        }

        Point AddTail(Point point)
        {
            snake.Add(point);
            return point;
        }

        Point GetHead()
        {
            return snake.First();
        }

        Point GetEnd()
        {
            return snake.Last();
        }

        void RemoveLast()
        {
            snake.RemoveAt(Length());
        }

        void RemoveTailAt(int i)
        {
            snake.RemoveAt(i);
        }
    }
}
