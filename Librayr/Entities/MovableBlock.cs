using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman_Class_Library
{
    public class MovableBlock : Entity
    {
        public Color KeyColor { get; private set; }

        public MovableBlock(Game game, PositionInGrid position, string texture, GameDataTools tools, bool canPush, bool canBePushed, string name, Color keyColor)
            : base(game, position, texture, tools, canPush, canBePushed, name, GameArea.Speed)
        {
            KeyColor = keyColor;
        }
    }
}
