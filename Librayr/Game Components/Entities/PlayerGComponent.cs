using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman_Class_Library
{
    public class PlayerGComponent : EntityGComponent
    {
        public bool IsHome { get; private set; }

        private int invulnerableAfterRespawnTimer;
        public new Player Model { get; private set; }
        private GameControls controls;
        public double Score { get; private set; }
        public PositionInGrid Spawnpoint { get; private set; }

        public PlayerGComponent(World tools, Player model)
            : base(tools, model)
        {
            Model = model;
            invulnerableAfterRespawnTimer = 0;
            Spawnpoint = model.Position;
            controls = GameControls.Parse(model.Controls.ToString());
        }
        protected override void LoadContent()
        {
            texture = Textures.Player;
            base.LoadContent();
        }
        public void Die(Mob killer)
        {
            if (!Model.Invulnerable && Model.Alive)
            {
                Model.Lives--;
                if (Model.Alive)
                {
                    Respawn();
                    invulnerableAfterRespawnTimer = 180;
                }
            }
        }
        public void PickupCoin(CoinGComponent coin)
        {
            Score += coin.Model.Value;
            World.RemoveBlock(coin);
        }
        public void Respawn()
        {
            Position = Spawnpoint;
        }
        public override void Update(GameTime gameTime)
        {
            if (invulnerableAfterRespawnTimer > 0)
            {
                invulnerableAfterRespawnTimer--;
                Model.Invulnerable = true;
            }
            if (invulnerableAfterRespawnTimer == 0)
                Model.Invulnerable = false;
            if (!IsMoving)
                ReactToControls(Keyboard.GetState());
            IEnumerable<CoinGComponent> coins = World.GetGameComponentsAt<CoinGComponent>(Model.Position).ToList();
            foreach (CoinGComponent coin in coins)
                PickupCoin(coin);
            IEnumerable<HomeGComponent> homes = World.GetGameComponentsAt<HomeGComponent>(Model.Position).ToList();
            if (homes.Count() > 0)
                IsHome = true;
            else
                IsHome = false;
            base.Update(gameTime);
        }
        public void ReactToControls(KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(controls.up))
            {
                this.Move(Direction.up);
            }
            else if (keyboardState.IsKeyDown(controls.down))
            {
                this.Move(Direction.down);
            }
            else if (keyboardState.IsKeyDown(controls.left))
            {
                this.Move(Direction.left);
            }
            else if (keyboardState.IsKeyDown(controls.right))
            {
                this.Move(Direction.right);
            }
        }
    }
}
