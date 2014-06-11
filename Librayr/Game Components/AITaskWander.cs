using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman_Class_Library
{
    public class AITaskWander : AITask
    {
        Navigator navigator;
        MobGComponent mob;
        bool wandering = false;
        int tries = 0;

        public AITaskWander(Navigator navigator, MobGComponent mob)
        {
            this.navigator = navigator;
            this.mob = mob;
            navigator.Completed += Stop;
        }

        public override void Execute()
        {
            if (!wandering)
            {
                navigator.Target = getRandomValidPosition();
                wandering = true;
            }
        }

        private PositionInGrid getRandomValidPosition()
        {
            List<PositionInGrid> positions = PositionInGrid.GetPositionsInSquareRadius(mob.Model.WanderRange, mob.Position);
            List<PositionInGrid> verifiedPositions = new List<PositionInGrid>();

            Random rnd = new Random();

            foreach (PositionInGrid pos in positions)
            {
                bool fap = true;
                bool penis = false;
                IEnumerable<GameComponent> comps = mob.World.GetGameComponentsAt<GameComponent>(pos);
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
            return verifiedPositions[rnd.Next(verifiedPositions.Count)];
        }

        public override bool Activate()
        {
            if (!navigator.Busy)
                tries++;
            if (tries > mob.World.Random.Next(mob.Model.WanderChance * 50, mob.Model.WanderChance * 60))
            {
                tries = 0;
                return true;
            }
            return false;
        }

        public override void Stop()
        {
            wandering = false;
        }
    }
}
