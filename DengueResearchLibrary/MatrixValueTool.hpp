#ifndef MatrixValueTool_hpp
#define MatrixValueTool_hpp
#include<vector>
#include<fstream>
#include<algorithm>
std::string NUMconvert2STR(int iv);
std::string NUMconvert2STR(double iv);
double STRconvert2Num(std::string nstr);
std::vector<double> capatureColumnVector(const std::vector<std::vector<double>>& doumtrx,int cidx);
bool setingColumnVector(std::vector<std::vector<double>>& doumtrx,const std::vector<double>& lir,int indx);
bool readVector( std::vector<double>&vc,std::ifstream& fin);
bool writeVector(const std::vector<double>&vc,std::ofstream& fout);
bool writeMatrix(const std::vector<std::vector<double>>&mtrx,std::ofstream& fout);
bool readMatrix( std::vector<std::vector<double>>&mtrx,std::ifstream& fin);

std::vector<double> operator*(double lhs,const std::vector<double>&rhs);
std::vector<double> operator+(const std::vector<double>& lhs,const std::vector<double>&rhs);
std::vector<double> operator-(const std::vector<double>& lhs,const std::vector<double>&rhs);
double norm(const std::vector<double>& lhs);
std::vector<double> abs(const std::vector<double>& vc);
#endif // MatrixValueTool_hpp
