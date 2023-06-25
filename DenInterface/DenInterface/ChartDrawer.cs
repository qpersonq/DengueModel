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
    public partial class ChartDrawer : Form
    {
        public ChartDrawer()
        {
            InitializeComponent();
            
            MChart.Legends[0].IsTextAutoFit = true;
            MChart.Legends[0].Font = new Font(FontFamily.GenericSansSerif, 14, FontStyle.Regular);
            MChart.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            MChart.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            
            MChart.ChartAreas[0].CursorX.IsUserEnabled = true;
            MChart.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            MChart.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            MChart.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;
            MChart.ChartAreas[0].AxisX.IsStartedFromZero = false;
            

            MChart.ChartAreas[0].CursorY.IsUserEnabled = true;
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

        private void MChart_Click(object sender, EventArgs e)
        {
            MChart.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.LightGray;
            MChart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.LightGray;



        }

        private void SaveIMG_Click(object sender, EventArgs e)
        {
            SVIMGDL.ShowDialog();
            if (SVIMGDL.FileName != "") MChart.SaveImage(SVIMGDL.FileName, ChartImageFormat.Tiff);
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
            
            

            for (int i = 0; i < MChart.Series.Count; i++)
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
            cltxt += "\\,";
            for(int i=0;i < ConsequenceList.Columns.Count; i++)
            {
                cltxt +=( ConsequenceList.Columns[i].Text+",");
            }
            cltxt += "\n";
            for(int q=0;q<ConsequenceList.Items.Count; q++)
            {
               // cltxt+=ConsequenceList.Items[q].Text+",";
                for(int t=0;t<ConsequenceList.Items[q].SubItems.Count; t++)
                {
                    cltxt += (ConsequenceList.Items[q].SubItems[t].Text + ",");
                }
                cltxt += "\n";
            }
           
            System.Windows.Forms.Clipboard.SetText(cltxt);
        }
    }
}
