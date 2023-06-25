using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DenInterface
{
    public class RecorderTablePlus: RecordeTable
    {
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // TableRecorderConatiner
            // 
            this.TableRecorderConatiner.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.TableRecorderConatiner_ItemChecked_x);
            this.TableRecorderConatiner.SelectedIndexChanged += new System.EventHandler(this.TableRecorderConatiner_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("PMingLiU", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Size = new System.Drawing.Size(64, 24);
            // 
            // RecorderTablePlus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.ClientSize = new System.Drawing.Size(1074, 671);
            this.ControlBox = false;
            this.Name = "RecorderTablePlus";
            this.Load += new System.EventHandler(this.RecorderTablePlus_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        public RecorderTablePlus(string xaxisitem,string yaxisitem)
        {
            //base.InitializeComponent();
            InitializeComponent();
            XAxisItem = xaxisitem;
            YAxisItem = yaxisitem;
        }
        public string XAxisItem, YAxisItem;
        public Series CurrentSeries;
        private void RecorderTablePlus_Load(object sender, EventArgs e)
        {

        }

        private void TableRecorderConatiner_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void TableRecorderConatiner_ItemChecked_x(object sender, System.Windows.Forms.ItemCheckedEventArgs e)
        {
            //MessageBox.Show("parent");
            base.TableRecorderConatiner_ItemChecked(sender,e);
            string[] columnit  = { XAxisItem, YAxisItem };
            //List<DataPoint> ptsdata = new List<DataPoint>();
            if (CurrentSeries == null) return;
            CurrentSeries.Points.Clear();
            for (int z = 0; z < TableRecorderConatiner.Items.Count; z++)//row
            {
                if (!TableRecorderConatiner.Items[z].Checked) continue;
                bool can_draw = true;
                List<double> xylist = new List<double>();

                foreach (string cit in columnit)//col
                {
                    string r = TableRecorderConatiner.Items[z].SubItems[indexofCapital(cit)].Text;
                    if (!Microsoft.VisualBasic.Information.IsNumeric(r))
                    {
                        can_draw = false;
                    }
                    else
                    {
                        double pvv = double.Parse(r);
                        //pvv = Math.Round(pvv, 5);
                        xylist.Add(pvv);
                    }
                }

                if (can_draw)
                {
                    CurrentSeries.Points.AddXY(xylist[0], xylist[1]);


                }

            }

        }

       
    }
}
