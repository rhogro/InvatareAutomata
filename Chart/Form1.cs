using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using PointsGenerator;

namespace Chart
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ComputePoints compute = new ComputePoints();
            compute.Compute();
            Console.WriteLine("Points computed, drawing chart...");
            DrawChart();
            Console.WriteLine("Chart drawn");
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

            var zones = dt.AsEnumerable().Select(r => r.Field<string>("Zone")).Distinct().ToList();

            chart1.DataSource = dt;

            foreach (var zone in zones)
            {
                List<int> xVals = new List<int>();
                List<int> yVals = new List<int>();

                chart1.Series.Add(zone);
                chart1.Series[zone].ChartType = SeriesChartType.Point;

                foreach (DataRow dataRow in dt.Rows)
                {
                    try
                    {
                        if (String.Equals(zone, dataRow["Zone"].ToString(), StringComparison.Ordinal))
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
                    chart1.Series[zone].Points.DataBindXY(xVals, yVals);
                }
                catch (Exception)
                {
                    throw new InvalidOperationException();
                }

                //chart1.Series[zone].XValueMember = "X_Value";
                //chart1.Series[zone].YValueMembers = "Y_Value";
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
        }
    }
}
