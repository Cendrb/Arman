using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman_Class_Library
{
    [Serializable]
    public abstract class Entity : GameElement
    {
        public bool CanPush { get; private set; }
        public bool CanBePushed { get; set; }
        public string Name { get; private set; }
        public bool IsMoving { get; private set; }
        public float Speed { get; private set; }
        public bool IsBeingPushed { get; private set; }

        private float speedBeforePush;
        // Moving mechanism
        private Direction movingDirection;
        private float movingDifference;

        /// <summary>
        /// Creates new Entity instance
        /// </summary>
        /// <param name="game">Main game component</param>
        /// <param name="spriteBatch">Specifies where to draw textures</param>
        /// <param name="position">Specifies position in game grid</param>
        /// <param name="canPush">Defines if that entity can push others</param>
        /// <param name="canBePushed">Defines if that entity can be pushed by others</param>
        /// <param name="move">Super-action to solve move dependencies - returns result</param>
        /// <param name="name">Name of entity</param>
        public Entity(Game game, PositionInGrid position, GameDataTools tools, bool canPush, bool canBePushed, string name, float speed)
            : base(game, position, tools)
        {
            movingDifference = 0.0F;
            Name = name;
            Speed = speed;
            CanPush = canPush;
            CanBePushed = canBePushed;
            Position = position;

            IsBeingPushed = false;
        }
        public override void Update(GameTime gameTime)
        {
            if (IsMoving)
                movingDifference += (float)GameArea.OneBlockSize / (GameArea.TimeForMove - Speed);
            if (movingDifference >= GameArea.OneBlockSize)
            {
                movingDifference = 0;
                IsMoving = false;
                Position = Position.ApplyDirection(movingDirection);
                if (IsBeingPushed)
                {
                    Speed = speedBeforePush;
                    IsBeingPushed = false;
                }
            }
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            Vector2 position = getAbsoluteCoordinates();
            spriteBatch.Begin();
            spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, GameArea.OneBlockSize, GameArea.OneBlockSize), Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
        public bool Move(Direction direction)
        {
            List<Entity> senders = new List<Entity>();
            senders.Add(this);
            if (solveColisions(direction, senders))
            {
                move(direction);
                return true;
            }
            else
                return false;
        }
        public bool Move(Direction direction, List<Entity> senders)
        {
            // = is being pushed by another entity
            speedBeforePush = Speed;
            this.Speed = senders.Last().Speed;
            IsBeingPushed = true;
            senders.Add(this);
            if (solveColisions(direction, senders))
            {
                move(direction);
                return true;
            }
            else
                return false;
        }
        private void move(Direction direction)
        {
            IsMoving = true;
            movingDirection = direction;
        }
        private bool solveColisions(Direction direction, List<Entity> senders)
        {
            // Prepare local variables
            Entity originalSender = senders.Last();
            PositionInGrid target = originalSender.Position.ApplyDirection(direction);

            // Most important IF :)
            if (originalSender.IsMoving)
                return false;

            Block targetBlock = tools.GetBlockAt(target);
            Entity targetEntity = tools.GetEntityAt(target);

            #region Colision with static blocks
            if (targetBlock is Solid)
                return false;
            #endregion

            #region Colision with entities

                // Basic pushing
            if (targetEntity != null)
            {
                if ((targetEntity is Player && originalSender is Mob) || (targetEntity is Mob && originalSender is Player)) // KILLING!
                    return true;
                else if (targetEntity.CanBePushed && originalSender.CanPush)
                {
                    if (!targetEntity.Move(direction, senders))
                        return false;
                }
                else
                {
                    if (!originalSender.CanPush || !targetEntity.CanBePushed)
                        return false;
                }
            }
            #endregion

            #region Out of area colisions
            if (target.X < 0 || target.Y < 0)
                return false;
            if (target.X >= tools.Data.XGameArea || target.Y >= tools.Data.YGameArea)
                return false;
            #endregion
            return true;
        }
        protected Vector2 getAbsoluteCoordinates()
        {
            Vector2 relative = getRelativeCoordinates();
            return new Vector2(relative.X + GameArea.StartingCoordinates.X, relative.Y + GameArea.StartingCoordinates.Y);
        }
        protected Vector2 getRelativeCoordinates()
        {
            if (IsMoving)
            {
                Vector2 coord = new Vector2();
                switch (movingDirection)
                {
                    case Direction.up:
                        coord.X = Position.X * GameArea.OneBlockSize;
                        coord.Y = Position.Y * GameArea.OneBlockSize - movingDifference;
                        break;
                    case Direction.down:
                        coord.X = Position.X * GameArea.OneBlockSize;
                        coord.Y = Position.Y * GameArea.OneBlockSize + movingDifference;
                        break;
                    case Direction.left:
                        coord.X = Position.X * GameArea.OneBlockSize - movingDifference;
                        coord.Y = Position.Y * GameArea.OneBlockSize;
                        break;
                    case Direction.right:
                        coord.X = Position.X * GameArea.OneBlockSize + movingDifference;
                        coord.Y = Position.Y * GameArea.OneBlockSize;
                        break;
                }
                return coord;
            }
            else
                return new Vector2(Position.X * GameArea.OneBlockSize, Position.Y * GameArea.OneBlockSize);
        }
    }
}
