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
using SOM_Kohonen;
using Point = System.Drawing.Point;

namespace SOMPredictedPoints
{

    public partial class Form1 : Form
    {
        private ZonesPredictor zonesPredictor;
        private DataTable dt = new DataTable();
        private int run = 0;
        public Form1()
        {
            zonesPredictor = new ZonesPredictor();
            InitializeComponent();
            zonesPredictor.Predict();
            runBox.Minimum = 0;
            runBox.Maximum = zonesPredictor.Runs.Count - 1;
            DrawChart();
        }

        private void DrawChart()
        {
            chart1.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;
            chart1.ChartAreas[0].AxisX.Crossing = 0;
            chart1.ChartAreas[0].AxisY.Crossing = 0;
            chart1.ChartAreas[0].AxisX.Minimum = -400d;
            chart1.ChartAreas[0].AxisX.Maximum = 400d;
            chart1.ChartAreas[0].AxisY.Minimum = -400d;
            chart1.ChartAreas[0].AxisY.Maximum = 400d;

            dt.Columns.Add("X_Value", typeof(int));
            dt.Columns.Add("Y_Value", typeof(int));
            dt.Columns.Add("Run", typeof(int));
            dt.Columns.Add("Alpha", typeof(double));
            dt.Columns.Add("Vecinatate", typeof(double));

            AssignValuesToChart();
        }

        private void AssignValuesToChart()
        {
            int neuronsNumber = zonesPredictor.Runs[run].Position.Length;
            int neuronsLength = (int)Math.Sqrt(neuronsNumber);

            for (int i = 0; i < neuronsLength; i++)
            {


                for (int j = 0; j < neuronsLength; j++)
                {
                    if (i < neuronsLength - 1)
                    {
                        chart1.Series.Add("NeuronsI1" + i.ToString() + j.ToString());
                        chart1.Series["NeuronsI1" + i.ToString() + j.ToString()].ChartType = SeriesChartType.Line;
                        chart1.Series["NeuronsI1" + i.ToString() + j.ToString()].IsVisibleInLegend = false;
                        chart1.Series["NeuronsI1" + i.ToString() + j.ToString()].Points
                            .Add(new DataPoint(zonesPredictor.Runs[run].Position[i, j].X, zonesPredictor.Runs[run].Position[i, j].Y));
                        chart1.Series["NeuronsI1" + i.ToString() + j.ToString()].Points
                            .Add(new DataPoint(zonesPredictor.Runs[run].Position[i + 1, j].X, zonesPredictor.Runs[run].Position[i + 1, j].Y));
                    }

                    if (j < neuronsLength - 1)
                    {
                        chart1.Series.Add("NeuronsI2" + i.ToString() + j.ToString());
                        chart1.Series["NeuronsI2" + i.ToString() + j.ToString()].ChartType = SeriesChartType.Line;
                        chart1.Series["NeuronsI2" + i.ToString() + j.ToString()].IsVisibleInLegend = false;
                        chart1.Series["NeuronsI2" + i.ToString() + j.ToString()].Points
                            .Add(new DataPoint(zonesPredictor.Runs[run].Position[i, j].X, zonesPredictor.Runs[run].Position[i, j].Y));
                        chart1.Series["NeuronsI2" + i.ToString() + j.ToString()].Points
                            .Add(new DataPoint(zonesPredictor.Runs[run].Position[i, j + 1].X, zonesPredictor.Runs[run].Position[i, j + 1].Y));
                    }

                }
            }
        }

        private void CleanChart()
        {
            chart1.Series.Clear();
        }

        private void lastRunButton_Click(object sender, EventArgs e)
        {
            run = zonesPredictor.Runs.Count - 1;
            CleanChart();
            AssignValuesToChart();
            alphaLabel.Text = zonesPredictor.Runs[run].Alpha.ToString();
            vecinatateLabel.Text = zonesPredictor.Runs[run].Vecinatate.ToString();
            lastRunButton.Enabled = false;
            firstRunButton.Enabled = true;
            runBox.Value = run;
        }

        private void firstRunButton_Click(object sender, EventArgs e)
        {
            run = 0;
            CleanChart();
            AssignValuesToChart();
            alphaLabel.Text = zonesPredictor.Runs[run].Alpha.ToString();
            vecinatateLabel.Text = zonesPredictor.Runs[run].Vecinatate.ToString();
            firstRunButton.Enabled = false;
            lastRunButton.Enabled = true;
            runBox.Value = run;
        }

        private void runBox_ValueChanged(object sender, EventArgs e)
        {
            run = (int)runBox.Value;
            CleanChart();
            AssignValuesToChart();
            alphaLabel.Text = zonesPredictor.Runs[run].Alpha.ToString();
            vecinatateLabel.Text = zonesPredictor.Runs[run].Vecinatate.ToString();
            if (run == 0)
            {
                firstRunButton.Enabled = false;
            }

            else if (run == zonesPredictor.Runs.Count - 1)
            {
                lastRunButton.Enabled = false;
            }
            else
            {
                firstRunButton.Enabled = true;
                lastRunButton.Enabled = true;
            }
        }
    }
}
