using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DenInterface
{
    class RunFunction
    {
        private TaskCompletionSource<bool> eventHandled;
        
        private bool NowRunning = false;
        
        public  async  Task  train(string Dicstr,string Dentr_exe,string pre_prop,string seed_numvar="")
        {
            eventHandled = new TaskCompletionSource<bool>();

            string ndicnm = Dicstr + "\\Train";
            Directory.CreateDirectory( ndicnm);
            if (seed_numvar != "") File.Copy(seed_numvar,Path.Combine(ndicnm, "RandomSeed.varser"));
            if(!File.Exists(ndicnm + "\\Denguetrain.exe")) { File.Copy(Dentr_exe, ndicnm + "\\Denguetrain.exe"); }



            using (StreamWriter outputFile = new StreamWriter(Path.Combine(ndicnm, "DengueDiffusionModel.ini"),false,Encoding.ASCII))
            {
                outputFile.Write(pre_prop);
            }
           
            /*
                Process prc = new Process();
            prc.StartInfo.WorkingDirectory = ndicnm;
            prc.StartInfo.FileName = "Denguetrain.exe";
            prc.Exited+= new EventHandler(myProcess_Exited); 
            prc.Start();
            */
           
                Process prc = new Process();
                prc.StartInfo.WorkingDirectory = ndicnm;
                prc.StartInfo.FileName = "Denguetrain.exe";
                prc.EnableRaisingEvents = true;
                prc.Exited += new EventHandler(myProcess_Exited);
                prc.Start();
               
                //Thread.Sleep(10000);
                //prc.WaitForExit();
                
           
                NowRunning = true;
                while (NowRunning)
                {
                    Thread.Sleep(10000);
                }
            //StopThread.Interrupt();
            //MessageBox.Show("t");
            // prc.WaitForExit();
            //prc.Exited
           
            await Task.WhenAny(eventHandled.Task);
            File.Delete(ndicnm + "\\Denguetrain.exe");
            
        }
        public  async Task  Prediction(bool isprediction,string Dicstr, string DenPr_exe, string pre_prop_as_trn,string vldsomsavstr,string forprdstr="")
        {
            string ndicnm = Dicstr + ((isprediction)? ("\\Prediction"+ forprdstr) : "\\Valid");
            Directory.CreateDirectory(ndicnm);
            if (!File.Exists(ndicnm + "\\DengueValid.exe")) { File.Copy(DenPr_exe, ndicnm + "\\DengueValid.exe"); }

            string svlnn = vldsomsavstr + ".sav";
            
             File.Copy(svlnn, Path.Combine(ndicnm,Path.GetFileName(svlnn)),true);
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(ndicnm, "DengueDiffusionModel.ini"), false, Encoding.ASCII))
            {
                outputFile.Write(pre_prop_as_trn);
            }

            Process prc = new Process();
            prc.StartInfo.WorkingDirectory = ndicnm;
            prc.StartInfo.FileName = "DengueValid.exe";
            prc.EnableRaisingEvents = true;
            prc.Exited += new EventHandler(myProcess_Exited);
            prc.Start();

            //prc.WaitForExit();
            NowRunning = true;
            while (NowRunning)
            {
                Thread.Sleep(10000);
            }
            await Task.WhenAny(eventHandled.Task);
            File.Delete(ndicnm + "\\DengueValid.exe");
            //return ndicnm;

        }
        private void myProcess_Exited(object sender, System.EventArgs e)
        {
           
            if(eventHandled!=null)  eventHandled.TrySetResult(true);
            NowRunning = false;
            //MessageBox.Show("123");
        }



    }
}
