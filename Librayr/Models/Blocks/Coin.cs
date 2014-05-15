using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman_Class_Library
{
    public class Coin : Block
    {
        public int Value { get; set; }

        public Coin(PositionInGrid position, string name, double explosionResistance, int value)
            : base(position, name, false, explosionResistance)
        {
            Value = value;
        }
        public Coin(PositionInGrid position, double explosionResistance, int value)
            : base(position, "Coin", false, explosionResistance)
        {
            Value = value;
        }
        public Coin()
        {

        }
    }
}
