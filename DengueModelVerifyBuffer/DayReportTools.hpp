#ifndef DAYREPORTTOOLS
#define DAYREPORTTOOLS
#include<tuple>
#include<fstream>
#include<iostream>
#include<map>
#include<string>
#include<vector>
#include"DynamicFullConnectedDengueTree.hpp"

//process slice of day
std::vector<std::tuple<time_t,double,double,double>>getTruePositionSliceFromDynamicFullConnectedDengueTree (const  DynamicFullConnectedDengueTree& dfcdtr);


//format tools
std::map<time_t,double> refillTimesTuplesBubbles(time_t start_t,time_t end_t,time_t add_t,const std::map<time_t,double>& tupll);


class DayReportController{
private:
  DynamicFullConnectedDengueTree* dynamicfullconnecttree_;
  std::vector<std::tuple<time_t,double,double,double>> SliceTable;
  std::map<time_t,double> StoredALLBuffers_D;
  std::map<time_t,double> StoredTPBuffers_D;
  std::map<time_t,double> StoredALLBuffers_ND;
  std::map<time_t,double> StoredTPBuffers_ND;
  time_t StartTime_;
  time_t EndTime_;


protected:
std::map<time_t,std::tuple<int,int,int>> PointsProcessor()const;// all,prd,imported
std::map<time_t,double> RadProcessor()const;


public:

DayReportController(  DynamicFullConnectedDengueTree* dfcdtree,
                      std::vector<std::tuple<time_t,double,double,double>> slicetable,
                      std::map<time_t,double> stallbuff_d,
                      std::map<time_t,double> sttpbuff_d,
                      std::map<time_t,double> stallbuff_nd,
                      std::map<time_t,double> sttpbuff_nd,
                      time_t start_t,
                      time_t end_t
                    )
                    {
                    dynamicfullconnecttree_=dfcdtree;
                    SliceTable=(slicetable);
                    StoredALLBuffers_D=(stallbuff_d);
                    StoredTPBuffers_D=(sttpbuff_d);
                    StoredALLBuffers_ND=(stallbuff_nd);
                    StoredTPBuffers_ND=(sttpbuff_nd);
                    StartTime_=(start_t);
                    EndTime_=(end_t);

                    };


//bool makeReport();
bool writeReport(std::ostream& os,bool wrcap=true );







const  std::vector<std::string> ReportItemsString=
  {
  "Date",
  "AllCs","ImpCs","PrCs","CsRecall","CsRecallFix",
  "RadLb","RadUb",
  "BuffA_ND","TPBuffA_ND","FPBuffA_ND","BuffPrecision_ND",
  "BuffA_D","TPBuffA_D","FPBuffA_D","BuffPrecision_D",
  "Density_ND","Density_D",
  "MixF1_ND","MixF1_D","MFF1_D"
  };


};


#endif // DAYREPORTTOOLS
