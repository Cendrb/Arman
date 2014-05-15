using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman_Class_Library
{
    public class GameComponent : DrawableGameComponent
    {
        protected GameDataTools tools;
        private GameElement model;

        public GameComponent(Game game, GameDataTools tools, GameElement model)
            : base(game)
        {
            this.tools = tools;
            this.model = model;
        }
    }
}
