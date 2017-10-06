using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnakeMess
{
    class Snake
    {

        Direction moveDir = new Direction(2);
        public List<Point> Points { get; private set; }
        public Snake()
        {
            Points = new List<Point>();
        }

        public int Length()
        {
            return Points.Count();
        }

        public Point AddPoint(Point point)
        {
            Points.Add(point);
            return point;
        }

        public Point GetHead()
        {
            return Points.First();
        }

        public void SetHead(Point newH)
        {
            Points[0] = newH;
        }

        public Point GetEnd()
        {
            return Points.Last();
        }

        public void RemoveLast()
        {
            Points.RemoveAt(Length());
        }

        public void RemovePointAt(int i)
        {
            Points.RemoveAt(i);
        }

        public Direction GetDirection()
        {
            return moveDir;
        }
    }
}
