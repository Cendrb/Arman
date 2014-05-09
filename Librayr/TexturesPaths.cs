using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman_Class_Library
{
    public class TexturesPaths
    {
        public string Player { get; set; }
        public string Mob { get; set; }
        public string MovableBlock { get; set; }

        public string SolidBlock { get; set; }
        public string AirBlock { get; set; }
        public string Detector { get; set; }
        public string Home { get; set; }

        public string Coin { get; set; }

        public string KeyLockColorDot { get; set; }


        public TexturesPaths(string playerTexture, string mobTexture, string mBlockTexture, string solidBlockTexture, string airBlockTexture, string detectorTexture, string coinTexture, string homeTexture, string keyLockColorDot)
        {

            Player = playerTexture;
            Mob = mobTexture;
            MovableBlock = mBlockTexture;

            SolidBlock = solidBlockTexture;
            AirBlock = airBlockTexture;
            Detector = detectorTexture;
            Home = homeTexture;

            Coin = coinTexture;

            KeyLockColorDot = keyLockColorDot;
        }
        public TexturesPaths()
        {

        }
    }
}
