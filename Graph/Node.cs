using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    public class Node
    {
        private int index;

        private int posx;
        private int posy;

        private List<int> connectEdge = new List<int>();//このノードに接続されているエッジのインデックスを保存
        private List<int>connectNode = new List<int>();//このノードに接続されているノードのインデックスを保存

        public Node(int n)
        {
            index = n;
        }

        public void showIndex()
        {
            //Console.WriteLine(index);
        }

        public void setPosition(int x, int y)
        {
            posx = x;
            posy = y;
        }

        public void addConnectEdge(int i)
        {
            this.connectEdge.Add(i);
        }

        public void AddConnectNode(int i)
        {
            this.connectNode.Add(i);
        }

        public List<int> getConnectEdge()
        {
            return this.connectEdge;
        }

        public List<int> getConnectNode()
        {
            return this.connectNode;
        }

        public int getx()
        {
            return posx;
        }

        public int gety()
        {
            return posy;
        }

        public int getID()
        {
            return index;
        }
    }

}
