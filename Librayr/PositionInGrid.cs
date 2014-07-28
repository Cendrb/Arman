using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman_Class_Library
{
    [Serializable]
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

        public PositionInGrid()
        {
            X = 0;
            Y = 0;
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
        public static bool operator ==(PositionInGrid pos1, PositionInGrid pos2)
        {
            return pos1.X == pos2.X && pos1.Y == pos2.Y;
        }
        public static bool operator !=(PositionInGrid pos1, PositionInGrid pos2)
        {
            if ((object)pos1 == null && (object)pos2 == null)
                return false;
            if ((object)pos1 == null || (object)pos2 == null)
                return true;
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
        public PositionInGrid ApplyDirection(Direction direction, int difference)
        {
            switch (direction)
            {
                case global::Arman_Class_Library.Direction.up:
                    return new PositionInGrid(this.X, this.Y - difference);

                case global::Arman_Class_Library.Direction.down:
                    return new PositionInGrid(this.X, this.Y + difference);

                case global::Arman_Class_Library.Direction.left:
                    return new PositionInGrid(this.X - difference, this.Y);

                case global::Arman_Class_Library.Direction.right:
                    return new PositionInGrid(this.X + difference, this.Y);
            }
            throw new InvalidOperationException("Jak se ti to povedlo!?");
        }
        public override string ToString()
        {
            return String.Join(",", X, Y);
        }
        public static Direction GetDirection(PositionInGrid from, PositionInGrid to)
        {
            int xDif = from.X - to.X;
            int yDif = from.Y - to.Y;

            if(Math.Abs(xDif) > Math.Abs(yDif))
            {
                if (xDif < 0)
                    return Direction.right;
                else
                    return Direction.left;
            }
            else
            {
                if (yDif > 0)
                    return Direction.up;
                else
                    return Direction.down;
            }
            throw new InvalidOperationException("Unknown direction");
        }
        public static List<PositionInGrid> GetPositionsInSquareRadius(int r, PositionInGrid center)
        {
            List<PositionInGrid> positions = new List<PositionInGrid>();
            PositionInGrid corner = center.ApplyDirection(Direction.up, r).ApplyDirection(Direction.left, r);
            for (int x = 2 * r; x >= 0; x--)
                for (int y = 2 * r; y >= 0; y--)
                    positions.Add(new PositionInGrid(corner.X + x, corner.Y + y));
            return positions;
        }
        public static PositionInGrid Parse(string text)
        {
            string[] values = text.Split(',');
            int x = int.Parse(values[0]);
            int y = int.Parse(values[1]);
            return new PositionInGrid(x, y);
        }
        public static int GetSquareDistance(PositionInGrid from, PositionInGrid to)
        {
            return Math.Abs((from.X - to.X) * (from.Y - to.Y));
        }
    }
}
