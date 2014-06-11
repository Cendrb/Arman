﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman_Class_Library
{
    public abstract class AITask
    {
        public abstract void Execute();
        public abstract void Stop();
        public abstract bool Activate();
    }
}
