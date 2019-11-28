namespace SOMPredictedPoints
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
            this.runBox = new System.Windows.Forms.NumericUpDown();
            this.firstRunButton = new System.Windows.Forms.Button();
            this.lastRunButton = new System.Windows.Forms.Button();
            this.vecinatateLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.alphaLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.runBox)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.runBox);
            this.panel1.Controls.Add(this.firstRunButton);
            this.panel1.Controls.Add(this.lastRunButton);
            this.panel1.Controls.Add(this.vecinatateLabel);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.alphaLabel);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(894, 41);
            this.panel1.TabIndex = 0;
            // 
            // runBox
            // 
            this.runBox.Location = new System.Drawing.Point(72, 12);
            this.runBox.Name = "runBox";
            this.runBox.Size = new System.Drawing.Size(55, 20);
            this.runBox.TabIndex = 10;
            this.runBox.ValueChanged += new System.EventHandler(this.runBox_ValueChanged);
            // 
            // firstRunButton
            // 
            this.firstRunButton.Enabled = false;
            this.firstRunButton.Location = new System.Drawing.Point(41, 10);
            this.firstRunButton.Name = "firstRunButton";
            this.firstRunButton.Size = new System.Drawing.Size(25, 23);
            this.firstRunButton.TabIndex = 9;
            this.firstRunButton.Text = "|<";
            this.firstRunButton.UseVisualStyleBackColor = true;
            this.firstRunButton.Click += new System.EventHandler(this.firstRunButton_Click);
            // 
            // lastRunButton
            // 
            this.lastRunButton.Location = new System.Drawing.Point(133, 10);
            this.lastRunButton.Name = "lastRunButton";
            this.lastRunButton.Size = new System.Drawing.Size(28, 23);
            this.lastRunButton.TabIndex = 8;
            this.lastRunButton.Text = ">|";
            this.lastRunButton.UseVisualStyleBackColor = true;
            this.lastRunButton.Click += new System.EventHandler(this.lastRunButton_Click);
            // 
            // vecinatateLabel
            // 
            this.vecinatateLabel.AutoSize = true;
            this.vecinatateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.vecinatateLabel.Location = new System.Drawing.Point(599, 10);
            this.vecinatateLabel.Name = "vecinatateLabel";
            this.vecinatateLabel.Size = new System.Drawing.Size(18, 20);
            this.vecinatateLabel.TabIndex = 7;
            this.vecinatateLabel.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(517, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Vecinatate:";
            // 
            // alphaLabel
            // 
            this.alphaLabel.AutoSize = true;
            this.alphaLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.alphaLabel.Location = new System.Drawing.Point(332, 10);
            this.alphaLabel.Name = "alphaLabel";
            this.alphaLabel.Size = new System.Drawing.Size(18, 20);
            this.alphaLabel.TabIndex = 5;
            this.alphaLabel.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(193, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Coeficient invatare:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chart1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 41);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(894, 409);
            this.panel2.TabIndex = 1;
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
            this.chart1.Size = new System.Drawing.Size(894, 409);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(3, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Run: ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(894, 450);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.runBox)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Label vecinatateLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label alphaLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button firstRunButton;
        private System.Windows.Forms.Button lastRunButton;
        private System.Windows.Forms.NumericUpDown runBox;
        private System.Windows.Forms.Label label1;
    }
}

