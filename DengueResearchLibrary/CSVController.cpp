#include"CSVController.hpp"
#include"MatrixValueTool.hpp"
#include<sstream>
#include<algorithm>
#include<vector>
#include<string>
#include<iostream>
using namespace std;

const string  SpaceTag= "+00xx__SpACe__";
string CSVController::processSpcce(string sln)const{

for(int i=0;i<sln.size()-1;i++){
    //if(sln[i]==','&&sln[i+1]==',')sln.insert(i+1,SpaceTag);
    if(sln[i]==','){
        int bk=1;
        while(sln[i+bk]==' '&& (i+bk<sln.size())){
            bk++;
        }
        if(sln[i+bk]==',')  sln.insert(i+1,SpaceTag);
    }


//cout<<sln[i];
}

return sln;
}


vector<string> CSVController::splitTool(const string& str_)const{
    string str=str_;
    vector<string> rt;
    stringstream ss;

    str=processSpcce(str);

    replace(str.begin(),str.end(),',',' ');

    ss<<str;
    int dbg=0;
    while(ss>>str){
        if(str==SpaceTag)str="";
        rt.push_back(str);
    }

//cout<<"echo1"<<endl;
    return rt;
}



bool CSVController::read(std::ifstream& readfin){
    string tstrtlt;
    getline(readfin,tstrtlt);

    this->Titles_=splitTool(tstrtlt);


        string ateln;
        while(getline(readfin,ateln)){
                //cout<<"echo";

             Pool_.push_back(splitTool(ateln));

        }


isPreloded_=true;
return true;
}
bool CSVController::write(std::ofstream& writecsv)const {
    for(string stl: Titles_){
        writecsv<<stl<<",";
    }
    writecsv<<endl;

          for(vector<string> ln: Pool_){
            for(string prp:ln){
                 writecsv<<prp<<",";
            }
            writecsv<<endl;
          }

return true;
}



string CSVController::getStringData(int r_,int c_)const{
    if(!isPreloded_){cerr<<"no preload !"<<endl;return "0";}
    return Pool_[r_][c_];
}
double CSVController::getRealData(int r_,int c_)const{
    return STRconvert2Num(getStringData( r_, c_));
}

int CSVController::getIntData(int r_,int c_)const{
    string s=getStringData( r_, c_);

    stringstream ss;
    ss<<s;
    double vl;
    ss>>vl;

    return  (int)vl;
}

int CSVController::getCowIndex(const string& title)const{
    for(int h=0;h<Titles_.size();h++ ){
        if(Titles_[h]==title) return h;
    }
return -1;
}
vector<string> CSVController::getStringLine(int r_,int hpos_,int cnt_)const{
    if(r_>Pool_.size()||hpos_>Pool_.size()||cnt_+hpos_-1>Pool_[0].size());


    return vector<string>(Pool_[r_].begin()+hpos_,Pool_[r_].begin()+hpos_+cnt_);
}
vector<double> CSVController::getRealLine(int r_,int hpos_,int cnt_)const{
auto q= this->getStringLine(r_,hpos_,cnt_);
vector<double>rt;
for(auto r:q){
    rt.push_back(STRconvert2Num(r));
}
return rt;
}

std::vector<std::vector<std::string>>CSVController::getPool()const{
return Pool_;
}

int CSVController::rowsize()const{
    return Pool_.size();
}
int CSVController::colsize()const{
    return Titles_.size();
}


bool CSVController::insertColumn(int colid,string title,const std::vector<string>& insrts ){


    if(insrts.size()!= this->Pool_.size())return false;

        vector<string>::iterator titptr=Titles_.begin();
        advance(titptr,colid);
    Titles_.insert( titptr,title);
    //cout<<"t2"<<endl;

    vector<vector<string>>npool;

    //cout<<Pool_.size()<<endl;

    for(int i=0;i<Pool_.size();i++){
        vector<string> vln;
          for(int j=0;j<Pool_[0].size();j++){
             if(j==colid)vln.push_back(insrts[i]);
                vln.push_back(Pool_[i][j]);


            //cout<<"insrt"<<insrts[i]<<endl;
          }
          npool.push_back(vln);
    }


Pool_=npool;
return true;
}
 bool CSVController::insertRow(int rowid,const vector<std::string>& inserts){
     if(inserts.size()!=Titles_.size())return false;

     vector<vector<string>>ist2 ;
     ist2.push_back(inserts);
     vector<vector<string>>::iterator rit=Pool_.begin();
     advance(rit,rowid);
     Pool_.insert(rit,ist2.begin(),ist2.end());


 return true;
 }
void CSVController::clear(){
isPreloded_=false;
Pool_.clear();
Titles_.clear();


}
