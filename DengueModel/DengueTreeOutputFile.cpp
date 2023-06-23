#include<iostream>
#include<iomanip>
#include"DengueTreeOutputFile.hpp"
#include<fstream>
#include<sstream>
#include<map>
#include<stack>
#include<algorithm>
#include"SpatialTimeProcessor.hpp"
#include"CoordinateTransmationTWD97.hpp"
#include"GeoJsonGenerator.hpp"
#include"MatrixValueTool.hpp"
using namespace std;
using namespace spatialtime;
TreeWriter::TreeWriter(processTree* ptptr ){
proctreeptr_=ptptr;


}


bool TreeWriter::writePoint(ofstream &fout){

  if (!fout.is_open())return false;
  fout<<"fid_,X,Y,Time,Oversea,Serotype"<<endl;
  bool zeroFlag=true;
  for(const NeuronNode& inn:proctreeptr_->DengueCaseSet){
       if(zeroFlag){
            zeroFlag=false;
            continue;
        }
   fout<<inn.getFid()<<","
   <<std::scientific <<inn.getCoordinate().X<<","
   <<std::scientific <<inn.getCoordinate().Y<<",";
   //append
   fout<< convertTime2String(inn.getDate())<<",";
   fout<< (bool)inn.AdditionInformation[0]<<",";
   fout<< (int)inn.AdditionInformation[1]<<",";




   fout<<endl;
  }
  //fout.close();
  return true;
}
bool TreeWriter::writeConnectLine(ofstream &fout){

  if (!fout.is_open())return false;
  fout<<"CONNNO,sfid,sx,sy,st,efid,ex,ey,et,dx,dt"<<endl;
  bool zeroFlag=true;
  int CONNNO=1;
  for(const NeuronNode& inn:proctreeptr_->DengueCaseSet){


        if(zeroFlag){
            zeroFlag=false;
            continue;
        }

    for(int nnptr :inn.forwardingCase){
            nnptr=proctreeptr_->IDHashTable[nnptr];
            fout
         <<CONNNO<<","
         <<inn.getFid()<<","
         <<std::scientific <<inn.getCoordinate().X<<","
         <<std::scientific <<inn.getCoordinate().Y<<","
         <<convertTime2String(inn.getDate()) <<",";
            fout
         <<proctreeptr_->DengueCaseSet.at(nnptr).getFid()<<","
         <<std::scientific <<proctreeptr_->DengueCaseSet.at(nnptr).getCoordinate().X<<","
         <<std::scientific <<proctreeptr_->DengueCaseSet.at(nnptr).getCoordinate().Y<<","
         <<convertTime2String(proctreeptr_->DengueCaseSet.at(nnptr).getDate()) <<",";
            fout
         <<(int)getDays(inn.getDate(),proctreeptr_->DengueCaseSet.at(nnptr).getDate())<<","
         <<getDistant(inn.getCoordinate(),proctreeptr_->DengueCaseSet.at(nnptr).getCoordinate())<<",";

         fout<<endl;
         CONNNO++;
    }
  }
  //fout.close();
  return true;
}
bool TreeWriter::writeGetPair(std::ofstream& fout){
if (!fout.is_open())return false;
  fout<<"CONNNO,sfid,sx,sy,st,efid,ex,ey,et,dx,dt"<<endl;
  //bool zeroFlag=true;
  int CONNNO=1;
  vector<vector<int>> vecpp =proctreeptr_->getAllPair();
  for(const vector<int>& pr:vecpp){
           const NeuronNode fdc= proctreeptr_->getDengueCase(proctreeptr_->IDHashTable[pr[0]]);
           const NeuronNode bdc= proctreeptr_->getDengueCase(proctreeptr_->IDHashTable[pr[1]]);





            fout
         <<CONNNO<<","
         <<fdc.getFid()<<","
         <<std::scientific <<fdc.getCoordinate().X<<","
         <<std::scientific <<fdc.getCoordinate().Y<<","
         <<convertTime2String(fdc.getDate()) <<",";
            fout
         <<bdc.getFid()<<","
         <<std::scientific <<bdc.getCoordinate().X<<","
         <<std::scientific <<bdc.getCoordinate().Y<<","
         <<convertTime2String(bdc.getDate()) <<",";
            fout
         <<(int)getDays(fdc.getDate(),bdc.getDate())<<","
         <<getDistant(fdc.getCoordinate(),bdc.getCoordinate())<<",";

         fout<<endl;
         CONNNO++;

  }
  //fout.close();
  return true;






}

