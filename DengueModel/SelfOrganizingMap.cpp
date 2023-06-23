#include<iostream>
#include<sstream>
#include<fstream>
#include<string>
#include<vector>
#include<algorithm>
#include<ctime>
#include<limits>
#include<random>
#include<iomanip>
#include<thread>
#include<queue>
#include"MatrixValueTool.hpp"
#include"SelfOrganizingMap.hpp"
#include <boost/asio/thread_pool.hpp>
#include <boost/asio/post.hpp>
//#include"/Users/Dimitri/Desktop/SomDebug/ThreadPool.h"
using namespace std;
vector<int> generateSeries(int lb,int ub,int app){//close
    if(ub<lb){cerr<<"err genSeries!"<<endl;return {};}
    if((ub-lb)%app ){cerr<<"err genSeries!"<<endl;return {};}
    vector<int> rt;rt.resize( ((ub-lb)/app)+1 );
    vector<int>::iterator rtptr=rt.begin();
    for(int i=lb;i<=ub;i+=app){
            *rtptr=i;
            rtptr++;
    }
return rt;
}
vector<int> generateRandomSeries(vector<int> series){
   int swtm= 3*series.size();
   if(!swtm){cerr<<"generateRandomSeries size =0"<<endl;return series;}

   //std::random_device rdv;
   std::default_random_engine geneg(time(0)) ;
   std::uniform_int_distribution<int> urdistr(0,series.size()-1);

   for(int u=0;u<swtm;u++){
        int ff=urdistr(geneg),
            bb=urdistr(geneg);
    swap(series[ff],series[bb]);

   }

   return series;
}


double EuclideanDistance::EvaluateDistance(const vector<double>& v1, const vector<double>& v2)const {
        double KernelValue = norm(v1 - v2);
        //std::cout<<KernelValue<<std::endl;
        return KernelValue;
}


double RadialKernelDistance::EvaluateDistance(const std::vector<double>&  v1, const std::vector<double>& v2, double sigma)const {
        double KernelValue = exp(-(norm(v1 - v2) / sigma));
        return KernelValue;
}

double RadialKernel::EvaluateKernel(const std::vector<double>&  v1, const std::vector<double>&   v2, double sigma)const{
 double KernelValue = exp(-(norm(v1 - v2) / sigma));
 return KernelValue;
 }
bool GetInputVectors::ImportBatch(const std::vector<std::vector<double>>& dat_){
if(dat_.empty())return false;
//InputVectors=dat_;
//InputVectorSize = InputVectors.back().size();
NumberInputVectors = dat_.size();
int elesz=dat_[0].size();

//dynamic allocate
InputVectors= new vector<double>[NumberInputVectors];
for(int i=0;i<NumberInputVectors;i++){
        InputVectors[i].resize(elesz);
}

for(int i=0;i<NumberInputVectors;i++) {
    for(int j=0;j<elesz;j++){
        InputVectors[i][j]=dat_[i][j];
    }
}



return true;
}
GetInputVectors::~GetInputVectors(){
if(!NumberInputVectors){
    /*for(int i=0;i<NumberInputVectors;i++){
        delete   InputVectors[i];
    }*/
    delete [] InputVectors;
}

}

