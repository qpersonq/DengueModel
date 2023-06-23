#include<cmath>
#include<limits>
#include<iostream>
#include<sstream>
#include<queue>
#include"MCMCDengueTree.hpp"
#include"MatrixValueTool.hpp"
using namespace std;
using namespace spatialtime;
void pushDengueQueue(std::queue<int>& dq,const std::vector<int>& bv);
bool isQflag(int val);


 void MCMCDengueTree::backPropagation(int nodenum_){


    //dim
    queue<int> processQ,backtrackQ;

    int flagtag=-1;
    //initial condition
    processQ.push(flagtag);
    pushDengueQueue(processQ,DengueCaseSet[nodenum_].backwardingCase);

    //back track
    bool pre_flag=false;
    while(!processQ.empty()){


        //process current node
        int curnode=processQ.front();
        processQ.pop();
        //if process flag
                if(isQflag(curnode)){
                        if(pre_flag) break;

                    backtrackQ.push(flagtag);
                    processQ.push(flagtag);
                    flagtag--;
                   // cout<<flagtag<<endl;
                    pre_flag=true;
                    continue;

                }
                else pre_flag=false;
        //push current node to backtrack and push the backwarding case to processQ
        backtrackQ.push(curnode);
        pushDengueQueue(processQ,DengueCaseSet[curnode].backwardingCase);


    }

    //test bt queue print
    /*while(!backtrackQ.empty()){


       // cout<<backtrackQ.front()<<endl;
        backtrackQ.pop();
    }*/

    //do BP
    vector<double>  prevec=DengueCaseSet[nodenum_].Neuron.getWeight();
    //vector< future< vector<double>> > asyvec;
    //vector<int> recordasytarget;
    double GNR= MCMCDengueTree::generateLearningRateExp(MCMCDengueTree::generateLearningRateExp(initLearningRate,CumulateEpoch,CumulateEpoch+RemainEpoch),0,2);
    //vector<thread>wrkthrdvec;
    while(!backtrackQ.empty()){
            int curnv=backtrackQ.front();
            backtrackQ.pop();
       // cout<<curnv<<endl;
        if(isQflag(curnv)){
            flagtag=curnv;
            GNR=MCMCDengueTree::generateLearningRateExp(MCMCDengueTree::generateLearningRateExp(initLearningRate,CumulateEpoch,CumulateEpoch+RemainEpoch),abs(flagtag),2);
             //processTree::generateLearningRate(5*abs(flagtag));
        }
        else{
            /*THREAD CALL
            recordasytarget.push_back(curnv);
            vector<double> dgtmp= DengueCaseSet[curnv].Neuron.getWeight();

           asyvec.push_back( async(std::launch::async,get_new_weight4para,dgtmp,ref(prevec),GNR));
            */



            //use thread to back propagation
            //Normal use BP

            DengueCaseSet[curnv].Neuron.weight=
            DengueCaseSet[curnv].Neuron.weight
            +(GNR*(prevec-DengueCaseSet[curnv].Neuron.weight));


        }

           /* for(int i=0;i<asyvec.size();i++){
                wrkthrdvec.push_back( thread ([](future<vector<double>> &ii){ii.wait();},ref(asyvec[i])));
            }*/

    }
          /*  for(int i=0;i<wrkthrdvec.size();i++){
                    wrkthrdvec[i].join();
            }*/
            /*THREAD CALL
            for(int i=0;i<asyvec.size();i++){
                    //cout<<asyvec[i].valid()<<endl;
                   DengueCaseSet[recordasytarget[i]].Neuron.setWeight(asyvec[i].get());

            }
            */

 }



double MCMCDengueTree::generateLearningRateLinear(double init,double tcnt,double tall){
return init-(tcnt*init)/tall;
}
double MCMCDengueTree::generateLearningRateExp(double init,double tcnt,double tall){
return init*exp((-1)*(tcnt/tall));
}

void pushDengueQueue(std::queue<int>& dq,const std::vector<int>& bv){
    for(int i:bv){
        dq.push(i);
    }
}
bool isQflag(int val){
if(val<0)return true;
else return false;
}


