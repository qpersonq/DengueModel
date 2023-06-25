namespace DengueGoogleMap
{
    partial class GoogleMapPresentation
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GoogleMapPresentation));
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.GMapDrawer = new GMap.NET.WindowsForms.GMapControl();
            this.DrawButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ModelFile = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CenterPositionLb = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.PointFidRange = new System.Windows.Forms.Label();
            this.CapturePictureBtn = new System.Windows.Forms.Button();
            this.ZoomLabelNumber = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.OpSOMBtn = new System.Windows.Forms.Button();
            this.ListPercentage = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.MapTypeCbx = new System.Windows.Forms.ComboBox();
            this.TestGMP = new System.Windows.Forms.Button();
            this.RefreshGMapBtn = new System.Windows.Forms.Button();
            this.LoadBTN = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.SELMarker = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.LayerListView = new System.Windows.Forms.ListView();
            this.label5 = new System.Windows.Forms.Label();
            this.PTFileOFD = new System.Windows.Forms.OpenFileDialog();
            this.SOMFileOFD = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.animationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.capturePictureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setMapSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showCenterToggleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeCenterPositionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CaptureMapFBD = new System.Windows.Forms.FolderBrowserDialog();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // GMapDrawer
            // 
            this.GMapDrawer.Bearing = 0F;
            this.GMapDrawer.CanDragMap = true;
            this.GMapDrawer.EmptyTileColor = System.Drawing.Color.Navy;
            this.GMapDrawer.GrayScaleMode = false;
            this.GMapDrawer.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.GMapDrawer.LevelsKeepInMemory = 5;
            this.GMapDrawer.Location = new System.Drawing.Point(20, 180);
            this.GMapDrawer.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.GMapDrawer.MarkersEnabled = true;
            this.GMapDrawer.MaxZoom = 2;
            this.GMapDrawer.MinZoom = 2;
            this.GMapDrawer.MouseWheelZoomEnabled = true;
            this.GMapDrawer.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.GMapDrawer.Name = "GMapDrawer";
            this.GMapDrawer.NegativeMode = false;
            this.GMapDrawer.PolygonsEnabled = true;
            this.GMapDrawer.RetryLoadTile = 0;
            this.GMapDrawer.RoutesEnabled = true;
            this.GMapDrawer.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.GMapDrawer.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.GMapDrawer.ShowTileGridLines = false;
            this.GMapDrawer.Size = new System.Drawing.Size(1547, 743);
            this.GMapDrawer.TabIndex = 0;
            this.GMapDrawer.Zoom = 0D;
            this.GMapDrawer.OnMarkerClick += new GMap.NET.WindowsForms.MarkerClick(this.GMapDrawer_OnMarkerClick);
            this.GMapDrawer.OnPositionChanged += new GMap.NET.PositionChanged(this.GMapDrawer_OnPositionChanged);
            this.GMapDrawer.Load += new System.EventHandler(this.GMapDrawer_Load);
            // 
            // DrawButton
            // 
            this.DrawButton.Location = new System.Drawing.Point(401, 43);
            this.DrawButton.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.DrawButton.Name = "DrawButton";
            this.DrawButton.Size = new System.Drawing.Size(76, 29);
            this.DrawButton.TabIndex = 11;
            this.DrawButton.Text = "Draw";
            this.DrawButton.UseVisualStyleBackColor = true;
            this.DrawButton.Click += new System.EventHandler(this.DrawButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 18);
            this.label2.TabIndex = 10;
            this.label2.Text = "Fid Range:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 18);
            this.label4.TabIndex = 15;
            this.label4.Text = "Model Point";
            // 
            // ModelFile
            // 
            this.ModelFile.Location = new System.Drawing.Point(107, 4);
            this.ModelFile.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.ModelFile.Name = "ModelFile";
            this.ModelFile.Size = new System.Drawing.Size(288, 29);
            this.ModelFile.TabIndex = 14;
            this.ModelFile.Text = "K:\\FullDengue\\Dengue2010\\sel\\ParClEN6\\Prediction13103_13957\\CaseReport\\Case0.9500" +
    "00.csv";
            this.ModelFile.TextChanged += new System.EventHandler(this.ModelFile_TextChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.CenterPositionLb);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.PointFidRange);
            this.panel1.Controls.Add(this.CapturePictureBtn);
            this.panel1.Controls.Add(this.ZoomLabelNumber);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.OpSOMBtn);
            this.panel1.Controls.Add(this.ListPercentage);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.MapTypeCbx);
            this.panel1.Controls.Add(this.TestGMP);
            this.panel1.Controls.Add(this.RefreshGMapBtn);
            this.panel1.Controls.Add(this.LoadBTN);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.SELMarker);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.LayerListView);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.DrawButton);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.ModelFile);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(20, 38);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1547, 132);
            this.panel1.TabIndex = 16;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // CenterPositionLb
            // 
            this.CenterPositionLb.AutoSize = true;
            this.CenterPositionLb.Location = new System.Drawing.Point(1273, 58);
            this.CenterPositionLb.Name = "CenterPositionLb";
            this.CenterPositionLb.Size = new System.Drawing.Size(119, 18);
            this.CenterPositionLb.TabIndex = 34;
            this.CenterPositionLb.Text = "Center Position:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1150, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 18);
            this.label1.TabIndex = 33;
            this.label1.Text = "Center Position:";
            // 
            // PointFidRange
            // 
            this.PointFidRange.AutoSize = true;
            this.PointFidRange.Location = new System.Drawing.Point(81, 48);
            this.PointFidRange.Name = "PointFidRange";
            this.PointFidRange.Size = new System.Drawing.Size(78, 18);
            this.PointFidRange.TabIndex = 32;
            this.PointFidRange.Text = "Fid Range";
            // 
            // CapturePictureBtn
            // 
            this.CapturePictureBtn.Location = new System.Drawing.Point(1359, 89);
            this.CapturePictureBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CapturePictureBtn.Name = "CapturePictureBtn";
            this.CapturePictureBtn.Size = new System.Drawing.Size(140, 35);
            this.CapturePictureBtn.TabIndex = 31;
            this.CapturePictureBtn.Text = "Capture Map";
            this.CapturePictureBtn.UseVisualStyleBackColor = true;
            this.CapturePictureBtn.Click += new System.EventHandler(this.CapturePictureBtn_Click);
            // 
            // ZoomLabelNumber
            // 
            this.ZoomLabelNumber.AutoSize = true;
            this.ZoomLabelNumber.Location = new System.Drawing.Point(1116, 58);
            this.ZoomLabelNumber.Name = "ZoomLabelNumber";
            this.ZoomLabelNumber.Size = new System.Drawing.Size(24, 18);
            this.ZoomLabelNumber.TabIndex = 30;
            this.ZoomLabelNumber.Text = "10";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(1064, 58);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 18);
            this.label8.TabIndex = 29;
            this.label8.Text = "Zoom: ";
            // 
            // OpSOMBtn
            // 
            this.OpSOMBtn.Location = new System.Drawing.Point(401, 6);
            this.OpSOMBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.OpSOMBtn.Name = "OpSOMBtn";
            this.OpSOMBtn.Size = new System.Drawing.Size(76, 30);
            this.OpSOMBtn.TabIndex = 27;
            this.OpSOMBtn.Text = "Open";
            this.OpSOMBtn.UseVisualStyleBackColor = true;
            this.OpSOMBtn.Click += new System.EventHandler(this.OpSOMBtn_Click);
            // 
            // ListPercentage
            // 
            this.ListPercentage.FormattingEnabled = true;
            this.ListPercentage.ItemHeight = 18;
            this.ListPercentage.Location = new System.Drawing.Point(586, 25);
            this.ListPercentage.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.ListPercentage.Name = "ListPercentage";
            this.ListPercentage.Size = new System.Drawing.Size(99, 94);
            this.ListPercentage.TabIndex = 17;
            this.ListPercentage.SelectedIndexChanged += new System.EventHandler(this.ListPercentage_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1064, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 18);
            this.label3.TabIndex = 26;
            this.label3.Text = "Map Provider";
            // 
            // MapTypeCbx
            // 
            this.MapTypeCbx.FormattingEnabled = true;
            this.MapTypeCbx.Location = new System.Drawing.Point(1067, 25);
            this.MapTypeCbx.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MapTypeCbx.Name = "MapTypeCbx";
            this.MapTypeCbx.Size = new System.Drawing.Size(325, 26);
            this.MapTypeCbx.TabIndex = 25;
            this.MapTypeCbx.Text = "GoogleMap";
            this.MapTypeCbx.SelectedIndexChanged += new System.EventHandler(this.MapTypeCbx_SelectedIndexChanged);
            // 
            // TestGMP
            // 
            this.TestGMP.Location = new System.Drawing.Point(1067, 89);
            this.TestGMP.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.TestGMP.Name = "TestGMP";
            this.TestGMP.Size = new System.Drawing.Size(140, 35);
            this.TestGMP.TabIndex = 24;
            this.TestGMP.Text = "Test Google Map";
            this.TestGMP.UseVisualStyleBackColor = true;
            this.TestGMP.Click += new System.EventHandler(this.TestGMP_Click);
            // 
            // RefreshGMapBtn
            // 
            this.RefreshGMapBtn.Location = new System.Drawing.Point(1213, 89);
            this.RefreshGMapBtn.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.RefreshGMapBtn.Name = "RefreshGMapBtn";
            this.RefreshGMapBtn.Size = new System.Drawing.Size(140, 35);
            this.RefreshGMapBtn.TabIndex = 18;
            this.RefreshGMapBtn.Text = "Refresh GMap";
            this.RefreshGMapBtn.UseVisualStyleBackColor = true;
            this.RefreshGMapBtn.Click += new System.EventHandler(this.RefreshGMapBtn_Click);
            // 
            // LoadBTN
            // 
            this.LoadBTN.Location = new System.Drawing.Point(317, 43);
            this.LoadBTN.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.LoadBTN.Name = "LoadBTN";
            this.LoadBTN.Size = new System.Drawing.Size(78, 29);
            this.LoadBTN.TabIndex = 23;
            this.LoadBTN.Text = "Load";
            this.LoadBTN.UseVisualStyleBackColor = true;
            this.LoadBTN.Click += new System.EventHandler(this.LoadBTN_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(793, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(99, 18);
            this.label7.TabIndex = 22;
            this.label7.Text = "Selected Fid:";
            // 
            // SELMarker
            // 
            this.SELMarker.FormattingEnabled = true;
            this.SELMarker.ItemHeight = 18;
            this.SELMarker.Location = new System.Drawing.Point(796, 25);
            this.SELMarker.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.SELMarker.Name = "SELMarker";
            this.SELMarker.Size = new System.Drawing.Size(99, 94);
            this.SELMarker.TabIndex = 21;
            this.SELMarker.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.SELMarker_MouseDoubleClick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(691, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 18);
            this.label6.TabIndex = 20;
            this.label6.Text = "Layer:";
            // 
            // LayerListView
            // 
            this.LayerListView.CheckBoxes = true;
            this.LayerListView.HideSelection = false;
            this.LayerListView.Location = new System.Drawing.Point(691, 25);
            this.LayerListView.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.LayerListView.Name = "LayerListView";
            this.LayerListView.Size = new System.Drawing.Size(99, 94);
            this.LayerListView.TabIndex = 19;
            this.LayerListView.UseCompatibleStateImageBehavior = false;
            this.LayerListView.View = System.Windows.Forms.View.List;
            this.LayerListView.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.LayerListView_ItemCheck);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(586, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 18);
            this.label5.TabIndex = 18;
            this.label5.Text = "Percentage:";
            // 
            // PTFileOFD
            // 
            this.PTFileOFD.FileName = "openFileDialog1";
            this.PTFileOFD.Filter = "HCSV File(*.hcsv) | *.hcsv";
            // 
            // SOMFileOFD
            // 
            this.SOMFileOFD.FileName = "openFileDialog1";
            this.SOMFileOFD.Filter = "CSV File (*.csv) |*.csv";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.animationToolStripMenuItem,
            this.capturePictureToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1601, 31);
            this.menuStrip1.TabIndex = 17;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // animationToolStripMenuItem
            // 
            this.animationToolStripMenuItem.Name = "animationToolStripMenuItem";
            this.animationToolStripMenuItem.Size = new System.Drawing.Size(111, 27);
            this.animationToolStripMenuItem.Text = "Animation";
            this.animationToolStripMenuItem.Click += new System.EventHandler(this.animationToolStripMenuItem_Click);
            // 
            // capturePictureToolStripMenuItem
            // 
            this.capturePictureToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pathToolStripMenuItem});
            this.capturePictureToolStripMenuItem.Name = "capturePictureToolStripMenuItem";
            this.capturePictureToolStripMenuItem.Size = new System.Drawing.Size(155, 27);
            this.capturePictureToolStripMenuItem.Text = "Capture Picture";
            // 
            // pathToolStripMenuItem
            // 
            this.pathToolStripMenuItem.Name = "pathToolStripMenuItem";
            this.pathToolStripMenuItem.Size = new System.Drawing.Size(131, 30);
            this.pathToolStripMenuItem.Text = "Path";
            this.pathToolStripMenuItem.Click += new System.EventHandler(this.pathToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setMapSizeToolStripMenuItem,
            this.showCenterToggleToolStripMenuItem,
            this.changeCenterPositionToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(67, 27);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // setMapSizeToolStripMenuItem
            // 
            this.setMapSizeToolStripMenuItem.Name = "setMapSizeToolStripMenuItem";
            this.setMapSizeToolStripMenuItem.Size = new System.Drawing.Size(293, 30);
            this.setMapSizeToolStripMenuItem.Text = "Set Map Size";
            this.setMapSizeToolStripMenuItem.Click += new System.EventHandler(this.setMapSizeToolStripMenuItem_Click);
            // 
            // showCenterToggleToolStripMenuItem
            // 
            this.showCenterToggleToolStripMenuItem.Name = "showCenterToggleToolStripMenuItem";
            this.showCenterToggleToolStripMenuItem.Size = new System.Drawing.Size(293, 30);
            this.showCenterToggleToolStripMenuItem.Text = "Show Center Toggle";
            this.showCenterToggleToolStripMenuItem.Click += new System.EventHandler(this.showCenterToggleToolStripMenuItem_Click);
            // 
            // changeCenterPositionToolStripMenuItem
            // 
            this.changeCenterPositionToolStripMenuItem.Name = "changeCenterPositionToolStripMenuItem";
            this.changeCenterPositionToolStripMenuItem.Size = new System.Drawing.Size(293, 30);
            this.changeCenterPositionToolStripMenuItem.Text = "Change Center Position";
            this.changeCenterPositionToolStripMenuItem.Click += new System.EventHandler(this.changeCenterPositionToolStripMenuItem_Click);
            // 
            // GoogleMapPresentation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1601, 964);
            this.Controls.Add(this.GMapDrawer);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.Name = "GoogleMapPresentation";
            this.Text = "Dengue Buffers Drawed by Online Maps ";
            this.Load += new System.EventHandler(this.RefreshGMapBtn_Click);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button DrawButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox ModelFile;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox ListPercentage;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListView LayerListView;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListBox SELMarker;
        private System.Windows.Forms.Button LoadBTN;
        private System.Windows.Forms.Button RefreshGMapBtn;
        private System.Windows.Forms.Button TestGMP;
        private System.Windows.Forms.ComboBox MapTypeCbx;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button OpSOMBtn;
        private System.Windows.Forms.OpenFileDialog PTFileOFD;
        private System.Windows.Forms.OpenFileDialog SOMFileOFD;
        private System.Windows.Forms.Label ZoomLabelNumber;
        private System.Windows.Forms.Label label8;
        public GMap.NET.WindowsForms.GMapControl GMapDrawer;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem animationToolStripMenuItem;
        private System.Windows.Forms.Button CapturePictureBtn;
        private System.Windows.Forms.ToolStripMenuItem capturePictureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pathToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog CaptureMapFBD;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setMapSizeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showCenterToggleToolStripMenuItem;
        private System.Windows.Forms.Label PointFidRange;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label CenterPositionLb;
        private System.Windows.Forms.ToolStripMenuItem changeCenterPositionToolStripMenuItem;
    }
}

