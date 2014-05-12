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

        public MovableBlock(Game game, PositionInGrid position, GameDataTools tools, bool canPush, bool canBePushed, string name, Color keyColor)
            : base(game, position, tools, canPush, canBePushed, name, GameArea.Speed)
        {
            KeyColor = keyColor;
        }
        protected override void LoadContent()
        {
            texture = Textures.MovableBlock;
            base.LoadContent();
        }
    }
}
