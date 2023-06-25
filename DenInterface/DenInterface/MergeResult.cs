using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DenInterface
{
    class MergeResult
    {
        public static void mergeCSVResultByLine(string mergeName,List<string> csvfl)
        {
            StreamWriter swr = new StreamWriter(mergeName);
            StreamReader[] fullstrrd=new StreamReader[csvfl.Count];
            for(var k=0; k<csvfl.Count;k++)
            {
                fullstrrd[k] = new StreamReader(csvfl[k]);
            }

            //wr sort title
            for (var k = 0; k < csvfl.Count; k++)
            {
               swr.Write(Path.GetFileNameWithoutExtension(csvfl[k]));
                swr.Write(","); swr.Write(",");
            }
            swr.Write("\n");
            //wr every files
            bool tltflg = true;
            while (true) {
                string tln="";
                
                for (var k = 0; k < csvfl.Count; k++)
                {
                    tln = fullstrrd[k].ReadLine();
                    if (tln == null) break;
                    swr.Write(tln);
                    swr.Write(",");
                            if (tltflg)// for title no ,
                            {
                                swr.Write(",");
                            }

                }
                
                swr.Write("\n");
                tltflg = false;
                if (tln == null) break;
            }

            //close all 
            swr.Close();
            for (var k = 0; k < csvfl.Count; k++)
            {
                fullstrrd[k].Close();
            }

        }

    }
}
