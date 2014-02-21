using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman_Class_Library
{
    public class Player : Entity
    {
        public int Score { get; private set; }
        public int Lives { get; private set; }
        public PositionInGrid SpawnPoint { get; private set; }
        public bool IsDead { get; private set; }
        public Controls Controls { get; private set; }
        public bool IsInvulnerable { get; private set; }

        public Player(Game game, SpriteBatch spriteBatch, PositionInGrid position, Texture2D texture, bool canPush, bool canBePushed, string name, Func<Direction, List<Entity>, bool> move, float speed, Controls controls, int lives, bool isInvulnerable)
            : base(game, spriteBatch, position, texture, canPush, canBePushed, name, move, speed)
        {
            IsInvulnerable = isInvulnerable;
            Score = 0;
            SpawnPoint = position;
            this.Controls = controls;
            Lives = lives;
        }
        public void Die(Mob killer)
        {
            if (!IsInvulnerable)
            {
                Lives--;
                if (Lives <= 0)
                {
                    IsDead = true;
                }
                else
                {
                    Respawn();
                }
            }
        }
        public void PickupCoin(Coin coin)
        {
            Score += coin.Value;
        }
        public void Respawn()
        {
            Position = SpawnPoint;
        }
        public override void Update(GameTime gameTime)
        {
            if (!IsMoving)
                ReactToControls(Keyboard.GetState());
            base.Update(gameTime);
        }
        public void ReactToControls(KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Controls.up))
            {
                this.Move(Direction.up);
            }
            else if (keyboardState.IsKeyDown(Controls.down))
            {
                this.Move(Direction.down);
            }
            else if (keyboardState.IsKeyDown(Controls.left))
            {
                this.Move(Direction.left);
            }
            else if (keyboardState.IsKeyDown(Controls.right))
            {
                this.Move(Direction.right);
            }
        }
    }
}
