using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman_Class_Library.Game_Components.Blocks
{
    public class HomeGComponent : BlockGComponent
    {
        private Home model;
        public HomeGComponent(Game game, GameDataTools tools, Home model)
            : base(game, tools, model)
        {

            this.model = model;
        }
    }
}
