#ifndef InterfaceSimplicity_hpp
#define InterfaceSimplicity_hpp
#include<string>
#include<tuple>
#include<vector>
#include<fstream>
#include<map>
#include"WeatherAggregateController.hpp"
#include"standardlizeTool.hpp"
#include"DengueMegaDataController.hpp"
#include"NeuronTree.hpp"
class ModelInformation{

    public:
    ModelInformation(const std::string& inidt);

    std::map<std::string,std::string> Dengue_ini;
    bool LoadPredictionFlag;
    std::string DengueFactorhcsvName;
    bool FactorControllerEnable;
    int EnviromentalUnit;
    std::vector<std::tuple<std::string,int,VectorAggregateSignal>>  WeatherTitleDayAggregate;
    std::vector<int> LowerNetworkBoundDengueID;
    //int UpperBoundDengueID;
                std::string LowerNetworkMode;
                int LowerNetworkFactorNumber;
                int LowerNetworkEpochNumber;
                    double LowerNetworkLagInf =7;
                    double LowerNetworkLagSup =50;
                    double LowerNetworkDistanceInf =0;
                    double LowerNetworkDistanceSup =1000;
                std::string LowerNetworkSavedFile ;
                float GetAllPairsPercentage =1.0;
        std::vector<std::string> KeepAttributes ;
        std::string OriginalSOMSavedMatrixFile ;
        bool InputSOMPositionFlag;
        bool InputSOMDirectionFlag;
        bool InputSOMDistanceFlag;
        bool InputSOMLagFlag;
        std::string SOMStandardlizeInformationFile ;
     int SOMOuterSize;
     int SOMEpochNum;
     int RMSECutUnit;
     bool SOMEpochShow ;
     bool SOMRMSEShow ;

     bool SOMUpdateShowEvery50k ;
     int SOMAutoSaveBackup;

     int SigmaNeighbouringInitial;
     int SigmaNeighbourhoodFinal;
     double LearningRateInitial;
     double LearningRateFinal;
     std::string SelfOrganizingMapFile;
     //std::string TopologyLineFileName;

     std::vector<int> PredictionBoundDengueID ;
         bool PollingFlag;
             std::string PredictionResultFile ;
             bool SpatialTimePercFlag ;
                std::string SpatialTimePercProperties ;
             bool HistogramPercFlag ;
                std::string HistogramPercProperties ;
             bool SOMPercFlag ;
                double SOM_LG_Axis=0.3;
                double SOM_DISTANCE_Axis=0.0;
                std::string SOMPercProperties ;
int CPUProcessorNum;




//choose
const std::string mdstrdbscan=("dbscan");
const std::string mdstrmcmcdenguetree=("mcmcdenguetree");
const std::string fullconnecteddenguetree=("fullconnected");



};

class FactorController{
protected:
    const DengueMegaDataControl& denguemegactrl;
    const std::vector<std::tuple<std::string,int,VectorAggregateSignal>>& WeatherTitleDayAggregate;
    const int EnviromentalUnit;
    //const ModelInformation& minf;
    void aggregateDatas();
public:
FactorController(const DengueMegaDataControl& dc,const std::vector<std::tuple<std::string,int,VectorAggregateSignal>>&  weathertitledayaggre,int eEunit_);

        std::map<int,std::vector<double>> FactorTable;
        std::vector<std::string> FactorTitle;

};

class SOMInputController{

protected:

bool FactorFLG__=false;

public:
bool PositionFlag=false;
bool DirectionFlag=true;
bool DistanceFlag=true;
bool LagFlag=true;
float GetAllPairsPercentage=1.0;
std::vector<std::string>SOMAttributeTitle;
std::vector<std::vector<double>>OriginalSOMInputMatrix;
std::vector<std::vector<double>>StandardLizeSOMInputMatrix;
std::vector<standardlizeInformation> StandardlizeInformationRecoder;
SOMInputController();
SOMInputController( FactorController* factrl, processTree* ltree,float getallp_perc=1.0,bool PF=false,bool DIRECF=true,bool DISF=true,bool LFG=true);
void ProcessInputData( FactorController* factrl, processTree* ltree);
void BatchStandardlize();
int size()const;
int findElementIndexbyTitle(const std::string& elestr)const;
bool deleteElementbyIndex(int del_idx);
bool keepBatch(const std::vector<std::string>& vecstr);




};

bool processTreeImportBatchInterface( DengueMegaDataControl* denguemegactrl, processTree* LowerTree,int factsz);


std::vector<int> generatePercentageRandomSeries(std::vector<int> sers);







#endif // InterfaceSimplicity_hpp
