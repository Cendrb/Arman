using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman_Class_Library
{
    [Serializable]
    public class GameData
    {
        public List<Block> Blocks { get; set; }
        public List<Entity> Entities { get; set; }
        public List<Coin> Coins { get; set; }

        public int XGameArea { get; set; }
        public int YGameArea { get; set; }
        public string Name { get; set; }
        public int Speed { get; set; }
        public int OneBlockSize { get; set; }

        public Objectives Objectives { get; set; }

        public GameData()
        {
            Blocks = new List<Block>();
            Entities = new List<Entity>();
            Coins = new List<Coin>();
            Objectives = new Objectives();
        }

        public GameData Clone()
        {
            GameData newGameData = new GameData();
            newGameData.Blocks = this.Blocks.ToList();
            newGameData.Entities = this.Entities.ToList();
            newGameData.Coins = this.Coins.ToList();
            newGameData.Name = Name;
            newGameData.Speed = Speed;
            newGameData.XGameArea = XGameArea;
            newGameData.YGameArea = YGameArea;
            newGameData.Objectives = Objectives.Clone();
            return newGameData;
        }
    }
}
