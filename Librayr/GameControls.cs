using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman_Class_Library
{
    public struct GameControls
    {
        public Keys up, right, down, left;
        public GameControls(Keys up, Keys down, Keys left, Keys right)
        {
            this.up = up;
            this.right = right;
            this.down = down;
            this.left = left;
        }
        public static GameControls Parse(string text)
        {
            string[] controls = text.Split(',');

            Keys up, right, down, left;
            up = (Keys)Enum.Parse(typeof(Keys), controls[0], true);
            down = (Keys)Enum.Parse(typeof(Keys), controls[1], true);
            left = (Keys)Enum.Parse(typeof(Keys), controls[2], true);
            right = (Keys)Enum.Parse(typeof(Keys), controls[3], true);

            return new GameControls(up, down, left, right);
        }
    }
}
