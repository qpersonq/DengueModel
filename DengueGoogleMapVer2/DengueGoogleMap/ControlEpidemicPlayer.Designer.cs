namespace DengueGoogleMap
{
    partial class ControlEpidemicPlayer
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlEpidemicPlayer));
            this.SelectingScrollBar = new System.Windows.Forms.HScrollBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.ModeComboBox = new System.Windows.Forms.ComboBox();
            this.SelectingScrollValueTextBox = new System.Windows.Forms.TextBox();
            this.SCRMItemLabel = new System.Windows.Forms.Label();
            this.SCRRItemLabel = new System.Windows.Forms.Label();
            this.SCRLItemLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.DateTimeModeRangeDays = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.CaseNumberModeRange = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.LoopPlayCkb = new System.Windows.Forms.CheckBox();
            this.AutoScrollValueTbx = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.SecondPerFrameTbx = new System.Windows.Forms.TextBox();
            this.AnimationStopBtn = new System.Windows.Forms.Button();
            this.AnimationPauseBtn = new System.Windows.Forms.Button();
            this.AnimationPlayBtn = new System.Windows.Forms.Button();
            this.AutoMovingWhenScrollingChk = new System.Windows.Forms.CheckBox();
            this.AutoPlayTimer = new System.Windows.Forms.Timer(this.components);
            this.NoShowBackwardPointChk = new System.Windows.Forms.CheckBox();
            this.NoShowPointChk = new System.Windows.Forms.CheckBox();
            this.BufferNoStrokeChk = new System.Windows.Forms.CheckBox();
            this.TransparentBackwardBufferChk = new System.Windows.Forms.CheckBox();
            this.TransparentForwardBufferChk = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.PtEqPoly = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.PredictiveRaterichTextBox = new System.Windows.Forms.RichTextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.RecordPredictiveRateChk = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // SelectingScrollBar
            // 
            this.SelectingScrollBar.LargeChange = 1;
            this.SelectingScrollBar.Location = new System.Drawing.Point(17, 54);
            this.SelectingScrollBar.Name = "SelectingScrollBar";
            this.SelectingScrollBar.Size = new System.Drawing.Size(746, 43);
            this.SelectingScrollBar.TabIndex = 0;
            this.SelectingScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.SelectingScrollBar_Scroll);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Lavender;
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.ModeComboBox);
            this.panel1.Controls.Add(this.SelectingScrollValueTextBox);
            this.panel1.Controls.Add(this.SCRMItemLabel);
            this.panel1.Controls.Add(this.SCRRItemLabel);
            this.panel1.Controls.Add(this.SCRLItemLabel);
            this.panel1.Controls.Add(this.SelectingScrollBar);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 22);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(777, 229);
            this.panel1.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 179);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(105, 18);
            this.label7.TabIndex = 9;
            this.label7.Text = "Scroll Value: ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(389, 179);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 18);
            this.label6.TabIndex = 8;
            this.label6.Text = "Mode:";
            // 
            // ModeComboBox
            // 
            this.ModeComboBox.Items.AddRange(new object[] {
            "Date Time Mode",
            "Cases Number Mode"});
            this.ModeComboBox.Location = new System.Drawing.Point(462, 179);
            this.ModeComboBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ModeComboBox.Name = "ModeComboBox";
            this.ModeComboBox.Size = new System.Drawing.Size(291, 26);
            this.ModeComboBox.TabIndex = 7;
            this.ModeComboBox.Text = "Date Time Mode";
            this.ModeComboBox.SelectedIndexChanged += new System.EventHandler(this.ModeComboBox_SelectedIndexChanged);
            // 
            // SelectingScrollValueTextBox
            // 
            this.SelectingScrollValueTextBox.Enabled = false;
            this.SelectingScrollValueTextBox.Location = new System.Drawing.Point(125, 179);
            this.SelectingScrollValueTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SelectingScrollValueTextBox.Name = "SelectingScrollValueTextBox";
            this.SelectingScrollValueTextBox.Size = new System.Drawing.Size(237, 29);
            this.SelectingScrollValueTextBox.TabIndex = 6;
            this.SelectingScrollValueTextBox.Text = "0";
            this.SelectingScrollValueTextBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // SCRMItemLabel
            // 
            this.SCRMItemLabel.AutoSize = true;
            this.SCRMItemLabel.Location = new System.Drawing.Point(326, 144);
            this.SCRMItemLabel.Name = "SCRMItemLabel";
            this.SCRMItemLabel.Size = new System.Drawing.Size(125, 18);
            this.SCRMItemLabel.TabIndex = 5;
            this.SCRMItemLabel.Text = "SCRMItemLabel";
            this.SCRMItemLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // SCRRItemLabel
            // 
            this.SCRRItemLabel.AutoSize = true;
            this.SCRRItemLabel.Location = new System.Drawing.Point(642, 144);
            this.SCRRItemLabel.Name = "SCRRItemLabel";
            this.SCRRItemLabel.Size = new System.Drawing.Size(121, 18);
            this.SCRRItemLabel.TabIndex = 4;
            this.SCRRItemLabel.Text = "SCRRItemLabel";
            this.SCRRItemLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.SCRRItemLabel.Click += new System.EventHandler(this.label3_Click);
            // 
            // SCRLItemLabel
            // 
            this.SCRLItemLabel.AutoSize = true;
            this.SCRLItemLabel.Location = new System.Drawing.Point(14, 144);
            this.SCRLItemLabel.Name = "SCRLItemLabel";
            this.SCRLItemLabel.Size = new System.Drawing.Size(120, 18);
            this.SCRLItemLabel.TabIndex = 3;
            this.SCRLItemLabel.Text = "SCRLItemLabel";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "Selecting Scroll Bar";
            // 
            // DateTimeModeRangeDays
            // 
            this.DateTimeModeRangeDays.Location = new System.Drawing.Point(119, 22);
            this.DateTimeModeRangeDays.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DateTimeModeRangeDays.Name = "DateTimeModeRangeDays";
            this.DateTimeModeRangeDays.Size = new System.Drawing.Size(85, 29);
            this.DateTimeModeRangeDays.TabIndex = 7;
            this.DateTimeModeRangeDays.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 18);
            this.label2.TabIndex = 3;
            this.label2.Text = "Range (days) :";
            // 
            // CaseNumberModeRange
            // 
            this.CaseNumberModeRange.Location = new System.Drawing.Point(119, 20);
            this.CaseNumberModeRange.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CaseNumberModeRange.Name = "CaseNumberModeRange";
            this.CaseNumberModeRange.Size = new System.Drawing.Size(85, 29);
            this.CaseNumberModeRange.TabIndex = 7;
            this.CaseNumberModeRange.Text = "20";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 18);
            this.label5.TabIndex = 3;
            this.label5.Text = "Cases :";
            // 
            // LoopPlayCkb
            // 
            this.LoopPlayCkb.AutoSize = true;
            this.LoopPlayCkb.Location = new System.Drawing.Point(8, 55);
            this.LoopPlayCkb.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.LoopPlayCkb.Name = "LoopPlayCkb";
            this.LoopPlayCkb.Size = new System.Drawing.Size(73, 22);
            this.LoopPlayCkb.TabIndex = 5;
            this.LoopPlayCkb.Text = "Loop ";
            this.LoopPlayCkb.UseVisualStyleBackColor = true;
            // 
            // AutoScrollValueTbx
            // 
            this.AutoScrollValueTbx.Location = new System.Drawing.Point(474, 16);
            this.AutoScrollValueTbx.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.AutoScrollValueTbx.Name = "AutoScrollValueTbx";
            this.AutoScrollValueTbx.Size = new System.Drawing.Size(90, 29);
            this.AutoScrollValueTbx.TabIndex = 15;
            this.AutoScrollValueTbx.Text = "2";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(334, 19);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(96, 18);
            this.label10.TabIndex = 14;
            this.label10.Text = "Scroll value:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(334, 68);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(135, 18);
            this.label9.TabIndex = 13;
            this.label9.Text = "Second per frame:";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // SecondPerFrameTbx
            // 
            this.SecondPerFrameTbx.Location = new System.Drawing.Point(474, 65);
            this.SecondPerFrameTbx.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SecondPerFrameTbx.Name = "SecondPerFrameTbx";
            this.SecondPerFrameTbx.Size = new System.Drawing.Size(90, 29);
            this.SecondPerFrameTbx.TabIndex = 12;
            this.SecondPerFrameTbx.Text = "1";
            // 
            // AnimationStopBtn
            // 
            this.AnimationStopBtn.Font = new System.Drawing.Font("新細明體", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.AnimationStopBtn.Location = new System.Drawing.Point(680, 16);
            this.AnimationStopBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.AnimationStopBtn.Name = "AnimationStopBtn";
            this.AnimationStopBtn.Size = new System.Drawing.Size(80, 79);
            this.AnimationStopBtn.TabIndex = 11;
            this.AnimationStopBtn.Text = " ▉";
            this.AnimationStopBtn.UseVisualStyleBackColor = true;
            this.AnimationStopBtn.Click += new System.EventHandler(this.AnimationStopBtn_Click);
            // 
            // AnimationPauseBtn
            // 
            this.AnimationPauseBtn.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.AnimationPauseBtn.Location = new System.Drawing.Point(590, 16);
            this.AnimationPauseBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.AnimationPauseBtn.Name = "AnimationPauseBtn";
            this.AnimationPauseBtn.Size = new System.Drawing.Size(80, 79);
            this.AnimationPauseBtn.TabIndex = 10;
            this.AnimationPauseBtn.Text = " ▌▌";
            this.AnimationPauseBtn.UseVisualStyleBackColor = true;
            this.AnimationPauseBtn.Click += new System.EventHandler(this.AnimationPauseBtn_Click);
            // 
            // AnimationPlayBtn
            // 
            this.AnimationPlayBtn.BackColor = System.Drawing.Color.Lime;
            this.AnimationPlayBtn.Font = new System.Drawing.Font("新細明體", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.AnimationPlayBtn.Location = new System.Drawing.Point(248, 16);
            this.AnimationPlayBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.AnimationPlayBtn.Name = "AnimationPlayBtn";
            this.AnimationPlayBtn.Size = new System.Drawing.Size(80, 79);
            this.AnimationPlayBtn.TabIndex = 9;
            this.AnimationPlayBtn.Text = "▶";
            this.AnimationPlayBtn.UseVisualStyleBackColor = false;
            this.AnimationPlayBtn.Click += new System.EventHandler(this.AnimationPlayBtn_Click);
            // 
            // AutoMovingWhenScrollingChk
            // 
            this.AutoMovingWhenScrollingChk.AutoSize = true;
            this.AutoMovingWhenScrollingChk.Location = new System.Drawing.Point(8, 28);
            this.AutoMovingWhenScrollingChk.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.AutoMovingWhenScrollingChk.Name = "AutoMovingWhenScrollingChk";
            this.AutoMovingWhenScrollingChk.Size = new System.Drawing.Size(228, 22);
            this.AutoMovingWhenScrollingChk.TabIndex = 8;
            this.AutoMovingWhenScrollingChk.Text = "Auto moving when scrolling";
            this.AutoMovingWhenScrollingChk.UseVisualStyleBackColor = true;
            // 
            // AutoPlayTimer
            // 
            this.AutoPlayTimer.Tick += new System.EventHandler(this.AutoPlayTimer_Tick);
            // 
            // NoShowBackwardPointChk
            // 
            this.NoShowBackwardPointChk.AutoSize = true;
            this.NoShowBackwardPointChk.Location = new System.Drawing.Point(204, 30);
            this.NoShowBackwardPointChk.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.NoShowBackwardPointChk.Name = "NoShowBackwardPointChk";
            this.NoShowBackwardPointChk.Size = new System.Drawing.Size(237, 22);
            this.NoShowBackwardPointChk.TabIndex = 11;
            this.NoShowBackwardPointChk.Text = "Transparent Backward Points";
            this.NoShowBackwardPointChk.UseVisualStyleBackColor = true;
            // 
            // NoShowPointChk
            // 
            this.NoShowPointChk.AutoSize = true;
            this.NoShowPointChk.Location = new System.Drawing.Point(8, 62);
            this.NoShowPointChk.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.NoShowPointChk.Name = "NoShowPointChk";
            this.NoShowPointChk.Size = new System.Drawing.Size(143, 22);
            this.NoShowPointChk.TabIndex = 10;
            this.NoShowPointChk.Text = "No Show Points";
            this.NoShowPointChk.UseVisualStyleBackColor = true;
            // 
            // BufferNoStrokeChk
            // 
            this.BufferNoStrokeChk.AutoSize = true;
            this.BufferNoStrokeChk.Location = new System.Drawing.Point(8, 30);
            this.BufferNoStrokeChk.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BufferNoStrokeChk.Name = "BufferNoStrokeChk";
            this.BufferNoStrokeChk.Size = new System.Drawing.Size(153, 22);
            this.BufferNoStrokeChk.TabIndex = 9;
            this.BufferNoStrokeChk.Text = "Buffer No Stroke";
            this.BufferNoStrokeChk.UseVisualStyleBackColor = true;
            this.BufferNoStrokeChk.CheckedChanged += new System.EventHandler(this.BufferNoStrokeChk_CheckedChanged);
            // 
            // TransparentBackwardBufferChk
            // 
            this.TransparentBackwardBufferChk.AutoSize = true;
            this.TransparentBackwardBufferChk.Location = new System.Drawing.Point(204, 62);
            this.TransparentBackwardBufferChk.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TransparentBackwardBufferChk.Name = "TransparentBackwardBufferChk";
            this.TransparentBackwardBufferChk.Size = new System.Drawing.Size(247, 22);
            this.TransparentBackwardBufferChk.TabIndex = 12;
            this.TransparentBackwardBufferChk.Text = "Transparent Backward Buffers";
            this.TransparentBackwardBufferChk.UseVisualStyleBackColor = true;
            // 
            // TransparentForwardBufferChk
            // 
            this.TransparentForwardBufferChk.AutoSize = true;
            this.TransparentForwardBufferChk.Location = new System.Drawing.Point(204, 96);
            this.TransparentForwardBufferChk.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TransparentForwardBufferChk.Name = "TransparentForwardBufferChk";
            this.TransparentForwardBufferChk.Size = new System.Drawing.Size(235, 22);
            this.TransparentForwardBufferChk.TabIndex = 13;
            this.TransparentForwardBufferChk.Text = "Transparent Forward Buffers";
            this.TransparentForwardBufferChk.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.DateTimeModeRangeDays);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(567, 270);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(225, 60);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Date Time Mode";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.CaseNumberModeRange);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(567, 338);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(225, 60);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Case Number Mode";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.PtEqPoly);
            this.groupBox3.Controls.Add(this.TransparentForwardBufferChk);
            this.groupBox3.Controls.Add(this.BufferNoStrokeChk);
            this.groupBox3.Controls.Add(this.TransparentBackwardBufferChk);
            this.groupBox3.Controls.Add(this.NoShowPointChk);
            this.groupBox3.Controls.Add(this.NoShowBackwardPointChk);
            this.groupBox3.Location = new System.Drawing.Point(12, 270);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Size = new System.Drawing.Size(548, 127);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Draw Controller";
            // 
            // PtEqPoly
            // 
            this.PtEqPoly.AutoSize = true;
            this.PtEqPoly.Location = new System.Drawing.Point(8, 96);
            this.PtEqPoly.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.PtEqPoly.Name = "PtEqPoly";
            this.PtEqPoly.Size = new System.Drawing.Size(130, 22);
            this.PtEqPoly.TabIndex = 14;
            this.PtEqPoly.Text = "Pt Eq Polygon";
            this.PtEqPoly.UseVisualStyleBackColor = true;
            this.PtEqPoly.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.AnimationStopBtn);
            this.groupBox4.Controls.Add(this.SecondPerFrameTbx);
            this.groupBox4.Controls.Add(this.AnimationPauseBtn);
            this.groupBox4.Controls.Add(this.AutoScrollValueTbx);
            this.groupBox4.Controls.Add(this.LoopPlayCkb);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.AutoMovingWhenScrollingChk);
            this.groupBox4.Controls.Add(this.AnimationPlayBtn);
            this.groupBox4.Location = new System.Drawing.Point(17, 406);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox4.Size = new System.Drawing.Size(777, 102);
            this.groupBox4.TabIndex = 12;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Animations Controller";
            // 
            // PredictiveRaterichTextBox
            // 
            this.PredictiveRaterichTextBox.BackColor = System.Drawing.Color.Black;
            this.PredictiveRaterichTextBox.ForeColor = System.Drawing.Color.White;
            this.PredictiveRaterichTextBox.Location = new System.Drawing.Point(125, 20);
            this.PredictiveRaterichTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.PredictiveRaterichTextBox.Name = "PredictiveRaterichTextBox";
            this.PredictiveRaterichTextBox.Size = new System.Drawing.Size(634, 56);
            this.PredictiveRaterichTextBox.TabIndex = 13;
            this.PredictiveRaterichTextBox.Text = "";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.RecordPredictiveRateChk);
            this.groupBox5.Controls.Add(this.PredictiveRaterichTextBox);
            this.groupBox5.Location = new System.Drawing.Point(17, 525);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox5.Size = new System.Drawing.Size(777, 84);
            this.groupBox5.TabIndex = 14;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Show predictive rate";
            // 
            // RecordPredictiveRateChk
            // 
            this.RecordPredictiveRateChk.AutoSize = true;
            this.RecordPredictiveRateChk.Location = new System.Drawing.Point(8, 47);
            this.RecordPredictiveRateChk.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.RecordPredictiveRateChk.Name = "RecordPredictiveRateChk";
            this.RecordPredictiveRateChk.Size = new System.Drawing.Size(104, 22);
            this.RecordPredictiveRateChk.TabIndex = 14;
            this.RecordPredictiveRateChk.Text = "Recording";
            this.RecordPredictiveRateChk.UseVisualStyleBackColor = true;
            this.RecordPredictiveRateChk.CheckedChanged += new System.EventHandler(this.RecordPredictiveRateChk_CheckedChanged);
            // 
            // ControlEpidemicPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 633);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ControlEpidemicPlayer";
            this.Text = "ControlEpidemicPlayer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ControlEpidemicPlayer_FormClosing);
            this.Load += new System.EventHandler(this.ControlEpidemicPlayer_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.HScrollBar SelectingScrollBar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label SCRRItemLabel;
        private System.Windows.Forms.Label SCRLItemLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label SCRMItemLabel;
        private System.Windows.Forms.TextBox SelectingScrollValueTextBox;
        private System.Windows.Forms.ComboBox ModeComboBox;
        private System.Windows.Forms.TextBox DateTimeModeRangeDays;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox CaseNumberModeRange;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button AnimationStopBtn;
        private System.Windows.Forms.Button AnimationPauseBtn;
        private System.Windows.Forms.Button AnimationPlayBtn;
        private System.Windows.Forms.CheckBox AutoMovingWhenScrollingChk;
        private System.Windows.Forms.TextBox AutoScrollValueTbx;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox SecondPerFrameTbx;
        private System.Windows.Forms.Timer AutoPlayTimer;
        private System.Windows.Forms.CheckBox LoopPlayCkb;
        private System.Windows.Forms.CheckBox NoShowPointChk;
        private System.Windows.Forms.CheckBox BufferNoStrokeChk;
        private System.Windows.Forms.CheckBox NoShowBackwardPointChk;
        private System.Windows.Forms.CheckBox TransparentBackwardBufferChk;
        private System.Windows.Forms.CheckBox TransparentForwardBufferChk;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RichTextBox PredictiveRaterichTextBox;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox RecordPredictiveRateChk;
        private System.Windows.Forms.CheckBox PtEqPoly;
    }
}