using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Arman_Class_Library
{
    public class Detector : Block
    {
        public Color LockColor { get; set; }
        public bool Activated { get; set; }
        public bool BlockMovableBlockOnApproach { get; set; }
        public bool IsPartOfObjectives { get; set; }
        public PositionInGrid AffectedPosition { get; set; }

        public Detector(PositionInGrid position, string name, double blastResistance, Color lockColor, bool blockMovableBlockOnApproach, bool isPartOfObjectives)
            : base(position, name, false, blastResistance)
        {
            BlockMovableBlockOnApproach = blockMovableBlockOnApproach;
            LockColor = lockColor;
            IsPartOfObjectives = isPartOfObjectives;
            AffectedPosition = new PositionInGrid(-1);
        }
        public Detector(PositionInGrid position, double blastResistance, Color lockColor, bool blockMovableBlockOnApproach, bool isPartOfObjectives, PositionInGrid affectedPosition)
            : base(position, "Detector", false, blastResistance)
        {
            BlockMovableBlockOnApproach = blockMovableBlockOnApproach;
            LockColor = lockColor;
            IsPartOfObjectives = isPartOfObjectives;
            AffectedPosition = affectedPosition;
        }
        public Detector()
        {

        }
    }
}
