using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Xsl;

namespace PointsGenerator
{
    public class ComputePoints
    {
        public Zone[] Zones { get; set; }
        private readonly Random _random;
        private readonly int _totalPoints = 10000;

        public ComputePoints()
        {
            Zones = new[]
            {
                new Zone(300, 150, 10, "Zone1"),
                new Zone(-200, -200, 5, "Zone2"),
                new Zone(-300, 20, 10, "Zone3"),
                new Zone(50, -200, 4, "Zone4")
            };

            _random = new Random();
        }
        public void Compute()
        {
            /* ALGORITM */
            /* 1. alegem aleator o zona
             * 2. alegem aleator x in intervalul [-400, 400]
             * 3. G(X) = e ^ -(((m-x)^2)/2*deviation^2), G(x) apartine intervalului (0, 1]
             * 4. alegem aleator p din intervalul [0, 1]
             * 5. daca G(x) > p, acceptam x, altfel se reia de la pasul 2.
             * 6. repetam pasii 2-5 pentru y
             * 7. generam un fisier care contine valorile pe x, valorile pe y si zona punctului (10 000 linii)
             */

            File.Delete(Directory.GetCurrentDirectory() + @"\points.txt");
            StreamWriter sw =
                new StreamWriter(Directory.GetCurrentDirectory() + @"\points.txt", true);

            for (int i = 1; i <= _totalPoints; i++)
            {
                int randomZoneIndex = _random.Next(0, 4);
                Zone currentZone = Zones[randomZoneIndex];
                int coordX = GenerateCoordinate(currentZone.CenterX, currentZone.Deviation);
                int coordY = GenerateCoordinate(currentZone.CenterY, currentZone.Deviation);
                WriteToFile(sw, coordX, coordY, currentZone.Name);
            }
            sw.Close();
        }

        private int GenerateCoordinate(int zoneCenterCoordinate, int deviation)
        {
            double gauss;
            double p;
            int coordinate;

            do
            {
                coordinate = _random.Next(-400, 401);
                double impartitor = Math.Pow(zoneCenterCoordinate - coordinate, 2);
                double deImpartit = 2 * Math.Pow(deviation, 2);
                double fraction = impartitor / deImpartit;
                fraction = fraction * -1;

                gauss = Math.Exp(fraction);
                do
                {
                    p = NextRandomRange();
                } while (p <= 0 && p >= 1);
            } while (gauss <= p);

            return coordinate;
        }

        private double NextRandomRange()//double minimum, double maximum)
        {
            //double r = _random.NextDouble() * (maximum - minimum) + minimum;
            //return r;
            int r = _random.Next(0, 20001);
            double ret = r / 20000.0;
            return ret;
        }

        private void WriteToFile(StreamWriter sw, int coordX, int coordY, string currentZoneName)
        {
            sw.WriteLine(coordX.ToString() + ',' + coordY + ',' + currentZoneName);
        }
    }
}
