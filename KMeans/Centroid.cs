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

        public int PointNumberTrust { get; set; } //numaru de puncte al unui centroid raportat la numarul total de puncte
        public double DeviationTrust { get; set; }  //deviatia de la medie ne poate indica daca un centroid are mai multe zone
                                                    //facem raportul intre media distantelor dintre un centroid si punctele lor
                                                    //cu deviatia de la medie, iar daca raportul e mai mare de 25%, avem mai multe zone

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
