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
        private PredictZones zonesPredictor;
        private int run = 1;
        private DataTable dt = new DataTable();
        private List<Centroid> centroids;
        private double evaluation = 0;
        private bool ShowCentroids = true;
        public Form1()
        {
            InitializeComponent();
            zonesPredictor = new PredictZones();
            centroids = new List<Centroid>();
            zonesPredictor.Predict();
            DrawChart("prediceted_points.txt");
        }

        private void DrawChart(string file)
        {
            dt.Columns.Add("X_Value", typeof(int));
            dt.Columns.Add("Y_Value", typeof(int));
            dt.Columns.Add("Centroid_Name", typeof(string));
            dt.Columns.Add("Centroid_X_Value", typeof(int));
            dt.Columns.Add("Centroid_Y_Value", typeof(int));
            dt.Columns.Add("Run", typeof(int));
            dt.Columns.Add("Evaluation", typeof(double));

            StreamReader sr = new StreamReader(Directory.GetCurrentDirectory() + @"\" + file);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] strarr = line.Split(',');
                dt.Rows.Add(strarr[0], strarr[1], strarr[2], strarr[3], strarr[4], strarr[5], strarr[6]);
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
            foreach (DataRow row in dt.Rows)
            {
                if (!centroids.Any(x => x.Name == row.Field<string>("Centroid_Name")))
                {
                    centroids.Add(new Centroid(row.Field<int>("Centroid_X_Value"), row.Field<int>("Centroid_Y_Value"), row.Field<string>("Centroid_Name")));
                }
            }

            Color[] colors = {
                Color.Red, Color.Blue, Color.Green, Color.Cyan, Color.Brown, Color.Orange, Color.Magenta
            };
            Dictionary<string, Color> zoneColors = new Dictionary<string, Color>();

            for (int i = 0; i < centroids.Count; i++)
            {
                zoneColors.Add(centroids[i].Name, colors[i]);
            }

            foreach (var centroid in centroids)
            {
                List<int> xVals = new List<int>();
                List<int> yVals = new List<int>();

                List<int> centroidXVals = new List<int>();
                List<int> centroidYVals = new List<int>();

                chart1.Series.Add(centroid.Name + " Points");
                chart1.Series[centroid.Name + " Points"].ChartType = SeriesChartType.Point;
                chart1.Series[centroid.Name + " Points"].MarkerStyle = MarkerStyle.Square;
                chart1.Series[centroid.Name + " Points"].Color = zoneColors[centroid.Name];

                chart1.Series.Add(centroid.Name);
                chart1.Series[centroid.Name].ChartType = SeriesChartType.Point;
                chart1.Series[centroid.Name].MarkerStyle = MarkerStyle.Cross;
                chart1.Series[centroid.Name].MarkerSize = 10;
                chart1.Series[centroid.Name].Color = ControlPaint.Dark(zoneColors[centroid.Name], 0.25f);

                foreach (DataRow dataRow in dt.Rows)
                {
                    try
                    {
                        if (String.Equals(centroid.Name, dataRow["Centroid_Name"].ToString(), StringComparison.Ordinal) &&
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

                centroidXVals.Add(centroid.Center.X);
                centroidYVals.Add(centroid.Center.Y);

                try
                {
                    chart1.Series[centroid.Name + " Points"].Points.DataBindXY(xVals, yVals);
                    chart1.Series[centroid.Name].Points.DataBindXY(centroidXVals, centroidYVals);
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
            chart1.Series.Clear();
        }

        private bool assignAllButtonToggled = false;

        private void assignAllButton_Click(object sender, EventArgs e)
        {
            if (assignAllButtonToggled == false)
            {
                assignAllButton.Text = "Please Wait";
                assignAllButton.Enabled = false;
                CleanChart();
                dt.Rows.Clear();
                dt.Columns.Clear();
                zonesPredictor.AssignAllPoints();
                run = 1;
                runLabel.Text = "Run: 1";
                evaluationLabel.Text = "";
                DrawChart("all_points.txt");
                assignAllButtonToggled = true;
                assignAllButton.Text = "Show zones";
                assignAllButton.Enabled = true;
                nextRun.Enabled = false;
                previousRun.Enabled = false;
            }
            else
            {
                assignAllButton.Text = "Please Wait";
                assignAllButton.Enabled = false;
                CleanChart();
                dt.Rows.Clear();
                dt.Columns.Clear();
                DrawChart("prediceted_points.txt");
                assignAllButtonToggled = false;
                assignAllButton.Enabled = true;
                assignAllButton.Text = "Assign all points to centroids";
                nextRun.Enabled = true;
                previousRun.Enabled = true;
                evaluationLabel.Text = "Evaluation: " + evaluation;
            }

        }

        private void showHideCentroidsButton_Click(object sender, EventArgs e)
        {
            if (ShowCentroids)
            {
                ShowCentroids = false;
                showHideCentroidsButton.Text = "Show Centroids";
                foreach (var centroid in centroids)
                {
                    chart1.Series[centroid.Name].Enabled = false;
                }
            }
            else
            {
                ShowCentroids = true;
                showHideCentroidsButton.Text = "Hide Centroids";
                foreach (var centroid in centroids)
                {
                    chart1.Series[centroid.Name].Enabled = true;
                }
            }
        }
    }
}
