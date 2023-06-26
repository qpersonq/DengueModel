#include<iostream>
#include<ctime>
#include<string>
#include<vector>
#include<algorithm>
#include<thread>
#include<utility>
#include<thread>
#include<sstream>
#include"SpatialTimeProcessor.hpp"
#include"InterfaceTool.hpp"
#include"MatrixValueTool.hpp"
#include"standardlizeTool.hpp"
#include"WeatherFacade.hpp"
#include"ArcgisRasterProcessor.hpp"

using namespace std;
using namespace spatialtime;


int main()
{


    map<string,string> Dengue_ini=readDengueINI("DengueData.ini");
    //initial

    const string DengueOriginalFileName ((Dengue_ini["DengueOriginalFileName"]));

    const string WeatherStationCsv =Dengue_ini["WeatherStationCsv"];
    const string WeatherDictionary=Dengue_ini["WeatherDictionary"];
    const vector<pair<string,int>>  WeatherAttributeAndTraceBackDay=decodeWeatherAttributeAndTraceBackDay(Dengue_ini["WeatherAttributeAndTraceBackDay" ]);

    const string NdviRaster =Dengue_ini["NdviRaster"];
    const string PopulationDensityRaster =Dengue_ini["PopulationDensityRaster"];
    const int CPUProcessorNum=(int)STRconvert2Num(Dengue_ini["CPUProcessorNum"]);
    const string DengueFactorOutputFileName =Dengue_ini["DengueFactorOutputFileName"];
    const string DengueFactorBackupDictionary =Dengue_ini["DengueFactorBackupDictionary"];

    //Control Signal
    const int LowerBoundDengueID=STRconvert2Num(Dengue_ini["LowerBoundDengueID"]);
    const int UpperBoundDengueID=STRconvert2Num(Dengue_ini["UpperBoundDengueID"]);
    const int BatchDengueWriteSize=STRconvert2Num(Dengue_ini["BatchDengueWriteSize"]);


    //Make title
    vector<string>OutputTitle={"DengueFid","OnsetDay","TW97X","TW97Y","Oversea","Serotype"};

    //show situation
    cout<<"*** Dengue Transmission Chain Reinforce Learning ***"<<endl;
    for(auto a:Dengue_ini){

        cout<<a.first<<" "<<a.second<<endl;
    }
    cout<<endl;
//Backup path
system(("mkdir "+DengueFactorBackupDictionary).c_str());
cout<<endl;

//Loading Dengue
DengueOrginalData DengueOData(DengueOriginalFileName);

//make weather data table
Weatherfacade weatherController(WeatherStationCsv,WeatherDictionary);
    for(const pair<string,int> & cps: WeatherAttributeAndTraceBackDay){
        for(int q=1;q<=abs(cps.second);q++){
            int iiiivl= cps.second>0? q:-q;
            OutputTitle.push_back(cps.first+NUMconvert2STR(iiiivl ));
        }

    }


    /*
//make raster
ArcGISRasterGenerator NdviController(NdviRaster,false);
    OutputTitle.push_back("Ndvi");
ArcGISRasterGenerator PopulationDensityController(PopulationDensityRaster,false);
    OutputTitle.push_back("PopDen");
    vector<thread> rasterthreadpool;
    rasterthreadpool.push_back(thread([&](){
            NdviController.CreatePyrimid();
    }));
    rasterthreadpool.push_back(thread([&](){
            PopulationDensityController.CreatePyrimid();
    }));
    for(int t=0;t<rasterthreadpool.size();t++) rasterthreadpool[t].join();
*/

//show title id
cout<<"Title [id] . name: "<<endl;
    for(int v=0;v<OutputTitle.size();v++){
        cout<<"["<<v<<"]"<<OutputTitle[v]<<", ";
    }
cout<<endl;

//decide dengue id by preloading file
int preloadDengueId=LowerBoundDengueID;
{//output file manage
ifstream denguefactoroutputcheck;

    denguefactoroutputcheck.open(DengueFactorOutputFileName);
    if(!denguefactoroutputcheck.is_open()){

            denguefactoroutputcheck.close();
            ofstream newdengueoutput(DengueFactorOutputFileName);
            for(auto r:OutputTitle)newdengueoutput<<r<<",";newdengueoutput<<endl;
            newdengueoutput.close();
    }
    else{

            string eatbuffer;
            string bkbuffer;
            int countertl=0;
            while(getline(denguefactoroutputcheck,eatbuffer)){
                if(eatbuffer.empty()){break;}//safe exit
                bkbuffer=eatbuffer;
                countertl++;
            }
            if(countertl>1){//have data
                //eatbuffer.resize(find(eatbuffer.begin(),eatbuffer.end(),',')-eatbuffer.begin()-1);
                stringstream ss;

                //cout<<bkbuffer<<endl;
                ss<<bkbuffer;
                ss>>preloadDengueId;

                denguefactoroutputcheck.close();
            }
            else if(countertl==1){//only title
                denguefactoroutputcheck.close();
            }
            else{//nothing
                denguefactoroutputcheck.close();
                    ofstream newdengueoutput(DengueFactorOutputFileName);
                    for(auto r:OutputTitle)newdengueoutput<<r<<",";newdengueoutput<<endl;
                    newdengueoutput.close();
            }
    }
}

//Batch Process

int currentId=preloadDengueId;

while(currentId<UpperBoundDengueID){
    //full batch
    vector<vector<string>> wrprop;
    //control template
    vector<Coordinate> dlrveccor;
    vector<tm> dlrvectm;
    cout<<"\nLoad from " <<currentId+1 <<" to "<<currentId+BatchDengueWriteSize<<" : " ;
    for(int ul=currentId+1;ul<=(currentId+BatchDengueWriteSize)&&ul<=UpperBoundDengueID;ul++){

        int cid_=DengueOData.getRowIndexByID(ul);
        //cout<<cid_<<endl;
        if(cid_==-1)continue;

        vector<string> bhy;
        bhy.push_back(DengueOData.getSingleData(cid_,0));
        //tm onset
        bhy.push_back(DengueOData.getSingleData(cid_,1));
        dlrvectm.push_back(SmartParseDateString(DengueOData.getSingleData(cid_,1)));

        //coordinate
            CoordinateTransform *ctf=CoordinateTransform::getInstance();
            Coordinate coortmp=ctf->Cal_lonlat_To_twd97(STRconvert2Num(DengueOData.getSingleData(cid_,8)),STRconvert2Num(DengueOData.getSingleData(cid_,9)));
        dlrveccor.push_back(coortmp);
        bhy.push_back(NUMconvert2STR(coortmp.X));
        bhy.push_back(NUMconvert2STR(coortmp.Y));
        //oversea
        bool ovsea=(DengueOData.getSingleData(cid_,10)==string("是"))? true:false;
        bhy.push_back(NUMconvert2STR(ovsea) );
        //stereo
        string stereo="0";
        vector<string> stereotypetag={"第一型","第二型","第三型","第四型"};
            for(int k=0;k<stereotypetag.size();k++){
                if(stereotypetag[k]==DengueOData.getSingleData(cid_,12))
                stereo=NUMconvert2STR(k+1);
            }
        bhy.push_back(stereo);

    //for(auto r:bhy)cout<<r<<" ";    cout<<endl;
    wrprop.push_back(bhy);
    }
    //batch process
    cout<<"  Done! "<<endl;
    vector<vector<double>>WeatherTable=WeatherTableGetter(dlrveccor,dlrvectm,WeatherAttributeAndTraceBackDay,weatherController,CPUProcessorNum);


    //fuller writer append
        //DengueFactorBackupDictionary backup
        stringstream factorbackupss;
    ofstream appwrter;
        appwrter.open(DengueFactorOutputFileName,ios_base::app|ios_base::out);
        for(int y=0;y<wrprop.size();y++)
        {
            //wr dengue
            for(auto u1:wrprop[y]){
                appwrter<<u1<<",";
                factorbackupss<<u1<<",";
            }
            //wr weather
            for(auto wv:WeatherTable[y]){
                appwrter<<wv<<",";
                factorbackupss<<wv<<",";
            }

            //wr ndvi and popd
            /*
            double NDVI=NdviController.getValue({dlrveccor[y].X,dlrveccor[y].Y});
            double POPDEN=PopulationDensityController.getValue({dlrveccor[y].X,dlrveccor[y].Y});
            appwrter<<NDVI<<","<<POPDEN;
            factorbackupss<<NDVI<<","<<POPDEN;
            */

            appwrter<<endl;
            factorbackupss<<endl;


        }

    appwrter.close();
    writebackupbatch( DengueFactorBackupDictionary +'\\'+NUMconvert2STR(currentId+1)+"_denguefactor.csv",factorbackupss);

currentId+=BatchDengueWriteSize;
}


system("pause");
return 0;
}


