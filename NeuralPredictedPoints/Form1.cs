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

namespace NeuralPredictedPoints
{
    public partial class Form1 : Form
    {
        private NeuralNetwork_Points.Computer neuralComputer;
        ToolTip tooltip;
        public Form1()
        {
            InitializeComponent();
            tooltip = new ToolTip();
        }

        private void DrawChart()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("X_Value", typeof(int));
            dt.Columns.Add("Y_Value", typeof(int));
            dt.Columns.Add("Zone", typeof(string));

            StreamReader sr = new StreamReader(Directory.GetCurrentDirectory() + @"\neural predicted points.txt");
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
                chart1.Series[zoneName].MarkerStyle = MarkerStyle.Square;
                chart1.Series[zoneName].Color = zoneColors[zoneName];

                foreach (DataRow dataRow in dt.Rows)
                {
                    try
                    {
                        if (String.Equals(zoneName, dataRow["Zone"].ToString(), StringComparison.Ordinal))
                        {
                            xVals.Add((int)dataRow["X_Value"]);
                            yVals.Add((int)dataRow["Y_Value"]);
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

        private void trainButton_Click(object sender, EventArgs e)
        {
            trainButton.Enabled = false;
            int hiddenLayerNumber = (int)hLayerNumberInput.Value;
            int agesNumber = (int)agesNumberInput.Value;
            neuralComputer = new NeuralNetwork_Points.Computer(hiddenLayerNumber, agesNumber);
            neuralComputer.Train();
            neuralComputer.ComputePointsFromFile();
            double accuracy = neuralComputer.CheckAccuracy("Training");
            accuracyLabel.Text = string.Format("{0:00.###}%", accuracy);
            DrawChart();
            predictAllButton.Enabled = true;
            clickPredictCheckbox.Visible = true;
        }

        private void predictAllButton_Click(object sender, EventArgs e)
        {
            predictAllButton.Visible = false;
            removeEntireAreaButton.Visible = true;
            removeEntireAreaButton.Enabled = false;
            clickPredictCheckbox.Checked = false;
            clickPredictCheckbox.Enabled = false;
            for (int i = -400; i <= 400; i++)
            {
                for (int j = -400; j <= 400; j++)
                {
                    string predictedZone = neuralComputer.PredictPoint(i, j);
                    try
                    {
                        chart1.Series[predictedZone].Points.AddXY(i, j);
                    }
                    catch (Exception)
                    {
                        chart1.Series.Add(predictedZone);
                        chart1.Series[predictedZone].Points.AddXY(i, j);
                    }

                }
            }
            removeEntireAreaButton.Enabled = true;
        }

        private void removeEntireAreaButton_Click(object sender, EventArgs e)
        {
            predictAllButton.Visible = true;
            removeEntireAreaButton.Visible = false;
            predictAllButton.Enabled = false;
            chart1.Series.Clear();
            DrawChart();
            predictAllButton.Enabled = true;
            clickPredictCheckbox.Enabled = true;
        }

        private void chart1_MouseClick(object sender, MouseEventArgs e)
        {
        }

        private void chart1_MouseMove(object sender, MouseEventArgs e)
        {
            if (clickPredictCheckbox.Checked)
            {
                var results = chart1.HitTest(e.X, e.Y, false, ChartElementType.PlottingArea);
                foreach (var result in results)
                {
                    if (result.ChartElementType == ChartElementType.PlottingArea)
                    {
                        double xVal = chart1.ChartAreas[0].AxisX.PixelPositionToValue(e.X);
                        double yVal = chart1.ChartAreas[0].AxisY.PixelPositionToValue(e.Y);
                        tooltip.Show("X=" + xVal + ", Y=" + yVal, this.chart1, e.Location.X, e.Location.Y - 15);
                    }
                    else
                    {
                        tooltip.RemoveAll();
                    }
                }
            }
        }

        private void chart1_MouseUp(object sender, MouseEventArgs e)
        {
            if (clickPredictCheckbox.Checked)
            {
                var results = chart1.HitTest(e.X, e.Y, false, ChartElementType.PlottingArea);
                foreach (var result in results)
                {
                    if (result.ChartElementType == ChartElementType.PlottingArea)
                    {
                        double xVal = chart1.ChartAreas[0].AxisX.PixelPositionToValue(e.X);
                        double yVal = chart1.ChartAreas[0].AxisY.PixelPositionToValue(e.Y);
                        string predictedZone = neuralComputer.PredictPoint((int)xVal, (int)yVal);
                        chart1.Series[predictedZone].Points.AddXY(xVal, yVal);
                    }
                }
            }
        }
    }

}
