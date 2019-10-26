using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using PointsGenerator;

namespace Chart
{
    public partial class Form1 : Form
    {

        private Zone[] zones;
        public Form1()
        {
            InitializeComponent();
            ComputePoints compute = new ComputePoints();
            compute.Compute();
            zones = compute.Zones;
            DrawChart();
        }

        private void DrawChart()
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

            List<string> zones = dt.AsEnumerable().Select(r => r.Field<string>("Zone")).Distinct().ToList();

            chart1.DataSource = dt;

            Color[] colors = {
                Color.Red, Color.Blue, Color.Green, Color.Pink, Color.Brown, Color.Orange
            };
            Dictionary<string, Color> zoneColors = new Dictionary<string, Color>();

            for (int i = 0; i < zones.Count; i++)
            {
                zoneColors.Add(zones[i], colors[i]);
            }
            

            foreach (var zoneName in zones)
            {
                List<int> xVals = new List<int>();
                List<int> yVals = new List<int>();

                chart1.Series.Add(zoneName);
                chart1.Series[zoneName].ChartType = SeriesChartType.Point;
                chart1.Series[zoneName].Color = zoneColors[zoneName];

                foreach (DataRow dataRow in dt.Rows)
                {
                    try
                    {
                        if (String.Equals(zoneName, dataRow["Zone"].ToString(), StringComparison.Ordinal))
                        {
                            xVals.Add((int) dataRow["X_Value"]);
                            yVals.Add((int) dataRow["Y_Value"]);
                        }
                    }
                    catch (Exception)
                    {
                        throw new InvalidOperationException();
                    }
                }

                try
                {
                    chart1.Series[zoneName].Points.DataBindXY(xVals, yVals);
                }
                catch (Exception)
                {
                    throw new InvalidOperationException();
                }

                /**** Zones delimiters ****/
                Series series1 = new Series(zoneName + "Rectangle");

                var zone = this.zones.First(z => z.Name == zoneName);

                series1.Points.AddXY(zone.CenterX - zone.Deviation / 2,
                    zone.CenterY - zone.Deviation / 2);
                series1.Points.AddXY(zone.CenterX - zone.Deviation / 2,
                    zone.CenterY + zone.Deviation / 2);
                series1.Points.AddXY(zone.CenterX + zone.Deviation / 2,
                    zone.CenterY - zone.Deviation / 2);
                series1.Points.AddXY(zone.CenterX + zone.Deviation / 2,
                    zone.CenterY + zone.Deviation / 2);

                chart1.Series.Add(series1);

                chart1.Series[zoneName + "Rectangle"].ChartType = SeriesChartType.Candlestick;
            }

            chart1.DataBind();
            
            chart1.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;

            chart1.ChartAreas[0].AxisX.Crossing = 0;
            chart1.ChartAreas[0].AxisY.Crossing = 0;

            chart1.ChartAreas[0].AxisX.Minimum = -400d;
            chart1.ChartAreas[0].AxisX.Maximum = 400d;
            chart1.ChartAreas[0].AxisY.Minimum = -400d;
            chart1.ChartAreas[0].AxisY.Maximum = 400d;

            sr.Close();
        }

        private void predict_Click(object sender, EventArgs e)
        {
            PredictedPoints.Form1 predictedPointsFrom = new PredictedPoints.Form1();
            predictedPointsFrom.Show();
        }
    }
}
