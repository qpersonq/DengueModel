using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DenInterface
{
    class RecorderReporterIntergrate
    {
       public double badNumber=-99999799999;
       public List<string> Capital = new List<string>();
       public List<List<string>> InternalData=new List<List<string>>();
        public int RowSize()
        {
            return InternalData.Count;
        }
        public int ColSize()
        {
            return Capital.Count;
        }
       public string accessData(int r,int c)
       {
            return InternalData[r][c];
       }
       public double accessDataDouble(int r, int c)
        {
            string sval=accessData(r, c);
            bool isnn=Microsoft.VisualBasic.Information.IsNumeric(sval);
            if (!isnn) return badNumber;

            return Double.Parse(sval);
        }
        public int Indexof(string cpstr) {

            return Capital.IndexOf(cpstr);
        }
        public bool readRecordReport(string fn)
        {
            StreamReader rdt = new StreamReader(fn);
            string line;
            // read cap
            line = rdt.ReadLine();
            line = line.Trim();
            string[] tmpdt = line.Split(',');
            foreach (string p in tmpdt)
            {
                string px = p.Trim();
                Capital.Add(px);
                
            }
            //read prop

            while ((line = rdt.ReadLine()) != null)
            {
                line = line.Trim();
                //line.Replace(',', ' ');
                if (line[line.Length - 1] == ',') line = line.Remove(line.Length - 1);
                tmpdt = line.Split(',');
                List<string> slist = new List<string>();
                foreach(string k in tmpdt){
                    slist.Add(k);
                }

                InternalData.Add(slist);
            }

            rdt.Close();

                return true;
        }
        public bool readfromListView(ListView lvc)
        {
            //lb
            for(int i=0; i< lvc.Columns.Count; i++)
            {
                Capital.Add(lvc.Columns[i].Text);
            }
            //data
            for(int i=0; i < lvc.Items.Count; i++)
            {
                List<string> tmpln = new List<string>();
                for(int j=0; j < lvc.Items[i].SubItems.Count; j++)
                {
                    tmpln.Add(lvc.Items[i].SubItems[j].Text);
                }
                    

                this.InternalData.Add(tmpln);
            }

            return true;
        }

    }
    class calculateRRI
    {
       // public static string XAxisItem, YAsisItem;
        public static double calculateIntegralByABS(RecorderReporterIntergrate rri, string xaxixitem,string yaxisitem)
        {
            string XAxisItem=xaxixitem, YAsisItem=yaxisitem;
            double culkrate = 0.0;
            int culcnt = 0;
            int idxc=rri.Indexof(XAxisItem), idxd=rri.Indexof(YAsisItem);
            for (int z = 0; z < rri.RowSize(); z++)
            {
                double cnt = rri.accessDataDouble(z, idxc);
                double dd = rri.accessDataDouble(z, idxd);
                if (cnt == rri.badNumber || dd == rri.badNumber)
                {
                    continue;
                }
                culcnt += 1;
                culkrate += Math.Sqrt (  
                    (cnt*cnt)+(dd*dd)
                    );

            }
            return culkrate/culcnt;
        }
        public static double calculateIntegralByArea(RecorderReporterIntergrate rri, string xaxixitem, string yaxisitem, AdditionCalculateRow addicalrw, bool divrange=true)
        {
            string XAxisItem=xaxixitem, YAsisItem=yaxisitem;
            double culkrate = 0.0;
            int culcnt = 0;
            int idxc = rri.Indexof(XAxisItem), idxd = rri.Indexof(YAsisItem);
            List<double> xarr = new List<double>(), yarr = new List<double>();

            for (int z = 0; z < rri.RowSize(); z++)
            {
                double cnt = rri.accessDataDouble(z, idxc);
                double dd = rri.accessDataDouble(z, idxd);
                if (cnt == rri.badNumber || dd == rri.badNumber)
                {
                    continue;
                }
                culcnt += 1;
                // prcs addition calculate

                cnt=  addicalrw.getCalculate(cnt, 0, z);
                dd = addicalrw.getCalculate(dd, 1, z);



                xarr.Add(cnt);
                yarr.Add(dd);
            }
            // let integrate
            double Px = xarr[0], Py = 0.0, Qx = xarr[xarr.Count - 1], Qy = 0.0;
            //cal Q point
            xarr.Add(Qx);
            yarr.Add(Qy);
            for (int e = 0; e < xarr.Count - 1/*end Q point */; e++)
            {
                culkrate += CALTRIANGLE(Px, Py, xarr[e], yarr[e], xarr[e + 1], yarr[e + 1]);

            }

            if (divrange == true) culkrate /= (xarr[xarr.Count - 1] - xarr[0]);

            return culkrate;
        }
        public static double CALTRIANGLE(double x1, double y1, double x2, double y2, double x3, double y3)
        {
            double area_ = 0.5 * Math.Abs(x2 * y3 + x1 * y2 + x3 * y1 - x3 * y2 - x2 * y1 - x1 * y3);
            return area_;
        }
    }
    
    public class getRankList
    {
        public static List<int> getRank(List<double> rk)
        {
            List<int> rt = new List<int>();
            //rt.Capacity=(rk.Count);
            var ranks = rk
            .GroupBy(item => item)
            .OrderByDescending(chunk => chunk.Key)
            .Select((chunk, index) => new {
                  item = chunk.Key,
                  rank = index + 1
             })
             .ToDictionary(x => x.item, x => x.rank);
            //MessageBox.Show(ranks.ToString());
            for (int  i=0; i<rk.Count;i++)
            {
                rt.Add( ranks[rk[i]]);
                //MessageBox.Show(ranks[rk[i]].ToString());
                
            }
                return rt;
        }
    }


}
