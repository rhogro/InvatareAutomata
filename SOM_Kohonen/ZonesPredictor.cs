using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOM_Kohonen
{ 
    public class ZonesPredictor
    {
        private const int SIZE = 10;
        public Point[,] W = new Point[SIZE, SIZE];
        private readonly StreamWriter _sw;

        public ZonesPredictor()
        {
            Console.WriteLine("size:" + W.Length);
            //File.Delete(Directory.GetCurrentDirectory() + @"\neurons.txt");
            //_sw = new StreamWriter(Directory.GetCurrentDirectory() + @"\neurons.txt", true);
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    W[i, j] = new Point(-400 + (800 / SIZE) * j + 400/SIZE, 400 - (800 / SIZE) * i - 400/SIZE);
                }
            }
        }
        public void Predict()
        {
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    Console.Write(W[i,j]+" ");
                }

                Console.WriteLine();
            }
        }
    }
}
