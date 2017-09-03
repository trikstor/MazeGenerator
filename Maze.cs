using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace MazeGenerator
{
    // Координата
    public class Point
    {
        public int x { get; set; }
        public int y { get; set; }

        public Point(int tx, int ty)
        {
            x = tx;
            y = ty;
        }
    }

    // Лабиринт
    interface IMazeble
    {
    }

    //
    public class Field : IMazeble
    {
        public const int width = 300;
        public const int height = 300;
        private Graphics _g;

        public Field(Graphics g)
        {
            _g = g;
        }

        public void Print()
        {
            _g.DrawRectangle(new Pen(Color.Black), 30, 30, width, height);
        }
    }

    // Клетка лабиринта 
    public class MazeCell : IMazeble
    {
        private const int _cellWidth = 30;
        private const int _cellHeight = 30;

        public enum cellSide
        {
            left,
            right,
            top,
            bottom
        }

        private Graphics _g;

        private int _x, _y;
        private List<cellSide> _sides;

        public MazeCell(Graphics g)
        {
            _g = g;
        }

        public Point CheckSide(cellSide side, out Point swic)
        {
            Point tempCoord = new Point(_x, _y);
            swic = new Point(_x, _y);

            switch (side)
                {
                    case cellSide.left:
                        swic.y += _cellHeight;
                        break;
                    case cellSide.right:
                        tempCoord.x += 30;
                        swic.x = tempCoord.x;
                        swic.y += _cellHeight;
                        break;
                    case cellSide.top:
                        swic.x += _cellWidth;
                        break;
                    case cellSide.bottom:
                        tempCoord.y -= 30;
                        swic.y = tempCoord.y;
                        swic.x += _cellWidth;
                        break;
                }
            return tempCoord;
        }

        public void Print(int x, int y, List<cellSide> _sides)
        {
            _x = x;
            _y = y;
            Point swic, tempCoord;

            foreach(cellSide side in _sides)
            {
                tempCoord = CheckSide(side, out swic);
                CellGraph(swic, tempCoord);
            }
        }

        public void CellGraph(Point swic, Point tempCoord)
        {
            _g.DrawLine(new Pen(Color.Black), tempCoord.x, tempCoord.y, swic.x, swic.y);
        }
    }

    // Построение лабиринта
    class Maze : IMazeble
    {
        public void Print(Graphics g)
        {
            int cnt;
            MazeCell mc = new MazeCell(g);
            //MazeCell [][]field;
            List<MazeCell.cellSide> lt;
            MazeCell.cellSide []Border = { MazeCell.cellSide.left, MazeCell.cellSide.right, 
                                             MazeCell.cellSide.top, MazeCell.cellSide.bottom };

            Random rnd = new Random();

            for (int i = 1; i <= 10; i++)
                for (int n = 1; n <= 10; n++)
                {
                    lt = new List<MazeCell.cellSide>();

                    // Если клетка расположена у границы поля лабиринта,
                    // то добавляем стенку клетки у границы лабиринта
                    if (i == 0)
                        lt.Add(MazeCell.cellSide.top);
                    if (n == 0)
                        lt.Add(MazeCell.cellSide.left);
                    if (i == 10)
                        lt.Add(MazeCell.cellSide.bottom);
                    if (n == 10)
                        lt.Add(MazeCell.cellSide.right);

                    while (lt.Count < 2)
                    {
                        cnt = rnd.Next(0, 3);
                        if( ! lt.Contains(Border[cnt]))
                            lt.Add(Border[cnt]);
                    }

                    mc.Print(i*30, n*30, lt);
                }

        }
    }
}
