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

namespace PatternModify
{
    public partial class ChartModify : Form
    {
        Chart MChart = null;
        public ChartModify(Chart cchart)
        {
            MChart = cchart;
            InitializeComponent();
            LoadInitSetter();
        }
        protected void LoadInitSetter()
        {
            ChartWidthTbx.Text = MChart.Width.ToString();
            ChartHeighTbx.Text = MChart.Height.ToString();

            TitleTbx.Text = MChart.Titles[0].Text;
            NoTitleChkbx.Checked = !MChart.Titles[0].Visible;
            TitleFontSizeTbx.Text = MChart.Titles[0].Font.Size.ToString();
            //x axis
            XAxisTbx.Text=   MChart.ChartAreas[0].AxisX.Title;
            XAxisFontSizeTbx.Text = MChart.ChartAreas[0].AxisX.TitleFont.Size.ToString();
            MajorTickMarkXChkbx.Checked = MChart.ChartAreas[0].AxisX.MajorTickMark.Enabled;
            XAxisValueFormatTbx.Text = MChart.ChartAreas[0].AxisX.LabelStyle.Format;
            //y axis
            YAxisTbx.Text = MChart.ChartAreas[0].AxisY.Title;
            YAxisFontSizeTbx.Text= MChart.ChartAreas[0].AxisY.TitleFont.Size.ToString();
            MajorTickMarkYChkbx.Checked = MChart.ChartAreas[0].AxisY.MajorTickMark.Enabled;
            YAxisValueFormatTbx.Text = MChart.ChartAreas[0].AxisY.LabelStyle.Format;



            {
                IsLegendCbx.Checked = MChart.Legends[0].Enabled;
            }

            if (MChart.ChartAreas[0].AxisX.MajorGrid.LineColor == Color.LightGray && MChart.ChartAreas[0].AxisY.MajorGrid.LineColor == Color.LightGray)
            {
                FrameChk.Checked = true;
            }

            //advance setter intervel and angal
                XAxisLabelStartTbx.Text = MChart.ChartAreas[0].AxisX.Minimum.ToString();
                XAxisLabelIntervelTbx.Text = MChart.ChartAreas[0].AxisX.LabelStyle.Interval.ToString();
                XAxisLabelEndTbx.Text = MChart.ChartAreas[0].AxisX.Maximum.ToString();

            YAxisLabelStartTbx.Text = MChart.ChartAreas[0].AxisY.Minimum.ToString();
            YAxisLabelIntervelTbx.Text = MChart.ChartAreas[0].AxisY.LabelStyle.Interval.ToString();
            YAxisLabelEndTbx.Text = MChart.ChartAreas[0].AxisY.Maximum.ToString();

            XAxisLabelAnglelTbx.Text = MChart.ChartAreas[0].AxisX.LabelStyle.Angle.ToString();
            YAxisLabelAnglelTbx.Text = MChart.ChartAreas[0].AxisY.LabelStyle.Angle.ToString();
            XAxisLabelFontSizeTbx.Text = MChart.ChartAreas[0].AxisX.LabelStyle.Font.Size.ToString();
            YAxisLabelFontSizeTbx.Text = MChart.ChartAreas[0].AxisY.LabelStyle.Font.Size.ToString();
            //check marker style is point
            bool markerstyleiscircle = false;
            for (int i = 0; i < MChart.Series.Count; i++)
            {
                if (MChart.Series[i].MarkerStyle != MarkerStyle.None) { markerstyleiscircle = true; break; }
            }
            MarkerPointChkbx.Checked = markerstyleiscircle;

            SeriesLabelsTbx.Text = MChart.Series[0].Label;
            //series for change color
            for(int i=0; i < MChart.Series.Count; i++)
            {
                SeriesColorsSelectorCbx.Items.Add(MChart.Series[i].Name);
            }

            SeriesColorsSelectorCbx.Text = "Choose Series";
            


        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void ChartModify_Load(object sender, EventArgs e)
        {

        }

        private void TitleTbx_TextChanged(object sender, EventArgs e)
        {
            MChart.Titles[0].Text = TitleTbx.Text;

        }

        private void TitleFontSizeTbx_TextChanged(object sender, EventArgs e)
        {
            float fsz = 0;
            bool isflt = float.TryParse(TitleFontSizeTbx.Text, out fsz);
            if (isflt) { Font dffont = new Font(FontFamily.GenericSansSerif, fsz, FontStyle.Regular);
                MChart.Titles[0].Font = dffont;
            }
        }

        private void XAxisTbx_TextChanged(object sender, EventArgs e)
        {
            MChart.ChartAreas[0].AxisX.Title = XAxisTbx.Text;
        }

        private void YAxisTbx_TextChanged(object sender, EventArgs e)
        {
            MChart.ChartAreas[0].AxisY.Title = YAxisTbx.Text;
        }

        private void FrameChk_CheckedChanged(object sender, EventArgs e)
        {
            if (FrameChk.Checked)
            {
                MChart.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray; MChart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
            }
            else
            {
                MChart.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Transparent; MChart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Transparent;
            }
        }

        private void IsLegendCbx_CheckedChanged(object sender, EventArgs e)
        {
            MChart.Legends[0].Enabled = IsLegendCbx.Checked;
        }

        private void XAxisFontSizeTbx_TextChanged(object sender, EventArgs e)
        {
            float fsz = 0;
            bool isflt = float.TryParse(XAxisFontSizeTbx.Text, out fsz);
            if (isflt)
            {
                Font dffont = new Font(FontFamily.GenericSansSerif, fsz, FontStyle.Regular);
                MChart.ChartAreas[0].AxisX.TitleFont = dffont;
            }
        }

        private void YAxisFontSizeTbx_TextChanged(object sender, EventArgs e)
        {
            float fsz = 0;
            bool isflt = float.TryParse(YAxisFontSizeTbx.Text, out fsz);
            if (isflt)
            {
                Font dffont = new Font(FontFamily.GenericSansSerif, fsz, FontStyle.Regular);
                MChart.ChartAreas[0].AxisY.TitleFont = dffont;
            }

        }

        private void ModifyLegendBtn_Click(object sender, EventArgs e)
        {
            string prptxt=SeriesLegendPropRtbx.Text;
            if (prptxt.Count() != 0)
            {
               string[] prparr = prptxt.Split('\n');
                if (prparr[prparr.Count() - 1] == "") Array.Resize(ref prparr, prparr.Length - 1);
                if (prparr.Count() == MChart.Series.Count())
                {
                    for(int i=0;i< MChart.Series.Count(); i++)
                    {
                        MChart.Series[i].LegendText = prparr[i];
                        MChart.Series[i].Name = prparr[i];
                    }
                    
                }
                else
                {
                    MessageBox.Show("Error series legend numbers", "Error series legend numbers =" + prparr.Count().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void XAxisLabelIntervelTbx_TextChanged(object sender, EventArgs e)
        {
            double ivl;
            bool isg=double.TryParse(XAxisLabelIntervelTbx.Text,out ivl);
            if (isg)
            {
                MChart.ChartAreas[0].AxisX.LabelStyle.Interval = ivl;
                
            }
            
        }

        private void YAxisLabelIntervelTbx_TextChanged(object sender, EventArgs e)
        {
            double ivl;
            bool isg = double.TryParse(YAxisLabelIntervelTbx.Text, out ivl);
            if (isg)
            {
                MChart.ChartAreas[0].AxisY.LabelStyle.Interval = ivl;
            }
        }

        private void XAxisLabelAnglelTbx_TextChanged(object sender, EventArgs e)
        {
            int ivl;
            bool isg = int.TryParse(XAxisLabelAnglelTbx.Text, out ivl);
            if (ivl > 90 || ivl < -90) isg = false;
            if (isg)
            {
                MChart.ChartAreas[0].AxisX.LabelStyle.Angle = ivl;
            }
        }

        private void YAxisLabelAnglelTbx_TextChanged(object sender, EventArgs e)
        {
            int ivl;
            bool isg = int.TryParse(YAxisLabelAnglelTbx.Text, out ivl);
            if (ivl > 90 || ivl < -90) isg = false; 
            if (isg)
            {
                MChart.ChartAreas[0].AxisY.LabelStyle.Angle = ivl;
            }
        }

        private void XAxisLabelFontSizeTbx_TextChanged(object sender, EventArgs e)
        {
            float fsz = 0;
            bool isflt = float.TryParse(XAxisLabelFontSizeTbx.Text, out fsz);
            if (isflt)
            {
                Font dffont = new Font(FontFamily.GenericSansSerif, fsz, FontStyle.Regular);
                MChart.ChartAreas[0].AxisX.LabelStyle.Font = dffont;
            }
        }

        private void YAxisLabelFontSizeTbx_TextChanged(object sender, EventArgs e)
        {
            float fsz = 0;
            bool isflt = float.TryParse(YAxisLabelFontSizeTbx.Text, out fsz);
            if (isflt)
            {
                Font dffont = new Font(FontFamily.GenericSansSerif, fsz, FontStyle.Regular);
                MChart.ChartAreas[0].AxisY.LabelStyle.Font = dffont;
            }

        }

        private void MarkerPointChkbx_CheckedChanged(object sender, EventArgs e)
        {
            if (MarkerPointChkbx.Checked)
            {
                for (int i = 0; i < MChart.Series.Count; i++)
                {
                    if (MChart.Series[i].MarkerStyle != MarkerStyle.Circle)
                    {
                        
                        MChart.Series[i].MarkerStyle = MarkerStyle.Circle;
                        //decide marker size
                        MarkerSizeTbx_TextChanged(null, null);


                    }
                }
            }
            else
            {
                for (int i = 0; i < MChart.Series.Count; i++)
                    MChart.Series[i].MarkerStyle = MarkerStyle.None;
            }
           
            
        }

        private void MarkerSizeTbx_TextChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < MChart.Series.Count; i++)
            {
                int defaultMarkerSize = 20;
                MChart.Series[i].MarkerSize = defaultMarkerSize;
                int rsultmksz;
                if (int.TryParse(MarkerSizeTbx.Text, out rsultmksz))
                {
                    MChart.Series[i].MarkerSize = rsultmksz;
                }
            }
        }

        private void XAxisLabelStartTbx_TextChanged(object sender, EventArgs e)
        {

            double num;
            bool is_nu = double.TryParse(XAxisLabelStartTbx.Text,out num);
            try
            {
                if (num < double.Parse(XAxisLabelEndTbx.Text)&&is_nu) MChart.ChartAreas[0].AxisX.Minimum = num;
            }
            catch
            {

            }
        }

        private void XAxisLabelEndTbx_TextChanged(object sender, EventArgs e)
        {
            double num;
            bool is_nu = double.TryParse(XAxisLabelEndTbx.Text, out num);
            try
            {
                if (num > double.Parse(XAxisLabelStartTbx.Text)&&is_nu) MChart.ChartAreas[0].AxisX.Maximum = num;
            }
            catch
            {

            }

        }

        private void YAxisLabelStartTbx_TextChanged(object sender, EventArgs e)
        {
            double num;
            bool is_nu = double.TryParse(YAxisLabelStartTbx.Text, out num);
            try
            {
                if (num < double.Parse(YAxisLabelEndTbx.Text)&&is_nu) MChart.ChartAreas[0].AxisY.Minimum = num;
            }
            catch
            {

            }

        }

        private void YAxisLabelEndTbx_TextChanged(object sender, EventArgs e)
        {
            double num;
            bool is_nu = double.TryParse(YAxisLabelEndTbx.Text, out num);
            try
            {
                if (num > double.Parse(YAxisLabelStartTbx.Text)&&is_nu) MChart.ChartAreas[0].AxisY.Maximum = num;
            }
            catch
            {

            }
            

        }

        private void ChartWidthTbx_TextChanged(object sender, EventArgs e)
        {
            int sztx;
            bool is_nu = int.TryParse(ChartWidthTbx.Text, out sztx);
            MChart.Width = sztx;
        }

        private void ChartHeighTbx_TextChanged(object sender, EventArgs e)
        {
            int sztx;
            bool is_nu = int.TryParse(ChartHeighTbx.Text, out sztx);
            MChart.Height = sztx;
        }

        private void NoTitleChkbx_CheckedChanged(object sender, EventArgs e)
        {
            MChart.Titles[0].Visible =(! NoTitleChkbx.Checked);
            
        }

        private void MajorTickMarkYChkbx_CheckedChanged(object sender, EventArgs e)
        {
            MChart.ChartAreas[0].AxisY.MajorTickMark.Enabled = MajorTickMarkYChkbx.Checked;
            MChart.ChartAreas[0].AxisY2.MajorTickMark.Enabled = MajorTickMarkYChkbx.Checked;
        }

        private void MajorTickMarkXChkbx_CheckedChanged(object sender, EventArgs e)
        {
            MChart.ChartAreas[0].AxisX.MajorTickMark.Enabled = MajorTickMarkXChkbx.Checked;
            
        }

        private void classicModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChartWidthTbx.Text = "500";
            ChartHeighTbx.Text = "900";
            IsLegendCbx.Checked = false;
            NoTitleChkbx.Checked = true;
            NoTitleChkbx_CheckedChanged(null,null);
            MajorTickMarkXChkbx.Checked = false;
            MajorTickMarkYChkbx.Checked = false;
            FrameChk.Checked = false;
            SetXRangeAutoBtn_Click(null,null);
            //SetYRangeBtn_Click(null, null);

        }

        private void SeriesLabelBtn_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < MChart.Series.Count; i++)
            {
                try
                {
                    MChart.Series[i].Label = SeriesLabelsTbx.Text;
                }
                catch
                {

                }

            }
        }

