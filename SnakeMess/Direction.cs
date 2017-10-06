using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnakeMess
{
    class Direction
    {
        public const short UP = 0;
        public const short RIGHT = 1;
        public const short DOWN = 2;
        public const short LEFT = 3;

        private short direction;
        private short lastDirection;

        public Direction(short direction)
        {
            this.direction = direction;
            this.lastDirection = this.direction;
        }

        public short AsInt()
        {
            return direction;
        }

        public short Set(Direction d)
        {
            lastDirection = direction;
            direction = d.AsInt();
            return direction;
        }

        public Direction GetLast()
        {
            return new Direction(lastDirection);
        }

        public static bool operator ==(Direction a, Direction b)
        {
            if (a == b) return true;
            else if (a.direction == b.direction) return true;
            return false;
        }

        public static bool operator ==(Direction a, int b)
        {
            if (a.direction == b) return true;
            return false;
        }

        public static bool operator !=(Direction a, Direction b) { return !(a == b); }
        public static bool operator !=(Direction a, int b) { return !(a == b); }
    }
}
