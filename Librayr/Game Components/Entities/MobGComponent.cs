
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman_Class_Library
{
    public class MobGComponent : EntityGComponent
    {
        public new Mob Model { get; private set; }

        private float movePause;
        private PositionInGrid target = new PositionInGrid();

        public MobGComponent(World tools, Mob model)
            : base(tools, model)
        {
            movePause = model.WanderChance;
            Model = model;
            Tasker.AddAITask(1, new AITaskWander(Navigator, this));
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        protected override void LoadContent()
        {
            texture = Textures.Mob;
            base.LoadContent();
        }
    }
}
