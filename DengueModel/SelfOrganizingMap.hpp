//
//  Classes_SOM.h
//  SOM
//
//  Created by Isidro on 6/4/14.
//  Copyright (c) 2014 Isidro Cortes Ciriano. All rights reserved.
//

#ifndef SOM_Classes_SOM_h
#define SOM_Classes_SOM_h

//#include </usr/local/include/eigen3/Eigen/eigen>
//#include <Eigen/eigen>
#include<vector>
#include<utility>
//typedef Eigen::Matrix<double, Eigen::Dynamic, Eigen::Dynamic> Matrix2D;
//typedef Eigen::Matrix<Eigen::VectorXd, Eigen::Dynamic, Eigen::Dynamic> MatrixOfVectors;
//std::vector<int> generateRandomSeries(std::vector<int> series);
//std::vector<int> generateSeries(int lb,int ub,int app);
//////////////////////////////////////////////
// Random Generator
//////////////////////////////////////////////
/*
class RandomGenerator {
    protected:
    std::vector<int> RNDseed;
    int RNDseedptr=0;
    public :
    void disturb(){
        for(int i=0;i<RNDseed.size();i++){
            int  ptr1=rand()%RNDseed.size(),ptr2=rand()%RNDseed.size();
            //std::cout<<ptr1<<" "<<ptr2<<std::endl;
            std::swap_ranges(RNDseed.begin()+ptr1,RNDseed.begin()+ptr1+1,RNDseed.begin()+ptr2);
        }
    }
    void setRandomGeneratorAdvance(int min_,int max_){
        srand(time(nullptr));
        for(int i=min_;i<=max_;i++)        RNDseed.push_back(i);

    }
    int getRandomGeneratorAdvance(){
        if((RNDseedptr%RNDseed.size())==0)disturb();
        int vl=RNDseed[RNDseedptr%RNDseed.size()];
        RNDseedptr++;

    return  vl;
    }

   double GenerateRandomNumber(int RandMax){
       double value = rand() % RandMax;
        return value;
    }
};
*/
//////////////////////////////////////////////


//////////////////////////////////////////////
// Euclidean distance
//////////////////////////////////////////////
class EuclideanDistance {
public:
double EvaluateDistance(const std::vector<double>& v1, const std::vector<double>& v2)const ;
};
////////////////////////////////////////////////////////////////////////////////////////////

//////////////////////////////////////////////
// Radial Kernel Distance
//////////////////////////////////////////////
class RadialKernelDistance {
public:
    //double sigma;
    double EvaluateDistance(const std::vector<double>&  v1, const std::vector<double>& v2, double sigma)const ;
};
////////////////////////////////////////////////////////////////////////////////////////////




 //////////////////////////////////////////////
 // Radial Kernel
 //////////////////////////////////////////////
 class RadialKernel {
 public:
 //double sigma;
 double EvaluateKernel(const std::vector<double>&  v1, const std::vector<double>&   v2, double sigma)const;
 };
 ////////////////////////////////////////////////////////////////////////////////////////////


 //////////////////////////////////////////////
 // Read Input Data
 //////////////////////////////////////////////
class GetInputVectors {
protected:
    int NumberInputVectors;
public:
    std::vector<double>*  InputVectors;
    ~GetInputVectors();
    bool ImportBatch(const std::vector<std::vector<double>>& dat_);

                    /*
                     bool fileExist(const char *fileName){
                        std::ifstream infile(fileName);
                        return infile.good();
                     };


                     void ReadData(const char *filename, int NumberVectors, int InputVectorSize){
                     MatrixOfVectors InputVectors(NumberVectors, 1);

                     GetInputVectors::InputVectorSize = InputVectorSize;
                     GetInputVectors::NumberInputVectors = NumberVectors;
                     //double MaxValueInputData;
                     fileExist(filename);
                     std::ifstream data(filename);
                     std::string line;
                     if (data.is_open()){std::cout << "Opening input file..\n\n" << std::endl;}
                     else{printf("Cannot open input file.. exiting..\n");}

                     int rowIndex = 0;


                     while(std::getline(data,line)){
                         int colIndex = 0;
                         std::stringstream  lineStream(line);
                         std::string  cell;
                         Eigen::VectorXd InputVector(InputVectorSize);

                            while(std::getline(lineStream,cell,',')){

                                InputVector(colIndex) =std::stof(cell);
                                //std::cout << cell <<std::endl;
                                if (std::stof(cell) > GetInputVectors::MaxValueInputData){GetInputVectors::MaxValueInputData = std::stof(cell);}
                                colIndex++;
                            }
                         InputVectors(rowIndex,0) = InputVector;
                         rowIndex++;
                     }
                         data.close();
                         printf("Closing input file..\n");
                         GetInputVectors::InputVectors = InputVectors;

                     }
                    */
 };
 ///////////////////////////////////////////////////////////////////////////////////////////////////////


 //////////////////////////////////////////////
 // SOM Map " Carte "
 //////////////////////////////////////////////
