using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace Arman_Class_Library
{
    public abstract class Block : GameElement
    {
        public Block(PositionInGrid position)
            : base(position)
        {

        }
    }
}
