#ifndef MCMCDengueTree_hpp
#define MCMCDengueTree_hpp
#include"NeuronTree.hpp"


class MCMCDengueTree:public processTree{
private:
    //learning rate initial
    double initLearningRate=0.1;

    //initial epoch
    int RemainEpoch=0;
    int CumulateEpoch=0;
protected:



    //controller tool
    void fixCase();
    void backPropagation(int nodenum_);

    //common tool
    static double generateLearningRateLinear(double init,double tcnt,double tall);
    static double generateLearningRateExp(double init,double tcnt,double tall);

    //parameter
    const double LagInf=6,LagSup=50; //open set
    const double DistanceSup=1000;   //open set

public:
    MCMCDengueTree(int atrbsz,const std::vector<double>& atrprp);

    bool setEpoch(int it__);
    bool trainModel();
    std::vector<std::vector<int> >getAllPair()const;
    bool Load(std::ifstream&fin);
    bool Save(std::ofstream&fout)const;
};
#endif // MCMCDengueTree_hpp
