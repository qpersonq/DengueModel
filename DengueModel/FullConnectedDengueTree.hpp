#ifndef FullConnectedDengueTree_hpp
#define FullConnectedDengueTree_hpp
#include"NeuronTree.hpp"

class FullConnectedDengueTree :public processTree{
private:

protected:

public:
    //parameter
    double LagInf=7,LagSup=50; //open set
    double DistanceSup=1000;   //open set
    double DistanceInf=0;
    bool ConsiderOverseaFlag_=true;


    FullConnectedDengueTree(int atrbsz,const std::vector<double>& atrprp);
    bool setParameter(double lginf,double lgsup,double distsup);
    bool setParameter(double lginf,double lgsup,double distinf,double distsup);
    bool Connect();

    std::vector<std::vector<int> >getAllPair()const;

    bool Load(std::ifstream&fin);
    bool Save(std::ofstream&fout)const;

};



#endif //FullConnectedDengueTree
