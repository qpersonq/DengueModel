using DengueCheckInterface;
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
        private void Critical_Click(object sender, EventArgs e)
        {
            if (cdrawer == null) { cdrawer = new ChartDrawer(); cdrawer.ControlBox = false; }
            else { cdrawer.Close();cdrawer = null; return; }


            cdrawer.Text +=" "+ label1.Text;
            cdrawer.Show();
        
            Series s1 = new Series("D\'");
            Series s2 = new Series("D");
            //Series s2 = new Series("D");s2.ChartType = SeriesChartType.Line;
            
            s1.Color = Color.Blue;
            s1.ChartType = SeriesChartType.Line;
            s2.Color = Color.Gray;
            s2.Color = Color.FromArgb(158, Color.Gray);
            s2.ChartType = SeriesChartType.Line;
            //s1.IsValueShownAsLabel = true;

            int tagidx = indexofCapital("Percentage");
            int  xidx = indexofCapital("Count"),yidx = indexofCapital("AAT");/*xy*/
                                                                             // if (yidx == -1) yidx = indexofCapital("Cr2t");//no found
          //CriticalPointEstimator cptestimator = new CriticalPointEstimator();
            DensityThresholdEstimator cptestimator = new DensityThresholdEstimator();
            cptestimator.allPoint = Math.Round( double.Parse( TableRecorderConatiner.Items[TableRecorderConatiner.Items.Count - 1].SubItems[indexofCapital("Count")].Text )/ double.Parse(TableRecorderConatiner.Items[TableRecorderConatiner.Items.Count - 1].SubItems[indexofCapital("C/N")].Text));
            for (var i = 0; i < TableRecorderConatiner.Items.Count; i++)
            {
                cptestimator.add(TableRecorderConatiner.Items[i].SubItems[tagidx].Text,
                    double.Parse(TableRecorderConatiner.Items[i].SubItems[xidx].Text),
                    double.Parse(TableRecorderConatiner.Items[i].SubItems[yidx].Text)
                    );

                /*
                if (TableRecorderConatiner.Items[i].SubItems[ddcidx].Text.Length==0) continue;
                if (!TableRecorderConatiner.Items[i].Checked) continue;
                double ans,pec;
                bool ps=double.TryParse(TableRecorderConatiner.Items[i].SubItems[ddcidx].Text, out ans);
                bool pecbl = double.TryParse(TableRecorderConatiner.Items[i].SubItems[percidx].Text, out pec);
                //MessageBox.Show(ans.ToString());
                if (ps&& pecbl) s1.Points.AddXY(pec, ans);*/
            }
            //List<Tuple<string,double,double>> consqlst= cptestimator.getAccerator();
            List<Tuple<string, double, double>> consqlst = cptestimator.getAreaPerCase();
            foreach (Tuple<string,double,double> tp in consqlst)
            {
                
                s1.Points.AddXY(tp.Item2, tp.Item3);
                s1.Points[s1.Points.Count-1].Label = tp.Item1;

            }
            for(int i=1; i < cptestimator.Xset.Count; i++)
            {
                s2.Points.AddXY(cptestimator.Xset[i], cptestimator.Yset[i]);
            }


            //s1.Label= "#VALX,#VALY";
            Title ctitle = new Title();
            ctitle.Font = new Font("Arial", 16, FontStyle.Regular);
            ctitle.Text ="Threshold points";
            cdrawer.MChart.Titles.Add(ctitle);
            s1.BorderWidth = 3;
            
            cdrawer.MChart.Series.Add(s1); cdrawer.MChart.Series.Add(s2);

            cdrawer.MChart.ChartAreas[0].AxisX.Title = "Number of cases (case)";    
            cdrawer.MChart.ChartAreas[0].AxisX.LabelStyle.Enabled = true;
            cdrawer.MChart.ChartAreas[0].AxisX.TitleFont = new Font("Arial", 12, FontStyle.Regular);

            cdrawer.MChart.ChartAreas[0].AxisY.Title = "△areas * days/△cases (km^2 day/case)";            
            cdrawer.MChart.ChartAreas[0].AxisY.LabelStyle.Enabled = true;
            cdrawer.MChart.ChartAreas[0].AxisY.TitleFont = new Font("Arial", 12, FontStyle.Regular);
            
            cdrawer.MChart.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
            cdrawer.MChart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
            //secondary y
            cdrawer.MChart.Series[0].YAxisType = AxisType.Primary;
            cdrawer.MChart.Series[1].YAxisType = AxisType.Secondary;
            cdrawer.MChart.Series[0].XAxisType = AxisType.Primary;
            cdrawer.MChart.Series[1].XAxisType = AxisType.Primary;
            cdrawer.MChart.ChartAreas[0].AxisY2.MajorGrid.Enabled = false;
            cdrawer.MChart.ChartAreas[0].AxisY2.Title = "Sum of the areas * days (km^2 day)";
            cdrawer.MChart.ChartAreas[0].AxisY2.TitleFont = new Font("Arial", 12, FontStyle.Regular);
            s2.MarkerStyle = MarkerStyle.None;
            s2.BorderWidth = 3;


            //write conseq
            //List<Tuple<string, double, string>> viseq = cptestimator.getLocalMaxAndMin(consqlst);
            List<Tuple<string, double, string>> viseq = cptestimator.getMaxAreaChangeSlope(consqlst);
            int vicnt = 0;
            cdrawer.ConsequenceList.Columns.Add("Percentage");
            cdrawer.ConsequenceList.Columns.Add("D\'");
            cdrawer.ConsequenceList.Columns.Add("Information");
            foreach (Tuple<string,double,string> pp in viseq)
            {
                cdrawer.ConsequenceList.Items.Add(pp.Item1);
                cdrawer.ConsequenceList.Items[vicnt].SubItems.Add(pp.Item2.ToString());
                cdrawer.ConsequenceList.Items[vicnt].SubItems.Add(pp.Item3);

                vicnt++;
            }

            //proc color
            s1.Color = Color.Green;
            s1.MarkerStyle = MarkerStyle.Circle;
            s1.MarkerSize = 8;
            bool outliter = false;
            for(int i=1; i < s1.Points.Count; i++)
            {
               
                if (outliter) s1.Points[i].Color = Color.Red;
                if (viseq[i].Item3.Contains("Threshold")) {
                    //s1.Points[i].Color = Color.Yellow;
                    s1.Points[i].MarkerStyle = MarkerStyle.Diamond;
                    s1.Points[i].MarkerSize = 20;
                    s1.Points[i].MarkerColor = Color.Orange;
                    outliter = true;
                }

               

            }

            

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
                        int percidx = indexofCapital(NewTableComboBoxX.Text),ddcidx = indexofCapital(NewTableComboBoxY.Text);
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

        private void NewTableBtn_Click(object sender, EventArgs e)
        {


            ChartDrawer ncdrawer = new ChartDrawer();
            string cbxtxt = NewTableComboBoxX.Text, cbytxt = NewTableComboBoxY.Text;
            
            ncdrawer.Show();

            Series s1 = new Series();
            //Series s2 = new Series("D");s2.ChartType = SeriesChartType.Line;

            //s1.Color = Color.Blue;
            s1.ChartType = SeriesChartType.Line;
            //s1.IsValueShownAsLabel = true;

            
            int xidx = indexofCapital(cbxtxt), yidx = indexofCapital(cbytxt);/*xy*/
            for (var i = 0; i < TableRecorderConatiner.Items.Count; i++)
                s1.Points.AddXY(double.Parse(TableRecorderConatiner.Items[i].SubItems[xidx].Text), double.Parse(TableRecorderConatiner.Items[i].SubItems[yidx].Text));

            s1.Label= "#VALX,#VALY";
            Title ctitle = new Title();
            ctitle.Font = new Font("Arial", 16, FontStyle.Regular);
            ctitle.Text = NewTableComboBoxX.Text + " & " + NewTableComboBoxY.Text;
            ncdrawer.MChart.Titles.Add(ctitle);
            s1.BorderWidth = 4;
            ncdrawer.MChart.Series.Add(s1);// cdrawer.MChart.Series.Add(s2);

            ncdrawer.MChart.ChartAreas[0].AxisX.Title = cbxtxt;
            ncdrawer.MChart.ChartAreas[0].AxisX.LabelStyle.Enabled = true;
            ncdrawer.MChart.ChartAreas[0].AxisX.TitleFont = new Font("Arial", 12, FontStyle.Regular);

            ncdrawer.MChart.ChartAreas[0].AxisY.Title =cbytxt;
            ncdrawer.MChart.ChartAreas[0].AxisY.LabelStyle.Enabled = true;
            ncdrawer.MChart.ChartAreas[0].AxisY.TitleFont = new Font("Arial", 12, FontStyle.Regular);

            ncdrawer.MChart.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
            ncdrawer.MChart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
            //wr conseq
            ncdrawer.ConsequenceList.Columns.Add("item");
            ncdrawer.ConsequenceList.Columns.Add(cbxtxt);
            ncdrawer.ConsequenceList.Columns.Add(cbytxt);
            for (var i = 0; i < TableRecorderConatiner.Items.Count; i++)
            {
                ncdrawer.ConsequenceList.Items.Add(i.ToString());
                ncdrawer.ConsequenceList.Items[ncdrawer.ConsequenceList.Items.Count - 1].SubItems.Add(TableRecorderConatiner.Items[i].SubItems[xidx].Text);
                ncdrawer.ConsequenceList.Items[ncdrawer.ConsequenceList.Items.Count - 1].SubItems.Add(TableRecorderConatiner.Items[i].SubItems[yidx].Text);
            }


        }
    }
}
