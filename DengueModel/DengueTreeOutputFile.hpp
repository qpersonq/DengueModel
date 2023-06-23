#ifndef DToutputfile_hpp
#define DToutputfile_hpp
#include<string>
#include<fstream>
#include"NeuronTree.hpp"
class TreeWriter{
private:
    processTree* proctreeptr_=nullptr;
public:
    TreeWriter(processTree* ptptr );
    bool writePoint(std::ofstream& fout);
    bool writeConnectLine(std::ofstream& fout);
    bool writeGetPair(std::ofstream& fout);
    bool writeNeutron(std::ofstream& fout);
    bool writeGeoJson(std::ofstream& fout);
    bool travelsal(std::ofstream& fout);
};





#endif


