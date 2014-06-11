using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman_Class_Library
{
    public class CoinGComponent : BlockGComponent
    {
        public new Coin Model { get; private set; }
        public CoinGComponent(World tools, Coin model)
            : base(tools, model)
        {
            this.Model = model;
        }
        protected override void LoadContent()
        {
            texture = Textures.Coin;
            base.LoadContent();
        }
    }
}
