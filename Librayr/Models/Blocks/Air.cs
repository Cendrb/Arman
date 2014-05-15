using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman_Class_Library
{
    public class Air : Block
    {
        public Air(PositionInGrid position, string name)
            : base(position, name, false, 500000.0 )
        {

        }
        public Air(PositionInGrid position)
            : base(position, "Air block" ,false, 500000.0)
        {

        }
    }
}
