
#ifndef SPATIOTIMEBUFFERCALCULATOR
#define SPATIOTIMEBUFFERCALCULATOR


#include<ctime>
#include<vector>
#include<tuple>
#include"BufferDissolver.hpp"
#include<time.h>
#include<map>
class SpatioTimeBufferCalculator{
private:

int CoreNumber=3;


protected:


public:
int DissolveMode=0;

std::vector<std::tuple<time_t,double,double,double>> InputTuples;
SpatioTimeBufferCalculator& inputSpatioTimeBuffer(time_t start_time_,int laglb,int lagub,double posx,double posy,double rad,time_t end_time_ = 0x7fffffffffffffff );
SpatioTimeBufferCalculator& inputSpatioTimeBuffer(time_t cur_time_,double posx,double posy,double rad,time_t end_time_ = 0x7fffffffffffffff);
void setCoreNumber(int cn);
double calculateDissovleBufferArea(time_t ttday,const int pointPerCircle=18)const;
double calculateNoDissovleBufferArea(time_t ttday)const;

std::tuple<int,double> calculateDissovleBufferSpatioTime(const int pointPerCircle=18)const;
std::map<time_t,double> StoreDissolveBufferAreaByTimeT;

std::tuple<int,double> calculateNODissovleBufferSpatioTime()const;
std::map<time_t,double> StoreNODissolveBufferAreaByTimeT;
};




#endif // SPATIOTIMEBUFFERCALCULATOR
