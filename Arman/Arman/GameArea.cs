using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Arman_Class_Library;


namespace Arman
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class GameArea : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private Arman game;

        private Texture2D area, wall;

        private int gameSpeed; // higher = slower

        private Block[,] gameArray;

        public static List<Entity> entities;
        public List<GameTarget> gameTargets;
        private int blockSize;
        private int detectorsForWin;
        private bool won;

        public GameArea(Arman game)
            : base(game)
        {
            this.game = game;

            gameSpeed = 10;
            blockSize = 20;
            entities = new List<Entity>();
            won = false;
            gameTargets = new List<GameTarget>();
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            gameTargets.Add(GameTarget.collectAllCoins);
            gameTargets.Add(GameTarget.placeBlocksOnDetectors);

            gameArray = new Block[900 / blockSize, 500 / blockSize];

            for (int x = 0; x != gameArray.GetLength(0); x++)
                for (int y = 0; y != gameArray.GetLength(1); y++)
                {
                    CreateBlock(BlockType.nonsolid, new PositionInGrid(x, y));
                }
            
            detectorsForWin = getDetectors();

            //temporary - template
            GameAreaTemplates.DrawPacman(this);

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            if (hasWon())
                won = true;
            IEnumerable<Player> players = from entity in entities
                                          where entity is Player
                                          select (Player)entity;
            foreach (Player player in players)
            {
                player.ReactToControls();
            }
            foreach (Entity mo in entities)
            {
                mo.Update(gameTime);
            }
            foreach (DrawableGameComponent comp in gameArray)
            {
                comp.Update(gameTime);
            }
            base.Update(gameTime);
        }

        private void Won()
        {
            game.spriteBatch.DrawStringWithShadow(game.fontCourierNewBig, "Vyhráli jste!", new Vector2(450, 400), Color.Orange);
        }
        protected override void LoadContent()
        {
            area = game.Content.Load<Texture2D>(@"Sprites/plocha");
            wall = game.Content.Load<Texture2D>(@"Sprites/Blocks/wall");

            base.LoadContent();
        }
        public override void Draw(GameTime gameTime)
        {
            game.spriteBatch.Begin();

            game.spriteBatch.Draw(area, new Vector2(460, 240), Color.White);

            foreach(DrawableGameComponent comp in gameArray)
            {
                comp.Draw(gameTime);
            }

            foreach (Entity entity in entities)
            {
                convertRelativeCoordinatesToAbsoluteAndDraw(entity);
                entity.DrawInfo(game.spriteBatch);
            }
            if (won)
                Won();

            viewPointsForWon();

            game.spriteBatch.End();

            base.Draw(gameTime);
        }
        private void viewPointsForWon()
        {
            if (gameTargets.Contains(GameTarget.placeBlocksOnDetectors))
            {
                game.spriteBatch.DrawStringWithShadow(game.fontCourierNew, "Zbývající poèet bodù: " + detectorsForWin, new Vector2(160, 100), Color.OrangeRed);
            }
            if (gameTargets.Contains(GameTarget.collectAllCoins))
            {
                game.spriteBatch.DrawStringWithShadow(game.fontCourierNew, "Zbývající poèet mincí: " + remainingCoins(), new Vector2(160, 150), Color.OrangeRed);
            }
            if (gameTargets.Contains(GameTarget.getHome))
            {
                game.spriteBatch.DrawStringWithShadow(game.fontCourierNew, "Zdrhni do baráku!" , new Vector2(160, 200), Color.OrangeRed);
            }
        }
        private void convertRelativeCoordinatesToAbsoluteAndDraw(Entity entity)
        {
            Vector2 coord = entity.GetRelativeCoordinates();
            coord.X += 510;
            coord.Y += 290;
            game.spriteBatch.Draw(entity.Texture, coord, Color.White);
        }
        private Block loadAndInitializeBlock(Block block)
        {
            block.Initialize();
            block.LoadContent();
            return block;
        }
        public void CreateBlock(BlockType type, PositionInGrid position)
        {
            if (type == BlockType.movable)
                entities.Add(new MovableBlock(game, position, game.Content.Load<Texture2D>(@"Sprites/Blocks/movable"), gameArray, blockSize, gameSpeed, entities));
            else if (type == BlockType.detector)
                gameArray[position.X, position.Y] = loadAndInitializeBlock(new Detector(game, BlockType.detector, position, blockSize, detectorActivated));
            else
                gameArray[position.X, position.Y] = loadAndInitializeBlock(new Block(game, type, position, blockSize));
        }
        public void SpawnPlayer(PositionInGrid position, Controls controls)
        {
            entities.Add(new Player(game, position, game.Content.Load<Texture2D>(@"Sprites/player"), gameArray, blockSize, gameSpeed, entities, this, controls));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="range"></param>
        /// <param name="speed"></param>
        /// <param name="movingFrequency">higher = smaller chance - number for random generator of direction</param>
        public void SpawnMob(PositionInGrid position, int range, int speed, int movingFrequency)
        {
            int movingFreq = movingFrequency + 4; //zamezení aby potvora nechodila poøád do jednoho smìru pøi zadání nízké hodnoty
            entities.Add(new Mob(game, position, game.Content.Load<Texture2D>(@"Sprites/potvora"), gameArray, blockSize, gameSpeed, entities, range, speed, movingFreq));
        }
        private void detectorActivated()
        {
            detectorsForWin--;
        }
        private int remainingCoins()
        {
            int coinsRemaining = 0;
                foreach (Block block in gameArray)
                {
                    if (block.Type == BlockType.coin)
                        coinsRemaining++;
                }
            return coinsRemaining;
        }
        private int getDetectors()
        {
            int detectorsRemaining = 0;
            foreach (Block block in gameArray)
            {
                if (block.Type == BlockType.detector)
                    detectorsRemaining++;
            }
            return detectorsRemaining;
        }
        private bool hasWon()
        {
            int result = 0;
            if (gameTargets.Contains(GameTarget.placeBlocksOnDetectors))
            {
                if (detectorsForWin == 0)
                    result++;
            }
            if (gameTargets.Contains(GameTarget.collectAllCoins))
            {
                if (remainingCoins() == 0)
                    result++;
            }
            if (gameTargets.Contains(GameTarget.getHome))
            {
                result++;
            }
            return result == gameTargets.Count;
        }
    }
}
