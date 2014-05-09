using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman_Class_Library
{
    public class Solid : Block
    {
        public Solid(Game game, PositionInGrid position, GameDataTools tools)
            : base(game, texture, tools)
        {

        }
        protected override void LoadContent()
        {
            texture = Textures.Solid;
            base.LoadContent();
        }
    }
}
