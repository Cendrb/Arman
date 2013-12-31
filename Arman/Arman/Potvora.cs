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
        private void walk()
        {
            Random random = new Random();
            switch(random.Next(0, 3))
            {
                case 0:
                    this.Move(Direction.left);
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
            }
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            walk();
            base.Update(gameTime);
        }
    } 
}
