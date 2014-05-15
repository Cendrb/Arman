using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman_Class_Library.Game_Components.Blocks
{
    public class SolidGComponent : BlockGComponent
    {
        private Solid model;
        public SolidGComponent(Game game, GameDataTools tools, Solid model)
            : base(game, tools, model)
        {

            this.model = model;
        }
    }
}
