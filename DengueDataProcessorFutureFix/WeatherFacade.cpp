#include<iostream>
#include<fstream>
#include<algorithm>
#include<sstream>
#include<mutex>
#include"WeatherFacade.hpp"
#include"SpatialTimeProcessor.hpp"
using namespace spatialtime;
using namespace std;
mutex weathermutex;
template<typename Numeric>
bool is_number(const std::string& s)
{
    Numeric n;
    return((std::istringstream(s) >> n >> std::ws).eof());
}



    bool wTable::wTableReader(std::istream& fin){
        string tmpstr;
        bool tltflg=false;
        while(getline(fin,tmpstr)){

                vector<string> tmpline;
                //cout<<tmpstr<<endl;
                //replace no value and ...
                string ddstr;
                for(int i=0;i<tmpstr.size();i++){

                    if(tmpstr[i]==','&& tmpstr[i+1]==','){
                        ddstr+=",...,";
                    }
                    else ddstr+=tmpstr[i];
                }
                tmpstr=ddstr;
                replace(tmpstr.begin(),tmpstr.end(),' ','_');
                replace(tmpstr.begin(),tmpstr.end(),',',' ');

                stringstream ss;
                ss<<tmpstr;
                while(ss>>tmpstr){
                    tmpline.push_back(tmpstr);

                }
                table.push_back(tmpline);
                 if(!tltflg){
                    for(int ssi=0;ssi<tmpline.size();ssi++){
                        titlehash[tmpline[ssi]]=ssi;
                    }

                    tltflg=true;
                }
        }
        return true;
    }
    bool wTable::wTableWriter(std::ostream& fout)const {
        for(const vector<string>& i:table){
            for(const string & j:i){
                fout<<j<<",";
            }
            fout<<endl;
        }
    return true;
    }
Weatherfacade::Weatherfacade(std::string statpth,std::string dpth){
DBPATH=(dpth);
ifstream sfin(statpth);
if(!sfin.is_open()){
    cerr<<"no weather station data csv!"<<endl;
    exit(-1);
}
wTable wtable;
wtable.wTableReader(sfin);
//wtable.wTableWriter(cout);
for(int i=1;i<=stationNumber;i++){
    vector<string> ttl;
    ttl.resize(3);
    ttl[0]=wtable.table[i][0];
    ttl[1]=wtable.table[i][3];
    ttl[2]=wtable.table[i][4];
    StationInformation.push_back(ttl);
    //for(auto l:ttl)cout<<l<<' '<<endl;
}

sfin.close();
}


bool Weatherfacade::readTable(std::string sid,int yy,int mm){
    stringstream ss;    ss<<yy<<" ";
    string tt;if(mm<10) ss<<'0'<<mm;else ss<<mm;
    string tmpy,tmpm;    ss>>tmpy;ss>>tmpm;
    string fnn=DBPATH+"//"+sid+"_"+ tmpy+"-"+ tmpm+".csv";
    ifstream fin(fnn);
    if(!fin.is_open())return false;

    wTable wtbl;
    wtbl.wTableReader(fin);
    weathertables[sid]=wtbl;


    return true;
}
std::vector<std::string> Weatherfacade::getPositionBySID(std::string sid)const{
vector<string> rt;
for(int i=0;i<StationInformation.size();i++){
   // cout<<StationInformation[i][0]<<endl;
    if(StationInformation[i][0]==sid){
        //rt.push_back(StationInformation[i][0]);

        rt.push_back(StationInformation[i][1]);
        rt.push_back(StationInformation[i][2]);
        //cout<<rt[0];
        //cout<<StationInformation[i][0];
        break;
    }

}
//cout<<rt.size();

return rt;
}

