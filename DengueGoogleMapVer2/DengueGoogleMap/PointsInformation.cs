
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace drawer
{
    class PointsInformation
    {
        public Dictionary<int,PointInf> Base= new Dictionary<int, PointInf>();
        public PointsInformation(DataTable dtbl)
        {
            for(int u=0; u < dtbl.Rows.Count; u++)
            {
                int tid = (int)dtbl.Rows[u]["Fid"];
                DateTime tdt= DateTime.Parse((string)dtbl.Rows[u]["Date"]);
                double twx=double.Parse(dtbl.Rows[u]["CoorX"].ToString()), twy= double.Parse(dtbl.Rows[u]["CoorY"].ToString());
                //MessageBox.Show(twx.ToString());
                Base[tid]= new PointInf(tdt, twx, twy);
                //Base[tid].AdditionalInformation["SeroType"] = int.Parse((string)dtbl.Rows[u]["Serotype"]);
            }
            

        }

    }

    class PointInf
    {
        
        public DateTime OnsetDay;
        public double TW97X, TW97Y;
       //  StereoType;


        
        public Dictionary<string, object> AdditionalInformation=new Dictionary<string, object>();
        public PointInf(DateTime dt, double tw97x,double tw97y)
        {
            OnsetDay = dt;
            TW97X = tw97x;
            TW97Y = tw97y;
            
            //AdditionalInformation["SeroType"] =0;
            /*
            AdditionalInformation["Emigration"] =(Boolean)false;
            AdditionalInformation["InModel"] = (Boolean)false;
            AdditionalInformation["IsPrediction"] = (Boolean)false;
            AdditionalInformation["Infected"] = (Boolean)true;
            */
            
        }

    }


}
