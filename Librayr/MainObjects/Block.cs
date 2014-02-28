using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;


namespace Arman_Class_Library
{
    [Serializable]
    public abstract class Block : GameElement
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="game">Main game</param>
        /// <param name="spriteBatch">Where to draw textures</param>
        /// <param name="position">Where it should be placed</param>
        /// <param name="texture">What to draw on Draw() call</param>
        public Block(Game game, PositionInGrid position, string texture, GameDataTools tools)
            : base(game, position, texture, tools)
        {

        }
        public override void Draw(GameTime gameTime)
        {
            Vector2 position = getAbsoluteCoordinates();
            spriteBatch.Begin();
            spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, GameArea.OneBlockSize, GameArea.OneBlockSize), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
        private Vector2 getRelativeCoordinates()
        {
            return new Vector2(Position.X * GameArea.OneBlockSize, Position.Y * GameArea.OneBlockSize);
        }
        private Vector2 getAbsoluteCoordinates()
        {
            Vector2 position = getRelativeCoordinates();
            return new Vector2(position.X + GameArea.StartingCoordinates.X, position.Y + GameArea.StartingCoordinates.Y);
        }
    }
}
