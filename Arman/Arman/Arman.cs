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
    
    public enum EntityType { player, mob }
    public enum BlockType { solid, nonsolid, movable, detector, coin }
    public enum Direction { up, down, left, right }
    public enum GameTarget { collectAllCoins, getHome, placeBlocksOnDetectors };

    public delegate void DetectorActivated();
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Arman : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        public BetterSpriteBatch spriteBatch;
        public SpriteFont fontCourierNew, fontCourierNewBig, arial;
        private Song musicEnjoyTheSilence;

        //položky okna
        private int windowHeight;
        private int windowWidth;

        //mob identifiers
        public static Texture2D mobSpeedID, mobRangeID;

        //grafika: sprity
        private Texture2D background;

        //sounds
        public static SoundEffect playerDeath;

        //lokace pozadí
        private Vector2 backgroundPosition;

        private GameArea gameArea;

        //rychlost hry (pùvodní hodnota vynásobená nasledující
        private float rychlostPosunuPozadi;

        public Arman()
        {
            //nastavení velikosti okna
            windowWidth = 1920;
            windowHeight = 1080;

            //nastavení rychlosti pozadi
            rychlostPosunuPozadi = 0.0F;

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
            //nastvaení základnáích hodnot hrý
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = windowHeight;
            graphics.PreferredBackBufferWidth = windowWidth;
            //nutno aplikovat zmìny!
            graphics.ApplyChanges();

            //púøidání herních komponent
            gameArea = new GameArea(this);
            Components.Add(gameArea);

            //pøíprava na posun pozadí
            backgroundPosition = new Vector2(0, 0);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new BetterSpriteBatch(GraphicsDevice);

            background = Content.Load<Texture2D>(@"Sprites/sipky");
            fontCourierNew = Content.Load<SpriteFont>(@"Fonts/DefaultFont");
            fontCourierNewBig = Content.Load<SpriteFont>(@"Fonts/BigCourier");
            arial = Content.Load<SpriteFont>(@"Fonts/Arial");
            musicEnjoyTheSilence = Content.Load<Song>(@"Music/EnjoyTheSilence");

            //mob identifiers
            mobSpeedID = Content.Load<Texture2D>(@"Sprites/MobID/bottomMobID");
            mobRangeID = Content.Load<Texture2D>(@"Sprites/MobID/bottomMobID");

            //sounds
            playerDeath = Content.Load<SoundEffect>(@"Sounds/death");

            AddToQueue(musicEnjoyTheSilence);
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

            //zmìna pozaïové vlastnosti
            backgroundPosition.Y += 1 * rychlostPosunuPozadi;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            VykresliPozadi();

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void AddToQueue(Song music)
        {
            // Nehraje již ta samá hudba?
            if (MediaPlayer.Queue.ActiveSong != music)
                MediaPlayer.Play(music);
        }

        private void VykresliPozadi()
        {
            if (background.Height <= backgroundPosition.Y)
                backgroundPosition.Y = 0;
            spriteBatch.Draw(background, new Vector2(0, backgroundPosition.Y - background.Height), Color.White);
            for (int i = 0; i != windowHeight / background.Height; i++)
            {
                spriteBatch.Draw(background, new Vector2(0, backgroundPosition.Y + i * background.Height), Color.White);
            }
        }
    }
}
