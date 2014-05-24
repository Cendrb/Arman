using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Xml.Serialization;


namespace Arman_Class_Library
{
    [Serializable]
    [XmlInclude(typeof(Air))]
    [XmlInclude(typeof(Coin))]
    [XmlInclude(typeof(Detector))]
    [XmlInclude(typeof(Home))]
    [XmlInclude(typeof(Solid))]
    public class Block : GameElement
    {
        public double BlastResistance { get; set; }

        public Block(PositionInGrid position, string name, bool solid, double blastResistence)
            : base(position, name)
        {
            Collides = solid;
            BlastResistance = blastResistence;
        }
        public Block(PositionInGrid position, bool solid, double blastResistence)
            : base(position, "Generic block")
        {
            Collides = solid;
            BlastResistance = blastResistence;
        }
        public Block()
        {

        }
    }
}
