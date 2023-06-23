#include"NeuronTree.hpp"
#include<iostream>
#include<assert.h>
#include<sstream>
#include<ctime>
#include<cmath>
#include<vector>
#include<fstream>
#include<queue>
#include<thread>
#include"SpatialTimeProcessor.hpp"
#include"MatrixValueTool.hpp"
using namespace spatialtime;
using namespace std;
NeuronNode::NeuronNode(int fn_):Neuron(fn_){
    ;
}



NeuronNode::NeuronNode(int fid_,const Coordinate& cord_,const tm& dt_,int fn_):Neuron(fn_){
this->Fid=fid_;
this->coordinate=cord_;
this->date=dt_;
this->FactorNumber=fn_;
}

NeuronNode::NeuronNode(int fid_,const spatialtime::Coordinate& cord_,const tm& dt_,int fn_,const std::vector<double>& adit_inf):Neuron(fn_),AdditionInformation(adit_inf){
this->Fid=fid_;
this->coordinate=cord_;
this->date=dt_;
this->FactorNumber=fn_;
}

int NeuronNode::getFid()const{
  return this->Fid;
}
neuron NeuronNode::getCurrentNeuron()const{
return  Neuron;
}

bool NeuronNode::setCurrentNeuron(const neuron& scn_){
     Neuron=scn_;
     return true;
}

spatialtime::Coordinate NeuronNode::getCoordinate()const{
    return this->coordinate;
}
std::tm NeuronNode::getDate()const{
    return this->date;
}

bool writeline(std::ofstream& fout,const string&prop ){
    fout<<prop<<endl;
    return true;
}
string mergeStringBySpace(const vector<string>&pstr ){
    string rtstr;
for(auto s_:pstr)rtstr+=(s_+" ");
return rtstr;
}
   /*1int Fid;
     2int FactorNumber;
     3spatialtime:: Coordinate coordinate;
     4std::tm date;
     5neuron Neuron;
     6std::vector<int> forwardingCase;
     7std::vector<int> backwardingCase;
     8std::vector<double> AdditionInformation;*/



bool NeuronNode::Load(std::ifstream&fin){
    string tmpstr;
    {//1Fid
        getline(fin,tmpstr);
        stringstream ss;ss<<tmpstr;
        ss>>Fid;
    }
    {//2FactorNumber
        getline(fin,tmpstr);
        stringstream ss;ss<<tmpstr;
        ss>>FactorNumber;
    }
    {//3coordinate
        getline(fin,tmpstr);
        stringstream ss;ss<<tmpstr;
        ss>>coordinate.CoordinateType;
        ss>>coordinate.X;
        ss>>coordinate.Y;
    }
    {//4date
        getline(fin,tmpstr);
        date=SmartParseDateString(tmpstr);

    }
    {//5Neuron
        getline(fin,tmpstr);
        stringstream ss;ss<<tmpstr;
        vector<double> uuvec;
        double tvl;
        while(ss>>tvl){
             uuvec.push_back(tvl);
        }
        Neuron.weight=uuvec;

    }
    {//6forwardingCase
        getline(fin,tmpstr);
        stringstream ss;ss<<tmpstr;
        forwardingCase.clear();
        int tvl;
        while(ss>>tvl){
            forwardingCase.push_back(tvl);
        }

    }
    {//7backwardingCase
        getline(fin,tmpstr);
        stringstream ss;ss<<tmpstr;
        backwardingCase.clear();
        int tvl;
        while(ss>>tvl){
            backwardingCase.push_back(tvl);
        }

    }
    {//8AdditionInformation
        getline(fin,tmpstr);
        stringstream ss;ss<<tmpstr;
        AdditionInformation.clear();
        double tvl;
        while(ss>>tvl){
            AdditionInformation.push_back(tvl);
        }

    }
return true;
}
bool NeuronNode::Save(std::ofstream&fout)const{
    writeline(fout,NUMconvert2STR(Fid));
    writeline(fout,NUMconvert2STR(FactorNumber));
    writeline(fout,mergeStringBySpace({coordinate.CoordinateType,NUMconvert2STR(coordinate.X),NUMconvert2STR(coordinate.Y)}));
    fout<<convertTime2String(date)<<endl;
    for(int q=0;q<Neuron.weight.size();q++)fout<<std::scientific <<Neuron.weight[q]<<" ";fout<<endl;
    for(int g :forwardingCase)fout<<g<<" ";fout<<endl;
    for(int g :backwardingCase)fout<<g<<" ";fout<<endl;
    for(int g :AdditionInformation)fout<<std::scientific<<g<<" ";fout<<endl;
    fout<<std::defaultfloat;
    return true;
}