void MCMCDengueTree::fixCase(){
{
    for(unsigned int cj=1;cj<DengueCaseSet.size();cj++){
           // cout<<"fix:"<<cj<<endl;

        NeuronNode  cdc=DengueCaseSet[cj];
        cdc.backwardingCase.clear();
        cdc.forwardingCase.clear();
        int minpointer=0;
        double minculval=numeric_limits<double>::max();
         vector<double> phy_min_delta,pre_min_delta,mother_min_weight;
         phy_min_delta.resize(getAttributeSize(),0);
         pre_min_delta.resize(getAttributeSize(),0);
         mother_min_weight.resize(getAttributeSize(),0);
    //compare with old point find minNode
        int oldsize=cj;

      for(int i=0;i<oldsize;i++){

        double phydeldistant=getDistant(cdc.getCoordinate(),DengueCaseSet[i].getCoordinate());
        double phydellag=getDays(DengueCaseSet[i].getDate(),cdc.getDate());
        vector<double> phydel;
        phydel.push_back(phydeldistant);
        phydel.push_back(phydellag);
        //cout<<phydellag<<" "<<phydeldistant<<endl;

        if(phydellag>LagSup) continue;
        if(phydeldistant>DistanceSup)continue;

       // phydeldistant/=getRootPtr()->getCurrentNeuron().getWeight(0);
       // phydellag/=getRootPtr()->getCurrentNeuron().getWeight(1);
         /*start compare minpointer*/
        vector<double> motherw=DengueCaseSet[i].Neuron.getWeight();
        vector<double> selfw=cdc.Neuron.getWeight();
        vector<double> predel= selfw+(MCMCDengueTree::generateLearningRateExp(MCMCDengueTree::initLearningRate,CumulateEpoch,CumulateEpoch+RemainEpoch)*(phydel-selfw));
        //generate predict delta

        //cout<<predel[0]<<' '<<predel[1]<<endl;

        vector<double> howfar = (predel-phydel);

        /*process standardlize*/
        //howfar[0]/= getNeuronNodePtr(i)->Neuron.getWeight(0);
        howfar[1]/= DengueCaseSet[i].Neuron.getWeight(1);
        //double cmptransratio=((predel[0]/predel[1])-(getNeuronNodePtr(i)->Neuron.getWeight(0)/getNeuronNodePtr(i)->Neuron.getWeight(1)));
        //cout<<howfar[0]<<' '<<howfar[1]<<endl;
        double howfarval=(pow(2*phydel[0]/DengueCaseSet[i].Neuron.getWeight(1),2)+pow(howfar[1],2));//+pow(cmptransratio,2));

                //cout<<nwlag<<endl;
       if((phydel[1]<=LagInf || phydel[1]>LagSup ||  phydel[0] >DistanceSup ) && i!=0)continue;
        //cout<<pow(howfar[0],2)<<" "<<pow(100*howfar[1],2)<<" "<<pow(cmptransratio,2)<<endl;
        //find min connect case
        if(howfarval<minculval ){
            minculval=howfarval;
            minpointer=i;
            phy_min_delta=phydel;
            pre_min_delta=predel;
            mother_min_weight=motherw;
        }



      }
            //accept or reject
            vector<double> ppdelta=  phy_min_delta-pre_min_delta;
                // one standard accept or reject



                if(phy_min_delta[0]<DistanceSup && phy_min_delta[1]<LagSup && phy_min_delta[1]>LagInf){
                    DengueCaseSet[minpointer].forwardingCase.push_back(cdc.getFid());
                    cdc.backwardingCase.push_back(DengueCaseSet[minpointer].getFid());
                    cdc.Neuron.setWeight(pre_min_delta);
                    backPropagation(cj);
                }
                else{
                    DengueCaseSet[0].forwardingCase.push_back(cdc.getFid());
                    cdc.backwardingCase.push_back(0);
                    cdc.Neuron.setWeight(DengueCaseSet[0].Neuron.getWeight());
                    minpointer=0;
                }

                //ofstream fout("wrmin_out.csv",std::ofstream::out | std::ofstream::app);

    //if(Epoch==1)cout<<cdc.getFid()<<" ,"<<minpointer <<" ,"<<cdc.Neuron.getWeight(0)<<" ,"<<cdc.Neuron.getWeight(1)<<" ,"<<getDistant(cdc.getCoordinate(),DengueCaseSet[minpointer].getCoordinate()) <<" ,"<<getDays(DengueCaseSet[minpointer].getDate(),cdc.getDate()) <<endl;

//fout.close();
    //process min
    //double deldistant=getDistant(cdc.getCoordinate(),DengueCaseSet[minpointer].getCoordinate());
    //double dellag=getDays(DengueCaseSet[minpointer].getDate(),cdc.getDate());



    //print min attribute

    DengueCaseSet[cj]=cdc;
    }
    }//end iterator

}
bool MCMCDengueTree::setEpoch(int it__){
    if(it__<1) return false;
this->RemainEpoch=it__;
return true;
}

bool MCMCDengueTree::trainModel(){

 while(RemainEpoch){
    cout<<RemainEpoch<<"ER,";
    fixCase();
 RemainEpoch--;CumulateEpoch++;
 }

return true;
}

std::vector<std::vector<int> > MCMCDengueTree::getAllPair()const{
std::vector<std::vector<int> > rttbl;
for(int k=1;k<DengueCaseSet.size();k++){
std::vector<std::vector<int> > cptbl= DengueCaseSet[k].getPairs();
rttbl.insert(rttbl.end(),cptbl.begin(),cptbl.end());
}
return rttbl;
}
bool MCMCDengueTree::Load(std::ifstream&fin){
    string tmpstr;
    getline(fin,tmpstr); stringstream ss;    ss<<tmpstr;
    ss>>RemainEpoch;ss>>CumulateEpoch; int dsz__;ss>>dsz__;
    // RemainEpoch;
    // CumulateEpoch;
    // DengueCaseSet size
    this->DengueCaseSet.resize(dsz__);
    for(int i=0;i<dsz__;i++){
        DengueCaseSet[i].Load(fin);
    }

     return true;
}
bool MCMCDengueTree::Save(std::ofstream&fout)const{
    // RemainEpoch;
    // CumulateEpoch;
    // DengueCaseSet size
    fout<<RemainEpoch<<" "<<CumulateEpoch<<" "<<DengueCaseSet.size()<<endl;

    for(const NeuronNode & nn:DengueCaseSet){
        nn.Save(fout);
    }


    return true;
}


MCMCDengueTree::MCMCDengueTree(int atrbsz,const std::vector<double>& atrprp):processTree(atrbsz,atrprp){


}
