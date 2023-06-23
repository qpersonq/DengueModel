#include<sstream>
#include<iostream>
#include<algorithm>
#include<random>
#include"InterfaceSimplicity.hpp"
#include"readDengueini.hpp"
#include"MatrixValueTool.hpp"
#include"SpatialTimeProcessor.hpp"
using namespace std;
using namespace spatialtime;

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

ModelInformation::ModelInformation(const std::string& inidt){
//cout<<inidt<<endl;
Dengue_ini=readDengueINI(inidt);

//cout<<Dengue_ini.size();
 //initial
    LoadPredictionFlag=(bool)STRconvert2Num(Dengue_ini["LoadPredictionFlag"]);
    if(LoadPredictionFlag)  DengueFactorhcsvName=Dengue_ini["DengueFactorForPrediction"];
    else                    DengueFactorhcsvName=Dengue_ini["DengueFactorForTrain"];

     FactorControllerEnable=(bool)STRconvert2Num(Dengue_ini["FactorControllerEnable"]);
     EnviromentalUnit=(int)STRconvert2Num(Dengue_ini["EnviromentalUnit"]);
    //Process W data



                {
                     stringstream wssbuff;
                     wssbuff<<Dengue_ini["WeatherAttributeTraceBackDayAndAggregate" ];
                     string itmstr;
                     while(wssbuff>>itmstr){
                        string uustr;
                        string sigstr;
                        wssbuff>>uustr;
                        wssbuff>>sigstr;
                        //tuple
                        WeatherTitleDayAggregate.push_back(
                        make_tuple(itmstr,STRconvert2Num(uustr),String2AggregateSignal(sigstr))
                        );
                     }
                }
     LowerNetworkBoundDengueID=splitString<int>(Dengue_ini["LowerNetworkBoundDengueID"]);
            /*
            {//LowerNetworkBoundDengueID
                   stringstream lnbdidss;
            lnbdidss<<Dengue_ini["LowerNetworkBoundDengueID"];
                        int vltmplndbidss;
                        while(lnbdidss>>vltmplndbidss){
                            LowerNetworkBoundDengueID.push_back(vltmplndbidss);
                        }
            }
            */


     //UpperBoundDengueID =(int)STRconvert2Num(Dengue_ini["UpperBoundDengueID"]);
     LowerNetworkMode = Dengue_ini["LowerNetworkMode"];
     LowerNetworkFactorNumber=(int)STRconvert2Num(Dengue_ini["LowerNetworkFactorNumber"]);
     LowerNetworkEpochNumber=(int)STRconvert2Num(Dengue_ini["LowerNetworkEpochNumber"]);


                     LowerNetworkLagInf =(double)STRconvert2Num(Dengue_ini["LowerNetworkLagInf"]);
                     LowerNetworkLagSup =(double)STRconvert2Num(Dengue_ini["LowerNetworkLagSup"]);
                     LowerNetworkDistanceInf =(double)STRconvert2Num(Dengue_ini["LowerNetworkDistanceInf"]);
                     LowerNetworkDistanceSup =(double)STRconvert2Num(Dengue_ini["LowerNetworkDistanceSup"]);


         LowerNetworkSavedFile=Dengue_ini["LowerNetworkSavedFile"];
         GetAllPairsPercentage =(float)STRconvert2Num(Dengue_ini["GetAllPairsPercentage"]);
         KeepAttributes=splitString<string>(Dengue_ini["KeepAttributes"]);
         OriginalSOMSavedMatrixFile= Dengue_ini["OriginalSOMSavedMatrixFile"];
         InputSOMPositionFlag=(bool)STRconvert2Num(Dengue_ini["InputSOMPositionFlag"]);
         InputSOMDirectionFlag=(bool)STRconvert2Num(Dengue_ini["InputSOMDirectionFlag"]);
         InputSOMDistanceFlag=(bool)STRconvert2Num(Dengue_ini["InputSOMDistanceFlag"]);
         InputSOMLagFlag=(bool)STRconvert2Num(Dengue_ini["InputSOMLagFlag"]);
         SOMStandardlizeInformationFile=Dengue_ini["SOMStandardlizeInformationFile"];

      SOMOuterSize=(int)STRconvert2Num(Dengue_ini["SOMOuterSize"]);
      SOMEpochNum=(int)STRconvert2Num(Dengue_ini["SOMEpochNum"]);
      RMSECutUnit=(int)STRconvert2Num(Dengue_ini["RMSECutUnit"]);
      SOMEpochShow =(bool)STRconvert2Num(Dengue_ini["SOMEpochShow"]);
      SOMRMSEShow = (bool)STRconvert2Num(Dengue_ini["SOMRMSEShow"]);
      SOMUpdateShowEvery50k=(bool)STRconvert2Num(Dengue_ini["SOMUpdateShowEvery50k"]);
      SOMAutoSaveBackup=(int)STRconvert2Num(Dengue_ini["SOMAutoSaveBackup"]);
      SigmaNeighbouringInitial=(int)STRconvert2Num(Dengue_ini["SigmaNeighbouringInitial"]);
      SigmaNeighbourhoodFinal=(int)STRconvert2Num(Dengue_ini["SigmaNeighbourhoodFinal"]);
      LearningRateInitial=(float)STRconvert2Num(Dengue_ini["LearningRateInitial"]);
      LearningRateFinal=(float)STRconvert2Num(Dengue_ini["LearningRateFinal"]);
      SelfOrganizingMapFile =Dengue_ini["SelfOrganizingMapFile"];
      //TopologyLineFileName=Dengue_ini["TopologyLineFileName"];


      //PredictionLowerBoundDengueID=STRconvert2Num( Dengue_ini["PredictionLowerBoundDengueID"]);
      //PredictionUpperBoundDengueID=STRconvert2Num( Dengue_ini["PredictionUpperBoundDengueID"]);
      PredictionBoundDengueID=splitString<int>(Dengue_ini["PredictionBoundDengueID"]);
                /*
               {//PredictionBoundDengueID
                   stringstream lnbdidss;
                lnbdidss<<Dengue_ini["PredictionBoundDengueID"];

                        int vltmplndbidss;
                        while(lnbdidss>>vltmplndbidss){
                            PredictionBoundDengueID .push_back(vltmplndbidss);
                        }
                }
                */
      PollingFlag =(bool)STRconvert2Num(Dengue_ini["PollingFlag"]);

              PredictionResultFile =Dengue_ini["PredictionResultFile"];
              SpatialTimePercFlag =(bool)STRconvert2Num(Dengue_ini["SpatialTimePercFlag"]);
                SpatialTimePercProperties= Dengue_ini["SpatialTimePercProperties"];
              HistogramPercFlag =(bool)STRconvert2Num(Dengue_ini["HistogramPercFlag"]);
                HistogramPercProperties= Dengue_ini["HistogramPercProperties"];
              SOMPercFlag =(bool)STRconvert2Num(Dengue_ini["SOMPercFlag"]);
                SOM_LG_Axis=(double)STRconvert2Num(Dengue_ini["SOM_LG_Axis"]);
                SOM_DISTANCE_Axis=(double)STRconvert2Num(Dengue_ini["SOM_DISTANCE_Axis"]);
                SOMPercProperties = Dengue_ini["SOMPercProperties"];

      CPUProcessorNum=(int)STRconvert2Num(Dengue_ini["CPUProcessorNum"]);




}




