#include<map>
#include<time.h>
#include<set>
#include"DayReportTools.hpp"
using namespace std;
using namespace spatialtime;
std::vector<std::tuple<time_t,double,double,double>>getTruePositionSliceFromDynamicFullConnectedDengueTree (const  DynamicFullConnectedDengueTree& dfcdtr){
std::vector<std::tuple<time_t,double,double,double>> rt;
        set<int> iscountfid;
      auto  radbigtable=dfcdtr.getBound();
    for(int i=1;i<dfcdtr.getDengueCaseSize();i++){

           vector<int> bkwdfid= dfcdtr.getDengueCase(i).backwardingCase;
           for(int bkfid: bkwdfid){
            // current
                if(iscountfid.count(bkfid)){
                iscountfid.insert(bkfid);
                //do nothing
                }
                else{

                iscountfid.insert(bkfid);

                time_t happentime=tm2time_t(dfcdtr.getDengueCase(i).getDate()) ;
                // past
                    //fid to array idx
                    //dfcdtr.IDHashTable
                    int bkidx=const_cast<DynamicFullConnectedDengueTree&>(dfcdtr).IDHashTable[bkfid];
                double posx=dfcdtr.getDengueCase(bkidx).getCoordinate().X;
                double posy=dfcdtr.getDengueCase(bkidx).getCoordinate().Y;
                double rad= radbigtable[bkidx][3];
                rt.push_back(make_tuple(happentime,posx,posy,rad));
                }


           }

    }


return rt;
}













std::map<time_t,double> refillTimesTuplesBubbles(time_t start_t,time_t end_t,time_t add_t,const std::map<time_t,double>& tupll){

map<time_t,double>  rt;
            for(time_t cur=start_t;cur<=end_t;cur=cur+add_t){
                //if(tupll.find(cur)!= tupll.end()){
                    rt[cur]=const_cast<map<time_t,double>&>(tupll)[cur];
                //}
                //rt[cur]=0;
            }




return rt;
}


std::map<time_t,std::tuple<int,int,int>> DayReportController::PointsProcessor()const{

map<time_t,int> allcount;
map<time_t,int> ispredpt;
map<time_t,int> importedcase;



for(int i=1;i<dynamicfullconnecttree_->getDengueCaseSize();i++){
     time_t cur=tm2time_t( dynamicfullconnecttree_->getDengueCase(i).getDate());
     //prc all
     if(allcount.find(cur)!=allcount.end()){
         allcount[cur]= (allcount[cur]+1);
     }
     else{
        allcount[cur]=1;
     }
     // is pred
    if( dynamicfullconnecttree_->getDengueCase(i).backwardingCase.size()!=0){

                 if(ispredpt.find(cur)!=ispredpt.end()){
                    ispredpt[cur]=(ispredpt[cur]+1);
                 }
                 else{
                     ispredpt[cur]=1;
                 }


    }
    // is imported
    if(((bool)dynamicfullconnecttree_->getDengueCase(i).AdditionInformation[0])==true){

                 if(importedcase.find(cur)!=importedcase.end()){
                    importedcase[cur]=(importedcase[cur]+1);
                 }
                 else{
                     importedcase[cur]=1;
                 }
    }


}




//fix bubbles
std::map<time_t,std::tuple<int,int,int>> rt;
for(time_t cur=StartTime_;cur<=EndTime_;cur+=86400){
     int all,prd,imp;
     if(allcount.find(cur)!=allcount.end() ){
        all=allcount[cur];
     }
     else{
        all=0;
     }
     if(ispredpt.find(cur)!=ispredpt.end()){
        prd=ispredpt[cur];
     }
     else{
        prd=0;
     }
     if(importedcase.find(cur)!=importedcase.end()){
        imp=importedcase[cur];
     }
     else{
        imp=0;
     }



    rt[cur]=make_tuple(all,prd,imp);

}

return rt;
}

std::map<time_t,double> DayReportController::RadProcessor()const{

map<time_t,vector<double>*> cnter;

    for(int i=0;i<SliceTable.size();i++){

       time_t tt= get<0>(SliceTable[i]);
       double rad= get<3>(SliceTable[i]);

        if(cnter.find(tt)!=cnter.end()){

            cnter[tt]->push_back(rad);
        }
        else{
            vector<double>* nnvec=new vector<double>();nnvec->push_back(rad);
            cnter[tt]=nnvec;
        }
    }
map<time_t,double> rt;
    for(time_t cur=StartTime_;cur<=EndTime_;cur+=86400){
        if(cnter.find(cur)!=cnter.end()){
            vector<double> tmpvec=*cnter[(cur)];
            int cnt=0;double val=0;
            for(double q:tmpvec){
                cnt+=1;
                val+=q;
            }
            if(cnt!=0)rt[cur]=val/(double)cnt;
            else rt[cur]=0;
        }
        else{
            rt[cur]=0;
        }


    }

    //delete new radvec
    for(auto itr=cnter.begin();itr!=cnter.end();itr++){
        delete itr->second;
    }

return rt;
}



