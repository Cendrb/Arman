﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arman_Class_Library;

namespace Arman
{
    public class MovableBlock : Entity
    {
        public MovableBlock(Arman game, PositionInGrid positionInGrid, Texture2D texture, Block[,] gameArray, int oneBlockSize, int timeForMove, List<Entity> movableObjects)
            : base(game, positionInGrid, texture, gameArray, oneBlockSize, timeForMove, movableObjects)
        {
        }
        public override bool Move(Direction direction)
        {
            IEnumerable<PositionInGrid> positions = from mObject in movableObjects
                                                    select mObject.PositionInGrid;
            bool stop = false;
            switch (direction)
            {
                case global::Arman.Direction.up:
                    stop = positions.Contains(new PositionInGrid(PositionInGrid.X, PositionInGrid.Y - 1));
                    break;

                case global::Arman.Direction.down:
                    stop = positions.Contains(new PositionInGrid(PositionInGrid.X, PositionInGrid.Y + 1));
                    break;

                case global::Arman.Direction.left:
                    stop = positions.Contains(new PositionInGrid(PositionInGrid.X - 1, PositionInGrid.Y));
                    break;

                case global::Arman.Direction.right:
                    stop = positions.Contains(new PositionInGrid(PositionInGrid.X + 1, PositionInGrid.Y));
                    break;
            }
            if (stop)
                return false;
            else
                return base.Move(direction);
        }
    }
}
