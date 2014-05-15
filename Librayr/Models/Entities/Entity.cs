using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Arman_Class_Library
{
    [XmlInclude(typeof(Mob))]
    [XmlInclude(typeof(MovableBlock))]
    [XmlInclude(typeof(Player))]
    public class Entity : GameElement
    {
        public bool CanPush { get; set; }
        public bool CanBePushed { get; set; }
        public float Speed { get; set; }
        private bool collides;
        public bool Collides
        {
            get
            {
                return collides;
            }
            set
            {
                collides = value;
                if (!collides)
                {
                    CanPush = false;
                    CanBePushed = false;
                }
            }
        }
        public double Health { get; set; }
        public bool Invulnerable { get; set; }

        public bool Alive
        {
            get
            {
                return Health > 0;
            }
        }


        public Entity(PositionInGrid position, string name, bool canPush, bool canBePushed, float speed, bool collides, double health, bool invulnerable)
            : base(position, name)
        {
            Collides = collides;
            Speed = speed;
            Health = health;
            Invulnerable = invulnerable;
        }
        public Entity(PositionInGrid position, bool canPush, bool canBePushed, float speed, bool collides, double health, bool invulnerable)
            : base(position, "Generic entity")
        {
            if (collides)
            {
                CanPush = canPush;
                CanBePushed = canBePushed;
            }
            else
            {
                CanPush = false;
                CanBePushed = false;
            }
            Collides = collides;
            Speed = speed;
            Health = health;
            Invulnerable = invulnerable;
        }
        public Entity()
        {

        }
    }
}
