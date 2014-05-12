using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman_Class_Library
{
    public class Detector : Block
    {
        public Color LockColor { get; private set; }
        public bool Activated { get; private set; }
        public bool BlockMovableBlockOnApproach { get; private set; }
        public bool IsPartOfObjectives { get; private set; }
        public PositionInGrid AffectedPosition { get; private set; }

        private bool blockRemoved = false;

        public Detector(Game game, PositionInGrid position, GameDataTools tools, Color lockColor, bool blockMovableBlockOnApproach, bool isPartOfObjectives)
            : base(game, position, tools)
        {
            BlockMovableBlockOnApproach = blockMovableBlockOnApproach;
            LockColor = lockColor;
            IsPartOfObjectives = isPartOfObjectives;
            AffectedPosition = new PositionInGrid(-1);
        }
        public Detector(Game game, PositionInGrid position, GameDataTools tools, Color lockColor, bool blockMovableBlockOnApproach, bool isPartOfObjectives, PositionInGrid affectedPosition)
            : base(game, position, tools)
        {
            BlockMovableBlockOnApproach = blockMovableBlockOnApproach;
            LockColor = lockColor;
            IsPartOfObjectives = isPartOfObjectives;
            AffectedPosition = affectedPosition;
        }
        public override void Update(GameTime gameTime)
        {
            Entity entity = tools.GetEntityAt(Position);
            if (entity is MovableBlock)
            {
                if ((entity as MovableBlock).KeyColor == Color.White || LockColor == Color.White || (entity as MovableBlock).KeyColor == LockColor)
                {
                    Activated = true;
                    (entity as MovableBlock).CanBePushed = !BlockMovableBlockOnApproach;
                }
            }
            else
            {
                Activated = false;
            }
            if (Activated && AffectedPosition.X != -1 && !blockRemoved)
            {
                Block affectedBlock = tools.GetBlockAt(AffectedPosition);
                if (affectedBlock != null)
                {
                    Air air = new Air(game, AffectedPosition, tools);
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
        protected override void LoadContent()
        {
            texture = Textures.Detector;
            base.LoadContent();
        }
    }
}
