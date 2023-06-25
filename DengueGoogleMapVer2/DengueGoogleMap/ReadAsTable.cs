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
    class ReadAsTable
    {
        public static DataTable readPointFiles(string ptfn, int startr=0,int endr= 2147483647)
        {
                DataTable dtbl = new DataTable();
            //preload 
            List<string> decideColumnType = new List<string>();
            {

                StreamReader prerd = new StreamReader(ptfn);
                //decide type
                string eatt = "";
                prerd.ReadLine();
                eatt = prerd.ReadLine();
                eatt.Trim();
                if (eatt[eatt.Length - 1] == ',') eatt = eatt.Remove(eatt.Length - 1);
                string[] prpcoln = eatt.Split(',');
                //decide type core

                for (int i = 0; i < prpcoln.Length; i++)
                {
                    decideColumnType.Add(DecideTypeForTable.getTypeString(prpcoln[i]));
                }


                prerd.Close();

            }



            StreamReader strrd = new StreamReader(ptfn);

            //col
            string ln = "";
            ln = strrd.ReadLine();
            {
                ln = ln.Trim();
                if (ln[ln.Length - 1] == ',') ln = ln.Remove(ln.Length - 1);
                string[] prpcoln = ln.Split(',');
                int p_cnt = 0;
                foreach (string item in prpcoln)
                {

                    DataColumn dtblcnmn = new DataColumn();
                    dtblcnmn.DataType = Type.GetType(decideColumnType[p_cnt]);   //Type.GetType("System.String");
                    dtblcnmn.ColumnName = item.Trim();
                    dtbl.Columns.Add(dtblcnmn);

                    p_cnt++;
                }
            }
            //row
            int cntctrl = 0;
                    while((ln = strrd.ReadLine()) != null)
                    {
                if (cntctrl < startr) { cntctrl++; continue; }
                        DataRow dr = dtbl.NewRow();
                      ln = ln.Trim();
                        if (ln[ln.Length - 1] == ',') ln = ln.Remove(ln.Length - 1);
                        string[] prpcoln = ln.Split(',');
                         for (int k= 0; k<prpcoln.Length;k++)
                         {
                           
                            dr[k] = prpcoln[k].Trim();
                         }
                        dtbl.Rows.Add(dr);
                        if (cntctrl >= endr) break;
                        cntctrl++;
                     
                    }
           // MessageBox.Show(cntctrl.ToString());


            strrd.Close();




            return dtbl;

        }

        public static DataTable readPointFilesControlByFid(string ptfn, int strfid = 0, int endfid = 2147483647)
        {
            DataTable dtbl = new DataTable();


            //preload 
            List<string> decideColumnType = new List<string>();
            {

                StreamReader prerd = new StreamReader(ptfn);
                //decide type
                string eatt="";
                prerd.ReadLine();
                eatt=prerd.ReadLine();
                eatt.Trim();
                    if (eatt[eatt.Length - 1] == ',') eatt = eatt.Remove(eatt.Length - 1);
                    string[] prpcoln = eatt.Split(',');
                //decide type core
                    
                for(int i=0; i < prpcoln.Length; i++)
                {
                    decideColumnType.Add(DecideTypeForTable.getTypeString(prpcoln[i]));
                }


                prerd.Close();

            }
            




            StreamReader strrd = new StreamReader(ptfn);

            //col
            string ln = "";
            ln = strrd.ReadLine();
            {
                ln = ln.Trim();
                if (ln[ln.Length - 1] == ',') ln = ln.Remove(ln.Length - 1);
                string[] prpcoln = ln.Split(',');
                int p_cnt = 0;
                foreach (string item in prpcoln)
                {

                    DataColumn dtblcnmn = new DataColumn();
                    dtblcnmn.DataType = Type.GetType(decideColumnType[p_cnt]);   //Type.GetType("System.String");
                    dtblcnmn.ColumnName = item.Trim();
                    dtbl.Columns.Add(dtblcnmn);

                    p_cnt++;
                }
            }




            //row
            int cntctrl = 0;
            while ((ln = strrd.ReadLine()) != null)
            {

                
                DataRow dr = dtbl.NewRow();
                
                ln = ln.Trim();
                if (ln[ln.Length - 1] == ',') ln = ln.Remove(ln.Length - 1);
                string[] prpcoln = ln.Split(',');
                        //process fid 
                        int par_fid=int.Parse(prpcoln[0]);
                //control start end by fid
                if (par_fid < strfid) { cntctrl++; continue; }

                if (par_fid > endfid) { break; }



               
                for (int k = 0; k < prpcoln.Length; k++)
                {
                   
                    string kk = prpcoln[k];  
                    dr[k] = prpcoln[k].Trim();
                    
                }
                dtbl.Rows.Add(dr);
                if (cntctrl >= endfid) break;
                cntctrl++;

            }
            // MessageBox.Show(cntctrl.ToString());


            strrd.Close();




            return dtbl;

        }
    }
}
