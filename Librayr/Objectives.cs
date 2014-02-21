using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman_Class_Library
{
    public class Objectives
    {
        public bool CollectAllCoins { get; set; }
        public bool GetHome { get; set; }
        public bool ActivateDetectors { get; set; }

        public Objectives Clone()
        {
            Objectives o = new Objectives();
            o.ActivateDetectors = ActivateDetectors;
            o.CollectAllCoins = CollectAllCoins;
            o.GetHome = GetHome;
            return o;
        }
    }
}
