using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Arman
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class GameArea : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private Arman game;

        private Texture2D area, wall;

        private int keyPressTimeLimit;
        private int gameSpeed; // higher = slower

        private Block[,] gameArray;

        private List<Potvora> potvory;
        public static List<MovableObject> movableObjects;
        public List<GameTarget> gameTargets;
        private KeyboardState keyboardState;
        private List<Player> players;
        private int blockSize;
        private int detectorsForWin;
        private bool won;

        public GameArea(Arman game)
            : base(game)
        {
            this.game = game;

            keyPressTimeLimit = 0;
            gameSpeed = 8;
            blockSize = 20;
            movableObjects = new List<MovableObject>();
            won = false;
            gameTargets = new List<GameTarget>();
            potvory = new List<Potvora>();
            players = new List<Player>();
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
            players.Add(new Player(game, new PositionInGrid(0), game.Content.Load<Texture2D>(@"Sprites/player"), gameArray, blockSize, gameSpeed, movableObjects, this));

            detectorsForWin = getDetectors();

            potvory.Add(new Potvora(game, new PositionInGrid(10), game.Content.Load<Texture2D>(@"Sprites/potvora"), gameArray, blockSize, gameSpeed + 5, movableObjects, players));

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            moveControl();
            if (hasWon())
                won = true;

            foreach (DrawableGameComponent comp in gameArray)
            {
                comp.Update(gameTime);
            }
            foreach(MovableObject mo in movableObjects)
            {
                mo.Update(gameTime);
            }
            foreach(Player player in players)
            {
                player.Update(gameTime);
            }
            foreach (Potvora potvora in potvory)
            {
                potvora.Update(gameTime);
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

            foreach (MovableBlock block in movableObjects)
            {
                convertRelativeCoordinatesToAbsoluteAndDraw(block);
            }
            foreach (Player player in players)
            {
                convertRelativeCoordinatesToAbsoluteAndDraw(player);
            }

            foreach (Potvora potvora in potvory)
                convertRelativeCoordinatesToAbsoluteAndDraw(potvora);

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
        private void convertRelativeCoordinatesToAbsoluteAndDraw(MovableObject mObject)
        {
            Vector2 coord = mObject.GetRelativeCoordinates();
            coord.X += 510;
            coord.Y += 290;
            game.spriteBatch.Draw(mObject.Texture, coord, Color.White);
        }
        private void moveControl()
        {
            Player player = players.First();

            keyPressTimeLimit++;

            if(keyboardState.IsKeyDown(Keys.Up))
            {
                if(player.Move(Direction.up))
                    keyPressTimeLimit = 0;
            }
            else if (keyboardState.IsKeyDown(Keys.Down))
            {
                if (player.Move(Direction.down))
                    keyPressTimeLimit = 0;
            }
            else if (keyboardState.IsKeyDown(Keys.Left))
            {
                if (player.Move(Direction.left))
                    keyPressTimeLimit = 0;
            }
            else if (keyboardState.IsKeyDown(Keys.Right))
            {
                if (player.Move(Direction.right))
                    keyPressTimeLimit = 0;
            }

            if (keyPressTimeLimit > 1000)
                keyPressTimeLimit = 50;
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
                movableObjects.Add(new MovableBlock(game, position, game.Content.Load<Texture2D>(@"Sprites/Blocks/movable"), gameArray, blockSize, gameSpeed, movableObjects));
            else if (type == BlockType.detector)
                gameArray[position.X, position.Y] = loadAndInitializeBlock(new Detector(game, BlockType.detector, position, blockSize, detectorActivated));
            else
                gameArray[position.X, position.Y] = loadAndInitializeBlock(new Block(game, type, position, blockSize));
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
