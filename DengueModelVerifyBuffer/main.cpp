#define _USE_MATH_DEFINES
#include<iostream>
#include<ctime>
#include<fstream>
#include<string>
#include<sstream>
#include<vector>
#include<algorithm>
#include<utility>
#include<iterator>
#include<thread>
#include<tuple>
#include<iomanip>
#include<set>
#include<cmath>

#include<direct.h>
#include <boost/asio/thread_pool.hpp>
#include <boost/asio/post.hpp>

#include"InterfaceSimplicity.hpp"
#include"DengueMegaDataController.hpp"
#include"SpatialTimeProcessor.hpp"
#include"NeuronTree.hpp"
#include"MCMCDengueTree.hpp"
#include"DBSCANDengueTree.hpp"

#include"MatrixValueTool.hpp"
#include"standardlizeTool.hpp"
#include"SelfOrganizingMap.hpp"
#include"HistogramMap.hpp"
#include"FullConnectedDengueTree.hpp"
#include"PredictionBuffer.hpp"

#include"DynamicFullConnectedDengueTree.hpp"

#include"SpatioTimeBufferCalculator.hpp"
#include"ExtraControlSignal.hpp"
#include"DayReportTools.hpp"

using namespace std;
using namespace spatialtime;

bool intergrateReport(const double Percentage,DynamicFullConnectedDengueTree* PredictionFullTree,
                       ofstream& wrOverall,ofstream& wrPoints,ofstream& wrDayReport,
                      ExtraControlSignal& ExtraCtrlSig,const int CPUProcessorNum,
                      bool isDisolve=true
                      );
template<class T> vector<T> splitString(const string& spc){
vector<T>rt;
T tmpvl;
string tmpstr=spc;
replace(tmpstr.begin(),tmpstr.end(),',',' ');
stringstream wssbuff;
wssbuff<<tmpstr;

while(wssbuff>>tmpvl){
    rt.push_back(tmpvl);
}
return rt;
}



