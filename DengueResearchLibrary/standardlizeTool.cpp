#include"standardlizeTool.hpp"
#include<iostream>
#include<cmath>
#include<algorithm>
#include<sstream>
using namespace std;
double standardlizeInformation::recover(double v)const{
return v*stdev+mean;
}

double standardlizeInformation::standardlize(double v)const{
return (v-mean)/stdev;
}

void standardlizeInformation::save(ofstream& os)const{
    os<<mean<<","<<stdev<<endl;
}
void standardlizeInformation::load(ifstream& is){
    string eat;
    getline(is,eat);
    replace(eat.begin(),eat.end(),',',' ');
    stringstream ss;
    ss<<eat;
    ss>>mean;
    ss>>stdev;
}

double getMean(const std::vector<double>& dim){
double v=0.0;
for(double i:dim)v+=i;
return v/dim.size();
}

double getStddev(const std::vector<double>& dim){
double v=0.0;
double mm=getMean(dim);
for(double i:dim)v+=pow((i-mm),2);
v/=dim.size();
return sqrt(v);
}
vector<double> standardlizeSeries(const std::vector<double>& ser,const standardlizeInformation&inf){
vector<double> rt;
rt.resize(ser.size());
for(int i=0;i<ser.size();i++) rt[i]=inf.standardlize(ser[i]);
return rt;
}
vector<double> recoverSeries(const std::vector<double>& ser,const standardlizeInformation&inf){
vector<double> rt;
rt.resize(ser.size());
for(int i=0;i<ser.size();i++)rt[i]=inf.recover(ser[i]);
return rt;
}

