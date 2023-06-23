#ifndef neuron_tree_hpp
#define neuron_tree_hpp
#include"neuron.hpp"
#include"spatialtimeprocessor.hpp"
#include<ctime>
#include<vector>
#include<iterator>
#include<map>
#define __defaultFatctorSize__ 2
class NeuronNode{
protected:
     int Fid;
     int FactorNumber;

     spatialtime:: Coordinate coordinate;
     std::tm date;
public:
    neuron Neuron;
    std::vector<int> forwardingCase;
    std::vector<int> backwardingCase;
    std::vector<double> AdditionInformation;
    NeuronNode(int fid_,const spatialtime::Coordinate& cord_,const tm& dt_,int fn_);
    NeuronNode(int fid_,const spatialtime::Coordinate& cord_,const tm& dt_,int fn_,const std::vector<double>& adit_inf);

    NeuronNode(int fn_=__defaultFatctorSize__);
    int getFid()const;
    neuron getCurrentNeuron()const;
    bool setCurrentNeuron(const neuron& scn_);
    spatialtime::Coordinate getCoordinate()const;
    std::tm getDate()const;


    std::vector<std::vector<int>> getPairs(bool FordwardPair=true)const;
    int getCentroSize()const ;
    bool Load(std::ifstream&fin);
    bool Save(std::ofstream&fout)const;

    friend class processTree;
    friend class TreeWriter;
};

class processTree{
private:
    const int AttributeSize;
    //double AttributeInitialProperties[dfAttributeSize_]={1,10};
protected:
    /*initiallize condition of neuron of zero*/
    std::vector<NeuronNode> DengueCaseSet;





public:
    std::map<int ,int> IDHashTable; //id,index

    processTree(int atrbsz,const std::vector<double>& atrprp);
    void importCase( NeuronNode cdc);
    void importCases(const std::vector< NeuronNode>& vcdc);


    void clearConnection();
    void clearNeuron();
    void clearDengueCaseSet();


     int getAttributeSize();
     const NeuronNode& getDengueCase(int idx)const{return DengueCaseSet[idx];}
     int  getDengueCaseSize()const{return DengueCaseSet.size();}

     std::vector<double> getDirectionVectorSum(int dcase)const;
     virtual std::vector<std::vector<int> >getAllPair()const=0;


     virtual bool Load(std::ifstream&fin)=0;
     virtual bool Save(std::ofstream&fout)const=0;

friend class TreeWriter;
};


#endif // neuron_tree_hpp
