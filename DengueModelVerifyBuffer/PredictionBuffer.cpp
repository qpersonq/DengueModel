

#include"PredictionBuffer.hpp"
#include<limits>
#include<algorithm>
#include<iostream>
using namespace std;
std::pair<int ,int>  FindActiveBestMatchUnit(const SelfOrganizingMap&som,const std::set<std::pair<int,int>>&an,const vector<double>&xv,const vector<int>&shield  ){
//som.getWeight()


if((xv.size())!=som.getInputVectorSize()){ cerr << "xv sz: "<<xv.size()<<"shield sz: "<<shield.size();cerr<<"findBestMatchUnit size error!"<<endl;return {0,0} ;};
if(an.empty()){cerr<<"Act neuron empty!"<<endl;return{0,0};}
double Score=numeric_limits<double>::max(), ScoreNow ;
int BestXMap=0, BestYMap=0;

            for (int RowMap=0;RowMap<som.getXSize();RowMap++){
                 for (int ColMap=0;ColMap<som.getYSize();ColMap++){
                     if(find(an.begin(),an.end(),(pair<int,int>){RowMap,ColMap})==an.end())continue;
                          vector<double> smvc=  som.getWeight(RowMap,ColMap);
                          vector<double>nxv=xv;
                        if(!shield.empty()){
                            for(int posss: shield){

                                nxv[posss]=smvc[posss];
                            }
                        }
                     ScoreNow = som.EvaluateDistance(smvc, nxv);
                     if (ScoreNow < Score){
                            Score = ScoreNow;
                            BestXMap=RowMap;
                            BestYMap=ColMap;
                     }
                 }
            }
return {BestXMap,BestYMap};

}
