using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Arman_Class_Library
{
    public class GameElement : Microsoft.Xna.Framework.DrawableGameComponent
    {
        protected SpriteBatch spriteBatch;
        protected Texture2D texture;
        protected Game game;
        protected GameDataTools tools;

        public PositionInGrid Position { get; protected set; }

        public GameElement(Game game, PositionInGrid position, GameDataTools tools)
            : base(game)
        {
            this.tools = tools;
            this.game = game;
            Position = position;
        }
        protected override void LoadContent()
        {
            spriteBatch = Textures.SpriteBatch;
        }
    }
}
