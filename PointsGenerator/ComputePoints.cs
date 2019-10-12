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
        private Zone[] zones;
        private Random random;

        public ComputePoints()
        {
            zones = new[]
            {
                new Zone(300, 150, 10, "Zone1"),
                new Zone(-200, -200, 10, "Zone2"),
                new Zone(-300, 20, 10, "Zone3"),
                new Zone(50, -200, 10, "Zone4")
            };

            random = new Random();
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

            for (int i = 1; i <= 100; i++)
            {
                int randomZoneIndex = random.Next(0, 4);
                Zone currentZone = zones[randomZoneIndex];
                int coordX = GenerateCoordinate(currentZone.CenterX, currentZone.Deviation);
                int coordY = GenerateCoordinate(currentZone.CenterY, currentZone.Deviation);
                WriteToFile(coordX, coordY, currentZone.Name);
            }
        }

        private int GenerateCoordinate(int zoneCenterCoordinate, int deviation)
        {
            double gauss;
            double p;
            int coordinate;

            do
            {
                coordinate = random.Next(-400, 401);
                double fraction = ((zoneCenterCoordinate - coordinate) ^ 2) / (double) (2 * (deviation ^ 2));
                fraction = fraction * -1;

                gauss = Math.Exp(fraction);
                do
                {
                    p = NextRandomRange(0, 1 + double.Epsilon);
                } while (p <= 0 && p >= 1);
            } while (gauss < p);

            return coordinate;
        }

        private double NextRandomRange(double minimum, double maximum)
        {
            Random rand = new Random();
            return rand.NextDouble() * (maximum - minimum) + minimum;
        }

        private void WriteToFile(int coordX, int coordY, string currentZoneName)
        {
            using (StreamWriter file =
                new StreamWriter(Directory.GetCurrentDirectory() + @"\points.txt", true))
            {
                file.WriteLine(coordX.ToString() + ',' + coordY + ',' + currentZoneName);
            }
        }
    }
}
