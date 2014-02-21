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

namespace New_Arman
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Arman : GameWithSpriteBatch
    {
        public override SpriteBatch SpriteBatch { get; protected set; }

        GraphicsDeviceManager graphics;
        GameArea area;

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
            path = @"c:\Users\Cendrb\SkyDrive\Dokumenty\Main.alvl";

            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferredBackBufferWidth = 1600;
            graphics.ApplyChanges();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D playerTex, mobTex, mBlockTex, solidBlockTex, airBlockTex, detectorTex, coinTex, homeTex;
            playerTex = Content.Load<Texture2D>(@"Sprites/Entities/player");
            mobTex = Content.Load<Texture2D>(@"Sprites/Entities/mob");
            mBlockTex = Content.Load<Texture2D>(@"Sprites/Entities/movable");
            solidBlockTex = Content.Load<Texture2D>(@"Sprites/Blocks/solid");
            airBlockTex = Content.Load<Texture2D>(@"Sprites/Blocks/air");
            detectorTex = Content.Load<Texture2D>(@"Sprites/Blocks/detector");
            coinTex = Content.Load<Texture2D>(@"Sprites/Blocks/coin");
            homeTex = Content.Load<Texture2D>(@"Sprites/Blocks/detector"); // TERXTURE!

            DataForLoader appData = new DataForLoader(playerTex, mobTex, mBlockTex, solidBlockTex, airBlockTex, detectorTex, coinTex, homeTex);
            area = new GameArea(this, path, appData);
            area.Initialize();
            Components.Add(area);
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

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
