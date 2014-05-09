using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman_Class_Library
{
    [Serializable]
    public class Coin : Block
    {
        public int Value { get; private set; }

        public Coin(Game game, PositionInGrid position, GameDataTools tools, int value)
            : base(game, position, tools)
        {
            Value = value;
        }
    }
}
