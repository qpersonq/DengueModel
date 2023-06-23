#include<iostream>
#include"neuron.hpp"
using namespace std;
neuron::neuron(int inisz_){
weight.resize(inisz_);

}

neuron neuron::operator=(const neuron &t){
    this->weight=t.weight;
return *this;
}

double neuron::getWeight(int index_)const{
   // cout<<weight.at(index_)<<endl;
return weight.at(index_);
}
bool neuron::setWeight(int index_,double val_){
//if(index_>=factorNumber) return false;
*(weight.begin()+index_)=val_;
return true;
}
std::vector<double> neuron::getWeight()const{
    return this->weight;
}
bool neuron::setWeight(const std::vector<double>&sw){
    if(sw.size()!=weight.size()) return false;
    weight=sw;
    return true;
}


