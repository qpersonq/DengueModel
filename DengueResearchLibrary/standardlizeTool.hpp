#ifndef standardlizetool_hpp
#define standardlizetool_hpp
#include<vector>
#include<fstream>
class standardlizeInformation{
public:
    double mean=0;
    double stdev=0;
    standardlizeInformation(double m,double s){
        mean=m;
        stdev=s;
    }
    standardlizeInformation(){;}
    double recover(double) const;
    double standardlize(double)const ;

    void save(std::ofstream& os)const;
    void load(std::ifstream& is);

};

double getMean(const std::vector<double>& dim);
double getStddev(const std::vector<double>& dim);
std::vector<double> standardlizeSeries(const std::vector<double>& ser,const standardlizeInformation&inf);
std::vector<double> recoverSeries(const std::vector<double>& ser,const standardlizeInformation&inf);


#endif // standardlizetool_hpp
