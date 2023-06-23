#ifndef DengueMegaDataController_hpp
#define DengueMegaDataController_hpp
#include<string>
#include<vector>
#include<utility>
#include<map>
#include<fstream>
class DengueMegaDataControl{
protected:
    const std::string FilePath__;
    std::ifstream DengueMegaFilePointer;
    std::map<int,long long > Pyramid_;
    bool isCreatedPyramid_=false;
    bool isPreloded_=false;

    bool createPyramid();

    std::vector<std::vector<std::string>> Pool_;
public:
    std::vector<std::string> Titles_;
    DengueMegaDataControl(const std::string&fn_,bool crPyra=true);
    //DengueMegaDataControl(const std::string&fn_,int lowboundid_,int upboundid_);
    bool loading_(int lowboundid_,int upboundid_);

    bool reset();
    std::string getStringData(int r_,int c_)const;
    double getRealData(int r_,int c_)const;


    std::vector<std::string> getStringLine(int r_,int hpos_,int cnt_)const;
    std::vector<double> getRealLine(int r_,int hpos_,int cnt_)const;

    std::vector<std::vector<std::string>>getPool()const;

    int rowsize()const;
    int colsize()const;

    int getCowIndex(const std::string& title)const;

    ~DengueMegaDataControl();

    friend class HotfixMegaData4Prediction;

};





#endif // DengueMegaDataController_hpp
