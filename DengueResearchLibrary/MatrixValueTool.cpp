#include"MatrixValueTool.hpp"
#include<sstream>
#include<iostream>
#include<algorithm>
#include<map>
#include<cmath>
using namespace std;

string NUMconvert2STR(int iv){
    stringstream ss;
    ss<<iv;
    string tmpstr;
    ss>>tmpstr;

return tmpstr;
}
string NUMconvert2STR(double iv){
    stringstream ss;
    ss<<scientific<<iv;
    string tmpstr;
    ss>>tmpstr;

return tmpstr;
}

double STRconvert2Num(string nstr){
double rt;
stringstream ss;
ss<<nstr;
ss>>rt;
return rt;
}

std::vector<double> capatureColumnVector(const std::vector<std::vector<double>>& doumtrx,int cidx){

vector<double> rtrow;
rtrow.resize(doumtrx.size());

//check consistent
int zsz=doumtrx[0].size();
for(int i=0;i<doumtrx.size();i++){
   if (doumtrx[i].size()!=zsz){
    cerr<<"error capatureColumnVector"<<endl;
    exit(-1);
    return rtrow;
   }
rtrow[i]=doumtrx[i][cidx];
}



return rtrow;
}
bool writeMatrix(const std::vector<std::vector<double>>&mtrx,std::ofstream& fout){
//if(!fout.is_open()){return false;}
//cout<<"echo"<<endl;
for(const vector<double>& i:mtrx)
{
    for(double j:i){
        fout<<std::scientific<<j<<",";
    }
    fout<<endl;
}
//cout<<"echo2"<<endl;
return true;
}
bool readMatrix( std::vector<std::vector<double>>&mtrx,std::ifstream& fin){
if(!fin.is_open()){return false;}
string eat;double tmpdb;
    while(getline(fin,eat)){


        if(eat.empty())break;
        vector<double>arln;
        replace(eat.begin(),eat.end(),',',' ');
        stringstream ss;
        ss<<eat;
        while(ss>>tmpdb){
            arln.push_back(tmpdb);
        }
    mtrx.push_back(arln);
    }


return true;
}
bool readVector( std::vector<double>&vc,std::ifstream& fin){
    vc.clear();
    string eat;
    getline(fin,eat);
    stringstream ss;
    replace(eat.begin(),eat.end(),',',' ');
    ss<<eat;
    double rr;
    while(ss>>rr){
        vc.push_back(rr);
    }

 return true;
}
bool writeVector(const std::vector<double>&vc,std::ofstream& fout){
    for(auto r:vc)fout<<std::scientific<<r<<",";
    fout<<endl;

}
bool setingColumnVector(std::vector<std::vector<double>>& doumtrx,const std::vector<double>& lir,int indx){
if(doumtrx.size()!=lir.size())return false;
for(int u=0;u<doumtrx.size();u++){
    doumtrx[u][indx]=lir[u];


}

return true;
}
std::vector<double> operator*(double lhs,const std::vector<double>&rhs){

    vector<double> rtval;
for(int i=0;i<rhs.size();i++){
    double v=rhs[i]*lhs;
    rtval.push_back(v);
}
return rtval;
}
std::vector<double> operator+(const std::vector<double>& lhs,const std::vector<double>&rhs){
    if(lhs.size()!=rhs.size()){
            cerr<<"vector calculate size not same!"<<endl;
            for(auto r: lhs)cerr<<r<<","<<" ; ";
            for(auto r: rhs)cerr<<r<<","<<endl;
            vector<double> fl;
            fl.resize(lhs.size(),0);
            return fl;
    }

vector<double> rtval;
for(int i=0;i<lhs.size();i++){
    double v=lhs[i]+rhs[i];
    rtval.push_back(v);
}
return rtval;
}
std::vector<double> operator-(const std::vector<double>& lhs,const std::vector<double>&rhs){
    if(lhs.size()!=rhs.size()){
            cerr<<"vector calculate size not same!"<<endl;
            for(auto r: lhs)cerr<<r<<","<<" ; ";
            for(auto r: rhs)cerr<<r<<","<<endl;
    }
    vector<double> mrhs =(-1.0)*rhs;
return lhs+mrhs;
}

double norm(const std::vector<double>& lhs){

double sum=0.0;
for(auto v:lhs){
    sum+=(v*v);
}
return sqrt(sum);
}
std::vector<double> abs(const std::vector<double>& vc){
vector<double> rt=(vc);
for(int u=0;u<rt.size();u++){
    if(rt[u]<0)rt[u]*=-1;
}
return rt;
}
