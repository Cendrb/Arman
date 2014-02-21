using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman_Class_Library.Blocks
{
    public class Air : Block
    {
        public Air(Game game, SpriteBatch spriteBatch, PositionInGrid position)
            : base(game, spriteBatch, position)
        {

        }
        protected override void LoadContent()
        {
            base.LoadContent();
        }
        public override void Draw(GameTime gameTime)
        {


            base.Draw(gameTime);
        }
    }
}
