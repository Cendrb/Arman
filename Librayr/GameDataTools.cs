using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Arman_Class_Library
{
    public class GameDataTools
    {
        public List<Detector> Detectors { get; private set; }
        public List<Player> Players { get; private set; }

        public GameData Data { get; set; }
        public Game Game { get; set; }

        public GameDataTools(GameData data, Game game)
        {
            this.Data = data;
            Game = game;
        }

        public GameDataTools()
        {
            Detectors = new List<Detector>();
            Players = new List<Player>();
        }

        public Entity GetEntityAt(PositionInGrid pos)
        {
            foreach (Entity e in Data.Entities)
            {
                if (e.Position == pos)
                    return e;
            }
            return null;
        }
        public Block GetBlockAt(PositionInGrid pos)
        {
            foreach (Block b in Data.Blocks)
            {
                if (b.Position == pos)
                    return b;
            }
            return null;
        }
        public Coin GetCoinAt(PositionInGrid pos)
        {
            foreach (Coin c in Data.Coins)
            {
                if (c.Position == pos)
                    return c;
            }
            return null;
        }

        public void Initialize()
        {
            foreach (Block b in Data.Blocks)
            {
                if (b is Detector)
                    Detectors.Add(b as Detector);
            }
            foreach (Entity e in Data.Entities)
            {
                if (e is Player)
                    Players.Add(e as Player);
            }
        }
    }
}