void Initialize2DMap::InitializeMap(int xsz, int ysz,bool randomize){
     Initialize2DMap::xsize = xsz;
     Initialize2DMap::ysize = ysz;
     MapReadyFlag=true;

     /*
     SOMMap.resize(xsize);
     for(int m=0;m<xsize;m++)SOMMap[m].resize(ysize);
        for(int i=0;i<xsize;i++){
            for(int j=0;j<ysize;j++){
                SOMMap[i][j].resize(InputVectorSize);
            }
        }
     */
     //Dynamic allocate
     SOMMap=new vector<double>*[xsz];
     //cout<<"echo";
     for(int i=0;i<xsz;i++){
        SOMMap[i]=new vector<double>[ysz];
        for(int j=0;j<ysz;j++){
            SOMMap[i][j].resize(InputVectorSize);
        }
     }

     //randomize
     if(randomize){
                    //std::random_device rdv;
                    std::default_random_engine geneg(time(0));
                    std::uniform_real_distribution<double> urdistr(0.5,-0.5);

                     for (int i=0; i<xsize; i++){
                        for (int j=0; j<ysize; j++){
                            for(int k=0;k<InputVectorSize;k++){
                                 SOMMap[i][j][k]=urdistr(geneg)*MaxValueInputData;
                                //cout<<SOMMap[i][j][k]<<endl;
                            }

                            /*
                            Eigen::VectorXd now = Eigen::VectorXd::Random(InputVectorSize);
                            now  = (now.cwiseAbs() / now.cwiseAbs().maxCoeff()) * GetInputVectors::MaxValueInputData; // random values between 0 and 1
                            SOMMap(i,j) = now;*/
                        }
                     }
                    }
//cout<<"r";
 }

Initialize2DMap::Initialize2DMap(int xsz,int ysz,int vecsz){
Initialize2DMap::xsize = xsz;
Initialize2DMap::ysize = ysz;
InputVectorSize=vecsz;

    InitializeMap(xsz,ysz);
//cout<<"r";
}

Initialize2DMap::Initialize2DMap(){
}
Initialize2DMap::~Initialize2DMap(){
    //cout<<"des init 2dmp"<<endl;
    for(int u=0;u<this->xsize;u++){
        delete[] SOMMap[u];
    }
    delete[] SOMMap;
}
int Initialize2DMap::getXSize()const{return xsize;}
int Initialize2DMap::getYSize()const{return ysize;}
int Initialize2DMap::getInputVectorSize()const{return InputVectorSize;}

double GaussianDistribution::Gaussian(double x, double sigma)const{
        double value = exp(-((double)x/(double)sigma));
        return value;
}

double SOMNeighbourhoodFunction::NeighbourhoodFunction(int BMUX, int BMUY, int BMUNowX, int BMUNowY, double SigmaNeighbouring)const {
        double value = (double)(pow(BMUX - BMUNowX,2) + pow(BMUY - BMUNowY,2)) / (double)(2*pow(SigmaNeighbouring,2)) ;
        value = exp(-value);
       // value = exp(value);
       // std::cout  << (double)(pow(BMUX - BMUNowX,2) + pow(BMUY - BMUNowY,2)) << (double)2*pow(SigmaNeighbouring,2) << value << "\n";
        //std::cout << value << "\n";
        return value;
}

std::vector<double> SOMLearningFunction::LearnFunction(const std::vector<double>& BMUNow, const std::vector<double>& Input, int BMUX,int BMUY,int BMUNowX, int BMUNowY,double SigmaNeighbouring, double LearningRate)const{
        std::vector<double>  value = BMUNow + LearningRate * NeighbourhoodFunction(BMUX,BMUY,BMUNowX,BMUNowY,SigmaNeighbouring)*(Input-BMUNow);
        return value;
}

double UpdateSOMLearningRate::UpdateLearningRate(double L0, double LearningRateFinal, double Iter, double lambda)const{
       //false L0 double value = (L0 - LearningRateFinal) * L0*exp(-((double)Iter/(double)lambda)) + LearningRateFinal;
        double value = (L0 - LearningRateFinal) *exp(-((double)Iter/(double)lambda)) + LearningRateFinal;
        return value;
}

double UpdateSOMSigmaNeighbouring::UpdateSigmaNeighbouring(double SigmaNeighbouringZero, double SigmaNeighbourhoodFinal, double Iter, double lambda)const {
        double value = (SigmaNeighbouringZero -SigmaNeighbourhoodFinal) * exp(-((double)Iter/(double)lambda)) + SigmaNeighbourhoodFinal;
        return value;
}







