#ifndef HotfixMegaData4Predition_hpp
#define HotfixMegaData4Predition_hpp
#include<map>
#include<string>
#include"DengueMegaDataController.hpp"
class HotfixMegaData4Predition{
private:


protected:
    int HotfixMoveDays=0;


    std::vector<std::string> InsertionTitle;
    std::map<int,int> IDFinder;
    std::vector<std::vector<std::string> >InserterTable;

public:

    HotfixMegaData4Predition();
    bool readHotfixFile(std::string htinsfile);
    void Hotfix(DengueMegaDataControl* DenMDC);



};







#endif
