using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DenInterface;


namespace DenInterface
{
    public partial class DengueInterface : Form
    {
        
      
        Dictionary<string, string> PropertiesDic;
        StatusBar sbr = new StatusBar();
        string BasicPathInformation = "BasicPathInformation.ini";
        public int PID = 0;
        public string REF_VALID_STRING;

        public DengueInterface()
        {
           // GraphLine gflnform = new GraphLine();
            //gflnform.Show();

            InitializeComponent();


            PID = 0;
            sbr.Parent = this;
            sbr.Text = "Nothing";
            readInitialInformation();
            label15.Text = CurrentRunPid.ToString();
        }
        public DengueInterface(int pid)
        {
           
            InitializeComponent();
            PID = pid;
            this.Text = "Dengue Sub Process " + pid.ToString();
            //this.Prptext.Text = proptxt;
            label15.Text = CurrentRunPid.ToString();
        }



        public void readInitialInformation()
        {
            try
            {
                StreamReader binf = new StreamReader(BasicPathInformation);
               


                inipath.Text = binf.ReadLine();
                RootTextBox.Text = binf.ReadLine();
                DicNN.Text = binf.ReadLine();
                trainEXEText.Text = binf.ReadLine();
                ValPreText.Text = binf.ReadLine();
                RAnaRanage.Text= binf.ReadLine();
                RanaProp.Text= binf.ReadLine();



                binf.Close();
            }
            catch(IOException e)
            {

            }
            


        }








        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {


            PropertiesDic = rdINI.rdinifl(inipath.Text);


            TextRefresh();


        }
        public void TextRefresh()
        {
            //PropertiesDic=rdINI.relfectContainer(Prptext.Text);
            string apstr = "";
            foreach (KeyValuePair<string, string> p in PropertiesDic)
            {
                apstr += (p.Key + " " + p.Value + '\r');
            }
            Prptext.Text = apstr;
        }
        private void DengueInterface_Load(object sender, EventArgs e)
        {
            PropertiesDic = rdINI.rdinifl(inipath.Text);        
            TextRefresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FBDRT.ShowDialog();
            RootTextBox.Text = FBDRT.SelectedPath;
        }

        private void setDengueIniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            opiniFD.ShowDialog();
            inipath.Text = opiniFD.FileName;

            PropertiesDic = rdINI.rdinifl(inipath.Text);


            TextRefresh();

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void enviromentAndClimateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PropertiesDic["KeepAttributes"] = "PositionX,PositionY,Distant,Lag,T_MinMIN1,T_MinMIN2,T_MinMIN3,T_MinMIN4,T_MinMIN5,T_MinMIN6,T_MinMIN7,T_MinMIN8,T_MinMIN9,T_MinMIN10,T_MinMIN11,T_MinMIN12,T_MinMIN13,T_MinMIN14,T_MinMIN15,T_MinMIN16,T_MinMIN17,T_MinMIN18,T_MinMIN19,T_MinMIN20,PrecpSUM1,PrecpSUM2,PrecpSUM3,PrecpSUM4,PrecpSUM5,PrecpSUM6,PrecpSUM7,PrecpSUM8,PrecpSUM9,PrecpSUM10,PrecpSUM11,PrecpSUM12,PrecpSUM13,PrecpSUM14,PrecpSUM15,PrecpSUM16,PrecpSUM17,PrecpSUM18,PrecpSUM19,PrecpSUM20,PopDen,";
            TextRefresh();
            DicNN.Text = "ClEN";
        }

        private void positionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PropertiesDic["KeepAttributes"] = "PositionX,PositionY,Distant,Lag,";
            TextRefresh();
            DicNN.Text = "POS";
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            string detnn=TrainValidNum.Text;
            

            PropertiesDic["LowerNetworkBoundDengueID"] = detnn;
            string nnmstr = detnn;
            nnmstr = nnmstr.Replace(' ', '_');
            PropertiesDic["LowerNetworkSavedFile"] = "LNN" + nnmstr;
            PropertiesDic["SOMStandardlizeInformationFile"] = "SSTDZINF" + nnmstr;
            PropertiesDic["SelfOrganizingMapFile"] = "DSOM" + nnmstr;


