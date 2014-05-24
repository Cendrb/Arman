using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman_Class_Library
{
    public class GameComponent : DrawableGameComponent
    {
        protected GameComponents tools;
        protected Texture2D texture;
        public GameElement Model { get; private set; }

        public PositionInGrid Position
        {
            get
            {
                return Model.Position;
            }
            protected set
            {
                Model.Position = value;
            }
        }

        public GameComponent(GameComponents tools, GameElement model)
            : base(tools.Game)
        {
            this.tools = tools;
            this.Model = model;
        }
        public override void Draw(GameTime gameTime)
        {
            Textures.SpriteBatch.Begin();
            Textures.SpriteBatch.Draw(texture, GetAbsoluteCoordinates(), Color.White);
            Textures.SpriteBatch.End();
            base.Draw(gameTime);
        }
        public virtual Vector2 GetAbsoluteCoordinates()
        {
            return new Vector2(69.0F);
        }
    }
}
