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
    public enum TryMoveResult { free, blockedByMovableEntity, blocked }
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
        private TexturesPaths dataForLoader;
        private GameData data;
        private GameDataTools tools;
        private string levelSourcePath;
        public static Random mobAIRandom;
        private SpriteBatch spriteBatch;
        private SpriteFont defaultFont;

        // Objectives
        private bool detectorsActivated = false;
        private bool coinsCollected = false;
        private bool playersHome = false;
        private bool won = false;

        public GameArea(Game game, string levelSource, TexturesPaths dataForLoader)
            : base(game)
        {
            this.dataForLoader = dataForLoader;
            levelSourcePath = levelSource;
        }
        public override void Initialize()
        {
            mobAIRandom = new Random();

            tools = new GameDataTools();
            dataLoader = new DataLoader(levelSourcePath, Game, dataForLoader, tools);

            data = dataLoader.ReadData(true);
            tools.Data = data;
            tools.Game = Game;
            tools.Initialize();

            staticBlocks = data.Blocks;
            entities = data.Entities;
            coins = data.Coins;
            OneBlockSize = data.OneBlockSize;
            Speed = data.Speed;

            foreach (Block block in staticBlocks)
                Game.Components.Add(block);

            foreach (Coin coin in coins)
                Game.Components.Add(coin);

            foreach (Entity entity in entities)
                Game.Components.Add(entity);

            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            defaultFont = Game.Content.Load<SpriteFont>(@"Fonts/BigCourier");
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            if (data.Objectives.ActivateDetectors && !detectorsActivated)
            {
                int activatedDetectors = 0;
                foreach (Detector detector in tools.Detectors)
                    if (detector.IsPartOfObjectives && detector.Activated)
                        activatedDetectors++;
                if (activatedDetectors == tools.Detectors.Count)
                    detectorsActivated = true;
            }
            if (data.Objectives.CollectAllCoins && !coinsCollected)
            {
                if (data.Coins.Count == 0)
                    coinsCollected = true;
            }
            if (data.Objectives.GetHome && !playersHome)
            {
                int playersAtHome = 0;
                foreach (Player player in tools.Players)
                    if (player.IsHome)
                        playersAtHome++;
                if (playersAtHome == tools.Detectors.Count)
                    playersHome = true;
            }
            int objectivesCompleted = 0;
            if (detectorsActivated)
                objectivesCompleted++;
            if (playersHome)
                objectivesCompleted++;
            if (coinsCollected)
                objectivesCompleted++;
            if (objectivesCompleted == data.Objectives.Count)
                won = true;

            foreach (Entity entity in entities.ToList())
            {
                if (entity is Player && (entity as Player).IsDead)
                {
                    entities.Remove(entity);
                    Game.Components.Remove(entity);
                }
            }
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            if (won)
                Won();
        }
        private void Won()
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(defaultFont, "You WON!!!", new Vector2(600, 600), Color.Red);
            spriteBatch.End();
        }
    }
}
