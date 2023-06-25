using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DenInterface
{
    public partial class RecordeTable : Form
    {
        
        public RecordeTable()
        {
            InitializeComponent();
        }
        //public ref ListView Gettrcctrl() { return ref TableRecorderConatiner; }
        


        private void RecordeTable_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
        }
        public int indexofCapital(string capstr)
        {
            //int colidx=-1;
            List< string > lcap= new List<string>();
            for (int z= 0;z < TableRecorderConatiner.Columns.Count;z++)
            {
                lcap.Add(TableRecorderConatiner.Columns[z].Text);
            }

            return lcap.IndexOf(capstr);
        }
        private void CKall_Click(object sender, EventArgs e)
        {
           foreach(ListViewItem g in TableRecorderConatiner.Items)
            {
                g.Checked = true;
            }
        }

        private void Clall_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem g in TableRecorderConatiner.Items)
            {
                g.Checked = false;
            }
        }
        public ChartDrawer cdrawer=null;
        private void VMarginal_Click(object sender, EventArgs e)
        {
            if (cdrawer == null) { cdrawer = new ChartDrawer(); cdrawer.ControlBox = false; }
            else { cdrawer.Close();cdrawer = null; return; }


            cdrawer.Text +=" "+ label1.Text;
            cdrawer.Show();
        
            Series s1 = new Series("Mrg");
            
            
            s1.Color = Color.Blue;
            s1.ChartType = SeriesChartType.Line;
            //s1.IsValueShownAsLabel = true;
            
            
            int  percidx = indexofCapital(MarginComboBoxX.Text), ddcidx = indexofCapital(MarginComboBoxY.Text);/*xy*/
            for (var i = 1; i < TableRecorderConatiner.Items.Count; i++)
            {
                if (TableRecorderConatiner.Items[i].SubItems[ddcidx].Text.Length==0) continue;
                if (!TableRecorderConatiner.Items[i].Checked) continue;
                double ans,pec;
                bool ps=double.TryParse(TableRecorderConatiner.Items[i].SubItems[ddcidx].Text, out ans);
                bool pecbl = double.TryParse(TableRecorderConatiner.Items[i].SubItems[percidx].Text, out pec);
                //MessageBox.Show(ans.ToString());
                if (ps&& pecbl) s1.Points.AddXY(pec, ans);
            }
            
            s1.Label= "#VALX,#VALY";
            Title ctitle = new Title();
            ctitle.Font = new Font("Arial", 16, FontStyle.Regular);
            ctitle.Text = MarginComboBoxX.Text+" & "+MarginComboBoxY.Text;
            cdrawer.MChart.Titles.Add(ctitle);
            s1.BorderWidth = 4;
            cdrawer.MChart.Series.Add(s1);

            cdrawer.MChart.ChartAreas[0].AxisX.Title = MarginComboBoxX.Text;    
            cdrawer.MChart.ChartAreas[0].AxisX.LabelStyle.Enabled = true;
            cdrawer.MChart.ChartAreas[0].AxisX.TitleFont = new Font("Arial", 12, FontStyle.Regular);

            cdrawer.MChart.ChartAreas[0].AxisY.Title = MarginComboBoxY.Text;            
            cdrawer.MChart.ChartAreas[0].AxisY.LabelStyle.Enabled = true;
            cdrawer.MChart.ChartAreas[0].AxisY.TitleFont = new Font("Arial", 12, FontStyle.Regular);
            
            cdrawer.MChart.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
            cdrawer.MChart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;


        }
        protected void fixSeries(Series sr,List<DataPoint> dpts)
        {
            sr.Points.Clear();
            foreach(DataPoint dpt in dpts){
                double x = dpt.XValue;
                double y = dpt.YValues[0];


                sr.Points.AddXY(x,y);
            }

        }
        private void RecordeTable_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void CPPAST_Click(object sender, EventArgs e)
        {
            String clipText = string.Empty;
            if (TableRecorderConatiner.SelectedIndices.Count != 0)
            {
                foreach( int u  in TableRecorderConatiner.SelectedIndices)
                {
                   for(int z=0; z < TableRecorderConatiner.Items[u].SubItems.Count; z++)
                    {
                        clipText += (TableRecorderConatiner.Items[u].SubItems[z].Text + ",");
                    }
                    clipText += Environment.NewLine;
                }
                
            }
            else
            {
                foreach (ListViewItem item in this.TableRecorderConatiner.Items)
                {
                    for (int z = 0; z < item.SubItems.Count; z++)
                    {
                        clipText += (item.SubItems[z].Text + ",");
                    }

                    clipText += Environment.NewLine;
                }
            }
            
           
            if (!String.IsNullOrEmpty(clipText))
            {
                Clipboard.SetText(clipText);
            }
        }

        protected void TableRecorderConatiner_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            //MessageBox.Show("chk chg");
            for(int j=0;j<TableRecorderConatiner.Items.Count; j++)
            {
                if (!TableRecorderConatiner.Items[j].Checked)
                {
                    if (cdrawer != null)
                    {
                        List<DataPoint> dpts = new List<DataPoint>();
                        int percidx = indexofCapital(MarginComboBoxX.Text),ddcidx = indexofCapital(MarginComboBoxY.Text);
                        for (int i = 1; i < TableRecorderConatiner.Items.Count; i++)
                        {
                            if (TableRecorderConatiner.Items[i].SubItems[ddcidx].Text.Length == 0) continue;
                            if (!TableRecorderConatiner.Items[i].Checked) continue;
                            double ans, pec;
                            bool ps = double.TryParse(TableRecorderConatiner.Items[i].SubItems[ddcidx].Text, out ans);
                            bool pecbl = double.TryParse(TableRecorderConatiner.Items[i].SubItems[percidx].Text, out pec);
                            //MessageBox.Show(ans.ToString());
                            if (ps && pecbl) dpts.Add(new DataPoint (pec, ans));
                        }
                        fixSeries(cdrawer.MChart.Series[0], dpts);
                        

                    }
                }
            }
        }

        private void RecordeTable_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(cdrawer!=null)cdrawer.Close();
            cdrawer = null;
        }

        private void TableRecorderConatiner_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void InterpolationBtn_Click(object sender, EventArgs e)
        {
            InterpolationForm    intplfm= new InterpolationForm(this.TableRecorderConatiner);
            intplfm.Show();
        }
    }
}
