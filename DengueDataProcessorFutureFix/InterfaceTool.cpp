#include"InterfaceTool.hpp"
#include<sstream>
#include<fstream>
#include<algorithm>
#include<iostream>
#include<future>
#include<thread>
#include"CoordinateTransmationTWD97.hpp"
#include"MatrixValueTool.hpp"
#include"WeatherFacade.hpp"
using namespace std;
using namespace spatialtime;

std::map<std::string,std::string> readDengueINI(std::string inifn_){

map<string ,string> rtpss;
ifstream inifin(inifn_);
if(!inifin.is_open())return rtpss;

string tmpstr;
while(getline(inifin,tmpstr)){
    if(tmpstr.empty())break;
    stringstream tss;
    string istr,pstr,ustr;
    tss<<tmpstr;
    tss>>istr;
    while(tss>>ustr)pstr+=(ustr+" ");
    pstr.pop_back();
//cout<<istr<<" : "<<pstr<<endl;
    rtpss[istr]=pstr;

}

inifin.close();
if(!rtpss.size()){
    cerr<<"error load ini!"<<endl;
    exit(-1);
}


return rtpss;
}



std::vector<std::vector<double>>WeatherTableGetter(const std::vector<spatialtime::Coordinate>&veccor,const std::vector<std::tm>&vectm,const std::vector<std::pair<std::string,int>>&waabd,Weatherfacade& weatherController,const int QPROCNUM){

vector<vector<double>> wtbl;

{
    //cout<<"Unexist"<<endl;

//Weatherfacade weatherController(WeatherStationCsv,WeatherDictionary);


//decide cache

vector<int> decidedaychchevec;for(const pair<string,int>&wprp:waabd)decidedaychchevec.push_back(wprp.second);
int callBackDay=*max(decidedaychchevec.begin(),decidedaychchevec.end());
tm optstart= calculateTime(  vectm[1],(-1)*(60*60*24)/(callBackDay/28+1));
weatherController.OptimizeReader(optstart.tm_year+1900,optstart.tm_mon+1,16);


int veccorsz_=veccor.size();
wtbl.resize(veccorsz_);
//cout<<veccorsz_<<endl;
vector<thread> ctrlthreadvec;
cout<<"Weather Data Processing.(L:Load/F:Finish) : ";
    for(int i=0;i<veccorsz_;i++){
            cout<<(string)(NUMconvert2STR(i)+"L,");
            //calculate weather
            ctrlthreadvec.push_back(
            thread ([&,i](){
            //thread initial waabd wtbl

                for(const pair<string,int>&wprp:waabd){

                    for(int trb=0;abs(trb)<abs(wprp.second);trb= ((wprp.second)>=0 )? (trb+1): (trb-1)  ){
                            wtbl[i].push_back(
                            weatherController.getSingleByDay(wprp.first,veccor[i],calculateTime(vectm[i],(-60)*60*24*trb))
                            );
                    }
                 }
            string say(NUMconvert2STR(i)+"F,");
            cout<< say;
            }));

             if(((i%QPROCNUM==0)&&(i!=0)) ||(i==(veccor.size()-1))){
                for(thread & ti:ctrlthreadvec)ti.join();
                ctrlthreadvec.clear();
             }
//end of weather
    }







}
//cout<<"Weather Data Loading Done!"<<endl;
return wtbl;
}


std::vector<std::pair<std::string,int>> decodeWeatherAttributeAndTraceBackDay(const std::string& waatbd){
 vector<pair<string,int>> WeatherAttributeAndTraceBackDay;
{


         stringstream wssbuff;
         wssbuff<<waatbd;
         string itmstr;
         while(wssbuff>>itmstr){
            string uustr;
            string sigstr;
            wssbuff>>uustr;
            //pair
            WeatherAttributeAndTraceBackDay.push_back(
                pair<string,int>{itmstr,STRconvert2Num(uustr)}
            );
            //aggregateString2AggregateSignal

         }
    }
return WeatherAttributeAndTraceBackDay;
}
//dengue data processor

int DengueOrginalData::getRowIndexByID(int id)const{
int rtval=-1;
for(int i=0;i<size();i++){
     int cid=  STRconvert2Num(getSingleData(i,0)) ;
     //cout<<getSingleData(i,0)<<endl;
    if(cid==id){rtval=i;break;}
}

return rtval;
}
DengueOrginalData::DengueOrginalData(const std::string& fn_){
         ifstream dodfin_(fn_);
    if(!dodfin_.is_open())cerr<<"error open dengue original file"<<endl;

    string buff;
    getline(dodfin_,buff);
    replace(buff.begin(),buff.end(),',',' ');
    stringstream ss;
    ss<<buff;
    while(ss>>buff){
        Titles.push_back(buff);
    }
    while(getline(dodfin_,buff)){
        prop_.push_back(buff);

    }
dodfin_.close();
}
std::string DengueOrginalData::getDataline(int ln)const{
    return prop_[ln];
}
string  DengueOrginalData::getSingleData(int r_,int c_)const{
stringstream ss;
string rpstr=getDataline(r_);

replace(rpstr.begin(),rpstr.end(),',',' ');
//cout<<rpstr<<endl;
ss<<rpstr;
if(c_>=Titles.size()){cerr<<"DengueOrginalData::getSingleData err!"<<endl;return "";}
string buffe;

for(int qu=0;qu<=c_;qu++){
    ss>>buffe;
}
//cout<<rpstr<<endl;
return buffe;
}
int DengueOrginalData::size()const{return prop_.size();}
int DengueOrginalData::getColIndexByAttribute(const std::string& atr)const{
return find(Titles.begin(),Titles.end(),atr)-Titles.begin();
}
bool writebackupbatch(const string &fn_,std::stringstream&bat_){
ofstream fout(fn_);
if(!fout.is_open()){cerr<<"error backup "+fn_<<endl;return false;}
string lntmp;
while(getline(bat_,lntmp)){
    fout<<lntmp<<endl;

}


fout.close();


return true;
}
