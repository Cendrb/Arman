using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Arman_Class_Library
{
    public abstract class Block : Microsoft.Xna.Framework.DrawableGameComponent
    {

        public PositionInGrid Position { get; set; }

        protected Game game;

        private SpriteBatch spriteBatch;
        private Texture2D texture;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="game">Main game</param>
        /// <param name="spriteBatch">Where to draw textures</param>
        /// <param name="position">Where it should be placed</param>
        /// <param name="texture">What to draw on Draw() call</param>
        public Block(Game game, SpriteBatch spriteBatch, PositionInGrid position, Texture2D texture)
            : base(game)
        {
            this.game = game;
            Position = position;
            this.spriteBatch = spriteBatch;
            this.texture = texture;
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Draw(texture, StaticProperties.startingCoordinates, Color.White);

            base.Draw(gameTime);
        }
    }
}