            TextRefresh();
        }

        private void PredictionNum_TextChanged(object sender, EventArgs e)
        {
            PropertiesDic["PredictionBoundDengueID"] = PredictionNum.Text;
            string nnmstr = PredictionNum.Text;
            nnmstr = nnmstr.Replace(' ', '_');
            PropertiesDic["PredictionResultFile"] = "PrRS" + nnmstr;

            TextRefresh();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string dic_nn =Path.Combine( RootTextBox.Text , DicNN.Text);
            if (Directory.Exists(dic_nn)) { MessageBox.Show("is Created " + dic_nn); }
            else
            {
                Directory.CreateDirectory(dic_nn);
            }

            DicNN_TextChanged(sender, e);
        }

        private void modeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void trueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PropertiesDic["PollingFlag"] = "1";
            TextRefresh();
        }

        private void noPollingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PropertiesDic["PollingFlag"] = "0";
            TextRefresh();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            PropertiesDic = rdINI.relfectContainer(Prptext.Text);
            string dic_nn = Path.Combine( RootTextBox.Text , DicNN.Text);
            RunFunction runfunction = new RunFunction();
            if (trainCKBX.Checked) {
             
                //textBox1_TextChanged_1(0,EventArgs.Empty);

                    string LoadPredictionFlagBackup = PropertiesDic["LoadPredictionFlag"];
                    PropertiesDic["LoadPredictionFlag"] = "0";
                    TextRefresh();
                sbr.Text = "Now training!";
                runfunction.train(dic_nn, trainEXEText.Text, Prptext.Text,VSDTBX.Text);

                    PropertiesDic["LoadPredictionFlag"] = LoadPredictionFlagBackup;
                    TextRefresh();

                //this scope wr case by case
                SAVTXBX.Text= Path.Combine(dic_nn + "\\Train", PropertiesDic["SelfOrganizingMapFile"]);
                SAVTXBX.Text = SAVTXBX.Text.Trim();
                        //fix valid 
                        PropertiesDic["SelfOrganizingMapFile"] = Path.GetFileNameWithoutExtension(SAVTXBX.Text+".k");

                        string memPredictionBoundDengueID = PropertiesDic["PredictionBoundDengueID"];
                        string memPredictionResultFileName = PropertiesDic["PredictionResultFile"];
                        PropertiesDic["PredictionBoundDengueID"] = PropertiesDic["LowerNetworkBoundDengueID"];
                                            string nmpairprcs = PropertiesDic["LowerNetworkBoundDengueID"];
                        PropertiesDic["PredictionResultFile"] = "PrRS" + nmpairprcs.Replace(' ','_');
                            //sub Program
                                    if (PID != 0)
                                    {
                                        string[] sptttmp = RAnaRanage.Text.Split();
                                        if ( (!Microsoft.VisualBasic.Information.IsNumeric(sptttmp[0]) )|| (RAnaRanage.Text.Length==0) )
                                        {

                                        }
                                        else
                                        {
                                            PropertiesDic["PredictionBoundDengueID"] = RAnaRanage.Text;
                                            //analysis head tail
                                            if (VLbyH_T.Checked)
                                            {
                                                //string[] sptttmp = RAnaRanage.Text.Split();
                                                PropertiesDic["PredictionBoundDengueID"] = sptttmp[0] + " " + sptttmp[(sptttmp.Length - 1)];
                                                TextRefresh();
                                            }


                                        }
                                       
                                    }
                        TextRefresh();

                sbr.Text = "Now Valid!";
                runfunction.Prediction(false,dic_nn, ValPreText.Text, Prptext.Text, SAVTXBX.Text);

                        //this scope return original 
                        PropertiesDic["PredictionBoundDengueID"]= memPredictionBoundDengueID;
                        PropertiesDic["PredictionResultFile"]= memPredictionResultFileName;


                TextRefresh();

            }
            if (PredictCKBX.Checked) {
                sbr.Text = "Now Prediction!";
                string prtxt=PredictionNum.Text;
                prtxt=prtxt.Replace(' ', '_');
                            if (FuturePrediction.Checked==true) {
                                PropertiesDic["LoadPredictionFlag"] = "1";
                                TextRefresh();
                            }
                    
                if (SAVTXBX.Text.Length == 0)
                {
                    SAVTXBX.Text = Path.Combine(dic_nn + "\\Train", PropertiesDic["SelfOrganizingMapFile"]+".sav");
                }
                runfunction.Prediction(true,dic_nn, ValPreText.Text, Prptext.Text, SAVTXBX.Text, prtxt);

                /*
                //merge csv report file
                //catch result file
                string[]  mfils=Directory.GetFiles(folder_of_Prdiction, "*.csv");
                List<string> vfils=new List<string>();
                for(int i=0;i<mfils.Length; i++)
                {
                    if (mfils[i].Contains("PrRS"))
                    {
                        vfils.Add(mfils[i]);
                    }
                }
                
                MergeResult.mergeCSVResultByLine(Path.Combine(folder_of_Prdiction, "MergeReport.csv"), vfils);

                */


            }
            sbr.Text = "All Done!";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var folderPath = Path.Combine( RootTextBox.Text , DicNN.Text);
            if (Directory.Exists(folderPath))
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    Arguments = folderPath,
                    FileName = "explorer.exe"
                };
                Process.Start(startInfo);
            }
            else
            {
                MessageBox.Show(string.Format("{0} Directory does not exist!", folderPath));
            }



        }

        private void sOMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string epn=Microsoft.VisualBasic.Interaction.InputBox("SOM Epoch Numbers");
            PropertiesDic["SOMEpochNum"] = epn;
            TextRefresh();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            opSavDiag.ShowDialog();
            SAVTXBX.Text =Path.GetDirectoryName(opSavDiag.FileName)+"\\"+Path.GetFileNameWithoutExtension(opSavDiag.FileName);
            //PropertiesDic["SelfOrganizingMapFile"] = Path.GetFileNameWithoutExtension(SAVTXBX.Text + ".k");
            TextRefresh();
        }

        private void SAVTXBX_TextChanged(object sender, EventArgs e)
        {
            PropertiesDic["SelfOrganizingMapFile"] = Path.GetFileNameWithoutExtension(SAVTXBX.Text + ".k");
            TextRefresh();
        }

        private void FBDRT_HelpRequest(object sender, EventArgs e)
        {

        }

        private void mergeResultCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FBDRT.ShowDialog();
            string ffd=FBDRT.SelectedPath;


            //merge csv report file
            //catch result file
            string[] mfils = Directory.GetFiles(ffd, "*.csv");
            List<string> vfils = new List<string> ();
            for (int i = 0; i < mfils.Length; i++)
            {
                if (mfils[i].Contains("PrRS"))
                {
                    vfils.Add(mfils[i]);
                }
            }

            MergeResult.mergeCSVResultByLine(Path.Combine(ffd, "MergeReport.csv"), vfils);

        }

        private void mergeResultCSVManualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MergeCSVMnINT f2 = new MergeCSVMnINT();
            f2.ShowDialog();

        }

        private void extensionToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            PropertiesDic=rdINI.relfectContainer(Prptext.Text);
            TextRefresh();
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void setPercentageOfPercentageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string vlstr = Microsoft.VisualBasic.Interaction.InputBox("GetAllPairsPercentage 0.1~1.0 ");
            float vl = float.Parse(vlstr);
            if (vl<=0 || vl > 1.0)
            {
                MessageBox.Show("Error GetAllPairsPercentage 0.1~1.0 : " + vlstr);
            }
            else
            {
                PropertiesDic["GetAllPairsPercentage"]=vlstr;
            }
            TextRefresh();
               
        }

        private void epochToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void setTheSeedvarserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            opvarseed.ShowDialog();
            VSDTBX.Text = opvarseed.FileName;
        }

        private void Prptext_TextChanged(object sender, EventArgs e)
        {

        }

        private void trainCKBX_CheckedChanged(object sender, EventArgs e)
        {
            if (TrainValidNum.Text =="")
            {
                TrainValidNum.Text = PropertiesDic["LowerNetworkBoundDengueID"] ;
            }
            

        }

        private void PredictCKBX_CheckedChanged(object sender, EventArgs e)
        {
            if (PredictionNum.Text == "")
            {
                PredictionNum.Text = PropertiesDic["PredictionBoundDengueID"];
            }
        }

        private void DicNN_TextChanged(object sender, EventArgs e)
        {
            /*
            string pp=Path.Combine(RootTextBox.Text, DicNN.Text);
            if (Directory.Exists(pp))
            {
                for(int k=1;k<1000; k++)
                {

                    string konch = k.ToString("00000");
                  
                     string   nnnm=pp + konch;
                    if (!Directory.Exists(nnnm))
                    {
                       if( Microsoft.VisualBasic.Information.IsNumeric(DicNN.Text.Substring(DicNN.Text.Length - 5,5)))
                        {
                            DicNN.Text = DicNN.Text.Substring(0, DicNN.Text.Length - 5) + konch;
                        }
                        else
                        {
                            DicNN.Text = DicNN.Text + konch;
                        }
                        

                        break;
                    }
                }
            }
            */
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (int.Parse(RanaProp.Text) == 1)
            {
                trainCKBX.Checked = true;
                PredictCKBX.Checked = true;
                return;
            }
            var rand = new Random();
            if (RAnaRanage.Text == "") {
                MessageBox.Show("error range!");
                return; }
            RAnaRanage.Text=new System.Text.RegularExpressions.Regex("[\\s]+").Replace(RAnaRanage.Text, " ").Trim();
            string [] sayid=RAnaRanage.Text.Trim().Split(' ');
            int lb=int.Parse(sayid[0]), ub = int.Parse(sayid[1]);
            List<int> dengueid=new List<int>();
            float prporate = float.Parse(RanaProp.Text);
            if ( prporate<0 || prporate >= 1)
            {
                MessageBox.Show("Error Analysis Random");
                return;
            }
            StreamReader mfl = new StreamReader(PropertiesDic["DengueFactorForTrain"]);
            string ln="";
            mfl.ReadLine();
            int clc = 0;
            while((ln=mfl.ReadLine())!=null)
            {
                if (ln == "") break;
                //MessageBox.Show(ln);
                string[] ss=ln.Split(',');
                int id = int.Parse(ss[0]);
                clc += 1;
                if((id>=lb) && (id <= ub))
                {
                    dengueid.Add(id);
                    //MessageBox.Show(id.ToString());
                }

            }
            int forpred =(int) ((1.00-prporate) * (float)dengueid.Count);
            int fortrain = dengueid.Count - forpred;

            int cutlb = rand.Next(0, dengueid.Count - 1-forpred);
            int cutub = cutlb + forpred;

            //PredictionNum.Text = dengueid[cutlb].ToString() + " " + dengueid[cutub].ToString();
            TrainValidNum.Text = dengueid[0].ToString() + " " + dengueid[cutlb - 1].ToString() +" "+ dengueid[cutub+1].ToString() + " " + dengueid[dengueid.Count-1].ToString();
            //TrainValidNum.Text = dengueid[0].ToString() + " " + dengueid[dengueid.Count - 1].ToString();
            mfl.Close();



            trainCKBX.Checked = true;
            PredictCKBX.Checked = true;
        }
        public int CurrentRunPid = 1;
        private void button8_Click(object sender, EventArgs e)
        {
            Random rndtt = new Random();
            button6_Click( sender,  e);
            int totlrun = Int32.Parse(TestNumberText.Text);
            
            List<Thread> Rthread = new List<Thread>();
           // List<ProcessThread>=new
            for (int k=0; k< totlrun; k++)
            {



                Rthread.Add(new Thread(new ParameterizedThreadStart(wrk_multitrain)));
                List<string> ls = new List<string>();
                ls.Add(CurrentRunPid.ToString());//pid 
                ls.Add(Prptext.Text);  //prop
                ls.Add(RAnaRanage.Text); //rang
                ls.Add(RanaProp.Text);//rana prop
                ls.Add(RootTextBox.Text); //root
                ls.Add(DicNN.Text); //dict
                ls.Add(trainEXEText.Text);  //trainexe
                ls.Add(ValPreText.Text);  //validexe
                ls.Add(Dou_factortrain_ckb.Checked.ToString());//doufactor
                ls.Add(VLbyH_T.Checked.ToString());//validHeadTail
                ls.Add(FuturePrediction.Checked.ToString());//FuturePrediction
                Rthread[Rthread.Count-1].Start(ls);

                Thread.Sleep(rndtt.Next(1000,15000));

                if ((k+1) % Int32.Parse(ProcessNumberLable.Text)==0)
                {
                    for(int u=0; u < Rthread.Count; u++)
                    {
                        Rthread[u].Join();
                    }
                    Rthread.Clear();
                }
                CurrentRunPid += 1;
            }
            /*
            Thread t1= new Thread(new ParameterizedThreadStart( wrk_multitrain));
            List<string> ls = new List<string>();
            ls.Add(CurrentRunPid.ToString());//pid 
            ls.Add(Prptext.Text);  //prop
            ls.Add(RAnaRanage.Text); //rang
            ls.Add(RanaProp.Text);//rana prop
            ls.Add(RootTextBox.Text); //root
            ls.Add(DicNN.Text); //dict
            ls.Add(trainEXEText.Text);  //trainexe
            ls.Add(ValPreText.Text);  //validexe
                

            t1.Start(ls);
            
            t1.Join();
            */


            label15.Text = CurrentRunPid.ToString();

        }
        public void wrk_multitrain(object prp)
        {
            List<string> ls = (List<string>)prp;
            
            DengueInterface dcp = new DengueInterface(Int32.Parse(ls[0]));
            
            dcp.Show();

            //dcp.trainCKBX.Checked = true;
            //Thread.Sleep(100000);
            dcp.Prptext.Text = ls[1];
            dcp.button6_Click(null,null);
            //dcp.PropertiesDic = rdINI.rdinifl(ls[1]);
            dcp.RAnaRanage.Text=((string) ls[2]);     /* do  inter save valid*/ 
            dcp.RanaProp.Text = ls[3];
            dcp.button7_Click(null , null);

            dcp.RootTextBox.Text = ls[4];
            dcp.DicNN.Text = "Par"+ls[5]+ls[0];
            dcp.trainEXEText.Text = ls[6];
            dcp.ValPreText.Text = ls[7];
            dcp.Dou_factortrain_ckb.Checked = bool.Parse(ls[8]);
            dcp.VLbyH_T.Checked = bool.Parse(ls[9]);
            dcp.FuturePrediction.Checked = bool.Parse(ls[10]);
            if (dcp.PID == 1)
            { dcp.onSpatialTimePercFlagToolStripMenuItem_Click(null, null);
                                dcp.onToolStripMenuItem_Click(null, null);
            }

            if (dcp.Dou_factortrain_ckb.Checked)
            {
                dcp.enviromentAndClimateToolStripMenuItem_Click(null, null);
                dcp.DicNN.Text = "Par" + dcp.DicNN.Text + ls[0];
                dcp.button1_Click_1(null, null);
                dcp.positionToolStripMenuItem_Click(null, null);
                dcp.DicNN.Text = "Par" + dcp.DicNN.Text + ls[0];
                dcp.button1_Click_1(null, null);
            }
            else
            {
                dcp.button1_Click_1(null, null);
            }
            //MessageBox.Show("");
            // dcp.DicNN.Text = this.DicNN.Text;
            //MessageBox.Show("c[]");
            //return this;
           



        }

        private void paralleToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void setProcessNumberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string kval=Microsoft.VisualBasic.Interaction.InputBox("Process Number: ");
            
            ProcessNumberLable.Text= kval;
        }

        private void TestNumberText_TextChanged(object sender, EventArgs e)
        {

        }

        private void Dou_factortrain_ckb_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void setCurrentRunPIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string kkk =Microsoft.VisualBasic.Interaction.InputBox("Set Folder Pid ");
            CurrentRunPid = int.Parse(kkk);
            label15.Text = CurrentRunPid.ToString();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void setTrainExeToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void onSpatialTimePercFlagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PropertiesDic["SpatialTimePercFlag"] = "1";
            TextRefresh();
        }

        private void onToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PropertiesDic["HistogramPercFlag"] = "1";
            TextRefresh();
            
        }

        private void VSDTBX_TextChanged(object sender, EventArgs e)
        {

        }

        private void multiTableAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GraphLine GL = new GraphLine();
            GL.ShowDialog();
        }

        private void analysisToolToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FuturePrediction_CheckedChanged(object sender, EventArgs e)
        {
            if (FuturePrediction.Checked == true) PropertiesDic["LoadPredictionFlag"] = "1";
            else PropertiesDic["LoadPredictionFlag"] = "0";
            TextRefresh();
        }
    }
}
