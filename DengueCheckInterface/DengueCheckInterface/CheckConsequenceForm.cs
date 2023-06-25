using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DenInterface
{
    public partial class GraphLine : Form
    {
        public List<CheckBox> DataOptionCheckBox = new List<CheckBox>();

        public GraphLine()
        {
            InitializeComponent();
        }

        private void OP_DSEQ_BTN_Click(object sender, EventArgs e)
        {

            OFD_DenData.ShowDialog();
            MessageBox.Show(OFD_DenData.FileName);
            CheckBox ck1 = new CheckBox();
            ck1.Text = OFD_DenData.FileName;
            ck1.AutoSize = true;
            //ck1.Appearance = Appearance.Button;
            //ck1.AutoCheck = false;
            //ck1.Visible = true;
            ck1.Location = new System.Drawing.Point(5, (DataOptionCheckBox.Count * ck1.Size.Height) + 30);
            DataOptionCheckBox.Add(ck1);
            Controls.Add(ck1);

        }



        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            FBDRT.ShowDialog();
            PathText.Text = FBDRT.SelectedPath;
        }

        private void LDIC_BTN_Click(object sender, EventArgs e)
        {
            ListData.Clear();
            DirectoryInfo cdic = new DirectoryInfo(PathText.Text);
            foreach (var fi in cdic.GetDirectories())
            {
                ListData.Items.Add(fi.ToString());
            }
            //ListData.Items.Add("1bc");
        }

        private void ListData_DoubleClick(object sender, EventArgs e)
        {
            if (ListData.SelectedItems != null)
            {

                var indexs = ListData.SelectedIndices;
                //string test="";
                foreach (int p in indexs) {

                    MessageBox.Show(ListData.Items[p].Text);
                }

            }
        }



        private void selectDraw_Click(object sender, EventArgs e)
        {
            CloseRecordTable();
            //ListData_DoubleClick(null, null);


            //string test="";
            for (int p = 0; p < OpedDic.Items.Count; p++)
            {
                if (OpedDic.Items[p].Text.Contains(":\\"))
                {
                    openConsequenceRecorder(OpedDic.Items[p].Text, p);
                    continue;
                }


                string oppth = PathText.Text + "\\" + OpedDic.Items[p].Text;
                oppth += ("\\" + "Valid" + "\\");
                if (!Directory.Exists(oppth))
                {
                    MessageBox.Show("Error Valid Path " + oppth);
                    return;
                }
                string[] allfls = Directory.GetFiles(oppth);
                string validreport = "";
                foreach (string fnm in allfls)
                {
                    if (fnm.Contains("Perc.csv"))
                    {
                        validreport = fnm;
                    }
                }
                if (validreport.Length != 0) openConsequenceRecorder(validreport, p);
                else MessageBox.Show("No report SOM Data");
            }

            drawMultiCDLineByRecordtables();
            //draw bkcolor
            /*
            for(int y=0;y< recordtables.Count; y++)
            {
                recordtables[y].BackColor = MultiSeriesChart.MChart.Series[y].Color;
                MessageBox.Show(MultiSeriesChart.MChart.Series[y].Color.ToString());
            }*/
        }

        //public RecordeTable srtbl;
        //public string fteststr= "D:\\student\\R07228003Dmitrii\\DengueNowRunning\\prd_tr20150601_20151231\\ParClEN2\\Valid\\PrRS38521_47194SOMPerc.csv";
        public void openConsequenceRecorder(string vdt, int idx = -1)
        {


            RecorderTablePlus rtbl;
            recordtables.Add(rtbl = new RecorderTablePlus(XAxisItemLabel.Text, YAxisItemLabel.Text));
            // cap text 
            string apdrtbltext = GetUpLevelDirectory(vdt, 1);
            apdrtbltext = Path.GetFileNameWithoutExtension(apdrtbltext + ".fake");
            rtbl.Text = apdrtbltext;

            StreamReader rdt = new StreamReader(vdt);
            string line;
            //title
            line = rdt.ReadLine();
            line = line.Trim();
            string[] tmpdt = line.Split(',');
            foreach (string p in tmpdt)
            {
                string px = p.Trim();
                rtbl.TableRecorderConatiner.Columns.Add(px, 80, HorizontalAlignment.Left);
                //MessageBox.Show(p);
            }

            //properties
            while ((line = rdt.ReadLine()) != null)
            {
                line = line.Trim();
                //line.Replace(',', ' ');
                if (line[line.Length - 1] == ',') line = line.Remove(line.Length - 1);
                tmpdt = line.Split(',');
                //MessageBox.Show(line);

                ListViewItem item = new ListViewItem();
                // item.SubItems.Clear();
                bool zflg_tmpdt = true;
                foreach (var k in tmpdt)
                {
                    string kk;

                    if (Microsoft.VisualBasic.Information.IsNumeric(k.ToString())) kk = Double.Parse(k).ToString();
                    else kk = "0";
                    if (zflg_tmpdt) { item.SubItems[0].Text = kk; zflg_tmpdt = false; continue; }

                    item.SubItems.Add(kk.ToString());
                }
                item.Checked = true;
                rtbl.TableRecorderConatiner.Items.Add(item);
            }
            rdt.Close();
            rtbl.Show();
            //rtbl.label1.Text = "mylb1";
            //srtbl = rtbl;
            rtbl.label1.Text = vdt;

            //Creater done
            calculateOnTable(rtbl.TableRecorderConatiner);

            //Filling ComboBox
            rtbl.NewTableComboBoxX.Text = "Percentage";
            rtbl.NewTableComboBoxY.Text = "CE6/AT";

            for (int q = 0; q < rtbl.TableRecorderConatiner.Columns.Count; q++)
            {
                rtbl.NewTableComboBoxX.Items.Add(rtbl.TableRecorderConatiner.Columns[q].Text);
                rtbl.NewTableComboBoxY.Items.Add(rtbl.TableRecorderConatiner.Columns[q].Text);
            }


            //return rtbl;

        }
        public void calculateOnTable(ListView lv)
        {
            //add ATT
            List<string> caplst = new List<string>();
            //MessageBox.Show(lv.Columns[0].Text);
            for (int i = 0; i < lv.Columns.Count; i++)
            {
                caplst.Add(lv.Columns[i].Text);
            }
            //AAT
            if (caplst.IndexOf("AT") != -1)
            {
                lv.Columns.Add("AAT", 80, HorizontalAlignment.Left);
                for (int i = 0; i < lv.Items.Count; i++)
                {
                    lv.Items[i].SubItems.Add((double.Parse(lv.Items[i].SubItems[caplst.IndexOf("AT")].Text) / 1000000).ToString());
                }

            }

            if (caplst.IndexOf("CAT") != -1)
            {
                lv.Columns.Add("OriAAT", 80, HorizontalAlignment.Left);
                for (int i = 0; i < lv.Items.Count; i++)
                {
                   lv.Items[i].SubItems.Add((1 / (double.Parse(lv.Items[i].SubItems[caplst.IndexOf("CAT")].Text)) * (double.Parse(lv.Items[i].SubItems[caplst.IndexOf("Count")].Text))).ToString());
                }
             }



            /*
            //calculate  delta
            lv.Columns.Add("Delta", 80, HorizontalAlignment.Left);

            //MessageBox.Show(lv.Columns[0].Text);
            List<string> caplst = new List<string>();
            //MessageBox.Show(lv.Columns[0].Text);
            for (int i = 0; i < lv.Columns.Count; i++)
            {
                caplst.Add(lv.Columns[i].Text);
            }
            string v = lv.Columns[0].Text;
            //int v= ltrt.IndexOf("b");
            int pcount = caplst.IndexOf("Percentage");//pmother
            int pden = caplst.IndexOf("Count");//pson

            int ggg = lv.Items.Count;
            //int ddddddddddddddd = 56;

            //no cal 0
            lv.Items[0].SubItems.Add("0");
            for (int k = 1; k < lv.Items.Count; k++)
            {

                if (
                Microsoft.VisualBasic.Information.IsNumeric(lv.Items[k].SubItems[pcount].Text) &&
                Microsoft.VisualBasic.Information.IsNumeric(lv.Items[k].SubItems[pden].Text) &&
                Microsoft.VisualBasic.Information.IsNumeric(lv.Items[k - 1].SubItems[pcount].Text) &&
                Microsoft.VisualBasic.Information.IsNumeric(lv.Items[k - 1].SubItems[pden].Text)
                )
                {
                    float curpc = float.Parse(lv.Items[k].SubItems[pcount].Text),
                          curpd = float.Parse(lv.Items[k].SubItems[pden].Text),
                          prepc = float.Parse(lv.Items[k - 1].SubItems[pcount].Text),
                          prepd = float.Parse(lv.Items[k - 1].SubItems[pden].Text);

                    if ((curpc - prepc) != 0.0) lv.Items[k].SubItems.Add(((curpd - prepd) / (curpc - prepc)).ToString());
                    else lv.Items[k].SubItems.Add(0.0.ToString());
                }
                else
                {
                    // MessageBox.Show(lv.GetItemAt( k, pcount).ToString());
                    lv.Items[k].SubItems.Add("0");
                }

            }
            //try smooth
            List<double> chglist = new List<double>(); chglist.Add(0);//hd
            for (int k = 1; k < lv.Items.Count; k++)
            {
                double mg;
                if (k < lv.Items.Count - 1)
                {
                    double pvl = double.Parse(lv.Items[k - 1].SubItems[(lv.Items[k - 1].SubItems.Count) - 1].Text),
                          cvl = double.Parse(lv.Items[k].SubItems[(lv.Items[k].SubItems.Count) - 1].Text),
                          bval = double.Parse(lv.Items[k + 1].SubItems[(lv.Items[k + 1].SubItems.Count) - 1].Text);
                     mg = (cvl * 0.5) + (0.25 * pvl) + (0.25 * bval);
                }
                else
                {
                    double pvl = double.Parse(lv.Items[k - 1].SubItems[(lv.Items[k - 1].SubItems.Count) - 1].Text),
                          cvl = double.Parse(lv.Items[k].SubItems[(lv.Items[k].SubItems.Count) - 1].Text);
                    mg = (cvl * 0.75) + (0.25 * pvl);
                }
                chglist.Add(mg);

            }
            
            for (int k = 0; k < chglist.Count; k++)
            {
                lv.Items[k].SubItems[(lv.Items[k ].SubItems.Count) - 1].Text = chglist[k].ToString();
            }
            
            */

        }


        public List<RecorderTablePlus> recordtables = new List<RecorderTablePlus>();
        public void CloseRecordTable()
        {
            foreach (RecordeTable rtbl in recordtables)
            {
                rtbl.Close();
            }
            MultiSeriesChart.Close();
            MultiSeriesChart = new ChartDrawer();
            recordtables.Clear();
        }

        public ChartDrawer MultiSeriesChart = new ChartDrawer();
        protected  Color[] LineColorList = { Color.Red,Color.Lime,Color.Blue};
        public void drawMultiCDLineByRecordtables()
        {

            MultiSeriesChart.ControlBox = false;
            string[] ttxtcap = { XAxisItemLabel.Text, YAxisItemLabel.Text };
            MultiSeriesChart.Show();
            MultiSeriesChart.Text = "Multi Series Chart";
            MultiSeriesChart.MChart.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
            MultiSeriesChart.MChart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
            MultiSeriesChart.MChart.ChartAreas[0].AxisX.Title = ttxtcap[0];
            MultiSeriesChart.MChart.ChartAreas[0].AxisX.TitleFont= new Font("Arial", 12, FontStyle.Regular);
            MultiSeriesChart.MChart.ChartAreas[0].AxisY.Title = ttxtcap[1];// "Density Count/(km^2* day) ";
            MultiSeriesChart.MChart.ChartAreas[0].AxisY.TitleFont = new Font("Arial", 12, FontStyle.Regular);
            Title ctitle = new Title();
            ctitle.Font = new Font("Arial", 16, FontStyle.Regular);
            ctitle.Text = XAxisItemLabel.Text+ " & "+ YAxisItemLabel.Text;
            MultiSeriesChart.MChart.Titles.Add(ctitle);

            //init conseq  list
            MultiSeriesChart.ConsequenceList.Columns.Add("Item       ");
            MultiSeriesChart.ConsequenceList.Columns.Add("Integral");
            MultiSeriesChart.ConsequenceList.Columns.Add("Int/delC");
            MultiSeriesChart.ConsequenceList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);



            for (int i=0; i<recordtables.Count;i++)
            {
                //wr informatrion in consequencelist
                            AdditionCalculateRow aclrw = new AdditionCalculateRowConst(1, 2, 20);
                            RecorderReporterIntergrate rri = new RecorderReporterIntergrate();
                            rri.readfromListView(recordtables[i].TableRecorderConatiner);

                                double integralfull=calculateRRI.calculateIntegralByArea(rri, XAxisItemLabel.Text, YAxisItemLabel.Text, aclrw, false);
                                double integralpart = calculateRRI.calculateIntegralByArea(rri, XAxisItemLabel.Text, YAxisItemLabel.Text, aclrw, true);
                                MultiSeriesChart.ConsequenceList.Items.Add(recordtables[i].Text);
                                MultiSeriesChart.ConsequenceList.Items[i].SubItems.Add(integralfull.ToString());
                                MultiSeriesChart.ConsequenceList.Items[i].SubItems.Add(integralpart.ToString());
                                MultiSeriesChart.ConsequenceList.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);


                //generateLineSeries(OpedDic.Items[i].Text,recordtables[i],{ "Count","R"});
                //generate series
                Series srs= generateLineSeries(/*OpedDic.Items[i].Text*/recordtables[i].Text, recordtables[i], ttxtcap);
                MultiSeriesChart.MChart.Series.Insert(0,srs);
                //line color set
                if(i< LineColorList.Length)
                {
                    srs.Color = LineColorList[i];
                }
                recordtables[i].CurrentSeries = srs;
                
            }
           


        }
        public Series generateLineSeries(string sername,RecordeTable rtbl,string [] columnit)
        {
            Series nnser = new Series(sername);
            
            nnser.BorderWidth = 3;
            nnser.ChartType = SeriesChartType.Line;
            nnser.IsValueShownAsLabel = false;
            nnser.Label = "#VALX,#VALY";
            for (int z = 0; z < rtbl.TableRecorderConatiner.Items.Count; z++)//row
            {
                if (!rtbl.TableRecorderConatiner.Items[z].Checked) continue;
                bool can_draw = true;
                List<double> xylist = new List<double>();

                foreach(string cit in columnit)//col
                {
                    string r = rtbl.TableRecorderConatiner.Items[z].SubItems[rtbl.indexofCapital(cit)].Text;
                    if (!Microsoft.VisualBasic.Information.IsNumeric(r))
                    {
                        can_draw = false;
                    }
                    else
                    {
                        double pvv=double.Parse(r);
                        pvv = Math.Round(pvv, 10);
                        xylist.Add(pvv);
                    }
                }

                if (can_draw)
                {
                    nnser.Points.AddXY(xylist[0], xylist[1]);
                    

                }
               
            }
            

            return nnser;
        }




        private void testbt_Click(object sender, EventArgs e)
        {
            string str = "123 456 789";
            string [] ss=str.Split();
            foreach(string s in ss)
            {
                MessageBox.Show(s);
            }
            //RecorderTablePlus rtp = new RecorderTablePlus();
            //rtp.Show();

            //openConsequenceRecorder(fteststr);
        }

        private void button1_Click(object sender, EventArgs e)
        {
           // MessageBox.Show(srtbl.TableRecorderConatiner.Items[3].SubItems[4].Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection slitnm =ListData.SelectedItems;
            foreach(ListViewItem q in slitnm){
               
                OpedDic.Items.Add(q.Text);
            }
           
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection slitnm = OpedDic.SelectedItems;
            foreach(ListViewItem q in slitnm)
            {
                OpedDic.Items.Remove(q);
            }
        }

        private void DrawPredict_Click(object sender, EventArgs e)
        {
            CloseRecordTable();



            for (int p = 0; p < OpedDic.Items.Count; p++)
            {

                if (OpedDic.Items[p].Text.Contains(":\\"))
                {
                    openConsequenceRecorder(OpedDic.Items[p].Text, p);
                    continue;
                }


                string oppth = PathText.Text + "\\" + OpedDic.Items[p].Text;
                //find pre
                string[] gdic = Directory.GetDirectories(oppth);
                foreach (string diall in gdic)
                {
                    if (diall.Contains("Prediction")) { oppth = diall; break; }
                }


                //oppth += ("\\" + "Valid" + "\\");
                if (!Directory.Exists(oppth))
                {
                    MessageBox.Show("Error Valid Pred " + oppth);
                    return;
                }
                string[] allfls = Directory.GetFiles(oppth);
                string validreport = "";
                foreach (string fnm in allfls)
                {
                    if (fnm.Contains("Perc.csv"))
                    {
                        validreport = fnm;
                    }
                }
                if (validreport.Length != 0) openConsequenceRecorder(validreport);
                else MessageBox.Show("No report SOM Data");
            }
            drawMultiCDLineByRecordtables();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            CloseRecordTable();
        }
        private string GetUpLevelDirectory(string path, int upLevel)
        {
            var directory = File.GetAttributes(path).HasFlag(FileAttributes.Directory)
                ? path
                : Path.GetDirectoryName(path);

            upLevel = upLevel < 0 ? 0 : upLevel;

            for (var i = 0; i < upLevel; i++)
            {
                directory = Path.GetDirectoryName(directory);
            }

            return directory;
        }
        private void XAnalysis_Click(object sender, EventArgs e)
        {
            const int showOnTablePrecision = 5;
            int usedRowNumber = 0;
            //MessageBox.Show(calculateRRI.CALTRIANGLE(0.0,0.0,10.0,0,0,10.0).ToString());
            EnvSomLV.Clear();
            PosSOMLV.Clear();

            EnvSomLV.Items.Add(new ListViewItem("IntArea"));
            PosSOMLV.Items.Add(new ListViewItem("IntArea"));
            EnvSomLV.Columns.Add("ITEM\\RE");
            PosSOMLV.Columns.Add("ITEM\\RE");

            List<double> envrc = new List<double>(), Uenvrc = new List<double>();
            List<double> posrc = new List<double>(), Uposrc = new List<double>();
            //init end----------------------------------------------------------

            foreach (ListViewItem lvi in ListData.Items)
            {
                string fdnm = lvi.Text;

                string oppth = PathText.Text + "\\" + fdnm + ("\\" + "Valid" + "\\");


                string[] mfls = Directory.GetFiles(oppth);
                foreach (string nn in mfls)
                {
                    if (nn.Contains("SOMPerc"))
                    {
                        oppth = nn;
                        break;
                    }
                }
                //tell env or pos
                RecorderReporterIntergrate rri = new RecorderReporterIntergrate();
                rri.readRecordReport(oppth);


                /*
                //prediction strt
                string opprd = PathText.Text + "\\" + fdnm + ("\\");
                string[] lstprd = Directory.GetDirectories(opprd);
                foreach (string nn in lstprd)
                {
                    if (nn.Contains("Prediction"))
                    {
                        opprd = nn + "\\";
                        break;
                    }
                }
                string[] fnprd = Directory.GetFiles(opprd);
                foreach (string nn in fnprd)
                {
                    if (nn.Contains("SOMPerc"))
                    {
                        opprd = nn;
                        break;
                    }
                }
                RecorderReporterIntergrate rdrp = new RecorderReporterIntergrate();
                rdrp.readRecordReport(opprd);
                //prediction end
                */

                //Loading end-----------------------------------------------------------------









                if (fdnm.Contains("ParClEN"))
                {
                    EnvSomLV.Columns.Add(fdnm);
                    double intarea=calculateRRI.calculateIntegralByArea(rri,XAxisItemLabel.Text,YAxisItemLabel.Text, additioncalculaterowMethod, false);
                    string istr = Math.Round(intarea, showOnTablePrecision).ToString();
                    envrc.Add(intarea);
                    EnvSomLV.Items[usedRowNumber].SubItems.Add(istr);
                    //cal utility
                    double uintarea = calculateRRI.calculateIntegralByArea(rri, XAxisItemLabel.Text, YAxisItemLabel.Text, additioncalculaterowMethod, true);
                    Uenvrc.Add(uintarea);
                    // EnvSomLV.Items.Add();
                }
                else if (fdnm.Contains("ParPOS"))
                {
                    PosSOMLV.Columns.Add(fdnm);
                    double intarea = calculateRRI.calculateIntegralByArea(rri, XAxisItemLabel.Text, YAxisItemLabel.Text, additioncalculaterowMethod, false);
                    string istr = Math.Round(intarea, showOnTablePrecision).ToString();
                    posrc.Add(intarea);
                    PosSOMLV.Items[usedRowNumber].SubItems.Add(istr);
                    //PosSOMLV.Items.Add();
                    //cal utility
                    double uintarea = calculateRRI.calculateIntegralByArea(rri, XAxisItemLabel.Text, YAxisItemLabel.Text, additioncalculaterowMethod, true);
                    Uposrc.Add(uintarea);

                }
                else
                {
                    MessageBox.Show("Error Analysis " + oppth);
                }


            }

            /*
            //set columnsz
            for (int z = 0; z < EnvSomLV.Columns.Count; z++)
            {
                EnvSomLV.AutoResizeColumn(z, ColumnHeaderAutoResizeStyle.HeaderSize);
            }
            for (int z = 0; z < PosSOMLV.Columns.Count; z++)
            {
                PosSOMLV.AutoResizeColumn(z, ColumnHeaderAutoResizeStyle.HeaderSize);
            }
            */




            //rank-------------------------------------------------------------------------------------------------
            {
                usedRowNumber += 1;
                EnvSomLV.Items.Add(new ListViewItem("IntAreaRK"));//[1]
                PosSOMLV.Items.Add(new ListViewItem("IntAreaRK"));//[1]
                List<int> envrk = getRankList.getRank(envrc);
                List<int> posrk = getRankList.getRank(posrc);
                for (int i = 0; i < envrc.Count; i++)
                {
                    EnvSomLV.Items[usedRowNumber].SubItems.Add(envrk[i].ToString());
                }
                for (int i = 0; i < posrc.Count; i++)
                {
                    PosSOMLV.Items[usedRowNumber].SubItems.Add(posrk[i].ToString());
                }

            }
            //cal ori utility
            usedRowNumber += 1;
            EnvSomLV.Items.Add(new ListViewItem("Utl"));//[2]
            PosSOMLV.Items.Add(new ListViewItem("Utl"));
            for (int i = 0; i < Uenvrc.Count; i++)
            {
                EnvSomLV.Items[usedRowNumber].SubItems.Add(Math.Round(Uenvrc[i],showOnTablePrecision).ToString());
            }
            for (int i = 0; i < Uposrc.Count; i++)
            {
                PosSOMLV.Items[usedRowNumber].SubItems.Add(Math.Round(Uposrc[i],showOnTablePrecision).ToString());
            }
            //rnk utlt
            usedRowNumber += 1;
            EnvSomLV.Items.Add(new ListViewItem("UtlRK"));//[3]
            PosSOMLV.Items.Add(new ListViewItem("UtlRK"));
            List<int> uenvrk = getRankList.getRank(Uenvrc);//[3]
            List<int> uposrk = getRankList.getRank(Uposrc);
            for (int i = 0; i < uenvrk.Count; i++)
            {
                EnvSomLV.Items[usedRowNumber].SubItems.Add(uenvrk[i].ToString());
            }
            for (int i = 0; i < uposrk.Count; i++)
            {
                PosSOMLV.Items[usedRowNumber].SubItems.Add(uposrk[i].ToString());
            }





            //cal prd
            if (RunAnalysisPrdChk.Checked==true) {
             usedRowNumber += 1;
            EnvSomLV.Items.Add(new ListViewItem("prIA"));//[4]
            PosSOMLV.Items.Add(new ListViewItem("prIA"));//[4]
                                                         // EnvSomLV.Columns.Add("ITEM\\RE");
                                                         //PosSOMLV.Columns.Add("ITEM\\RE");

            List<double> prenvrc = new List<double>(), Uprenvrc = new List<double>();
            List<double> prposrc = new List<double>(), Uprposrc = new List<double>();
            //init end----------------------------------------------------------

            foreach (ListViewItem lvi in ListData.Items)
            {
                string fdnm = lvi.Text;
                /*
                string oppth = PathText.Text + "\\" + fdnm + ("\\" + "Valid" + "\\");


                string[] mfls = Directory.GetFiles(oppth);
                foreach (string nn in mfls)
                {
                    if (nn.Contains("SOMPerc"))
                    {
                        oppth = nn;
                        break;
                    }
                }
                //tell env or pos
                RecorderReporterIntergrate rri = new RecorderReporterIntergrate();
                rri.readRecordReport(oppth);
                */


                //prediction strt
                string opprd = PathText.Text + "\\" + fdnm + ("\\");
                string[] lstprd = Directory.GetDirectories(opprd);
                foreach (string nn in lstprd)
                {
                    if (nn.Contains("Prediction"))
                    {
                        opprd = nn + "\\";
                        break;
                    }
                }
                string[] fnprd = Directory.GetFiles(opprd);
                foreach (string nn in fnprd)
                {
                    if (nn.Contains("SOMPerc"))
                    {
                        opprd = nn;
                        break;
                    }
                }
                RecorderReporterIntergrate rdrp = new RecorderReporterIntergrate();
                rdrp.readRecordReport(opprd);
                //prediction end


                //Loading end-----------------------------------------------------------------









                if (fdnm.Contains("ParClEN"))
                {
                       
                    double intarea = calculateRRI.calculateIntegralByArea(rdrp, XAxisItemLabel.Text, YAxisItemLabel.Text, additioncalculaterowMethod, false);
                    string istr = Math.Round(intarea, showOnTablePrecision).ToString();
                    prenvrc.Add(intarea);
                        EnvSomLV.Items[usedRowNumber].SubItems.Add(istr);
                    Uprenvrc.Add(calculateRRI.calculateIntegralByArea(rdrp, XAxisItemLabel.Text, YAxisItemLabel.Text, additioncalculaterowMethod, true));
                   
                }
                else if (fdnm.Contains("ParPOS"))
                {
                        
                    double intarea = calculateRRI.calculateIntegralByArea(rdrp, XAxisItemLabel.Text, YAxisItemLabel.Text, additioncalculaterowMethod, false);
                    string istr = Math.Round(intarea, showOnTablePrecision).ToString();
                    prposrc.Add(intarea);
                        PosSOMLV.Items[usedRowNumber].SubItems.Add(istr);
                    Uprposrc.Add(calculateRRI.calculateIntegralByArea(rdrp, XAxisItemLabel.Text, YAxisItemLabel.Text, additioncalculaterowMethod, true));
                    
                }
                else
                {
                    MessageBox.Show("Error Analysis " + opprd);
                }


            }

            //rank pr-------------------------------------------------------------------------------------------------
            
                usedRowNumber += 1;
                EnvSomLV.Items.Add(new ListViewItem("prIARK"));//[5]
                PosSOMLV.Items.Add(new ListViewItem("prIARK"));//[5]
                List<int> envrk = getRankList.getRank(prenvrc);
                List<int> posrk = getRankList.getRank(prposrc);
                for (int i = 0; i < envrc.Count; i++)
                {
                    EnvSomLV.Items[usedRowNumber].SubItems.Add(envrk[i].ToString());
                }
                for (int i = 0; i < posrc.Count; i++)
                {
                    PosSOMLV.Items[usedRowNumber].SubItems.Add(posrk[i].ToString());
                }

            
                //end rkpr
                //cal utl prd
                usedRowNumber += 1;
                EnvSomLV.Items.Add(new ListViewItem("prUtl"));//[6]
                PosSOMLV.Items.Add(new ListViewItem("prUtl"));//[6]
                for (int i = 0; i < Uprenvrc.Count; i++)
                {
                    EnvSomLV.Items[usedRowNumber].SubItems.Add(Math.Round(Uprenvrc[i],showOnTablePrecision).ToString());
                }
                for (int i = 0; i < Uprposrc.Count; i++)
                {
                    PosSOMLV.Items[usedRowNumber].SubItems.Add(Math.Round(Uprposrc[i],showOnTablePrecision).ToString());
                }

                usedRowNumber += 1;
                EnvSomLV.Items.Add(new ListViewItem("prUtlRK"));//[7]
                PosSOMLV.Items.Add(new ListViewItem("prUtlRK"));//[7]
                List<int> uprenvrk = getRankList.getRank(Uprenvrc);
                List<int> uprposrk = getRankList.getRank(Uprposrc);
                for (int i = 0; i < uprenvrk.Count; i++)
                {
                    EnvSomLV.Items[usedRowNumber].SubItems.Add(uprenvrk[i].ToString());
                }
                for (int i = 0; i < uprposrk.Count; i++)
                {
                    PosSOMLV.Items[usedRowNumber].SubItems.Add(uprposrk[i].ToString());
                }





            }
            //set columnsz
            for (int z = 0; z < EnvSomLV.Columns.Count; z++)
            {
                EnvSomLV.AutoResizeColumn(z, ColumnHeaderAutoResizeStyle.HeaderSize);
            }
            for (int z = 0; z < PosSOMLV.Columns.Count; z++)
            {
                PosSOMLV.AutoResizeColumn(z, ColumnHeaderAutoResizeStyle.HeaderSize);
            }













            // end of prediction condition

            //start here  



















        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpedDic.Clear();
        }

        private void QuickOPd_Click(object sender, EventArgs e)
        {
            int v;
            if(int.TryParse(QKNM.Text, out v))
            {
                //OpedDic.Clear();
                OpedDic.Items.Add("ParClEN"+v.ToString());
                OpedDic.Items.Add("ParPOS"+v.ToString());
            }

          
              
        }
        private List<ChartDrawer> RMSEChartCtrlList = new List<ChartDrawer>();
        private void ViewRMSEBtn_Click(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection slitnm = ListData.SelectedItems;
            foreach (ListViewItem q in slitnm)
            {
                List<double> itcrd = new List<double>(), rmsecrd = new List<double>();
                string oppth = PathText.Text + "\\" + q.Text;
                oppth += ("\\" + "Train" + "\\");
                if (!Directory.Exists(oppth))
                {
                    MessageBox.Show("Error Valid Path " + oppth);
                    return;
                }
                string[] allfls = Directory.GetFiles(oppth);
                string rmsereport = "";
                foreach (string fnm in allfls)
                {
                    if (fnm.Contains("ProcRec.csv"))
                    {
                        rmsereport = fnm;
                    }
                }
                StreamReader rmserpt = new StreamReader(rmsereport);

                string rptcpln="";
                    while((rptcpln= rmserpt.ReadLine())!= null)
                    {
                        rptcpln=rptcpln.Trim();
                        rptcpln=   rptcpln.Replace(',', ' ');
                        rptcpln = rptcpln.Replace(';', ' ');
                    string[] du=  rptcpln.Split();
                        for(int i=0;i<du.Length ;i++)
                        {
                            if (du[i].Contains("Iter:"))
                            {
                                double k=Double.Parse(du[i + 1]);
                                itcrd.Add(k);
                            }
                            else if (du[i].Contains("RMSE:"))
                            {
                                double k = Double.Parse(du[i + 1]);
                                rmsecrd.Add(k);
                            }
                            else
                            {

                            }
                        }


                    }



                rmserpt.Close();

                ChartDrawer rmsecdrw = new ChartDrawer();
                
                //call chart
                rmsecdrw.ControlBox = false;
                string[] ttxtcap = { "Epochs", "RMSE" };
                
                rmsecdrw.Text = "Epoch & RMSE Chart";
                rmsecdrw.MChart.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
                rmsecdrw.MChart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
                rmsecdrw.MChart.ChartAreas[0].AxisX.Title = ttxtcap[0];
                rmsecdrw.MChart.ChartAreas[0].AxisX.TitleFont = new Font("Arial", 12, FontStyle.Regular);
                rmsecdrw.MChart.ChartAreas[0].AxisY.Title = ttxtcap[1];
                rmsecdrw.MChart.ChartAreas[0].AxisY.TitleFont = new Font("Arial", 12, FontStyle.Regular);
                Title ctitle = new Title();
                ctitle.Font = new Font("Arial", 16, FontStyle.Regular);
                ctitle.Text = "Epoch & RMSE " + q.Text;
                rmsecdrw.MChart.Titles.Add(ctitle);
                Series rser = new Series("RMSE");
                rser.BorderWidth = 3;
                rser.ChartType = SeriesChartType.Line;
                rser.IsValueShownAsLabel = false;
                rser.SmartLabelStyle.IsOverlappedHidden = false;
                //rser.Label = "#VALY";
                rmsecdrw.SwitchControllerTagReferenceSample = "#VALY";
                for (int u=0; u< itcrd.Count; u++)
                {
                    rser.Points.AddXY(itcrd[u],rmsecrd[u]);
                }




                rser.Color = Color.Black;
                rmsecdrw.MChart.Series.Add(rser);
                // write rmse on conseq list
                rmsecdrw.ConsequenceList.Columns.Add("Epoch");
                rmsecdrw.ConsequenceList.Columns.Add("RMSE");
                for (int u = 0; u < itcrd.Count; u++)
                {
                    rmsecdrw.ConsequenceList.Items.Add(itcrd[u].ToString());
                    //rmsecdrw.ConsequenceList.Items[u].SubItems.Add(itcrd[u].ToString());
                    rmsecdrw.ConsequenceList.Items[u].SubItems.Add(rmsecrd[u].ToString());

                    //rser.Points.AddXY(itcrd[u], rmsecrd[u]);
                }


                rmsecdrw.Show();
                RMSEChartCtrlList.Add(rmsecdrw);
                //rmsecdrw.MChart.ChartAreas[0].AxisY.Maximum = rmsecrd.Max();
                //rmsecdrw.MChart.ChartAreas[0].AxisY.Minimum = rmsecrd.Min();
                //rmsecdrw.MChart.ChartAreas[0].RecalculateAxesScale();











            }
           



        }

        private void calIntbyfl_Click(object sender, EventArgs e)
        {
            ofd4calint.ShowDialog();

            string rpfn = ofd4calint.FileName;
            ConsoleWrBox.Text += "cal integrate \"" + rpfn + "\"\n";
            if (rpfn == "")
            {
                ConsoleWrBox.Text += "no seleect fn!\n";
                return;
            }
            
            RecorderReporterIntergrate rrpt = new RecorderReporterIntergrate();
            try {
                rrpt.readRecordReport(rpfn);
            } catch {
                return;
            }
            
            double intvl=calculateRRI.calculateIntegralByArea(rrpt, XAxisItemLabel.Text, YAxisItemLabel.Text, additioncalculaterowMethod, false);
            ConsoleWrBox.Text += "val = " +intvl.ToString()+"\n";

        }

        private void CLSXX_Click(object sender, EventArgs e)
        {
            ConsoleWrBox.Clear();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            foreach(ChartDrawer cd in RMSEChartCtrlList)
            {
                cd.Close();
            }
            RMSEChartCtrlList.Clear();
            
        }

        private void RunAnalysisPrdChk_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", @PathText.Text);
        }

        private void OP_RCDtblManual_Click(object sender, EventArgs e)
        {
            OFD_rcdtblman.ShowDialog();
            string rcdflnm = OFD_rcdtblman.FileName;
            ConsoleWrBox.Text += ("open " + rcdflnm + "\n");
            if (rcdflnm == "")
            {
                ConsoleWrBox.Text += ("err open " + rcdflnm + "\n");
                return;
            }
            try
            {
                openConsequenceRecorder(rcdflnm);
            }
            catch
            {

            }
            
            //drawMultiCDLineByRecordtables();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            ofd4calint.ShowDialog();

            string rpfn = ofd4calint.FileName;
            ConsoleWrBox.Text += "cal integrate \"" + rpfn + "\"\n";
            if (rpfn == "")
            {
                ConsoleWrBox.Text += "no seleect fn!\n";
                return;
            }

            RecorderReporterIntergrate rrpt = new RecorderReporterIntergrate();
            try
            {
                rrpt.readRecordReport(rpfn);
            }
            catch
            {
                return;
            }

            double intvl = calculateRRI.calculateIntegralByArea(rrpt, XAxisItemLabel.Text, YAxisItemLabel.Text, additioncalculaterowMethod);
            ConsoleWrBox.Text += "val = " + intvl.ToString() + "\n";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            String clipText = string.Empty;
            clipText += ("envsom" + Environment.NewLine);
            foreach (ListViewItem item in this.EnvSomLV.Items)
            {
                for (int z = 0; z < item.SubItems.Count; z++)
                {
                    clipText += (item.SubItems[z].Text + ",");
                }

                clipText += Environment.NewLine;
            }
            clipText += ("possom" + Environment.NewLine);
            foreach (ListViewItem item in this.PosSOMLV.Items)
            {
                for (int z = 0; z < item.SubItems.Count; z++)
                {
                    clipText += (item.SubItems[z].Text + ",");
                }

                clipText += Environment.NewLine;
            }

            if (!String.IsNullOrEmpty(clipText))
            {
                Clipboard.SetText(clipText);
            }
        }

        private void OpedDic_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show(OpedDic.SelectedItems[0].Text);
            
        }

        private void Opeextfiles_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void OpdicExtrFn_Click(object sender, EventArgs e)
        {
            Opeextfiles.ShowDialog();
            string kk=Opeextfiles.FileName;
            if (kk.Length!=0)
            {
                OpedDic.Items.Add(kk);
            }
        }

        private void XAxisItemLabel_TextChanged(object sender, EventArgs e)
        {

        }

        private AdditionCalculateRow additioncalculaterowMethod=new AdditionCalculateRowConst(1,2,20);
        private void CalculateIntegralAdditionalComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
           int cbselbx= CalculateIntegralAdditionalComboBox.SelectedIndex;
            if (cbselbx == 0)
            {
                additioncalculaterowMethod = new AdditionCalculateRowConst(1, 2, 20);
            }
            else if(cbselbx == 1)
            {
                string inputParas=Microsoft.VisualBasic.Interaction.InputBox("Input [X][exp(u)~exp[v]],[Y][exp(w)~exp(x)] u,v,w,x,elements,rows:", "Input Exponential Parameters", "0,0,0,10,2,20");
                if (inputParas.Length != 0)
                {
                    ConsoleWrBox.Text += "Input [X][exp(u)~exp[v]],[Y][exp(w)~exp(x)] u,v,w,x,elements,rows=" + inputParas+"\n";
                    string[] paramtarr = inputParas.Trim().Split(',');
                    double du = double.Parse(paramtarr[0]);
                    double dv = double.Parse(paramtarr[1]);
                    double dw = double.Parse(paramtarr[2]);
                    double dx = double.Parse(paramtarr[3]);
                    int elen = int.Parse(paramtarr[4]);
                    int rwn = int.Parse(paramtarr[5]);
                    additioncalculaterowMethod = new AdditionCalculateRowExponential(new List<List<double>> { new List<double> { du, dv }, new List<double> { dw, dx } }, elen, rwn);
                }
                
            }
            else
            {

            }
        }

        private void MoreToolsBtn_Click(object sender, EventArgs e)
        {
            if (this.Width != 1500)
            {
                this.Width = 1500;
                MoreToolsBtn.Text = "Less Tools <<";
            }
            else
            {
                this.Width = 350;
                MoreToolsBtn.Text = "More Tools >>";
                //MessageBox.Show(this.Width.ToString());
            }


        }
    }
}
