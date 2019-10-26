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
        private readonly int _centroidsNumber;
        private readonly Centroid[] _centroids;
        private const double TOLERANCE = 0.000001d;
        private const int MAX_RUNS = 20;
        private StreamWriter sw;

        public PredictZones()
        {
            _random = new Random();
            //centroidNumber = random.Next(2, 11);
            _centroidsNumber = 5;
            _centroids = new Centroid[_centroidsNumber];
            File.Delete(Directory.GetCurrentDirectory() + @"\prediceted_points.txt");
            sw =
                new StreamWriter(Directory.GetCurrentDirectory() + @"\prediceted_points.txt", true);
        }

        public void Predict()
        {
            double evaluation = double.MaxValue;
            int run = 1;
            for (int i = 0; i < _centroidsNumber; i++)
            {
                _centroids[i] = new Centroid(_random.Next(-400, 401), _random.Next(-400, 401), "Centroid" + (i+1));
            }

            List<Point> points = GetPoints();
            int ct = 1;
            while (ct++ <= MAX_RUNS)
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
                }

                double newEvaluation = ComputeEvaluation();
                Console.WriteLine(newEvaluation);
                WriteToFile(run, newEvaluation);
                if (Math.Abs(evaluation - newEvaluation) < TOLERANCE)
                {
                    break;
                }

                evaluation = newEvaluation;
                run++;
                RemovePointsFromCentroids();
            }
            sw.Close();
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
                    sw.WriteLine(assignedPoint.X + "," + 
                                 assignedPoint.Y + "," + centroid.Name + "," + run + "," + evaluation);
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
