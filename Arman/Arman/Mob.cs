using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arman_Class_Library;

namespace Arman
{
    public class Mob : Entity
    {
        private IEnumerable<Player> players;
        private IEnumerable<Mob> mobs;
        private int movingFrequency;
        private int seed;

        private int Range { get; set; }

        public Mob(Arman game, PositionInGrid positionInGrid, Texture2D texture, Block[,] gameArray, int oneBlockSize, int timeForMove, List<Entity> entities, int range, int aditionalTimeForMove, int movingFrequency)
            : base(game, positionInGrid, texture, gameArray, oneBlockSize, timeForMove, entities)
        {
            players = from entity in entities
                      where entity is Player
                      select (Player)entity;
            mobs = from entity in entities
                   where entity is Mob
                   select (Mob)entity;

            this.movingFrequency = movingFrequency;

            Range = range;

            this.timeForMove += aditionalTimeForMove;
        }
        public Mob(Arman game, PositionInGrid positionInGrid, Texture2D texture, Block[,] gameArray, int oneBlockSize, int timeForMove, List<Entity> entities, int range, int aditionalTimeForMove, int movingFrequency, int seed)
            : base(game, positionInGrid, texture, gameArray, oneBlockSize, timeForMove, entities)
        {
            players = from entity in entities
                      where entity is Player
                      select (Player)entity;
            mobs = from entity in entities
                   where entity is Mob
                   select (Mob)entity;

            this.movingFrequency = movingFrequency;

            Range = range;

            this.seed = seed;

            this.timeForMove += aditionalTimeForMove;
        }
        private void walk()
        {
            if (!tryToCatchPlayer())
            {
                Random random;
                if(seed != 0)
                    random = new Random(seed);
                else
                    random = new Random();
                switch (random.Next(0, movingFrequency))
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
            foreach(Mob mob in mobs)
            {
                if (mob.PositionInGrid == position)
                    return false;
            }
            return base.TryMove(position);
        }
        private bool tryToCatchPlayer()
        {
            foreach (Player player in players)
            {
                for (int x = Range; x > 0; x--)
                {
                    if (player.PositionInGrid == new PositionInGrid(PositionInGrid.X - x, PositionInGrid.Y))
                    {
                        this.Move(Direction.left);
                        return true;
                    }
                    if (player.PositionInGrid == new PositionInGrid(PositionInGrid.X + x, PositionInGrid.Y))
                    {
                        this.Move(Direction.right);
                        return true;
                    }
                    if (player.PositionInGrid == new PositionInGrid(PositionInGrid.X, PositionInGrid.Y - x))
                    {
                        this.Move(Direction.up);
                        return true;
                    }
                    if (player.PositionInGrid == new PositionInGrid(PositionInGrid.X, PositionInGrid.Y + x))
                    {
                        this.Move(Direction.down);
                        return true;
                    }
                }
            }
            return false;
        }
        public override void DrawInfo(BetterSpriteBatch batch)
        {
            base.DrawInfo(batch);
            Vector2 coord = GetRelativeCoordinates();
            coord.X += 510;
            coord.Y += 290;

            if(this.Range <= 5)
                batch.Draw(Arman.mobRangeID, coord, Color.Purple);
            else if (this.Range <= 10)
                batch.Draw(Arman.mobRangeID, coord, Color.Blue);
            else if (this.Range <= 20)
                batch.Draw(Arman.mobRangeID, coord, Color.Green);
            else if (this.Range <= 40)
                batch.Draw(Arman.mobRangeID, coord, Color.Yellow);
            else if (this.Range > 40)
                batch.Draw(Arman.mobRangeID, coord, Color.DarkRed);
        }
    } 
}