bool DayReportController::writeReport(std::ostream& os,bool wrcap ){
    //wr cap
/*
for(auto cap: ReportItemsString){

    os<<cap<<",";
}
os<<endl;
*/
//calculate
map<time_t,tuple<int,int,int>> ptrc=PointsProcessor();

map<time_t,double> radrc=RadProcessor();
/*
  "Date",
  "AllCs","ImpCs","PrCs","CsRecall","CsRecallFix",
  "RadLb","RadUb",
  "BuffA_ND","TPBuffA_ND","FPBuffA_ND","BuffPrecision_ND",
  "BuffA_D","TPBuffA_D","FPBuffA_D","BuffPrecision_D",
  "Density_ND","Density_D",
  "MixF1_ND","MixF1_D","MFF1_D"
*/
// wr every day

    if(wrcap){
            for(auto cp: ReportItemsString){
                os<<cp<<",";

            }
            os<<endl;
    }




    for(time_t cur=StartTime_;cur<=EndTime_;cur+=86400){

        //Date
        os<< convertTime2String(time_t2tm(cur))<<",";
        //Points
        os<<get<0>(ptrc[cur]) <<",";
        os<<get<2>(ptrc[cur]) <<",";
        os<<get<1>(ptrc[cur]) <<",";
        if(get<0>(ptrc[cur])!=0)os<<((double)get<1>(ptrc[cur])/(double)get<0>(ptrc[cur]))<<",";else os<<"0"<<",";
        if((get<0>(ptrc[cur])-get<2>(ptrc[cur]))!=0)os<<((double)get<1>(ptrc[cur])/(double)(get<0>(ptrc[cur])-get<2>(ptrc[cur])))<<",";else os<<"0"<<",";

        //Rad
        os<<0<<",";
        os<<radrc[cur] <<",";


        //BuffND
        os<<StoredALLBuffers_ND[cur]<<",";
        os<<StoredTPBuffers_ND[cur]<<",";
        os<<(StoredALLBuffers_ND[cur]-StoredTPBuffers_ND[cur])<<",";
        if(StoredALLBuffers_ND[cur]!=0)os<<(StoredTPBuffers_ND[cur]/StoredALLBuffers_ND[cur])<<",";else os<<"0"<<",";
        //BuffD

            //Buff D fixing
            double Buff_ALL=StoredALLBuffers_D[cur],Buff_TP=StoredTPBuffers_D[cur];
            if(Buff_TP>Buff_ALL) Buff_TP=Buff_ALL;

        os<<Buff_ALL<<",";
        os<<Buff_TP<<",";
        os<<(Buff_ALL-Buff_TP)<<",";
        if(Buff_ALL!=0)os<<(Buff_TP/Buff_ALL)<<",";else os<<"0"<<",";
        //Density
        if(StoredALLBuffers_ND[cur]!=0)os<< ((double)get<1>(ptrc[cur]) /StoredALLBuffers_ND[cur]*1000000)<<",";else os<<"0"<<",";
        if(StoredALLBuffers_D[cur]!=0)os<< ((double)get<1>(ptrc[cur]) /StoredALLBuffers_D[cur]*1000000)<<",";else os<<"0"<<",";

        //F1

            double CsRecall=(get<0>(ptrc[cur])!=0)? ((double)get<1>(ptrc[cur])/(double)get<0>(ptrc[cur])):0.0;
            double CsRecallFix=((get<0>(ptrc[cur])-get<2>(ptrc[cur]))!=0)?((double)get<1>(ptrc[cur])/(double)(get<0>(ptrc[cur])-get<2>(ptrc[cur]))) :0;

            double BuffPrecision_ND= (StoredALLBuffers_ND[cur]!=0)? StoredTPBuffers_ND[cur]/StoredALLBuffers_ND[cur]:0.0;
            double BuffPrecision_D= (Buff_ALL!=0)? Buff_TP/Buff_ALL:0.0;

            double f1_ND= ( (CsRecall+BuffPrecision_ND)!=0)? ( 2*(CsRecall*BuffPrecision_ND)/ (CsRecall+BuffPrecision_ND)):0;
            double f1_D=  ( (CsRecall+BuffPrecision_D)!=0)? ( 2*(CsRecall*BuffPrecision_D)/ (CsRecall+BuffPrecision_D)):0;
            double fixf1_D=  ( (CsRecallFix+BuffPrecision_D)!=0)? ( 2*(CsRecallFix*BuffPrecision_D)/ (CsRecallFix+BuffPrecision_D)):0;

        os<<f1_ND<<",";
        os<<f1_D<<",";
        os<<fixf1_D<<",";
     os<<endl;
    }


return true;
}







