using drawer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
namespace DengueGoogleMap
{
    class PointInformationProcessor
    {
        //basic
        public string BaseModelFileName;
        public PointsInformation pointInformation=null;
        protected int StartFid,EndFid;

        //tables
        //public DataTable MainPointTable=null;
        public DataTable BaseModelTable = null;
        public DataTable CompareModelTable = null;






        public PointInformationProcessor(string basemdfn, string fidrangetext)
        {
            //init
            BaseModelFileName = basemdfn;
            //CompareModelFileName = cmpmdfn;

            //pre reading
            BaseModelTable = ReadAsTable.readPointFiles(BaseModelFileName);
          

            int start_fid= (int)BaseModelTable.Rows[0]["Fid"];
            StartFid = start_fid;
            int end_fid  = (int)BaseModelTable.Rows[BaseModelTable.Rows.Count-1]["Fid"];
            EndFid = end_fid;

            /*
            MainPointTable = ReadAsTable.readPointFilesControlByFid(pointfn, StartFid, EndFid);
            pointInformation = new PointsInformation(MainPointTable);
            */
            pointInformation = new PointsInformation(BaseModelTable);
            callProcessPointProperties(); 
        }

        public void callProcessPointProperties()
        {
            Dictionary<int, PointInf> bkpbase = new Dictionary<int, PointInf>();


            //find distinct infected in percentage base
            List<int> distinctInfectedFid;
            {
                List<int> prpol = new List<int>();
                for (int i = 0; i < BaseModelTable.Rows.Count; i++)
                {
                    int pnum = (int)BaseModelTable.Rows[i]["ConnectedPairNUM"];
                    if (pnum == 0) continue;
                    //if ((double)BaseModelTable.Rows[i]["Percentage"] != 1) continue;
                    
                    string ckpr =( (string)BaseModelTable.Rows[i]["PairFid"]).Trim();
                    if (ckpr[ckpr.Length - 1] == ';') ckpr = ckpr.Remove(ckpr.Length - 1);
                    string[] pr = ckpr.Split(';');

                    foreach (string gg in pr)
                    {
                        prpol.Add(int.Parse(gg));
                    }
                }

                distinctInfectedFid = prpol.Distinct().ToList();
            }
            

            foreach ( KeyValuePair<int, PointInf> prcs in pointInformation.Base)
            {
                PointInf curprcpt = prcs.Value;

                    DataRow[] drow = BaseModelTable.Select(("Fid="+prcs.Key.ToString() ));
                    int oversea= int.Parse(drow[0]["Imported"].ToString());
                prcs.Value.AdditionalInformation["Imported"] = oversea;



                // DataRow[]  prow = BaseModelTable.Select("Fid="+prcs.Key.ToString()+" And " + " Percentage="+"1");
                
                prcs.Value.AdditionalInformation["Infected"] = distinctInfectedFid.Contains(prcs.Key);

                DataRow [] crdrow= BaseModelTable.Select("Fid =" + prcs.Key.ToString());
                prcs.Value.AdditionalInformation["Infector"] =((int)crdrow[0]["ConnectedPairNUM"]!=0)? true: false;

                //prcs.Value.AdditionalInformation["InModel"] = (Boolean)false;
                // prcs.Value.AdditionalInformation["IsPrediction"] = (Boolean)false;
                // prcs.Value.AdditionalInformation["Infected"] = (Boolean)true;


                bkpbase[prcs.Key] = curprcpt;

            }


            pointInformation.Base = bkpbase;

        }

        //process comparetable
        public void LoadCompareTable(string cpfn)
        {
            CompareModelTable =ReadAsTable.readPointFiles(cpfn);


        }

        public List<double> getPercentageList()
        {
            List<double> rt = new List<double>();
            List<object> pclist = (from row in CompareModelTable.AsEnumerable() select row["Percentage"]).Distinct().ToList();
            foreach(object k in pclist)
            {
                rt.Add(double.Parse(k.ToString()));
            }
            return rt;
        }
        public Dictionary<int, Dictionary<string, object>> CompareModelExtraInformation = null;
        public void SetCompareModelExtraInformation(double percentage)
        {
            IDForewardRelation.Clear();
            IDBackwardRelation.Clear();
            CompareModelExtraInformation = new Dictionary<int, Dictionary<string, object>>();
            DataRow[] pdr= CompareModelTable.Select("Percentage=" + percentage.ToString());
            for(int i=0; i < pdr.Length; i++)
            {
                Dictionary<string, object> edic = new Dictionary<string, object>();
                string lginf = "LgInf"; edic[lginf] = pdr[i][lginf];
                string lgsup = "LgSup"; edic[lgsup] = pdr[i][lgsup];
                string distance = "Distance_ub"; 
                //smrt distance
               
                edic[distance] = pdr[i][distance];


                CompareModelExtraInformation[(int)(pdr[i]["Fid"])] =edic ;

                        //process Connected
                        if (!IDForewardRelation.ContainsKey((int)pdr[i]["Fid"]))
                        {
                            IDForewardRelation[(int)pdr[i]["Fid"]] = new List<int>();
                        }
                            //parse relation
                             string pstr= pdr[i]["PairFid"].ToString();
                             pstr = pstr.Replace(';',' ');
                             string[] psaint= pstr.Trim().Split();
                                foreach(string kkk in psaint)
                                {
                                    if (kkk =="") break;
                                    int fwid = int.Parse(kkk);

                                        if (pointInformation.Base.ContainsKey(fwid) && pointInformation.Base.ContainsKey((int)pdr[i]["Fid"]))
                                        {
                                            //add fd
                                            IDForewardRelation[(int)pdr[i]["Fid"]].Add(fwid);
                                            if (!IDBackwardRelation.ContainsKey(fwid))
                                            {
                                                IDBackwardRelation[fwid] = new List<int>();
                                            }
                                            //add wd
                                            IDBackwardRelation[fwid].Add((int)pdr[i]["Fid"]);
                                        }

                                }
                        //fix backrelation null keycontain error
                        if (!IDBackwardRelation.ContainsKey((int)pdr[i]["Fid"]))
                        {
                            IDBackwardRelation[(int)pdr[i]["Fid"]] = new List<int>();
                        }


            }





        }

        //Forward Connected
        public Dictionary<int, List<int>> IDForewardRelation = new Dictionary<int, List<int>>();
        //Backward Connected
        public Dictionary<int, List<int>> IDBackwardRelation = new Dictionary<int, List<int>>();


    }
}
