using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman_Class_Library
{
    public class DataForLoader
    {
        public Action<Mob> Wander { get; set; }
        public Func<Mob, bool> TryToCatchPlayer { get; set; }
        public Func<Direction, List<Entity>, bool> Move { get; set; }
        public Action<Entity> PostMoveControl { get; set; }

        public Texture2D PlayerTexture { get; private set; }
        public Texture2D MobTexture { get; private set; }
        public Texture2D MovableBlockTexture { get; private set; }

        public Texture2D SolidBlockTexture { get; private set; }
        public Texture2D AirBlockTexture { get; private set; }
        public Texture2D DetectorTexture { get; private set; }
        public Texture2D HomeTexture { get; private set; }

        public Texture2D CoinTexture { get; private set; }


        public DataForLoader(Texture2D playerTexture, Texture2D mobTexture, Texture2D mBlockTexture, Texture2D solidBlockTexture, Texture2D airBlockTexture, Texture2D detectorTexture, Texture2D coinTexture, Texture2D homeTexture)
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
