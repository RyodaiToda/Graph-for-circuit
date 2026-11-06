using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graph
{
    public class Graph
    {

        private List<Node> node = new List<Node>();
        private List<Edge> edge = new List<Edge>();

        private List<int> visited = new List<int>();    //探索で通ったノード
        private List<int> tree = new List<int>();       //木のエッジ
        private List<int> cotree = new List<int>();     //補木のエッジ
        private List<List<int>> loop;                   //ループ

        private CircuitEquation ce = new CircuitEquation();
        private Matrix I_result;

        private int radius = 15;

        public void addNode()
        {
            Node n = new Node(node.Count);
            node.Add(n);
        }

        public void addEdge(int start, int end, int ele = 0, double v = 0.0)
        {
            Edge e = new Edge(node[start], node[end], edge.Count, ele,v);

            this.node[start].addConnectEdge(e.getID());
            this.node[end].addConnectEdge(e.getID());

            this.node[start].AddConnectNode(end);
            this.node[end].AddConnectNode(start);

            edge.Add(e);
        }

        public void setPosition()
        {
            for (int i = 0; i < node.Count; i++)
            {
                int x = (int)(200 * Math.Cos(i * 2 * Math.PI / node.Count - Math.PI / 2));
                int y = (int)(200 * Math.Sin(i * 2 * Math.PI / node.Count - Math.PI / 2));

                node[i].setPosition(x + 300, y + 300);
            }
        }

        public void setPosition(int i, int x, int y)
        {
            node[i].setPosition(x, y);
        }

        public int getPositionx(int i)
        {
            return node[i].getx();
        }

        public Matrix getResult()
        {
            return this.I_result;
        }

        public void showNode()
        {
            foreach (Node i in node) i.showIndex();
        }

        public void showGraph(PictureBox pb, int grid = 50)
        {
            Bitmap canvas = new Bitmap(800, 800);
            Graphics g = Graphics.FromImage(canvas);

            // Graphics オブジェクトを取得
            //Graphics g = e.Graphics;

            // 青色，太さ 2 のペンを定義
            Pen pen = new Pen(Color.Black, 2);
            Font fnt = new Font("MS UI Gothic", 20);

            showNode(g, pen, fnt);
            showEdge(g, pen, fnt);
            showArrow(g, pen);
            //showTree(g, pen);

            //foreach(Node n in this.node)
            //{
            //    Console.Write("Node");
            //    Console.WriteLine(n.getID());
            //    foreach(int i in n.getConnectNode())
            //    {
            //        Console.WriteLine(i);
            //    }
            //}


            int r = 2;

            //for (int i = 0; i < canvas.Width / grid; i++)
            //{
            //    for (int j = 0; j < canvas.Height / grid; j++)
            //    {
            //        //g.DrawLine(pen, i * grid, 0, i * grid, canvas.Height);
            //        g.DrawEllipse(pen, i * grid  -r / 2, j * grid - r / 2, r, r);
            //    }
            //}

            // ペンを破棄
            pen.Dispose();

            pb.Image = canvas;
        }

        private void showNode(Graphics g, Pen pen,Font fnt)
        {

            //setPosition();
           

            foreach (Node n in node)
            {
                g.FillEllipse(Brushes.Black, n.getx() - radius / 2, n.gety() - radius / 2, radius, radius);
                g.DrawString("n" + n.getID().ToString(), fnt, Brushes.Black, n.getx() - radius / 2, n.gety() - (int)(2 * radius));
            }

        }

        private void showEdge(Graphics g, Pen pen, Font fnt)
        {
            foreach (Edge e in edge)
            {
                //g.DrawLine(pen, e.getStartx(), e.getStarty(), e.getEndx(), e.getEndy());

                switch (e.getElement())
                {

                    case 0:
                        g.DrawLine(pen, e.getStartx(), e.getStarty(), e.getEndx(), e.getEndy());
                        g.DrawString("e" + e.getID().ToString(), fnt, Brushes.Black, (e.getStartx() + e.getEndx()) / 2 - 15, (e.getStarty() + e.getEndy()) / 2 - 15);

                        break;

                    case 1:
                        showVoltageSource(g, pen, e, fnt);
                        break;

                    case 2:
                        showRegister(g, pen, e, fnt);
                        break;

                    default:
                        break;
                }
            }
        }

        private void showTree(Graphics g, Pen pen)
        {

            Pen penr = new Pen(Color.Red, 5);
            //for (int i = 0; i < tree.Count - 1; i++)
            //{
            //    //g.DrawLine(penr, node[tree[i]].getx(), node[tree[i]].gety(), node[tree[i + 1]].getx(), node[tree[i + 1]].gety());

            //}

            foreach(int i in this.tree)
            {
                g.DrawLine(penr, this.edge[i].getStartx(), this.edge[i].getStarty(), this.edge[i].getEndx(), this.edge[i].getEndy());
            }

        }

        private void showRegister(Graphics g, Pen pen)
        {
            int width = 40;
            int height = 20;

            int xc, yc;

            int dx, dy;
            double r;

            int x1, y1;
            int x2, y2;
            int x3, y3;
            int x4, y4;

            foreach (Edge n in edge)
            {
                //g.DrawEllipse(pen, (n.getStartx() + n.getEndx()) / 2, (n.getStarty() + n.getEndy()) / 2, radius, radius);
                //g.DrawRectangle(pen, (n.getStartx() + n.getEndx() - width) / 2, (n.getStarty() + n.getEndy() - height) / 2, width, height);
                //g.DrawLine(pen, (n.getStartx() + n.getEndx()) / 2, (n.getStarty() + n.getEndy()) / 2, (n.getStartx() + n.getEndx()) / 2, (n.getStarty() + n.getEndy()) / 2);

               

                dx = -n.getStartx() + n.getEndx();
                dy = -n.getStarty() + n.getEndy();

                r = Math.Sqrt(dx * dx + dy * dy);

                if (r == 0.0) continue;

                x1 = (int)(-(dx * width + dy * height) / r);
                y1 = (int)(-(-dy * width + dx * height) / r);

                x2 = (int)((dx * width - dy * height) / r);
                y2 = (int)(-(dy * width + dx * height) / r);


                g.DrawLine(pen, x1 + (n.getStartx() + n.getEndx()) / 2, -y1 + (n.getStarty() + n.getEndy()) / 2, x2 + (n.getStartx() + n.getEndx()) / 2, -y2 + (n.getStarty() + n.getEndy()) / 2);
                g.DrawLine(pen, x2 + (n.getStartx() + n.getEndx()) / 2, -y2 + (n.getStarty() + n.getEndy()) / 2, -x1 + (n.getStartx() + n.getEndx()) / 2, y1 + (n.getStarty() + n.getEndy()) / 2);
                g.DrawLine(pen, -x1 + (n.getStartx() + n.getEndx()) / 2, y1 + (n.getStarty() + n.getEndy()) / 2, -x2 + (n.getStartx() + n.getEndx()) / 2, y2 + (n.getStarty() + n.getEndy()) / 2);
                g.DrawLine(pen, -x2 + (n.getStartx() + n.getEndx()) / 2, y2 + (n.getStarty() + n.getEndy()) / 2, x1 + (n.getStartx() + n.getEndx()) / 2, -y1 + (n.getStarty() + n.getEndy()) / 2);

                //g.DrawEllipse(pen, x2 + (n.getStartx() + n.getEndx()) / 2, y2 + (n.getStarty() + n.getEndy()) / 2, radius, radius);


            }
        }

        private void showRegister(Graphics g, Pen pen, Edge n, Font fnt)
        {
            int width = 40;
            int height = 20;

            int dx, dy;
            double r;

            int xc, yc;//中点
            int x0, y0;
            int x1, y1;
            int x2, y2;

            xc = (n.getStartx() + n.getEndx()) / 2;
            yc = (n.getStarty() + n.getEndy()) / 2;

            dx = -n.getStartx() + n.getEndx();
            dy = -n.getStarty() + n.getEndy();

            r = Math.Sqrt(dx * dx + dy * dy);
            if (r == 0.0) return;

            x0 = (int)(xc - width * dx / r);
            y0 = (int)(yc - width * dy / r);

            g.DrawLine(pen,n.getStartx(),n.getStarty(),x0,y0);

            x0 = (int)(xc + width * dx / r);
            y0 = (int)(yc + width * dy / r);

            g.DrawLine(pen, n.getEndx(), n.getEndy(), x0, y0);


            x1 = (int)(-(dx * width + dy * height) / r);
            y1 = (int)(-(-dy * width + dx * height) / r);

            x2 = (int)((dx * width - dy * height) / r);
            y2 = (int)(-(dy * width + dx * height) / r);


            g.DrawLine(pen, x1 + (n.getStartx() + n.getEndx()) / 2, -y1 + (n.getStarty() + n.getEndy()) / 2, x2 + (n.getStartx() + n.getEndx()) / 2, -y2 + (n.getStarty() + n.getEndy()) / 2);
            g.DrawLine(pen, x2 + (n.getStartx() + n.getEndx()) / 2, -y2 + (n.getStarty() + n.getEndy()) / 2, -x1 + (n.getStartx() + n.getEndx()) / 2, y1 + (n.getStarty() + n.getEndy()) / 2);
            g.DrawLine(pen, -x1 + (n.getStartx() + n.getEndx()) / 2, y1 + (n.getStarty() + n.getEndy()) / 2, -x2 + (n.getStartx() + n.getEndx()) / 2, y2 + (n.getStarty() + n.getEndy()) / 2);
            g.DrawLine(pen, -x2 + (n.getStartx() + n.getEndx()) / 2, y2 + (n.getStarty() + n.getEndy()) / 2, x1 + (n.getStartx() + n.getEndx()) / 2, -y1 + (n.getStarty() + n.getEndy()) / 2);

            g.DrawString("e" + n.getID().ToString() +"\n"+ n.getValue().ToString()+"Ω", fnt, Brushes.Black, xc-15 , yc-15);

            //g.DrawString("e" + n.getID().ToString(), fnt, Brushes.Black, xc - radius / 2, yc - radius / 2);


            //g.DrawEllipse(pen, x2 + (n.getStartx() + n.getEndx()) / 2, y2 + (n.getStarty() + n.getEndy()) / 2, radius, radius);   
        }
        private void showVoltageSource(Graphics g, Pen pen, Edge n, Font fnt)
        {
            int width = 10;
            int height = 20;

            int dx, dy;
            double r;

            int x0, y0;
            int x1, y1;
            int x2, y2;
            int x3, y3;
            int x4, y4;
            int x5, y5;

            int xc, yc;

            dx = -n.getStartx() + n.getEndx();
            dy = -n.getStarty() + n.getEndy();

            xc = (n.getStartx() + n.getEndx()) / 2;
            yc = (n.getStarty() + n.getEndy()) / 2;


            r = Math.Sqrt(dx * dx + dy * dy);
            if (r == 0.0) return;

            int r1 = 2;
            int r2 = 4;

            x0 = (int)(xc - width * dx / r);
            y0 = (int)(yc - width * dy / r);


            x1 = (int)(x0 + r1 * width * dy / r);
            y1 = (int)(y0 - r1 * width * dx / r);

            x2 = (int)(x0 - r1 * width * dy / r);
            y2 = (int)(y0 + r1 * width * dx / r);

            x3 = (int)(xc + width * dx / r);
            y3 = (int)(yc + width * dy / r);

            x4 = (int)(x3 + r2 * width * dy / r);
            y4 = (int)(y3 - r2 * width * dx / r);

            x5 = (int)(x3 - r2 * width * dy / r);
            y5 = (int)(y3 + r2 * width * dx / r);

            //g.DrawLine(pen, x1 + (n.getStartx() + n.getEndx()) / 2, -y1 + (n.getStarty() + n.getEndy()) / 2, x2 + (n.getStartx() + n.getEndx()) / 2, -y2 + (n.getStarty() + n.getEndy()) / 2);
            //g.DrawLine(pen, x2 + (n.getStartx() + n.getEndx()) / 2, -y2 + (n.getStarty() + n.getEndy()) / 2, -x1 + (n.getStartx() + n.getEndx()) / 2, y1 + (n.getStarty() + n.getEndy()) / 2);
            //g.DrawLine(pen, -x1 + (n.getStartx() + n.getEndx()) / 2, y1 + (n.getStarty() + n.getEndy()) / 2, -x2 + (n.getStartx() + n.getEndx()) / 2, y2 + (n.getStarty() + n.getEndy()) / 2);
            //g.DrawLine(pen, -x2 + (n.getStartx() + n.getEndx()) / 2, y2 + (n.getStarty() + n.getEndy()) / 2, x1 + (n.getStartx() + n.getEndx()) / 2, -y1 + (n.getStarty() + n.getEndy()) / 2);

            //g.DrawEllipse(pen, xc - radius / 2, yc - radius / 2, radius, radius);
            //g.DrawEllipse(pen, x1 - radius / 2, y1- radius / 2, radius, radius);
            //g.DrawEllipse(pen, x2 - radius / 2, y2 - radius / 2, radius, radius);
            g.DrawLine(pen, x1, y1, x2, y2);
            g.DrawLine(pen, x4, y4, x5, y5);

            g.DrawLine(pen, n.getStartx(), n.getStarty(), x0, y0);
            g.DrawLine(pen, n.getEndx(), n.getEndy(), x3, y3);

            g.DrawString("e"+n.getID().ToString()+" "+n.getValue().ToString() + "V", fnt, Brushes.Black, x1 - 15, y1 - 7);

            //g.DrawString("e" + e.getID().ToString(), fnt, Brushes.Black, (e.getStartx() + e.getEndx()) / 2 - 15, (e.getStarty() + e.getEndy()) / 2 - 15);

        }

        private void showRegister(Graphics g, Pen pen, PictureBox pb)
        {
            int width = 40;
            int height = 20;

            int x1, x2, y1, y2;

            foreach (Edge n in edge)
            {
                x1 = n.getStartx();
                y1 = n.getEndx();
                x2 = n.getStarty();
                y2 = n.getEndy();

                g.DrawEllipse(pen, (n.getStartx() + n.getEndx()) / 2, (n.getStarty() + n.getEndy()) / 2, radius, radius);
                g.TranslateTransform((n.getStartx() + n.getEndx()) / 2, (n.getStarty() + n.getEndy()) / 2);
                g.RotateTransform((float)(-180.0 * Math.Atan2((double)(y2 - y1), (double)(x2 - x1)) / Math.PI));
                g.DrawRectangle(Pens.Black, new Rectangle(0, 0, 100, 100));

            }

        }

        private void showArrow(Graphics g, Pen pen)
        {
            int theta = 25;//矢印の先端の角度[degree]
            int width = 30;//エッジと矢印の幅
            int length = 20;//矢印の先端の長さ

            double costh = Math.Cos(theta * Math.PI / 180.0);
            double sinth = Math.Sin(theta * Math.PI / 180.0);



            int xc, yc;


            int dx, dy;
            double r;

            //int x0, y0;
            int x1, y1;
            int x2, y2;
            int x3, y3;
            int x4, y4;
            int x5, y5;

            foreach (Edge e in edge)
            {
                if (e.getElement() == 1) continue;

                dx = -e.getStartx() + e.getEndx();
                dy = -e.getStarty() + e.getEndy();

                xc = (e.getStartx() + e.getEndx()) / 2;
                yc = (e.getStarty() + e.getEndy()) / 2;

                r = Math.Sqrt(dx * dx + dy * dy);
                if (r == 0.0) return;

                x1 = (int)(xc + width * dy / r);//矢印中心
                y1 = (int)(yc - width * dx / r);

                x2 = (int)(xc + width * (-dx + dy) / r);//矢印始点
                y2 = (int)(yc - width * (dx + dy) / r);

                x3 = (int)(xc + width * (dx + dy) / r);//矢印終点
                y3 = (int)(yc + width * (-dx + dy) / r);

                x4 = (int)(x3 - length * (costh * dx - sinth * dy) / r);
                y4 = (int)(y3 - length * (costh * dy + sinth * dx) / r);

                x5 = (int)(x3 - length * (costh * dx + sinth * dy) / r);
                y5 = (int)(y3 - length * (costh * dy - sinth * dx) / r);

                //g.DrawEllipse(pen, xc - radius / 2, yc - radius / 2, radius, radius);
                //g.DrawEllipse(pen, x2 - radius / 2, y2 - radius / 2, radius, radius);
                g.DrawLine(pen, x2, y2, x3, y3);
                g.DrawLine(pen, x3, y3, x4, y4);
                g.DrawLine(pen, x3, y3, x5, y5);

            }
        }

        public void DFS(int startId)
        {
            this.tree = new List<int>();
            this.cotree = new List<int>();
            this.visited = new List<int>();

            //List<int> vsd = new List<int>();
            DFSHelper(startId, -1, this.visited);


            //for (int i = 0; i < visited.Count - 1; i++)
            //{
            //    foreach (Edge e in this.edge)
            //    {
            //        if (e.getStartID() == visited[i] && e.getEndID() == visited[i + 1])
            //        {
            //            this.tree.Add(e.getID());
            //        }
            //        else if (e.getStartID() == visited[i + 1] && e.getEndID() == visited[i]) 
            //        {
            //            this.tree.Add(e.getID());
            //        }
            //    }
            //}

            bool flag;

            foreach(Edge e in this.edge)
            {
                flag = false;
                foreach(int i in this.tree)
                {
                    if (e.getID() == i) flag = true;
                }

                if (!flag) this.cotree.Add(e.getID());
            }

            //Console.WriteLine("visited");
            //foreach(int i in this.visited)
            //{
            //    Console.WriteLine(i);
            //}

            //Console.WriteLine("tree");
            //foreach (int i in tree)
            //{
            //    Console.WriteLine(i);
            //}

            //Console.WriteLine("cotree");
            //foreach (int i in cotree)
            //{
            //    Console.WriteLine(i);
            //}
        }

        private void DFSHelper(int current, List<int> visited)
        {
            //currentをすでに訪れていたらreturn 
            foreach (int i in visited)
            {
                if (i == current) return;
            }

            //Console.WriteLine(current.ToString() + "is addedd");
            //Console.WriteLine(current);
            visited.Add(current);




            for (int i = 0; i < edge.Count; i++)
            {
                if (edge[i].getStartID() == current)
                {
                    Console.WriteLine("edge" + edge[i].getID().ToString() + ".startID() = current " + current.ToString());
                    DFSHelper(edge[i].getEndID(), visited);
                }
                
                if (edge[i].getEndID() == current)
                {
                    //tree.Add(edge[i].getID());
                    Console.WriteLine("edge" + edge[i].getID().ToString() + ".EndID() = current " + current.ToString());
                    DFSHelper(edge[i].getStartID(), visited);
                }

            }
        }

        private void DFSHelper(int current, int before, List<int> vsd)
        {
            //currentをすでに訪れていたらreturn 
            foreach (int i in vsd)
            {
                if (i == current) return;
            }

            //Console.WriteLine(current.ToString() + "is addedd");
            //Console.WriteLine(current);
            vsd.Add(current);

            //まだ来てないノードなら、currentとbeforeをつなぐedgeをtreeに追加
            foreach(Edge e in this.edge)
            {
                if (e.getStartID() == current && e.getEndID() == before) this.tree.Add(e.getID());
                else if (e.getStartID() == before && e.getEndID() == current) this.tree.Add(e.getID());
            }


            for (int i = 0; i < edge.Count; i++)
            {
                if (edge[i].getStartID() == current)
                {
                    //Console.WriteLine("edge" + edge[i].getID().ToString() + ".startID() = current " + current.ToString());
                    DFSHelper(edge[i].getEndID(), current, vsd);
                }

                if (edge[i].getEndID() == current)
                {
                    //tree.Add(edge[i].getID());
                    //Console.WriteLine("edge" + edge[i].getID().ToString() + ".EndID() = current " + current.ToString());
                    DFSHelper(edge[i].getStartID(), current, vsd);

                }
            }

        }

        public void LoopDetecter()
        {
            this.loop = new List<List<int>>();
            //Console.WriteLine("------------ loop " + loop.Count.ToString());

            List<int> temp;

            List<int> waste = new List<int>();

            List<bool> seen;
            List<bool> finished ;

            foreach(int i in cotree)
            {
                seen = new List<bool>();
                finished = new List<bool>();
                foreach (Node n in this.node)
                {
                    seen.Add(false);
                    finished.Add(false);
                }
                temp = new List<int>();
                temp.Add(this.edge[i].getStartID());
                Console.WriteLine();
                LDHelper2(this.edge[i].getEndID(), temp[0], temp, seen, finished, 0);
                loop.Add(temp);

            }

            Console.WriteLine("\nloop");
            foreach (List<int> l in loop)
            {
                foreach (int i in l)
                {
                    Console.Write(i);
                    Console.Write(",");
                }
                Console.WriteLine("\n");
            }


            I_result = ce.solve(this.node, this.edge, this.loop);

        }

        private int LDHelper(int current,int before, List<int> vsd, int depth)
        {

            if (current == vsd[0])
            {
                Console.WriteLine("end");
                return 1;
            }

            foreach (int i in vsd)
            {
                Console.Write(depth);
                Console.WriteLine(" : already");
                if (current == i) return 0;
            }

            vsd.Add(current);

            bool flag = false;

            //foreach(int i in vsd)
            //{
            //    //if()
            //}

            //if (!flag)
            //{
            //    vsd.RemoveAt(vsd.Count - 1);
            //    return 0;
            //}

            foreach (int i in this.tree)
            {

                if (this.edge[i].getStartID() == current)
                {
                    if (this.edge[i].getEndID() != before) flag = true;
                    Console.Write(depth);
                    Console.WriteLine(" : edge" + edge[i].getID().ToString() + ".StartID() = current " + current.ToString());
                    if (LDHelper(this.edge[i].getEndID(), current, vsd, depth + 1) == 1) return 1;
                }

                if (this.edge[i].getEndID() == current)
                {
                    if (this.edge[i].getStartID() != before) flag = true;
                    Console.Write(depth);
                    Console.WriteLine(" : edge" + edge[i].getID().ToString() + ".EndID() = current " + current.ToString());
                    if (LDHelper(this.edge[i].getStartID(), current, vsd, depth + 1) == 1) return 1;
                }
            }


            return 0;

        }

        private int LDHelper(int current, int before,int depth, List<int> vsd, List<int> waste)
        {

            if (current == vsd[0])
            {
                Console.WriteLine("end");
                return 1;
            }

            //int count = 0;

            //foreach(int i in this.node[current].getConnectEdge())
            //{
            //    if (!this.tree.Contains(i)) continue;

            //    if (this.edge[i].getStartID() == current)
            //    {
            //        if ( !waste.Contains(this.edge[i].getEndID()))
            //        {
            //            count++;
            //        }
            //    }

            //    if (this.edge[i].getEndID() == current)
            //    {
            //        if (!waste.Contains(this.edge[i].getStartID()))
            //        {
            //            count++;
            //        }
            //    }


            //}

            //if (count == 1)
            //{
            //    waste.Add(current);
            //    //vsd.RemoveAt(vsd.Count - 1);
            //    Console.Write(current);
            //    Console.WriteLine(" ここで切って良いですか?");
            //    return 0;
            //}

            //int count = 0;

            //foreach (int e in this.node[current].getConnectEdge())
            //{
            //    if (!this.tree.Contains(e)) continue;

            //    if (this.edge[e].getStartID() == current)
            //    {
            //        if (!vsd.Contains(this.edge[e].getEndID()) || this.edge[e].getEndID() == vsd[0]) count++;
            //    }
            //    else if (this.edge[e].getEndID() == current)
            //    {
            //        if (!vsd.Contains(this.edge[e].getStartID()) || this.edge[e].getStartID() == vsd[0]) count++;
            //    }
            //}

            //if (count == 0)
            //{
            //    for (int k = 0; k < depth; k++)
            //    {
            //        Console.Write("   ");
            //    }
            //    Console.WriteLine("ここで" + current.ToString() + "を切って良いですか?");
            //    return 0;
            //}



            foreach (int i in vsd)
            {
                if (current == i)
                {
                    //vsd.Add(current);
                    return 0;
                }
            }


            vsd.Add(current);


            //bool flag = false;

            //foreach (int i in this.node[current].getConnectEdge())
            //{
            //    if (!this.tree.Contains(i)) continue;

            //    if (this.edge[i].getStartID() == current)
            //    {
            //        if (this.edge[i].getEndID() == vsd[0] || !vsd.Contains(this.edge[i].getEndID()))
            //        {
            //            flag = true;
            //            break;
            //        }
            //    }

            //    if (this.edge[i].getEndID() == current)
            //    {
            //        if (this.edge[i].getStartID() == vsd[0] || !vsd.Contains(this.edge[i].getStartID()))
            //        {
            //            flag = true;
            //            break;
            //        }
            //    }
            //}
            //if (!flag)
            //{
            //    waste.Add(current);
            //    //vsd.RemoveAt(vsd.Count - 1);
            //    Console.Write(current);
            //    Console.WriteLine(" ここで切って良いですか?");
            //    //return 0;
            //}



            //Console.WriteLine("\nvsd");
            //foreach (int i in vsd)
            //{
            //    Console.WriteLine(i);
            //}

            //これだとtree以外のedgeも入るのでうまくいかない
            //foreach (int c in this.node[current].getConnectNode())
            //{
            //    //if (!this.tree.Contains(c)) continue;

            //    if (!vsd.Contains(c))
            //    {
            //        flag = true;
            //        break;
            //    }

            //}




            foreach (int i in this.tree)
            {

                if (this.edge[i].getStartID() == current)
                {
                    for(int k = 0; k < depth; k++)
                    {
                        Console.Write("   ");
                    }
                    Console.WriteLine("edge" + edge[i].getID().ToString() + ".StartID() = current " + current.ToString());
                    if (LDHelper(this.edge[i].getEndID(), current, depth + 1, vsd, waste) == 1) return 1;
                }

                if (this.edge[i].getEndID() == current)
                {
                    for (int k = 0; k < depth; k++)
                    {
                        Console.Write("   ");
                    }
                    //if (this.edge[i].getStartID() != before) flag = true;
                    Console.WriteLine("edge" + edge[i].getID().ToString() + ".EndID() = current " + current.ToString());
                    if (LDHelper(this.edge[i].getStartID(), current, depth + 1, vsd, waste) == 1) return 1;
                }
            }

            Console.WriteLine("hello");
            return 0;

        }

        private void LDHelper(int current, int before, List<int> history, List<bool> seen, List<bool> finished)
        {
            seen[current] = true;
            history.Add(current);

            foreach(int e in this.node[current].getConnectEdge())
            {
                if (!this.tree.Contains(e)) continue;

                int next = (this.edge[e].getStartID() == current) ? this.edge[e].getEndID() : this.edge[e].getStartID();

                if (next == before) continue;

                if (finished[next]) continue;

                if (seen[next] && !finished[next])
                {
                    history.Add(next);
                    Console.WriteLine("LOOP");
                    return;
                }

                LDHelper(next, current, history, seen, finished);

            }
            finished[current] = true;

            history.RemoveAt(history.Count - 1);
            
        }

        private int LDHelper2(int current,int before, List<int> visited, List<bool>seen, List<bool> finished, int depth)
        {

            //CommentTab("current " + current.ToString(), depth);

            if (current == visited[0])
            {
                //CommentTab("Reached", depth);
                return 1;
            }

            seen[current] = true;
            visited.Add(current);

            foreach (int e in this.node[current].getConnectEdge())
            {
                if (!this.tree.Contains(e)) continue;

                int next = (this.edge[e].getStartID() == current) ? this.edge[e].getEndID() : this.edge[e].getStartID();

                //CommentTab("Next " + next.ToString(), depth);
                //CommentTab("Before " + before.ToString(), depth);

                if (next == before) continue;

                if (finished[next]) continue;


                for(int i = 0; i < depth; i++)
                {
                    //Console.Write("    ");
                }
                //Console.Write(current.ToString() + " => " + next.ToString());
                //Console.WriteLine(" edge[" +next.ToString() + "]");

                if (LDHelper2(next, current, visited, seen, finished, depth + 1) == 1) return 1;

            }

            finished[current] = true;
            visited.RemoveAt(visited.Count - 1);

            //CommentTab("ikidomari", depth);
            return 0;

        }

        private void CommentTab(string s, int depth)
        {
            for (int i = 0; i < depth; i++)
            {
                Console.Write("    ");
            }
            Console.WriteLine(s);
        }

        public int posToindex(int x, int y)
        {

            int j = -1;

            for (int i = 0; i < node.Count; i++)
            {

                if (Math.Abs(node[i].getx() - x) > radius) continue;
                if (Math.Abs(node[i].gety() - y) > radius) continue;

                j = i;

            }

            return j;
        }

        public int getNodeCount()
        {
            return node.Count;
        }


    }

}
