using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    public class Edge
    {
        private Node start; //スタートのNode
        private Node end;   //エンドのNode

        private int element;    //短絡0 電圧源1 抵抗2 コンデンサ3 コイル4
        private int index;

        private double value;//物理量　抵抗なら抵抗値

        public Edge(Node s, Node e, int id, int ele = 0, double v = 0.0)
        {
            this.start = s;
            this.end = e;
            this.element = ele;
            this.index = id;
            this.value = v;

            //Console.WriteLine(ele.ToString());
        }

        public int getStartID()
        {
            return start.getID();
        }

        public int getEndID()
        {
            return end.getID();
        }


        public int getStartx()
        {
            return start.getx();
        }

        public int getStarty()
        {
            return start.gety();
        }

        public int getEndx()
        {
            return end.getx();
        }
        public int getEndy()
        {
            return end.gety();
        }

        public int getElement()
        {
            return element;
        }

        public int getID()
        {
            return index;
        }

        public double getValue()
        {
            return value;
        }

    }
}