int main()
{
bool isWriteFindTheBestMatchUnit=true;
clock_t START_TIME,POLLING_START_TIME=0,POLLING_END_TIME=0,END_TIME;START_TIME=clock();
   ModelInformation  ModelInf("DengueDiffusionModel.ini");
   ExtraControlSignal ExtraCtrlSig("ExtraControlSignal.ini");
    cout<<"*** Dengue Transmission Chain Verify ***"<<endl;
    for(auto k:ModelInf.Dengue_ini){
        cout<< k.first<<" "<<k.second<<endl;
    }

    cout<<"** Read Dengue Information saver file **"<<endl;
    ifstream DengueInformationSaverReader(ModelInf.SelfOrganizingMapFile+".sav");
    if(!DengueInformationSaverReader.is_open()){cerr<<"DengueInformationSaverReader err"<<endl;return -1;}
    vector<string> inputTitles;
        {
            string eatTitleTemp;
            getline(DengueInformationSaverReader,eatTitleTemp);
            inputTitles=splitString<string>(eatTitleTemp);
        }
    cout<<"Titles: ";
    for(auto k:inputTitles)cout<<k<<",";cout<<endl;



//self organize map start
cout<<" ** Load Self Organizing Map ** "<<endl;
    SelfOrganizingMap *som=new SelfOrganizingMap(ModelInf.SigmaNeighbouringInitial ,ModelInf.SigmaNeighbourhoodFinal ,ModelInf.LearningRateInitial ,ModelInf.LearningRateFinal );
    /*
    ifstream SelfOMapFileRead(ModelInf.SelfOrganizingMapFile);
    if(SelfOMapFileRead.is_open()){
        cout<<"Load file : "<<ModelInf.SelfOrganizingMapFile<<endl;
         som=new SelfOrganizingMap(ModelInf.SigmaNeighbouringInitial ,ModelInf.SigmaNeighbourhoodFinal ,ModelInf.LearningRateInitial ,ModelInf.LearningRateFinal );
    */
        som->load(DengueInformationSaverReader);
    /*SelfOMapFileRead.close();
    }
    else{
        cerr<<"err loding"<<endl;
        return -1;
    }*/

//Load std inf
cout<<" ** Load Standard Information ** "<<endl;
vector<standardlizeInformation>  currentStdInfVec;
for(int g=0;g<som->getInputVectorSize();g++){
    standardlizeInformation stdinf;
    stdinf.load(DengueInformationSaverReader);
    currentStdInfVec.push_back(stdinf);
}





cout<<" ** Load Full Histogram Information ** "<<endl;
//new Histogram
HistorgramMap FullHistogram_Distance,FullHistogram_Lag;
FullHistogram_Distance.load(DengueInformationSaverReader);
FullHistogram_Lag.load(DengueInformationSaverReader);

cout<<" ** Load Active Neuron Recode ** "<<endl;
set<pair<int,int>>ActiveNeuronRecoder;
        {
            vector<int>tmpacnn;
            string eatactnnln;
            getline(DengueInformationSaverReader,eatactnnln);
            tmpacnn=splitString<int>(eatactnnln);
            for(int u:tmpacnn){
                ActiveNeuronRecoder.insert({u/som->getYSize(),u%som->getYSize()});
            }

        }

cout<<" ** Load SOM Histogram  ** "<<endl;
       HistorgramMap  **LagHistgMp  , **DistHistgMp;
       LagHistgMp=new HistorgramMap*[som->getXSize()];
       DistHistgMp=new HistorgramMap*[som->getXSize()];
       for(int i=0;i<som->getXSize();i++){
        LagHistgMp[i]=new HistorgramMap[som->getYSize()];
        DistHistgMp[i]=new HistorgramMap[som->getYSize()];
       }
       for(int i=0;i<som->getXSize();i++){
        for(int j=0;j<som->getYSize();j++){
            DistHistgMp[i][j].load(DengueInformationSaverReader);
            LagHistgMp[i][j].load(DengueInformationSaverReader);
        }
       }

DengueInformationSaverReader.close();
/* ---------------------------------------start prediction----------------------------------------------------*/
cout<<"**** Start Prediction ****"<<endl;


DengueMegaDataControl* PredictionDengueMegaCtrl=new DengueMegaDataControl(ModelInf.DengueFactorhcsvName,true);
//cout<<ModelInf.PredictionBoundDengueID.size()<<endl;
//for(auto g:ModelInf.PredictionBoundDengueID)cout<<g<<",";cout<<endl;
for(int k=0;k<ModelInf.PredictionBoundDengueID.size();k+=2){
         PredictionDengueMegaCtrl->loading_(ModelInf.PredictionBoundDengueID[k],ModelInf.PredictionBoundDengueID[k+1]);

}

//cout<<denguemegactrl->getPool().size()<<endl;
                vector<double>initRootProp__;initRootProp__.resize(ModelInf.LowerNetworkFactorNumber,0.0);
FullConnectedDengueTree* PredictionFullTree=new DynamicFullConnectedDengueTree(ModelInf.LowerNetworkFactorNumber,initRootProp__);

FactorController* PredictionfactorCtrl=nullptr;
if(ModelInf.FactorControllerEnable)  PredictionfactorCtrl=new FactorController(*PredictionDengueMegaCtrl,ModelInf.WeatherTitleDayAggregate,ModelInf.EnviromentalUnit);
processTreeImportBatchInterface(  PredictionDengueMegaCtrl,PredictionFullTree  ,ModelInf.LowerNetworkFactorNumber);


SOMInputController *EditSomControl=new SOMInputController;
EditSomControl->SOMAttributeTitle={"PositionX","PositionY","DirectionX","DirectionY","Distant","Lag"};
EditSomControl->SOMAttributeTitle.insert(EditSomControl->SOMAttributeTitle.end(),PredictionfactorCtrl->FactorTitle.begin(),PredictionfactorCtrl->FactorTitle.end());
EditSomControl->OriginalSOMInputMatrix.resize(PredictionDengueMegaCtrl->rowsize());
//cout<<"key rsz:"<<PredictionDengueMegaCtrl->rowsize()<<endl;;
for(int k=0;k<(PredictionDengueMegaCtrl->rowsize());k++){
    vector<double> tmpprevec={PredictionDengueMegaCtrl->getRealData(k,PredictionDengueMegaCtrl->getCowIndex("TW97X")),PredictionDengueMegaCtrl->getRealData(k,PredictionDengueMegaCtrl->getCowIndex("TW97Y")),
                                0,0,
                                0,0
                             };
        vector<double> envfvec= PredictionfactorCtrl->FactorTable[PredictionDengueMegaCtrl->getRealData(k,PredictionDengueMegaCtrl->getCowIndex("DengueFid"))];
    tmpprevec.insert(tmpprevec.end(),envfvec.begin(),envfvec.end());
    EditSomControl->OriginalSOMInputMatrix[k]=tmpprevec;
}
/*
ofstream dbg("debg.csv");
writeMatrix(EditSomControl->OriginalSOMInputMatrix,dbg);
dbg.close();
*/
EditSomControl->StandardLizeSOMInputMatrix=EditSomControl->OriginalSOMInputMatrix;
EditSomControl->keepBatch(inputTitles);
        //check is equal
        if(EditSomControl->size()!= currentStdInfVec.size()){
            cerr<<"error sz EditSomControl : currentStdInfVec"<<endl;
            return -1;

        }
        bool erroutflg=false;
        for(int z=0;z<EditSomControl->size();z++){
            if(EditSomControl->SOMAttributeTitle[z]!= inputTitles[z]){
                cerr<<"z:"<<z<<",SOMAttributeTitle : inputTitles :: "<<EditSomControl->SOMAttributeTitle[z]<<" : "<<inputTitles[z]<<endl;
                erroutflg=true;
            }
        }
        if(erroutflg){
                return -1;

        }
EditSomControl->StandardlizeInformationRecoder=currentStdInfVec;
EditSomControl->BatchStandardlize();
/*
ofstream dbg("debg.csv");
for(auto g:EditSomControl->SOMAttributeTitle)dbg<<g<<",";dbg<<endl;
writeMatrix(EditSomControl->OriginalSOMInputMatrix,dbg);
writeMatrix(EditSomControl->StandardLizeSOMInputMatrix,dbg);
dbg.close();
*/
delete PredictionfactorCtrl;
ofstream* BestUnitsWriter=nullptr;
if(isWriteFindTheBestMatchUnit){
     BestUnitsWriter=new ofstream("WriteBestMatchUnit.csv");
     //wr bmu title
     (*BestUnitsWriter)<<"Cases\\Neurons,";
     for(int i=0;i<som->getXSize();i++){
        for(int j=0;j<som->getYSize();j++){
            (*BestUnitsWriter)<<i*som->getYSize()+j<<",";
        }
     }
     for(string tlt: EditSomControl->SOMAttributeTitle){
        (*BestUnitsWriter)<<tlt<<",";
     }
     (*BestUnitsWriter)<<endl;;
}


// if som flag initial;
    vector<int*>* RecordNeuronVector=new vector<int*>();

    HistorgramMap *PollingMapLag,*PollingMapDistance;
if(ModelInf.SOMPercFlag ){

                cout<<"** Start Polling! **"<<endl;
                        POLLING_START_TIME=clock();

                boost::asio::thread_pool PoolingTHPL(ModelInf.CPUProcessorNum);
                //Polling Model
                //HistorgramMap *PollingMapLag,*PollingMapDistance;
                int crszfornew=EditSomControl->StandardLizeSOMInputMatrix.size();
                    PollingMapLag=new HistorgramMap[crszfornew];
                    PollingMapDistance=new HistorgramMap[crszfornew];
                //double pl_lg_sup=50,pl_lg_inf=6,pl_dist_sup=1000;
                    for(int u=0;u<EditSomControl->StandardLizeSOMInputMatrix.size();u++){
                        int Curr_Fid_=PredictionFullTree->getDengueCase(u).getFid();


                            int *RealActiveNeuron =new int [som->getXSize()*som->getYSize()];
                            for(int ggg=0;ggg<som->getYSize()*som->getYSize();ggg++){
                                RealActiveNeuron[ggg]=0;
                            }

                            boost::asio::post(PoolingTHPL,[=](){
                                               vector<double>player  = EditSomControl->StandardLizeSOMInputMatrix[u];
                                               HistorgramMap his_lg(FullHistogram_Lag.getInfIntervelSet()),his_ds(FullHistogram_Distance.getInfIntervelSet());
                                               if(ModelInf.PollingFlag){

                                                //Polling
                                                //for(double gp=7;gp<pl_lg_sup;gp+=15){
                                                    //for(double gds=10;gds<pl_dist_sup;gds+=300){
                                                    vector<HistorgramMap> hislgtmp,hisdisttmp;
                                                    for(set<pair<int,int>>::iterator ac=ActiveNeuronRecoder.begin();ac!=ActiveNeuronRecoder.end();++ac){


                                                            //vector<double>player  =  {0,0,som->getWeight(ac->first,ac->second)[2],som->getWeight(ac->first,ac->second)[3]};
                                                            int dist_idx=EditSomControl->findElementIndexbyTitle("Distant");
                                                            int lag_idx=EditSomControl->findElementIndexbyTitle("Lag");
                                                            player[dist_idx]=som->getWeight(ac->first,ac->second)[dist_idx];
                                                            player[lag_idx]=som->getWeight(ac->first,ac->second)[lag_idx];


                                                            pair<int,int> bspos=FindActiveBestMatchUnit(*som,ActiveNeuronRecoder,player);
                                                            //record active som neurons
                                                            RealActiveNeuron[(bspos.first*som->getYSize() +bspos.second)]+=1;

                                                            hislgtmp.push_back(LagHistgMp[bspos.first][bspos.second]);
                                                            hisdisttmp.push_back(DistHistgMp[bspos.first][bspos.second]);
                                                            //his_lg=PDFPlus(his_lg,(LagHistgMp[bspos.first][bspos.second]));
                                                            //his_ds=PDFPlus(his_ds,(DistHistgMp[bspos.first][bspos.second]));
                                                    }
                                                       // }
                                                //}//end polling
                                                    his_lg=PDFPlusBatch(hislgtmp);
                                                    his_ds=PDFPlusBatch(hisdisttmp);

                                                }
                                                else{
                                                    pair<int,int> bspos=FindActiveBestMatchUnit(*som,ActiveNeuronRecoder,player,{EditSomControl->findElementIndexbyTitle("Distant"),EditSomControl->findElementIndexbyTitle("Lag")});
                                                    //record active som neurons
                                                            RealActiveNeuron[(bspos.first*som->getYSize() +bspos.second)]+=1;
                                                    his_lg=LagHistgMp[bspos.first][bspos.second];
                                                    his_ds=DistHistgMp[bspos.first][bspos.second];
                                                }

                                            PollingMapLag[u]=(his_lg);
                                            PollingMapDistance[u]=(his_ds);
                                            });//end pool


                                RecordNeuronVector->push_back(RealActiveNeuron);

                        }//end prediction

                PoolingTHPL.join();


                    POLLING_END_TIME=clock();

                cout<<"** Polling done! **"<<endl;




}//som polling macro
else{
    cout<<"**No Polling, because SOM flag = false **"<<endl;
}






 //wr nn match
                                if(isWriteFindTheBestMatchUnit){

                                        for(int k=0;k<RecordNeuronVector->size();k++){
                                                         (*BestUnitsWriter)<<k<<",";
                                                    int cntactnn=0;
                                                    //cout<<"xy * sz"<<som->getXSize()*som->getYSize();
                                                    vector<double> TotalWeight;TotalWeight.resize(som->getInputVectorSize());
                                                    for(int ggg=0;ggg<som->getXSize()*som->getYSize();ggg++){


                                                        // plus wei
                                                        if(RecordNeuronVector->at(k)[ggg]>0){
                                                            cntactnn+=RecordNeuronVector->at(k)[ggg];
                                                           TotalWeight=TotalWeight + (double)RecordNeuronVector->at(k)[ggg]*(som->getWeight(ggg/som->getYSize(),ggg %  som->getYSize()));
                                                        }
                                                        //wr actnn
                                                        (*BestUnitsWriter)<<RecordNeuronVector->at(k)[ggg]<<",";


                                                    }

                                                        for(int qq=0;qq<TotalWeight.size();qq++){
                                                            (*BestUnitsWriter)<<(TotalWeight[qq]/(double)(cntactnn))<<",";
                                                        }

                                                     (*BestUnitsWriter)<<endl;




                                        }


                                        //wr square

                                         (*BestUnitsWriter)<<endl;
                                          (*BestUnitsWriter)<<"Write Som Topology"<<endl;
                                        for(int sr=0;sr<som->getXSize();sr++){
                                            for(int sc=0;sc<som->getYSize();sc++){
                                                    int cnnt=0;
                                                    //cal
                                                    for(int iit=0;iit<RecordNeuronVector->size();iit++){
                                                        cnnt+=RecordNeuronVector->at(iit)[sr*som->getYSize()+sc];
                                                    }
                                                    (*BestUnitsWriter)<<cnnt<<",";

                                            }
                                            (*BestUnitsWriter)<<endl;
                                        }



                                }// end wr





//deal with junk
for(int w=0;w<RecordNeuronVector->size();w++){
    delete [] RecordNeuronVector->at(w);
}
delete RecordNeuronVector;





if(isWriteFindTheBestMatchUnit){
 BestUnitsWriter->close();
 delete BestUnitsWriter;
}







int Percentage_init_int=10,Percentage_end=100,Percentage_add_int=5;

ofstream wrOverallReport;

                if(ModelInf.SpatialTimePercFlag){
                                wrOverallReport.open(ModelInf.PredictionResultFile+"SpatialTimePerc.csv" );
                }
                else if(ModelInf.HistogramPercFlag){
                                wrOverallReport.open(ModelInf.PredictionResultFile+"HistogramPerc.csv" );
                }
                else if(ModelInf.SOMPercFlag ){
                                wrOverallReport.open(ModelInf.PredictionResultFile+"SOMPerc.csv" );
                }
                else{
                    cout<<"Do Nothing"<<endl;
                }


{// all of routine


           wrOverallReport<<" Percentage, ConnectedPair, Lag_lb, Lag_ub,  Distance_lb , Distance_ub, Count, AllCase , C/N , CR2T , OAT , OD , Day , AT , AT/Day, D "<<endl;
           //title info app
           vector<string> ModelNameArray={"FIX MOD","HIST MOD","SOM MOD"};if(ModelInf.SpatialTimePercFlag)cout<<ModelNameArray[0]<<endl;else if(ModelInf.HistogramPercFlag)cout<<ModelNameArray[1]<<endl;else if(ModelInf.SOMPercFlag)cout<<ModelNameArray[2]<<endl; else cout<<ModelNameArray[3]<<endl;
                      cout<<" Percentage, ConnectedPair, Lag_lb, Lag_ub,  Distance_lb , Distance_ub, Count, AllCase , C/N , CR2T , OAT , OD , Day , AT , AT/Day, D "<<endl;

         for(int p_int=Percentage_init_int;p_int<=Percentage_end;p_int+=Percentage_add_int){
         double Percentage=((double)p_int/(double)100.0);
         vector<vector<double>> BoundSeries;
         // here selector



                if(ModelInf.SpatialTimePercFlag){
                       p_int=100;

                           double opt_lg_lb=6;
                           double opt_lg_ub=23;
                           double opt_dist_lb =0;
                           double opt_dist_ub =300;

                                for(int u=1;u<PredictionFullTree->getDengueCaseSize();u++){

                                BoundSeries.push_back({opt_lg_lb,opt_lg_ub,opt_dist_lb,opt_dist_ub});
                                }
                                //title info file
                                //cout<<"**Fix 6-23 300 m Connect Spatial and Time**"<<endl;
                                //wrOverallReport.open((string)"Overall"+ModelInf.SpatialTimePercProperties);



                }
                else if(ModelInf.HistogramPercFlag){


                           double opt_lg_lb=FullHistogram_Lag.getInverseCDF( ModelInf.SOM_LG_Axis- (  ModelInf.SOM_LG_Axis    *Percentage) )    ;
                           double opt_lg_ub=FullHistogram_Lag.getInverseCDF( ModelInf.SOM_LG_Axis+ (1-ModelInf.SOM_LG_Axis)   *Percentage  )    ;
                           double opt_dist_lb =FullHistogram_Distance.getInverseCDF(ModelInf.SOM_DISTANCE_Axis- ( ModelInf.SOM_DISTANCE_Axis     *Percentage));
                           double opt_dist_ub =FullHistogram_Distance.getInverseCDF(ModelInf.SOM_DISTANCE_Axis+ ( (1-ModelInf.SOM_DISTANCE_Axis) *Percentage));

                                for(int u=1;u<PredictionFullTree->getDengueCaseSize();u++){

                                BoundSeries.push_back({opt_lg_lb,opt_lg_ub,opt_dist_lb,opt_dist_ub});
                                }
                                //title info file
                                //cout<<"**Histogram Connect Spatial and Time**"<<endl;
                                //wrOverallReport.open((string)"Overall"+ModelInf.HistogramPercProperties);



                }
                else if(ModelInf.SOMPercFlag ){

                                for(int u=1;u<PredictionFullTree->getDengueCaseSize();u++){
                                    double opt_lg_lc_lb=(PollingMapLag[u-1].getInverseCDF(ModelInf.SOM_LG_Axis- ( ModelInf.SOM_LG_Axis     *Percentage)    ));
                                    double opt_lg_lc_ub=(PollingMapLag[u-1].getInverseCDF(ModelInf.SOM_LG_Axis+ ( (1-ModelInf.SOM_LG_Axis) *Percentage)    ));
                                    double opt_dist_lc_lb=PollingMapDistance[u-1].getInverseCDF(ModelInf.SOM_DISTANCE_Axis- ( ModelInf.SOM_DISTANCE_Axis     *Percentage)    );
                                    double opt_dist_lc_ub=PollingMapDistance[u-1].getInverseCDF(ModelInf.SOM_DISTANCE_Axis+ ( (1-ModelInf.SOM_DISTANCE_Axis) *Percentage)    );
                                BoundSeries.push_back({opt_lg_lc_lb,opt_lg_lc_ub,opt_dist_lc_lb,opt_dist_lc_ub});
                                }
                                //title info file
                                //cout<<"**Self Organizing Map Connect Spatial and Time**"<<endl;
                                //wrOverallReport.open((string)"Overall"+ModelInf.SOMPercProperties);

                }
                else{
                    cout<<"Do Nothing"<<endl;
                }



         //run routine


           ((DynamicFullConnectedDengueTree*) PredictionFullTree)->setBound(BoundSeries);
            //cal report
            _mkdir("DayReport");
            _mkdir("CaseReport");
           ofstream wrPoints((string)"./CaseReport/Case"+to_string(Percentage)+".csv");
           wrPoints <<" Percentage, Fid, ConnectedPairNUM, PairFid, Date, CoorX, CoorY, Imported , LgInf, LgSup, Distance_lb , Distance_ub"<<endl;
           ofstream wrDayReport((string)"./DayReport/Day"+to_string(Percentage)+".csv");
           //wrDayReport<<"Date,AllPoints,Import,PredictedPoints,PointsPrecision,RadLb,RadUb,AllBufferArea_ND,TPBufferArea_ND,FPBufferArea_ND,AreaRecall_ND,AllBufferArea_D,TPBufferArea_D,FPBufferArea_D,AreaRecall_D,Density_ND,Density_D,MixF1_ND,MixF1_D"<<endl;
           intergrateReport(Percentage,
                             (DynamicFullConnectedDengueTree*) PredictionFullTree,
                             wrOverallReport,wrPoints,wrDayReport,
                             ExtraCtrlSig,
                             ModelInf.CPUProcessorNum);
            wrPoints.close();
            wrDayReport.close();
            }//percentage end
            wrOverallReport.close();
            //cout<<"r1"<<endl;
}

//cout<<"r2"<<endl;
//cout<<"som xsize "<<som->getXSize()<<endl;
if(ModelInf.SOMPercFlag ){

    for(int i=0;i<som->getXSize();i++){
     delete []  LagHistgMp[i];
     delete [] DistHistgMp[i];
    }
    delete [] LagHistgMp;delete [] DistHistgMp;

delete [] PollingMapLag;
delete [] PollingMapDistance;
}




//cout<<"r3"<<endl;
delete EditSomControl;
delete som;
         //Time Processing
         END_TIME=clock();
         ofstream RECORDER_TIME("RunTimeRecording.txt");
         cout<<"runt3"<<endl;
         RECORDER_TIME<<"RUNTIME , "<<(float)(END_TIME-START_TIME)/CLOCKS_PER_SEC<<endl;
         RECORDER_TIME<<"POLLINGTIME , "<<(float)(POLLING_END_TIME-POLLING_START_TIME)/CLOCKS_PER_SEC<<endl;
         RECORDER_TIME.close();
//system("pause");
return 0;
}



















