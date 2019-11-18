using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using SOM_Kohonen;

namespace SOMPredictedPoints
{
    public partial class Form1 : Form
    {
        private ZonesPredictor zonesPredictor;
        public Form1()
        {
            zonesPredictor = new ZonesPredictor();
            InitializeComponent();
            zonesPredictor.Predict();
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

            int neuronsNumber = zonesPredictor.W.Length;
            int neuronsLength = (int)Math.Sqrt(neuronsNumber);

            for (int i = 0; i < neuronsLength; i++)
            {
                chart1.Series.Add("NeuronsI" + i);
                chart1.Series["NeuronsI" + i].ChartType = SeriesChartType.Line;
                chart1.Series["NeuronsI" + i].IsVisibleInLegend = false;

                for (int j = 0; j < neuronsLength; j++)
                {

                    chart1.Series["NeuronsI" + i].Points
                        .Add(new DataPoint(zonesPredictor.W[i, j].X, zonesPredictor.W[i, j].Y));
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
                        .Add(new DataPoint(zonesPredictor.W[i, j].X, zonesPredictor.W[i, j].Y));
                }
            }

        }
    }
}
