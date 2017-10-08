using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnakeBeauty
{
    public class Direction
    {

        public const short UP = 0;
        public const short RIGHT = 1;
        public const short DOWN = 2;
        public const short LEFT = 3;

        private short direction;

        public Direction(short direction)
        {
            this.direction = direction;
        }

        public short Set(short d)
        {
            direction = d;
            return direction;
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

        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(short))
            {
                short s = (short) obj;
                if (direction == s)
                    return true;
            }
            return Equals(obj);
        }
    }
}
