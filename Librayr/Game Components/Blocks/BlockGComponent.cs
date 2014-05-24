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
        public BlockGComponent(GameComponents tools, Block model)
            : base(tools, model)
        {
            this.DrawOrder = 5;
            Model = model;
        }

        public Vector2 GetRelativeCoordinates()
        {
            return new Vector2(Model.Position.X * tools.Data.OneBlockSize, Model.Position.Y * tools.Data.OneBlockSize);
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
