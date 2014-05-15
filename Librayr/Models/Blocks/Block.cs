using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace Arman_Class_Library
{
    public class Block : GameElement
    {
        public bool Solid { get; protected set; }
        public double BlastResistance { get; protected set; }

        public Block(PositionInGrid position, string name, bool solid, double blastResistence)
            : base(position, name)
        {
            Solid = solid;
            BlastResistance = blastResistence;
        }
        public Block(PositionInGrid position, bool solid, double blastResistence)
            : base(position, "Generic block")
        {
            Solid = solid;
            BlastResistance = blastResistence;
        }
        public Block()
        {

        }
    }
}
