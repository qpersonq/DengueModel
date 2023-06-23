#include"DynamicFullConnectedDengueTree.hpp"
#include<iostream>

//define bound
#define lginf 0
#define lgsup 1
#define distinf 2
#define distsup 3

using namespace std;
using namespace spatialtime;

bool DynamicFullConnectedDengueTree::Connect(bool FBMODE){
    //back find
    //
    if(FBMODE){
                            for(int denidx=1;denidx<DengueCaseSet.size();denidx++){

                                    //forwarding
                                    for(int fwidx=denidx;fwidx<DengueCaseSet.size();fwidx++){
                                        //set jump condition
                                        if(
                                            getDays(DengueCaseSet[denidx].getDate() ,DengueCaseSet[fwidx].getDate())
                                            >Bound_[denidx][lgsup])break;


                                        //normal
                                            //check oversea
                                            if(ConsiderOverseaFlag_&& (DengueCaseSet[fwidx].AdditionInformation[0]!=0))continue;
                                            //test the lag INF
                                            if(
                                                getDays(DengueCaseSet[denidx].getDate() ,DengueCaseSet[fwidx].getDate())
                                                <Bound_[denidx][lginf])continue;
                                            //test the distance
                                            if(
                                                getDistant(DengueCaseSet[denidx].getCoordinate(),DengueCaseSet[fwidx].getCoordinate())
                                                >Bound_[denidx][distsup])continue;

                                            //pass the test of lag SUP/INF, distance SUP
                                            DengueCaseSet[denidx].forwardingCase.push_back( DengueCaseSet[fwidx].getFid());
                                            DengueCaseSet[fwidx].backwardingCase.push_back( DengueCaseSet[denidx].getFid());


                                    }


                        }







    }
    else
    {
                                    for(int denidx=1;denidx<DengueCaseSet.size();denidx++){

                                        //forwarding
                                        for(int fwidx=denidx-1;fwidx>=0;fwidx--){
                                            //set jump condition
                                            if(
                                                getDays(DengueCaseSet[fwidx].getDate() ,DengueCaseSet[denidx].getDate())
                                                >Bound_[denidx][lgsup])break;


                                            //normal
                                                //check oversea
                                                if(ConsiderOverseaFlag_&& (DengueCaseSet[denidx].AdditionInformation[0]!=0))continue;
                                                //test the lag INF
                                                if(
                                                    getDays(DengueCaseSet[fwidx].getDate() ,DengueCaseSet[denidx].getDate())
                                                    <Bound_[denidx][lginf])continue;
                                                //test the distance
                                                if(
                                                    getDistant(DengueCaseSet[denidx].getCoordinate(),DengueCaseSet[fwidx].getCoordinate())
                                                    >Bound_[denidx][distsup])continue;

                                                //pass the test of lag SUP/INF, distance SUP
                                                DengueCaseSet[fwidx].forwardingCase.push_back( DengueCaseSet[denidx].getFid());
                                                DengueCaseSet[denidx].backwardingCase.push_back( DengueCaseSet[fwidx].getFid());


                                        }


                                    }


    }




return true;
}
DynamicFullConnectedDengueTree::DynamicFullConnectedDengueTree(int atrbsz,const std::vector<double>& atrprp):FullConnectedDengueTree(atrbsz,atrprp){

}
void DynamicFullConnectedDengueTree::setBound(const std::vector<std::vector<double>>&bd){
if(bd.empty()){cerr<<"DynamicFullConnectedDengueTree::setBound empty!"<<endl;return;}
Bound_.clear();
vector<double>tmp;tmp.resize( bd[0].size(),0);
Bound_.push_back(tmp);
Bound_.insert(Bound_.end(),bd.begin(),bd.end());
}
std::vector<std::vector<double>>DynamicFullConnectedDengueTree::getBound()const{
    return Bound_;
}
