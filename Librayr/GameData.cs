using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman_Class_Library
{
    public class GameData
    {
        public List<Block> Blocks { get; private set; }
        public List<Entity> Entities { get; private set; }
        public List<Coin> Coins { get; private set; }

        public GameData(List<Block> blocks, List<Entity> entities, List<Coin> coins)
        {
            Blocks = blocks;
            Entities = entities;
            Coins = coins;
        }
    }
}