/*
void SOMTrain::ThreadForLearnFunction(int RowMap,int ColMap, vector<double>ivp,int BestXMap, int BestYMap, int CoordX,int  CoordY, double SigmaNeighbouring,double LearningRate ){

 Initialize2DMap::SOMMap[RowMap][ColMap] = LearnFunction(Initialize2DMap::SOMMap[RowMap][ColMap],ivp,BestXMap, BestYMap, CoordX, CoordY, SigmaNeighbouring, LearningRate);


}*/

int SOMTrain::getCurrentIteratorNumber()const{
    return CurrentIteratorNumber;
}
int SOMTrain::getIteratorNumber()const{
    return IteratorNumber;
}

 void SOMTrain::Train(){  //, MatrixOfVectors InputVectors, int xsize, int ysize, MatrixOfVectors SOMMap

    //int Iters=IteratorNumber;

                 // Initialize alpha
                 //SOMTrain::SigmaNeighbouringInitial;
                 //SOMTrain::SigmaNeighbourhoodFinal;
                 //SOMTrain::LearningRateInitial;
                 //SOMTrain::LearningRateFinal;
     //SOMTrain::LearningRate=SOMTrain::LearningRateInitial;
     //SOMTrain::SigmaNeighbouring=SOMTrain::SigmaNeighbouringInitial;
            SOMTrain::lambda = (double)(IteratorNumber)/(double)(SOMTrain::xsize/2);
            SOMTrain::LearningRate = UpdateLearningRate(SOMTrain::LearningRateInitial,SOMTrain::LearningRateFinal, CurrentIteratorNumber, SOMTrain::lambda);
            SOMTrain::SigmaNeighbouring = UpdateSigmaNeighbouring(SOMTrain::SigmaNeighbouringInitial,SOMTrain::SigmaNeighbourhoodFinal,  CurrentIteratorNumber, SOMTrain::lambda);
     //std::cout <<  SOMTrain::SigmaNeighbouring <<std::endl;
            //SOMTrain::lambda = (double)(IteratorNumber)/(double)(SOMTrain::xsize/2);
                //Thread
                /*
                queue<void(**)> INFQUEUE_;
                bool CLCTHREAD_STOP_SIGNAL=false;
                thread CLCTHREAD_([&](){
                                    while(!CLCTHREAD_STOP_SIGNAL){
                                    while(!INFQUEUE_.empty()){
                                    INFQUEUE_.front();

                                    INFQUEUE_.pop();
                                    }

                                  }
                                } );
                 */

     //vector<double> BMU;
        //setRandomGeneratorAdvance(0,SOMTrain::NumberInputVectors-1);
    {//EPOCH once
        //if(isShowIteratorNumber)std::cout<<"Itr:"<<setw(4)<<setiosflags(ios::left)<<CurrentIteratorNumber+1<<" ";
            //randomize
        vector<int> randseri_=generateRandomSeries(generateSeries(0,NumberInputVectors-1,1));

        int BestXMap, BestYMap = 0;
        double Score=numeric_limits<double>::max(), ScoreNow ;




         for (int jit=0; jit<NumberInputVectors;jit++){ //XX the order should be random
         //int j = GenerateRandomNumber(SOMTrain::NumberInputVectors);
         //int bkj=j;
         //j = getRandomGeneratorAdvance();//std::cout<<j<<std::endl;
         //j = GenerateRandomNumber(SOMTrain::NumberInputVectors);
            // Find the BMU
             for (int RowMap=0;RowMap<SOMTrain::xsize;RowMap++){
                 for (int ColMap=0;ColMap<ysize;ColMap++){

                     ScoreNow = EvaluateDistance(Initialize2DMap::SOMMap[RowMap][ColMap], InputVectors[ randseri_[jit]  ]); //XX ,1.0 sigma
                     if (ScoreNow < Score){Score = ScoreNow; BestXMap=RowMap, BestYMap=ColMap;}
                  }
             }
             //cout<<"bmu"<<jit<<endl;
             //cout<<"echo1"<<endl;
             //cout<<"xysz"<<xsize<<","<<ysize<<endl;
             //BMU = Initialize2DMap::SOMMap[SOMTrain::BestXMap,SOMTrain::BestYMap];
             // Update the BMUs

             for (int RowMap=0;RowMap<SOMTrain::xsize;RowMap++){
                 for (int ColMap=0;ColMap<ysize;ColMap++){


                     int CoordX = RowMap;
                     int CoordY = ColMap;
                    // if (CoordX < (SOMTrain::xsize/2)) {CoordX = RowMap;}
                     //if (CoordX >= (SOMTrain::xsize/2)){CoordX = (SOMTrain::xsize/2) - RowMap%(SOMTrain::xsize/2);}
                     //if (CoordY < (SOMTrain::ysize/2)) {CoordY = ColMap;}
                     //if (CoordY >= (SOMTrain::ysize/2)){CoordY = (SOMTrain::ysize/2) - ColMap%(SOMTrain::xsize/2);}
                     //std::cout << CoordX << "\n";// ;<< "  " << CoordY << std::endl;
            //important
            Initialize2DMap::SOMMap[RowMap][ColMap] = LearnFunction(Initialize2DMap::SOMMap[RowMap][ColMap],SOMTrain::InputVectors[randseri_[jit]],BestXMap, BestYMap, CoordX, CoordY, SOMTrain::SigmaNeighbouring, SOMTrain::LearningRate);
                //INFQUEUE_.push((void**)(*ThreadForLearnFunction)(RowMap,ColMap,InputVectors[randseri_[jit]],BestXMap,BestYMap,CoordX,CoordY,SigmaNeighbouring,LearningRate));
                     //cout<<BestXMap<<","<<BestYMap<<endl;
                     //for(auto v:Initialize2DMap::SOMMap[RowMap][ColMap])cout<<v<<" ";cout<<endl;
                     //Initialize2DMap::SOMMap(RowMap,ColMap) = BMUUpdated;
                     //std::cout << BMUUpdated << std::endl;
                 //cout<<"echo2"<<endl;
                 }
             }
             //j+(i*InputVectors.rows());
            //j=bkj;
            if(is_TrainingUpdateShowEvery50k&&((jit+1)%50000==0))cout<<"update"<<jit+1<<";";
         }
        //if(is_TrainingUpdateShowEvery50k)cout<<endl;
     // Update Learning Rate
         //SOMTrain::LearningRate = UpdateLearningRate(SOMTrain::LearningRateInitial,SOMTrain::LearningRateFinal, CurrentIteratorNumber, SOMTrain::lambda);
         //SOMTrain::SigmaNeighbouring = UpdateSigmaNeighbouring(SOMTrain::SigmaNeighbouringInitial,SOMTrain::SigmaNeighbourhoodFinal,  CurrentIteratorNumber, SOMTrain::lambda);


    }
     CurrentIteratorNumber+=1;

            /* only for thread
            CLCTHREAD_STOP_SIGNAL=true;
             CLCTHREAD_.join();*/
}


