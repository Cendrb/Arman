using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman
{
    public class MovableObject
    {
        protected Arman game;
        public Texture2D Texture { get; private set; }
        public PositionInGrid PositionInGrid;
        protected Block[,] gameArray;
        public bool isMoving;
        protected float movingDifference;
        protected Direction movingDirection;
        protected int oneBlockSize;
        protected int timeForMove;
        protected List<MovableObject> movableObjects;
        public bool Blocked { get; set; }

        public MovableObject(Arman game, PositionInGrid positionInGrid, Texture2D texture, Block[,] gameArray, int oneBlockSize, int timeForMove, List<MovableObject> movableObjects)
        {
            Blocked = false;
            this.game = game;
            PositionInGrid = positionInGrid;
            this.Texture = texture;
            this.gameArray = gameArray;
            isMoving = false;
            this.timeForMove = timeForMove;
            this.oneBlockSize = oneBlockSize;
            this.movableObjects = movableObjects;
            movingDifference = 0.0F;
            movingDirection = Direction.up;
        }
        public virtual bool Move (Direction direction)
        {
            switch(direction)
            {
                case global::Arman.Direction.up:
                    if (TryMove(new PositionInGrid(PositionInGrid.X, PositionInGrid.Y - 1)))
                    {
                        isMoving = true;
                        movingDirection = global::Arman.Direction.up;

                        PositionInGrid.Y--;
                        return true;
                    }
                    break;

                case global::Arman.Direction.down:
                    if (TryMove(new PositionInGrid(PositionInGrid.X, PositionInGrid.Y + 1)))
                    {
                        isMoving = true;
                        movingDirection = global::Arman.Direction.down;

                         PositionInGrid.Y++;
                        return true;
                    }
                    break;

                case global::Arman.Direction.left:
                    if (TryMove(new PositionInGrid(PositionInGrid.X - 1, PositionInGrid.Y)))
                    {
                        isMoving = true;
                        movingDirection = global::Arman.Direction.left;

                        PositionInGrid.X--;
                        return true;
                    }
                    break;

                case global::Arman.Direction.right:
                    if (TryMove(new PositionInGrid(PositionInGrid.X + 1, PositionInGrid.Y)))
                    {
                        isMoving = true;
                        movingDirection = global::Arman.Direction.right;

                        PositionInGrid.X++;
                        return true;
                    }
                    break;
            }
            return false;
        }
        public virtual bool TryMove(PositionInGrid position)
        {
            if (isMoving)
                return false;
            if (Blocked)
                return false;
            if (position.X >= gameArray.GetLength(0) || position.Y >= gameArray.GetLength(1) || position.X < 0 || position.Y < 0)
                return false;
            else if ((gameArray[position.X, position.Y] as Block).Type == BlockType.solid)
                return false;
            else
                return true;
        }
        public virtual Vector2 GetRelativeCoordinates()
        {
            if (isMoving)
            {
                Vector2 coord = new Vector2();
                movingDifference += (float)oneBlockSize/(float)timeForMove;
                //odčítaní, je zde proto, aby vyrovnalo již provedený posun metodou Move()
                switch(movingDirection)
                {
                    case Direction.up:
                        coord.X = PositionInGrid.X * oneBlockSize;
                        coord.Y = (PositionInGrid.Y + 1) * oneBlockSize - movingDifference;
                        break;
                    case Direction.down:
                        coord.X = PositionInGrid.X * oneBlockSize;
                        coord.Y = (PositionInGrid.Y - 1) * oneBlockSize + movingDifference;
                        break;
                    case Direction.left:
                        coord.X = (PositionInGrid.X + 1) * oneBlockSize - movingDifference;
                        coord.Y = PositionInGrid.Y * oneBlockSize;
                        break;
                    case Direction.right:
                        coord.X = (PositionInGrid.X - 1) * oneBlockSize + movingDifference;
                        coord.Y = PositionInGrid.Y * oneBlockSize;
                        break;
                }
                return coord;
            }
            else
                return new Vector2(PositionInGrid.X * oneBlockSize, PositionInGrid.Y * oneBlockSize);
        }
        public virtual void Update(GameTime gameTime)
        {
            if (movingDifference >= oneBlockSize)
            {
                isMoving = false;
                movingDifference = 0;
            }

        }
    }
}
