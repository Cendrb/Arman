using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman_Class_Library
{
    public class World
    {
        public Random Random { get; private set; }
        public float TimeForMove
        {
            get
            {
                return (float)Data.OneBlockSize - Data.Speed;
            }
        }

        public Game Game { get; private set; }
        public GameData Data { get; private set; }

        private List<GameComponent> components;

        private List<BlockGComponent> blocks;
        private List<EntityGComponent> entities;

        public World(Game game, GameData data)
        {
            this.Game = game;
            this.Data = data;
            Random = new Random();
            components = new List<GameComponent>();
            entities = new List<EntityGComponent>();
            blocks = new List<BlockGComponent>();

            foreach (Block b in data.Blocks)
            {
                if (b is Air)
                    AddBlock(new AirGComponent(this, b as Air));
                if (b is Solid)
                    AddBlock(new SolidGComponent(this, b as Solid));
                if (b is Coin)
                    AddBlock(new CoinGComponent(this, b as Coin));
                if (b is Home)
                    AddBlock(new HomeGComponent(this, b as Home));
                if (b is Detector)
                    AddBlock(new DetectorGComponent(this, b as Detector));
            }
            /*for (int x = data.XGameArea; x != 0; x--)
                for (int y = data.YGameArea; y != 0; y--)
                {
                    if (data.Blocks.Contains(new Block(new PositionInGrid(x, y), true, 0), new GameElementComparer()))
                        blocks.Add(new AirGComponent(this, new Air(new PositionInGrid(x, y))));
                }*/
            foreach (Entity e in data.Entities)
            {
                if (e is MovableBlock)
                    AddEntity(new MovableBlockGComponent(this, e as MovableBlock));
                if (e is Mob)
                    AddEntity(new MobGComponent(this, e as Mob));
                if (e is Player)
                    AddEntity(new PlayerGComponent(this, e as Player));
            }
        }

        public IEnumerable<T> GetGameComponentsAt<T>(PositionInGrid pos)
            where T : GameComponent
        {
            return from gc in components
                   where gc.Model.Position == pos
                   where gc is T
                   select gc as T;
        }

        public IEnumerable<T> GetGameComponents<T>()
            where T : GameComponent
        {
            return from gc in components
                   where gc is T
                   select gc as T;
        }

        public void AddComponent(GameComponent gc)
        {
            if (gc is BlockGComponent)
            {
                AddBlock(gc as BlockGComponent);
            }
            else if (gc is EntityGComponent)
            {
                AddEntity(gc as EntityGComponent);
            }
            else
            {
                Game.Components.Add(gc);
                components.Add(gc);
            }
        }
        public void RemoveComponent(GameComponent gc)
        {
            if (gc is BlockGComponent)
            {
                RemoveBlock(gc as BlockGComponent);
            }
            else if (gc is EntityGComponent)
            {
                RemoveEntity(gc as EntityGComponent);
            }
            else
            {
                Game.Components.Remove(gc);
                components.Remove(gc);
            }
        }

        public void AddBlock(BlockGComponent block)
        {
            Game.Components.Add(block);
            components.Add(block);
            blocks.Add(block);
        }
        public void AddEntity(EntityGComponent entity)
        {
            Game.Components.Add(entity);
            components.Add(entity);
            entities.Add(entity);
        }

        public void RemoveBlock(BlockGComponent block)
        {
            Game.Components.Remove(block);
            components.Remove(block);
            blocks.Remove(block);
        }
        public void RemoveEntity(EntityGComponent entity)
        {
            Game.Components.Remove(entity);
            components.Remove(entity);
            entities.Remove(entity);
        }

        public List<T> GetGameComponentsInSquareRadius<T>(int r, PositionInGrid center)
            where T : GameComponent
        {
            List<T> gcs = new List<T>();

            Rectangle rect = new Rectangle(center.X - r, center.Y - r, r * 2, r * 2);
            return GetGameComponentsInRectangle<T>(rect);
        }

        public List<T> GetGameComponentsInRectangle<T>(Rectangle r)
            where T : GameComponent
        {
            List<T> gcs = new List<T>();
            for (int x = r.Width; x > 0; x--)
                for (int y = r.Height; y > 0; y--)
                    gcs.AddRange(GetGameComponentsAt<T>(new PositionInGrid(x + r.X, y + r.Y)));
            return gcs;
        }

    }
}
