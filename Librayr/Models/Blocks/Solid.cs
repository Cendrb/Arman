using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman_Class_Library
{
    public class Solid : Block
    {
        public Solid(PositionInGrid position, string name, double blastResistence)
            : base(position, name, true, blastResistence)
        {

        }
        public Solid(PositionInGrid position, double blastResistence)
            : base(position, "Solid", true, blastResistence)
        {

        }
        public Solid()
        {

        }
    }
}
