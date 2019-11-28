using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOM_Kohonen
{
    struct Position
    {
        public int I;
        public int J;
    }

    public class Run
    {
        public int Index { get; set; }
        public double Alpha { get; set; }
        public double Vecinatate { get; set; }
        public PointDouble[,] Position { get; set; }

        public Run(int index, double alpha, double vecinatate, PointDouble[,] position)
        {
            Index = index;
            Alpha = alpha;
            Vecinatate = vecinatate;
            int positionLength = (int) Math.Sqrt(position.Length);
            Position = new PointDouble[positionLength, positionLength];
            for (int i = 0; i < positionLength; i++)
            {
                for (int j = 0; j < positionLength; j++)
                {
                    Position[i, j] = new PointDouble(position[i, j].X, position[i, j].Y);
                }
            }
        }
    }

    public class PointDouble
    {
        public double X { get; set; }
        public double Y { get; set; }

        public PointDouble(double x, double y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return "[" + X + ", " + Y + "]";
        }
    }
    public class ZonesPredictor
    {
        private const int SIZE = 10;
        public PointDouble[,] W = new PointDouble[SIZE, SIZE];
        private readonly StreamWriter _sw;
        private double Alpha;
        private double Vecinatate;
        private const int RUNS = 10;
        public List<Run> Runs;

        public ZonesPredictor()
        {
            Console.WriteLine("size:" + W.Length);
            File.Delete(Directory.GetCurrentDirectory() + @"\neurons.txt");
            _sw = new StreamWriter(Directory.GetCurrentDirectory() + @"\neurons.txt", true);
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    W[i, j] = new PointDouble(-400 + (800 / SIZE) * j + 400 / SIZE, 400 - (800 / SIZE) * i - 400 / SIZE);
                }
            }

            Alpha = 1;
            Vecinatate = 1;
            Runs = new List<Run>();
            Runs.Add(new Run(0, 0, 0, W));
            //WriteToFile(0);
            //_sw.Close();
        }
        public void Predict()
        {
            List<Point> points = GetPoints();
            double run = 0;
            while(Alpha > 0.001)
            {
                Vecinatate = 7 * Math.Pow(Math.E, -1 * (run / RUNS));
                Alpha = 0.7 * Math.Pow(Math.E, -1 * (run / RUNS));

                Console.WriteLine("Run {0} \n", run);
                //for (int i = 0; i < SIZE; i++)
                //{
                //    for (int j = 0; j < SIZE; j++)
                //    {
                //        Console.Write(W[i, j] + " ");
                //    }

                //    Console.WriteLine();
                //}

                Console.WriteLine("\n\n");

                foreach (var point in points)
                {
                    Position WinnerPosition = GetWinner(point);
                    PointDouble winner = W[WinnerPosition.I, WinnerPosition.J];
                    MoveWTowardsPoint(winner, point);
                    MoveNeighboursTowardsPoint(WinnerPosition, point);
                }
                Runs.Add(new Run((int)run, Alpha, Vecinatate, W));
                //WriteToFile((int)run);
                run++;
            }
            
            _sw.Close();
        }

        private void MoveNeighboursTowardsPoint(Position pos, Point point)
        {

            //int rowStart = (int)Math.Max(pos.I - Vecinatate, 0);
            //int rowFinish = (int)Math.Min(pos.I + Vecinatate, SIZE - 1);
            //int colStart = (int)Math.Max(pos.J - Vecinatate, 0);
            //int colFinish = (int)Math.Min(pos.J + Vecinatate, SIZE - 1);

            //for (int curRow = rowStart; curRow <= rowFinish; curRow++)
            //{
            //    for (int curCol = colStart; curCol <= colFinish; curCol++)
            //    {
            //        MoveWTowardsPoint(W[curCol, curRow], point);
            //    }
            //}

            //TODO
            for (int i = 1; i <= (int)Vecinatate; i++)
            {
                if ((pos.I - i) >= 0)
                    MoveWTowardsPoint(W[(int)(pos.I - i), pos.J], point);
                if ((int)(pos.J - i) >= 0)
                    MoveWTowardsPoint(W[pos.I, (int)(pos.J - i)], point);

                if ((int)(pos.I + i) < SIZE)
                    MoveWTowardsPoint(W[(int)(pos.I + i), pos.J], point);
                if ((int)(pos.J + i) < SIZE)
                    MoveWTowardsPoint(W[pos.I, (int)(pos.J + i)], point);

                if ((int)(pos.I + i) < SIZE && (int)(pos.J + i) < SIZE)
                    MoveWTowardsPoint(W[(int)(pos.I + i), (int)(pos.J + i)], point);
                if ((pos.I - i) >= 0 && (int)(pos.J - i) >= 0)
                    MoveWTowardsPoint(W[(int)(pos.I - i), (int)(pos.J - i)], point);

                if ((pos.I - i) >= 0 && (int)(pos.J + i) < SIZE)
                    MoveWTowardsPoint(W[(int)(pos.I - i), (int)(pos.J + i)], point);
                if ((int)(pos.I + i) < SIZE && (int)(pos.J - i) >= 0)
                    MoveWTowardsPoint(W[(int)(pos.I + i), (int)(pos.J - i)], point);
            }

            //for (int i = (int)(pos.I - Vecinatate); i <= (int)(pos.I + Vecinatate); i++)
            //{
            //    for (int j = (int) (pos.J - Vecinatate); j <= (int) (pos.J + Vecinatate); j++)
            //    {
            //        if (i < 0 || j < 0 || i >= SIZE || j >= SIZE) continue;
            //        MoveWTowardsPoint(W[i, j], point);
            //    }
            //}
        }

        private void MoveWTowardsPoint(PointDouble winner, Point point)
        {
            winner.X = winner.X + Alpha * (point.X - winner.X);
            winner.Y = winner.Y + Alpha * (point.Y - winner.Y);
        }

        private Position GetWinner(Point point)
        {
            Position position = new Position();
            double minDistance = Double.MaxValue;
            double distance;
            for (int i = 0; i < SIZE; i++)
            {
                {
                    for (int j = 0; j < SIZE; j++)
                    {
                        distance = ComputeDistance(W[i, j], point);
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            position.I = i;
                            position.J = j;
                        }
                    }

                }
            }
            return position;
        }

        private double ComputeDistance(PointDouble W, Point point)
        {
            //euclidean distance
            return (Math.Sqrt(Math.Pow(Math.Abs(W.X - point.X), 2) + Math.Pow(Math.Abs(W.Y - point.Y), 2)));

            //manhattan distance
            //return (Math.Abs(W.X - point.X) + Math.Abs(W.Y - point.Y));
        }

        private void WriteToFile(int run)
        {
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    _sw.WriteLine(W[i, j].X + "," + W[i,j].Y+ ","+run+","+Alpha+","+Vecinatate);
                }
            }

        }

        private List<Point> GetPoints()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("X_Value", typeof(int));
            dt.Columns.Add("Y_Value", typeof(int));
            dt.Columns.Add("Zone", typeof(string));

            StreamReader sr = new StreamReader(Directory.GetCurrentDirectory() + @"\points.txt");

            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] strarr = line.Split(',');
                dt.Rows.Add(strarr[0], strarr[1], strarr[2]);
            }

            sr.Close();

            dt.Columns.Remove("Zone");
            List<Point> points = new List<Point>();

            foreach (DataRow row in dt.Rows)
            {
                points.Add(new Point((int)row["X_Value"], (int)row["Y_Value"]));
            }

            return points;
        }
    }
}
