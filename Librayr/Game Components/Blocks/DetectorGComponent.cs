using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman_Class_Library
{
    public class DetectorGComponent : BlockGComponent
    {
        public new Detector Model { get; private set; }
        private bool blockRemoved = false;
        public bool Activated { get; private set; }

        public DetectorGComponent(GameComponents tools, Detector model)
            : base(tools, model)
        {
            this.Model = model;
        }
        public override void Update(GameTime gameTime)
        {
            IEnumerable<MovableBlockGComponent> entityGComponents = tools.GetGameComponentsAt<MovableBlockGComponent>(Position);

            MovableBlock mb;
            foreach (MovableBlockGComponent entity in entityGComponents)
            {
                mb = entity.Model;
                if (Model.LockColor.GetXnaColor() == Color.White || Model.LockColor == mb.KeyColor || mb.KeyColor == Model.LockColor)
                {
                    Activated = true;
                    mb.CanBePushed = !Model.BlockMovableBlockOnApproach;
                }
                else
                {
                    Activated = false;
                }
            }
            if (Activated && Model.AffectedPosition.X != -1 && !blockRemoved)
            {
                IEnumerable<SolidGComponent> affectedBlock = tools.GetGameComponentsAt<SolidGComponent>(Model.AffectedPosition);
                if (affectedBlock != null)
                {
                    tools.RemoveBlock(affectedBlock.First());
                    tools.AddBlock(new AirGComponent(tools, new Air(Model.AffectedPosition)));
                }
            }
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            Vector2 coor = GetAbsoluteCoordinates();
            Rectangle rect = new Rectangle((int)coor.X + tools.Data.OneBlockSize / 3, (int)coor.Y + tools.Data.OneBlockSize / 3, tools.Data.OneBlockSize / 3, tools.Data.OneBlockSize / 3);

            Textures.SpriteBatch.Begin();
            Textures.SpriteBatch.Draw(Textures.KeyLockColorDot, rect, Model.LockColor.GetXnaColor());
            Textures.SpriteBatch.End();       
        }
        protected override void LoadContent()
        {
            texture = Textures.Detector;
            base.LoadContent();
        }
    }
}
