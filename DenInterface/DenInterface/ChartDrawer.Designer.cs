namespace DenInterface
{
    partial class ChartDrawer
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.MChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.ConsequenceList = new System.Windows.Forms.ListView();
            this.SaveIMG = new System.Windows.Forms.Button();
            this.SVIMGDL = new System.Windows.Forms.SaveFileDialog();
            this.ValueLabelSwitch = new System.Windows.Forms.CheckBox();
            this.NameLabelSwitch = new System.Windows.Forms.CheckBox();
            this.ConsequenceGroupBox = new System.Windows.Forms.GroupBox();
            this.DrawerGroupBox = new System.Windows.Forms.GroupBox();
            this.CopyConsequencePropertiesToClickBoardBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.MChart)).BeginInit();
            this.ConsequenceGroupBox.SuspendLayout();
            this.DrawerGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // MChart
            // 
            chartArea3.Name = "ChartArea1";
            chartArea4.Name = "ChartArea2";
            this.MChart.ChartAreas.Add(chartArea3);
            this.MChart.ChartAreas.Add(chartArea4);
            legend2.Name = "Legend1";
            this.MChart.Legends.Add(legend2);
            this.MChart.Location = new System.Drawing.Point(16, 108);
            this.MChart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MChart.Name = "MChart";
            this.MChart.Size = new System.Drawing.Size(1489, 1365);
            this.MChart.TabIndex = 1;
            this.MChart.Text = "chart1";
            this.MChart.Click += new System.EventHandler(this.MChart_Click);
            // 
            // ConsequenceList
            // 
            this.ConsequenceList.HideSelection = false;
            this.ConsequenceList.Location = new System.Drawing.Point(17, 108);
            this.ConsequenceList.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ConsequenceList.Name = "ConsequenceList";
            this.ConsequenceList.Size = new System.Drawing.Size(337, 715);
            this.ConsequenceList.TabIndex = 2;
            this.ConsequenceList.UseCompatibleStateImageBehavior = false;
            this.ConsequenceList.View = System.Windows.Forms.View.Details;
            // 
            // SaveIMG
            // 
            this.SaveIMG.Location = new System.Drawing.Point(1070, 39);
            this.SaveIMG.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.SaveIMG.Name = "SaveIMG";
            this.SaveIMG.Size = new System.Drawing.Size(435, 59);
            this.SaveIMG.TabIndex = 3;
            this.SaveIMG.Text = "Save Image";
            this.SaveIMG.UseVisualStyleBackColor = true;
            this.SaveIMG.Click += new System.EventHandler(this.SaveIMG_Click);
            // 
            // SVIMGDL
            // 
            this.SVIMGDL.DefaultExt = "\".jpg\"";
            this.SVIMGDL.Filter = "\"Jpeg Image|*.jpg\"";
            this.SVIMGDL.Title = "\"Save Chart\"";
            // 
            // ValueLabelSwitch
            // 
            this.ValueLabelSwitch.AutoSize = true;
            this.ValueLabelSwitch.Checked = true;
            this.ValueLabelSwitch.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ValueLabelSwitch.Location = new System.Drawing.Point(917, 39);
            this.ValueLabelSwitch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ValueLabelSwitch.Name = "ValueLabelSwitch";
            this.ValueLabelSwitch.Size = new System.Drawing.Size(127, 22);
            this.ValueLabelSwitch.TabIndex = 5;
            this.ValueLabelSwitch.Text = "Value Switch";
            this.ValueLabelSwitch.UseVisualStyleBackColor = true;
            this.ValueLabelSwitch.CheckedChanged += new System.EventHandler(this.LabelChk_CheckedChanged);
            // 
            // NameLabelSwitch
            // 
            this.NameLabelSwitch.AutoSize = true;
            this.NameLabelSwitch.Location = new System.Drawing.Point(917, 69);
            this.NameLabelSwitch.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.NameLabelSwitch.Name = "NameLabelSwitch";
            this.NameLabelSwitch.Size = new System.Drawing.Size(127, 22);
            this.NameLabelSwitch.TabIndex = 6;
            this.NameLabelSwitch.Text = "Name Switch";
            this.NameLabelSwitch.UseVisualStyleBackColor = true;
            this.NameLabelSwitch.CheckedChanged += new System.EventHandler(this.CHKName_CheckedChanged);
            // 
            // ConsequenceGroupBox
            // 
            this.ConsequenceGroupBox.Controls.Add(this.CopyConsequencePropertiesToClickBoardBtn);
            this.ConsequenceGroupBox.Controls.Add(this.ConsequenceList);
            this.ConsequenceGroupBox.Location = new System.Drawing.Point(1580, 34);
            this.ConsequenceGroupBox.Name = "ConsequenceGroupBox";
            this.ConsequenceGroupBox.Size = new System.Drawing.Size(361, 831);
            this.ConsequenceGroupBox.TabIndex = 7;
            this.ConsequenceGroupBox.TabStop = false;
            this.ConsequenceGroupBox.Text = "Consequence";
            // 
            // DrawerGroupBox
            // 
            this.DrawerGroupBox.Controls.Add(this.MChart);
            this.DrawerGroupBox.Controls.Add(this.ValueLabelSwitch);
            this.DrawerGroupBox.Controls.Add(this.SaveIMG);
            this.DrawerGroupBox.Controls.Add(this.NameLabelSwitch);
            this.DrawerGroupBox.Location = new System.Drawing.Point(34, 34);
            this.DrawerGroupBox.Name = "DrawerGroupBox";
            this.DrawerGroupBox.Size = new System.Drawing.Size(1540, 831);
            this.DrawerGroupBox.TabIndex = 8;
            this.DrawerGroupBox.TabStop = false;
            this.DrawerGroupBox.Text = "Drawer";
            // 
            // CopyConsequencePropertiesToClickBoardBtn
            // 
            this.CopyConsequencePropertiesToClickBoardBtn.Location = new System.Drawing.Point(17, 39);
            this.CopyConsequencePropertiesToClickBoardBtn.Name = "CopyConsequencePropertiesToClickBoardBtn";
            this.CopyConsequencePropertiesToClickBoardBtn.Size = new System.Drawing.Size(337, 59);
            this.CopyConsequencePropertiesToClickBoardBtn.TabIndex = 3;
            this.CopyConsequencePropertiesToClickBoardBtn.Text = "Copy to Clickboard";
            this.CopyConsequencePropertiesToClickBoardBtn.UseVisualStyleBackColor = true;
            this.CopyConsequencePropertiesToClickBoardBtn.Click += new System.EventHandler(this.CopyConsequencePropertiesToClickBoardBtn_Click);
            // 
            // ChartDrawer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1978, 1410);
            this.Controls.Add(this.DrawerGroupBox);
            this.Controls.Add(this.ConsequenceGroupBox);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ChartDrawer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ChartDrawer";
            this.Load += new System.EventHandler(this.ChartDrawer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MChart)).EndInit();
            this.ConsequenceGroupBox.ResumeLayout(false);
            this.DrawerGroupBox.ResumeLayout(false);
            this.DrawerGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataVisualization.Charting.Chart MChart;
        private System.Windows.Forms.Button SaveIMG;
        private System.Windows.Forms.SaveFileDialog SVIMGDL;
        private System.Windows.Forms.CheckBox ValueLabelSwitch;
        private System.Windows.Forms.CheckBox NameLabelSwitch;
        public System.Windows.Forms.ListView ConsequenceList;
        private System.Windows.Forms.GroupBox ConsequenceGroupBox;
        private System.Windows.Forms.GroupBox DrawerGroupBox;
        private System.Windows.Forms.Button CopyConsequencePropertiesToClickBoardBtn;
    }
}