//intergrate report

bool intergrateReport(const double Percentage,DynamicFullConnectedDengueTree* PredictionFullTree,
                       ofstream& wrOverall,ofstream& wrPoints,ofstream& wrDayReport,
                      ExtraControlSignal& ExtraCtrlSig,const int CPUProcessorNum,
                      bool isDissolve
                      )
{

    vector<vector<double>> Bound=PredictionFullTree->getBound();
      time_t BigStartTime_t=tm2time_t(PredictionFullTree->getDengueCase(1).getDate());
      time_t BigEndTime_t=tm2time_t(PredictionFullTree->getDengueCase(PredictionFullTree->getDengueCaseSize()-1).getDate());

          const int ADaySecond=86400;
          long double opt_lg=0,opt_dist=0; long double opt_area_T=0; int cunt_pt=0;
          long double opt_lg_ub=0,opt_lg_lb=0;
          long double opt_dist_ub=0,opt_dist_lb=0;
           //cal buffer


             SpatioTimeBufferCalculator sptbuffcalter;sptbuffcalter.setCoreNumber(CPUProcessorNum);if(ExtraCtrlSig.isExist()&& (ExtraCtrlSig.getInformation("BufferMode") == "Raster") )  sptbuffcalter.DissolveMode=1;

                   for(int u=1;u<PredictionFullTree->getDengueCaseSize();u++){
                    const double opt_lg_lc_lb=Bound[u][0];
                    const double opt_lg_lc_ub=Bound[u][1];
                    const double opt_dist_lc_lb=Bound[u][2];
                    const double opt_dist_lc_ub=Bound[u][3];
                    Coordinate tw97pos= PredictionFullTree->getDengueCase(u).getCoordinate();
                         cunt_pt++;
                         opt_lg+=((opt_lg_lc_ub-opt_lg_lc_lb)+1);
                         opt_lg_ub+=opt_lg_lc_ub;
                         opt_lg_lb+=opt_lg_lc_lb;

                         opt_dist+=(opt_dist_lc_ub-opt_dist_lc_lb+1);
                         opt_dist_ub+=opt_dist_lc_ub;
                         opt_dist_lb+=opt_dist_lc_lb;


                         time_t cur= tm2time_t(PredictionFullTree->getDengueCase(u).getDate());
                         time_t laglb=cur+(ADaySecond*opt_lg_lc_lb);
                         time_t lagub=cur+(ADaySecond*opt_lg_lc_ub);
                         int delday=calDayLag(laglb,lagub,BigStartTime_t,BigEndTime_t,ADaySecond);

                         opt_area_T+=((double)(((opt_dist_lc_ub-opt_dist_lc_lb)+0.1)*((opt_dist_lc_ub-opt_dist_lc_lb)+0.1)*acos(-1))*(delday));


                             //ending condiction time

                         sptbuffcalter.inputSpatioTimeBuffer(
                                                             cur,
                                                             (int)opt_lg_lc_lb,(int)opt_lg_lc_ub,tw97pos.X,tw97pos.Y,opt_dist_lc_ub,
                                                             BigEndTime_t
                                                            );

                   }
                   //cout<<"end perc ";
                   opt_lg/=(float)cunt_pt;opt_dist/=(float)cunt_pt;
                   opt_lg_ub/=(double)cunt_pt;
                   opt_lg_lb/=(double)cunt_pt;
                   opt_dist_ub/=(double)cunt_pt;
                   opt_dist_lb/=(double)cunt_pt;


            PredictionFullTree->clearConnection();
            (PredictionFullTree)->Connect(true);
            //calculate coverage
                    set<int>IDCounter;
                    for(int i=1;i<PredictionFullTree->getDengueCaseSize();i++){
                        for(int gfid:PredictionFullTree->getDengueCase(i).forwardingCase){
                        IDCounter.insert(gfid);
                        }
                    }
                    int PredictedCases=IDCounter.size();
            //conclude buffer

             tuple<int,double>  buffdislv_day_tarea= sptbuffcalter.calculateDissovleBufferSpatioTime(18);
             sptbuffcalter.calculateNODissovleBufferSpatioTime();
             //opt_area_T=get<1>(sptbuffcalter.calculateNODissovleBufferSpatioTime());
              // cout<<"geted buff"<<endl;
             vector<double>  wrprp ={
                                     Percentage,
                                     (double)PredictionFullTree->getAllPair().size(),
                                     opt_lg_lb,
                                     opt_lg_ub,
                                     opt_dist_lb,
                                     opt_dist_ub,
                                     (double)PredictedCases,
                                     (double)cunt_pt,
                                     ((double)PredictedCases/ (double)cunt_pt),
                                     ((double)PredictedCases/ (double)((opt_lg)*opt_dist*opt_dist*acos(-1)) ),
                                     (double)opt_area_T,
                                     ((double)PredictedCases/opt_area_T*1000000.0),
                                     (double) get<0>(buffdislv_day_tarea),
                                     (double) get<1>(buffdislv_day_tarea),
                                     ((double) get<1>(buffdislv_day_tarea)/(double) get<0>(buffdislv_day_tarea)),
                                     ((double) IDCounter.size()/(double) get<1>(buffdislv_day_tarea)*1000000),
                                     };


              writeVector(wrprp,(ofstream&)cout);
              writeVector(wrprp,wrOverall);
                //write days info
               vector<tuple<time_t,double,double,double>> TPSlices =getTruePositionSliceFromDynamicFullConnectedDengueTree(*((DynamicFullConnectedDengueTree*)PredictionFullTree));
                 SpatioTimeBufferCalculator TPbuffcalter;TPbuffcalter.setCoreNumber(CPUProcessorNum);if(ExtraCtrlSig.isExist()&& (ExtraCtrlSig.getInformation("BufferMode") == "Raster") )  TPbuffcalter.DissolveMode=1;
                for(const tuple<time_t,double,double,double>& ttp:TPSlices){
                    //TPbuffcalter.inputSpatioTimeBuffer(get<0>(ttp),get<1>(ttp),get<2>(ttp),get<3>(ttp));
                    TPbuffcalter.inputSpatioTimeBuffer(get<0>(ttp),0,0,get<1>(ttp),get<2>(ttp),get<3>(ttp),BigEndTime_t);
                }
                TPbuffcalter.calculateDissovleBufferSpatioTime(18);TPbuffcalter.calculateNODissovleBufferSpatioTime();

                  auto TPFP_D =refillTimesTuplesBubbles( tm2time_t(PredictionFullTree->getDengueCase(1).getDate()),
                                            tm2time_t(PredictionFullTree->getDengueCase(PredictionFullTree->getDengueCaseSize()-1).getDate()),
                                            ADaySecond,
                                            sptbuffcalter.StoreDissolveBufferAreaByTimeT
                                            );

                  auto TP_D =refillTimesTuplesBubbles( tm2time_t(PredictionFullTree->getDengueCase(1).getDate()),
                                            tm2time_t(PredictionFullTree->getDengueCase(PredictionFullTree->getDengueCaseSize()-1).getDate()),
                                            ADaySecond,
                                            TPbuffcalter.StoreDissolveBufferAreaByTimeT
                                            );

                  auto TPFP_ND =refillTimesTuplesBubbles( tm2time_t(PredictionFullTree->getDengueCase(1).getDate()),
                                            tm2time_t(PredictionFullTree->getDengueCase(PredictionFullTree->getDengueCaseSize()-1).getDate()),
                                            ADaySecond,
                                            sptbuffcalter.StoreNODissolveBufferAreaByTimeT
                                            );

                  auto TP_ND =refillTimesTuplesBubbles( tm2time_t(PredictionFullTree->getDengueCase(1).getDate()),
                                            tm2time_t(PredictionFullTree->getDengueCase(PredictionFullTree->getDengueCaseSize()-1).getDate()),
                                            ADaySecond,
                                            TPbuffcalter.StoreNODissolveBufferAreaByTimeT
                                            );


                DayReportController dayreportctrl
                                    (
                                    ((DynamicFullConnectedDengueTree*)PredictionFullTree),
                                    sptbuffcalter.InputTuples,
                                    TPFP_D,
                                    TP_D,
                                    TPFP_ND,
                                    TP_ND,
                                    tm2time_t(PredictionFullTree->getDengueCase(1).getDate()),
                                    tm2time_t(PredictionFullTree->getDengueCase(PredictionFullTree->getDengueCaseSize()-1).getDate())

                                    );
                dayreportctrl.writeReport(wrDayReport);


              //write point
                        for(int u=1;u<PredictionFullTree->getDengueCaseSize();u++)
                        {
                        wrPoints<<std::scientific<<Percentage<<","<<PredictionFullTree->getDengueCase(u).getFid()<<","
                                <<PredictionFullTree->getDengueCase(u).forwardingCase.size()<<",";
                        for(int vlfid:PredictionFullTree->getDengueCase(u).forwardingCase)wrPoints<<vlfid<<";";wrPoints<<",";
                        wrPoints<<convertTime2String(PredictionFullTree->getDengueCase(u).getDate()) <<",";
                        wrPoints<<std::scientific<<(PredictionFullTree->getDengueCase(u).getCoordinate().X)<<","<<(PredictionFullTree->getDengueCase(u).getCoordinate().Y)<<",";
                        wrPoints<<((bool)(PredictionFullTree->getDengueCase(u).AdditionInformation[0]))<<",";
                        wrPoints<<Bound[u][0]<<","<<Bound[u][1]<<","<<Bound[u][2]<<","<<Bound[u][3]<<",";
                        wrPoints<<endl;
                        }

return true;
}





