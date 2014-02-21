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

        private Action<Mob> wander;
        private Func<Mob, bool> tryToCatchPlayer;

        public Mob(Game game, SpriteBatch spriteBatch, PositionInGrid position, Texture2D texture, bool canPush, bool canBePushed, string name, Func<Direction, List<Entity>, bool> move, float speed, int vision, int moveRatio)
            : base(game, spriteBatch, position, texture, canPush, canBePushed, name, move, speed)
        {
            Vision = vision;
            MoveRatio = moveRatio;
            this.wander = wander;
            movePause = 0;
            this.tryToCatchPlayer = tryToCatchPlayer;
        }
        public override void Update(GameTime gameTime)
        {
            if (!IsMoving)
            {
                if (!tryToCatchPlayer(this))
                    movePause += GameArea.OneBlockSize / (GameArea.TimeForMove - Speed);
                    if (movePause > GameArea.OneBlockSize)
                    {
                        movePause = 0;
                        wander(this);
                    }
            }

            base.Update(gameTime);
        }
    }
}
