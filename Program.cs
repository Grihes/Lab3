using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab3
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
        public static double[,] CreateArray1()
        {
            Random rnd = new Random();
            double[,] array = new double[10, 10];
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (i == 0 || j == 0 || i == 9 || j == 9)
                        array[i, j] = 10;
                    else
                        array[i, j] = rnd.Next(-10, 10) * 0.05 + 10;
                }
            }
            return array;
        }
        public static double[,] CreateArray2()
        {
            Random rnd = new Random();
            double[,] array = new double[10, 10];
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (i == 0 || j == 0 || i == 9 || j == 9)
                        array[i, j] = 10;
                    else
                    array[i, j] = 0;
                }
            }
            array[4, 4] = 4;
            return array;
        }
        public static double[,] CreateArray3()
        {
            Random rnd = new Random();
            double[,] array = new double[10, 10];
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (i == 0 || i == 9)
                        array[i, j] = 10;
                    else if (j == 0 || j == 9)
                        array[i, j] = 5;
                    else array[i, j] = 0;
                }
            }
            array[4, 4] = 4;
            return array;
        }
        public static int FindCount (double[,] array)
        {
            double[,] arr1 = new double[10, 10];
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                    arr1[i, j] = array[i, j];
            }
            int count = 0;
            int k = 0;
            while (k != 64)
            {
                k = 0;
                arr1 = GetAvaragedArray(arr1);
                for (int i = 1; i < 9; i++)
                {
                    for (int j = 1; j < 9; j++)
                    {
                        if ((Math.Abs(10 - arr1[i, j])) / 10 <= 0.01)
                            k += 1;
                    }
                }
                count += 1;
            }
            return count;
        }
        public static double[,] AvarageArray(double[,] array)
        {
            int c = 150; // Количество итераций, меняю прямо в программе для нужного задания
            double[,] arr2 = new double[10, 10];
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                  arr2[i, j] = array[i, j];
            }
            for (int i = 0; i < c; i++)
                arr2 = GetAvaragedArray(arr2);
            return arr2;
        }
        public static double[,] GetAvaragedArray(double[,] array)
        {
            double[,] arr = new double[10, 10];
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (i == 0 || j == 0 || i == 9 || j == 9)
                        arr[i, j] = array[i,j];
                    else
                        arr[i, j] = 0.25 * (array[i + 1, j] + array[i - 1, j] + array[i, j + 1] + array[i, j - 1]);
                }
            }
            return arr;
        }
    }
}
