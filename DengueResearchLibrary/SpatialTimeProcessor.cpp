#include<iostream>
#include<ctime>
#include<sstream>
#include<sstream>
#include<algorithm>
#include<utility>
#include"SpatialTimeProcessor.hpp"
#include"CoordinateTransmationTWD97.hpp"

using namespace std;
using namespace spatialtime;

namespace spatialtime
{
double getDays(struct tm atime,struct tm btime)
{
    std::time_t x = std::mktime(&atime);
    std::time_t y = std::mktime(&btime);

    double difference;



     /*std::cout<<" " << std::ctime(&x)<<" /";
     std::cout << std::ctime(&y)<<" ";*/

    if ( x != (std::time_t)(-1) && y != (std::time_t)(-1) )
    {
        difference = std::difftime(y, x) / (60 * 60 * 24);
        /*std::cout << std::ctime(&x);
        std::cout << std::ctime(&y);
        std::cout << "difference = " << difference << " days" << std::endl;*/
    }
    else
    {
        cout<<"fail time";

        exit(-1);
    }

    return difference;
}


double getDistant(const Coordinate &c1,const Coordinate &c2){
    Coordinate cc1=c1,cc2=c2;
    CoordinateTransform *ctf=CoordinateTransform::getInstance();


    if(c1.CoordinateType!="twd97")cc1=ctf->Cal_lonlat_To_twd97(c1.X,c1.Y);
    if(c2.CoordinateType!="twd97")cc2=ctf->Cal_lonlat_To_twd97(c2.X,c2.Y);


return distantTWD97(cc1,cc2);
}
std::vector<double> getVector(const Coordinate &c1,const Coordinate &c2){

return vector<double>{c2.X-c1.X,c2.Y-c1.Y};
}


 std::tm parseDateString(std::string dstr){
    replace(dstr.begin(),dstr.end(),'/',' ');
    stringstream ss;
    ss<<dstr;

    int yy,dd,mm;
    ss>>mm;
    ss>>dd;
    ss>>yy;


    std::tm iit={0,0,0,dd,mm-1,yy-1900};
    time_t curtime=mktime(&iit);
    //curtime+=(60*60*24);
    std::tm *rt=localtime(&curtime);


return *rt;

};
std::tm parseDateStringYearFront(std::string dstr){
    replace(dstr.begin(),dstr.end(),'/',' ');
    stringstream ss;
    ss<<dstr;

    int yy,dd,mm;
    ss>>yy;
    ss>>mm;
    ss>>dd;



    std::tm iit={0,0,0,dd,mm-1,yy-1900};
    time_t curtime=mktime(&iit);
    //curtime+=(60*60*24);
    std::tm *rt=localtime(&curtime);


return *rt;

};
 std::tm SmartParseDateString(std::string dstr){
    replace(dstr.begin(),dstr.end(),'/',' ');
    stringstream ss;
    ss<<dstr;
    int unknownval;
    ss>>unknownval;
    if(unknownval>1000) return(parseDateStringYearFront(dstr));
    return parseDateString(dstr);
 }


 std::string convertTime2String(const tm& tmobj){
 stringstream ss;
 ss<<tmobj.tm_year+1900;
 ss<<" ";
 ss<<tmobj.tm_mon+1;
 ss<<" ";
 ss<<tmobj.tm_mday;
 string ty,tm__,td;
 ss>>ty;
 ss>>tm__;
 ss>>td;

 return ty+"/"+tm__+"/"+td;
 }


 time_t tm2time_t(const std::tm& tmmd){
    tm ttttt=tmmd;
 return mktime(&ttttt);
}
tm time_t2tm(time_t tt){
 return *localtime(&tt);
}


int calDayLag(time_t laglb,time_t lagub,time_t strtm,time_t endtm, const time_t adaysec){
    lagub+=adaysec;
    endtm+=adaysec;
    if(lagub >= endtm) lagub= endtm;
    if(lagub <= strtm) lagub= strtm;

    if(laglb <= strtm) laglb= strtm;
    if(laglb >= endtm) laglb= endtm;

    time_t delsec=(lagub-laglb);
    int day=   delsec/adaysec;
    //day+=1;
    return day;

}




std::tm calculateTime(std::tm cur,int sec){

time_t curtime=mktime(&cur);
//curtime+=(60*60*24);

curtime+=sec;
tm* ntm=localtime(&curtime);
return *ntm;
}







std::vector<int>convert2YYMMDD(const std::tm& tmt){
return vector<int>{tmt.tm_year+1900,tmt.tm_mon+1,tmt.tm_mday};

}
std::tm convert2TM(int yy,int mm,int dd){
    std::tm iit={0,0,0,dd,mm-1,yy-1900};
    time_t curtime=mktime(&iit);
    //curtime+=(60*60*24);
    std::tm *rt=localtime(&curtime);


return *rt;

}




std::ostream& operator<<(std::ostream& os,const tm c0){


os<<c0.tm_year<<","
<<c0.tm_yday<<","
<<c0.tm_wday<<","
<<c0.tm_sec<<","
<<c0.tm_mon<<","
<<c0.tm_min<<","
<<c0.tm_mday<<","
<<c0.tm_isdst<<","
<<c0.tm_hour;

return os;

}



std::vector<double> NormalizeVector(const std::vector<double>& iv){
double bt=0;
for(double r:iv){
    bt+=r*r;
}
bt=sqrt(bt);
if(bt==0) return vector<double>{0,0};
std::vector<double> rt;

for(double r:iv){
    rt.push_back(r/bt);
}

return rt;
}





}

