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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.MChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.ConsequenceList = new System.Windows.Forms.ListView();
            this.SaveIMG = new System.Windows.Forms.Button();
            this.SVIMGDL = new System.Windows.Forms.SaveFileDialog();
            this.ValueLabelSwitch = new System.Windows.Forms.CheckBox();
            this.NameLabelSwitch = new System.Windows.Forms.CheckBox();
            this.ConsequenceGroupBox = new System.Windows.Forms.GroupBox();
            this.CopyConsequencePropertiesToClickBoardBtn = new System.Windows.Forms.Button();
            this.DrawerGroupBox = new System.Windows.Forms.GroupBox();
            this.MarkBtn = new System.Windows.Forms.Button();
            this.MarkerFunctionTxb = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ChartMarkerStyleCbx = new System.Windows.Forms.ComboBox();
            this.PatternSetterBtn = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.MChart)).BeginInit();
            this.ConsequenceGroupBox.SuspendLayout();
            this.DrawerGroupBox.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // MChart
            // 
            chartArea1.Name = "ChartArea1";
            chartArea2.Name = "ChartArea2";
            this.MChart.ChartAreas.Add(chartArea1);
            this.MChart.ChartAreas.Add(chartArea2);
            legend1.Name = "Legend1";
            this.MChart.Legends.Add(legend1);
            this.MChart.Location = new System.Drawing.Point(14, 90);
            this.MChart.Margin = new System.Windows.Forms.Padding(4);
            this.MChart.Name = "MChart";
            this.MChart.Size = new System.Drawing.Size(1241, 1136);
            this.MChart.TabIndex = 1;
            this.MChart.Text = "chart1";
            this.MChart.Click += new System.EventHandler(this.MChart_Click);
            // 
            // ConsequenceList
            // 
            this.ConsequenceList.HideSelection = false;
            this.ConsequenceList.Location = new System.Drawing.Point(14, 90);
            this.ConsequenceList.Margin = new System.Windows.Forms.Padding(4);
            this.ConsequenceList.Name = "ConsequenceList";
            this.ConsequenceList.Size = new System.Drawing.Size(709, 595);
            this.ConsequenceList.TabIndex = 2;
            this.ConsequenceList.UseCompatibleStateImageBehavior = false;
            this.ConsequenceList.View = System.Windows.Forms.View.Details;
            // 
            // SaveIMG
            // 
            this.SaveIMG.Location = new System.Drawing.Point(891, 25);
            this.SaveIMG.Margin = new System.Windows.Forms.Padding(4);
            this.SaveIMG.Name = "SaveIMG";
            this.SaveIMG.Size = new System.Drawing.Size(350, 50);
            this.SaveIMG.TabIndex = 3;
            this.SaveIMG.Text = "Save Image";
            this.SaveIMG.UseVisualStyleBackColor = true;
            this.SaveIMG.Click += new System.EventHandler(this.SaveIMG_Click);
            // 
            // SVIMGDL
            // 
            this.SVIMGDL.DefaultExt = "\".jpg\"";
            this.SVIMGDL.Filter = "Tiff Image|*.tiff|EMF |*.emf";
            this.SVIMGDL.Title = "\"Save Chart\"";
            // 
            // ValueLabelSwitch
            // 
            this.ValueLabelSwitch.AutoSize = true;
            this.ValueLabelSwitch.Checked = true;
            this.ValueLabelSwitch.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ValueLabelSwitch.Location = new System.Drawing.Point(764, 31);
            this.ValueLabelSwitch.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.ValueLabelSwitch.Name = "ValueLabelSwitch";
            this.ValueLabelSwitch.Size = new System.Drawing.Size(105, 19);
            this.ValueLabelSwitch.TabIndex = 5;
            this.ValueLabelSwitch.Text = "Value Switch";
            this.ValueLabelSwitch.UseVisualStyleBackColor = true;
            this.ValueLabelSwitch.CheckedChanged += new System.EventHandler(this.LabelChk_CheckedChanged);
            // 
            // NameLabelSwitch
            // 
            this.NameLabelSwitch.AutoSize = true;
            this.NameLabelSwitch.Location = new System.Drawing.Point(764, 56);
            this.NameLabelSwitch.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.NameLabelSwitch.Name = "NameLabelSwitch";
            this.NameLabelSwitch.Size = new System.Drawing.Size(105, 19);
            this.NameLabelSwitch.TabIndex = 6;
            this.NameLabelSwitch.Text = "Name Switch";
            this.NameLabelSwitch.UseVisualStyleBackColor = true;
            this.NameLabelSwitch.CheckedChanged += new System.EventHandler(this.CHKName_CheckedChanged);
            // 
            // ConsequenceGroupBox
            // 
            this.ConsequenceGroupBox.Controls.Add(this.CopyConsequencePropertiesToClickBoardBtn);
            this.ConsequenceGroupBox.Controls.Add(this.ConsequenceList);
            this.ConsequenceGroupBox.Location = new System.Drawing.Point(12, 4);
            this.ConsequenceGroupBox.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.ConsequenceGroupBox.Name = "ConsequenceGroupBox";
            this.ConsequenceGroupBox.Padding = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.ConsequenceGroupBox.Size = new System.Drawing.Size(729, 691);
            this.ConsequenceGroupBox.TabIndex = 7;
            this.ConsequenceGroupBox.TabStop = false;
            this.ConsequenceGroupBox.Text = "Consequence";
            // 
            // CopyConsequencePropertiesToClickBoardBtn
            // 
            this.CopyConsequencePropertiesToClickBoardBtn.Location = new System.Drawing.Point(14, 31);
            this.CopyConsequencePropertiesToClickBoardBtn.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.CopyConsequencePropertiesToClickBoardBtn.Name = "CopyConsequencePropertiesToClickBoardBtn";
            this.CopyConsequencePropertiesToClickBoardBtn.Size = new System.Drawing.Size(709, 49);
            this.CopyConsequencePropertiesToClickBoardBtn.TabIndex = 3;
            this.CopyConsequencePropertiesToClickBoardBtn.Text = "Copy to Clickboard";
            this.CopyConsequencePropertiesToClickBoardBtn.UseVisualStyleBackColor = true;
            this.CopyConsequencePropertiesToClickBoardBtn.Click += new System.EventHandler(this.CopyConsequencePropertiesToClickBoardBtn_Click);
            // 
            // DrawerGroupBox
            // 
            this.DrawerGroupBox.Controls.Add(this.MarkBtn);
            this.DrawerGroupBox.Controls.Add(this.MarkerFunctionTxb);
            this.DrawerGroupBox.Controls.Add(this.label2);
            this.DrawerGroupBox.Controls.Add(this.label1);
            this.DrawerGroupBox.Controls.Add(this.ChartMarkerStyleCbx);
            this.DrawerGroupBox.Controls.Add(this.PatternSetterBtn);
            this.DrawerGroupBox.Controls.Add(this.MChart);
            this.DrawerGroupBox.Controls.Add(this.ValueLabelSwitch);
            this.DrawerGroupBox.Controls.Add(this.SaveIMG);
            this.DrawerGroupBox.Controls.Add(this.NameLabelSwitch);
            this.DrawerGroupBox.Location = new System.Drawing.Point(5, 20);
            this.DrawerGroupBox.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.DrawerGroupBox.Name = "DrawerGroupBox";
            this.DrawerGroupBox.Padding = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.DrawerGroupBox.Size = new System.Drawing.Size(1284, 691);
            this.DrawerGroupBox.TabIndex = 8;
            this.DrawerGroupBox.TabStop = false;
            this.DrawerGroupBox.Text = "Drawer";
            this.DrawerGroupBox.Enter += new System.EventHandler(this.DrawerGroupBox_Enter);
            // 
            // MarkBtn
            // 
            this.MarkBtn.Location = new System.Drawing.Point(548, 56);
            this.MarkBtn.Name = "MarkBtn";
            this.MarkBtn.Size = new System.Drawing.Size(105, 33);
            this.MarkBtn.TabIndex = 12;
            this.MarkBtn.Text = "Mark";
            this.MarkBtn.UseVisualStyleBackColor = true;
            this.MarkBtn.Click += new System.EventHandler(this.MarkBtn_Click);
            // 
            // MarkerFunctionTxb
            // 
            this.MarkerFunctionTxb.Location = new System.Drawing.Point(402, 58);
            this.MarkerFunctionTxb.Name = "MarkerFunctionTxb";
            this.MarkerFunctionTxb.Size = new System.Drawing.Size(140, 25);
            this.MarkerFunctionTxb.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(245, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 15);
            this.label2.TabIndex = 10;
            this.label2.Text = "Marker Function(S,idx): ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(245, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 15);
            this.label1.TabIndex = 9;
            this.label1.Text = "Chart Marker Styles: ";
            // 
            // ChartMarkerStyleCbx
            // 
            this.ChartMarkerStyleCbx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ChartMarkerStyleCbx.FormattingEnabled = true;
            this.ChartMarkerStyleCbx.Items.AddRange(new object[] {
            "None",
            "Square",
            "Circle",
            "Diamond",
            "Triangle",
            "Cross",
            "Star4",
            "Star5",
            "Star6",
            "Star10"});
            this.ChartMarkerStyleCbx.Location = new System.Drawing.Point(402, 22);
            this.ChartMarkerStyleCbx.Name = "ChartMarkerStyleCbx";
            this.ChartMarkerStyleCbx.Size = new System.Drawing.Size(251, 23);
            this.ChartMarkerStyleCbx.TabIndex = 8;
            // 
            // PatternSetterBtn
            // 
            this.PatternSetterBtn.Location = new System.Drawing.Point(14, 25);
            this.PatternSetterBtn.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.PatternSetterBtn.Name = "PatternSetterBtn";
            this.PatternSetterBtn.Size = new System.Drawing.Size(190, 50);
            this.PatternSetterBtn.TabIndex = 7;
            this.PatternSetterBtn.Text = "Pattern Modify";
            this.PatternSetterBtn.UseVisualStyleBackColor = true;
            this.PatternSetterBtn.Click += new System.EventHandler(this.PatternSetterBtn_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(24, 10);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1334, 750);
            this.tabControl1.TabIndex = 9;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.DrawerGroupBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage1.Size = new System.Drawing.Size(1326, 721);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Drawer";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.ConsequenceGroupBox);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage2.Size = new System.Drawing.Size(1326, 721);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Consequence";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // ChartDrawer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1369, 769);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ChartDrawer";
            this.Text = "ChartDrawer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChartDrawer_FormClosing);
            this.Load += new System.EventHandler(this.ChartDrawer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MChart)).EndInit();
            this.ConsequenceGroupBox.ResumeLayout(false);
            this.DrawerGroupBox.ResumeLayout(false);
            this.DrawerGroupBox.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataVisualization.Charting.Chart MChart;
        private System.Windows.Forms.Button SaveIMG;
        private System.Windows.Forms.SaveFileDialog SVIMGDL;
        public System.Windows.Forms.ListView ConsequenceList;
        private System.Windows.Forms.GroupBox ConsequenceGroupBox;
        private System.Windows.Forms.GroupBox DrawerGroupBox;
        private System.Windows.Forms.Button CopyConsequencePropertiesToClickBoardBtn;
        private System.Windows.Forms.Button PatternSetterBtn;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ChartMarkerStyleCbx;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button MarkBtn;
        private System.Windows.Forms.TextBox MarkerFunctionTxb;
        public System.Windows.Forms.CheckBox ValueLabelSwitch;
        public System.Windows.Forms.CheckBox NameLabelSwitch;
    }
}