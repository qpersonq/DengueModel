#ifndef EXTRACONTROLSIGNAL_HPP

#define EXTRACONTROLSIGNAL_HPP
#include<string>
#include<map>
#include<fstream>
class ExtraControlSignal{
protected:
bool isExist_=false;
std::map<std::string,std::string> Infomations;
std::string ExtraFilePath;



bool readExtraFile(std::string extrafn);

private:

public:

ExtraControlSignal(std::string extrafilefpth_);
std::string getExtraFilePath()const;
bool isExist()const;
std::string getInformation(std::string tag);




};


#endif // EXTRACONTROLSIGNAL_HPP
