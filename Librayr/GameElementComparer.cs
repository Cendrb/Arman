using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman_Class_Library
{
    public class GameElementComparer : IEqualityComparer<GameElement>
    {
        public bool Equals(GameElement x, GameElement y)
        {
            return x.Position == y.Position;
        }

        public int GetHashCode(GameElement obj)
        {
            throw new NotImplementedException();
        }
    }
}
