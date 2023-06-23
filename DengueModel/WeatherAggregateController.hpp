#ifndef WeatherACL_hpp
#define WeatherACL_hpp
#include<vector>
#include<string>

enum VectorAggregateSignal{MAX,MIN,SUM,MEAN,STDEV};
VectorAggregateSignal String2AggregateSignal(std::string vstr);
std::string VectorAggregateSignal2String(VectorAggregateSignal vas);
double VectorLineAggregate(const std::vector<double>& vl,VectorAggregateSignal sig);
#endif // WeatherACL_hpp