void SOMTrain::ParallelTrain(const int CORENUM){  //, MatrixOfVectors InputVectors, int xsize, int ysize, MatrixOfVectors SOMMap

    /*
    if(realIteratorNumber>IteratorNumber-CurrentIteratorNumber){
        cerr<<"realIteratorNumber too big"<<endl;
        return;
    }
    if(!MapReadyFlag){
        cerr<<"map not ready for training"<<endl;
        return;
    }
    int Iters=IteratorNumber;
    */

                 // Initialize alpha
                 //SOMTrain::SigmaNeighbouringInitial;
                 //SOMTrain::SigmaNeighbourhoodFinal;
                 //SOMTrain::LearningRateInitial;
                 //SOMTrain::LearningRateFinal;
     //SOMTrain::LearningRate=SOMTrain::LearningRateInitial;
     //SOMTrain::SigmaNeighbouring=SOMTrain::SigmaNeighbouringInitial;
     SOMTrain::lambda = (double)(IteratorNumber)/(double)(SOMTrain::xsize/2);
     SOMTrain::LearningRate = UpdateLearningRate(SOMTrain::LearningRateInitial,SOMTrain::LearningRateFinal, CurrentIteratorNumber, SOMTrain::lambda);
     SOMTrain::SigmaNeighbouring = UpdateSigmaNeighbouring(SOMTrain::SigmaNeighbouringInitial,SOMTrain::SigmaNeighbourhoodFinal,  CurrentIteratorNumber, SOMTrain::lambda);


     //std::cout <<  SOMTrain::SigmaNeighbouring <<std::endl;
     //SOMTrain::lambda = (double)(CurrentIteratorNumber)/(double)(SOMTrain::xsize/2);
                //Thread
                /*
                queue<void(**)> INFQUEUE_;
                bool CLCTHREAD_STOP_SIGNAL=false;
                thread CLCTHREAD_([&](){
                                    while(!CLCTHREAD_STOP_SIGNAL){
                                    while(!INFQUEUE_.empty()){
                                    INFQUEUE_.front();

                                    INFQUEUE_.pop();
                                    }

                                  }
                                } );
                 */

     //vector<double> BMU;
        //setRandomGeneratorAdvance(0,SOMTrain::NumberInputVectors-1);
//ThreadPool THRPOOL(CORENUM);

     {//epoch 1
        //if(isShowIteratorNumber)std::cout<<"Itr:"<<setw(4)<<setiosflags(ios::left)<<i+CurrentIteratorNumber+1<<" ";
            //randomize
        vector<int> randseri_=generateRandomSeries(generateSeries(0,NumberInputVectors-1,1));

        int BestXMap, BestYMap = 0;
        double Score=numeric_limits<double>::max(), ScoreNow ;



        boost::asio::thread_pool BIGPOOLS(CORENUM);
         for (int jit=0; jit<NumberInputVectors;jit++){ //XX the order should be random

         //int j = GenerateRandomNumber(SOMTrain::NumberInputVectors);
         //int bkj=j;
         //j = getRandomGeneratorAdvance();//std::cout<<j<<std::endl;
         //j = GenerateRandomNumber(SOMTrain::NumberInputVectors);
            // Find the BMU
             //std::vector< std::future<double> > DISTANCE_RESULT;
           const vector<double> SVivec=InputVectors[ randseri_[jit]  ];

           // boost::asio::thread_pool THRPLCDIST(CORENUM);

             double** DIST_PAR_TMP;DIST_PAR_TMP= new double*[xsize];
             for(int z=0;z<xsize;z++) DIST_PAR_TMP[z]=new double [ysize];

                        //pooling detection
                        const  int  XYSZ_PL=xsize*ysize;
                        bool* PLDC_DS ;
                        PLDC_DS=new bool[XYSZ_PL];for(int qqq=0;qqq<XYSZ_PL;qqq++)PLDC_DS[qqq]=false;

              for (int RowMap=0;RowMap<SOMTrain::xsize;RowMap++){
                 for (int ColMap=0;ColMap<ysize;ColMap++){

                                      boost::asio::post(BIGPOOLS,[=](){
                                                            double disttmp=EvaluateDistance(Initialize2DMap::SOMMap[RowMap][ColMap],SVivec );
                                                            DIST_PAR_TMP[RowMap][ColMap]=disttmp;
                                                                //Thread Pool Control
                                                                PLDC_DS[RowMap*ysize+ColMap]=true;
                                                            //return disttmp;
                                                          });




                 }
              }

            //THRPLCDIST.join();
                    //pooling detection wait
                       while(true){
                            int CULPLDCDS=0;
                            for(int yyy=0;yyy<XYSZ_PL;yyy++){
                                CULPLDCDS+=PLDC_DS[yyy];
                            }
                            if(CULPLDCDS==XYSZ_PL)break;
                            //cout<<CULPLDCDS;

                       }
                       delete []PLDC_DS;



           // ofstream df("df.txt");
             for (int RowMap=0;RowMap<SOMTrain::xsize;RowMap++){
                 for (int ColMap=0;ColMap<ysize;ColMap++){
                     //ScoreNow=DISTANCE_RESULT[RowMap*ysize+ColMap].get();
                     //ScoreNow = EvaluateDistance(Initialize2DMap::SOMMap[RowMap][ColMap], InputVectors[ randseri_[jit]  ]); //XX ,1.0 sigma
                     ScoreNow=DIST_PAR_TMP[RowMap][ColMap];
                    //df<<ScoreNow<<" ,";
                     if (ScoreNow < Score){Score = ScoreNow; BestXMap=RowMap, BestYMap=ColMap;}
                  }
                  //df<<endl;
             }
             //df.close();
            //cout<<BestXMap*ysize+BestYMap<<" ";


            for(int z=0;z<xsize;z++) delete [] DIST_PAR_TMP[z];
            delete [] DIST_PAR_TMP;
            // DISTANCE_RESULT.clear();
             //cout<<"bmu"<<jit<<endl;
             //cout<<"echo1"<<endl;
             //cout<<"xysz"<<xsize<<","<<ysize<<endl;
             //BMU = Initialize2DMap::SOMMap[SOMTrain::BestXMap,SOMTrain::BestYMap];
             // Update the BMUs
             //std::this_thread::sleep_for(std::chrono::seconds(1));

             //std::vector< std::future<int> > THRPOOLRESULT;
             //boost::asio::thread_pool THRPLUPD(CORENUM);

                        //pooling detection
                            //const  int  XYSZ_PL=xsize*ysize;
                            //bool* PLDC_DS ;
                        PLDC_DS=new bool[XYSZ_PL];for(int qqq=0;qqq<XYSZ_PL;qqq++)PLDC_DS[qqq]=false;

             for (int RowMap=0;RowMap<SOMTrain::xsize;RowMap++){
                 for (int ColMap=0;ColMap<ysize;ColMap++){


                     int CoordX = RowMap;
                     int CoordY = ColMap;
                    // if (CoordX < (SOMTrain::xsize/2)) {CoordX = RowMap;}
                     //if (CoordX >= (SOMTrain::xsize/2)){CoordX = (SOMTrain::xsize/2) - RowMap%(SOMTrain::xsize/2);}
                     //if (CoordY < (SOMTrain::ysize/2)) {CoordY = ColMap;}
                     //if (CoordY >= (SOMTrain::ysize/2)){CoordY = (SOMTrain::ysize/2) - ColMap%(SOMTrain::xsize/2);}
                     //std::cout << CoordX << "\n";// ;<< "  " << CoordY << std::endl;
            //important


                       // THRPOOLRESULT.emplace_back(
                                                    //THRPOOL.enqueue
                                                   boost::asio::post(BIGPOOLS,
                                                                    [=](){
                                                                            SOMMap[RowMap][ColMap] = LearnFunction(SOMMap[RowMap][ColMap],SVivec,BestXMap, BestYMap, CoordX, CoordY, SOMTrain::SigmaNeighbouring, SOMTrain::LearningRate);
                                                                                //Thread Pool Control
                                                                                PLDC_DS[RowMap*ysize+ColMap]=true;
                                                                            //return 1 ;
                                                                         }
                                                                    );
                                                   //);




                        /*if((THRD_UPD.size()% CORENUM==0)|| ((RowMap*ysize+ColMap)== (RowMap*ColMap-1) )){
                            //cout<<"kill thread"<<THRD_UPD.size()<<endl;
                            for(int trt=0;trt<THRD_UPD.size();trt++){
                                THRD_UPD[trt].join();
                            }
                            THRD_UPD.clear();
                        }*/
                //INFQUEUE_.push((void**)(*ThreadForLearnFunction)(RowMap,ColMap,InputVectors[randseri_[jit]],BestXMap,BestYMap,CoordX,CoordY,SigmaNeighbouring,LearningRate));
                     //cout<<BestXMap<<","<<BestYMap<<endl;
                     //for(auto v:Initialize2DMap::SOMMap[RowMap][ColMap])cout<<v<<" ";cout<<endl;
                     //Initialize2DMap::SOMMap(RowMap,ColMap) = BMUUpdated;
                     //std::cout << BMUUpdated << std::endl;
                 //cout<<"echo2"<<endl;
                 }
             }
            //THRPLUPD.join();

            //pooling detection wait
                       while(true){
                            int CULPLDCDS=0;
                            for(int yyy=0;yyy<XYSZ_PL;yyy++){
                                CULPLDCDS+=PLDC_DS[yyy];
                            }
                            if(CULPLDCDS==XYSZ_PL)break;

                       }
                       delete []PLDC_DS;

             //for(auto && trr: THRPOOLRESULT) trr.get();
             //THRPOOLRESULT.clear();
             //j+(i*InputVectors.rows());
            //j=bkj;

            if(is_TrainingUpdateShowEvery50k&&((jit+1)%50000==0))cout<<"update"<<jit+1<<";";
         }
         BIGPOOLS.join();
        //if(is_TrainingUpdateShowEvery50k)cout<<endl;
     // Update Learning Rate
         //SOMTrain::LearningRate = UpdateLearningRate(SOMTrain::LearningRateInitial,SOMTrain::LearningRateFinal, i, SOMTrain::lambda);
         //SOMTrain::SigmaNeighbouring = UpdateSigmaNeighbouring(SOMTrain::SigmaNeighbouringInitial,SOMTrain::SigmaNeighbourhoodFinal,  i, SOMTrain::lambda);


    }
     CurrentIteratorNumber+=1;

            /* only for thread
            CLCTHREAD_STOP_SIGNAL=true;
             CLCTHREAD_.join();*/
}







