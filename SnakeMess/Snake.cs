using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnakeBeauty
{
    class Snake
    {

        public Direction moveDir;
        public List<Point> Points { get; private set; }
        public Snake()
        {
            Points = new List<Point>();
            moveDir = new Direction(2);
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
            return Points.Last();
        }

        public void SetHead(Point newH)
        {
            Points.Add(newH);
        }

        public Point GetEnd()
        {
            return Points.First();
        }

        public void RemoveLast()
        {
            Points.RemoveAt(0);
        }

        public void RemovePointAt(int i)
        {
            Points.RemoveAt(i);
        }

        public Direction GetDirection()
        {
            return moveDir;
        }

        public short ChangeDirection(short s)
        {
            return moveDir.Set(s);
        }
    }
}
