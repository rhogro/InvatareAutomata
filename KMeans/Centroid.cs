using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMeans
{
    public class Centroid
    {
        public string Name { get; set; }
        public Point Center { get; set; }
        public List<Point> AssignedPoints { get; set; }

        public Centroid(int centerX, int centerY, string name)
        {
            if (centerX > 400 || centerX < -400)
            {
                throw new ArgumentOutOfRangeException(nameof(centerX), "All coordinates have to be in [-400, 400] interval");
            }
            if (centerY > 400 || centerY < -400)
            {
                throw new ArgumentOutOfRangeException(nameof(centerY), "All coordinates have to be in [-400, 400] interval");
            }

            Center = new Point(centerX, centerY);
            Name = name;
            AssignedPoints = new List<Point>();
        }
    }
}
