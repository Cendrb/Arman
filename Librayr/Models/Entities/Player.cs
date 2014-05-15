using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman_Class_Library
{
    public class Player : Entity
    {
        public int Lives { get; private set; }
        public Controls Controls { get; private set; }

        public Player(PositionInGrid position, string name, bool canPush, bool canBePushed, float speed, bool collides, double health, bool invulnerable, Controls controls, int lives)
            : base(position, name, canPush, canBePushed, speed, collides, health, invulnerable)
        {
            Lives = lives;
            Controls = controls;
        }
        public Player(PositionInGrid position, bool canPush, bool canBePushed, float speed, bool collides, double health, bool invulnerable, Controls controls, int lives)
            : base(position, "Player", canPush, canBePushed, speed, collides, health, invulnerable)
        {
            Lives = lives;
            Controls = controls;
        }
    }
}
