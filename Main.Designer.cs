namespace LLP
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose (bool disposing)
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
        private void InitializeComponent ()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.button1 = new System.Windows.Forms.Button();
            this.chartGraphic = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.button2 = new System.Windows.Forms.Button();
            this.systemOfConstraintsDataGridView = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.objectFunctionDataGridView = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.chartGraphic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.systemOfConstraintsDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectFunctionDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(430, 550);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 50);
            this.button1.TabIndex = 3;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // chartGraphic
            // 
            chartArea5.AxisX.Interval = 1D;
            chartArea5.AxisX.Maximum = 10D;
            chartArea5.AxisX.Minimum = 0D;
            chartArea5.AxisY.Interval = 1D;
            chartArea5.AxisY.Maximum = 10D;
            chartArea5.AxisY.Minimum = 0D;
            chartArea5.Name = "ChartArea1";
            this.chartGraphic.ChartAreas.Add(chartArea5);
            legend5.Name = "Legend1";
            this.chartGraphic.Legends.Add(legend5);
            this.chartGraphic.Location = new System.Drawing.Point(638, 102);
            this.chartGraphic.Name = "chartGraphic";
            this.chartGraphic.Size = new System.Drawing.Size(553, 393);
            this.chartGraphic.TabIndex = 5;
            this.chartGraphic.Text = "chart1";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(604, 550);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(105, 50);
            this.button2.TabIndex = 4;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // systemOfConstraintsDataGridView
            // 
            this.systemOfConstraintsDataGridView.AllowUserToAddRows = false;
            this.systemOfConstraintsDataGridView.AllowUserToDeleteRows = false;
            this.systemOfConstraintsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.systemOfConstraintsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5});
            this.systemOfConstraintsDataGridView.Location = new System.Drawing.Point(12, 114);
            this.systemOfConstraintsDataGridView.Name = "systemOfConstraintsDataGridView";
            this.systemOfConstraintsDataGridView.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.systemOfConstraintsDataGridView.RowTemplate.Height = 24;
            this.systemOfConstraintsDataGridView.Size = new System.Drawing.Size(597, 261);
            this.systemOfConstraintsDataGridView.TabIndex = 6;
            // 
            // groupBox1
            // 
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1203, 55);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Количество строк системы ограничений";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(823, 545);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(194, 55);
            this.button3.TabIndex = 8;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // objectFunctionDataGridView
            // 
            this.objectFunctionDataGridView.AllowUserToAddRows = false;
            this.objectFunctionDataGridView.AllowUserToDeleteRows = false;
            this.objectFunctionDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.objectFunctionDataGridView.Location = new System.Drawing.Point(12, 61);
            this.objectFunctionDataGridView.Name = "objectFunctionDataGridView";
            this.objectFunctionDataGridView.ReadOnly = true;
            this.objectFunctionDataGridView.RowTemplate.Height = 24;
            this.objectFunctionDataGridView.Size = new System.Drawing.Size(597, 47);
            this.objectFunctionDataGridView.TabIndex = 9;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "№";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "x1";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "x2";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Operator";
            this.Column4.Items.AddRange(new object[] {
            "<=",
            ">="});
            this.Column4.Name = "Column4";
            this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "c";
            this.Column5.Name = "Column5";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1203, 645);
            this.Controls.Add(this.objectFunctionDataGridView);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.systemOfConstraintsDataGridView);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.chartGraphic);
            this.Controls.Add(this.button1);
            this.Name = "Main";
            this.Text = "LLP";
            ((System.ComponentModel.ISupportInitialize)(this.chartGraphic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.systemOfConstraintsDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.objectFunctionDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartGraphic;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView systemOfConstraintsDataGridView;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DataGridView objectFunctionDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewComboBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
    }
}

