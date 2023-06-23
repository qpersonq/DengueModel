#include<fstream>
#include<sstream>
#include"FullConnectedDengueTree.hpp"

using namespace std;
using namespace spatialtime;
FullConnectedDengueTree::FullConnectedDengueTree(int atrbsz,const std::vector<double>& atrprp):processTree(atrbsz,atrprp){
    //Connect();
}
bool FullConnectedDengueTree::Connect(){
    //do DBSCAN root do not care!
    for(int denidx=1;denidx<DengueCaseSet.size();denidx++){

        //forwarding
        for(int fwidx=denidx;fwidx<DengueCaseSet.size();fwidx++){
            //set jump condition
            if(
                getDays(DengueCaseSet[denidx].getDate() ,DengueCaseSet[fwidx].getDate())
                >LagSup)break;


            //normal
                //check oversea
                if(ConsiderOverseaFlag_&& (DengueCaseSet[fwidx].AdditionInformation[0]!=0))continue;
                //test the lag INF
                if(
                    getDays(DengueCaseSet[denidx].getDate() ,DengueCaseSet[fwidx].getDate())
                    <LagInf)continue;
                //test the distance
                if(
                    getDistant(DengueCaseSet[denidx].getCoordinate(),DengueCaseSet[fwidx].getCoordinate())
                    >DistanceSup)continue;
                if(
                    getDistant(DengueCaseSet[denidx].getCoordinate(),DengueCaseSet[fwidx].getCoordinate())
                    <DistanceInf)continue;


                //pass the test of lag SUP/INF, distance SUP
                DengueCaseSet[denidx].forwardingCase.push_back( DengueCaseSet[fwidx].getFid());
                DengueCaseSet[fwidx].backwardingCase.push_back( DengueCaseSet[denidx].getFid());


        }


    }



return true;
}
bool FullConnectedDengueTree::Load(std::ifstream&fin){
    string tmpstr;
    getline(fin,tmpstr); stringstream ss;    ss<<tmpstr;
     int dsz__;ss>>dsz__;
    // RemainEpoch;
    // CumulateEpoch;
    // DengueCaseSet size
    this->DengueCaseSet.resize(dsz__);
    for(int i=0;i<dsz__;i++){
        DengueCaseSet[i].Load(fin);
    }

     return true;
}
bool FullConnectedDengueTree::Save(std::ofstream&fout)const{
    // RemainEpoch;
    // CumulateEpoch;
    // DengueCaseSet size
    fout<<DengueCaseSet.size()<<endl;

    for(const NeuronNode & nn:DengueCaseSet){
        nn.Save(fout);
    }


    return true;
}
 bool FullConnectedDengueTree::setParameter(double lginf,double lgsup,double distsup){
     LagInf=lginf;
     LagSup=lgsup;
     DistanceInf=0.0;
     DistanceSup=distsup;
     return true;
 }

 bool  FullConnectedDengueTree::setParameter(double lginf,double lgsup,double distinf,double distsup){
     LagInf=lginf;
     LagSup=lgsup;
     DistanceInf=distinf;
     DistanceSup=distsup;
     return true;


 }
 std::vector<std::vector<int> > FullConnectedDengueTree::getAllPair()const{
std::vector<std::vector<int> > rttbl;
for(int k=1;k<DengueCaseSet.size();k++){
std::vector<std::vector<int> > cptbl= DengueCaseSet[k].getPairs();
rttbl.insert(rttbl.end(),cptbl.begin(),cptbl.end());
}
return rttbl;
}

