using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman
{
    class Detector : Block
    {
        private DetectorActivated detectorActivated;
        private bool alreadyActivated;
        public Detector(Arman game, BlockType type, PositionInGrid position, int oneBlockSize, DetectorActivated detectorActivated)
            : base(game, type, position, oneBlockSize)
        {
            this.detectorActivated = detectorActivated;
            alreadyActivated = false;
        }
        public override void Update(GameTime gameTime)
        {
            IEnumerable<MovableObject> choosenBlocks = from block in GameArea.movableObjects
                                                       where block is MovableBlock
                                                       where block.PositionInGrid.X == Position.X
                                                       where block.PositionInGrid.Y == Position.Y
                                                       select block;
            if (choosenBlocks.Count() > 0 && !alreadyActivated)
            {
                detectorActivated();
                alreadyActivated = true;
                ((MovableBlock)GameArea.movableObjects[GameArea.movableObjects.IndexOf(choosenBlocks.First())]).Blocked = true;
            }
            base.Update(gameTime);
        }
    }
}
