using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman_Class_Library
{
    public class DetectorGComponent : BlockGComponent
    {
        private Detector model;
        private bool blockRemoved = false;

        public bool Activated { get; private set; }

        public DetectorGComponent(Game game, GameDataTools tools, Detector model)
            : base(game, tools, model)
        {

            this.model = model;
        }
        public override void Update(GameTime gameTime)
        {
            Entity entity = tools.GetEntityAt(model.Position);
            if (entity is MovableBlock)
            {
                if (Color.AliceBlue == Color.White || Color.AliceBlue == Color.White/* || (entity as MovableBlock).KeyColor == LockColor*/)
                {
                    Activated = true;
                    (entity as MovableBlock).CanBePushed = !model.BlockMovableBlockOnApproach;
                }
            }
            else
            {
                Activated = false;
            }
            if (Activated && model.AffectedPosition.X != -1 && !blockRemoved)
            {
                BlockGComponent affectedBlock = tools.GetBlockAt(model.AffectedPosition);
                if (affectedBlock != null)
                {
                    AirGComponent air = new Air(model.AffectedPosition);
                    tools.Data.Blocks.Remove(affectedBlock);
                    tools.Data.Blocks.Add(air);
                    Game.Components.Remove(affectedBlock);
                    Game.Components.Insert(1, air);
                    blockRemoved = true;
                }
            }
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            Vector2 coor = getAbsoluteCoordinates();
            Rectangle rect = new Rectangle((int)coor.X + tools.Data.OneBlockSize / 3, (int)coor.Y + tools.Data.OneBlockSize / 3, tools.Data.OneBlockSize / 3, tools.Data.OneBlockSize / 3);

            spriteBatch.Begin();
            spriteBatch.Draw(Textures.KeyLockColorDot, rect, LockColor);
            spriteBatch.End();
        }
    }
}
