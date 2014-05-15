using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman_Class_Library.Game_Components.Blocks
{
    public class CoinGComponent : BlockGComponent
    {
        private Coin model;
        public CoinGComponent(Game game, GameDataTools tools, Coin model)
            : base(game, tools, model)
        {

            this.model = model;
        }
    }
}
