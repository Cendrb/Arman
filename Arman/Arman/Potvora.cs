using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman
{
    public class Potvora : MovableObject
    {
        public Potvora(Arman game, PositionInGrid positionInGrid, Texture2D texture, Block[,] gameArray, int oneBlockSize, int timeForMove, List<MovableObject> movableObjects)
            :base(game, positionInGrid, texture, gameArray, oneBlockSize, timeForMove, movableObjects)
        {

        }
    } 
}
