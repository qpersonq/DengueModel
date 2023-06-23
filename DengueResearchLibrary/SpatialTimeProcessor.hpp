#ifndef spatialtimeprocessor_h
#define spatialtimeprocessor_h
#include<ctime>
#include<string>
#include<vector>
#include<iostream>
namespace spatialtime{
struct Coordinate{
std::string CoordinateType;
double X;
double Y;
};

 std::tm SmartParseDateString(std::string dstr);
 std::string convertTime2String(const tm& tmobj);
 time_t tm2time_t(const std::tm& tmmd);
 tm time_t2tm(time_t tt);
 int calDayLag(time_t laglb,time_t lagub,time_t strtm,time_t endtm, const time_t adaysec=86400);
double getDays(tm atime,tm btime);
double getDistant(const Coordinate &c1,const Coordinate &c2);
std::vector<double> getVector(const Coordinate &c1,const Coordinate &c2);
std::vector<double> NormalizeVector(const std::vector<double>& iv);
std::ostream& operator<<(std::ostream& os,const tm c0);
std::tm calculateTime(std::tm cur,int sec);
std::vector<int>convert2YYMMDD(const std::tm& tmt);
std::tm convert2TM(int yy,int mm,int dd);
};


#endif
