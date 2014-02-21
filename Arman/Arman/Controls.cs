using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman
{
    public struct Controls
    {
        public Keys up, right, down, left;
        public Controls(Keys up, Keys right, Keys down, Keys left)
        {
            this.up = up;
            this.right = right;
            this.down = down;
            this.left = left;
        }
    }
}