string Weatherfacade::PageNameHelper(string sid,int yy,int mm)const{
    stringstream ssdyymm;
    ssdyymm<<yy;
    ssdyymm<<"-";
    if(mm<10)ssdyymm<<"0"<<mm;
    else ssdyymm<<mm;
    string yymmstr;
    ssdyymm>>yymmstr;
return    sid+"_"+yymmstr;
}
void Weatherfacade::OptimizeReader(int yy,int mm,int optunit){
 for(int k=0;k<optunit;k++){
        mm+=1;
        if(mm>12){
            yy+=1;
            mm=1;
        }

    vector<string>fndb;
    for(vector<string> sinf :StationInformation){//make f name iterator by station

    fndb.push_back(PageNameHelper(sinf[0],yy,mm));


    }
    for(string fnn__:fndb){

        if(weathertables.find(fnn__)!=weathertables.end())  { continue;}
        if(badtablefile.find(fnn__)!=badtablefile.end())continue;
        ifstream fin(DBPATH+"//"+fnn__+".csv");
        if(!fin.is_open()){
                fin.close();
                weathermutex.lock();
                badtablefile.insert(fnn__);
                weathermutex.unlock();
            //cout<<fnn__+".csv fail!"<<endl;
                continue;
        }
        wTable wtbl;

        wtbl.wTableReader(fin);

        wtbl.table.erase(wtbl.table.begin());
        weathermutex.lock();
        weathertables[fnn__]=wtbl;
        weathermutex.unlock();


        fin.close();
    }
 }
}
 std::vector<std::vector<std::string>> Weatherfacade::getLineByDay(int yy,int mm,int day){
    std::vector<std::vector<std::string>>rt;




    vector<string>fndb;
    for(vector<string> sinf :StationInformation){//make f name iterator by station

    fndb.push_back(PageNameHelper(sinf[0],yy,mm));




    }
//for(auto ai:fndb) cout<<ai<<endl;
    //
    vector<string> safegetter;
    for(string fnn__:fndb){

        if(weathertables.find(fnn__)!=weathertables.end())  {safegetter.push_back(fnn__); continue;}
        if(badtablefile.find(fnn__)!=badtablefile.end())continue;
        ifstream fin(DBPATH+"//"+fnn__+".csv");
        if(!fin.is_open()){
                fin.close();
                weathermutex.lock();
                badtablefile.insert(fnn__);
                weathermutex.unlock();
                //cout<<fnn__+".csv fail!"<<endl;
                continue;}
        wTable wtbl;

        wtbl.wTableReader(fin);

        wtbl.table.erase(wtbl.table.begin());
        weathermutex.lock();
        weathertables[fnn__]=wtbl;
        weathermutex.unlock();
        safegetter.push_back(fnn__);


        fin.close();
    }
/*putting */
    for(string fnnstr_:safegetter){
            int lnnno=day-1;
            vector<string> vsss;

            vsss=( weathertables[fnnstr_].table[lnnno]);
            string tstr=fnnstr_;
            tstr.resize(6);
            vector<string> gpbsid=getPositionBySID(tstr);
            vsss.insert(vsss.begin(),gpbsid.begin(),gpbsid.end());
            vsss.insert(vsss.begin(),tstr);
            rt.push_back(vsss);




    }



    return rt;
}
double Weatherfacade::interpolation(const spatialtime::Coordinate&oricor,const vector<spatialtime::Coordinate>&corvec,const vector<double>&vvec )const{
if(vvec.size()!=corvec.size()){
    cerr<<"error interpolation input"<<endl;
    exit(-1);
}
double dist[vvec.size()];
double distsum=0;
for(int i=0;i<corvec.size();i++){
    dist[i]=getDistant(oricor,corvec[i]);
    //pow2
    dist[i]*=dist[i];dist[i]*=dist[i];
    distsum+=dist[i];
}
double rtval=0.0;
for(int i=0;i<vvec.size();i++){
rtval+=vvec[i]*(dist[i]/distsum);
}

return rtval;

}

double Weatherfacade::getSingleByDay(std::string indexstr,const spatialtime::Coordinate& lonlat ,int yy,int mm,int day){


 //get my position
  CoordinateTransform*  ctf=CoordinateTransform::getInstance();
  Coordinate mypos;
  if(lonlat.CoordinateType!="twd97")  mypos=ctf->Cal_lonlat_To_twd97(lonlat.X,lonlat.Y);
  else mypos=lonlat;
 vector<vector<string>>lprop= getLineByDay(yy,mm,day);
 vector<double> effvvec;
 vector<Coordinate> effcorvec97;

 for(int i=0;i<lprop.size();i++){
     int ptr=weathertables[PageNameHelper(lprop[i][0],yy,mm)].titlehash[indexstr];

     ptr+=3;
     if(lprop[i][ptr]=="T")lprop[i][ptr]="1.1";
     if(is_number<double>(lprop[i][ptr])){
     stringstream ss;
     ss<<lprop[i][ptr];
      double xoval ;
      ss>> xoval;


      stringstream csscv;
      csscv<<lprop[i][1]<<" "<<lprop[i][2];
      double lx_,ly_;
      csscv>>lx_>>ly_;
      Coordinate hispos= ctf->Cal_lonlat_To_twd97(lx_,ly_);
        effvvec.push_back(xoval);
        effcorvec97.push_back(hispos);

     }
 }
 //cout<<effcorvec97.size()<<endl;
 return interpolation(mypos,effcorvec97,effvvec);
}


double Weatherfacade::getSingleByDay(std::string indexstr,const spatialtime::Coordinate& lonlat ,const std::tm&tmt){

    //ymd

return getSingleByDay( indexstr,  lonlat ,tmt.tm_year+1900,tmt.tm_mon+1,tmt.tm_mday);

}




