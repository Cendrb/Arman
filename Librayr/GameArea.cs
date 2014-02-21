using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        public static int OneBlockSize;

        // speed
        public static float TimeForMove
        {
            get
            {
                return (float)OneBlockSize - speed;
            }
            private set
            {

            }
        }
        private static float speed;
        public static float Speed
        {
            get
            {
                return speed;
            }
            set
            {
                if (value > OneBlockSize)
                    throw new ArgumentException("Speed must be smaller than size of one block in pixels!");
                else
                    speed = value;
            }
        }

        public static Vector2 StartingCoordinates = new Vector2(10, 10);

        public List<Block> staticBlocks = new List<Block>();
        public List<Coin> coins = new List<Coin>();
        public List<Entity> entities = new List<Entity>();

        private DataLoader dataLoader;
        private DataForLoader dataForLoader;
        private string levelSourcePath;
        private Random mobAIRandom;

        public GameArea(GameWithSpriteBatch game, string levelSource, DataForLoader data)
            : base(game)
        {
            dataForLoader = data;
            levelSourcePath = levelSource;
        }
        public override void Initialize()
        {
            mobAIRandom = new Random();

            dataForLoader.Move = Move;
            dataLoader = new DataLoader(levelSourcePath, Game, (Game as GameWithSpriteBatch).SpriteBatch, dataForLoader);

            GameData data = dataLoader.ReadData(true);
            staticBlocks = data.Blocks;
            entities = data.Entities;
            coins = data.Coins;
            Speed = data.Speed;
            OneBlockSize = data.OneBlockSize;

            foreach (Block block in staticBlocks)
                base.Game.Components.Add(block);

            foreach (Entity entity in entities)
                base.Game.Components.Add(entity);

            foreach (Coin coin in coins)
                base.Game.Components.Add(coin);

            base.Initialize();
        }
        public override void Update(GameTime gameTime)
        {
            Entity eToRemove = null;
            foreach(Entity entity in entities)
            {
                if (entity is Player && (entity as Player).IsDead)
                {
                    eToRemove = entity;
                }
            }
            if (eToRemove != null)
            {
                entities.Remove(eToRemove);
                Game.Components.Remove(eToRemove);
            }
            base.Update(gameTime);
        }
        public bool Move(Direction direction, List<Entity> senders)
        {
            // Prepare local variables
            Entity originalSender = senders.Last();
            PositionInGrid target = originalSender.Position.ApplyDirection(direction);

            // Most important IF :)
            if (originalSender.IsMoving)
                return false;

            #region Colision with static blocks
            foreach (Block block in staticBlocks)
            {
                if (target == block.Position && block is Solid)
                    return false;
                if (target == block.Position && block is Detector && originalSender is MovableBlock)
                    (block as Detector).TryActivate(originalSender as MovableBlock);
            }
            #endregion

            #region Colision with entities
            foreach (Entity entity in entities)
            {
                // Killing players
                if (target == entity.Position && originalSender is Player && entity is Mob)
                {
                    (originalSender as Player).Die(entity as Mob);
                    break;
                }
                if (target == entity.Position && entity is Player && originalSender is Mob)
                {
                    (entity as Player).Die(originalSender as Mob);
                    break;
                }
                // Basic pushing
                if (target == entity.Position && entity.CanBePushed && originalSender.CanPush)
                {
                    if (!entity.Move(direction, senders))
                        return false;
                }
                else
                {
                    if (target == entity.Position && (!originalSender.CanPush || !entity.CanBePushed))
                        return false;
                }
            }
            #endregion

            #region Colision with coins
            Coin coinToRemove = null;
            foreach (Coin coin in coins)
                if (coin.Position == target && originalSender is Player)
                {
                    (originalSender as Player).PickupCoin(coin);
                    coinToRemove = coin;
                }
            if (coinToRemove != null)
            {
                coins.Remove(coinToRemove);
                Game.Components.Remove(coinToRemove);
            }
            #endregion

            return true;
        }
    }
}
