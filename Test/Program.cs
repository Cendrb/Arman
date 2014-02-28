using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            if (null is Object)
                Console.WriteLine("ROTOTO!");
            Console.WriteLine(Color.White.PackedValue);
            Console.ReadKey(true);
        }
    }
}
