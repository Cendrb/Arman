using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman_Class_Library
{
    public class DataForLoader
    {
        public string PlayerTexture { get; private set; }
        public string MobTexture { get; private set; }
        public string MovableBlockTexture { get; private set; }

        public string SolidBlockTexture { get; private set; }
        public string AirBlockTexture { get; private set; }
        public string DetectorTexture { get; private set; }
        public string HomeTexture { get; private set; }

        public string CoinTexture { get; private set; }


        public DataForLoader(string playerTexture, string mobTexture, string mBlockTexture, string solidBlockTexture, string airBlockTexture, string detectorTexture, string coinTexture, string homeTexture)
        {

            PlayerTexture = playerTexture;
            MobTexture = mobTexture;
            MovableBlockTexture = mBlockTexture;

            SolidBlockTexture = solidBlockTexture;
            AirBlockTexture = airBlockTexture;
            DetectorTexture = detectorTexture;
            HomeTexture = homeTexture;

            CoinTexture = coinTexture;
        }
    }
}
