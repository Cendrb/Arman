using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/*
namespace Arman_Class_Library.Game_Components.Entities
{
    public class PlayerGComponent : EntityGComponent
    {
        public bool IsHome { get; private set; }

        private int invulnerableAfterRespawnTimer;

        public Player(Game game, PositionInGrid position, GameDataTools tools, bool canPush, bool canBePushed, string name, float speed, Controls controls, int lives, bool isInvulnerable)
            : base(game, position, tools, canPush, canBePushed, name, speed)
        {
            IsInvulnerable = isInvulnerable;
            Score = 0;
            SpawnPoint = position;
            this.Controls = controls;
            Lives = lives;
            invulnerableAfterRespawnTimer = 0;
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
                    IsInvulnerable = true;
                    invulnerableAfterRespawnTimer = 180;
                }
            }
        }
        public void PickupCoin(Coin coin)
        {
            Score += coin.Value;
            tools.Data.Coins.Remove(coin);
            Game.Components.Remove(coin);
        }
        public void Respawn()
        {
            Position = SpawnPoint;
        }
        public override void Update(GameTime gameTime)
        {
            if (invulnerableAfterRespawnTimer > 0)
                invulnerableAfterRespawnTimer--;
            if (invulnerableAfterRespawnTimer == 0)
                IsInvulnerable = false;
            if (!IsMoving)
                ReactToControls(Keyboard.GetState());
            Coin coin = tools.GetCoinAt(Position);
            if (coin != null)
            {
                PickupCoin(coin);
            }
            Block block = tools.GetBlockAt(Position);
            if (block is Home)
                IsHome = true;
            else
                IsHome = false;
            base.Update(gameTime);
        }
        protected override void LoadContent()
        {
            texture = Textures.Player;
            base.LoadContent();
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
*/