using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

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
                Thread thread = new Thread(setRandomValidPosition);
                thread.Start();
                wandering = true;
            }
        }

        private void setRandomValidPosition()
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
            if(verifiedPositions.Count > 0)
                navigator.Target = verifiedPositions[rnd.Next(verifiedPositions.Count) - 1];
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