FactorController::FactorController(const DengueMegaDataControl& dc,const std::vector<std::tuple<std::string,int,VectorAggregateSignal>>&  weathertitledayaggre,int eEunit_):denguemegactrl(dc),WeatherTitleDayAggregate(weathertitledayaggre),EnviromentalUnit(eEunit_){
aggregateDatas();
}


void FactorController::aggregateDatas(){


                        bool isLoadTitle=false;

                        for(int u=0;u<denguemegactrl.rowsize();u++){

                               int fcfid_=(int)denguemegactrl.getRealData(u,denguemegactrl.getCowIndex("DengueFid"));
                                vector<double> dengtmp_;
                                for(int item=0;item<WeatherTitleDayAggregate.size();item++){
                                    string token_= get<0>(WeatherTitleDayAggregate[item]);
                                    int days_= get<1>(WeatherTitleDayAggregate[item]);
                                    VectorAggregateSignal vasig_=get<2>(WeatherTitleDayAggregate[item]);

                                    bool is_numb_flag=(days_>0);


                                    for(int ps=0;ps<  ((is_numb_flag==true)?(days_/EnviromentalUnit):1 );ps++){

                                        vector<double> curarr=(is_numb_flag==true) ?(denguemegactrl.getRealLine(u,denguemegactrl.getCowIndex(token_+NUMconvert2STR(ps+1)),EnviromentalUnit)):denguemegactrl.getRealLine(u,denguemegactrl.getCowIndex(token_),1);
                                        //for(auto a:curarr)cout<<a<<" ";
                                            if(!isLoadTitle)FactorTitle.push_back(token_+(is_numb_flag==true? ( VectorAggregateSignal2String(vasig_)+NUMconvert2STR(ps+1) )  :("")));
                                        dengtmp_.push_back(
                                                            VectorLineAggregate(curarr,vasig_)
                                                           );
                                    }


                                }


                        (FactorTable)[fcfid_]=dengtmp_;
                        isLoadTitle=true;

                        }//one person














}

