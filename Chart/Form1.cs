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

namespace Chart
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            DrawChart();
        }

        private void DrawChart()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("X_Value", typeof(double));
            dt.Columns.Add("Y_Value", typeof(double));

            StreamReader sr = new StreamReader(@"D:\Desktop\points.txt");
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                string[] strarr = line.Split(',');
                dt.Rows.Add(strarr[0], strarr[1]);
            }
            chart1.DataSource = dt;
            chart1.Series["Series1"].XValueMember = "X_Value";
            chart1.Series["Series1"].YValueMembers = "Y_Value";
            chart1.Series["Series1"].ChartType = SeriesChartType.Point;

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
    }
}
