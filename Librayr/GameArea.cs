using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Arman_Class_Library
{
    public enum TryMoveResult { free, blockedByMovableEntity, blocked}
    public enum Direction { up, down, left, right }

    public class GameArea : DrawableGameComponent
    {
        public List<Block> staticBlocks = new List<Block>();
        public List<Coin> coins = new List<Coin>();
        public List<Entity> entities = new List<Entity>();

        public GameArea(Game game, XmlDocument levelSource)
            : base(game)
        {

        }
        public bool Move(Direction direction, Entity sender)
        {
            // Prepare local variable
            PositionInGrid target = sender.Position.ApplyDirection(direction);

            #region Colision with static blocks
            foreach (Block block in staticBlocks)
                if (target == block.Position && block is Solid)
                    return false;
            #endregion

            #region Colision with entities
            foreach (Entity entity in entities)
            {
                // Killing players
                if (target == entity.Position && sender is Player && entity is Mob)
                    (sender as Player).Die(entity as Mob);

                // Basic pushing
                if (target == entity.Position && entity.CanBePushed && sender.CanPush && !entity.Blocked)
                    if (!entity.Move(direction))
                        return false;
            }
            #endregion

            #region Colision with coins
            foreach (Coin coin in coins)
                if (coin.Position == target && sender is Player)
                    (sender as Player).PickupCoin(coin);
            #endregion

            return true;
        }
    }
}
