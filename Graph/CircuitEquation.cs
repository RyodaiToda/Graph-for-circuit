using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    public class CircuitEquation
    {
        private Matrix B;   //接続行列
        private Matrix Z;   //インピーダンス行列
        private Matrix I;   //電流ベクトル
        private Matrix V;   //電圧ベクトル
        private Matrix Il;  //リンク電流
        private Matrix Vs;
           
        public CircuitEquation()
        {

        }

        public CircuitEquation(List<Node> node, List<Edge> edge, List<int> loop) {
            int n = node.Count;
            int b = edge.Count;
            int l = loop.Count;

            B = new Matrix(l, b);
            Z = new Matrix(b, b);
            I = new Matrix(b, 1);
            V = new Matrix(b, 1);

        }

        public Matrix solve(List<Node> node, List<Edge> edge, List<List<int>> loop)
        {

            setZ(edge);
            setB(node, edge, loop);
            LUsolve();


            return I;
        }

        private void setZ(List<Edge> edge)
        {
            int b = edge.Count;
            Z = new Matrix(b,b);

            for (int i = 0; i < b; i++) 
            {
                if (edge[i].getElement() == 2) Z.setElement(i, i, edge[i].getValue());
            }

            //Console.WriteLine("\nZ");
            //Z.showMatrix();
        }

        private void setB(List<Node> node, List<Edge> edge, List<List<int>> loop)
        {
            B = new Matrix(loop.Count, edge.Count);
            V = new Matrix(loop.Count, 1);

            for (int i = 0; i < B.getRow(); i++)
            {
                for (int j = 0; j < loop[i].Count; j++)
                {

                    foreach (Edge e in edge)
                    {
                        if (e.getStartID() == loop[i][j] && e.getEndID() == loop[i][(j + 1) % (loop[i].Count)])
                        {
                            B.setElement(i, e.getID(), 1.0);
                            if (e.getElement() == 1) V.setElement(i, 0, V.getElement(i, 0) + e.getValue());
                            break;
                        }
                        else if (e.getEndID() == loop[i][j] && e.getStartID() == loop[i][(j + 1) % (loop[i].Count)])
                        {
                            B.setElement(i, e.getID(), -1.0);
                            if (e.getElement() == 1) V.setElement(i, 0, V.getElement(i, 0) - e.getValue());

                            break;
                        }
                    }
                }

            }
            //Console.WriteLine("\nB");
            //B.showMatrix();

            Console.WriteLine("\nV");
            V.showMatrix();


        }

        private void LUsolve()
        {
            Il = new Matrix(V.getRow(), 1);
            Matrix temp = new Matrix(V.getRow(), 1);
            Matrix BZBT = Matrix.Times(B, Z);
            BZBT = Matrix.Times(BZBT, Matrix.TransPose(B));

            var LU = Matrix.LU(BZBT);

            Matrix L = LU.L;
            Matrix U = LU.U;

            //Console.WriteLine("\nU");
            //U.showMatrix();

            //Matrix m = Matrix.Inverse(BZBT);

            double sum;
            for (int i = 0; i < temp.getRow(); i++)
            {
                sum = 0.0;
                for (int j = 0; j < i; j++)  
                {
                    sum += L.getElement(i, j) * temp.getElement(j, 0);
                }
                temp.setElement(i, 0, (V.getElement(i, 0) - sum) );
            }

            for(int i = Il.getRow() - 1; i >= 0; i--)
            {
                sum = 0.0;
                for (int j = i + 1; j < Il.getRow(); j++)
                {
                    sum += U.getElement(i, j) * Il.getElement(j, 0);
                }
                Il.setElement(i, 0, (temp.getElement(i, 0) - sum) / U.getElement(i, i));
            }

            Console.WriteLine("\n----------Il----------");
            Il.showMatrix();

            Matrix mi = Matrix.Times(Matrix.TransPose(B), Il);

            //Console.WriteLine("\\n----------I----------\"");
            //mi.showMatrix();

            I = new Matrix(Z.getRow(), 1);

            for(int i = 0; i < I.getRow(); i++)
            {
                I.setElement(i, 0, mi.getElement(i, 0));
            }

            Console.WriteLine("\\n----------I----------\"");
            I.showMatrix();
        }

        public Matrix getB()
        {
            return this.B;
        }

        public Matrix getZ()
        {
            return this.Z;
        }

        public Matrix getBZBT()
        {
            Matrix m1 = Matrix.Times(B, Z);
            m1 = Matrix.Times(m1, Matrix.TransPose(B));

            Matrix m = Matrix.Inverse(m1);
            Console.WriteLine("\nLU");
            //m.showMatrix();
            return m1;
        }

    }
}
