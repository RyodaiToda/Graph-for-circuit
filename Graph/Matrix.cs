using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    public class Matrix
    {

        private int col;    //列
        private int row;    //行

        private double[,] matrix;
        public Matrix(int r, int c)
        {
            this.matrix = new double[r, c];
            col = c;
            row = r;

            for(int i = 0; i < r; i++)
            {
                for(int j = 0; j < c; j++)
                {
                    this.matrix[i, j] = 0.0;
                }
            }

            //Console.WriteLine(this.col.ToString());
            //Console.WriteLine(this.row.ToString());

        }

        public int getCol()
        {
            return this.col;
        }

        public int getRow()
        {
            return this.row;
        }

        public void setElement(int i, int j, double x)
        {
            this.matrix[i, j] = x;
        }

        public double getElement(int i, int j)
        {
            return this.matrix[i, j];
        }

        public void showMatrix()
        {
            Console.WriteLine();
            for (int i = 0; i < this.row; i++)
            {
                for (int j = 0; j < this.col; j++)
                {
                    Console.Write(this.matrix[i, j].ToString() + ",   ");
                }
                Console.WriteLine();
            }
        }

        public static Matrix Add(Matrix m1, Matrix m2)
        {
            if (m1.getCol() != m2.getCol() || m1.getRow() != m2.getRow())
            {
                Console.WriteLine("error : sizes are diferrent");
                return new Matrix(m1.getRow(), m1.getCol());
            }

            Matrix mout = new Matrix(m1.getRow(), m1.getCol());

            for(int i = 0; i < m1.getRow(); i++)
            {
                for(int j = 0; j < m1.getCol(); j++)
                {
                    mout.setElement(i, j, m1.getElement(i, j) + m2.getElement(i, j));
                }
            }

            return mout;
        }

        public static Matrix TransPose(Matrix m)
        {
            Matrix mout = new Matrix(m.getCol(), m.getRow());

            for(int i = 0; i < mout.getRow(); i++)
            {
                for (int j = 0; j < mout.getCol(); j++) 
                {
                    mout.setElement(i, j, m.getElement(j, i));
                }
            }


            return mout;
        }

        public static Matrix Times(Matrix m1, Matrix m2)
        {

            if (m1.getCol() != m2.getRow())
            {
                Console.WriteLine("error : size is diferrent");
                return m1;
            }

            Matrix mout = new Matrix(m1.getRow(), m2.getCol());

            double sum;

            for(int i = 0; i < mout.getRow(); i++)
            {
                for (int j = 0; j < mout.getCol(); j++) 
                {
                    sum = 0.0;

                    for (int k = 0; k < m1.getCol(); k++)
                    {
                        sum += m1.getElement(i, k) * m2.getElement(k, j);
                    }

                    mout.setElement(i, j, sum);
                }
            }


            return mout;

        }

        public static Matrix Inverse(Matrix m)
        {
            if(m.getCol() != m.getRow())
            {
                Console.WriteLine("error this is no a square matrix");
                return m;
            }

            Matrix mout = new Matrix(m.getRow(), m.getCol());

            Matrix L = new Matrix(m.getRow(), m.getCol());
            Matrix U = new Matrix(m.getRow(), m.getCol());

            for(int i = 0; i < L.getRow(); i++)
            {
                L.setElement(i, i, 1.0);
            }

            double sum;

            for(int j = 0; j < m.getRow(); j++)
            {
                
                
                for(int i = 0; i <= j; i++)
                {
                    

                    if (i == 0) U.setElement(i, j, m.getElement(i, j));
                    else
                    {
                        sum = 0.0;
                        for(int k = 0; k <= i - 1; k++)
                        {
                            sum += L.getElement(i, k) * U.getElement(k, j);
                        }
                        U.setElement(i, j, m.getElement(i, j) - sum);

                    }

                    //Console.WriteLine(i.ToString() + j.ToString());
                }


                //if (j == 0) continue;
                if (j == m.getRow() - 1) break;

                for (int i = j + 1; i < m.getRow(); i++)
                {

                    if (j == 0)
                    {
                        L.setElement(i, j, m.getElement(i, j) / U.getElement(j, j));

                    }
                    else
                    {
                        sum = 0.0;
                        for (int k = 0; k <= j - 1; k++)
                        {
                            sum += L.getElement(i, k) * U.getElement(k, j);
                        }
                        L.setElement(i, j, (m.getElement(i, j) - sum) / U.getElement(j, j));
                    }


                }
            }



            Console.WriteLine("L");
            L.showMatrix();
            Console.WriteLine("U");
            U.showMatrix();

            Matrix LU = Matrix.Times(L, U);
            Console.WriteLine("LU");
            LU.showMatrix();

            return mout;
        }

        public static (Matrix L, Matrix U) LU(Matrix m)
        {
            if (m.getCol() != m.getRow())
            {
                Console.WriteLine("error this is no a square matrix");
                return (m, m);
            }

            Matrix mout = new Matrix(m.getRow(), m.getCol());

            Matrix L = new Matrix(m.getRow(), m.getCol());
            Matrix U = new Matrix(m.getRow(), m.getCol());

            for (int i = 0; i < L.getRow(); i++)
            {
                L.setElement(i, i, 1.0);
            }

            double sum;

            for (int j = 0; j < m.getRow(); j++)
            {


                for (int i = 0; i <= j; i++)
                {


                    if (i == 0) U.setElement(i, j, m.getElement(i, j));
                    else
                    {
                        sum = 0.0;
                        for (int k = 0; k <= i - 1; k++)
                        {
                            sum += L.getElement(i, k) * U.getElement(k, j);
                        }
                        U.setElement(i, j, m.getElement(i, j) - sum);

                    }

                    //Console.WriteLine(i.ToString() + j.ToString());
                }


                //if (j == 0) continue;
                if (j == m.getRow() - 1) break;

                for (int i = j + 1; i < m.getRow(); i++)
                {

                    if (j == 0)
                    {
                        L.setElement(i, j, m.getElement(i, j) / U.getElement(j, j));

                    }
                    else
                    {
                        sum = 0.0;
                        for (int k = 0; k <= j - 1; k++)
                        {
                            sum += L.getElement(i, k) * U.getElement(k, j);
                        }
                        L.setElement(i, j, (m.getElement(i, j) - sum) / U.getElement(j, j));
                    }


                }
            }



            //Console.WriteLine("L");
            //L.showMatrix();
            //Console.WriteLine("U");
            //U.showMatrix();

            //Matrix LU = Matrix.Times(L, U);
            //Console.WriteLine("LU");
            //LU.showMatrix();

            return (L, U);
        }

    }
}
