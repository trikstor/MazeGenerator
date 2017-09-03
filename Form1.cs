using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MazeGenerator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();

            Field fl = new Field(g);
            fl.Print();

            // test
            /*
            MazeCell cl = new MazeCell(g);

            List<MazeCell.cellSide> lt = new List<MazeCell.cellSide>() { MazeCell.cellSide.right, MazeCell.cellSide.top };

            for(int i = 1; i <= 10; i++)
                for(int n = 1; n <= 10; n++)
                    cl.Print(i*30, n*30, lt);
             */
            Maze nm = new Maze();
            nm.Print(g);
        }
    }
}