class Initialize2DMap {
 protected:
     bool MapReadyFlag=false;
     int InputVectorSize=3;
     void InitializeMap(int xsz, int ysz,bool randomize=true);
     int xsize, ysize;

 public:
    std::vector<double>** SOMMap=nullptr;
    double MaxValueInputData=0.5;
    Initialize2DMap(int xsz,int ysz,int vecsz);
    Initialize2DMap();
    int getXSize()const;
    int getYSize()const;
    int getInputVectorSize()const;
    ~Initialize2DMap();
 //double MaxValueInputData; // the maximum value in the input data. To have the same range of values in the SOM


};
 ///////////////////////////////////////////////////////////////////////////////////////////////////////


//////////////////////////////////////////////
// SOM Learning Function
//////////////////////////////////////////////
class GaussianDistribution {
public:
    double Gaussian(double x, double sigma)const;
};
////////////////////////////////////////////////////////////////////////////////////////////


//////////////////////////////////////////////
// SOM Learning Function
//////////////////////////////////////////////
// Learning (or updating) Function
//      BMU(t+1) = BMU(t) + LearningRate(t) * NeighbourhoodFunction(t)*[InputVector - BMU(t)]
// NeighbourhoodFunction
//
class SOMNeighbourhoodFunction  {
public:
    double NeighbourhoodFunction(int BMUX, int BMUY, int BMUNowX, int BMUNowY, double SigmaNeighbouring)const ;
};

///////

class SOMLearningFunction : public SOMNeighbourhoodFunction {
public:
    std::vector<double> LearnFunction(const std::vector<double>& BMUNow, const std::vector<double>& Input, int BMUX,int BMUY,int BMUNowX, int BMUNowY,double SigmaNeighbouring, double LearningRate)const;

};

///////

class UpdateSOMLearningRate {
public:
    double UpdateLearningRate(double L0, double LearningRateFinal, double Iter, double lambda)const;
};

///////

class UpdateSOMSigmaNeighbouring {
public:
    double UpdateSigmaNeighbouring(double SigmaNeighbouringZero, double SigmaNeighbourhoodFinal, double Iter, double lambda)const ;
};
////////////////////////////////////////////////////////////////////////////////////////////


//////////////////////////////////////////////
// Print Learing Results
//////////////////////////////////////////////
/*
class PrintSOM {
    public :
    void PrintToCSVFileRowWise (const char *filename, MatrixOfVectors SOMMap, int xsize, int ysize, int InputVectorSize){

        std::ofstream file(filename);
        if (file.is_open())
        {
            for (int i=0; i<xsize; i++){  // xsize is considered the rows of the SOM (i or RowMap below), and ysize the columns (j or ColMap below)
                for (int j=0; j<ysize; j++){
                    for (int k=0; k<InputVectorSize; k++){
                        file << SOMMap(i,j)(k) << ",";
                    }
                file << '\n';
                }
            }
        }
    }
};
*/
////////////////////////////////////////////////////////////////////////////////////////////


//////////////////////////////////////////////
// Train the SOM
//////////////////////////////////////////////

class SOMTrain:public Initialize2DMap,public GetInputVectors,public EuclideanDistance,protected GaussianDistribution,protected SOMLearningFunction,protected UpdateSOMLearningRate,protected UpdateSOMSigmaNeighbouring{
 protected:
     int IteratorNumber=300;
     int CurrentIteratorNumber=0;

 public:
 //int Iters;
 //bool isShowIteratorNumber=true;
 bool is_TrainingUpdateShowEvery50k=true;
 double LearningRateInitial, LearningRateFinal, SigmaNeighbouringInitial, SigmaNeighbourhoodFinal;
 double SigmaNeighbouring, LearningRate, lambda;
 void Train();
 void setIterator(int Iters);
 SOMTrain(int xsz,int ysz,int vecsz);
 SOMTrain();

 //Special train
  void ParallelTrain(const int CORENUM=4);
 //Iternal information
    int getCurrentIteratorNumber()const;
    int getIteratorNumber()const;

 //void ThreadForLearnFunction(int RowMap,int ColMap, std::vector<double>ivp,int BestXMap, int BestYMap, int CoordX,int  CoordY, double SigmaNeighbouring,double LearningRate );

};
 ///////////////////////////////////////////////////////////////////////////////////////////////////////


class SelfOrganizingMap:public SOMTrain{
public:
    SelfOrganizingMap(int mapx,int mapy,int vecsz,int sigmainit=3,int sigmafinal=1,double learnratioinit=.8,double learnrationfinal=.1);
    SelfOrganizingMap(int sigmainit=3,int sigmafinal=1,double learnratioinit=.8,double learnrationfinal=.1);
    std::pair<int,int> findBestMatchUnit(const std::vector<double>& factor)const;
    std::vector<double> getWeight(int r,int c)const;

    double calculateRootMeanSquareError()const;
    double calculateRootMeanSquareError_Parallel(const int CORENUM=4)const;

    bool save(std::ofstream& fout)const;
    bool load(std::ifstream&fin);
    SelfOrganizingMap& operator<<(const std::vector<std::vector<double>>& dat_);
};






#endif
