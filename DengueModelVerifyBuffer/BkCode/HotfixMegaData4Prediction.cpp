#include"HotfixMegaData4Prediction.hpp"
#include<iostream>
#include<fstream>
#include<algorithm>
#include<sstream>
using namespace std;
bool HotfixMegaData4Predition::readHotfixFile(std::string htinsfile){
ifstream hfl(htinsfile);
string eat;
getline(hfl,eat);
replace(eat.begin(),eat.end(),',' , ' ');
stringstream sst;
sst<<eat;

while(sst>>eat){
    InsertionTitle.push_back(eat);
}
int prpcnt=0;
            while(getline(hfl,eat)){
            replace(eat.begin(),eat.end(),',' , ' ');
            stringstream ssp;
                ssp<<eat;
                vector<string> pl;
                while(sst>>eat){
                    pl.push_back(eat);
                }
              IDFinder[ stoi(  pl[0])]=prpcnt;
              InserterTable.push_back(pl);

              prpcnt++;
            }


hfl.close();

}
void HotfixMegaData4Predition::Hotfix(DengueMegaDataControl* DenMDC){


}
