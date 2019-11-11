namespace PredictedPoints
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.nextRun = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.assignAllButton = new System.Windows.Forms.Button();
            this.evaluationLabel = new System.Windows.Forms.Label();
            this.runLabel = new System.Windows.Forms.Label();
            this.previousRun = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.showHideCentroidsButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // chart1
            // 
            chartArea2.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea2);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend1";
            this.chart1.Legends.Add(legend2);
            this.chart1.Location = new System.Drawing.Point(0, 0);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(940, 559);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // nextRun
            // 
            this.nextRun.Location = new System.Drawing.Point(100, 3);
            this.nextRun.Name = "nextRun";
            this.nextRun.Size = new System.Drawing.Size(75, 23);
            this.nextRun.TabIndex = 0;
            this.nextRun.Text = "Next Run";
            this.nextRun.UseVisualStyleBackColor = true;
            this.nextRun.Click += new System.EventHandler(this.nextRun_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.showHideCentroidsButton);
            this.panel1.Controls.Add(this.assignAllButton);
            this.panel1.Controls.Add(this.evaluationLabel);
            this.panel1.Controls.Add(this.runLabel);
            this.panel1.Controls.Add(this.previousRun);
            this.panel1.Controls.Add(this.nextRun);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(940, 31);
            this.panel1.TabIndex = 2;
            // 
            // assignAllButton
            // 
            this.assignAllButton.Location = new System.Drawing.Point(786, 5);
            this.assignAllButton.Name = "assignAllButton";
            this.assignAllButton.Size = new System.Drawing.Size(151, 23);
            this.assignAllButton.TabIndex = 4;
            this.assignAllButton.Text = "Assign all points to centroids";
            this.assignAllButton.UseVisualStyleBackColor = true;
            this.assignAllButton.Click += new System.EventHandler(this.assignAllButton_Click);
            // 
            // evaluationLabel
            // 
            this.evaluationLabel.AutoSize = true;
            this.evaluationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.evaluationLabel.Location = new System.Drawing.Point(307, 6);
            this.evaluationLabel.Name = "evaluationLabel";
            this.evaluationLabel.Size = new System.Drawing.Size(105, 20);
            this.evaluationLabel.TabIndex = 3;
            this.evaluationLabel.Text = "Evaluation: 0";
            // 
            // runLabel
            // 
            this.runLabel.AutoSize = true;
            this.runLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.runLabel.Location = new System.Drawing.Point(207, 6);
            this.runLabel.Name = "runLabel";
            this.runLabel.Size = new System.Drawing.Size(58, 20);
            this.runLabel.TabIndex = 2;
            this.runLabel.Text = "Run: 1";
            // 
            // previousRun
            // 
            this.previousRun.Enabled = false;
            this.previousRun.Location = new System.Drawing.Point(12, 3);
            this.previousRun.Name = "previousRun";
            this.previousRun.Size = new System.Drawing.Size(82, 23);
            this.previousRun.TabIndex = 1;
            this.previousRun.Text = "Previous Run";
            this.previousRun.UseVisualStyleBackColor = true;
            this.previousRun.Click += new System.EventHandler(this.previousRun_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chart1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 31);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(940, 559);
            this.panel2.TabIndex = 3;
            // 
            // showHideCentroidsButton
            // 
            this.showHideCentroidsButton.Location = new System.Drawing.Point(682, 6);
            this.showHideCentroidsButton.Name = "showHideCentroidsButton";
            this.showHideCentroidsButton.Size = new System.Drawing.Size(98, 23);
            this.showHideCentroidsButton.TabIndex = 5;
            this.showHideCentroidsButton.Text = "Hide Centroids";
            this.showHideCentroidsButton.UseVisualStyleBackColor = true;
            this.showHideCentroidsButton.Click += new System.EventHandler(this.showHideCentroidsButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(940, 590);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button nextRun;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button previousRun;
        private System.Windows.Forms.Label runLabel;
        private System.Windows.Forms.Label evaluationLabel;
        private System.Windows.Forms.Button assignAllButton;
        private System.Windows.Forms.Button showHideCentroidsButton;
    }
}