        private void XAxisValueFormatTbx_TextChanged(object sender, EventArgs e)
        {
            MChart.ChartAreas[0].AxisX.LabelStyle.Format = XAxisValueFormatTbx.Text;
        }

        private void YAxisValueFormatTbx_TextChanged(object sender, EventArgs e)
        {
            MChart.ChartAreas[0].AxisY.LabelStyle.Format = YAxisValueFormatTbx.Text;
        }

        private void SeriesChangeColorBtn_Click(object sender, EventArgs e)
        {
            ColorDialog cdlg = new ColorDialog();
            cdlg.ShowDialog();
            if (!cdlg.Color.IsEmpty)
            {
                for(int i=0; i < MChart.Series.Count; i++)
                {
                    if (MChart.Series[i].Name == SeriesColorsSelectorCbx.Text)
                    {
                       MChart.Series[i].Color=    cdlg.Color;
                        break;
                    }
                }
            }
        }

        private void rMSEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XAxisLabelStartTbx.Text = "0";
            XAxisLabelEndTbx.Text = "301";
            XAxisLabelIntervelTbx.Text = "50";
            DengueCheckInterface.ChartModifyMode.RMSEMD(XAxisTbx,YAxisTbx);
        }

        private void caseDensityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DengueCheckInterface.ChartModifyMode.CasedensityMD(XAxisTbx, YAxisTbx);
        }

        private void caseAreaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DengueCheckInterface.ChartModifyMode.CaseareaMD(XAxisTbx, YAxisTbx);
        }

        private void percentageDensityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DengueCheckInterface.ChartModifyMode.PercentagedensityMD(XAxisTbx, YAxisTbx);
        }

        private void percentageAreaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DengueCheckInterface.ChartModifyMode.PercentageareaMD(XAxisTbx, YAxisTbx);
        }

        private void dCaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextBox tmpy2 = new TextBox();
            DengueCheckInterface.ChartModifyMode.DcaseMD(XAxisTbx, YAxisTbx,tmpy2 );
            MChart.ChartAreas[0].AxisY2.Title = tmpy2.Text;
        }

        private void SetXRangeAutoBtn_Click(object sender, EventArgs e)
        {
            List<double> xlist= new List<double>();
            for(int i=0;i<MChart.Series.Count; i++)
            {
                for (int j = 0; j < MChart.Series[i].Points.Count; j++)
                {
                    xlist.Add(MChart.Series[i].Points[j].XValue);
                }
                   
                
            }

            double lcmaxx = xlist.Max();
            double lcminx = xlist.Min();
            double xstart = 0;
            double xend = 0;
            if ((Math.Log10(lcmaxx) - 1) < 2.0)
            {
                xstart = 0;

                xend = (int)((lcmaxx / 10) + 3) * 10;
            }

            else
            {
                if (XAxisStartZeroChk.Checked)
                {
                    xstart = 0;
                }
                else
                {
                    
                    xstart = ((int)(lcminx / Math.Pow(10 , (int)(Math.Log10(lcminx) - 1)) ) - 2) * Math.Pow(10 , (int)(Math.Log10(lcminx) - 1));

                    if (xstart < 100)
                    {
                       xstart = 0;
                    }      
                }


                xend = ((int)(lcmaxx / Math.Pow(10, (int)(Math.Log10(lcmaxx) - 1)))) * Math.Pow(10, (int)(Math.Log10(lcmaxx) - 1));
            }

            double xinterval = (int)((xend - xstart + 1) / 9);
            xinterval = ((int)(xinterval / 10)) * 10;
            xend = (xstart + xinterval * 10) + 1;
            XAxisLabelStartTbx.Text = xstart.ToString();
            XAxisLabelIntervelTbx.Text = xinterval.ToString();
            XAxisLabelEndTbx.Text = xend.ToString();

        }

        private void SetYRangeBtn_Click(object sender, EventArgs e)
        {
            /*
            bool isrun1= false,isrun3=false,isrun2=false;
            double ystr, yintr, yend;
            isrun1=double.TryParse(YAxisLabelStartTbx.Text,out ystr);
            isrun2=double.TryParse(YAxisLabelIntervelTbx.Text, out yintr);
            isrun3=double.TryParse(YAxisLabelEndTbx.Text, out yend);
            if(isrun1&& isrun2&& isrun3&& yintr>10)
            {
                yintr /= 1.5;
                if( Math.Log10( yintr)>=2)yintr = (int)(yintr /  Math.Pow( 10, (int)(Math.Log10(yintr)))    ) * Math.Pow(10, (int)(Math.Log10(yintr)) );
                YAxisLabelIntervelTbx.Text = yintr.ToString();
            }
            else
            {
                MessageBox.Show("Error str,int,end,yinterval" + isrun1.ToString() + "," + isrun2.ToString() + "," + isrun3.ToString()+","+" yintr < 10");
            }
            */

            List<double> ylist = new List<double>();
            for (int i = 0; i < MChart.Series.Count; i++)
            {
                for (int j = 0; j < MChart.Series[i].Points.Count; j++)
                {
                    ylist.Add(MChart.Series[i].Points[j].YValues[0]);
                }


            }

            double lcmaxy = ylist.Max();
            double lcminy = ylist.Min();
            double ystart = 0;
            double yend = 0;
            if ((Math.Log10(lcmaxy) - 1) < 2.0)
            {
                ystart = 0;

                yend = (int)((lcmaxy / 10) + 3) * 10;
            }

            else
            {
                if (YAxisStartZeroChk.Checked)
                {
                    ystart = 0;
                }
                else
                {

                    ystart = ((int)(lcminy / Math.Pow(10, (int)(Math.Log10(lcminy) - 1))) - 2) * Math.Pow(10, (int)(Math.Log10(lcminy) - 1));

                    if (ystart < 100)
                    {
                        ystart = 0;
                    }
                }


                yend = ((int)(lcmaxy / Math.Pow(10, (int)(Math.Log10(lcmaxy) - 1)))) * Math.Pow(10, (int)(Math.Log10(lcmaxy) - 1));
            }

            double yinterval = (int)((yend - ystart + 1) / 9);
            yinterval = ((int)(yinterval / 10)) * 10;
            yend = (ystart + yinterval * 10) + 1;
            YAxisLabelStartTbx.Text = ystart.ToString();
            YAxisLabelIntervelTbx.Text = yinterval.ToString();
            YAxisLabelEndTbx.Text = yend.ToString();


        }

        private void ChineseChkbx_CheckedChanged(object sender, EventArgs e)
        {
            DengueCheckInterface.ChartModifyMode.IsChinese = ChineseChkbx.Checked;
        }

        private void SeriesLabelsTbx_TextChanged(object sender, EventArgs e)
        {

        }

        private void coverageAndNumberOfCasesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DengueCheckInterface.ChartModifyMode.SeedCoverageCaseMD(XAxisTbx, YAxisTbx,XAxisValueFormatTbx);
            SetYRangeBtn_Click(null,null);
            XAxisLabelIntervelTbx.Text = ".1";
        }
    }
}