bool TreeWriter::writeNeutron(ofstream &fout){

if (!fout.is_open())return false;
fout<<"ffid,bfid,wdist,wlag,rdist,rlag"<<endl;
  for(int i=1;i<proctreeptr_->DengueCaseSet.size();i++){
     const NeuronNode& rdnn = proctreeptr_->DengueCaseSet[i];
        int ffid=i;
        int bfid1=rdnn.backwardingCase[0];
        //if(bfid1==0)continue;
        double wd= proctreeptr_->DengueCaseSet[bfid1].Neuron.getWeight(0);
        double wl= proctreeptr_->DengueCaseSet[bfid1].Neuron.getWeight(1);
        double rd= getDistant(proctreeptr_->DengueCaseSet[ffid].getCoordinate(),proctreeptr_->DengueCaseSet[bfid1].getCoordinate());
        double rl= getDays(proctreeptr_->DengueCaseSet[bfid1].getDate(),proctreeptr_->DengueCaseSet[ffid].getDate());
        //append


    fout<<ffid<<","<<bfid1<<","<<wd<<","<<wl<<","<<rd<<","<<rl;
    //appendfout<<"";
    //fout<<",";
    //fout<<proctreeptr_->DengueCaseSet[ffid].coordinate.X<<",";
    //fout<<proctreeptr_->DengueCaseSet[ffid].coordinate.Y;
    fout<<endl;

  }
//fout.close();
return true;
}

 bool TreeWriter::writeGeoJson(ofstream &fout){
     bool zeroFlag=true;

     if(!fout.is_open())return false;


    //head
    fout<<"eqfeed_callback("<<endl;
    fout<<"{ \"type\": \"FeatureCollection\", \"features\": ["<<endl;



    //properties
    for(const NeuronNode& inn:proctreeptr_->DengueCaseSet){
       if(zeroFlag){
            zeroFlag=false;
            continue;
        }

   //append
   string propstr,tmpstrrrrr;
   stringstream ss;
   ss<<inn.getFid()<<" ";
   ss<<(inn.getDate().tm_year+1900)<<"/"<<(inn.getDate().tm_mon+1)<<"/"<<inn.getDate().tm_mday;
   ss>>propstr;
   ss>>tmpstrrrrr;
   map<string,string>propmap;
   propstr=propstr+"_"+tmpstrrrrr;
   propmap["notation"]=propstr;
   propmap["distant"]=NUMconvert2STR(inn.Neuron.getWeight(0));
   propmap["time"]=NUMconvert2STR(inn.Neuron.getWeight(1));

    CoordinateTransform *CTF=CoordinateTransform::getInstance();
    Coordinate lonlatcoor=CTF->Cal_twd97_To_lonlat(inn.getCoordinate().X,inn.getCoordinate().Y);
    fout<<geojPoint(lonlatcoor.X,lonlatcoor.Y,propmap);
    fout<<","<<endl;
  }

  //string line

   zeroFlag=true;
  for(const NeuronNode& inn:proctreeptr_->DengueCaseSet){
        if(zeroFlag){
            zeroFlag=false;
            continue;
        }

    for(int nnptr :inn.forwardingCase){

        //generate connect JSON
        CoordinateTransform *CTF=CoordinateTransform::getInstance();
        Coordinate sc=CTF->Cal_twd97_To_lonlat(inn.getCoordinate().X,inn.getCoordinate().Y),ec=CTF->Cal_twd97_To_lonlat(proctreeptr_->DengueCaseSet.at(nnptr).getCoordinate().X,proctreeptr_->DengueCaseSet.at(nnptr).getCoordinate().Y);
        vector<Coordinate> gjltmpvec;
        gjltmpvec.push_back(sc);
        gjltmpvec.push_back(ec);
        //propmap  fill in
         map<string,string>propmap;
         propmap["start_Fid"]=NUMconvert2STR(inn.getFid());
         propmap["start_Date"]=convertTime2String(inn.getDate());
         propmap["end_Fid"]=NUMconvert2STR(proctreeptr_->DengueCaseSet[(nnptr)].getFid());
         propmap["end_Date"]=convertTime2String(proctreeptr_->DengueCaseSet[(nnptr)].getDate());
        fout<<geojlinestring(gjltmpvec,propmap);
        fout<<","<<endl;

    }
  }













    //tail

    fout<<"]});"<<endl;
    //fout.close();
 return true;
 }

  bool TreeWriter::travelsal(ofstream &fout){


     if(!fout.is_open())return false;


    vector<NeuronNode> cpdcs=proctreeptr_->DengueCaseSet;
    vector<int> hdrecord=     cpdcs[0].forwardingCase;

    stack<int> fidstack;
    fidstack.push(0);
    int rctop;
    while(!fidstack.empty()){

        if(!cpdcs[fidstack.top()].forwardingCase.empty()){
            //down
            rctop=fidstack.top();

            for(int ei:cpdcs[fidstack.top()].forwardingCase){
            fidstack.push(ei);
            }

            cpdcs[rctop].forwardingCase.clear();

        }
        else{
            //pop

            fout<<fidstack.top()<<" ";
            if(fidstack.top()==rctop)fout<<"+";
            if(find(hdrecord.begin(),hdrecord.end(),fidstack.top())!=hdrecord.end())fout<<endl;

            fidstack.pop();


        }
    }
//fout.close();


  return true;
  }
