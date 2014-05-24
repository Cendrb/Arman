using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman_Class_Library
{
    [Serializable]
    public class ArmanColor
    {
        public float A { get; set; }
        public float R { get; set; }
        public float G { get; set; }
        public float B { get; set; }

        public ArmanColor(float a, float r, float g, float b)
        {
            A = a;
            R = r;
            G = g;
            B = b;
        }
        public ArmanColor()
        {

        }
        public Microsoft.Xna.Framework.Color GetXnaColor()
        {
            return new Microsoft.Xna.Framework.Color(R, G, B, A);
        }
        public static ArmanColor ParseWindowsColor(System.Windows.Media.Color color)
        {
            return new ArmanColor(color.ScA, color.ScR, color.ScG, color.ScB);
        }
    }
}
