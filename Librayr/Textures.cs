using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman_Class_Library
{
    public static class Textures
    {
        public static Texture2D Player { get; private set; }
        public static Texture2D Mob { get; private set; }
        public static Texture2D MovableBlock { get; private set; }

        public static Texture2D Solid { get; private set; }
        public static Texture2D Air { get; private set; }
        public static Texture2D Detector { get; private set; }
        public static Texture2D Home { get; private set; }

        public static Texture2D Coin { get; private set; }

        public static Texture2D KeyLockColorDot { get; private set; }

        public static SpriteBatch SpriteBatch { get; private set; }

        public static bool Load(TexturesPaths paths, GameDataTools tools)
        {
            Air = tools.Game.Content.Load<Texture2D>(paths.AirBlock);
            Mob = tools.Game.Content.Load<Texture2D>(paths.KeyLockColorDot);
            MovableBlock = tools.Game.Content.Load<Texture2D>(paths.MovableBlock);
            Solid = tools.Game.Content.Load<Texture2D>(paths.SolidBlock);
            Player = tools.Game.Content.Load<Texture2D>(paths.Player);
            Detector = tools.Game.Content.Load<Texture2D>(paths.Detector);
            Home = tools.Game.Content.Load<Texture2D>(paths.Home);
            Coin = tools.Game.Content.Load<Texture2D>(paths.Coin);
            KeyLockColorDot = tools.Game.Content.Load<Texture2D>(paths.KeyLockColorDot);

            SpriteBatch = new Microsoft.Xna.Framework.Graphics.SpriteBatch(tools.Game.GraphicsDevice);

            return true;
        }
    }
}
