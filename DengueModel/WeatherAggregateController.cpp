#include<algorithm>
#include<cmath>
#include<map>
#include"WeatherAggregateController.hpp"
using namespace std;
double VectorLineAggregate(const std::vector<double>& vl,VectorAggregateSignal sig){
double rtv=0;
switch(sig){
    case MAX:
        rtv=*max_element(vl.begin(),vl.end());
    break;
    case MIN:
        rtv=*min_element(vl.begin(),vl.end());
    break;
    case SUM:
        for(auto v:vl){
            rtv+=v;
        }
    break;
    case MEAN:
        for(auto v:vl){
            rtv+=v;
        }
        rtv/=vl.size();
    break;
    case STDEV:
        double m=0;
        for(auto v:vl){
            m+=v;
        }
        m/=vl.size();

        for(auto v:vl){
            rtv+=((v-m)*(v-m));

        }
        rtv/=vl.size();
        rtv=sqrt(rtv);

    break;
}
return rtv;
}
std::string VectorAggregateSignal2String(VectorAggregateSignal vas){

return vector<string> {"MAX","MIN","SUM","MEAN","STDEV"}[vas];
}


VectorAggregateSignal String2AggregateSignal(string vstr){
return map<string,VectorAggregateSignal>({{"MAX",MAX},{"MIN",MIN},{"SUM",SUM},{"MEAN",MEAN},{"STDEV",STDEV}})[vstr];
}
