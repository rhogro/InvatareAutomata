using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointsGenerator
{
    public class Zone
    {
        public string Name { get; set; }
        public int CenterX { get; set; }
        public int CenterY { get; set; }
        public int Deviation { get; set; }

        public Zone(int centerX, int centerY, int deviation, string name)
        {
            if (centerX >= 400 || centerX <= -400)
            {
                throw new ArgumentOutOfRangeException(nameof(centerX),"All coordinates have to be in [-400, 400] interval");
            }
            if (centerY >= 400 || centerY <= -400)
            {
                throw new ArgumentOutOfRangeException(nameof(centerY), "All coordinates have to be in [-400, 400] interval");
            }

            if (centerX + deviation > 400 || centerX - deviation < -400 || centerY + deviation > 400 ||
                centerY - deviation < -400)
            {
                throw new ArgumentException("Zone out of boundary. Deviation too large", nameof(deviation));
            }
            
            CenterX = centerX;
            CenterY = centerY;
            Deviation = Math.Abs(deviation);
            Name = name;
        }
    }
}
