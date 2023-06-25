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
            this.VMarginal = new System.Windows.Forms.Button();
            this.CPPAST = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.MarginComboBoxY = new System.Windows.Forms.ComboBox();
            this.MarginComboBoxX = new System.Windows.Forms.ComboBox();
            this.InterpolationBtn = new System.Windows.Forms.Button();
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
            // VMarginal
            // 
            this.VMarginal.Location = new System.Drawing.Point(928, 622);
            this.VMarginal.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.VMarginal.Name = "VMarginal";
            this.VMarginal.Size = new System.Drawing.Size(138, 35);
            this.VMarginal.TabIndex = 24;
            this.VMarginal.Text = "View Marginal";
            this.VMarginal.UseVisualStyleBackColor = true;
            this.VMarginal.Click += new System.EventHandler(this.VMarginal_Click);
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
            this.label2.Location = new System.Drawing.Point(669, 610);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(153, 18);
            this.label2.TabIndex = 31;
            this.label2.Text = "Marginal Table X,Y:";
            // 
            // MarginComboBoxY
            // 
            this.MarginComboBoxY.FormattingEnabled = true;
            this.MarginComboBoxY.Location = new System.Drawing.Point(803, 633);
            this.MarginComboBoxY.Name = "MarginComboBoxY";
            this.MarginComboBoxY.Size = new System.Drawing.Size(119, 26);
            this.MarginComboBoxY.TabIndex = 30;
            // 
            // MarginComboBoxX
            // 
            this.MarginComboBoxX.FormattingEnabled = true;
            this.MarginComboBoxX.Location = new System.Drawing.Point(672, 633);
            this.MarginComboBoxX.Name = "MarginComboBoxX";
            this.MarginComboBoxX.Size = new System.Drawing.Size(125, 26);
            this.MarginComboBoxX.TabIndex = 29;
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
            // RecordeTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1075, 671);
            this.Controls.Add(this.InterpolationBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.MarginComboBoxY);
            this.Controls.Add(this.MarginComboBoxX);
            this.Controls.Add(this.CPPAST);
            this.Controls.Add(this.VMarginal);
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
        private System.Windows.Forms.Button VMarginal;
        private System.Windows.Forms.Button CPPAST;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.ComboBox MarginComboBoxY;
        public System.Windows.Forms.ComboBox MarginComboBoxX;
        private System.Windows.Forms.Button InterpolationBtn;
    }
}