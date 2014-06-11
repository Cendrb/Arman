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
        public PositionInGrid Target { get; set; }
        public event Action Completed = delegate { };
        public bool Busy { get; private set; }

        public Navigator(EntityGComponent entity)
        {
            this.entity = entity;
            Busy = false;
        }

        public void Update(GameTime time)
        {
            if (!isNull(Target))
            {
                if (entity.Position == Target)
                    completed();
                if (!isNull(Target))
                    GoToPosition(Target);
            }
        }

        private bool isNull(PositionInGrid pos)
        {
            try
            {
                int x = pos.X;
            }
            catch(NullReferenceException e)
            {
                return true;
            }
            return false;
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
