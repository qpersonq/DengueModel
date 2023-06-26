#ifndef AECGISRASTERPROCESSOR_hpp
#define AECGISRASTERPROCESSOR_hpp
#include<string>
#include<vector>
#include<fstream>
class ArcGISRasterGenerator{
private:
    int    ncol,nrow;
    double xllcorne, yllcorner;
    double cellsize;
    double NODATA_value;

    const std::string FileName;
    const int InformationLNNM__=6;
    const double replaceNoDataValue=0.0;

    bool isCreatePyrimid_=false;

    std::ifstream diskpointer;
    std::vector<long long>Pyrimid_;


    std::vector<int> MatrixCalculator(const std::vector<double>& corr)const;

public:
    ArcGISRasterGenerator(std::string fn_,bool createPyrimid=true);
    void showInformation()const;
    double getValue(const std::vector<double>& corr);
    std::vector<double> getLeftUp() const;
    bool CreatePyrimid();
    ~ArcGISRasterGenerator();

};
#endif