void SOMTrain::setIterator(int Iters){
IteratorNumber=Iters;
}
SOMTrain::SOMTrain(int xsz,int ysz,int vecsz): Initialize2DMap(xsz,ysz,vecsz){

}
SOMTrain::SOMTrain(){
}

SelfOrganizingMap::SelfOrganizingMap(int mapx,int mapy,int vecsz,int sigmainit,int sigmafinal,double learnratioinit,double learnrationfinal):SOMTrain(mapx,mapy,vecsz){

SigmaNeighbouringInitial=sigmainit;
SigmaNeighbourhoodFinal=sigmafinal;
LearningRateInitial=learnratioinit;
LearningRateFinal=learnrationfinal;

}
SelfOrganizingMap::SelfOrganizingMap(int sigmainit,int sigmafinal,double learnratioinit,double learnrationfinal){
SigmaNeighbouringInitial=sigmainit;
SigmaNeighbourhoodFinal=sigmafinal;
LearningRateInitial=learnratioinit;
LearningRateFinal=learnrationfinal;
}


std::pair<int,int> SelfOrganizingMap::findBestMatchUnit(const vector<double>& factor)const{
     if(factor.size()!=InputVectorSize){cerr<<"findBestMatchUnit size error!"<<endl;return {0,0} ;};
double Score=numeric_limits<double>::max(), ScoreNow ;
int BestXMap, BestYMap;

            for (int RowMap=0;RowMap<xsize;RowMap++){
                 for (int ColMap=0;ColMap<ysize;ColMap++){
                     ScoreNow = EvaluateDistance(Initialize2DMap::SOMMap[RowMap][ColMap], factor);
                     if (ScoreNow < Score){
                            Score = ScoreNow;
                            BestXMap=RowMap;
                            BestYMap=ColMap;
                     }
                 }
            }
return {BestXMap,BestYMap};
}
double SelfOrganizingMap::calculateRootMeanSquareError()const{
double RMSE=0;
double Score=numeric_limits<double>::max(), ScoreNow ;
int BestXMap, BestYMap;
     for (int jit=0; jit<NumberInputVectors;jit++){

         for (int RowMap=0;RowMap<xsize;RowMap++){
                 for (int ColMap=0;ColMap<ysize;ColMap++){
                     ScoreNow = EvaluateDistance(Initialize2DMap::SOMMap[RowMap][ColMap],InputVectors[jit] );
                     if (ScoreNow < Score){
                            Score = ScoreNow;
                            BestXMap=RowMap;
                            BestYMap=ColMap;
                     }
                 }
            }




        RMSE+=(Score*Score);


     }
return sqrt(RMSE/NumberInputVectors);
}

