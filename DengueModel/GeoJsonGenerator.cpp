#include<iostream>
#include<vector>
#include<sstream>
#include<map>
#include<iterator>
#include"GeoJsonGenerator.hpp"
using namespace std;
using namespace spatialtime;
std::string jarray(const std::vector<std::string>& prop_ ){
string rtstr;
for(int i=0;i<prop_.size()-1;i++){
    rtstr+=(prop_[i]+",");
}

rtstr+=prop_[prop_.size()-1];
return "["+rtstr+"]";
}
std::string jitem(const std::string& ll,const std::string& rr){

return ll+":"+rr;
}
std::string jset(const std::vector<std::string>& prop_){
string rtstr;
for(int i=0;i<prop_.size()-1;i++){
    rtstr+=(prop_[i]+",");
}

rtstr+=prop_[prop_.size()-1];
return "{"+rtstr+"}";
}
std::string jnotation(string str){
return "\""+str+"\"";
}
std::string geojPoint(double lon,double lat ,const std::map<std::string,std::string>& notation){
string rtstr;
        //edit notation
        string notationstr;
        {


            vector<string> tmpvecmi;
            for(map<string,string>::const_iterator mi=notation.begin();mi!=notation.end();mi++){
                tmpvecmi.push_back(jitem(jnotation(mi->first) ,jnotation(mi->second)));
            }
            notationstr=jset(tmpvecmi);

        }

string itype="\"type\": \"Feature\"";
string iproperties="\"properties\":"+notationstr;

//coordinate process
vector<string> lonlat;
stringstream ss;
ss<<lon;
ss<<" ";
ss<<lat;
string lonstr;
string latstr;
ss>>lonstr;
ss>>latstr;
lonlat.push_back(lonstr);
lonlat.push_back(latstr);
string coordinatearray=jarray(lonlat);
string coordinates;
coordinates= jitem("\"coordinates\"",coordinatearray);
string typestr="\"type\":\"Point\"";
//generate geometric
vector<string> geompropvec;
geompropvec.push_back(typestr);
geompropvec.push_back(coordinates);
string geomprop=jset(geompropvec);

string igeometry=jitem("\"geometry\"",geomprop);

//edit all i item
vector<string> container ;
container.push_back(itype);
container.push_back(iproperties);
container.push_back(igeometry);
rtstr=jset(container);


return rtstr;
}
std::string geojlinestring(const std::vector<spatialtime::Coordinate>&ftset,const std::map<std::string,std::string>& notation){


string rtstr;
   //edit notation
        string notationstr;
        {


            vector<string> tmpvecmi;
            for(map<string,string>::const_iterator mi=notation.begin();mi!=notation.end();mi++){
                tmpvecmi.push_back(jitem(jnotation(mi->first) ,jnotation(mi->second)));
            }
            notationstr=jset(tmpvecmi);

        }

string itype="\"type\": \"Feature\"";
string iproperties="\"properties\":"+notationstr;

//edit coordinate

vector <string>coorsets;
for(const Coordinate& ci:ftset){
    vector<string> forinpvec;
    stringstream ss;
    ss<<ci.X;
    ss<<" ";
    ss<<ci.Y;
    string lonstr,latstr;
    ss>>lonstr;
    ss>>latstr;
    forinpvec.push_back(lonstr);
    forinpvec.push_back(latstr);
    coorsets.push_back(jarray(forinpvec));
}
string setofpoint=jarray(coorsets);

string coordinates=jitem("\"coordinates\"",setofpoint);
string typestr="\"type\":\"LineString\"";
vector<string> forgemsetstr;
forgemsetstr.push_back(typestr);
forgemsetstr.push_back(coordinates);

string gemsetstr=jset(forgemsetstr);
    string igeometry=jitem("geometry",gemsetstr);
//edit all i item
    vector<string> forrtstr;
    forrtstr.push_back(itype);
    forrtstr.push_back(iproperties);
    forrtstr.push_back(igeometry);
rtstr=jset(forrtstr);


return rtstr;
}


