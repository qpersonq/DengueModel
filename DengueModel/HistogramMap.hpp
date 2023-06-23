#ifndef HistorgramMap_hpp
#define HistorgramMap_hpp
#include<vector>
#include<fstream>
class HistorgramMap{
protected:

    // x<=val<y
    bool isFreQMap=false;
    double CntFreQ=0.000000000000000000000001;
    std::vector<double> InfIntervelSet;
    std::vector<double> ValueSet;
public:
    //Control sig
    bool isEqualInterval=true;


    HistorgramMap();
    HistorgramMap(const std::vector<double>&  its);
    HistorgramMap(double lb,double ub,double intvl);
    void set(const std::vector<double>&  its);
    void set(double lb,double ub,double intvl);
    bool import(double val);
    std::vector<double> getInfIntervelSet()const;
    double getCount(double bl)const;
    double getPDF(double vl)const;
    double getCDF(double vl)const;
    double getInverseCDF(float CDF)const;

    void save(std::ofstream& fout)const;
    void load(std::ifstream& fin);

    std::vector<std::vector<double>>getBoundAndCount()const;

    friend HistorgramMap PDFPlus(const  HistorgramMap& a,const  HistorgramMap& b);
    friend HistorgramMap CountPlus(const  HistorgramMap& a,const  HistorgramMap& b);
    friend HistorgramMap PDFPlusBatch(const std::vector<HistorgramMap>&hsmp);
    friend void Smooth(HistorgramMap& hm,float p);

};

HistorgramMap PDFPlus(const  HistorgramMap& a,const  HistorgramMap& b);
HistorgramMap CountPlus(const  HistorgramMap& a,const  HistorgramMap& b);
void Smooth(HistorgramMap& hm,float pc=0.25);
HistorgramMap PDFPlusBatch(const std::vector<HistorgramMap>&hsmp);
#endif // HistorgramMap_hpp