double SelfOrganizingMap::calculateRootMeanSquareError_Parallel(const int CORENUM)const{

//double RMSE=0;
double *sclib=new double[NumberInputVectors];
boost::asio::thread_pool jpool(CORENUM);

     for (int jit=0; jit<NumberInputVectors;jit++){


            boost::asio::post(jpool,[=](){
                                            double Score=numeric_limits<double>::max(), ScoreNow ;
                                            int BestXMap, BestYMap;
                                                             for (int RowMap=0;RowMap<xsize;RowMap++){
                                                                     for (int ColMap=0;ColMap<ysize;ColMap++){
                                                                         ScoreNow = EvaluateDistance(Initialize2DMap::SOMMap[RowMap][ColMap],InputVectors[jit] );
                                                                         if (ScoreNow < Score){
                                                                                Score = ScoreNow;
                                                                                BestXMap=RowMap;
                                                                                BestYMap=ColMap;
                                                                         }
                                                                     }
                                                                }
                                            sclib[jit]=Score*Score;
                                        }
                                );





        //RMSE+=(Score*Score);
      }
      jpool.join();

long double RMSE=0.0;
for(int q=0;q<NumberInputVectors;q++){
    RMSE+=sclib[q];
}

delete [] sclib;
return sqrt(RMSE/NumberInputVectors);
}










