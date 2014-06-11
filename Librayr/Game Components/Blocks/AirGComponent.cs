using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman_Class_Library
{
    public class AirGComponent : BlockGComponent
    {
        public new Air Model { get; private set; }
        public AirGComponent(World tools, Air model)
            : base(tools, model)
        {
            Model = model;
        }
        protected override void LoadContent()
        {
            texture = Textures.Air;
            base.LoadContent();
        }
    }
}
