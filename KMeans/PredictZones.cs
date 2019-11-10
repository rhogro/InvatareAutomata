using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMeans
{
    public class PredictZones
    {
        private readonly Random _random;
        private const int CentroidsNumber = 5;
        private readonly Centroid[] _centroids;
        private const double Tolerance = 0.000001d;
        private const int MaxRuns = 20;
        private readonly StreamWriter _sw;

        public PredictZones()
        {
            _random = new Random();
            //centroidNumber = random.Next(2, 11);
            _centroids = new Centroid[CentroidsNumber];
            File.Delete(Directory.GetCurrentDirectory() + @"\prediceted_points.txt");
            _sw =
                new StreamWriter(Directory.GetCurrentDirectory() + @"\prediceted_points.txt", true);
        }

        public void Predict()
        {
            double evaluation = double.MaxValue;
            int run = 1;
            for (int i = 0; i < CentroidsNumber; i++)
            {
                _centroids[i] = new Centroid(_random.Next(-400, 401), _random.Next(-400, 401), "Centroid" + (i + 1));
            }

            List<Point> points = GetPoints();
            int ct = 1;
            while (ct++ <= MaxRuns)
            {
                foreach (var point in points)
                {
                    AssignPointToCentroid(point);
                }

                foreach (var centroid in _centroids)
                {
                    if (centroid.AssignedPoints.Count > 0)
                    {
                        MoveToCenterOfItsPoints(centroid);
                    }
                    else
                    {
                        //centroid.Center = new Point(_random.Next(-400, 401), _random.Next(-401));
                    }
                }

                double newEvaluation = ComputeEvaluation();
                WriteToFile(run, newEvaluation);
                if (Math.Abs(evaluation - newEvaluation) < Tolerance)
                {
                    foreach (var centroid in _centroids)
                    {
                        Console.WriteLine("{0} - x: {1}, y: {2}", centroid.Name, centroid.Center.X, centroid.Center.Y);
                    }
                    break;
                }

                evaluation = newEvaluation;
                run++;
                RemovePointsFromCentroids();
            }
            _sw.Close();
        }

        private void RemovePointsFromCentroids()
        {
            foreach (var centroid in _centroids)
            {
                centroid.AssignedPoints.Clear();
            }
        }

        private void WriteToFile(int run, double evaluation)
        {
            foreach (var centroid in _centroids)
            {
                foreach (var assignedPoint in centroid.AssignedPoints)
                {
                    _sw.WriteLine(assignedPoint.X + "," +
                                 assignedPoint.Y + "," + centroid.Name + 
                                 "," + centroid.Center.X + "," + centroid.Center.Y + 
                                 "," + run + "," + evaluation);
                }
            }

        }

        private double ComputeEvaluation()
        {
            double totalDistance = 0;
            foreach (var centroid in _centroids)
            {
                foreach (var assignedPoint in centroid.AssignedPoints)
                {
                    totalDistance += ComputeDistance(centroid.Center, assignedPoint);
                }
            }

            return totalDistance;
        }

        private void MoveToCenterOfItsPoints(Centroid centroid)
        {
            int sumX = 0;
            int sumY = 0;
            foreach (var point in centroid.AssignedPoints)
            {
                sumX += point.X;
                sumY += point.Y;
            }

            int averageX = sumX / centroid.AssignedPoints.Count;
            int averageY = sumY / centroid.AssignedPoints.Count;

            centroid.Center = new Point(averageX, averageY);
        }

        private void AssignPointToCentroid(Point point)
        {
            double minDistance = double.MaxValue;
            Centroid minCentroid = _centroids[0];
            foreach (var centroid in _centroids)
            {
                double distance = ComputeDistance(centroid.Center, point);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    minCentroid = centroid;
                }
            }
            minCentroid.AssignedPoints.Add(point);
        }

        private double ComputeDistance(Point centroidCenter, Point point)
        {
            //euclidean distance
            return (Math.Sqrt(Math.Pow(Math.Abs(centroidCenter.X - point.X), 2) + Math.Pow(Math.Abs(centroidCenter.Y - point.Y), 2)));

            //manhattan distance
            //return (Math.Abs(centroidCenter.X - point.X) + Math.Abs(centroidCenter.Y - point.Y));
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

        public void AssignAllPoints()
        {
            List<Point> allPoints = new List<Point>();
            for (int i = (-400); i <= 400; i += 2)
            {
                for (int j = (-400); j <= 400; j += 2)
                {
                    allPoints.Add(new Point(i, j));
                }
            }

            foreach (Point point in allPoints)
            {
                AssignPointToCentroid(point);
            }

            File.Delete(Directory.GetCurrentDirectory() + @"\all_points.txt");

            StreamWriter sw =
                new StreamWriter(Directory.GetCurrentDirectory() + @"\all_points.txt", true);

            foreach (var centroid in _centroids)
            {
                foreach (var assignedPoint in centroid.AssignedPoints)
                {
                    sw.WriteLine(assignedPoint.X + "," + assignedPoint.Y + 
                                  "," + centroid.Name + "," + centroid.Center.X + "," + centroid.Center.Y + ",1,0");
                }
            }
            sw.Close();
        }

    }
}