bool SelfOrganizingMap::save(std::ofstream& fout)const{
    //if(!fout.is_open())return false;
    fout<<xsize<<","<<ysize<<","<<InputVectorSize<<endl;
    for(int si=0;si<xsize;si++){
        for(int sj=0;sj<ysize;sj++){
            for(auto c:SOMMap[si][sj]){
                fout<<std::scientific<<c<<",";
                //cout<<c;
            }
            fout<<endl;

        }
    }

    return true;
}
bool SelfOrganizingMap::load(std::ifstream&fin){
    //cout<<"load"<<endl;
    if(!fin.is_open())return false;

    string eat;
    getline(fin,eat);

    stringstream ss;
    replace(eat.begin(),eat.end(),',',' ');

    //cout<<eat<<endl;
    int xsz,ysz,vecsz;
    ss<<eat;
    ss>>xsz;
    ss>>ysz;
    ss>>vecsz;
    Initialize2DMap::InputVectorSize=vecsz;
    InitializeMap(xsz,ysz,false);


    //cout<<xsize<<","<<ysize<<","<<InputVectorSize<<endl;

    for(int i=0;i<xsize;i++){
        for(int j=0;j<ysize;j++){

            //vector<double> sp;
            readVector(SOMMap[i][j],fin);
            //cout<<sp.size();
            //for()cout<<sp[0];

        }
    }



    return true;
}
vector<double> SelfOrganizingMap::getWeight(int r,int c)const{
return SOMMap[r][c];
}
SelfOrganizingMap& SelfOrganizingMap::operator<<(const std::vector<std::vector<double>>& dat_){
ImportBatch(dat_);
return *this;
}
