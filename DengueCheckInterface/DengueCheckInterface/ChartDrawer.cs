using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DenInterface
{
    public partial class ChartDrawer : Form
    {
        public ChartDrawer()
        {
            InitializeComponent();

            //init

            ChartMarkerStyleCbx.SelectedIndex = 4;

            //chart init
            MChart.Legends[0].IsTextAutoFit = true;
            MChart.Legends[0].Font = new Font(FontFamily.GenericSansSerif, 14, FontStyle.Regular);
            MChart.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            MChart.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            
            //MChart.ChartAreas[0].CursorX.IsUserEnabled = true;
            MChart.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            MChart.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            MChart.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;
            MChart.ChartAreas[0].AxisX.IsStartedFromZero = false;
            

            //MChart.ChartAreas[0].CursorY.IsUserEnabled = true;
            MChart.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
            MChart.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            MChart.ChartAreas[0].AxisY.ScrollBar.IsPositionedInside = true;
            MChart.ChartAreas[0].AxisY.IsStartedFromZero = false;
            //MChart.ChartAreas[0].AxisY.Interval = 0.01;


            MChart.MouseWheel += Chart1_MouseWheel;


        }

        private void MChart_MouseWheel1(object sender, MouseEventArgs e)
        {
            //MessageBox.Show(e.Delta.ToString());
            Chart cib = (Chart)sender;
            try
            {
                if (e.Delta>0)
                {
                    cib.ChartAreas[0].AxisY.Interval /= 2;
                }
                else
                {
                    cib.ChartAreas[0].AxisY.Interval *= 2;
                }
            }
            catch { }
        }

        private void MChart_MouseWheel(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        public Series HelpMarkerSeries =null;
        private void MChart_Click(object sender, EventArgs e)
        {
            MouseEventArgs msevarg = (MouseEventArgs)e;
            //MessageBox.Show(msevarg.X.ToString()+" "+ msevarg.Y.ToString());
           HitTestResult hitrslt=  MChart.HitTest(msevarg.X, msevarg.Y);
            //init helpMarkerseries
            if (HelpMarkerSeries == null)
            {
                HelpMarkerSeries = new Series();
                HelpMarkerSeries.ChartType = SeriesChartType.Point;
                HelpMarkerSeries.IsVisibleInLegend = false;
                MChart.Series.Add(HelpMarkerSeries);
            }
            //MessageBox.Show("12");
            
            try{
                //HelpMarkerSeries = MChart.Series[MChart.Series.Count - 1];
                if (hitrslt.Series!= MChart.Series[MChart.Series.Count - 1])
                {
                    HelpMarkerSeries.Points.AddXY(hitrslt.Series.Points[hitrslt.PointIndex].XValue, hitrslt.Series.Points[hitrslt.PointIndex].YValues[0]);
                    //MessageBox.Show(hitrslt.Series.Points[hitrslt.PointIndex].XValue.ToString());
                    HelpMarkerSeries.Points[HelpMarkerSeries.Points.Count - 1].MarkerStyle =(MarkerStyle) ChartMarkerStyleCbx.SelectedIndex;
                    HelpMarkerSeries.Points[HelpMarkerSeries.Points.Count - 1].MarkerSize = hitrslt.Series.Points[hitrslt.PointIndex].MarkerSize * 3;
                    int kakser=MChart.Series.IndexOf(hitrslt.Series);
                    //color empty
                    if (hitrslt.Series.Color.IsEmpty)
                    {
                        ChartColorPalette cc = hitrslt.Series.Palette == ChartColorPalette.None ? ChartColorPalette.BrightPastel : hitrslt.Series.Palette;
                        HelpMarkerSeries.Points[HelpMarkerSeries.Points.Count - 1].MarkerColor = ColorProcess.GetChartColorPaletteColors(cc)[kakser];
                    }
                    else
                    {
                        HelpMarkerSeries.Points[HelpMarkerSeries.Points.Count - 1].MarkerColor = hitrslt.Series.Color;
                    }
                        MessageBox.Show("Series: "+kakser.ToString()+" Marked, Point index: "+ hitrslt.PointIndex);

                }
                else
                {
                    HelpMarkerSeries.Points.Remove(hitrslt.Series.Points[hitrslt.PointIndex]);
                    if (HelpMarkerSeries.Points.Count == 0)
                    {
                        MChart.Series.Remove(HelpMarkerSeries);
                        HelpMarkerSeries = null;
                    }
                }

            }
            catch
            {

            }
            
            


            /*
            try
            {
                if (hitrslt.Series.Points[hitrslt.PointIndex].MarkerStyle != MarkerStyle.Triangle)
                { 
                    hitrslt.Series.Points[hitrslt.PointIndex].MarkerStyle = MarkerStyle.Triangle;
                    hitrslt.Series.Points[hitrslt.PointIndex].MarkerSize = hitrslt.Series.Points[hitrslt.PointIndex].MarkerSize*2;
                    hitrslt.Series.Points[hitrslt.PointIndex].mar
                }
                else
                {
                    hitrslt.Series.Points[hitrslt.PointIndex].MarkerStyle = MarkerStyle.Circle;
                    hitrslt.Series.Points[hitrslt.PointIndex].MarkerSize = hitrslt.Series.Points[hitrslt.PointIndex].MarkerSize / 2;
                }
                    
            }
            catch
            {

            }*/
            /*
            MChart.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
            MChart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;
            */


        }

        private void SaveIMG_Click(object sender, EventArgs e)
        {
            
            SVIMGDL.ShowDialog();
            //MessageBox.Show(SVIMGDL.FilterIndex.ToString());
            if (SVIMGDL.FileName != "") {
                if (SVIMGDL.FilterIndex == 1)
                {
                    //MessageBox.Show("sv");
                    using (MemoryStream ms = new MemoryStream())
                    {
                        //MessageBox.Show("ms");
                        MChart.SaveImage(ms, ChartImageFormat.Tiff);
                        using (Bitmap img = new Bitmap(ms))
                        {
                            Bitmap b = new Bitmap(MChart.Width, MChart.Height/2);
                            //MessageBox.Show("ld img");
                            using (Graphics g = Graphics.FromImage(b))
                            {
                                var dest = new Rectangle(0, 0, b.Width , b.Height );
                                var crop = new Rectangle(0, 0, b.Width, b.Height);
                                // var crop = new Rectangle(0, 0, MChart.Width, MChart.Height);
                                
                                g.DrawImage(img,dest,crop , GraphicsUnit.Pixel);

                                // MessageBox.Show(img.Width.ToString() + " " + img.Height.ToString());
                                b.Save(SVIMGDL.FileName, System.Drawing.Imaging.ImageFormat.Tiff);
                            }

                          
                        }



                    }
                    //MChart.SaveImage(SVIMGDL.FileName, ChartImageFormat.Tiff);
                }

                else
                {
                    //MChart.SaveImage(SVIMGDL.FileName, ChartImageFormat.EmfPlus);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        //MessageBox.Show("ms");
                        MChart.SaveImage(ms, ChartImageFormat.Tiff);
                        using (Bitmap img = new Bitmap(ms))
                        {
                            Bitmap b = new Bitmap(MChart.Width, MChart.Height / 2);
                            //MessageBox.Show("ld img");
                            using (Graphics g = Graphics.FromImage(b))
                            {
                                var dest = new Rectangle(0, 0, b.Width, b.Height);
                                var crop = new Rectangle(0, 0, b.Width, b.Height);
                                // var crop = new Rectangle(0, 0, MChart.Width, MChart.Height);

                                g.DrawImage(img, dest, crop, GraphicsUnit.Pixel);

                                // MessageBox.Show(img.Width.ToString() + " " + img.Height.ToString());
                                b.Save(SVIMGDL.FileName, System.Drawing.Imaging.ImageFormat.Emf);
                            }


                        }



                    }


                }

            }
            else MessageBox.Show("Error Image Path!");
        }

        private void Lbtog_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(MChart.Series.Count.ToString());

            //MChart.ResetAutoValues();
            //MChart.DataBind();

        }

        private void ChartDrawer_Load(object sender, EventArgs e)
        {
            //this.ControlBox = false;
           
        }
        public string SwitchControllerTagReferenceSample= " #VALX,#VALY";
        private void SwitchController()
        {
            int labelctrlcnt;
            if (HelpMarkerSeries != null) labelctrlcnt = MChart.Series.Count - 1;
            else labelctrlcnt = MChart.Series.Count;


            for (int i = 0; i < labelctrlcnt; i++)
            {
                if (NameLabelSwitch.Checked)
                {
                    if (ValueLabelSwitch.Checked)
                    {
                        MChart.Series[i].Points[0].Label = MChart.Series[i].Name + SwitchControllerTagReferenceSample;
                        MChart.Series[i].Label = SwitchControllerTagReferenceSample;
                    }
                    else
                    {
                        MChart.Series[i].Points[0].Label = MChart.Series[i].Name;
                        MChart.Series[i].Label = "";
                    }
                }
                else
                {
                    if (ValueLabelSwitch.Checked)
                    {
                        MChart.Series[i].Points[0].Label = SwitchControllerTagReferenceSample;
                        MChart.Series[i].Label = SwitchControllerTagReferenceSample;
                    }
                    else
                    {
                        MChart.Series[i].Points[0].Label = "";
                        MChart.Series[i].Label = "";
                    }


                }
            }

        }
        private void LabelChk_CheckedChanged(object sender, EventArgs e)
        {
            SwitchController();
        }

        private void CHKName_CheckedChanged(object sender, EventArgs e)
        {
            SwitchController();
        }



        //ctrl mouse zoom

        private const float CZoomScale = 1.1f;
        private int FZoomLevel = 0;

        private void Chart1_MouseWheel(object sender, MouseEventArgs e)
        {
            
            try
            {
                Axis xAxis = MChart.ChartAreas[0].AxisY; ;//MChart.ChartAreas[0].AxisX; ;// = MChart.ChartAreas[0].AxisY;
                /*
                //MessageBox.Show(ctrlzx.ToString()+ ctrlzy.ToString());
                if (ctrlzx == true)
                {
                     xAxis = MChart.ChartAreas[0].AxisX;
                }
                else if (ctrlzy== true)
                {
                    xAxis = MChart.ChartAreas[0].AxisY;
                }
                else
                {
                    return;
                }
                */
                double xMin = xAxis.ScaleView.ViewMinimum;
                double xMax = xAxis.ScaleView.ViewMaximum;
                double xPixelPos = xAxis.PixelPositionToValue(e.Location.X);

                if (e.Delta < 0 && FZoomLevel > 0)
                {
                    // Scrolled down, meaning zoom out
                    if (--FZoomLevel <= 0)
                    {
                        FZoomLevel = 0;
                        xAxis.ScaleView.ZoomReset();
                    }
                    else
                    {
                        double xStartPos = Math.Max(xPixelPos - (xPixelPos - xMin) * CZoomScale, 0);
                        double xEndPos = Math.Min(xStartPos + (xMax - xMin) * CZoomScale, xAxis.Maximum);
                        xAxis.ScaleView.Zoom(xStartPos, xEndPos);
                    }
                }
                else if (e.Delta > 0)
                {
                    // Scrolled up, meaning zoom in
                    double xStartPos = Math.Max(xPixelPos - (xPixelPos - xMin) / CZoomScale, 0);
                    double xEndPos = Math.Min(xStartPos + (xMax - xMin) / CZoomScale, xAxis.Maximum);
                    xAxis.ScaleView.Zoom(xStartPos, xEndPos);
                    FZoomLevel++;
                }
            }
            catch { }
            //MChart.ChartAreas[0].AxisY.IsStartedFromZero = false;
           // MChart.ChartAreas[0].RecalculateAxesScale();
        }

        private void CopyConsequencePropertiesToClickBoardBtn_Click(object sender, EventArgs e)
        {
            string cltxt="";
            
            if(ConsequenceList.SelectedIndices.Count!=0)
            {
                foreach(int idx in ConsequenceList.SelectedIndices)
                {
                    for (int t = 0; t < ConsequenceList.Items[idx].SubItems.Count; t++)
                    {
                        cltxt += (ConsequenceList.Items[idx].SubItems[t].Text + ",");
                    }
                    cltxt += "\n";
                }
            }
            else
            {
                for (int i = 0; i < ConsequenceList.Columns.Count; i++)
                {
                    cltxt += (ConsequenceList.Columns[i].Text + ",");
                }
                cltxt += "\n";
                for (int q = 0; q < ConsequenceList.Items.Count; q++)
                {
                    // cltxt+=ConsequenceList.Items[q].Text+",";
                    for (int t = 0; t < ConsequenceList.Items[q].SubItems.Count; t++)
                    {
                        cltxt += (ConsequenceList.Items[q].SubItems[t].Text + ",");
                    }
                    cltxt += "\n";
                }
            }
                
           
            System.Windows.Forms.Clipboard.SetText(cltxt);
        }
        PatternModify.ChartModify chartmdfy = null;
        private void PatternSetterBtn_Click(object sender, EventArgs e)
        {
            ValueLabelSwitch.Checked = false;
             if(chartmdfy==null|| chartmdfy.IsDisposed) chartmdfy = new PatternModify.ChartModify(MChart);
            chartmdfy.ChineseChkbx.Checked = true;
             chartmdfy.Show();
            

        }

        private void ChartDrawer_FormClosing(object sender, FormClosingEventArgs e)
        {
           if(chartmdfy!=null) chartmdfy.Close();
            
        }

        private void DrawerGroupBox_Enter(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void SimpleCalculateBtn_Click(object sender, EventArgs e)
        {

        }

        private void MarkBtn_Click(object sender, EventArgs e)
        {
            if (MarkerFunctionTxb.Text.Count() == 0) return;
             string[] ss=MarkerFunctionTxb.Text.Split(',');
            int sid;
            int pid;
            bool sisint = int.TryParse(ss[0],out sid);
                bool pisint = int.TryParse(ss[1],out pid);
            if (!(sisint && pisint)) return;
            //init helpMarkerseries
            if (HelpMarkerSeries == null)
            {
                HelpMarkerSeries = new Series();
                HelpMarkerSeries.ChartType = SeriesChartType.Point;
                HelpMarkerSeries.IsVisibleInLegend = false;
                MChart.Series.Add(HelpMarkerSeries);
            }
            try
            {
                if (MChart.Series[sid] != MChart.Series[MChart.Series.Count - 1])
                {
                    HelpMarkerSeries.Points.AddXY(MChart.Series[sid].Points[pid].XValue, MChart.Series[sid].Points[pid].YValues[0]);
                    HelpMarkerSeries.Points[HelpMarkerSeries.Points.Count - 1].MarkerStyle = (MarkerStyle)ChartMarkerStyleCbx.SelectedIndex;
                    HelpMarkerSeries.Points[HelpMarkerSeries.Points.Count - 1].MarkerSize = MChart.Series[sid].Points[pid].MarkerSize * 3;
                    int kakser = sid;
                    //color empty
                    if (MChart.Series[sid].Color.IsEmpty)
                    {
                        ChartColorPalette cc = MChart.Series[sid].Palette == ChartColorPalette.None ? ChartColorPalette.BrightPastel : MChart.Series[sid].Palette;
                        HelpMarkerSeries.Points[HelpMarkerSeries.Points.Count - 1].MarkerColor = ColorProcess.GetChartColorPaletteColors(cc)[kakser];
                    }
                    else
                    {
                        HelpMarkerSeries.Points[HelpMarkerSeries.Points.Count - 1].MarkerColor = MChart.Series[sid].Color;
                    }
                    MessageBox.Show("Series: " + kakser.ToString() + " Marked, Point index: " + pid);
                }
                else
                {
                    HelpMarkerSeries.Points.Remove(MChart.Series[sid].Points[pid]);
                    if (HelpMarkerSeries.Points.Count == 0)
                    {
                        MChart.Series.Remove(HelpMarkerSeries);
                        HelpMarkerSeries = null;
                    }
                }



            }
            catch
            {

            }
        }
    }
}
