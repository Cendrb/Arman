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
        protected SpriteBatch spriteBatch;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="game">Main game</param>
        /// <param name="spriteBatch">Where to draw textures</param>
        /// <param name="position">Where it should be placed</param>
        public Block(Game game, SpriteBatch spriteBatch, PositionInGrid position, Texture2D texture)
            : base(game)
        {
            this.game = game;
            Position = position;
            this.spriteBatch = spriteBatch;
        }
    }
}
