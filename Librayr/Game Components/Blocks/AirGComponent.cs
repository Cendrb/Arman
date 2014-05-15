using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman_Class_Library.Game_Components
{
    public class AirGComponent : BlockGComponent
    {
        private Air model;
        public AirGComponent(Game game, GameDataTools tools, Air model)
            : base(game, tools, model)
        {

            this.model = model;
        }
    }
}
