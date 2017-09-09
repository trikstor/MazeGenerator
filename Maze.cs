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
        void Print();
    }

    // Поле для лабиринта
    public class Field : IMazeble
    {
        private const int width = 300;
        private const int height = 300;
        private Graphics _g;

        static public MazeCell[,] field = new MazeCell[10, 10];

        public Field(Graphics g)
        {
            _g = g;
        }

        public void Print()
        {
            _g.DrawRectangle(new Pen(Color.Black), 30, 30, width, height);

            for (int i = 1; i <= 10; i++)
                for (int n = 1; n <= 10; n++)
                    field[i - 1, n - 1].Print();
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

        private Point _dotCoord;
        public List<cellSide> _sides { get; private set; }

        public MazeCell(Graphics g, int x, int y, List<cellSide> sides)
        {
            _g = g;
            _dotCoord = new Point(x, y);
            _sides = sides;
        }

       private Point CheckSide(cellSide side, out Point swic)
        {
            Point tempCoord = new Point(_dotCoord.x, _dotCoord.y);
            swic = _dotCoord;

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

        public void Print()
        {
            Point swic, tempCoord;

            foreach(cellSide side in _sides)
            {
                tempCoord = CheckSide(side, out swic);
                CellGraph(swic, tempCoord);
            }
        }

        private void CellGraph(Point swic, Point tempCoord)
        {
            _g.DrawLine(new Pen(Color.Black), tempCoord.x, tempCoord.y, swic.x, swic.y);
        }
    }

    // Построение лабиринта
    class SquareMaze : IMazeble
    {
        Graphics _g;

        public SquareMaze(Graphics g)
        {
            int cnt;

            List<MazeCell.cellSide> lt;
            MazeCell.cellSide[] Border = { MazeCell.cellSide.left, MazeCell.cellSide.right, 
                                             MazeCell.cellSide.top, MazeCell.cellSide.bottom };
            _g = g;
            Random rnd = new Random();

            for (int i = 1; i <= 10; i++)
            {
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
                        if (!lt.Contains(Border[cnt]))
                            lt.Add(Border[cnt]);
                    }

                    Field.field[i - 1, n - 1] = new MazeCell(_g, i * 30, n * 30, lt);
                }
            }
            MazeAlgo MA = new MazeAlgo();
            MA.MazeGen(Field.field, MazeCell.cellSide.bottom);
            MA.MazeGen(Field.field, MazeCell.cellSide.right);
        }

        // Печать лабиринта
        public void Print()
        {
            Field MF = new Field(_g);
            MF.Print();
        }
    }

    // Алгоритм построения лабиринта
    public class MazeAlgo
    {
        private MazeCell.cellSide LogNo(MazeCell.cellSide side)
        {
            if (side == MazeCell.cellSide.bottom)
                return MazeCell.cellSide.right;
            return MazeCell.cellSide.bottom;
        }

        public void MazeGen(MazeCell[,] field, MazeCell.cellSide delSide)
        {
            int i = 0, n = 0; // Столбцы, строки

            while (n < 10 && i < 10)
            {
                if (!field[i, n]._sides.Contains(delSide))
                {
                    if (delSide == MazeCell.cellSide.bottom)
                        i++;
                    else
                        n++;
                }
                else if (!field[i, n]._sides.Contains(LogNo(delSide)))
                {
                    if (delSide == MazeCell.cellSide.bottom)
                        n++;
                    else
                        i++;
                }
                else
                {
                    field[i, n]._sides.Remove(delSide); // Пробиваем путь для исполнителя

                    if (delSide == MazeCell.cellSide.bottom)
                        i++;
                    else
                        n++;
                }
            }
        }
    }
}
