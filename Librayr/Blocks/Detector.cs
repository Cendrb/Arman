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

        public Detector(Game game, SpriteBatch spriteBatch, PositionInGrid position, Texture2D texture, Color lockColor, bool blockMovableBlockOnApproach, bool isPartOfObjectives)
            : base(game, spriteBatch, position, texture)
        {
            BlockMovableBlockOnApproach = blockMovableBlockOnApproach;
            LockColor = lockColor;
            IsPartOfObjectives = isPartOfObjectives;
            AffectedPosition = new PositionInGrid(-1);
        }
        public Detector(Game game, SpriteBatch spriteBatch, PositionInGrid position, Texture2D texture, Color lockColor, bool blockMovableBlockOnApproach, bool isPartOfObjectives, PositionInGrid affectedPosition)
            : base(game, spriteBatch, position, texture)
        {
            BlockMovableBlockOnApproach = blockMovableBlockOnApproach;
            LockColor = lockColor;
            IsPartOfObjectives = isPartOfObjectives;
            AffectedPosition = affectedPosition;
        }
        protected override void LoadContent()
        {
            base.LoadContent();
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
        public bool TryActivate(MovableBlock activator)
        {
            if (LockColor == Color.White || activator.KeyColor == Color.White)
            {
                activator.CanBePushed = BlockMovableBlockOnApproach;
                return true;
            }
            else if (LockColor == activator.KeyColor)
            {
                activator.CanBePushed = !BlockMovableBlockOnApproach;
                return true;
            }
            else
                return false;
        }
    }
}
