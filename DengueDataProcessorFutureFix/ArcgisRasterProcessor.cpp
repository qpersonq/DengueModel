#include"ArcgisRasterProcessor.hpp"
#include<iostream>
#include<fstream>
#include<sstream>
using namespace std;
ArcGISRasterGenerator::ArcGISRasterGenerator(std::string fn_,bool createPyrimid):FileName(fn_){
    ifstream fin(FileName);
    if(!fin.is_open()){cerr<<"error open ArcgisRaster!"<<endl;return;}

    {
        stringstream ss;string tmpln;

            getline(fin,tmpln);
            ss<<tmpln;
            ss>>tmpln;
            ss>>ncol;
            ss.clear();
            getline(fin,tmpln);
            ss<<tmpln;
            ss>>tmpln;
            ss>>nrow;
            ss.clear();

            getline(fin,tmpln);
            ss<<tmpln;
            ss>>tmpln;
            ss>>xllcorne;
            ss.clear();
            getline(fin,tmpln);
            ss<<tmpln;
            ss>>tmpln;
            ss>>yllcorner;
            ss.clear();

            getline(fin,tmpln);
            ss<<tmpln;
            ss>>tmpln;
            ss>>cellsize;
            ss.clear();
            getline(fin,tmpln);
            ss<<tmpln;
            ss>>tmpln;
            ss>>NODATA_value;
            ss.clear();
    }
    fin.close();
    if(createPyrimid)CreatePyrimid();
}


double ArcGISRasterGenerator::getValue(const std::vector<double>& corr){
    if(!isCreatePyrimid_){cerr<<"not create pyrimid!"<<endl;return -1;}

    double rtvl;
    auto pos=MatrixCalculator(corr);
    if(pos[1]>=nrow||pos[0]>=ncol){cerr<<"ArcgisRaster error!"<<endl;return 0;}
    diskpointer.seekg( Pyrimid_[ pos[1] ],ios::beg);
    string prop;
    getline(diskpointer,prop);
    stringstream ss;
    ss<<prop;
    {
        int i=0;
        for(;i<pos[0];i++){
            ss>>prop;
        }
        ss>>rtvl;
    }
if(rtvl==NODATA_value) rtvl=replaceNoDataValue;
return rtvl;
}

void ArcGISRasterGenerator::showInformation()const{
        cout<<FileName<<endl;
        if(isCreatePyrimid_)cout<<"create pyrimid "<<endl;
        else cout<<"not create pyrimid "<<endl;

        cout<<"ncol: "<<ncol<<endl;
        cout<<"nrow: "<<nrow<<endl;
        cout<<"xllcorne: "<<xllcorne<<endl;
        cout<<"yllcorner: "<<yllcorner<<endl;
        cout<<"cellsize: "<<cellsize<<endl;
        cout<<"NODATA_value: "<<NODATA_value<<endl;
        auto q=getLeftUp();
        cout<<"UP_Left: "<<q[0]<<","<<q[1]<<endl;
}
bool ArcGISRasterGenerator::CreatePyrimid(){
    isCreatePyrimid_=true;
    diskpointer.open(FileName,ios_base::in);
    if(!diskpointer.is_open()){cerr<<"ArcgisRaster fail!"<<endl;return false;}

    for(int p=0;p<InformationLNNM__;p++){
        string tllll;
        getline(diskpointer,tllll);
    }
    for(int r=0;r<nrow;r++){
        string tlll;

        Pyrimid_.push_back(diskpointer.tellg());
        getline(diskpointer,tlll);
    }



    return true;
}

ArcGISRasterGenerator::~ArcGISRasterGenerator(){
diskpointer.close();
}
std::vector<double> ArcGISRasterGenerator::getLeftUp()const{

return {xllcorne,yllcorner+cellsize*nrow};
}
std::vector<int> ArcGISRasterGenerator::MatrixCalculator(const std::vector<double>& corr)const{
if(corr.size()!=2){
    cerr<<"error MatrixCalculator size!"<<endl;
    return {0,0};
}

int x,y;
auto lu=getLeftUp();
x=(corr[0]-lu[0])/cellsize;
y=(lu[1]-corr[1])/cellsize;
//cout<<"xy: "<<x<<" , "<<y<<endl;
if(x<0 || y<0){cerr<<"error x y MatrixCalculator"<<endl;return {0,0};}

 return{x,y};
}
