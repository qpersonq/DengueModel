using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DenInterface
{
    public partial class InterpolationForm : Form
    {

        private ListView reflistview;

        public InterpolationForm(ListView lv)
        {
            InitializeComponent();

            reflistview = lv;

            //load  column
            foreach (ColumnHeader    k in reflistview.Columns)
            {
                xcbx.Items.Add(k.Text);
                ycbx.Items.Add(k.Text);
            }
           


        }
        
        private void InterpolationForm_Load(object sender, EventArgs e)
        {

        }

        private void intpBtn_Click(object sender, EventArgs e)
        {
            if(xtbx.Text!=String.Empty  && Microsoft.VisualBasic.Information.IsNumeric(xtbx.Text)  && xcbx.Text!=String.Empty&& ycbx.Text != String.Empty)
            {
                double XVAL = double.Parse(xtbx.Text);
                string wrstring = "";
                int xidx=0 , yidx=0;
                //fd idx
                for(int kl=0;   kl< reflistview.Columns.Count; kl++)
                {
                    if (reflistview.Columns[kl].Text == xcbx.Text) xidx = kl;
                    if (reflistview.Columns[kl].Text == ycbx.Text) yidx = kl;

                }

                //filled in x y array
                List<double> xarr=new List<double>(), yarr=new List<double>();
                foreach(ListViewItem lvi in reflistview.Items)
                {
                    xarr.Add(double.Parse(lvi.SubItems[xidx].Text));
                    yarr.Add(double.Parse(lvi.SubItems[yidx].Text));
                }

                //cal position

                wrstring = "?";
                double x1=0, x2=0, y1=0, y2=0;
                  for (int k=0;k<xarr.Count-1; k++)
                  {
                    if(   (Math.Abs(XVAL-xarr[k])+Math.Abs(XVAL - xarr[k+1])  )     <=    Math.Abs(xarr[k]-xarr[k+1]))//in range
                    {
                        x1 = xarr[k];
                        x2 = xarr[k + 1];
                        y1 = yarr[k];
                        y2 = yarr[k + 1];
                    }
                  }


                if ((x2 - x1) == 0) wrstring = "mod 0";
                else //cal slope
                {
                  wrstring= ( y1 + ((y2 - y1) / (x2 - x1)) * (XVAL - x1)).ToString();

                }

                ytbx.Text = wrstring;
            }
            else
            {
                MessageBox.Show("Error!");
            }

            if (CpClkbACloseCkb.Checked)
            {
                Clipboard.SetText(ytbx.Text);
                this.Close();
            }


        }
    }
}
