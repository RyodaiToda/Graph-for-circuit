using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Graph
{
    public partial class Form1 : Form
    {
        private PictureBox pb = new PictureBox();

        private CircuitEquation ce = new CircuitEquation();

        private Graph g = new Graph();
        private int clickId = -1;

        private Button bt;

        private int grid = 50;

        private ContextMenuStrip _nodeContextMenuStrip = new ContextMenuStrip();
        private ContextMenuStrip _edgeContextMenuStrip = new ContextMenuStrip();
        private ContextMenuStrip _canvasContextMenuStrip = new ContextMenuStrip();
        

        public Form1()
        {


            InitializeComponent();

            Image img = new Bitmap(16, 16);

            _nodeContextMenuStrip.Items.Add("Add node", img, func);
            _nodeContextMenuStrip.Items.Add("Delete node");

            _edgeContextMenuStrip.Items.Add("Add edge");
            _edgeContextMenuStrip.Items.Add("Delete edge");

            _canvasContextMenuStrip.Items.Add("Hello");

            this.ContextMenuStrip = _edgeContextMenuStrip;


            bt = new Button();
            bt.Top = 30;
            bt.Left = 10;
            bt.Text = "Calclation";
            bt.Click += Bt_Click;
            Controls.Add(bt);


            pb.Width = 1000;
            pb.Height = 1000;
            pb.Top = 0;
            pb.Left = 0;
            pb.Click += Pb_Click;
            pb.MouseDown += Pb_MouseDown;
            pb.MouseUp += Pb_MouseUp;
            pb.MouseMove += Pb_MouseMove;
            Controls.Add(pb);

            g.addNode();
            g.addNode();
            g.addNode();
            g.addNode();
            g.addNode();
            g.addNode();

            g.setPosition();

            g.addEdge(0, 1, 2, 6.0);
            g.addEdge(2, 1, 2, 3.0);
            g.addEdge(3, 2, 2, 1.0);
            g.addEdge(3, 0, 2, 2.0);
            g.addEdge(0, 2, 2, 5.0);
            g.addEdge(4, 5, 1, 24.0);
            g.addEdge(1, 4);
            g.addEdge(5, 3);

            //g.addEdge(1, 0, 1, 16.0);
            //g.addEdge(3, 0, 1, 20.0);
            //g.addEdge(0, 2, 2, 2.0);
            //g.addEdge(3, 2, 2, 2.0);
            //g.addEdge(1, 2, 2, 3.0);

            //g.addEdge(0, 1, 2, 1.0);
            //g.addEdge(3, 0, 1, 16.0);
            //g.addEdge(1, 2, 2, 1.0);

            //g.addEdge(1, 3, 2, 2.0);
            //g.addEdge(2, 3, 2, 1.0);

            //g.addEdge(0, 1);
            //g.addEdge(1, 2);
            //g.addEdge(2, 3);

            //g.addEdge(4, 0);
            //g.addEdge(4, 3);
            //g.addEdge(0, 3);
            //g.addEdge(2, 4);

            g.DFS(3);
            g.LoopDetecter();


            //g.showNode(pb);


            this.Size = new Size(600, 600);
        }

        private void func(object  sender, EventArgs e)
        {
            Console.WriteLine("Hello");
        }

        private void Bt_Click(object sender, EventArgs e)
        {
            g.DFS(0);
            g.LoopDetecter();

            //Matrix I_result = g.getResult();

            //String s = "";

            //for (int i = 0; i < I_result.getRow(); i++)
            //{
            //    s += "I" + i.ToString() + " = " + I_result.getElement(i, 0).ToString() + "\n";
            //}

            //MessageBox.Show(s);

            Form2 f2 = new Form2(g.getResult());
            f2.Show();

        }

        private void Pb_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.X < this.Width / 2)
                this.ContextMenuStrip = _nodeContextMenuStrip;
            else
                this.ContextMenuStrip = _edgeContextMenuStrip;

            if (clickId == -1) return;
            if (e.Button != MouseButtons.Left)
            {

                return;
            }
                g.setPosition(clickId, e.Location.X, e.Location.Y);
            //Console.Write(e.Location.X.ToString());
            //Console.WriteLine("," + e.Location.Y.ToString());

            //System.Drawing.Point p = System.Windows.Forms.Cursor.Position;

            //Console.WriteLine(p.X + ", " + p.Y);
            g.showGraph(pb);

        }

        private void Pb_MouseUp(object sender, MouseEventArgs e)
        {
            if (clickId != -1)
            {
                //g.setPosition(clickId, (e.Location.X / grid) * grid, (e.Location.Y / grid) * grid);
                g.showGraph(pb);
            }

                clickId = -1;
        }

        private void Pb_MouseDown(object sender, MouseEventArgs e)
        {
            clickId = g.posToindex(e.Location.X, e.Location.Y);
        }

        private void Pb_Click(object sender, EventArgs e)
        {

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            g.showGraph(pb);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.X < this.Width / 2)
                this.ContextMenuStrip = _nodeContextMenuStrip;
            else
                this.ContextMenuStrip = _edgeContextMenuStrip;

            Console.WriteLine("F");

            base.OnMouseMove(e);
        }


        private void addNodeNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            g.addNode();
            g.setPosition(g.getNodeCount() - 1, 100, 100);
            g.showGraph(pb);

        }

        private void menuMToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void addEdgeEToolStripMenuItem_Click(object sender, EventArgs e)
        {



        }

        private void drawGrid()
        {
            Bitmap canvas = new Bitmap(1000, 1000);
            Graphics g = Graphics.FromImage(canvas);
            Pen pen = new Pen(Color.Black, 2);



            // ペンを破棄
            pen.Dispose();

            pb.Image = canvas;
        }


    }

}



