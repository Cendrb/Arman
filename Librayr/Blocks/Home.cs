using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman_Class_Library
{
    public class Home : Block
    {
        public bool PlayerInside { get; set; }
        public Home(Game game, SpriteBatch spriteBatch, PositionInGrid position, Texture2D texture)
            : base(game, spriteBatch, position, texture)
        {

        }
    }
}
