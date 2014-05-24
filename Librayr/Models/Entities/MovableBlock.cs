using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman_Class_Library
{
    [Serializable]
    public class MovableBlock : Entity
    {
        public ArmanColor KeyColor { get; set; }

        public MovableBlock(PositionInGrid position, string name, bool canPush, bool canBePushed, ArmanColor keyColor)
            : base(position, name, canPush, canBePushed, 0, true, 1, true)
        {
            KeyColor = keyColor;
        }
        public MovableBlock(PositionInGrid position, bool canPush, bool canBePushed, ArmanColor keyColor)
            : base(position, "Movable block", canPush, canBePushed, 0, true, 1, true)
        {
            KeyColor = keyColor;
        }
        public MovableBlock()
        {

        }
    }
}
