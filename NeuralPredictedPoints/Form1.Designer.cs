namespace NeuralPredictedPoints
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.predictAllButton = new System.Windows.Forms.Button();
            this.accuracyLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.trainButton = new System.Windows.Forms.Button();
            this.agesNumberInput = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.hLayerNumberInput = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.removeEntireAreaButton = new System.Windows.Forms.Button();
            this.clickPredictCheckbox = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.agesNumberInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hLayerNumberInput)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.clickPredictCheckbox);
            this.panel1.Controls.Add(this.removeEntireAreaButton);
            this.panel1.Controls.Add(this.predictAllButton);
            this.panel1.Controls.Add(this.accuracyLabel);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.trainButton);
            this.panel1.Controls.Add(this.agesNumberInput);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.hLayerNumberInput);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 46);
            this.panel1.TabIndex = 1;
            // 
            // predictAllButton
            // 
            this.predictAllButton.Enabled = false;
            this.predictAllButton.Location = new System.Drawing.Point(402, 8);
            this.predictAllButton.Name = "predictAllButton";
            this.predictAllButton.Size = new System.Drawing.Size(107, 23);
            this.predictAllButton.TabIndex = 7;
            this.predictAllButton.Text = "Predict entire area";
            this.predictAllButton.UseVisualStyleBackColor = true;
            this.predictAllButton.Click += new System.EventHandler(this.predictAllButton_Click);
            // 
            // accuracyLabel
            // 
            this.accuracyLabel.AutoSize = true;
            this.accuracyLabel.Location = new System.Drawing.Point(725, 18);
            this.accuracyLabel.Name = "accuracyLabel";
            this.accuracyLabel.Size = new System.Drawing.Size(10, 13);
            this.accuracyLabel.TabIndex = 6;
            this.accuracyLabel.Text = "-";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(628, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Prediction accuracy:";
            // 
            // trainButton
            // 
            this.trainButton.Location = new System.Drawing.Point(306, 8);
            this.trainButton.Name = "trainButton";
            this.trainButton.Size = new System.Drawing.Size(75, 23);
            this.trainButton.TabIndex = 4;
            this.trainButton.Text = "TRAIN";
            this.trainButton.UseVisualStyleBackColor = true;
            this.trainButton.Click += new System.EventHandler(this.trainButton_Click);
            // 
            // agesNumberInput
            // 
            this.agesNumberInput.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.agesNumberInput.Location = new System.Drawing.Point(223, 11);
            this.agesNumberInput.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.agesNumberInput.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.agesNumberInput.Name = "agesNumberInput";
            this.agesNumberInput.Size = new System.Drawing.Size(63, 20);
            this.agesNumberInput.TabIndex = 3;
            this.agesNumberInput.Value = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(183, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Ages:";
            // 
            // hLayerNumberInput
            // 
            this.hLayerNumberInput.Location = new System.Drawing.Point(129, 11);
            this.hLayerNumberInput.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.hLayerNumberInput.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.hLayerNumberInput.Name = "hLayerNumberInput";
            this.hLayerNumberInput.Size = new System.Drawing.Size(36, 20);
            this.hLayerNumberInput.TabIndex = 1;
            this.hLayerNumberInput.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Hidden layer neurons:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chart1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 46);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 404);
            this.panel2.TabIndex = 2;
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
            this.chart1.Size = new System.Drawing.Size(800, 404);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            this.chart1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.chart1_MouseClick);
            this.chart1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart1_MouseMove);
            this.chart1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.chart1_MouseUp);
            // 
            // removeEntireAreaButton
            // 
            this.removeEntireAreaButton.Location = new System.Drawing.Point(402, 8);
            this.removeEntireAreaButton.Name = "removeEntireAreaButton";
            this.removeEntireAreaButton.Size = new System.Drawing.Size(117, 23);
            this.removeEntireAreaButton.TabIndex = 8;
            this.removeEntireAreaButton.Text = "Remove entire area";
            this.removeEntireAreaButton.UseVisualStyleBackColor = true;
            this.removeEntireAreaButton.Visible = false;
            this.removeEntireAreaButton.Click += new System.EventHandler(this.removeEntireAreaButton_Click);
            // 
            // clickPredictCheckbox
            // 
            this.clickPredictCheckbox.AutoSize = true;
            this.clickPredictCheckbox.Location = new System.Drawing.Point(526, 13);
            this.clickPredictCheckbox.Name = "clickPredictCheckbox";
            this.clickPredictCheckbox.Size = new System.Drawing.Size(96, 17);
            this.clickPredictCheckbox.TabIndex = 9;
            this.clickPredictCheckbox.Text = "Click to predict";
            this.clickPredictCheckbox.UseVisualStyleBackColor = true;
            this.clickPredictCheckbox.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Neural Network Predictor";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.agesNumberInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hLayerNumberInput)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.NumericUpDown hLayerNumberInput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button trainButton;
        private System.Windows.Forms.NumericUpDown agesNumberInput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label accuracyLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button predictAllButton;
        private System.Windows.Forms.Button removeEntireAreaButton;
        private System.Windows.Forms.CheckBox clickPredictCheckbox;
    }
}

