#ifndef WeatherFacade
#define WeatherFacade
#include<string>
#include<vector>
#include<map>
#include<fstream>
#include<ctime>
#include<set>
#include"SpatialTimeProcessor.hpp"
#include"CoordinateTransmationTWD97.hpp"
class wTable{
public:
    std::vector<std::vector<std::string>> table;
    std::map<std::string,int> titlehash;
    bool wTableReader(std::istream& fin);
    bool wTableWriter(std::ostream& fout)const ;
};

class Weatherfacade{
private:
const int stationNumber=594;
protected:
 //bool title_flag_=false;




 std::string DBPATH;
public:
      std::set<std::string> badtablefile;
      std::map<std::string,wTable> weathertables;
      bool readTable(std::string sid,int yy,int mm);

 std::vector<std::vector<std::string>>StationInformation;
 //std::vector<std::string> title;
 Weatherfacade(std::string statpth,std::string dpth);
 std::vector<std::string> getPositionBySID(std::string sid)const;
 std::vector<std::vector<std::string>> getLineByDay(int yy,int mm,int day);
 void OptimizeReader(int yy,int mm,int optunit=12);
 std::string PageNameHelper(std::string sid,int yy,int mm)const;
 double interpolation(const spatialtime::Coordinate&oricor,const std::vector<spatialtime::Coordinate>&corvec,const std::vector<double>&vvec )const;
 double getSingleByDay(std::string indexstr,const spatialtime::Coordinate& lonlat ,int yy,int mm,int day);
 double getSingleByDay(std::string indexstr,const spatialtime::Coordinate& lonlat ,const std::tm&tmt);

};


#endif // WeatherFacade
