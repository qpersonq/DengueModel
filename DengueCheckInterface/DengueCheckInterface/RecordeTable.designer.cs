namespace DenInterface
{
    partial class RecordeTable
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
            this.TableRecorderConatiner = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.CKall = new System.Windows.Forms.Button();
            this.Clall = new System.Windows.Forms.Button();
            this.ViewCriticalBtn = new System.Windows.Forms.Button();
            this.CPPAST = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.NewTableComboBoxY = new System.Windows.Forms.ComboBox();
            this.NewTableComboBoxX = new System.Windows.Forms.ComboBox();
            this.InterpolationBtn = new System.Windows.Forms.Button();
            this.NewTableBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TableRecorderConatiner
            // 
            this.TableRecorderConatiner.CheckBoxes = true;
            this.TableRecorderConatiner.FullRowSelect = true;
            this.TableRecorderConatiner.GridLines = true;
            this.TableRecorderConatiner.HideSelection = false;
            this.TableRecorderConatiner.Location = new System.Drawing.Point(28, 67);
            this.TableRecorderConatiner.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TableRecorderConatiner.Name = "TableRecorderConatiner";
            this.TableRecorderConatiner.Size = new System.Drawing.Size(1006, 531);
            this.TableRecorderConatiner.TabIndex = 20;
            this.TableRecorderConatiner.UseCompatibleStateImageBehavior = false;
            this.TableRecorderConatiner.View = System.Windows.Forms.View.Details;
            this.TableRecorderConatiner.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.TableRecorderConatiner_ItemChecked);
            this.TableRecorderConatiner.SelectedIndexChanged += new System.EventHandler(this.TableRecorderConatiner_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 18);
            this.label1.TabIndex = 21;
            this.label1.Text = "label1";
            // 
            // CKall
            // 
            this.CKall.Location = new System.Drawing.Point(28, 622);
            this.CKall.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CKall.Name = "CKall";
            this.CKall.Size = new System.Drawing.Size(138, 35);
            this.CKall.TabIndex = 22;
            this.CKall.Text = "Check All";
            this.CKall.UseVisualStyleBackColor = true;
            this.CKall.Click += new System.EventHandler(this.CKall_Click);
            // 
            // Clall
            // 
            this.Clall.Location = new System.Drawing.Point(189, 622);
            this.Clall.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Clall.Name = "Clall";
            this.Clall.Size = new System.Drawing.Size(138, 35);
            this.Clall.TabIndex = 23;
            this.Clall.Text = "Clear All";
            this.Clall.UseVisualStyleBackColor = true;
            this.Clall.Click += new System.EventHandler(this.Clall_Click);
            // 
            // ViewCriticalBtn
            // 
            this.ViewCriticalBtn.Location = new System.Drawing.Point(896, 622);
            this.ViewCriticalBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ViewCriticalBtn.Name = "ViewCriticalBtn";
            this.ViewCriticalBtn.Size = new System.Drawing.Size(138, 35);
            this.ViewCriticalBtn.TabIndex = 24;
            this.ViewCriticalBtn.Text = "View Critical";
            this.ViewCriticalBtn.UseVisualStyleBackColor = true;
            this.ViewCriticalBtn.Click += new System.EventHandler(this.Critical_Click);
            // 
            // CPPAST
            // 
            this.CPPAST.Location = new System.Drawing.Point(352, 622);
            this.CPPAST.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CPPAST.Name = "CPPAST";
            this.CPPAST.Size = new System.Drawing.Size(138, 35);
            this.CPPAST.TabIndex = 25;
            this.CPPAST.Text = "Copy Paste";
            this.CPPAST.UseVisualStyleBackColor = true;
            this.CPPAST.Click += new System.EventHandler(this.CPPAST_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 680);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 18);
            this.label2.TabIndex = 31;
            this.label2.Text = "Table X,Y:";
            // 
            // NewTableComboBoxY
            // 
            this.NewTableComboBoxY.FormattingEnabled = true;
            this.NewTableComboBoxY.Location = new System.Drawing.Point(261, 677);
            this.NewTableComboBoxY.Name = "NewTableComboBoxY";
            this.NewTableComboBoxY.Size = new System.Drawing.Size(119, 26);
            this.NewTableComboBoxY.TabIndex = 30;
            // 
            // NewTableComboBoxX
            // 
            this.NewTableComboBoxX.FormattingEnabled = true;
            this.NewTableComboBoxX.Location = new System.Drawing.Point(116, 677);
            this.NewTableComboBoxX.Name = "NewTableComboBoxX";
            this.NewTableComboBoxX.Size = new System.Drawing.Size(125, 26);
            this.NewTableComboBoxX.TabIndex = 29;
            // 
            // InterpolationBtn
            // 
            this.InterpolationBtn.Location = new System.Drawing.Point(507, 622);
            this.InterpolationBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.InterpolationBtn.Name = "InterpolationBtn";
            this.InterpolationBtn.Size = new System.Drawing.Size(138, 35);
            this.InterpolationBtn.TabIndex = 32;
            this.InterpolationBtn.Text = "Interpolation";
            this.InterpolationBtn.UseVisualStyleBackColor = true;
            this.InterpolationBtn.Click += new System.EventHandler(this.InterpolationBtn_Click);
            // 
            // NewTableBtn
            // 
            this.NewTableBtn.Location = new System.Drawing.Point(507, 677);
            this.NewTableBtn.Name = "NewTableBtn";
            this.NewTableBtn.Size = new System.Drawing.Size(138, 35);
            this.NewTableBtn.TabIndex = 33;
            this.NewTableBtn.Text = "New Table";
            this.NewTableBtn.UseVisualStyleBackColor = true;
            this.NewTableBtn.Click += new System.EventHandler(this.NewTableBtn_Click);
            // 
            // RecordeTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1058, 717);
            this.Controls.Add(this.NewTableBtn);
            this.Controls.Add(this.InterpolationBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.NewTableComboBoxY);
            this.Controls.Add(this.NewTableComboBoxX);
            this.Controls.Add(this.CPPAST);
            this.Controls.Add(this.ViewCriticalBtn);
            this.Controls.Add(this.Clall);
            this.Controls.Add(this.CKall);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TableRecorderConatiner);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "RecordeTable";
            this.Text = "RT";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RecordeTable_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RecordeTable_FormClosed);
            this.Load += new System.EventHandler(this.RecordeTable_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ListView TableRecorderConatiner;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button CKall;
        private System.Windows.Forms.Button Clall;
        private System.Windows.Forms.Button ViewCriticalBtn;
        private System.Windows.Forms.Button CPPAST;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.ComboBox NewTableComboBoxY;
        public System.Windows.Forms.ComboBox NewTableComboBoxX;
        private System.Windows.Forms.Button InterpolationBtn;
        private System.Windows.Forms.Button NewTableBtn;
    }
}