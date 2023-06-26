#ifndef InterfaceTool_h
#define InterfaceTool_h
#include<string>
#include<vector>
#include<ctime>
#include<map>
#include<utility>
#include<sstream>
#include"SpatialTimeProcessor.hpp"
#include"MatrixValueTool.hpp"
#include"WeatherFacade.hpp"
std::map<std::string,std::string> readDengueINI(std::string inifn_);
 //std::vector<NeuronNode> BasicLoading(std::string fn_,int factorNum=2,bool T_flg=true);
std::vector<std::vector<double>>WeatherTableGetter(const std::vector<spatialtime::Coordinate>&veccor,const std::vector<std::tm>&vectm,const std::vector<std::pair<std::string,int>>&waabd,Weatherfacade& weatherController,const int QPROCNUM);
std::vector<std::pair<std::string,int>> decodeWeatherAttributeAndTraceBackDay(const std::string& waatbd);
class DengueOrginalData{
private:
    std::vector<std::string>prop_;

public:
    std::vector<std::string> Titles;

    DengueOrginalData(const std::string& fn_);
    std::string getDataline(int ln) const;
    std::string getSingleData(int r_,int c_)const;
    int size()const;
    int getColIndexByAttribute(const std::string& atr)const;
    int getRowIndexByID(int id)const;
};

bool writebackupbatch(const std::string &fn_,std::stringstream&bat_);

#endif // InterfaceTool_h
