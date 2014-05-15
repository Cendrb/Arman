using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman_Class_Library
{
    public class Controls
    {
        public string up, right, down, left;
        public Controls(string up, string down, string left, string right)
        {
            this.up = up;
            this.right = right;
            this.down = down;
            this.left = left;
        }
        public static Controls Parse(string text)
        {
            string[] controls = text.Split(',');

            string up, right, down, left;
            up = controls[0];
            down = controls[1];
            left = controls[2];
            right = controls[3];

            return new Controls(up, down, left, right);
        }
    }
}
