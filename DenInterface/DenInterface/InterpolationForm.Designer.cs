namespace DenInterface
{
    partial class InterpolationForm
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
            this.ycbx = new System.Windows.Forms.ComboBox();
            this.xcbx = new System.Windows.Forms.ComboBox();
            this.xtbx = new System.Windows.Forms.TextBox();
            this.ytbx = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.intpBtn = new System.Windows.Forms.Button();
            this.CpClkbACloseCkb = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // ycbx
            // 
            this.ycbx.FormattingEnabled = true;
            this.ycbx.Location = new System.Drawing.Point(301, 37);
            this.ycbx.Name = "ycbx";
            this.ycbx.Size = new System.Drawing.Size(285, 26);
            this.ycbx.TabIndex = 0;
            this.ycbx.Text = "Cr2t";
            // 
            // xcbx
            // 
            this.xcbx.FormattingEnabled = true;
            this.xcbx.Location = new System.Drawing.Point(10, 37);
            this.xcbx.Name = "xcbx";
            this.xcbx.Size = new System.Drawing.Size(285, 26);
            this.xcbx.TabIndex = 1;
            this.xcbx.Text = "C/N";
            // 
            // xtbx
            // 
            this.xtbx.Location = new System.Drawing.Point(10, 80);
            this.xtbx.Name = "xtbx";
            this.xtbx.Size = new System.Drawing.Size(285, 29);
            this.xtbx.TabIndex = 2;
            // 
            // ytbx
            // 
            this.ytbx.Location = new System.Drawing.Point(301, 80);
            this.ytbx.Name = "ytbx";
            this.ytbx.Size = new System.Drawing.Size(285, 29);
            this.ytbx.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "X Item";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(298, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 18);
            this.label2.TabIndex = 5;
            this.label2.Text = "Y Item";
            // 
            // intpBtn
            // 
            this.intpBtn.Location = new System.Drawing.Point(610, 37);
            this.intpBtn.Name = "intpBtn";
            this.intpBtn.Size = new System.Drawing.Size(178, 72);
            this.intpBtn.TabIndex = 6;
            this.intpBtn.Text = "Interpolation";
            this.intpBtn.UseVisualStyleBackColor = true;
            this.intpBtn.Click += new System.EventHandler(this.intpBtn_Click);
            // 
            // CpClkbACloseCkb
            // 
            this.CpClkbACloseCkb.AutoSize = true;
            this.CpClkbACloseCkb.Checked = true;
            this.CpClkbACloseCkb.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CpClkbACloseCkb.Location = new System.Drawing.Point(559, 12);
            this.CpClkbACloseCkb.Name = "CpClkbACloseCkb";
            this.CpClkbACloseCkb.Size = new System.Drawing.Size(229, 22);
            this.CpClkbACloseCkb.TabIndex = 7;
            this.CpClkbACloseCkb.Text = "Copy to Clickboard and Exit";
            this.CpClkbACloseCkb.UseVisualStyleBackColor = true;
            // 
            // InterpolationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 121);
            this.Controls.Add(this.CpClkbACloseCkb);
            this.Controls.Add(this.intpBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ytbx);
            this.Controls.Add(this.xtbx);
            this.Controls.Add(this.xcbx);
            this.Controls.Add(this.ycbx);
            this.Name = "InterpolationForm";
            this.Text = "InterpolationForm";
            this.Load += new System.EventHandler(this.InterpolationForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ComboBox ycbx;
        public System.Windows.Forms.ComboBox xcbx;
        private System.Windows.Forms.TextBox xtbx;
        private System.Windows.Forms.TextBox ytbx;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button intpBtn;
        private System.Windows.Forms.CheckBox CpClkbACloseCkb;
    }
}