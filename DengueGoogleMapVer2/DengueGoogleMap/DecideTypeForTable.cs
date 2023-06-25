using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    class DecideTypeForTable
    {
    public static string getTypeString(string str)
    {
        string rt= "System.String";
        int vl;double dv; 
        if(int.TryParse(str,out vl))
        {
            rt = "System.Int32";
        }
        else if (double.TryParse(str, out dv))
        {
            rt = "System.Double";
        }


        return rt;
    }



    }

