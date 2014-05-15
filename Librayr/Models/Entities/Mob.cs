using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman_Class_Library
{
    public class Mob : Entity
    {
        public int Vision { get; set; }
        public int WanderRange { get; set; }
        public int WanderChance { get; set; }

        public Mob(PositionInGrid position, string name, bool canPush, bool canBePushed, float speed, bool collides, double health, bool invulnerable, int scanRange, int wanderRange, int wanderChance)
            : base(position, name, canPush, canBePushed, speed, collides, health, invulnerable)
        {
            Vision = scanRange;
            WanderRange = wanderRange;
            WanderChance = wanderChance;
        }
        public Mob(PositionInGrid position, bool canPush, bool canBePushed, float speed, bool collides, double health, bool invulnerable, int scanRange, int wanderRange, int wanderChance)
            : base(position, "Mob", canPush, canBePushed, speed, collides, health, invulnerable)
        {
            Vision = scanRange;
            WanderRange = wanderRange;
            WanderChance = wanderChance;
        }
        public Mob()
        {

        }
    }
}
