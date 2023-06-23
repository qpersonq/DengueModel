#ifndef neuron_hpp
#define neuron_hpp

#include<vector>


class neuron{
public:
std::vector<double> weight;



neuron(int inisz_);


double getWeight(int index_)const;
bool setWeight(int index_,double val_);
std::vector<double> getWeight()const;
bool setWeight(const std::vector<double>&sw);
neuron operator=(const neuron &t);
};
#endif // neuron_hpp


