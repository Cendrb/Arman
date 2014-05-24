using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman_Class_Library
{
    public class EntityGComponent : GameComponent
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

        public new Entity Model { get; private set; }

        public EntityGComponent(GameComponents tools, Entity entity)
            : base(tools, entity)
        {
            this.DrawOrder = 69;
            movingDifference = 0.0F;
            IsBeingPushed = false;
            Model = entity;
        }
        public override void Update(GameTime gameTime)
        {
            if (IsMoving)
                movingDifference += (float)tools.Data.OneBlockSize / (tools.TimeForMove - Speed);
                if (movingDifference >= tools.Data.OneBlockSize)
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
        public bool Move(Direction direction)
        {
            List<EntityGComponent> senders = new List<EntityGComponent>();
            senders.Add(this);
            if (solveColisions(direction, senders))
            {
                move(direction);
                return true;
            }
            else
                return false;
        }
        public bool Move(Direction direction, List<EntityGComponent> senders)
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
        private bool solveColisions(Direction direction, List<EntityGComponent> senders)
        {
            // Prepare local variables
            EntityGComponent originalSender = senders.Last();
            PositionInGrid target = originalSender.Position.ApplyDirection(direction);

            // Most important IF :)
            if (originalSender.IsMoving)
                return false;

            IEnumerable<GameComponent> targetComponents = tools.GetGameComponentsAt<GameComponent>(target);

            foreach (GameComponent component in targetComponents)
            {
                if (component.Model.Collides)
                {
                    if (component is EntityGComponent)
                    {
                        if ((component is PlayerGComponent && originalSender is MobGComponent) || (component is MobGComponent && originalSender is PlayerGComponent)) // KILLING!
                            return true;
                        EntityGComponent e = component as EntityGComponent;
                        if (e.Model.CanBePushed && this.CanPush)
                        {
                            return e.Move(direction, senders);
                        }
                        else
                        {
                            return false;
                        }
                    }
                    return false;
                }
            }
            #region Out of area colisions
            if (target.X < 0 || target.Y < 0)
                return false;
            if (target.X >= tools.Data.XGameArea || target.Y >= tools.Data.YGameArea)
                return false;
            #endregion
            return true;
        }
        public override Vector2 GetAbsoluteCoordinates()
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
                        coord.X = Position.X * tools.Data.OneBlockSize;
                        coord.Y = Position.Y * tools.Data.OneBlockSize - movingDifference;
                        break;
                    case Direction.down:
                        coord.X = Position.X * tools.Data.OneBlockSize;
                        coord.Y = Position.Y * tools.Data.OneBlockSize + movingDifference;
                        break;
                    case Direction.left:
                        coord.X = Position.X * tools.Data.OneBlockSize - movingDifference;
                        coord.Y = Position.Y * tools.Data.OneBlockSize;
                        break;
                    case Direction.right:
                        coord.X = Position.X * tools.Data.OneBlockSize + movingDifference;
                        coord.Y = Position.Y * tools.Data.OneBlockSize;
                        break;
                }
                return coord;
            }
            else
                return new Vector2(Position.X * tools.Data.OneBlockSize, Position.Y * tools.Data.OneBlockSize);
        }
    }
}
