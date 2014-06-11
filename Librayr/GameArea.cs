using Arman_Class_Library;
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
    public enum Direction { up, down, left, right }
    public class GameArea : DrawableGameComponent
    {
        public static Vector2 StartingCoordinates = new Vector2(10, 10);

        private DataLoader dataLoader;
        private GameData data;

        private World gameComponents;
        private TexturesPaths paths;
        private string levelSourcePath;
        private SpriteBatch spriteBatch;
        private SpriteFont defaultFont;

        // Objectives
        private bool detectorsActivated = false;
        private bool coinsCollected = false;
        private bool playersHome = false;
        private bool won = false;

        public GameArea(Game game, string levelSource, TexturesPaths paths)
            : base(game)
        {
            levelSourcePath = levelSource;
            this.paths = paths;
        }
        public override void Initialize()
        {
            dataLoader = new DataLoader(levelSourcePath);

            data = dataLoader.ReadData(true);

            gameComponents = new World(Game, data);

            base.Initialize();
        }
        protected override void LoadContent()
        {
            Textures.Load(paths, gameComponents);
            spriteBatch = new SpriteBatch(GraphicsDevice);
            defaultFont = Game.Content.Load<SpriteFont>(@"Fonts/BigCourier");
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            IEnumerable<PlayerGComponent> players = gameComponents.GetGameComponents<PlayerGComponent>();
            if (data.Objectives.ActivateDetectors && !detectorsActivated)
            {
                int activatedDetectors = 0;
                IEnumerable<DetectorGComponent> detectors = gameComponents.GetGameComponents<DetectorGComponent>();
                foreach (DetectorGComponent detector in detectors)
                    if (detector.Model.IsPartOfObjectives && detector.Activated)
                        activatedDetectors++;
                if (activatedDetectors == detectors.Count())
                    detectorsActivated = true;
            }
            if (data.Objectives.CollectAllCoins && !coinsCollected)
            {
                if (gameComponents.GetGameComponents<CoinGComponent>().Count() == 0)
                    coinsCollected = true;
            }
            if (data.Objectives.GetHome && !playersHome)
            {
                int playersAtHome = 0;
                foreach (PlayerGComponent player in players)
                    if (player.IsHome)
                        playersAtHome++;
                if (playersAtHome == players.Count())
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

            foreach (PlayerGComponent player in players.ToList())
            {
                if (!player.Model.Alive)
                {
                    gameComponents.RemoveEntity(player);
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