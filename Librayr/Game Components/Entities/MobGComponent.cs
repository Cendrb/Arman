
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman_Class_Library
{
    public class MobGComponent : EntityGComponent
    {
        private bool onTheWay = false;

        public new Mob Model { get; private set; }

        private float movePause;
        private PositionInGrid target = new PositionInGrid();

        public MobGComponent(GameComponents tools, Mob model)
            : base(tools, model)
        {
            movePause = model.WanderChance;
            Model = model;
        }
        public override void Update(GameTime gameTime)
        {
            if (Position == target)
                onTheWay = false;
            if (onTheWay && !IsMoving)
                GoToPosition(target);
            if (!IsMoving)
            {
                if (!onTheWay)
                    movePause -= 1;
                if (movePause == 0)
                {
                    movePause = Model.WanderChance;
                    wander();
                }
            }

            base.Update(gameTime);
        }
        private void wander()
        {
            if (!onTheWay)
            {
                List<PositionInGrid> positions = PositionInGrid.GetPositionsInSquareRadius(Model.WanderRange, Position);
                List<PositionInGrid> verifiedPositions = new List<PositionInGrid>();

                Random rnd = new Random();

                foreach (PositionInGrid pos in positions)
                {
                    bool fap = true;
                    bool penis = false;
                    IEnumerable<GameComponent> comps = tools.GetGameComponentsAt<GameComponent>(pos);
                    foreach (GameComponent comp in comps)
                    {
                        penis = true;
                        if (!comp.Model.Collides)
                            fap = fap && true;
                        else if (comp is EntityGComponent && (comp.Model as Entity).CanBePushed)
                            fap = fap && true;
                        else
                            fap = false;
                    }
                    if (penis && fap)
                        verifiedPositions.Add(pos);
                }
                target = verifiedPositions[rnd.Next(verifiedPositions.Count)];
                onTheWay = true;
            }
        }
        public void GoToPosition(PositionInGrid pos)
        {
            Direction direction = PositionInGrid.GetDirection(Position, pos);
            Move(direction);
        }
        protected override void LoadContent()
        {
            texture = Textures.Mob;
            base.LoadContent();
        }
    }
}
