#include"ExtraControlSignal.hpp"
#include<algorithm>
#include<sstream>
#include<fstream>
using namespace std;


bool ExtraControlSignal::readExtraFile(std::string extrafn){
ifstream exfin(extrafn);
    if(!exfin.is_open())   return false;
    string eat;
    while(getline(exfin,eat)){
        stringstream ss;
        ss<<eat;
        string k,v;
        ss>>k;
        ss>>v;
        Infomations[k]=v;

    }


exfin.close();
return true;
}
ExtraControlSignal::ExtraControlSignal(std::string extrafilefpth_){

    this->ExtraFilePath=extrafilefpth_;
   isExist_= readExtraFile(ExtraFilePath);

}
std::string ExtraControlSignal::getExtraFilePath()const{
return ExtraFilePath;
}
bool ExtraControlSignal::isExist()const{
return isExist_;
}
std::string ExtraControlSignal::getInformation(std::string tag){
auto it =Infomations.find(tag);
if(it==Infomations.end())return "";
string consq=Infomations[tag];
return consq;
}
