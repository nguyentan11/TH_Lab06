using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab06_Bai02
{
    class Program
    {
        class Matrix
        {
            public int Row { get; set; }
            public int Column { get; set; }
            double[,] arr;
            Matrix() { }
            public Matrix(int row, int column)
            {
                Row = row;
                Column = column;
                arr = new double[row, column];
            }
            public double[] GetColumn(int i)
            {
                double[] res = new double[Row];
                for (int j = 0; j < Row; j++)
                    res[j] = arr[j, i];
                return res;
            }
            public double[] GetRow(int i)
            {
                double[] res = new double[Column];
                for (int j = 0; j < Column; j++)
                    res[j] = arr[i, j];
                return res;
            }
            public double this[int i, int j]
            {
                get { return arr[i, j]; }
                set { arr[i, j] = value; }
            }
            public Matrix RandomValues()
            {
                Random rnd = new Random();
                for (int i = 0; i < Row; i++)
                    for (int j = 0; j < Column; j++)
                        arr[i, j] = rnd.Next(10);
                return this;
            }

            public void Print()
            {
                for (int i = 0; i < Row; i++)
                {
                    for (int j = 0; j < Column; j++)
                        Console.Write(arr[i, j] + " ");
                    Console.WriteLine();
                }
            }

            public static Matrix operator *(Matrix a, Matrix b)
            {
                Matrix result = new Matrix(a.Row, b.Column);
                List<Thread> threads = new List<Thread>();
                for (int i = 0; i < a.Row * b.Column; i++)
                {
                    int tempi = i;
                    Thread thread = new Thread(() => VectorMult(tempi, a, b, result));
                    thread.Start();
                    threads.Add(thread);
                }
                foreach (Thread t in threads)
                    t.Join();
                return result;
            }

            public static void VectorMult(int tmp, Matrix a, Matrix b, Matrix result)
            {
                int i = tmp / b.Column;
                int j = tmp % b.Column;
                double[] x = a.GetRow(i);
                double[] y = b.GetColumn(j);
                for (int k = 0; k < x.Length; k++)
                    result[i, j] += x[k] * y[k];
                Console.WriteLine("Calculate element{0}{1}", i, j);
            }
        }
        static void Main(string[] args)
        {
            Console.Write("Nhap a: ");
            int a = int.Parse(Console.ReadLine());
            Console.Write("Nhap b: ");
            int b = int.Parse(Console.ReadLine());
            Console.Write("Nhap c: ");
            int c = int.Parse(Console.ReadLine());
            Matrix A = new Matrix(a, b).RandomValues();
            Matrix B = new Matrix(b, c).RandomValues();
            A.Print();
            Console.WriteLine(new String('-', 20));
            B.Print();
            Console.WriteLine(new String('-', 20));
            Matrix C = A * B;
            C.Print();
        }
    }
}
