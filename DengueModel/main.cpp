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
#include"InterfaceSimplicity.hpp"
#include"DengueMegaDataController.hpp"
#include"SpatialTimeProcessor.hpp"
#include"NeuronTree.hpp"
#include"MCMCDengueTree.hpp"
#include"DBSCANDengueTree.hpp"
#include"HistogramMap.hpp"
#include"MatrixValueTool.hpp"
#include"standardlizeTool.hpp"
#include"SelfOrganizingMap.hpp"
#include"FullConnectedDengueTree.hpp"
#include"DengueTreeOutputFile.hpp"
#include <boost/asio/thread_pool.hpp>
#include <boost/asio/post.hpp>
//#include"PredictionBuffer.hpp"

//#include"DynamicFullConnectedDengueTree.hpp"
using namespace std;
using namespace spatialtime;

int main()
{
    clock_t START_TIME,CREATETREE_TIME,END_TIME;START_TIME=clock();



   ModelInformation  ModelInf("DengueDiffusionModel.ini");
    cout<<"*** Dengue Transmission Chain Reinforce Learning ***"<<endl;
    for(auto k:ModelInf.Dengue_ini){
        cout<< k.first<<" "<<k.second<<endl;
    }
    //cout<<ModelInf.Dengue_ini.size();

//Loading Dengue

    DengueMegaDataControl* denguemegactrl;

    denguemegactrl=new DengueMegaDataControl(ModelInf.DengueFactorhcsvName,true);
    //Loading Training dengue id:
    for(int k=0;k<ModelInf.LowerNetworkBoundDengueID.size();k+=2){
         denguemegactrl->loading_(ModelInf.LowerNetworkBoundDengueID[k],ModelInf.LowerNetworkBoundDengueID[k+1]);
    }
/*
ofstream dbg("debug.csv");
for(auto k:denguemegactrl->getPool()){
    dbg<<k[0]<<endl;
}



dbg.close();
*/


vector<double>initRootProp__;initRootProp__.resize(ModelInf.LowerNetworkFactorNumber,0.0);

                processTree *LowerTree=nullptr;
                    if(ModelInf.LowerNetworkMode==ModelInf.mdstrdbscan){
                        LowerTree=new DBSCANDengueTree(ModelInf.LowerNetworkFactorNumber,initRootProp__);

                    }
                    else if(ModelInf.LowerNetworkMode==ModelInf.mdstrmcmcdenguetree){
                        LowerTree=new MCMCDengueTree(ModelInf.LowerNetworkFactorNumber,initRootProp__);
                    }
                    else if(ModelInf.LowerNetworkMode==ModelInf.fullconnecteddenguetree){
                        LowerTree=new FullConnectedDengueTree(ModelInf.LowerNetworkFactorNumber,initRootProp__);
                    }
                    else{
                        cerr<<"error process tree!"<<endl;return -1;
                    }


//Weather and spatial Data thread Loading
FactorController *factorCtrl=nullptr;
if(ModelInf.FactorControllerEnable) factorCtrl=new FactorController(*denguemegactrl,ModelInf.WeatherTitleDayAggregate,ModelInf.EnviromentalUnit);

                //for(auto y:FactorTitle)cout<<y<<",";
                    //cout<<denguemegactrl->getRealData(3,denguemegactrl->getCowIndex("Temperature10"))<<","<<denguemegactrl->getPool().size()<<endl;
//Loading to lowerNetwork

processTreeImportBatchInterface(  denguemegactrl,  LowerTree,ModelInf.LowerNetworkFactorNumber);


if(ModelInf.LowerNetworkMode==ModelInf.mdstrdbscan){
    cout<<"mdstrdbscan Tree Mode"<<endl;
        ((DBSCANDengueTree*)LowerTree )->LagInf=ModelInf.LowerNetworkLagInf;
        ((DBSCANDengueTree*)LowerTree )->LagSup=ModelInf.LowerNetworkLagSup;
        ((DBSCANDengueTree*)LowerTree )->DistanceSup=ModelInf.LowerNetworkDistanceSup;

   ((DBSCANDengueTree*)LowerTree )->DBSCAN();
   ((DBSCANDengueTree*)LowerTree )->PairControl_select_Flag=3;
   cout<<"Lower Tree of Connected Pairs : "<< ((DBSCANDengueTree*)LowerTree)->getAllPair().size() <<endl;
   //LowerTree->Save((ofstream&)cout);
}
else if(ModelInf.LowerNetworkMode==ModelInf.mdstrmcmcdenguetree){
    cout<<"mdstrmcmcdenguetree Tree Mode"<<endl;
    ((MCMCDengueTree*)LowerTree )->setEpoch(ModelInf.LowerNetworkEpochNumber);
    ((MCMCDengueTree*)LowerTree )->trainModel();
    cout<<"Lower Tree of Connected Pairs : "<< ((MCMCDengueTree*)LowerTree)->getAllPair().size() <<endl;

}
else if(ModelInf.LowerNetworkMode==ModelInf.fullconnecteddenguetree){
     cout<<"fullconnecteddenguetree Tree Mode"<<endl;
        ((FullConnectedDengueTree*)LowerTree )->LagInf=ModelInf.LowerNetworkLagInf;
        ((FullConnectedDengueTree*)LowerTree )->LagSup=ModelInf.LowerNetworkLagSup;
        ((FullConnectedDengueTree*)LowerTree )->DistanceSup=ModelInf.LowerNetworkDistanceSup;
    ((FullConnectedDengueTree*)LowerTree )->Connect();
cout<<"Lower Tree of Connected Pairs : "<< ((FullConnectedDengueTree*)LowerTree)->getAllPair().size() <<endl;

}
else{
    cerr<<"err mod type!"<<endl;
    exit(-1);
}
cout<<"Lower Tree Done! Write Lower Tree."<<endl;
    //write lower tree
    ofstream LowerTreeSaver(ModelInf.LowerNetworkSavedFile+".sav");
    LowerTree->Save(LowerTreeSaver);
    LowerTreeSaver.close();
    //write lower tree
    TreeWriter *treewrctrl =new TreeWriter(LowerTree);
    ofstream
     lt_node_wr(ModelInf.LowerNetworkSavedFile+"_nod.csv")
    ,lt_line_wr(ModelInf.LowerNetworkSavedFile+"_line.csv")
    ,lt_gpr_wr(ModelInf.LowerNetworkSavedFile+"_gpr.csv");

    treewrctrl->writePoint(lt_node_wr);
    treewrctrl->writeConnectLine(lt_line_wr);
    treewrctrl->writeGetPair(lt_gpr_wr);
    lt_node_wr.close();
    lt_line_wr.close();
    lt_gpr_wr.close();
    delete treewrctrl;
    if(ModelInf.LowerNetworkMode==ModelInf.mdstrdbscan){//write out dbscan layer
            ofstream dbscan_wr_acs(ModelInf.LowerNetworkSavedFile+"_dbscacs.csv");
            dbscan_wr_acs
            <<"OID,FID,GRP,LYR"<<endl;
            for(int vv=1;vv<((DBSCANDengueTree*)LowerTree )->ClusterRecoder.size() ;vv++){

                dbscan_wr_acs<<vv<<",";
                dbscan_wr_acs<<LowerTree->getDengueCase(vv).getFid()<<",";
                if(((DBSCANDengueTree*)LowerTree )->ClusterRecoder[vv].empty()){
                    dbscan_wr_acs<<-1<<",";
                    dbscan_wr_acs<<-1<<",";
                }
                else{
                    auto GL_= ((DBSCANDengueTree*)LowerTree )->ClusterRecoder[vv].back();
                    dbscan_wr_acs<<GL_.first<<",";
                    dbscan_wr_acs<<GL_.second<<",";
                }

                dbscan_wr_acs<<endl;
            }

    }

SOMInputController *somInputCtrl=new SOMInputController(factorCtrl,LowerTree,ModelInf.GetAllPairsPercentage,ModelInf.InputSOMPositionFlag,ModelInf.InputSOMDirectionFlag,ModelInf.InputSOMDistanceFlag,ModelInf.InputSOMLagFlag);
somInputCtrl->keepBatch(ModelInf.KeepAttributes);

delete factorCtrl;
delete LowerTree;

cout<<"Loading Done! Number of Connected Pairs : "<<somInputCtrl->OriginalSOMInputMatrix.size()<<endl;
cout<<"Input Data Title size: "<<somInputCtrl->OriginalSOMInputMatrix[0].size() <<" Titles : ";
for(auto r:somInputCtrl->SOMAttributeTitle){
    cout<<r<<",";
}cout<<endl;

ofstream stdinf_wr(ModelInf.SOMStandardlizeInformationFile+"_inf.csv" );
for(string stx: somInputCtrl->SOMAttributeTitle)stdinf_wr<<stx<<",";stdinf_wr<<endl;
writeMatrix(somInputCtrl->OriginalSOMInputMatrix,stdinf_wr);
for(standardlizeInformation& k :somInputCtrl->StandardlizeInformationRecoder)k.save(stdinf_wr);
writeMatrix(somInputCtrl->StandardLizeSOMInputMatrix,stdinf_wr);
stdinf_wr.close();
        CREATETREE_TIME=clock();
//self organize map start
cout<<endl<<" **Start Self Organizing Map** "<<endl;
ofstream SOMProcessRecoderWriter(ModelInf.SelfOrganizingMapFile+"_ProcRec.csv" );
    SelfOrganizingMap *som;
     som=new SelfOrganizingMap(ModelInf.SOMOuterSize,ModelInf.SOMOuterSize,somInputCtrl->size(),ModelInf.SigmaNeighbouringInitial ,ModelInf.SigmaNeighbourhoodFinal ,ModelInf.LearningRateInitial ,ModelInf.LearningRateFinal );
    ifstream SelfOMapFileRead(ModelInf.OriginalSOMSavedMatrixFile);
    if(SelfOMapFileRead.is_open()){
        cout<<"Load file : "<<ModelInf.OriginalSOMSavedMatrixFile<<endl;
         //som=new SelfOrganizingMap(ModelInf.SigmaNeighbouringInitial ,ModelInf.SigmaNeighbourhoodFinal ,ModelInf.LearningRateInitial ,ModelInf.LearningRateFinal );
        som->load(SelfOMapFileRead);
    SelfOMapFileRead.close();
    }
    else{

        cout<<"No Loding ! Open new SOM"<<endl;
        //return -1;
    }

        som->operator<<( somInputCtrl->StandardLizeSOMInputMatrix);
        som->setIterator(ModelInf.SOMEpochNum);
    //som->isShowIteratorNumber=ModelInf.SOMEpochShow;
    //som->is_TrainingUpdateShowEvery50k=ModelInf.SOMUpdateShowEvery50k;
    //som->setIterator(ModelInf.SOMEpochNum);
    while(som->getCurrentIteratorNumber()!=som->getIteratorNumber()){

        if(ModelInf.CPUProcessorNum==1)som->Train();
        else if(ModelInf.CPUProcessorNum>1) som->ParallelTrain(ModelInf.CPUProcessorNum);
        else {cerr<<"Err ModelInf.CPUProcessorNum !" <<endl;return -1;}


        //shower

        if(ModelInf.SOMEpochShow) {
            cout<<setw(4)<<setiosflags(ios::left)<<"Iter: "<<som->getCurrentIteratorNumber()
                <<",lrt: "<<som->LearningRate
                <<",sig: "<<som->SigmaNeighbouring
                <<",lbd: "<<som->lambda;

            SOMProcessRecoderWriter<<setw(4)<<setiosflags(ios::left)<<"Iter: "<<som->getCurrentIteratorNumber()
                <<",lrt: "<<som->LearningRate
                <<",sig: "<<som->SigmaNeighbouring
                <<",lbd: "<<som->lambda;

        }
            set<int> SOMRMSECompensetIterSet;
            for(int yy=1;yy<=10;yy++) SOMRMSECompensetIterSet.insert(yy);
        if(( SOMRMSECompensetIterSet.find(som->getCurrentIteratorNumber())!= SOMRMSECompensetIterSet.end() )||(ModelInf.SOMRMSEShow &&(!(som->getCurrentIteratorNumber()% ModelInf.RMSECutUnit) ))){
                double ccrmse=som->calculateRootMeanSquareError_Parallel(ModelInf.CPUProcessorNum);
                cout <<setw(4)<<setiosflags(ios::left)<<",RMSE: "<<ccrmse ;
                SOMProcessRecoderWriter <<setw(4)<<setiosflags(ios::left)<<",RMSE: "<<ccrmse ;
        }

            //auto backup
            if(!(som->getCurrentIteratorNumber()%ModelInf.SOMAutoSaveBackup)){
                ofstream backupfout(ModelInf.SelfOrganizingMapFile+".bku",ios_base::app );
                backupfout<<"Epoch,"<<som->getCurrentIteratorNumber()<<endl;
                som->save(backupfout);
                backupfout.close();
            }
        SOMProcessRecoderWriter<<endl;
        cout<<endl;
    }
SOMProcessRecoderWriter.close();
//end of training
cout<<"Now write the SOM Topology"<<endl;
    ofstream SelfOMapFileWriter(ModelInf.SelfOrganizingMapFile+".som");
    som->save(SelfOMapFileWriter);
    SelfOMapFileWriter.close();


cout<<"Now write the SOM Accessory Data"<<endl;
/*Generate SOM Accessory data*/
    //new Histogram
const int LG_ADD=1,DIS_ADD=10;
const int LG_LB=ModelInf.LowerNetworkLagInf,LG_UB=ModelInf.LowerNetworkLagSup,DIS_LB=0,DIS_UB=ModelInf.LowerNetworkDistanceSup;
HistorgramMap FullHistogram_Lag(LG_LB,LG_UB,LG_ADD),FullHistogram_Distance(DIS_LB,DIS_UB,DIS_ADD);
vector<vector< HistorgramMap> >LagHistgMp,DistHistgMp;LagHistgMp.resize(som->getXSize());DistHistgMp.resize(som->getXSize());
    for(int i=0;i<som->getXSize();i++){
            LagHistgMp[i].resize(som->getYSize());
            DistHistgMp[i].resize(som->getYSize());
        for(int j=0;j<som->getYSize();j++){
            LagHistgMp[i][j].set(LG_LB,LG_UB,LG_ADD);
            DistHistgMp[i][j].set(DIS_LB,DIS_UB,DIS_ADD);

        }
    }



boost::asio::thread_pool ActNNRCPool;
pair<int,int>* rc_actnn_pair_array=new pair<int,int>[somInputCtrl->StandardLizeSOMInputMatrix.size()];
for(int u=0;u<somInputCtrl->StandardLizeSOMInputMatrix.size();u++){
boost::asio::post(ActNNRCPool,[=](){

                       rc_actnn_pair_array[u] = som->findBestMatchUnit(somInputCtrl->StandardLizeSOMInputMatrix[u]);

                  });
};
ActNNRCPool.join();

set<pair<int,int>>ActiveNeuronRecoder;
for(int u=0;u<somInputCtrl->StandardLizeSOMInputMatrix.size();u++){

        //auto pos=som->findBestMatchUnit(somInputCtrl->StandardLizeSOMInputMatrix[u]);
        auto pos=rc_actnn_pair_array[u];
        DistHistgMp[pos.first][pos.second].import(somInputCtrl->OriginalSOMInputMatrix[u][2]);
        LagHistgMp[pos.first][pos.second].import(somInputCtrl->OriginalSOMInputMatrix[u][3]);
        FullHistogram_Distance.import(somInputCtrl->OriginalSOMInputMatrix[u][2]);
        FullHistogram_Lag.import(somInputCtrl->OriginalSOMInputMatrix[u][3]);
        ActiveNeuronRecoder.insert({pos.first,pos.second});
}
delete [] rc_actnn_pair_array;
//write date
    //TITLES
    //SOM
    //STDINF
    //FULL_HISTOGRAM_DIS
    //FULL_HISTOGRAM_LG
    //ACTNN_SET
    //ALL_NN_HISTOGRAM
        //dis
        //lg
ofstream SOMAccessoryWriter(ModelInf.SelfOrganizingMapFile+".sav");
for(string stx: somInputCtrl->SOMAttributeTitle)SOMAccessoryWriter<<stx<<",";SOMAccessoryWriter<<endl;
som->save(SOMAccessoryWriter);
for(int g=0;g<somInputCtrl->size();g++)somInputCtrl->StandardlizeInformationRecoder[g].save(SOMAccessoryWriter);
    FullHistogram_Distance.save(SOMAccessoryWriter);
    FullHistogram_Lag.save(SOMAccessoryWriter);
for(auto k:ActiveNeuronRecoder) SOMAccessoryWriter<<(k.first*som->getYSize())+k.second<<",";SOMAccessoryWriter<<endl;
for(int m=0;m<som->getXSize();m++){
    for(int n=0;n<som->getYSize();n++){
        DistHistgMp[m][n].save(SOMAccessoryWriter);
        LagHistgMp[m][n].save(SOMAccessoryWriter);
    }
}


SOMAccessoryWriter.close();


delete som;
delete somInputCtrl;

         //Time Processing
         END_TIME=clock();
         ofstream RECORDER_TIME("RunTimeRecording.txt");
         RECORDER_TIME<<"RUNTIME , "<<(float)(END_TIME-START_TIME)/CLOCKS_PER_SEC<<endl;
         RECORDER_TIME<<"SOMTIME , "<<(float)(END_TIME-CREATETREE_TIME)/CLOCKS_PER_SEC<<endl;
         RECORDER_TIME.close();
//system("pause");
return 0;
}


