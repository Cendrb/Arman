using Microsoft.Xna.Framework;
using Arman_Class_Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine(PositionInGrid.GetDirection(new PositionInGrid(), new PositionInGrid(2, 5)));
            Console.ReadKey();


            /*
            GameData data = new GameData();
            data.Blocks.Add(new Air(new PositionInGrid(23, 69), "Maryporn"));
            data.Entities.Add(new Mob(new PositionInGrid(39, 69), "gay", true, true, 69, true, 100.0, false, 10, 69, 69));
            data.Name = "Level 69";
            data.OneBlockSize = 69;
            data.Speed = 69;
            data.XGameArea = 23;
            data.YGameArea = 23;

            FileStream fs = new FileStream(@"c:\Users\cendr_000\Downloads\rake test\gay.bin", FileMode.OpenOrCreate);

            BinaryFormatter formatter = new BinaryFormatter();
            data = (GameData)formatter.Deserialize(fs);
            formatter.Serialize(fs, data);

            DataLoader fap = new DataLoader(@"c:\Users\cendr_000\Downloads\rake test\dildo.xml");
            fap.SaveData(data);*/
        }
    }
}
