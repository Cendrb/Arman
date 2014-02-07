using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman_Class_Library
{
    public class Coin : Block
    {
        public int Value { get; private set; }

        public Coin(Game game, SpriteBatch spriteBatch, PositionInGrid position, Texture2D texture, int value)
            : base(game, spriteBatch, position, texture)
        {

        }
    }
}
