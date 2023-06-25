using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DengueGoogleMap
{
    public partial class ControlEpidemicPlayer : Form
    {
        public ControlEpidemicPlayer(GMap.NET.WindowsForms.GMapControl gmpctrl, Dictionary<int, List<int>> bkidfinder)
        {
            //gmap controller
            gmapcontroller=gmpctrl;
            //backward id
            BackIdFinder = bkidfinder;
            processIDAndFidTable();
            InitializeComponent();
            // initial to map
            MapInital();
            ModeChange();
            //common tasks
            

        }
        
        ~ControlEpidemicPlayer()
        {
            gmapcontroller.Overlays[getOverlaysTypes("Marker")].Polygons.Clear();
        }
        public Dictionary<int, List<int>> BackIdFinder = null;


        //id fid table

        private Dictionary<int, int> IDXTABLE= new Dictionary<int, int>();
        private Dictionary<int, int> FIDTABLE = new Dictionary<int, int>();
        private void processIDAndFidTable()
        {
            for(int u = 0; u < gmapcontroller.Overlays[getOverlaysTypes("Marker")].Markers.Count; u++)
            {
                string fidtext = gmapcontroller.Overlays[getOverlaysTypes("Marker")].Markers[u].ToolTipText.Split()[0].Substring(4);
                int cfid = int.Parse(fidtext);
                IDXTABLE[u] = cfid;
                FIDTABLE[cfid] = u;


            }

        }



        private List<bool> OvrVisbleSveList = new List<bool>();
        private void MapInital()
        {
            int pos_polygon = 0, pos_marker = 0, pos_route = 0;
            for (int i = 0; i < gmapcontroller.Overlays.Count; i++)
            {
                if ("Marker" == gmapcontroller.Overlays[i].Id) pos_marker = i;
                else if ("Polygon" == gmapcontroller.Overlays[i].Id) pos_polygon = i;
                else if ("Route" == gmapcontroller.Overlays[i].Id) pos_route = i;
                else MessageBox.Show("Error Type");
            }
            OvrVisbleSveList.Add(gmapcontroller.Overlays[pos_polygon].IsVisibile);
            OvrVisbleSveList.Add(gmapcontroller.Overlays[pos_marker].IsVisibile);
            OvrVisbleSveList.Add(gmapcontroller.Overlays[pos_route].IsVisibile);

            gmapcontroller.Overlays[pos_polygon].IsVisibile = false;
            gmapcontroller.Overlays[pos_marker].IsVisibile = false;
            gmapcontroller.Overlays[pos_route].IsVisibile = false;

        }


        protected GMap.NET.WindowsForms.GMapControl gmapcontroller=null;
        private void ControlEpidemicPlayer_Load(object sender, EventArgs e)
        {
           
           
        }
        private void ModeChange()
        {
            if (ModeComboBox.Text == "Date Time Mode")
            {
                List<DateTime> analysismarkerslist = AnalysisMarkerDates();
                if (analysismarkerslist.Count != 0)
                {
                    SCRLItemLabel.Text = analysismarkerslist[0].ToString("yyyy/MM/dd");
                    //SCRMItemLabel.Text = analysismarkerslist[((int)analysismarkerslist.Count / 2)].ToString();
                    SCRMItemLabel.Text = (analysismarkerslist[0] .AddDays( ((analysismarkerslist[analysismarkerslist.Count - 1] - analysismarkerslist[0]).Days/2))).ToString("yy/MM/dd");
                    SCRRItemLabel.Text = analysismarkerslist[analysismarkerslist.Count-1].ToString("yyyy/MM/dd");
                    SelectingScrollBar.Minimum = 0;
                    SelectingScrollBar.Maximum = (analysismarkerslist[analysismarkerslist.Count - 1] - analysismarkerslist[0]).Days;
                   

                };
            }
            else if (ModeComboBox.Text == "Cases Number Mode")
            {
                List<int> analysismarkerslist = AnalysisMarkerNumber();
                if (analysismarkerslist.Count != 0)
                {
                    SCRLItemLabel.Text = analysismarkerslist[0].ToString("yyyy/MM/dd");
                    SCRMItemLabel.Text = analysismarkerslist[((int)analysismarkerslist.Count / 2)].ToString("yyy/MM/dd");
                    SCRRItemLabel.Text = analysismarkerslist[analysismarkerslist.Count-1].ToString("yyyy/MM/dd");
                    SelectingScrollBar.Minimum = analysismarkerslist[0];
                    SelectingScrollBar.Maximum = analysismarkerslist[analysismarkerslist.Count - 1];

                };
            }
            else
            {

            }
        }

       




        private List<int> AnalysisMarkerNumber()
        {
            List<int> anatime = new List<int>();
            int pos_polygon = 0, pos_marker = 0, pos_route = 0;
            for (int i = 0; i < gmapcontroller.Overlays.Count; i++)
            {
                if ("Marker" == gmapcontroller.Overlays[i].Id) pos_marker = i;
                else if ("Polygon" == gmapcontroller.Overlays[i].Id) pos_polygon = i;
                else if ("Route" == gmapcontroller.Overlays[i].Id) pos_route = i;
                else MessageBox.Show("Error Type");
            }
            //gmpctrl;

            for (int i = 0; i < gmapcontroller.Overlays[pos_marker].Markers.Count; i++)
            {
               
                anatime.Add(i);

            }


            return anatime;
        }



        private List<DateTime> AnalysisMarkerDates()
        {
            List<DateTime> anatime = new List<DateTime>();
            int pos_polygon = 0, pos_marker = 0, pos_route = 0;
            for (int i = 0; i < gmapcontroller.Overlays.Count; i++)
            {
                if ("Marker" == gmapcontroller.Overlays[i].Id) pos_marker = i;
                else if ("Polygon" == gmapcontroller.Overlays[i].Id) pos_polygon = i;
                else if ("Route" == gmapcontroller.Overlays[i].Id) pos_route = i;
                else MessageBox.Show("Error Type");
            }
            //gmpctrl;
            
            for (int i = 0; i < gmapcontroller.Overlays[pos_marker].Markers.Count; i++)
            {
                /*
                GMapMarker gmk   =gmapcontroller.Overlays[pos_marker].Markers[i];
                string tttxt= gmk.ToolTipText;
                string lndate=tttxt.Split()[1];
                lndate= lndate.Replace(':', ' ');
                lndate = lndate.Split()[1];
                DateTime dt=new DateTime();
                DateTime.TryParse(lndate, out dt);
                //MessageBox.Show(dt.ToString());*/

                anatime.Add(parseDateTimeByMarkerToolTipText(gmapcontroller.Overlays[pos_marker].Markers[i]));

            }


            return anatime;
        }


        private DateTime parseDateTimeByMarkerToolTipText(GMapMarker gmk)
        {
            //GMapMarker gmk = gmapcontroller.Overlays[pos_marker].Markers[i];
            DateTime dt = new DateTime();

            {
                string tttxt = gmk.ToolTipText;
                string lndate = tttxt.Split()[1];
                lndate = lndate.Replace(':', ' ');
                lndate = lndate.Split()[1];

                DateTime.TryParse(lndate, out dt);
            }
            

            return dt;

        }
        private List<DateTime> parseStartEndPeriodByMarkerToolTipText(GMapMarker gmk)
        {
            List< DateTime > rt= new List<DateTime>();
           // get oridate
           DateTime oridt = new DateTime();
            {
                string tttxt = gmk.ToolTipText;
                string lndate = tttxt.Split()[1];
                lndate = lndate.Replace(':', ' ');
                lndate = lndate.Split()[1];

                DateTime.TryParse(lndate, out oridt);
            }
            //get str
            {
                string tttxt = gmk.ToolTipText;
                string strday = tttxt.Split()[6];
                strday = strday.Replace(':', ' ');
                strday = strday.Split()[1];
                double strd= double.Parse(strday);
                rt.Add(oridt.AddDays(strd));

            }


            //get end
            {
                string tttxt = gmk.ToolTipText;
                string endday = tttxt.Split()[7];
                endday = endday.Replace(':', ' ');
                endday = endday.Split()[1];
                double endd=double.Parse(endday);
                rt.Add(oridt.AddDays(endd));

            }




            return rt;
        }

        bool comparePeriod(DateTime astr,DateTime aend,DateTime bstr,DateTime bend)
        {
            bool isperiod = false;

            long as_= astr.ToFileTime();
            long ae_ = aend.ToFileTime();
            long bs_ = bstr.ToFileTime();
            long be_ = bend.ToFileTime();

            if ((as_ >= bs_ &  as_<=be_)||(ae_>=bs_ & ae_<=be_) )
            {
                isperiod = true;
            }


            return isperiod;
        }


        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void ModeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ModeChange();
        }

       private List<DateTime> __Analysismarkerslist__ = null;// for this func
        private void SelectingScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            SelectingScrollValueTextBox.Text = SelectingScrollBar.Value.ToString();
            // change selecting buffers
            int mdrg=0;
           if( __Analysismarkerslist__ ==null) __Analysismarkerslist__= AnalysisMarkerDates();
            
            if (ModeComboBox.Text == "Date Time Mode")
            {
                mdrg = int.Parse( DateTimeModeRangeDays.Text);
                SelectingScrollValueTextBox.Text += ("," + __Analysismarkerslist__[0].AddDays(SelectingScrollBar.Value).ToString("yyyy/MM/dd"));
            }
            else if (ModeComboBox.Text == "Cases Number Mode")
            {
                mdrg = int.Parse(CaseNumberModeRange.Text);
            }
            else
            {

            }

           
           

            //redraw
            List<int> selectingMarkerIDList = GetIndexArrayByScrollValueRange_Marker(SelectingScrollBar.Value, mdrg, ModeComboBox.Text);
            List<int> selectingPolygonIDList = GetIndexArrayByScrollValueRange_Polygon(SelectingScrollBar.Value, mdrg, ModeComboBox.Text);
            if (PtEqPoly.Checked == true)
            {
                drawBufferBysSelectedIdList(selectingMarkerIDList, selectingMarkerIDList, !NoShowPointChk.Checked);
                fixBackwardBuffer(selectingMarkerIDList, ScrollRangeGapIndex);
            }
            else drawBufferBysSelectedIdList(selectingMarkerIDList, selectingPolygonIDList, !NoShowPointChk.Checked);
            
            //is moving
            int pos_polygon = 0, pos_marker = 0, pos_route = 0;
            for (int i = 0; i < gmapcontroller.Overlays.Count; i++)
            {
                if ("Marker" == gmapcontroller.Overlays[i].Id) pos_marker = i;
                else if ("Polygon" == gmapcontroller.Overlays[i].Id) pos_polygon = i;
                else if ("Route" == gmapcontroller.Overlays[i].Id) pos_route = i;
                else MessageBox.Show("Error Type");
            }
            if (AutoMovingWhenScrollingChk.Checked)
            {
                gmapcontroller.Position = gmapcontroller.Overlays[pos_marker].Markers[selectingMarkerIDList[0]].Position;
            }
            //calculate accuracy of prediction
            if (RecordPredictiveRateChk.Checked)
            {
                //PredictiveRaterichTextBox.Text += (calculateAccuracyRate(selectingIDList, BackIdFinder) +"\n");
                caculateRollingInformation calrinf = concludeRollingInformation(selectingMarkerIDList, BackIdFinder);

                float AC= calrinf.accuracy();
                int allpoints= calrinf.allpoints, hitpoints= calrinf.hitpoints;
                double LGUB= calrinf.getUpperBoundAverageLag(),
                    LGLB= calrinf.getLoweerBoundAverageLag(),
                    DSTU= calrinf.getUpperBoundAverageDistance(),
                    DENS= calrinf.getAverageDensity()*1000000;
                //date
                DateTime strtdate=DateTime.Parse(SCRLItemLabel.Text);
                strtdate=strtdate.AddDays(int.Parse(SelectingScrollValueTextBox.Text));
                rollingresultform.DateTbx.Text = strtdate.ToShortDateString();
                rollingresultform.PropotionTbx.Text = Math.Round(AC*100,2).ToString()+"% ="+ hitpoints.ToString() + "/" + allpoints.ToString()  ;
                rollingresultform.LagLowerBoundTbx.Text = Math.Round(LGLB,1).ToString();
                rollingresultform.LagUpperBoundTbx.Text = Math.Round(LGUB,1).ToString();
                rollingresultform.DistanceUpperBoundTbx.Text = Math.Round(DSTU,1).ToString();
                rollingresultform.DensityTbx.Text = DENS.ToString("e3");

                PredictiveRaterichTextBox.AppendText(
                    "Date:," + strtdate.ToString()+","
                    + "AC:,"+hitpoints.ToString() + "/" + allpoints.ToString()+","+AC.ToString()+","
                    +"LGUB:,"+ LGUB.ToString() +","
                    +"LGLB:,"+ LGLB.ToString() +","
                    +"DSTU:,"+ DSTU.ToString()+","
                    +"DENS:," + DENS.ToString() + ","
                    );
                PredictiveRaterichTextBox.ScrollToCaret();
                PredictiveRaterichTextBox.AppendText("\n");
            }
        }

        class caculateRollingInformation
        {
           
            public int hitpoints;
            public int allpoints;
            public float accuracy()
            {
                if (allpoints == 0) return -1;
                return (float)hitpoints / (float)allpoints;
            }
            public List<double> LagUpperBoundSet = new List<double>(), LagLowerBoundSet = new List<double>(), DistanceUpperBoundSet=new List<double>(); 
            public double getUpperBoundAverageLag()
            {
                if (LagUpperBoundSet.Count == 0) return 0;
                double rt=0;
                foreach(double r in LagUpperBoundSet)
                {
                    rt += r;
                }
                rt /= LagUpperBoundSet.Count;
                return rt;
            }
            public double getLoweerBoundAverageLag()
            {
                if (LagLowerBoundSet.Count == 0) return 0;
                double rt = 0;
                foreach (double r in LagLowerBoundSet)
                {
                    rt += r;
                }
                rt /= LagLowerBoundSet.Count;
                return rt;
            }
            public double getUpperBoundAverageDistance()
            {
                if (DistanceUpperBoundSet.Count == 0) return 0;
                double rt = 0;
                foreach (double r in DistanceUpperBoundSet)
                {
                    rt += r;
                }
                rt /= DistanceUpperBoundSet.Count;
                return rt;

            }
            public double getAverageDensity()
            {
                if (DistanceUpperBoundSet.Count == 0) return 0;
                double rt = 0;
                double cul = 0;
                for(int i=0; i < DistanceUpperBoundSet.Count; i++)
                {
                 cul+=     ((DistanceUpperBoundSet[i] + 1) * (DistanceUpperBoundSet[i] + 1) * Math.PI * (LagUpperBoundSet[i] - LagLowerBoundSet[i] + 1));
                }
                //cul /= DistanceUpperBoundSet.Count;
                rt=hitpoints / cul;
                return rt;
            }

        }
        private caculateRollingInformation concludeRollingInformation(List<int>  selidlst, Dictionary<int , List<int> > bkiddic)
        {
            //string owr="";
            caculateRollingInformation calrinf = new caculateRollingInformation();
            //List<int> idpool=new List<int>();
            int cnthit = 0;
            for (int i=0; i < selidlst.Count; i++)
            {
                


                //gmapcontroller.Overlays[pos_marker].Markers[gidx].ToolTipText.Split()[0].Substring(4);
                string fidtext =gmapcontroller.Overlays[getOverlaysTypes("Marker")].Markers[selidlst[i]].ToolTipText.Split()[0].Substring(4);
                int cfid = int.Parse(fidtext);
                if (bkiddic.ContainsKey(cfid))
                {
                    if(bkiddic[cfid].Count!=0) cnthit++;
                    //idpool.AddRange(bkiddic[cfid]);
                }
                //lowerlagbnd
                string llgtxt = (gmapcontroller.Overlays[getOverlaysTypes("Marker")].Markers[selidlst[i]].ToolTipText.Split()[6].Replace(':', ' ').Split()[1]);
                double llg     =double.Parse(llgtxt);
                //upperlagbnd
                string ulgtxt = (gmapcontroller.Overlays[getOverlaysTypes("Marker")].Markers[selidlst[i]].ToolTipText.Split()[7].Replace(':', ' ').Split()[1]);
                double ulg = double.Parse(ulgtxt);
                //upperdist
                string udsttxt = (gmapcontroller.Overlays[getOverlaysTypes("Marker")].Markers[selidlst[i]].ToolTipText.Split()[8].Replace(':', ' ').Split()[1]);
                double udst = double.Parse(udsttxt);
                calrinf.LagLowerBoundSet.Add(llg);
                calrinf.LagUpperBoundSet.Add(ulg);
                calrinf.DistanceUpperBoundSet.Add(udst);

            }
            // int cnthit = idpool.Distinct().ToList().Count;
            int allpoint = selidlst.Count;

           
            calrinf.hitpoints = cnthit;
            calrinf.allpoints = allpoint;



            //string tt = SelectingScrollValueTextBox.Text;
            //owr = tt + "," + cnthit.ToString() + "/" + allpoint.ToString()+" , " +((float)cnthit/(float)allpoint   ).ToString() ;
            //return owr;
            return calrinf;
        }

        private  int getOverlaysTypes(string type)
        {
            int pos_polygon = 0, pos_marker = 0, pos_route = 0;
            for (int i = 0; i < gmapcontroller.Overlays.Count; i++)
            {
                if ("Marker" == gmapcontroller.Overlays[i].Id) pos_marker = i;
                else if ("Polygon" == gmapcontroller.Overlays[i].Id) pos_polygon = i;
                else if ("Route" == gmapcontroller.Overlays[i].Id) pos_route = i;
                else MessageBox.Show("Error Type");
            }
            int rtstr;
            if (type == "Marker") rtstr = pos_marker;
            else if (type == "Polygon") rtstr = pos_polygon;
            else if (type == "Route") rtstr = pos_route;
            else rtstr = -1;

            return rtstr;
        }


        private void  fixBackwardBuffer(List<int> selitlist,int gap__)
        {
            int pos_polygon = 0, pos_marker = 0, pos_route = 0;
            for (int i = 0; i < gmapcontroller.Overlays.Count; i++)
            {
                if ("Marker" == gmapcontroller.Overlays[i].Id) pos_marker = i;
                else if ("Polygon" == gmapcontroller.Overlays[i].Id) pos_polygon = i;
                else if ("Route" == gmapcontroller.Overlays[i].Id) pos_route = i;
                else MessageBox.Show("Error Type");
            }
            for(int st=0 ; st< gap__; st++)
            {

                if (TransparentBackwardBufferChk.Checked) gmapcontroller.Overlays[pos_polygon].Polygons[st].Fill = Brushes.Transparent;
                else gmapcontroller.Overlays[pos_polygon].Polygons[st].Fill = CreatedMovingBrushesPolygonBackward;
                if (NoShowBackwardPointChk.Checked) gmapcontroller.Overlays[pos_marker].Markers[st].IsVisible = false;
            }
           

        }

        private int ScrollRangeGapIndex=-1;
        protected List<int> GetIndexArrayByScrollValueRange_Marker(int vlu, int rng,string cbxmdtext)
        {
            int pos_polygon = 0, pos_marker = 0, pos_route = 0;
            for (int i = 0; i < gmapcontroller.Overlays.Count; i++)
            {
                if ("Marker" == gmapcontroller.Overlays[i].Id) pos_marker = i;
                else if ("Polygon" == gmapcontroller.Overlays[i].Id) pos_polygon = i;
                else if ("Route" == gmapcontroller.Overlays[i].Id) pos_route = i;
                else MessageBox.Show("Error Type");
            }


            List<int> arset=new List<int>();
            for(int k=0;k<gmapcontroller.Overlays[pos_marker].Markers.Count; k++)
            {



                if (cbxmdtext == "Date Time Mode")
                {
                    // set base
                    DateTime bsedateTime = parseDateTimeByMarkerToolTipText(gmapcontroller.Overlays[pos_marker].Markers[0]);
                    DateTime dattm=parseDateTimeByMarkerToolTipText(gmapcontroller.Overlays[pos_marker].Markers[k]);
                   // MessageBox.Show((dattm - bsedateTime).Days.ToString());
                    if (( dattm- bsedateTime).Days < vlu-rng)
                    {
                        continue;
                    }
                    else if((dattm-bsedateTime).Days>( vlu + rng))
                    {
                        break;
                    }
                    else
                    {
                        if (((dattm - bsedateTime).Days < vlu)) ScrollRangeGapIndex = k;
                        arset.Add(k);
              
                    }



                }
                else if (cbxmdtext == "Cases Number Mode")
                {
                    if (k < vlu-rng)
                    {
                        continue;
                    }
                    else if(k> vlu+rng)
                    {
                        break;
                    }
                    else
                    {
                        if ( (vlu ) <k) ScrollRangeGapIndex = k;
                        arset.Add(k);
                    }
                    
                }
                else
                {
                    // nothing
                    MessageBox.Show("err");
                }

            }
               


                return arset;
        }
        protected List<int> GetIndexArrayByScrollValueRange_Polygon(int vlu, int rng, string cbxmdtext)
        {
            int pos_polygon = 0, pos_marker = 0, pos_route = 0;
            for (int i = 0; i < gmapcontroller.Overlays.Count; i++)
            {
                if ("Marker" == gmapcontroller.Overlays[i].Id) pos_marker = i;
                else if ("Polygon" == gmapcontroller.Overlays[i].Id) pos_polygon = i;
                else if ("Route" == gmapcontroller.Overlays[i].Id) pos_route = i;
                else MessageBox.Show("Error Type");
            }


            List<int> arset = new List<int>();
            for (int k = 0; k < gmapcontroller.Overlays[pos_marker].Markers.Count; k++)
            {



                if (cbxmdtext == "Date Time Mode")
                {
                    // set base
                    DateTime bsedateTime = parseDateTimeByMarkerToolTipText(gmapcontroller.Overlays[pos_marker].Markers[0]);
                    DateTime dattm = parseDateTimeByMarkerToolTipText(gmapcontroller.Overlays[pos_marker].Markers[k]);
                    List<DateTime> riskbuffer = parseStartEndPeriodByMarkerToolTipText(gmapcontroller.Overlays[pos_marker].Markers[k]);
                    // MessageBox.Show((dattm - bsedateTime).Days.ToString());
                    /* if ((dattm - bsedateTime).Days < vlu - rng)
                    {
                        continue;
                    }
                    else if ((dattm - bsedateTime).Days > (vlu + rng))
                    {
                        break;
                    }
                    else
                    {
                        //if (((dattm - bsedateTime).Days < vlu)) ScrollRangeGapIndex = k;
                        arset.Add(k);

                    }*/
                    if (comparePeriod(bsedateTime.AddDays(vlu - rng), bsedateTime.AddDays(vlu + rng), riskbuffer[0], riskbuffer[1]))
                    {
                        arset.Add(k);
                    }



                }
                else if (cbxmdtext == "Cases Number Mode")
                {
                    if (k < vlu - rng)
                    {
                        continue;
                    }
                    else if (k > vlu + rng)
                    {
                        break;
                    }
                    else
                    {
                        //if ((vlu) < k) ScrollRangeGapIndex = k;
                        arset.Add(k);
                    }

                }
                else
                {
                    // nothing
                    MessageBox.Show("err");
                }

            }



            return arset;
        }








        private void ControlEpidemicPlayer_FormClosing(object sender, FormClosingEventArgs e)
        {
            int pos_polygon = 0, pos_marker = 0, pos_route = 0;
            for (int i = 0; i < gmapcontroller.Overlays.Count; i++)
            {
                if ("Marker" == gmapcontroller.Overlays[i].Id) pos_marker = i;
                else if ("Polygon" == gmapcontroller.Overlays[i].Id) pos_polygon = i;
                else if ("Route" == gmapcontroller.Overlays[i].Id) pos_route = i;
                else MessageBox.Show("Error Type");
            }
            /*
            OvrVisbleSveList.Add(gmapcontroller.Overlays[pos_polygon].IsVisibile);
            OvrVisbleSveList.Add(gmapcontroller.Overlays[pos_marker].IsVisibile);
            OvrVisbleSveList.Add(gmapcontroller.Overlays[pos_route].IsVisibile);*/
            ClearBufferBysSelectedIdList();
            recoverTransparentBufferStroke();
            gmapcontroller.Overlays[pos_polygon].IsVisibile = OvrVisbleSveList[0];
            gmapcontroller.Overlays[pos_marker].IsVisibile = OvrVisbleSveList[1]; 
            gmapcontroller.Overlays[pos_route].IsVisibile = OvrVisbleSveList[2];
            //close rolling result form
            if (rollingresultform != null)
            {
                rollingresultform.Close();
                rollingresultform = null;
            }
        }
        private Brush SavedBrushesPolygon=null;
        private Brush CreatedMovingBrushesPolygonForward_Effect = /*Brushes.Red; */ new SolidBrush(Color.FromArgb(64, 255, 0, 0));
        private Brush CreatedMovingBrushesPolygonForward_NonEffect = /*Brushes.Yellow;*/ new SolidBrush(Color.FromArgb(128, 255, 255, 0));
        private Brush CreatedMovingBrushesPolygonBackward = new SolidBrush(Color.FromArgb(255, 255, 255, 0));
        protected void drawBufferBysSelectedIdList(List<int> MarkerIDList,List<int> PolygonIdList,bool isShowPoints=true)
        {
            int pos_polygon = 0, pos_marker = 0, pos_route = 0;
            for (int i = 0; i < gmapcontroller.Overlays.Count; i++)
            {
                if ("Marker" == gmapcontroller.Overlays[i].Id) pos_marker = i;
                else if ("Polygon" == gmapcontroller.Overlays[i].Id) pos_polygon = i;
                else if ("Route" == gmapcontroller.Overlays[i].Id) pos_route = i;
                else MessageBox.Show("Error Type");
            }
            gmapcontroller.Overlays[pos_marker].IsVisibile = true;
            gmapcontroller.Overlays[pos_polygon].IsVisibile = true;



            //check effect polygon

            List<int> EffectBufferPolygons = new List<int>();//store fid
            foreach(int idx in MarkerIDList)
            {
                
                EffectBufferPolygons.AddRange(BackIdFinder[IDXTABLE[idx]]);//store fid
            }


            //runner protect
            //gmapcontroller.Overlays[pos_marker].Polygons.Clear();

            for (int id=0; id< gmapcontroller.Overlays[pos_marker].Markers.Count;id++)
            {
                //marker control
                if (MarkerIDList.Contains(id))
                {
                    
                    if (isShowPoints) gmapcontroller.Overlays[pos_marker].Markers[id].IsVisible = true;
                 

              

                    //MessageBox.Show(id.ToString());
                }
                else
                {
                    gmapcontroller.Overlays[pos_marker].Markers[id].IsVisible = false;
             

                }




                //polygon control
                if (PolygonIdList.Contains(id))
                {
                    if (SavedBrushesPolygon == null) SavedBrushesPolygon = (Brush)gmapcontroller.Overlays[pos_polygon].Polygons[id].Fill.Clone();

                    gmapcontroller.Overlays[pos_polygon].Polygons[id].IsVisible = true;
                    if (TransparentForwardBufferChk.Checked){
                        gmapcontroller.Overlays[pos_polygon].Polygons[id].Fill = Brushes.Transparent;
                    }
                    else
                    {
                        if (EffectBufferPolygons.Contains(IDXTABLE[id]))
                        {
                           
                           gmapcontroller.Overlays[pos_polygon].Polygons[id].Fill = CreatedMovingBrushesPolygonForward_Effect;
                            GMapPolygon gpol =    gmapcontroller.Overlays[pos_polygon].Polygons[id];
                            
                            gmapcontroller.Overlays[pos_marker].Polygons.Add(  gmapcontroller.Overlays[pos_polygon].Polygons[id]);


                        }
                        else
                        {
                            gmapcontroller.Overlays[pos_polygon].Polygons[id].Fill = CreatedMovingBrushesPolygonForward_NonEffect;
                        }

                    }
                    


                }
                else
                {
                    
                    gmapcontroller.Overlays[pos_polygon].Polygons[id].IsVisible = false;
                    if(SavedBrushesPolygon!=null) gmapcontroller.Overlays[pos_polygon].Polygons[id].Fill = SavedBrushesPolygon;

                }
               
            }//all point run

           


        }
        protected void ClearBufferBysSelectedIdList()
        {
            int pos_polygon = 0, pos_marker = 0, pos_route = 0;
            for (int i = 0; i < gmapcontroller.Overlays.Count; i++)
            {
                if ("Marker" == gmapcontroller.Overlays[i].Id) pos_marker = i;
                else if ("Polygon" == gmapcontroller.Overlays[i].Id) pos_polygon = i;
                else if ("Route" == gmapcontroller.Overlays[i].Id) pos_route = i;
                else MessageBox.Show("Error Type");
            }
            gmapcontroller.Overlays[pos_marker].IsVisibile = true;
            gmapcontroller.Overlays[pos_polygon].IsVisibile = true;


            //MessageBox.Show(IdList.Count.ToString());
            for (int id = 0; id < gmapcontroller.Overlays[pos_marker].Markers.Count; id++)
            {
                
                if(gmapcontroller.Overlays[pos_marker].Markers[id].IsVisible==false || gmapcontroller.Overlays[pos_polygon].Polygons[id].IsVisible)
                {
                    gmapcontroller.Overlays[pos_marker].Markers[id].IsVisible = true; 
                    gmapcontroller.Overlays[pos_polygon].Polygons[id].IsVisible = true;
                }

               if(SavedBrushesPolygon!=null) gmapcontroller.Overlays[pos_polygon].Polygons[id].Fill = SavedBrushesPolygon;
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void AnimationPlayBtn_Click(object sender, EventArgs e)
        {
            AutoPlayTimer.Enabled = true;
            AnimationPlayBtn.BackColor = Color.Red;
            AutoPlayTimer.Interval = (int)(float.Parse(SecondPerFrameTbx.Text)*1000);
            AutoScrollValueTbx.Enabled = false;
            SecondPerFrameTbx.Enabled = false;
        }

        private void AnimationStopBtn_Click(object sender, EventArgs e)
        {
            
            AnimationPauseBtn_Click(null, null);
            SelectingScrollBar.Value = 0;
            SelectingScrollBar_Scroll(null, null);
        }

        private void AnimationPauseBtn_Click(object sender, EventArgs e)
        {
           
            
            AutoPlayTimer.Enabled = false;
            AnimationPlayBtn.BackColor = Color.Lime;
            AutoScrollValueTbx.Enabled = true;
            SecondPerFrameTbx.Enabled = true;


        }

        private void AutoPlayTimer_Tick(object sender, EventArgs e)
        {


            if((SelectingScrollBar.Value + int.Parse(AutoScrollValueTbx.Text))> SelectingScrollBar.Maximum)
            {
                SelectingScrollBar.Value = SelectingScrollBar.Maximum;
                AnimationStopBtn_Click(null, null);
                if (LoopPlayCkb.Checked == true)
                {
                    AnimationPlayBtn_Click(null,null);
                }
            }
             SelectingScrollBar.Value += int.Parse(AutoScrollValueTbx.Text);
            SelectingScrollBar_Scroll(null, null);


        }
        // Control Buffer stroke
        private Pen BackupStroke = null; 
        private void  transparentBufferStroke()
        {
            
            {
                int pos_polygon = 0, pos_marker = 0, pos_route = 0;
                for (int i = 0; i < gmapcontroller.Overlays.Count; i++)
                {
                    if ("Marker" == gmapcontroller.Overlays[i].Id) pos_marker = i;
                    else if ("Polygon" == gmapcontroller.Overlays[i].Id) pos_polygon = i;
                    else if ("Route" == gmapcontroller.Overlays[i].Id) pos_route = i;
                    else MessageBox.Show("Error Type");
                }

                if (BackupStroke == null) BackupStroke = (Pen)gmapcontroller.Overlays[pos_polygon].Polygons[0].Stroke.Clone();
                for (int id = 0; id < gmapcontroller.Overlays[pos_polygon].Polygons.Count; id++)
                {
                    gmapcontroller.Overlays[pos_polygon].Polygons[id].Stroke = Pens.Transparent;
                }

            }

        }
        private void recoverTransparentBufferStroke()
        {
            if ( BackupStroke!=null)
            {
                int pos_polygon = 0, pos_marker = 0, pos_route = 0;
                for (int i = 0; i < gmapcontroller.Overlays.Count; i++)
                {
                    if ("Marker" == gmapcontroller.Overlays[i].Id) pos_marker = i;
                    else if ("Polygon" == gmapcontroller.Overlays[i].Id) pos_polygon = i;
                    else if ("Route" == gmapcontroller.Overlays[i].Id) pos_route = i;
                    else MessageBox.Show("Error Type");
                }
                for (int id = 0; id < gmapcontroller.Overlays[pos_polygon].Polygons.Count; id++)
                {
                    if(BackupStroke!=null)gmapcontroller.Overlays[pos_polygon].Polygons[id].Stroke = BackupStroke;
                }


            }

        }

        private void BufferNoStrokeChk_CheckedChanged(object sender, EventArgs e)
        {
            if(BufferNoStrokeChk.Checked) transparentBufferStroke();
            else recoverTransparentBufferStroke();

        }

        RollingResultForm rollingresultform = null;
        private void RecordPredictiveRateChk_CheckedChanged(object sender, EventArgs e)
        {
            
            if (RecordPredictiveRateChk.Checked == true)
            {
                if(rollingresultform == null) rollingresultform = new RollingResultForm();
                rollingresultform.Show();
            }
            else
            {
                rollingresultform.Close();
                rollingresultform = null;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
