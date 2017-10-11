
namespace SnakeBeauty
{
    internal class Direction
    {
        protected bool Equals(Direction other)
        {
            return _direction == other._direction;
        }

        public override int GetHashCode()
        {
            // ReSharper disable once NonReadonlyMemberInGetHashCode
            return _direction.GetHashCode();
        }

        public const short Up = 0;
        public const short Right = 1;
        public const short Down = 2;
        public const short Left = 3;

        private short _direction;

        //Instantiates a new _direction
        public Direction(short direction)
        {
            _direction = direction;
        }

        // Set new _direction
        public short Set(short d)
        {
            _direction = d;
            return _direction;
        }

        public static bool operator ==(Direction a, Direction b)
        {
            if (a == b) return true;
            else if (b != null && (a != null && a._direction == b._direction)) return true;
            return false;
        }

        public static bool operator ==(Direction a, int b)
        {
            return a != null && a._direction == b;
        }

        public static bool operator !=(Direction a, Direction b) { return !(a == b); }
        public static bool operator !=(Direction a, int b) { return !(a == b); }

        public override bool Equals(object obj)
        {
            while (true)
            {
                if (!(obj is short s)) continue;
                return _direction == s || Equals(obj);
            }
        }

    }
}
