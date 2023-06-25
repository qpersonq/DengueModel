using drawer;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DengueGoogleMap
{
    public partial class GoogleMapPresentation : Form
    {
        PointInformationProcessor pointInformationProcessor;
        public GoogleMapPresentation()
        {
            InitializeComponent();
            processMapProviders();
            GoogleMapPresentation_Load(null, null);



            this.Resize += FormResize;
            GMapDrawerSizeStorage = GMapDrawer.Size;
        }






        private Size GMapDrawerSizeStorage;
        private void FormResize(object sender, EventArgs e)
        {
            //MessageBox.Show("resize!");
            // Properties.Settings.Default.State = this.WindowState;
            // if (this.WindowState == FormWindowState.Normal) Properties.Settings.Default.Size = this.Size;+
            if (this.WindowState == FormWindowState.Normal)
            {
                GMapDrawer.Size = GMapDrawerSizeStorage;
            }
            else
            {
                GMapDrawer.Size = new Size(1680, 750);
            }
        }

        private Dictionary<string, GMapProvider> MapProviders = new Dictionary<string, GMapProvider>();
        private void processMapProviders()
        {
            
            List<GMapProvider> allgmpprvd=GMapProviders.List;
            foreach(GMapProvider gmp in allgmpprvd)
            {
                
                MapProviders[gmp.Name] = gmp;
                
            }
            /*
            MapProviders["GoogleMap"] = GMapProviders.GoogleMap;
            MapProviders["GoogleSatelliteMap"] = GMapProviders.GoogleSatelliteMap;
            MapProviders["OpenStreetMap"] = GMapProviders.OpenStreetMap;
            MapProviders["ArcGIS_World_Street_Map"] = GMapProviders.ArcGIS_World_Street_Map;
            MapProviders["ArcGIS_World_Topo_Map"] = GMapProviders.ArcGIS_World_Topo_Map;*/
            foreach (KeyValuePair<string, GMapProvider> it in MapProviders)
            {
               
                MapTypeCbx.Items.Add(it.Key);
            }

        }
        private void GoogleMapPresentation_Load(object sender, EventArgs e)
        {
            GMapDrawer.ShowCenter = false;
            GMapDrawer.DragButton = MouseButtons.Left;
            
            GMapDrawer.MapProvider = MapProviders[MapTypeCbx.Text];
            
            GMapDrawer.Position = new GMap.NET.PointLatLng(23.5, 121);

            GMapDrawer.MinZoom = 2;
            GMapDrawer.MaxZoom = 24;
            
            GMapDrawer.Zoom = int.Parse(ZoomLabelNumber.Text);
            this.MouseWheel += MouseWheelForReadZoom;
            /*
            string[] rgtxt = PointFidRange.Text.Trim().Split();
            pointInformationProcessor = new PointInformationProcessor(PointFilePath.Text, SpatialPercentageReferenceFile.Text);
            if(File.Exists(   ModelFile.Text))
            {
                pointInformationProcessor.LoadCompareTable(ModelFile.Text);
                List<double> pclst = pointInformationProcessor.getPercentageList();
                ListPercentage.Items.Clear();
                foreach (double kk in pclst) ListPercentage.Items.Add(kk);
                ListPercentage.SelectedIndex = 0;
            }
            */

        }

        private void MouseWheelForReadZoom(object sender, MouseEventArgs e)
        {
            ZoomLabelNumber.Text = GMapDrawer.Zoom.ToString();
        }

        private Dictionary<int, int> MemoriesOfIndexOfPolygonAndMarker = new Dictionary<int, int>();



        private Dictionary<Point, int> RouteIndexPositionRecorder = new Dictionary<Point, int>();
        private void DrawButton_Click(object sender, EventArgs e)
        {
            SELMarker.Items.Clear();

            if (controlepidemicplayer != null)
            {
                controlepidemicplayer.Close();
            }


            RouteIndexPositionRecorder.Clear();
            MemoriesOfIndexOfPolygonAndMarker.Clear();


            Random RND = new Random();
            pointInformationProcessor.SetCompareModelExtraInformation(double.Parse(ListPercentage.SelectedItem.ToString()));
            //declear connected relation








            List<GMapOverlay> LayerPool = new List<GMapOverlay>();

            // DataTable pointtables = ReadAsTable.readPointFilesControlByFid(PointFilePath.Text, int.Parse(rgtxt[0]), int.Parse(rgtxt[1]));
            //PointsInformation pointsInformation = new PointsInformation(pointtables);

            GMapDrawer.Overlays.Clear();
            LayerListView.Items.Clear();
            GMapOverlay PolygonLayer = new GMapOverlay("Polygon");
            GMapOverlay MarkerLayer = new GMapOverlay("Marker");
            GMapOverlay RouteLayer = new GMapOverlay("Route");
            RouteLayer.IsVisibile = false;
            LayerPool.Add(PolygonLayer);
            LayerPool.Add(RouteLayer);
            LayerPool.Add(MarkerLayer);
            foreach (GMapOverlay gly in LayerPool)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.SubItems[0].Text = gly.Id;
                LayerListView.Items.Add(lvi);
                LayerListView.Items[LayerListView.Items.Count - 1].Checked = gly.IsVisibile;

            }



            //GMapOverlay goverlay = new GMapOverlay();

            int KeyValCounter = 0;
            foreach (KeyValuePair<int, PointInf> pt in pointInformationProcessor.pointInformation.Base)
            {

                // List<double> ptlatlng =CoordinateTransform.CoordinateStringToCoordinate( CoordinateTransform.TWD97_To_lonlat(Double.Parse((string)pointtables.Rows[y]["TW97X"]), Double.Parse((string)pointtables.Rows[y]["TW97Y"])));
                List<double> ptlatlng = CoordinateTransform.CoordinateStringToCoordinate(((CoordinateTransform.TWD97_To_lonlat(pt.Value.TW97X, pt.Value.TW97Y))));
                GMapMarker gmarker = null;
                //bool.Parse(pt.Value.AdditionalInformation["Emigration"].ToString()) ==



                if (int.Parse(pt.Value.AdditionalInformation["Imported"].ToString()) != 0)
                {
                    gmarker = new GMarkerGoogle(new GMap.NET.PointLatLng(
                                                                                     ptlatlng[1] + ((-5 + (RND.Next() % 10)) * (float)GoogleMapDrawTool.METER)
                                                                                   , ptlatlng[0] + ((-5 + (RND.Next() % 10)) * (float)GoogleMapDrawTool.METER)
                                                                                   )
                                                       , createSolidGMarkerBMP(40,40,Brushes.Purple));
                }

                else
                {
                   
                    if (pt.Value.AdditionalInformation["Infected"].ToString() == "True")
                    {
                        gmarker = new GMarkerGoogle(new GMap.NET.PointLatLng(
                                                                                     ptlatlng[1] + ((-5 + (RND.Next() % 10)) * (float)GoogleMapDrawTool.METER)
                                                                                   , ptlatlng[0] + ((-5 + (RND.Next() % 10)) * (float)GoogleMapDrawTool.METER)
                                                                                   )
                                                       , createSolidGMarkerBMP(40, 40, Brushes.Green));

                    }
                    

                     
                    else{
                        gmarker = new GMarkerGoogle(new GMap.NET.PointLatLng(
                                                                                   ptlatlng[1] + ((-5 + (RND.Next() % 10)) * (float)GoogleMapDrawTool.METER)
                                                                                 , ptlatlng[0] + ((-5 + (RND.Next() % 10)) * (float)GoogleMapDrawTool.METER)
                                                                                 )
                                                     , createSolidGMarkerBMP(40, 40, Brushes.Blue));
                    }
                       

                }



                MemoriesOfIndexOfPolygonAndMarker[pt.Key] = KeyValCounter;
                //gmarker.Tag = q.ToString();
                gmarker.ToolTipText = "Fid:" + pt.Key.ToString();
                gmarker.ToolTipText += "\nOnsetDate:" + pt.Value.OnsetDay.ToShortDateString();
                gmarker.ToolTipText += "\nLog,Lat:" + ptlatlng[0].ToString() + "," + ptlatlng[1].ToString();

                foreach (KeyValuePair<string, object> tmpt in pt.Value.AdditionalInformation)
                {
                    gmarker.ToolTipText += "\n" + tmpt.Key.ToString() + ":" + tmpt.Value.ToString();
                }
                foreach (KeyValuePair<string, object> tmpt in pointInformationProcessor.CompareModelExtraInformation[pt.Key])
                {
                    gmarker.ToolTipText += "\n" + tmpt.Key.ToString() + ":" + tmpt.Value.ToString();

                }
                //draw polygon
                GMapPolygon ctjr = GoogleMapDrawTool.CreateCircle(new PointF((float)ptlatlng[1], (float)ptlatlng[0]), (double)pointInformationProcessor.CompareModelExtraInformation[pt.Key]["Distance_ub"] * GoogleMapDrawTool.METER, 100);
                ctjr.Stroke = Pens.LightPink;
                ctjr.Fill = new SolidBrush(Color.FromArgb(100,255,128,128));
                if (BKBrush == null) BKBrush =(Brush) ctjr.Fill.Clone();
                PolygonLayer.Polygons.Add(ctjr);
                // gmarker.ToolTipMode = MarkerTooltipMode.Always;
                MarkerLayer.Markers.Add(gmarker);

                KeyValCounter++;
            }
            /*
            for (int y = 0; y < pointtables.Rows.Count; y++)
            {
                List<double> ptlatlng = CoordinateTransform.CoordinateStringToCoordinate(CoordinateTransform.TWD97_To_lonlat(Double.Parse((string)pointtables.Rows[y]["TW97X"]), Double.Parse((string)pointtables.Rows[y]["TW97Y"])));

                GMapMarker gmarker = new GMarkerGoogle(new GMap.NET.PointLatLng(ptlatlng[1], ptlatlng[0]), GMarkerGoogleType.blue_pushpin);

                goverlay.Markers.Add(gmarker);
            }*/

            //draw buffer
            /*
            foreach (KeyValuePair<int,Dictionary<string,object>> exinf in pointInformationProcessor.CompareModelExtraInformation)
            {
              double tw97x= pointInformationProcessor.pointInformation.Base[exinf.Key].TW97X;
              double tw97y= pointInformationProcessor.pointInformation.Base[exinf.Key].TW97Y;

                List<double> ptlatlng = CoordinateTransform.CoordinateStringToCoordinate(((CoordinateTransform.TWD97_To_lonlat(tw97x, tw97y))));

                var ctjr=GoogleMapDrawTool.CreateCircle(new PointF((float)ptlatlng[0], (float)ptlatlng[1]),500,10);
                goverlay.Polygons.Add(ctjr);
            }
            */
            //draw route
            int Cnt_forw_p = 0;
            foreach (KeyValuePair<int, List<int>> fwrt in pointInformationProcessor.IDForewardRelation)
            {
                int orip = fwrt.Key;
                foreach (int u in fwrt.Value)
                {
                    RouteIndexPositionRecorder[new Point(orip, u)] = Cnt_forw_p;
                    int endp = u;
                    /*
                    //no relation of  forwd
                    if (!MemoriesOfIndexOfPolygonAndMarker.ContainsKey(endp))
                    {
                        continue;
                    }
                    */
                    List<PointLatLng> pll = new List<PointLatLng>();
                    pll.Add(MarkerLayer.Markers[MemoriesOfIndexOfPolygonAndMarker[orip]].Position);

                    pll.Add(MarkerLayer.Markers[MemoriesOfIndexOfPolygonAndMarker[endp]].Position);
                    RouteLayer.Routes.Add(GoogleMapDrawTool.CreateRoute(pll, ""));
                    Cnt_forw_p++;
                }

            }




            foreach (GMapOverlay govly in LayerPool)
            {
                GMapDrawer.Overlays.Add(govly);
            }

            RefreshGoogleMap();
        }

        private void ModelFile_TextChanged(object sender, EventArgs e)
        {
            /*
            pointInformationProcessor.LoadCompareTable(ModelFile.Text);
            List<double> pclst=pointInformationProcessor.getPercentageList();
            ListPercentage.Items.Clear();
            foreach(double kk in pclst)            ListPercentage.Items.Add(kk);
            ListPercentage.SelectedIndex=0;
            */

        }

        private Brush BKBrush = null;
        private Pen BKRouteStroke = null;

        private void selectedControler()
        {

        }

        private void GMapDrawer_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {



            // GMapDrawer.Overlays[2]
            // process position
            int pos_polygon = 0, pos_marker = 0, pos_route = 0;
            for (int i = 0; i < GMapDrawer.Overlays.Count; i++)
            {
                if ("Marker" == GMapDrawer.Overlays[i].Id) pos_marker = i;
                else if ("Polygon" == GMapDrawer.Overlays[i].Id) pos_polygon = i;
                else if ("Route" == GMapDrawer.Overlays[i].Id) pos_route = i;
                else MessageBox.Show("Error Type");
            }
            //polygon
            int gidx = GMapDrawer.Overlays[pos_marker].Markers.IndexOf(item);
            string fidtext = GMapDrawer.Overlays[pos_marker].Markers[gidx].ToolTipText.Split()[0].Substring(4);

            if (BKBrush == null) BKBrush =(Brush) GMapDrawer.Overlays[pos_polygon].Polygons[gidx].Fill.Clone();
            if (GMapDrawer.Overlays[pos_polygon].Polygons[gidx].Fill == BKBrush)
            {
                GMapDrawer.Overlays[pos_polygon].Polygons[gidx].Fill = new SolidBrush(Color.FromArgb(150, 0, 64, 255));
                SELMarker.Items.Add(fidtext);
            }
            else
            {
                GMapDrawer.Overlays[pos_polygon].Polygons[gidx].Fill = BKBrush;
                SELMarker.Items.Remove(fidtext);
            }
            //select Line routers
            if (BKRouteStroke == null) BKRouteStroke = (Pen)GMapDrawer.Overlays[pos_route].Routes[0].Stroke.Clone();
            int IDNum = int.Parse(fidtext);
            List<int> cidlst = pointInformationProcessor.IDForewardRelation[IDNum];
            foreach (int cid in cidlst)
            {
                int p_router = RouteIndexPositionRecorder[new Point(IDNum, cid)];




                if (GMapDrawer.Overlays[pos_route].Routes[p_router].Stroke == BKRouteStroke)
                {
                    Pen dw = new Pen(Color.Red);
                    //dw.CustomEndCap= new CustomLineCap(GMapDrawer.Overlays[pos_route].Routes[p_router].Stroke.CustomEndCap.BaseCap);
                    dw.CustomEndCap = (CustomLineCap)GMapDrawer.Overlays[pos_route].Routes[p_router].Stroke.CustomEndCap.Clone();
                    dw.Width = GMapDrawer.Overlays[pos_route].Routes[p_router].Stroke.Width;

                    GMapDrawer.Overlays[pos_route].Routes[p_router].Stroke = dw;
                }
                else
                {
                    GMapDrawer.Overlays[pos_route].Routes[p_router].Stroke = BKRouteStroke;
                }

            }

            List<int> cidblst = pointInformationProcessor.IDBackwardRelation[IDNum];
            foreach (int cid in cidblst)
            {
                int p_router = RouteIndexPositionRecorder[new Point(cid, IDNum)];
                if (GMapDrawer.Overlays[pos_route].Routes[p_router].Stroke == BKRouteStroke)
                {
                    Pen dw = new Pen(Color.Yellow);
                    //dw.CustomEndCap= new CustomLineCap(GMapDrawer.Overlays[pos_route].Routes[p_router].Stroke.CustomEndCap.BaseCap);
                    dw.CustomEndCap = (CustomLineCap)GMapDrawer.Overlays[pos_route].Routes[p_router].Stroke.CustomEndCap.Clone();
                    dw.Width = GMapDrawer.Overlays[pos_route].Routes[p_router].Stroke.Width;

                    GMapDrawer.Overlays[pos_route].Routes[p_router].Stroke = dw;

                }
                else
                {
                    GMapDrawer.Overlays[pos_route].Routes[p_router].Stroke = BKRouteStroke;
                }

            }


        }

        private void LayerListView_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string idd = LayerListView.Items[e.Index].SubItems[0].Text;
            GMapOverlay st = new GMapOverlay();
            foreach (GMapOverlay govs in GMapDrawer.Overlays)
            {
                if (govs.Id == idd)
                {
                    st = govs;
                    break;
                }
            }

            if (e.CurrentValue == CheckState.Unchecked)
            {

                st.IsVisibile = true;
            }
            else
            {
                st.IsVisibile = false;
            }
        }

        private void SELMarker_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (SELMarker.SelectedItem == null) return;

            int id = int.Parse((string)SELMarker.SelectedItem);
            //MessageBox.Show(id.ToString());
            int mkidx = 0;
            for (; mkidx < GMapDrawer.Overlays.Count; mkidx++)
            {
                if (GMapDrawer.Overlays[mkidx].Id == "Marker") break;
            }
            PointLatLng pll = GMapDrawer.Overlays[mkidx].Markers[MemoriesOfIndexOfPolygonAndMarker[id]].Position;
            //  PointInf ptinf=pointInformationProcessor.pointInformation.Base[id];
            // List<double> ptlatlng = CoordinateTransform.CoordinateStringToCoordinate(((CoordinateTransform.TWD97_To_lonlat(ptinf.TW97X, ptinf.TW97Y))));



            GMapDrawer.Position = pll;

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ListPercentage_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void LoadBTN_Click(object sender, EventArgs e)
        {

            pointInformationProcessor = new PointInformationProcessor( ModelFile.Text, PointFidRange.Text);

            {
                string fidlb, fidub;
                fidlb = pointInformationProcessor.pointInformation.Base.Keys.Min().ToString();
                fidub = pointInformationProcessor.pointInformation.Base.Keys.Max().ToString();
                PointFidRange.Text = fidlb + " " + fidub;

            }

            if (File.Exists(ModelFile.Text))
            {
                pointInformationProcessor.LoadCompareTable(ModelFile.Text);
                List<double> pclst = pointInformationProcessor.getPercentageList();
                ListPercentage.Items.Clear();
                foreach (double kk in pclst) ListPercentage.Items.Add(kk);
                ListPercentage.SelectedIndex = 0;
            }
        }

        private void RefreshGMapBtn_Click(object sender, EventArgs e)
        {
            RefreshGoogleMap();
        }
        private void RefreshGoogleMap()
        {
            var cruz = GMapDrawer.Zoom;
            GMapDrawer.Zoom = cruz + 1;
            GMapDrawer.Zoom = cruz;
            ZoomLabelNumber.Text = GMapDrawer.Zoom.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //GMapDrawer_OnMarkerClick();
        }

        private void GMapDrawer_Load(object sender, EventArgs e)
        {
            
        }

        private void TestGMP_Click(object sender, EventArgs e)
        {
            GoogleMapPresentation_Load(null, null);
        }

        private void MapTypeCbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            GMapDrawer.MapProvider = MapProviders[MapTypeCbx.Text];

        }



        private void OpSOMBtn_Click(object sender, EventArgs e)
        {
            SOMFileOFD.ShowDialog();
            if (SOMFileOFD.FileName != String.Empty) ModelFile.Text = SOMFileOFD.FileName;
        }

        ControlEpidemicPlayer controlepidemicplayer = null;
        private void OpenAnimationPlayerForm_Click(object sender, EventArgs e)
        {

        }

        private void animationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (controlepidemicplayer == null || controlepidemicplayer.IsDisposed) controlepidemicplayer = new ControlEpidemicPlayer(this.GMapDrawer, pointInformationProcessor.IDBackwardRelation);
            controlepidemicplayer.Show();
            controlepidemicplayer.TopMost = true;
            this.TopMost = false;
        }

        private string CaptureMapPath= System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
        int CaptureMapPicturesCounter = 0;
        private void CapturePictureBtn_Click(object sender, EventArgs e)
        {
            Image gmpmg = GMapDrawer.ToImage();
            gmpmg.Save(CaptureMapPath+"\\"+"gmppic"+CaptureMapPicturesCounter.ToString()+".tiff", System.Drawing.Imaging.ImageFormat.Tiff);
            CaptureMapPicturesCounter++;
        }

        private void pathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CaptureMapFBD.ShowDialog();
            string cmpth=CaptureMapFBD.SelectedPath;
            if (cmpth.Count() != 0) CaptureMapPath=cmpth;
        }

        private void setMapSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sztxt=Interaction.InputBox("Input Size (x,y):", "Set map size");
            string[] sztarr=sztxt.Split(',');
            int szx, szy;
            bool  isx=int.TryParse(sztarr[0], out szx);
            bool isy=int.TryParse(sztarr[1], out szy);
            if (isx && isy)
            {
                GMapDrawer.Size = new Size(szx, szy);
            }

            
        }
        private Bitmap createSolidGMarkerBMP(int bitmpw,int bitmph,Brush br )
        {
            //create bitmap
           // int bitmpw = 20, bitmph = 20;
            Bitmap bmpgmarker = new Bitmap(bitmpw, bitmph);

            Graphics g = Graphics.FromImage(bmpgmarker);

            Pen blackPen = new Pen(Color.Black);
            int x = bitmpw /8*3;
            int y = bitmph /8*6;
            int width = bitmpw / 4;
            int height = bitmph / 4;
            int diameter = Math.Min(width, height);
            //g.DrawEllipse(blackPen, x, y, diameter, diameter);
            g.FillEllipse(br, x, y, diameter, diameter);
            //end create bitmap
            return bmpgmarker;
        }

        private void showCenterToggleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GMapDrawer.ShowCenter = !GMapDrawer.ShowCenter;
            RefreshGoogleMap();
        }

        private void GMapDrawer_OnPositionChanged(PointLatLng point)
        {
            CenterPositionLb.Text = Math.Round(point.Lat,4).ToString() + "," + Math.Round(point.Lng,4).ToString();
        }

        private void changeCenterPositionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sztxt = Interaction.InputBox("Input  Lat,Lng:", "Set map center");
            string[] sztarr = sztxt.Split(',');
            double szx, szy;
            bool isx = double.TryParse(sztarr[0], out szx);
            bool isy = double.TryParse(sztarr[1], out szy);
            if (isx && isy)
            {
                PointLatLng pll=new PointLatLng(szx,szy);
            

                GMapDrawer.Position = pll;
            }

        }
    }
}
