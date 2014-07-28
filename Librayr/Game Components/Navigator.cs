using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman_Class_Library
{
    public class Navigator
    {
        EntityGComponent entity;
        PositionInGrid target;
        public PositionInGrid Target
        {
            get
            {
                return target;
            }
            set
            {
                Busy = true;
                target = Target;
            }
        }
        public event Action Completed = delegate { };
        public bool Busy { get; private set; }

        public Navigator(EntityGComponent entity)
        {
            this.entity = entity;
            Busy = false;
        }

        public void Update(GameTime time)
        {
            if (Target != null)
            {
                if (entity.Position == Target)
                    completed();
                if (Target != null)
                    GoToPosition(Target);
            }
        }

        private void completed()
        {
            Target = null;
            Completed();
        }

        public void GoToPosition(PositionInGrid pos)
        {
            Direction direction = PositionInGrid.GetDirection(entity.Position, pos);
            if (!entity.IsMoving)
                if (!entity.Move(direction))
                    completed();
        }
    }
}
