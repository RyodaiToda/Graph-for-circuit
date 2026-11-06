using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graph
{
    public class Circuit
    {
        private Graph g;

        public void addNode()
        {
            g.addNode();
        }

        public void addEdge(int start, int end, int ele=0)
        {
            g.addEdge(start, end, ele);
        }

        public void DFS(int start = 0)
        {
            g.DFS(start);
        }

        public void showGraph(PictureBox pb)
        {
            g.showGraph(pb);
        }

    }
    }
