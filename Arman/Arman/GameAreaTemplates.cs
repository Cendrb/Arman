using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arman
{
    public class GameAreaTemplates
    {
        public static void DrawTata(GameArea GA)
        {
            GA.CreateBlock(BlockType.solid, new PositionInGrid(4, 2));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(5, 2));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(6, 2));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(7, 2));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(8, 2));
            GA.CreateBlock(BlockType.movable, new PositionInGrid(16, 2));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(24, 2));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(25, 2));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(26, 2));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(27, 2));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(28, 2));
            GA.CreateBlock(BlockType.movable, new PositionInGrid(36, 2));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(6, 3));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(15, 3));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(17, 3));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(26, 3));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(35, 3));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(37, 3));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(6, 4));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(14, 4));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(18, 4));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(26, 4));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(34, 4));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(38, 4));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(6, 5));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(14, 5));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(18, 5));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(26, 5));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(34, 5));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(38, 5));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(6, 6));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(14, 6));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(15, 6));
            GA.CreateBlock(BlockType.movable, new PositionInGrid(16, 6));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(17, 6));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(18, 6));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(26, 6));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(34, 6));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(35, 6));
            GA.CreateBlock(BlockType.movable, new PositionInGrid(36, 6));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(37, 6));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(38, 6));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(6, 7));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(14, 7));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(18, 7));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(26, 7));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(34, 7));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(38, 7));
            //puzzle/1 solid
            GA.CreateBlock(BlockType.solid, new PositionInGrid(0, 21));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(2, 24));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(3, 24));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(3, 21));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(4, 20));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(5, 20));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(5, 22));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(7, 21));
            //2 solid
            GA.CreateBlock(BlockType.solid, new PositionInGrid(18, 21));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(19, 20));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(19, 22));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(20, 19));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(21, 18));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(21, 24));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(22, 23));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(23, 20));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(23, 22));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(24, 21));
            //3 solid
            GA.CreateBlock(BlockType.solid, new PositionInGrid(37, 23));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(37, 24));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(38, 22));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(39, 21));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(39, 22));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(39, 24));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(40, 19));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(41, 20));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(41, 21));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(42, 18));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(44, 19));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(44, 20));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(44, 21));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(44, 22));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(44, 23));
            GA.CreateBlock(BlockType.solid, new PositionInGrid(44, 24));
            //1 movable
            GA.CreateBlock(BlockType.movable, new PositionInGrid(0, 20));
            GA.CreateBlock(BlockType.movable, new PositionInGrid(0, 24));
            GA.CreateBlock(BlockType.movable, new PositionInGrid(1, 22));
            GA.CreateBlock(BlockType.movable, new PositionInGrid(2, 23));
            GA.CreateBlock(BlockType.movable, new PositionInGrid(3, 22));
            GA.CreateBlock(BlockType.movable, new PositionInGrid(4, 23));
            GA.CreateBlock(BlockType.movable, new PositionInGrid(6, 20));
            //2 movable
            GA.CreateBlock(BlockType.movable, new PositionInGrid(20, 23));
            GA.CreateBlock(BlockType.movable, new PositionInGrid(21, 20));
            GA.CreateBlock(BlockType.movable, new PositionInGrid(21, 22));
            GA.CreateBlock(BlockType.movable, new PositionInGrid(22, 19));
            //3 movable
            GA.CreateBlock(BlockType.movable, new PositionInGrid(40, 23));
            GA.CreateBlock(BlockType.movable, new PositionInGrid(41, 24));
            GA.CreateBlock(BlockType.movable, new PositionInGrid(42, 19));
            GA.CreateBlock(BlockType.movable, new PositionInGrid(42, 23));
            GA.CreateBlock(BlockType.movable, new PositionInGrid(43, 22));

            //detectors
            GA.CreateBlock(BlockType.detector, new PositionInGrid(6, 10));
            GA.CreateBlock(BlockType.detector, new PositionInGrid(16, 10));
            GA.CreateBlock(BlockType.detector, new PositionInGrid(26, 10));
            GA.CreateBlock(BlockType.detector, new PositionInGrid(36, 10));
            GA.CreateBlock(BlockType.detector, new PositionInGrid(1, 17));
            GA.CreateBlock(BlockType.detector, new PositionInGrid(3, 17));
            GA.CreateBlock(BlockType.detector, new PositionInGrid(5, 17));
            GA.CreateBlock(BlockType.detector, new PositionInGrid(18, 17));
            GA.CreateBlock(BlockType.detector, new PositionInGrid(21, 17));
            GA.CreateBlock(BlockType.detector, new PositionInGrid(24, 17));
            GA.CreateBlock(BlockType.detector, new PositionInGrid(39, 17));
            GA.CreateBlock(BlockType.detector, new PositionInGrid(41, 17));
            GA.CreateBlock(BlockType.detector, new PositionInGrid(43, 17));

        }
        public static void DrawPacman(GameArea GA)
        {
            //coins
            GA.CreateBlock(BlockType.coin, new PositionInGrid(1, 1));
            GA.CreateBlock(BlockType.coin, new PositionInGrid(1, 3));
            GA.CreateBlock(BlockType.coin, new PositionInGrid(1, 5));
        }
    }
}