SOMInputController::SOMInputController(){
;
}

SOMInputController::SOMInputController( FactorController* factrl, processTree* ltree,float getallp_perc,bool PF,bool DIRECF,bool DISF,bool LFG){
GetAllPairsPercentage=getallp_perc;
PositionFlag=PF;
DirectionFlag=DIRECF;
DistanceFlag=DISF;
LagFlag=LFG;
    if(factrl==nullptr)FactorFLG__=false;
    else FactorFLG__=true;
    if(ltree==nullptr){cerr<<"error SOMInputController ltree"<<endl;return;}

ProcessInputData(factrl,ltree);
}
void SOMInputController::ProcessInputData( FactorController* factrl, processTree* LowerTree){

//set Title of SOMNN
//vector<string>SOMAttributeTitle;
if(PositionFlag){SOMAttributeTitle.push_back("PositionX");SOMAttributeTitle.push_back("PositionY");}
if(DirectionFlag){SOMAttributeTitle.push_back("DirectionX");SOMAttributeTitle.push_back("DirectionY");}
if(DistanceFlag)SOMAttributeTitle.push_back("Distant");
if(LagFlag)SOMAttributeTitle.push_back("Lag");

if(FactorFLG__)SOMAttributeTitle.insert(SOMAttributeTitle.end(),factrl->FactorTitle.begin(),factrl->FactorTitle.end());
//Input SOM matrix
//vector<vector<double>>OriginalSOMInputMatrix;
    {

            vector<vector<int>> AllConnectedPair=LowerTree->getAllPair();

            //get by percentage

            //cout<<"ori:"<< AllConnectedPair.size()<<endl;
            //cout<<"GetAllPairsPercentage:"<<GetAllPairsPercentage<<endl;
                    if(GetAllPairsPercentage!=1.0){

                            vector<int> idxvec_for_perc;
                            for(int kk=0;kk<AllConnectedPair.size();kk++){
                                idxvec_for_perc.push_back(kk);
                            }
                            //open backup seed file
                            vector<int> kp_ser_idx;
                                        ifstream seedfin("RandomSeed.varser");
                                        if(seedfin.is_open()){
                                            string tlnx;
                                                getline(seedfin,tlnx);
                                            stringstream ss;

                                            ss<<tlnx;
                                            int ustmpint;
                                            while(ss>>ustmpint){
                                                kp_ser_idx.push_back(ustmpint);
                                            }


                                            seedfin.close();
                                        }
                                        else {
                                            seedfin.close();
                                            kp_ser_idx=generatePercentageRandomSeries(idxvec_for_perc);
                                            ofstream seedfout("RandomSeed.varser");
                                            for(int vlll:kp_ser_idx)seedfout<<vlll<<" ";
                                            seedfout.close();
                                        }






                            int kpsz_4kp_ser=GetAllPairsPercentage*idxvec_for_perc.size();
                            kp_ser_idx.resize(kpsz_4kp_ser);
                            sort(kp_ser_idx.begin(),kp_ser_idx.end());

                            vector<vector<int>> TMPACP;
                               for(int kk:kp_ser_idx){
                                TMPACP.push_back(AllConnectedPair[kk]);
                               }
                    AllConnectedPair=TMPACP;
                    }
             //cout<<"delp:"<< AllConnectedPair.size()<<endl;
             //for(auto r: AllConnectedPair) cout<<r[0]<<" , "<<r[1]<<endl;
             //cout<<"Pair num: "<<AllConnectedPair.size()<<endl;
            if(!AllConnectedPair.size()){ cerr<<"No Connected Pair! Cannot making SOMNN"<<endl; return ;}
            OriginalSOMInputMatrix.resize(AllConnectedPair.size());
                for(int g=0;g<AllConnectedPair.size();g++){
                    //Initialize And Prepare LR Case
                    //cout<<AllConnectedPair[g][0]<<","<<AllConnectedPair[g][1]<<endl;
                    const NeuronNode&
                    ldnn= LowerTree->getDengueCase( LowerTree->IDHashTable[AllConnectedPair[g][0]]),
                    rdnn= LowerTree->getDengueCase( LowerTree->IDHashTable[AllConnectedPair[g][1]]);
                    //Direction
                    //cerr<<"echo1"<<endl;
                    //cout<<"c: "<<ldnn.getCoordinate().X<<" "<<rdnn.getCoordinate().X<<endl;

                    vector<double> LRDIRCTION=getVector(ldnn.getCoordinate(),rdnn.getCoordinate());
                    //cerr<<"echo1.1"<<endl;
                    LRDIRCTION=  NormalizeVector( LRDIRCTION);
                    //cerr<<"echo1.2"<<endl;
                    //cout<<LRDIRCTION[0]<<LRDIRCTION[1]<<endl;
                    //Distant
                    double DIST2CASE=getDistant(ldnn.getCoordinate(),rdnn.getCoordinate());
                    //cerr<<"echo1.3"<<endl;
                    //Lag
                    double LAG2CASE=getDays(ldnn.getDate(),rdnn.getDate());
                    //cerr<<"echo2"<<endl;
                    //Infection Number
                    //double INFECTIONNUM=ldnn.forwardingCase.size();
                    //Position
                    //Coordinate POSITION=ldnn.getCoordinate();
                    //ENVIROMENT

                    if(PositionFlag){
                    OriginalSOMInputMatrix[g].push_back(rdnn.getCoordinate().X);
                    OriginalSOMInputMatrix[g].push_back(rdnn.getCoordinate().Y);
                    }
                    if(DirectionFlag){
                        OriginalSOMInputMatrix[g].push_back(LRDIRCTION[0]);
                        OriginalSOMInputMatrix[g].push_back(LRDIRCTION[1]);
                    }
                    if(DistanceFlag)OriginalSOMInputMatrix[g].push_back(DIST2CASE);
                    if(LagFlag)OriginalSOMInputMatrix[g].push_back(LAG2CASE);
                    //InputSOMNNMTRX[g].push_back(INFECTIONNUM);
                    if(FactorFLG__){
                        vector<double> fcvec =factrl->FactorTable[rdnn.getFid()];
                        OriginalSOMInputMatrix[g].insert( OriginalSOMInputMatrix[g].end(),fcvec.begin(),fcvec.end());
                    }
        }//end of for pair
    }//end of input som mtrx

    //Collect Garbage
    //delete denguemegactrl;
     for(int z=0;z<OriginalSOMInputMatrix[0].size();z++){
             vector<double>cvlvec= capatureColumnVector( OriginalSOMInputMatrix,z);
             StandardlizeInformationRecoder.push_back(standardlizeInformation(getMean(cvlvec),getStddev(cvlvec)));
      }

    BatchStandardlize();
/*
//cout<<"Loading Done! Number of Connected Pair : "<<OriginalSOMInputMatrix.size()<<endl;
    StandardLizeSOMInputMatrix=OriginalSOMInputMatrix;
    //vector<standardlizeInformation> StandardlizeInformationRecoder;
    {//InputSOMNNMTRX data STDLZ
        for(int z=0;z<StandardLizeSOMInputMatrix[0].size();z++){
             vector<double>cvlvec= capatureColumnVector( StandardLizeSOMInputMatrix,z);
             StandardlizeInformationRecoder.push_back(standardlizeInformation(getMean(cvlvec),getStddev(cvlvec)));
             for(int sg=0;sg<cvlvec.size();sg++)cvlvec[sg] =StandardlizeInformationRecoder.back().standardlize(cvlvec[sg]);
             setingColumnVector(StandardLizeSOMInputMatrix,cvlvec,z);
        }

    }//END OF InputSOMNNMTRX data STDLZ
*/

}
int SOMInputController::size()const{
return SOMAttributeTitle.size();
}
void SOMInputController::BatchStandardlize(){
StandardLizeSOMInputMatrix=OriginalSOMInputMatrix;
 {//InputSOMNNMTRX data STDLZ
        for(int z=0;z<StandardlizeInformationRecoder.size();z++){
             vector<double>cvlvec= capatureColumnVector( StandardLizeSOMInputMatrix,z);
             //StandardlizeInformationRecoder.push_back(standardlizeInformation(getMean(cvlvec),getStddev(cvlvec)));
             for(int sg=0;sg<cvlvec.size();sg++)cvlvec[sg] =StandardlizeInformationRecoder[z].standardlize(cvlvec[sg]);
             setingColumnVector(StandardLizeSOMInputMatrix,cvlvec,z);
        }

 }//END OF InputSOMNNMTRX data STDLZ


}


