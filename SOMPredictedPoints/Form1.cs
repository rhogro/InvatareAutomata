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

            //StreamReader sr = new StreamReader(Directory.GetCurrentDirectory() + @"\" + "neurons.txt");
            //string line;
            //while ((line = sr.ReadLine()) != null)
            //{
            //    string[] strarr = line.Split(',');
            //    dt.Rows.Add(strarr[0], strarr[1], strarr[2], strarr[3], strarr[4]);
            //}
            //sr.Close();

            //chart1.DataSource = dt;



            AssignValuesToChart();

        }

        private void AssignValuesToChart()
        {
            int neuronsNumber = zonesPredictor.Runs[run].Position.Length;
            int neuronsLength = (int)Math.Sqrt(neuronsNumber);

            //List<Run> runList = new List<Run>();
            //int r = 0;
            //runList.Add(new Run(0,0,0, new Point[neuronsLength,neuronsLength]));
            //int a = 0, b = 0;
            //Point[,] points = new Point[neuronsLength,neuronsLength]; 
            //foreach (DataRow row in dt.Rows)
            //{
            //    if (row.Field<int>("Run") == r)
            //    {
            //        points[a,b] = new Point(row.Field<int>("X_Value"), row.Field<int>("Y_Value"));
            //        if (a == neuronsLength - 1)
            //        {
            //            a = 0;
            //            b++;
            //        }
            //        else
            //        {
            //            a++;
            //        }
            //    }
            //    else
            //    {

            //        runList.Find(x => x.Index == r).Position = points;
            //        r++;
            //        runList.Add(new Run(r, row.Field<double>("Alpha"), row.Field<double>("Vecinatate"), points));
            //        points[0,0] = new Point(row.Field<int>("X_Value"), row.Field<int>("Y_Value"));
            //        a = 1;
            //        b = 0;
            //    }
            //}

            //for (int i = 0; i < neuronsLength; i++)
            //{
            //    for (int j = 0; j < neuronsLength; j++)
            //    {

            //    }
            //}

            for (int i = 0; i < neuronsLength; i++)
            {
                chart1.Series.Add("NeuronsI" + i);
                chart1.Series["NeuronsI" + i].ChartType = SeriesChartType.Line;
                chart1.Series["NeuronsI" + i].IsVisibleInLegend = false;

                for (int j = 0; j < neuronsLength; j++)
                {

                    chart1.Series["NeuronsI" + i].Points
                        .Add(new DataPoint(zonesPredictor.Runs[run].Position[i, j].X, zonesPredictor.Runs[run].Position[i, j].Y));
                }
            }

            for (int j = 0; j < neuronsLength; j++)
            {
                chart1.Series.Add("NeuronsJ" + j);
                chart1.Series["NeuronsJ" + j].ChartType = SeriesChartType.Line;
                chart1.Series["NeuronsJ" + j].IsVisibleInLegend = false;
                for (int i = 0; i < neuronsLength; i++)
                {

                    chart1.Series["NeuronsJ" + j].Points
                        .Add(new DataPoint(zonesPredictor.Runs[run].Position[i, j].X, zonesPredictor.Runs[run].Position[i, j].Y));
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
