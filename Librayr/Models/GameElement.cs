using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman_Class_Library
{
    public class GameElement
    {
        public string Name { get; protected set; }
        public PositionInGrid Position { get; protected set; }
        public string Info
        {
            get
            {
                return String.Format("{0} ({1}; {2})");
            }
            private set
            {

            }
        }

        public GameElement(PositionInGrid position)
        {
            Position = position;
            Name = "Game element";
        }

        public GameElement(PositionInGrid position, string name)
        {
            Position = position;
            Name = name;
        }
    }
}
