#include"HistogramMap.hpp"
#include<iostream>
#include<limits>
#include"MatrixValueTool.hpp"
using namespace std;
HistorgramMap::HistorgramMap(){
}
HistorgramMap::HistorgramMap(const std::vector<double>&  its){
set(its);
}
HistorgramMap::HistorgramMap(double lb,double ub,double intvl){
set(lb,ub,intvl);
}
void HistorgramMap::set(const std::vector<double>&  its){
InfIntervelSet=its;
ValueSet.resize(its.size()+1,0.0);
}
void HistorgramMap::set(double lb,double ub,double intvl){
        for(;lb<=ub;lb+=intvl){
        InfIntervelSet.push_back(lb);
    }
    ValueSet.resize(InfIntervelSet.size()+1,0.0);
}

bool  HistorgramMap::import(double val){
    if(isFreQMap){
        return false;
    }

    CntFreQ+=1;
            if(isEqualInterval){
                if(InfIntervelSet.front()>val){
                    ValueSet[0]+=1;
                }
                else if(InfIntervelSet.back()<=val){
                    ValueSet[(ValueSet.size()-1)]+=1;
                }
                else{
                    double _itv_= (InfIntervelSet.back()-InfIntervelSet.front())/((double)InfIntervelSet.size()-1);
                    int pos=( (val-InfIntervelSet.front())/_itv_);
                    ValueSet[pos+1]+=1;
                    //cout<<pos<<endl;
                }
                    return true;
            }


        bool overflg=true;
        int k=0;
        for(;k<InfIntervelSet.size();k++){

            if(val<InfIntervelSet[k] ){
                ValueSet[k]++;
                overflg=false;
                break;
            }

        }
        if(overflg){
            ValueSet[k]++;
        }


return true;
}
std::vector<double> HistorgramMap::getInfIntervelSet()const{
return InfIntervelSet;
}
double  HistorgramMap::getCount(double vl)const{
     int k=0;
        for(;k<InfIntervelSet.size();k++){
             if(vl<InfIntervelSet[k]){

                    break;

             }
        }
return ValueSet[k];
}
double  HistorgramMap::getPDF(double vl)const{
    return ((double)getCount(vl))/ ((double)CntFreQ);

}
double  HistorgramMap::getCDF(double vl)const{
    double cul=0;
    int k=0;
        for(;k<InfIntervelSet.size();k++){
             cul+=ValueSet[k];
             if(vl<InfIntervelSet[k]){

                    break;

             }

        }
    return (double)cul/(double)CntFreQ;
}

 std::vector<std::vector<double>>HistorgramMap::getBoundAndCount()const{
      std::vector<std::vector<double>>rt;
              rt.push_back({(double)numeric_limits<double>::min() ,InfIntervelSet[0],ValueSet[0]});
              for(int i=1;i<ValueSet.size()-1;i++){
                rt.push_back({InfIntervelSet[i-1] ,InfIntervelSet[i],ValueSet[i]});

              }
              rt.push_back({ InfIntervelSet.back(),(double) numeric_limits<double>::max(),ValueSet[ValueSet.size()-1]});
 return rt;
 }




void HistorgramMap::save(ofstream& fout)const{

    writeVector({(double)isFreQMap,CntFreQ,(double)isEqualInterval},fout);
    writeVector(InfIntervelSet,fout);
    writeVector(ValueSet,fout);
}
void HistorgramMap::load(ifstream& fin){
    vector<double>tmpdarr;
    readVector(tmpdarr,fin);
        isFreQMap=(int)tmpdarr[0];
        CntFreQ=tmpdarr[1];
        isEqualInterval=(int)tmpdarr[2];
    readVector(InfIntervelSet ,fin);
    readVector(ValueSet,fin);
}



HistorgramMap PDFPlus(const  HistorgramMap& a,const  HistorgramMap& b){
 HistorgramMap hgm(a.getInfIntervelSet());
        if(a.getInfIntervelSet()!=b.getInfIntervelSet()){
            cerr<<"err intervelset"<<endl;
            return hgm;
        }
        hgm.isFreQMap=true;


        for(int i=0;i<a.ValueSet.size();i++ ){
            double rr=(a.ValueSet.at(i)/a.CntFreQ)+(b.ValueSet.at(i)/b.CntFreQ);
            hgm.ValueSet[i]=rr;
            hgm.CntFreQ+=rr;
        }


 return hgm;

}
HistorgramMap CountPlus(const  HistorgramMap& a,const  HistorgramMap& b)
{
       HistorgramMap hgm(a.getInfIntervelSet());
        if(a.getInfIntervelSet()!=b.getInfIntervelSet()){
            cerr<<"err intervelset"<<endl;
            return hgm;
        }
        for(int i=0;i<a.ValueSet.size();i++ ){
            hgm.ValueSet[i]=(a.ValueSet[i]+b.ValueSet[i]);
            hgm.CntFreQ+=hgm.ValueSet[i];
        }

 return hgm;
}
double HistorgramMap::getInverseCDF(float CDF)const{
     double cul=0;
     int k=0;
        for(;k<InfIntervelSet.size();k++){
             cul+=ValueSet[k]/CntFreQ;
             if(cul>=CDF){

                    break;

             }
        }
if((k-1)<0)return InfIntervelSet[k];
return InfIntervelSet[k-1];

}
void Smooth(HistorgramMap& hm,float pc){
    hm.isFreQMap=true;
    vector<double> cpvlset;
    cpvlset.resize(hm.ValueSet.size());

    double nnfreqcnt=0;
    if(cpvlset.size()<=2) return;
    cpvlset[0]=(1-pc)*hm.ValueSet[0] +pc*hm.ValueSet[1];
    cpvlset[cpvlset.size()-1]=(1-pc)*hm.ValueSet[cpvlset.size()-2] +pc*hm.ValueSet[cpvlset.size()-1];

    for(int i=1; i<cpvlset.size()-1;i++){
        cpvlset[i]=pc*hm.ValueSet[i+1]+pc*hm.ValueSet[i-1]+(1-(2*pc))*hm.ValueSet[i];
   // cout<<i<<" ";
    }
    for(auto r:cpvlset){
        nnfreqcnt+=r;
    }
hm.ValueSet=cpvlset;
hm.CntFreQ=nnfreqcnt;
}
HistorgramMap PDFPlusBatch(const std::vector<HistorgramMap>&hsmp){
 HistorgramMap hgm(hsmp.front().getInfIntervelSet());


        bool rtchk=true;
            for(auto k:hsmp){
                if(k.InfIntervelSet!=hsmp.front().InfIntervelSet){rtchk=false;break;}
            }



        if(!rtchk){
            cerr<<"err intervelset"<<endl;
            return hgm;
        }
        hgm.isFreQMap=true;


        for(int i=0;i<hsmp.front().ValueSet.size();i++ ){
            double rr=0;
            for(auto g:hsmp)    rr+=(g.ValueSet.at(i)/g.CntFreQ);

            hgm.ValueSet[i]=rr;
            hgm.CntFreQ+=rr;
        }


 return hgm;

}
