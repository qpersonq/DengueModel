using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace DenInterface
{
    class rdINI
    {
        public rdINI()
        {

        }
         
        static public Dictionary<string ,string> rdinifl(string pth_)
        {
            Dictionary<string, string> rdarr =new Dictionary<string, string>();
            //if (!File.Exists(pth_)) { MessageBox.Show("no ini data " + pth_); return rdarr };
            try
            {
                StreamReader rfs = new StreamReader(pth_);
                string ln;
                while ((ln = rfs.ReadLine()) != null)
                {
                    // ln = ln.Replace(',', ' ');
                    string[] sp = ln.Split(' ');
                    string bh = "";
                    for (int k = 0; k < sp.Length; k++)
                    {
                        if (k != 0) bh += ((sp[k]) + " ");
                    }
                    bh=bh.Trim();
                    rdarr[sp[0]] = bh;
                }

                rfs.Close();



            }
            catch(FileNotFoundException e)
            {
                MessageBox.Show(e.ToString()); return rdarr;
            }
            
           
            return rdarr;

        }
        static public Dictionary<string, string> relfectContainer(string prop)
        {
            
            string ln;
            Dictionary<string, string> rdarr = new Dictionary<string, string>();
            if (prop == "") return rdarr;


            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(prop);
            writer.Flush();

            // convert stream to string
            stream.Position = 0;



            StreamReader rfs = new StreamReader(stream);
            
            while ((ln = rfs.ReadLine()) != null)
            {
                // ln = ln.Replace(',', ' ');
                string[] sp = ln.Split(' ');
                string bh = "";
                for (int k = 0; k < sp.Length; k++)
                {
                    if (k != 0) bh += ((sp[k]) + " ");
                }
                bh=bh.Trim();
                rdarr[sp[0]] = bh;
            }
            rfs.Close();
            return rdarr;
        }
    }
    

}