std::vector<double> processTree::getDirectionVectorSum(int dcase)const{
    vector<std::vector<int>>conntbl =DengueCaseSet[dcase].getPairs();
    vector<double> vc;vc.resize(2,0);
    for(const vector<int>&par  : conntbl){
        Coordinate
        lc=        DengueCaseSet[par[0]].getCoordinate(),
        rc=        DengueCaseSet[par[1]].getCoordinate();

        vc[0]+=(rc.X-lc.X);
        vc[1]+=(rc.Y-lc.Y);

    }

return vc;
}
vector<std::vector<int>> NeuronNode::getPairs(bool FordwardPair)const{
vector<std::vector<int>> rtmtx;
        for(int iv :  (FordwardPair==true?forwardingCase:backwardingCase)    ){
            rtmtx.push_back(vector<int>{Fid,iv});
        }

return rtmtx;
}
int NeuronNode::getCentroSize()const {
return forwardingCase.size();
}



processTree::processTree(int atrbsz,const std::vector<double>& atrprp):AttributeSize(atrbsz){
    //set root neuron
    neuron rootdfn(AttributeSize);
    IDHashTable[0]=0;
    for(int i=0;i<rootdfn.weight.size();i++){
        rootdfn.weight[i]=atrprp[i];
    }


    Coordinate rcoordfn={"twd97",0.0,0.0};


    tm tmprdd=SmartParseDateString("1/1/1990");

    NeuronNode rootdfnn(0,rcoordfn,tmprdd,AttributeSize);
    rootdfnn.Neuron=rootdfn;
    DengueCaseSet.push_back(rootdfnn);

    //cout<<DengueCaseSet[0].Neuron.getWeight(0)<<endl;



}




void processTree::importCase( NeuronNode cdc){

    cdc.Neuron.setWeight(DengueCaseSet[0].Neuron.getWeight());

    //cout<<DengueCaseSet.at(0).Neuron.getWeight(0)<<" "<<DengueCaseSet.at(0).Neuron.getWeight(1)<<endl;
    //print min attribute
   // cout<<cdc.Neuron.getWeight(0)<<" "<<cdc.Neuron.getWeight(1)<<endl;
    IDHashTable[cdc.getFid()]=DengueCaseSet.size();
    DengueCaseSet.push_back(cdc);



}
void processTree::importCases(const std::vector< NeuronNode>& vcdc){

DengueCaseSet.insert(DengueCaseSet.end(),vcdc.begin(),vcdc.end());
//cout<<DengueCaseSet.size()<<endl;
for(int i=1;i<DengueCaseSet.size();i++){
    DengueCaseSet[i].Neuron.weight=DengueCaseSet.front().Neuron.weight;
    IDHashTable[DengueCaseSet[i].getFid()]=i;
}



}






/*THREAD CALL
vector<double> get_new_weight4para( vector<double> Zow , const vector<double>& pv,double GNR_){
//cout<<"thread"<<endl;
//DengueCaseSet[curnv].Neuron.setWeight(tmpchgw);
return Zow+GNR_*(pv-Zow);

};
*/

/*
std::vector<std::vector<int> > processTree::getAllPair()const{
std::vector<std::vector<int> > rttbl;
for(int k=1;k<DengueCaseSet.size();k++){
std::vector<std::vector<int> > cptbl= DengueCaseSet[k].getPairs();
rttbl.insert(rttbl.end(),cptbl.begin(),cptbl.end());
}
return rttbl;
}
*/
int processTree::getAttributeSize(){
return AttributeSize;
}


    void processTree::clearConnection(){
        for(int i=0;i<DengueCaseSet.size();i++){
            DengueCaseSet[i].forwardingCase.clear();
            DengueCaseSet[i].backwardingCase.clear();

        }

    }
    void processTree::clearNeuron(){
        for(int i=0;i<DengueCaseSet.size();i++){
            int nnsz=DengueCaseSet[i].Neuron.getWeight().size();
            vector<double> zro;zro.resize(nnsz,0.0);
            DengueCaseSet[i].Neuron.setWeight(zro);
        }
    }
    void processTree::clearDengueCaseSet(){
        DengueCaseSet.erase(DengueCaseSet.begin()+1,DengueCaseSet.end());

    }
