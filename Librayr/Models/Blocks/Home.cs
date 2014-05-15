using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman_Class_Library
{
    public class Home : Block
    {
        public bool PlayerInside { get; private set; }
        public Home(PositionInGrid position, string name, double blastResistence)
            : base(position, name, false, blastResistence)
        {

        }
        public Home(PositionInGrid position, double blastResistence)
            : base(position, "Home", false, blastResistence)
        {

        }
    }
}
