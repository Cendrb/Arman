using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman_Class_Library
{
    [Serializable]
    public abstract class GameElement
    {
        public string Name { get; set; }
        public PositionInGrid Position { get; set; }
        public string Info
        {
            get
            {
                return String.Format("{0} ({1}; {2})", Name, Position.X, Position.Y);
            }
        }

        public GameElement(PositionInGrid position, string name)
        {
            Position = position;
            Name = name;
        }
        public GameElement()
        {

        }
    }
}
