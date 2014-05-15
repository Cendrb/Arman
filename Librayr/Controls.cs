using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman_Class_Library
{
    public class Controls
    {
        public string Up { get; set; }
        public string Right { get; set; }
        public string Down { get; set; }
        public string Left { get; set; }
        public Controls(string up, string down, string left, string right)
        {
            this.Up = up;
            this.Right = right;
            this.Down = down;
            this.Left = left;
        }
        public Controls()
        {

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
