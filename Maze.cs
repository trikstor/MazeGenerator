using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace MazeGenerator
{
    public class Field
    {
        public const int width = 300;
        public const int height = 300;
    }

    public class Point
    {
        public int x{ get; set; }
        public int y{ get; set; }

        public Point(int x, int y)
        {
            x = x;
            y = y;
        }
    }

    public class MazeCell : Form
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

        private int _x, _y;
        private List<cellSide> _sides;

        public Point CheckSide(cellSide side, out Point swic)
        {
            Point tempCoord = new Point(0, 0);
            swic = new Point(0, 0);

            switch (side)
                {
                    case cellSide.left:
                        swic.y = _cellHeight;
                        break;
                    case cellSide.right:
                        tempCoord.x += 30;
                        swic.y = _cellHeight;
                        break;
                    case cellSide.top:
                        swic.x = _cellHeight;
                        break;
                    case cellSide.bottom:
                        swic.x = _cellHeight;
                        tempCoord.y -= 30;
                        break;
                }
            return tempCoord;
        }
        public void PrintCell(int _x, int _y, List<cellSide> _sides)
        {
            Point swic;

            foreach(cellSide side in _sides)
            {
                CheckSide(side, swic);
                CellGraph(swic.x, swic.y);
            }
        }

        public void CellGraph()
        {
            Graphics g = this.CreateGraphics(int x, int y);
            g.DrawLine(new Pen(Color.Black), x, y, 100, 100);
        }
    }
    class CreateMaze
    {

    }
}
