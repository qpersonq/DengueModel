#include<iostream>
#include<fstream>
#include<sstream>
#include"readDengueini.hpp"
using namespace std;
std::map<std::string,std::string> readDengueINI(std::string inifn_){
//cout<<"echo1\n";
map<string ,string> rtpss;
ifstream inifin(inifn_);
if(!inifin.is_open())return rtpss;
//cout<<"echo2\n";
string tmpstr;
while(getline(inifin,tmpstr)){
       // cout<<"echo3\n";
    if(tmpstr.empty())break;
    stringstream tss;
    string istr,pstr,ustr;
    tss<<tmpstr;
    tss>>istr;
    while(tss>>ustr)pstr+=(ustr+" ");
    pstr.pop_back();
//cout<<istr<<" : "<<pstr<<endl;
    rtpss[istr]=pstr;

}

inifin.close();
if(!rtpss.size()){
    cerr<<"error load ini!"<<endl;
    exit(-1);
}


return rtpss;
}

