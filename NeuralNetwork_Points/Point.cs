using System;
using System.Collections.Generic;
using System.Text;

namespace NeuralNetwork_Points
{
    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }
        public string Zone { get; set; }
        public int[] Result{ get; set; } //


        public Point(int x, int y, string zone)
        {
            X = (double)x / 400;
            Y = (double)y / 400;
            Zone = zone;

            Result = new int[4]; // 4 zone
            if(string.Equals(Zone, "Zone1"))
            {
                Result[0] = 1;
                Result[1] = 0;
                Result[2] = 0;
                Result[3] = 0;
            }
            if (string.Equals(Zone, "Zone2"))
            {
                Result[0] = 0;
                Result[1] = 1;
                Result[2] = 0;
                Result[3] = 0;
            }
            if (string.Equals(Zone, "Zone3"))
            {
                Result[0] = 0;
                Result[1] = 0;
                Result[2] = 1;
                Result[3] = 0;
            }
            if (string.Equals(Zone, "Zone4"))
            {
                Result[0] = 0;
                Result[1] = 0;
                Result[2] = 0;
                Result[3] = 1;
            }
        }
    }
}