bool processTreeImportBatchInterface( DengueMegaDataControl* denguemegactrl, processTree* LowerTree,int factsz){
for(int ipr=0;ipr<denguemegactrl->rowsize();ipr++){

   LowerTree->importCase( NeuronNode(
                                    (int)denguemegactrl->getRealData(ipr,denguemegactrl->getCowIndex("DengueFid")),
                                                Coordinate{
                                                    "twd97",
                                                    denguemegactrl->getRealData(ipr,denguemegactrl->getCowIndex("TW97X")),
                                                    denguemegactrl->getRealData(ipr,denguemegactrl->getCowIndex("TW97Y"))
                                                },
                                    SmartParseDateString(denguemegactrl->getStringData(ipr,denguemegactrl->getCowIndex("OnsetDay"))),
                                    factsz,
                                        {
                                            (int)denguemegactrl->getRealData(ipr,denguemegactrl->getCowIndex("Oversea")),
                                            (int)denguemegactrl->getRealData(ipr,denguemegactrl->getCowIndex("Serotype"))
                                        }
                                    ));


}
return true;

}


int SOMInputController::findElementIndexbyTitle(const string& elestr)const{
    std::vector<std::string>::const_iterator smit=find(SOMAttributeTitle.begin(),SOMAttributeTitle.end(),elestr);
    if(  smit != SOMAttributeTitle.end()){
            return smit-SOMAttributeTitle.begin();

    }
return -1;
}
bool SOMInputController::deleteElementbyIndex(int del_idx){
    if(del_idx==-1){cerr<<" err deleteElementbyIndex del_idx==-1"<<endl;return false;}
    if(del_idx>=size()){cerr<<" err deleteElementbyIndex del_idx>="<<del_idx<<endl;return false;}

int CSZ=size();

SOMAttributeTitle.erase(SOMAttributeTitle.begin()+del_idx);
StandardlizeInformationRecoder.erase(StandardlizeInformationRecoder.begin()+del_idx);

for(int k=0;k<OriginalSOMInputMatrix.size();k++){
    OriginalSOMInputMatrix[k].erase(OriginalSOMInputMatrix[k].begin()+del_idx);
    StandardLizeSOMInputMatrix[k].erase(StandardLizeSOMInputMatrix[k].begin()+del_idx);
}



return true;
}
bool SOMInputController::keepBatch(const std::vector<std::string>& vecstr){

vector<int> allidxvec,keepidxvec;
for(int i=0;i<size();i++)allidxvec.push_back(i);
for(auto w:vecstr){
        int id=findElementIndexbyTitle(w);
        if(id!=-1)keepidxvec.push_back(id);
}
sort(keepidxvec.begin(),keepidxvec.end());
vector<int> opvec;opvec.resize(allidxvec.size());
vector<int>::iterator lit=set_difference(allidxvec.begin(),allidxvec.end(),keepidxvec.begin(),keepidxvec.end(),opvec.begin());
opvec.resize(lit-opvec.begin());
//cout<<allidxvec.size();
vector<string> contxvec;
for(int vl:opvec)contxvec.push_back(this->SOMAttributeTitle[vl]);
//rmv
        for(string idstx:contxvec){
            deleteElementbyIndex(findElementIndexbyTitle(idstx));
        }

return true;
}


vector<int> generatePercentageRandomSeries(vector<int> sers){

    std::default_random_engine generator(time(0));
    std::uniform_int_distribution<int> distribution(0,sers.size()-1);
    for(int i=0;i<(sers.size()*2);i++){
        int bx=distribution(generator);
        int fx=distribution(generator);
        swap_ranges(sers.begin()+bx,sers.begin()+bx+1,sers.begin()+fx);
    }
    /*int pp=perct*sers.size();
    sers.resize(pp);
    sort(sers.begin(),sers.end());*/
    return sers;
}
