#ifndef DynamicFullConnectedDengueTree_hpp
#define DynamicFullConnectedDengueTree_hpp
#include"FullConnectedDengueTree.hpp"
#include<vector>
class DynamicFullConnectedDengueTree:public FullConnectedDengueTree{
protected:
    std::vector<std::vector<double>> Bound_;
    //lginf,lgsup,distinf,distsup
public:
     bool ConsiderOverseaFlag_=true;

    DynamicFullConnectedDengueTree(int atrbsz,const std::vector<double>& atrprp);
    void setBound(const std::vector<std::vector<double>>&bd);
    std::vector<std::vector<double>>getBound()const;
    bool Connect(bool FBMODE);

};

#endif // DynamicFullConnectedDengueTree_hpp
