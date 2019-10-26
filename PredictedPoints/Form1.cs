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
using KMeans;

namespace PredictedPoints
{
    public partial class Form1 : Form
    {
        private int run = 1;
        private DataTable dt = new DataTable();
        private List<string> zones;
        private double evaluation = 0;
        public Form1()
        {
            InitializeComponent();
            PredictZones zonesPredictor = new PredictZones();
            zonesPredictor.Predict();
            DrawChart();
        }

        private void DrawChart()
        {
            dt.Columns.Add("X_Value", typeof(int));
            dt.Columns.Add("Y_Value", typeof(int));
            dt.Columns.Add("Zone_Name", typeof(string));
            dt.Columns.Add("Run", typeof(int));
            dt.Columns.Add("Evaluation", typeof(double));

            StreamReader sr = new StreamReader(Directory.GetCurrentDirectory() + @"\prediceted_points.txt");
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] strarr = line.Split(',');
                dt.Rows.Add(strarr[0], strarr[1], strarr[2], strarr[3], strarr[4]);
            }
            sr.Close();

            chart1.DataSource = dt;

            AssignValuesToChart();

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

        private void AssignValuesToChart()
        {
            zones = dt.AsEnumerable().Select(r => r.Field<string>("Zone_Name")).Distinct().ToList();

            Color[] colors = {
                Color.Red, Color.Blue, Color.Green, Color.Cyan, Color.Brown, Color.Orange, Color.Magenta
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
                        if (String.Equals(zoneName, dataRow["Zone_Name"].ToString(), StringComparison.Ordinal) &&
                            (int)dataRow["run"] == run)
                        {
                            xVals.Add((int)dataRow["X_Value"]);
                            yVals.Add((int)dataRow["Y_Value"]);
                            evaluation = (double)dataRow["Evaluation"];
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
            }

            evaluationLabel.Text = "Evaluation: " + evaluation;
        }

        private void nextRun_Click(object sender, EventArgs e)
        {
            run++;
            CleanChart();
            AssignValuesToChart();
            runLabel.Text = "Run: " + run;
            previousRun.Enabled = true;
        }

        private void previousRun_Click(object sender, EventArgs e)
        {
            run--;
            CleanChart();
            AssignValuesToChart();
            runLabel.Text = "Run: " + run;

            if (run <= 1)
            {
                previousRun.Enabled = false;
            }
        }

        private void CleanChart()
        {
            foreach (var zoneName in zones)
            {
                chart1.Series.Remove(chart1.Series[zoneName]);
            }
        }

    }
}
