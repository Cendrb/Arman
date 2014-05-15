using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Arman_Class_Library
{
    public class MovableBlock : Entity
    {
        public Color KeyColor { get; set; }

        public MovableBlock(PositionInGrid position, string name, bool canPush, bool canBePushed, float speed, bool collides, double health, bool invulnerable, Color keyColor)
            : base(position, name, canPush, canBePushed, speed, collides, health, invulnerable)
        {
            KeyColor = keyColor;
        }
        public MovableBlock(PositionInGrid position, bool canPush, bool canBePushed, float speed, bool collides, double health, bool invulnerable, Color keyColor)
            : base(position, "Movable block", canPush, canBePushed, speed, collides, health, invulnerable)
        {
            KeyColor = keyColor;
        }
        public MovableBlock()
        {

        }
    }
}
