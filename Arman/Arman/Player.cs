using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Arman
{
    public class Player : Entity
    {
        private GameArea gameArea;
        private Controls controls;
        private KeyboardState keyboardState;
        private PositionInGrid spawnPoint;

        public Player(Arman game, PositionInGrid positionInGrid, Texture2D texture, Block[,] gameArray, int oneBlockSize, int timeForMove, List<Entity> movableObjects, GameArea gameArea, Controls controls)
            : base(game, positionInGrid, texture, gameArray, oneBlockSize, timeForMove, movableObjects)
        {
            this.gameArea = gameArea;
            this.controls = controls;
            spawnPoint = positionInGrid;
        }
        public override bool Move(Direction direction)
        {
            //zabezpeèení posunu kostek
            IEnumerable<PositionInGrid> positions = from block in movableObjects
                                                    select block.PositionInGrid;
            switch(direction)
            {
                case global::Arman.Direction.up:
                    if (positions.Contains(new PositionInGrid(PositionInGrid.X, PositionInGrid.Y - 1)))
                    {
                        Entity selectedBlock = (from block in movableObjects
                                                where block.PositionInGrid.X == PositionInGrid.X
                                                where block.PositionInGrid.Y == PositionInGrid.Y - 1
                                                select block)
                                                .First();
                        if (!selectedBlock.Move(direction))
                            return false;
                    }
                    break;
                case global::Arman.Direction.down:
                    if (positions.Contains(new PositionInGrid(PositionInGrid.X, PositionInGrid.Y + 1)))
                    {
                        Entity selectedBlock = (from block in movableObjects
                                                      where block.PositionInGrid.X == PositionInGrid.X
                                                      where block.PositionInGrid.Y == PositionInGrid.Y + 1
                                                      select block)
                                                      .First();
                        if (!selectedBlock.Move(direction))
                            return false;
                    }
                    break;
                case global::Arman.Direction.left:
                    if (positions.Contains(new PositionInGrid(PositionInGrid.X - 1, PositionInGrid.Y)))
                    {
                        Entity selectedBlock = (from block in movableObjects
                                                      where block.PositionInGrid.X == PositionInGrid.X - 1
                                                      where block.PositionInGrid.Y == PositionInGrid.Y
                                                      select block)
                                                      .First();
                        if (!selectedBlock.Move(direction))
                            return false;
                    }
                    break;
                case global::Arman.Direction.right:
                    if (positions.Contains(new PositionInGrid(PositionInGrid.X + 1, PositionInGrid.Y)))
                    {
                        Entity selectedBlock = (from block in movableObjects
                                                      where block.PositionInGrid.X == PositionInGrid.X + 1
                                                      where block.PositionInGrid.Y == PositionInGrid.Y
                                                      select block)
                                                      .First();
                        if (!selectedBlock.Move(direction))
                            return false;
                    }
                    break;
            }
            return base.Move(direction);
        }
        public override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            base.Update(gameTime);
            //zabezpeèení sbírání bodíkù
            if (gameArray[PositionInGrid.X, PositionInGrid.Y].Type == BlockType.coin && !this.isMoving)
            {
                gameArea.CreateBlock(BlockType.nonsolid, new PositionInGrid(PositionInGrid.X, PositionInGrid.Y));
            }
        }
        public void Die()
        {
            this.PositionInGrid = spawnPoint;
        }
        public void ReactToControls()
        {
            if (keyboardState.IsKeyDown(controls.up))
            {
                this.Move(Direction.up);
            }
            else if (keyboardState.IsKeyDown(controls.down))
            {
                this.Move(Direction.down);
            }
            else if (keyboardState.IsKeyDown(controls.left))
            {
                this.Move(Direction.left);
            }
            else if (keyboardState.IsKeyDown(controls.right))
            {
                this.Move(Direction.right);
            }
        }
    }
}
