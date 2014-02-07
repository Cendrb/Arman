using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman_Class_Library
{
    public class Player : Entity
    {
        public Player(Game game, SpriteBatch spriteBatch, PositionInGrid position, Texture2D texture, bool canPush, bool canBePushed, Controls controls, Func<Direction, bool> move)
            : base(game, spriteBatch, position, texture, canPush, canBePushed, move)
        {

        }
        public void Die(Mob killer)
        {

        }
        public void PickupCoin(Coin coin)
        {

        }
    }
}
