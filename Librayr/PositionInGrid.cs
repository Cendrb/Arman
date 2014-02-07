using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman_Class_Library
{
    public class PositionInGrid : IComparable<PositionInGrid>
    {
        public PositionInGrid(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }
        public PositionInGrid(int both)
        {
            this.X = both;
            this.Y = both;
        }
        public int X;
        public int Y;

        public int CompareTo(PositionInGrid other)
        {
            if (X == other.X && Y == other.Y)
                return 0;
            else if (X > other.X && Y > other.Y)
                return -1;
            else
                return 1;
        }
        public static bool operator == (PositionInGrid pos1, PositionInGrid pos2)
        {
            return pos1.X == pos2.X && pos1.Y == pos2.Y;
        }
        public static bool operator != (PositionInGrid pos1, PositionInGrid pos2)
        {
            return pos1.X != pos2.X || pos1.Y != pos2.Y;
        }
        public override bool Equals(object obj)
        {
            if (obj is PositionInGrid)
                return ((PositionInGrid)obj).X == this.X && ((PositionInGrid)obj).Y == this.Y;
            else
                return false;
        }
        /// <summary>
        /// Returns new PositionInGrid instance calculated from original and entered direction
        /// </summary>
        /// <param name="direction">Enter direction to adjust</param>
        /// <returns>New PositionInGrid instance calculated from original and entered direction</returns>
        public PositionInGrid ApplyDirection(Direction direction)
        {
            switch (direction)
            {
                case global::Arman_Class_Library.Direction.up:
                    return new PositionInGrid(this.X, this.Y - 1);

                case global::Arman_Class_Library.Direction.down:
                    return new PositionInGrid(this.X, this.Y + 1);

                case global::Arman_Class_Library.Direction.left:
                    return new PositionInGrid(this.X - 1, this.Y);

                case global::Arman_Class_Library.Direction.right:
                    return new PositionInGrid(this.X + 1, this.Y);
            }
            throw new InvalidOperationException("Jak se ti to povedlo!?");
        }
    }
}
