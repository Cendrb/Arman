﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman_Class_Library
{
    public class Air : Block
    {
        public Air(Game game, PositionInGrid position, string texture, GameDataTools tools)
            : base(game, position, texture, tools)
        {

        }
    }
}
