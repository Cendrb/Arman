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
        public Home(Game game, PositionInGrid position, GameDataTools tools)
            : base(game, position, tools)
        {

        }
        protected override void LoadContent()
        {
            texture = Textures.Home;
            base.LoadContent();
        }
    }
}
