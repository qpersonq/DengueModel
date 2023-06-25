using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    class GoogleMapDrawTool
    {
    public  static readonly double METER = 0.00000900900901;
    public static GMapPolygon CreateCircle(PointF point, double radius, int segments = 30)
        {
        

            List<GMap.NET.PointLatLng> gpollist = new List<GMap.NET.PointLatLng>();

            double seg = Math.PI * 2 / segments;

            //int y = 0;
            for (int i = 0; i < segments; i++)
            {
                double theta = seg * i;
                double a = point.X + Math.Cos(theta) * radius;
                double b = point.Y + Math.Sin(theta) * radius;

                GMap.NET.PointLatLng gpoi = new GMap.NET.PointLatLng(a, b);

                gpollist.Add(gpoi);
            }
            GMapPolygon gpol = new GMapPolygon(gpollist, "pol");
            return gpol;
            //overlayOne.Polygons.Add(gpol);
        }
        public static GMapRoute CreateRoute(List<GMap.NET.PointLatLng> plist, string rtnm)
        {

            GMapRoute grt = new GMapRoute(rtnm);
            grt.Stroke.Color = Color.FromArgb(50, Color.SkyBlue);
             GraphicsPath ggp = new GraphicsPath();
            //ggp.AddLine(100, 0, -100, 0);
            PointF[] trilns = new PointF[3];
            trilns[0] = new PointF(-2, -2);
            trilns[1] = new PointF(0, 0);
            trilns[2] = new PointF(2, -2);

            ggp.AddLines(trilns);
            CustomLineCap eccp = new CustomLineCap(null, ggp);
           

            grt.Stroke.CustomEndCap = eccp;



            //    GMarkerGoogleType.arrow;
            grt.Points.AddRange(plist);



            return grt;
        }
    }

