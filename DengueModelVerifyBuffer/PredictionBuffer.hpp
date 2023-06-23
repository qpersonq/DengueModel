#ifndef PredictionBuffer_hpp
#define PredictionBuffer_hpp
#include<utility>
#include<map>
#include<vector>
#include<set>
#include"SelfOrganizingMap.hpp"
using namespace std;

std::pair<int ,int> FindActiveBestMatchUnit(const SelfOrganizingMap&som,const std::set<std::pair<int,int>>&an,const std::vector<double>&xv,const std::vector<int>&shield={}  );
#endif // PredictionBuffer_hpp
