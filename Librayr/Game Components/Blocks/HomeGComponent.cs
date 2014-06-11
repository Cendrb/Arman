using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman_Class_Library
{
    public class HomeGComponent : BlockGComponent
    {
        public new Home Model { get; private set; }
        public HomeGComponent(World tools, Home model)
            : base(tools, model)
        {
            Model = model;
        }
        protected override void LoadContent()
        {
            texture = Textures.Home;
            base.LoadContent();
        }
    }
}
