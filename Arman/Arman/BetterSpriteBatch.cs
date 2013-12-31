using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman
{
    public class BetterSpriteBatch : SpriteBatch
    {
        public BetterSpriteBatch(GraphicsDevice graphicsDevice) : base(graphicsDevice)
        {

        }
        public void DrawStringWithShadow(SpriteFont spriteFont, string text, Vector2 position, Color color)
        {
            DrawString(spriteFont, text, new Vector2(position.X + 2, position.Y + 2), Color.Black * 0.8f);
            DrawString(spriteFont, text, position, color);
        }
    }
}
