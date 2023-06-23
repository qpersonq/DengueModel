#include<fstream>
#include<sstream>
#include<iostream>
#include<set>
#include<algorithm>
#include<list>
#include"DBSCANDengueTree.hpp"
#include"NeuronTree.hpp"

using namespace std;
using namespace spatialtime;
DBSCANDengueTree::DBSCANDengueTree(int atrbsz,const std::vector<double>& atrprp):processTree(atrbsz,atrprp){


    //DBSCAN();
}
bool DBSCANDengueTree::DBSCAN(){
    //init
    ClusterRecoder.resize(DengueCaseSet.size());
    adjPoints.resize(DengueCaseSet.size());
    visited.resize(DengueCaseSet.size());
    CurrentCluster.resize(DengueCaseSet.size(),NOT_CLASSIFIED);







//cout<<"start"<<endl;
    int clusterIdx=-1;
    //cout<<"bf cnp";
    checkNearPoints();
//cout<<"af cnp";
    for( int i=1;i<DengueCaseSet.size();i++){
//cout<<(i)<<" ";
            if(CurrentCluster[i] != NOT_CLASSIFIED) continue;
            if(isCoreObject(i)){
                dfs(i, ++clusterIdx,0);
            }
            else{
                CurrentCluster[i]=NOISE;
            }


    }
            /*
            ofstream fout("debg.csv");
            for( int i=1;i<DengueCaseSet.size();i++){
                   if(!ClusterRecoder[i].empty()){
                    //auto r=ClusterRecoder[i].back();
                    Coordinate cc=DengueCaseSet[i].getCoordinate();
                    fout<<DengueCaseSet[i].getFid()<<","<<std::scientific<<cc.X<<","<<std::scientific<<cc.Y<<","<<convertTime2String(DengueCaseSet[i].getDate())<<",";
                            for(auto r:ClusterRecoder[i]){
                                 fout<<r.first<<","<<r.second <<",";
                            }
                            fout<<endl;;

                   }
            }
            fout.close();
            */
            // process connected
                //create cache
                map<pair<int,int>,list<int>*> lstche;

                for( int i=1;i<DengueCaseSet.size();i++){
                        if(ClusterRecoder[i].empty())continue;
                        pair<int,int> r=ClusterRecoder[i].back();
                        if(lstche.find({r.first,r.second})==lstche.end()){
                            lstche[{r.first,r.second}]=new list<int>;
                        }

                        (*lstche[{r.first,r.second}]).push_back(i);


                }
                //try and make the solution
                  for( int i=1;i<DengueCaseSet.size();i++){
                    if(ClusterRecoder[i].empty())continue;
                        pair<int,int> r=ClusterRecoder[i].back();
                        if(lstche.find({r.first,r.second-1})!=lstche.end()){
                              list<int>*crlst=  (lstche[{r.first,r.second-1}]);
                                for(list<int>::const_iterator lcit=crlst->begin();lcit!=crlst->end();lcit++){
                                    int _fidx_=(*lcit);//previous points
                                      if( isLowerEps(_fidx_,i)){
                                        DengueCaseSet[i].backwardingCase.push_back(DengueCaseSet[_fidx_].getFid());
                                        DengueCaseSet[_fidx_].forwardingCase.push_back(DengueCaseSet[i].getFid());

                                      }


                                }

                        }


                  }

                //del cache
                for(auto k:lstche){

                    delete k.second;
                }





return true;
}

void DBSCANDengueTree::dfs(int nowidx,int clustid,int lyr){
    CurrentCluster[nowidx] =clustid;
    ClusterRecoder[nowidx].push_back({clustid,lyr});
    if(!isCoreObject(nowidx)) return;

    for(int r:adjPoints[nowidx]){
        if(CurrentCluster[r]!= NOT_CLASSIFIED) continue;
        dfs(r,clustid,lyr+1);
    }

}



