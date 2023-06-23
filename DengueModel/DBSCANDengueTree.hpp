#ifndef DBSCANDengueTree_hpp
#define DBSCANDengueTree_hpp
#include"NeuronTree.hpp"
#include<utility>
#include<limits>
class DBSCANDengueTree :public processTree{
private:

protected:

    bool isLowerEps(int idxa,int idxb)const;
    //tool
    const int NOISE =std::numeric_limits<int>::min()+2; //-2;
    const int NOT_CLASSIFIED =std::numeric_limits<int>::min()+1; //-1;

    std::vector<int> CurrentCluster;
    std::vector<std::vector<int> > adjPoints;
    std::vector<bool> visited;

    void checkNearPoints();
    bool isCoreObject(int idx)const ;
    void dfs(int nowidx,int clustid,int lyr);


public:
        std::vector<std::vector<std::pair<int,int>>>ClusterRecoder;

     int minPts=2;
     bool ConsiderOverseaFlag_=true;
    //const double eps;
    //parameter EP
     double LagInf=7,LagSup=50; //open set
     double DistanceSup=1000;   //open set

    DBSCANDengueTree(int atrbsz,const std::vector<double>& atrprp);

    bool DBSCAN();

    int PairControl_select_Flag=0;
    std::vector<std::vector<int> >getAllPair() const;
    bool Load(std::ifstream&fin);
    bool Save(std::ofstream&fout)const;

};



#endif // DBSCANDengueTree_hpp
