using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman_Class_Library
{
    public class SolidGComponent : BlockGComponent
    {
        public new Solid Model { get; private set; }
        public SolidGComponent(World tools, Solid model)
            : base(tools, model)
        {
            Model = model;
        }
        protected override void LoadContent()
        {
            texture = Textures.Solid;
            base.LoadContent();
        }
    }
}
