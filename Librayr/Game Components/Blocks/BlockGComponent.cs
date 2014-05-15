﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman_Class_Library
{
    public class BlockGComponent : GameComponent
    {
        private Block model;
        public BlockGComponent(Game game, GameDataTools tools, Block model)
            : base(game, tools, model)
        {

            this.model = model;
        }
    }
}
