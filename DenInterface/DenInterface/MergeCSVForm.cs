using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DenInterface
{
    public partial class MergeCSVMnINT : Form
    {
        public MergeCSVMnINT()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OFD.ShowDialog();
            richTextBox1.Text += (OFD.FileName+"\n");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SFD.ShowDialog();
            string svf = SFD.FileName;
            if (svf == "") return;


            string itms=richTextBox1.Text;
            string[] itmprc = itms.Split('\n');
            List<string> itmlst = new List<string>(itmprc);
            itmlst.RemoveAt(itmlst.Count - 1);
            MergeResult.mergeCSVResultByLine(svf,itmlst);

        }

        private void MergeCSVMnINT_Load(object sender, EventArgs e)
        {

        }
    }
}
