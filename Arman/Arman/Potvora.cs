using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman
{
    public class Potvora : MovableObject
    {
        private List<Player> players;
        public Potvora(Arman game, PositionInGrid positionInGrid, Texture2D texture, Block[,] gameArray, int oneBlockSize, int timeForMove, List<MovableObject> movableObjects, List<Player> players)
            :base(game, positionInGrid, texture, gameArray, oneBlockSize, timeForMove, movableObjects)
        {
            this.players = players;
        }
        private void walk()
        {
            Random random = new Random();
            switch(random.Next(0, 10))
            {
                case 0:
                    this.Move(Direction.left);
                    break;
                case 1:
                    this.Move(Direction.up);
                    break;
                case 2:
                    this.Move(Direction.right);
                    break;
                case 3:
                    this.Move(Direction.down);
                    break;
            }
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            walk();
            //zabezpečení likvidace hráčů
            IEnumerable<Player> affectedPlayers = from player in players
                                                  where player.PositionInGrid == PositionInGrid
                                                  select player;
            foreach(Player player in affectedPlayers)
            {
                player.Die();
            }
            base.Update(gameTime);
        }
    } 
}
