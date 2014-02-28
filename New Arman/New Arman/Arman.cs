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
using Arman_Class_Library;
using System.Xml;
using System.IO;

namespace New_Arman
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Arman : Game
    {
        // fPS
        int _total_frames = 0;
        float _elapsed_time = 0.0f;
        int _fps = 0;

        private SpriteBatch spriteBatch;

        SpriteFont arial;
        GraphicsDeviceManager graphics;
        GameArea area;
        DataForLoader appData;

        private string path;

        public Arman()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            if (!Directory.Exists(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Arman")))
                Directory.CreateDirectory(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Arman"));
            path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Arman", "level.alvl");

            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferredBackBufferWidth = 1600;
            graphics.ApplyChanges();

            string playerTex, mobTex, mBlockTex, solidBlockTex, airBlockTex, detectorTex, coinTex, homeTex;
            playerTex = @"Sprites/Entities/player";
            mobTex = @"Sprites/Entities/mob";
            mBlockTex = @"Sprites/Entities/movable";
            solidBlockTex = @"Sprites/Blocks/solid";
            airBlockTex = @"Sprites/Blocks/air";
            detectorTex = @"Sprites/Blocks/detector";
            coinTex = @"Sprites/Blocks/coin";
            homeTex = @"Sprites/Blocks/detector"; // TERXTURE!

            appData = new DataForLoader(playerTex, mobTex, mBlockTex, solidBlockTex, airBlockTex, detectorTex, coinTex, homeTex);

            area = new GameArea(this, path, appData);
            Components.Add(area);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            arial = Content.Load<SpriteFont>(@"Fonts/Arial");

            base.LoadContent();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            _total_frames++;
            // Update
            _elapsed_time += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
 
            // 1 Second has passed
            if (_elapsed_time >= 1000.0f)
            {
                _fps = _total_frames;
                _total_frames = 0;
                _elapsed_time = 0;
            }
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);

            spriteBatch.Begin();
            spriteBatch.DrawString(arial, string.Format("FPS={0}", _fps),
                new Vector2(20.0f, 20.0f), Color.White);
            spriteBatch.End();

            
        }
    }
}
