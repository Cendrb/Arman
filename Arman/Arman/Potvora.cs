using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman
{
    public class Mob : Entity
    {
        private List<Player> players;
        private List<Mob> mobs;

        public Mob(Arman game, PositionInGrid positionInGrid, Texture2D texture, Block[,] gameArray, int oneBlockSize, int timeForMove, List<Entity> movableObjects, List<Player> players, List<Mob> mobs)
            :base(game, positionInGrid, texture, gameArray, oneBlockSize, timeForMove, movableObjects)
        {
            this.players = players;
            this.mobs = mobs;
        }
        private void walk()
        {
            foreach(Player player in players)
            {
                if (player.PositionInGrid == new PositionInGrid(PositionInGrid.X - 1, PositionInGrid.Y))
                    this.Move(Direction.left);
                if (player.PositionInGrid == new PositionInGrid(PositionInGrid.X + 1, PositionInGrid.Y))
                    this.Move(Direction.right);
                if (player.PositionInGrid == new PositionInGrid(PositionInGrid.X, PositionInGrid.Y - 1))
                    this.Move(Direction.up);
                if (player.PositionInGrid == new PositionInGrid(PositionInGrid.X, PositionInGrid.Y + 1))
                    this.Move(Direction.down);
            }
            Random random = new Random();
            switch(random.Next(0, 10))
            {
                case 0:
                    if (this.Move(Direction.left))
                        break;
                    else
                        return;
                case 1:
                    if (this.Move(Direction.up))
                        break;
                    else
                        return;
                case 2:
                    if (this.Move(Direction.right))
                        break;
                    else
                        return;
                case 3:
                    if (this.Move(Direction.down))
                        break;
                    else
                        return;
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
        public override bool TryMove(PositionInGrid position)
        {
            return base.TryMove(position);
        }
    } 
}
