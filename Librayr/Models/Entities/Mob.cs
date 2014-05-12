using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman_Class_Library
{
    public class Mob : Entity
    {
        public int Vision { get; private set; }
        public int MoveRatio { get; private set; }

        private float movePause;

        public Mob(Game game, PositionInGrid position, GameDataTools tools, bool canPush, bool canBePushed, string name, float speed, int vision, int moveRatio)
            : base(game, position, tools, canPush, canBePushed, name, speed)
        {
            Vision = vision;
            MoveRatio = moveRatio;
            movePause = 0;
        }
        public override void Update(GameTime gameTime)
        {
            Entity entity = tools.GetEntityAt(Position);
            if (entity is Player)
            {
                (entity as Player).Die(this);
            }
            else
            {
                if (!IsMoving)
                {
                    if (!tryToCatchPlayer())
                        movePause += GameArea.OneBlockSize / (GameArea.TimeForMove - Speed);
                    if (movePause > GameArea.OneBlockSize)
                    {
                        movePause = 0;
                        wander();
                    }
                }
            }

            base.Update(gameTime);
        }
        protected override void LoadContent()
        {
            texture = Textures.Mob;
            base.LoadContent();
        }
        private bool tryToCatchPlayer()
        {
            for (int x = Vision; x > 0; x--)
            {
                if (tools.GetEntityAt(Position.ApplyDirection(Direction.up, x)) is Player)
                {
                    Move(Direction.up);
                    return true;
                }
                if (tools.GetEntityAt(Position.ApplyDirection(Direction.down, x)) is Player)
                {
                    Move(Direction.down);
                    return true;
                }
                if (tools.GetEntityAt(Position.ApplyDirection(Direction.left, x)) is Player)
                {
                    Move(Direction.left);
                    return true;
                }
                if (tools.GetEntityAt(Position.ApplyDirection(Direction.right, x)) is Player)
                {
                    Move(Direction.right);
                    return true;
                }
            }
            return false;
        }
        private void wander()
        {
            if (!tryToCatchPlayer())
            {
                if (MoveRatio > 0)
                    switch (GameArea.mobAIRandom.Next(0, MoveRatio * 4))
                    {
                        case 0:
                            if (this.Move(Direction.left))
                                break;
                            else
                                return;
                        case 1:
                            if (this.Move(Direction.up))
                                break;
                            else
                                return;
                        case 2:
                            if (this.Move(Direction.right))
                                break;
                            else
                                return;
                        case 3:
                            if (this.Move(Direction.down))
                                break;
                            else
                                return;
                    }
            }
        }
    }
}
