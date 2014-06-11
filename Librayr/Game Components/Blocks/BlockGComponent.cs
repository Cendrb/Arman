using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman_Class_Library
{
    public class BlockGComponent : GameComponent
    {
        public new Block Model { get; private set; }
        public BlockGComponent(World tools, Block model)
            : base(tools, model)
        {
            this.DrawOrder = 5;
            Model = model;
        }

        public Vector2 GetRelativeCoordinates()
        {
            return new Vector2(Model.Position.X * World.Data.OneBlockSize, Model.Position.Y * World.Data.OneBlockSize);
        }
        public override Vector2 GetAbsoluteCoordinates()
        {
            Vector2 coor = GetRelativeCoordinates();
            coor.X += GameArea.StartingCoordinates.X;
            coor.Y += GameArea.StartingCoordinates.Y;
            return coor;
        }
    }
}
