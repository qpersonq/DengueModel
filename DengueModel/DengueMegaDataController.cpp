#include<algorithm>
#include<sstream>
#include<iostream>
#include"DengueMegaDataController.hpp"
#include"MatrixValueTool.hpp"
using namespace std;
vector<string> split__(const string& str_){
    string str=str_;
    vector<string> rt;
    stringstream ss;
    replace(str.begin(),str.end(),',',' ');
    ss<<str;
    while(ss>>str){
        rt.push_back(str);
    }
    return rt;
}

bool DengueMegaDataControl::createPyramid(){
    DengueMegaFilePointer.open(FilePath__);
    isCreatedPyramid_=true;
    string eat;
    long long pos=DengueMegaFilePointer.tellg();
    while(getline(DengueMegaFilePointer,eat)){

             eat.resize(eat.find(","));int id=(int)STRconvert2Num(eat);

             Pyramid_[id]=pos;
             //cout<<id<<","<<pos<<endl;
            pos=DengueMegaFilePointer.tellg();

    }
    DengueMegaFilePointer.close();
    return true;
}
DengueMegaDataControl::DengueMegaDataControl(const std::string&fn_,bool pyra):FilePath__(fn_){
    DengueMegaFilePointer.open(fn_);

    if(!DengueMegaFilePointer.is_open()){cerr<<"error open mega dengue data!"<<endl;exit(-1);}
    string tstrtlt;
    getline(DengueMegaFilePointer,tstrtlt);
    //cout<<tstrtlt<<endl;
    Titles_=split__(tstrtlt);
    DengueMegaFilePointer.close();
    if(pyra) createPyramid();
}
/*
DengueMegaDataControl::DengueMegaDataControl(const std::string&fn_,int lowboundid_,int upboundid_):DengueMegaDataControl(fn_,true){
loading_(lowboundid_,upboundid_);
}
*/


 bool DengueMegaDataControl::reset(){
 Pool_.clear();

 return true;
 }



bool DengueMegaDataControl::loading_(int lowboundid_,int upboundid_){
    if(!isCreatedPyramid_){cerr<<"no create pyramid of dengue mega data controller"<<endl;return false;}
    if(Pyramid_[upboundid_]-Pyramid_[lowboundid_]<0){cerr<<"bound error of mega!"<<endl;return false;}
    isPreloded_=true;
    DengueMegaFilePointer.open(FilePath__);
    DengueMegaFilePointer.seekg(Pyramid_[lowboundid_]);
    //cout<<DengueMegaFilePointer.tellg()<<endl;
    //ofstream debug2("debugxxx.csv");
    while(Pyramid_[upboundid_]>DengueMegaFilePointer.tellg()){
        string ateln;
        getline(DengueMegaFilePointer,ateln);
        //debug2<<ateln;
        //cout<<ateln<<endl;

        Pool_.push_back(split__(ateln));
        //cout<<split__(ateln).size()<<","<<Titles_.size()<<endl;
        if(Pool_[(Pool_.size()-1)].size()!=Titles_.size()){cerr<<"error preloading of long megacontrol!"<<endl;}
    }
    //final close interval
    DengueMegaFilePointer.seekg(Pyramid_[upboundid_]);
    //cout<<"kkk"<<Pyramid_[upboundid_];
    {
        string ateln;
        getline(DengueMegaFilePointer,ateln);
        //cout<<"x*x"<<ateln<<endl;
        Pool_.push_back(split__(ateln));
    }




    //debug2.close();
    //for pool add 1 problem bug
        //Pool_.pop_back();
    DengueMegaFilePointer.close();
return true;
}


string DengueMegaDataControl::getStringData(int r_,int c_)const{
    if(!isPreloded_){cerr<<"no preload DengueMegaDataControl!"<<endl;return "0";}
    return Pool_[r_][c_];
}
double DengueMegaDataControl::getRealData(int r_,int c_)const{
    return STRconvert2Num(getStringData( r_, c_));
}

int DengueMegaDataControl::getCowIndex(const string& title)const{
    for(int h=0;h<Titles_.size();h++ ){
        if(Titles_[h]==title) return h;
    }
return -1;
}
vector<string> DengueMegaDataControl::getStringLine(int r_,int hpos_,int cnt_)const{
    if(r_>Pool_.size()||hpos_>Pool_.size()||cnt_+hpos_-1>Pool_[0].size());


    return vector<string>(Pool_[r_].begin()+hpos_,Pool_[r_].begin()+hpos_+cnt_);
}
vector<double> DengueMegaDataControl::getRealLine(int r_,int hpos_,int cnt_)const{
auto q= this->getStringLine(r_,hpos_,cnt_);
vector<double>rt;
for(auto r:q){
    rt.push_back(STRconvert2Num(r));
}
return rt;
}

std::vector<std::vector<std::string>>DengueMegaDataControl::getPool()const{
return Pool_;
}

int DengueMegaDataControl::rowsize()const{
    return Pool_.size();
}
int DengueMegaDataControl::colsize()const{
    return Titles_.size();
}
DengueMegaDataControl::~DengueMegaDataControl(){
DengueMegaFilePointer.close();

}
