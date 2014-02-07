using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using Arman_Class_Library;


namespace Arman
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Block : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public BlockType Type { get; set; }
        private Texture2D texture;
        private Arman game;
        private float oneBlockSize;
        public PositionInGrid Position { get; set; }

        public Block(Arman game, BlockType type, PositionInGrid position, int oneBlockSize)
            : base(game)
        {
            this.game = game;
            Type = type;
            Position = position;
            this.oneBlockSize = oneBlockSize;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            base.Update(gameTime);
        }
        public new void LoadContent()
        {
            if (Type == BlockType.solid)
                texture = game.Content.Load<Texture2D>(@"Sprites/Blocks/wall");
            else if (Type == BlockType.nonsolid)
                texture = game.Content.Load<Texture2D>(@"Sprites/Blocks/nonsolid");
            else if (Type == BlockType.detector)
                texture = game.Content.Load<Texture2D>(@"Sprites/Blocks/detector");
            else if (Type == BlockType.coin)
                texture = game.Content.Load<Texture2D>(@"Sprites/Blocks/coin");
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            game.spriteBatch.Draw(texture, new Vector2(Position.X * oneBlockSize + 510, Position.Y * oneBlockSize + 290), Color.White);

            base.Draw(gameTime);
        }
    }
}
