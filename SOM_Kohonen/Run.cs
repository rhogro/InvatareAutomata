using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOM_Kohonen
{
    public class Run
    {
        public int Index { get; set; }
        public double Alpha { get; set; }
        public double Vecinatate { get; set; }
        public Point[,] Position { get; set; }

        public Run(int index, double alpha, double vecinatate, Point[,] position)
        {
            Index = index;
            Alpha = alpha;
            Vecinatate = vecinatate;
            int positionLength = (int)Math.Sqrt(position.Length);
            Position = new Point[positionLength, positionLength];
            for (int i = 0; i < positionLength; i++)
            {
                for (int j = 0; j < positionLength; j++)
                {
                    Position[i, j] = new Point(position[i, j].X, position[i, j].Y);
                }
            }
        }
    }
}
