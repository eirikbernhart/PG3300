
namespace SnakeBeauty
{
    //Keeps track of a certain direction given upon instantiating
    public class Direction
    {
        

        public override int GetHashCode()
        {
            // ReSharper disable once NonReadonlyMemberInGetHashCode
            return _direction.GetHashCode();
        }

        protected bool Equals(Direction other)
        {
            return _direction == other._direction;
        }

        public const short Up = 0;
        public const short Right = 1;
        public const short Down = 2;
        public const short Left = 3;

        private short _direction;

        //Instantiates a new direction
        public Direction(short direction)
        {
            _direction = direction;
        }

        // Set new direction
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
            // ReSharper disable once PossibleNullReferenceException
            return a._direction == b;
        }

        public static bool operator !=(Direction a, Direction b) { return !(a == b); }
        public static bool operator !=(Direction a, int b) { return !(a == b); }

        //Correct
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Direction)obj);
        }
        //Fail1
        /*
        public override bool Equals(object obj)
        {
            while (true)
            {
                if (!(obj is short s)) continue;
                return _direction == s || Equals(obj);
            }
        }
        */
        //Fail2
        /*
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
        */


    }
}
