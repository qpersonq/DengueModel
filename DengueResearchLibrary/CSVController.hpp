#ifndef CSVCONTROLLER_HPP
#define CSVCONTROLLER_HPP

#include<vector>
#include<string>

#include<fstream>

class CSVController{
private:
    bool isPreloded_=false;
    //const  std::string  DefaultSpaceTag= "ABCDEZZQ";
    std::string processSpcce(std::string sln)const;
    std::vector<std::string> splitTool(const std::string& str_)const;
public:

    std::vector<std::vector<std::string>> Pool_;
    std::vector<std::string> Titles_;
    void clear();


    std::string getStringData(int r_,int c_)const;
    double getRealData(int r_,int c_)const;
    int getIntData(int r_,int c_)const;


    std::vector<std::string> getStringLine(int r_,int hpos_,int cnt_)const;
    std::vector<double> getRealLine(int r_,int hpos_,int cnt_)const;

    std::vector<std::vector<std::string>>getPool()const;

    int rowsize()const;
    int colsize()const;

    int getCowIndex(const std::string& title)const;

    bool insertColumn(int colid,std::string title,const std::vector<std::string>& insrts );
    bool insertRow(int rowid,const std::vector<std::string>& inserts);

    bool read(std::ifstream& readfin);
    bool write(std::ofstream& writecsv)const ;

};




#endif // CSVCONTROLLER_HPP