void DBSCANDengueTree::checkNearPoints(){
    for( int i=1;i<DengueCaseSet.size();i++){
           // cout<<i<<" ";
         for( int j=i+1;j<DengueCaseSet.size();j++){
            if(i==j)continue;
            //cout<<i<<" "<<j<<endl;
            if(ConsiderOverseaFlag_)if((isLowerEps(i,j))&&(DengueCaseSet[j].AdditionInformation[0]==0  ) )adjPoints[i].push_back(j);
            else if((isLowerEps(i,j)) )adjPoints[i].push_back(j);
            //cout<<"pb adjpts"<<endl;

             //cout<<"pb adjpts"<<endl;
         }
    }


}
bool DBSCANDengueTree::isLowerEps(int idxa,int idxb)const{
    //DengueCaseSet[idxa]
    //DengueCaseSet[idxb]
    double dis=getDistant(DengueCaseSet[idxa].getCoordinate(),DengueCaseSet[idxb].getCoordinate());
    int dd=getDays(DengueCaseSet[idxa].getDate(),DengueCaseSet[idxb].getDate());
//cout<<idxa<<" "<<idxb<<" "<<dis<<" "<<dd<<endl;
    if( (dis<=DistanceSup) && (dd<=LagSup )&& (dd>=LagInf)) return true;

    return false;
}
bool DBSCANDengueTree::isCoreObject(int idx)const {

return (adjPoints[idx].size() >= minPts);

}


std::vector<std::vector<int> > DBSCANDengueTree::getAllPair()const{
std::vector<std::vector<int> > rttbl;

if(PairControl_select_Flag==0){
    for(int k=1;k<DengueCaseSet.size();k++){
    std::vector<std::vector<int> > cptbl= DengueCaseSet[k].getPairs();
    rttbl.insert(rttbl.end(),cptbl.begin(),cptbl.end());
    }
}
else{
     for(int k=1;k<DengueCaseSet.size();k++){
        std::vector<std::vector<int> > cptbl= DengueCaseSet[k].getPairs();
        if(cptbl.size()<=PairControl_select_Flag){
            rttbl.insert(rttbl.end(),cptbl.begin(),cptbl.end());
        }
        else{
            set<int> tmpset;
            vector<int>idsss;
            for(auto p:cptbl){idsss.push_back(p[1]);}

            // by lag
            sort(idsss.begin(),idsss.end(),[&](int lid,int bid) ->bool{
                                                 const NeuronNode& ccs=  DengueCaseSet[k];
                                                 const NeuronNode& lcs=  DengueCaseSet[const_cast<map<int,int>*>(&IDHashTable)->operator[](lid) ];
                                                 const NeuronNode& rcs=  DengueCaseSet[const_cast<map<int,int>*>(&IDHashTable)->operator[](bid) ];
                                                 double dd1=getDays(ccs.getDate(),lcs.getDate());
                                                 double dd2=getDays(ccs.getDate(),rcs.getDate());
                                                 return dd1 > dd2;
                                              });
                tmpset.insert(idsss.front());
                tmpset.insert(idsss.back());

                            int IUIT=(idsss.size()/((PairControl_select_Flag-2)+1));
                            for(int qxq=0;qxq<(PairControl_select_Flag-2);qxq++){

                                  tmpset.insert( idsss[(qxq+1)*IUIT]);
                            }


            //by distance
            sort(idsss.begin(),idsss.end(),[&](int lid,int bid) ->bool{
                                                 const NeuronNode& ccs=  DengueCaseSet[k];
                                                 const NeuronNode& lcs=  DengueCaseSet[const_cast<map<int,int>*>(&IDHashTable)->operator[](lid) ];
                                                 const NeuronNode& rcs=  DengueCaseSet[const_cast<map<int,int>*>(&IDHashTable)->operator[](bid) ];
                                                 double dd1=getDistant(ccs.getCoordinate(),lcs.getCoordinate());
                                                 double dd2=getDistant(ccs.getCoordinate(),rcs.getCoordinate());
                                                 return dd1 > dd2;
                                              });
                tmpset.insert(idsss.front());
                tmpset.insert(idsss.back());

                            IUIT=(idsss.size()/((PairControl_select_Flag-2)+1));
                            for(int qxq=0;qxq<(PairControl_select_Flag-2);qxq++){

                                  tmpset.insert( idsss[(qxq+1)*IUIT]);
                            }






            //merge
            for(auto zzz:tmpset){
                rttbl.push_back({cptbl[0][0],zzz});
            }



        }//end else of flag not equal 0
    }



}

return rttbl;
}





















bool DBSCANDengueTree::Load(std::ifstream&fin){
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
bool DBSCANDengueTree::Save(std::ofstream&fout)const{
    // RemainEpoch;
    // CumulateEpoch;
    // DengueCaseSet size
    fout<<DengueCaseSet.size()<<endl;

    for(const NeuronNode & nn:DengueCaseSet){
        nn.Save(fout);
    }


    return true;
}
