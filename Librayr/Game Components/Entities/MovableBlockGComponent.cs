using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman_Class_Library
{
    public class MovableBlockGComponent : EntityGComponent
    {
        public new MovableBlock Model { get; private set; }
        public MovableBlockGComponent(GameComponents tools, MovableBlock entity)
            : base(tools, entity)
        {
            Model = entity;
        }
        protected override void LoadContent()
        {
            texture = Textures.MovableBlock;
            base.LoadContent();
        }
    }
}
