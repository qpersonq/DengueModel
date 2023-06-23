#include"SpatioTimeBufferCalculator.hpp"
#include<mutex>
#include<tuple>
#include<iostream>
#include <boost/asio/thread_pool.hpp>
#include <boost/asio/post.hpp>
using namespace std;
//std::mutex g_mutex;
SpatioTimeBufferCalculator& SpatioTimeBufferCalculator::inputSpatioTimeBuffer(time_t start_time_,int laglb,int lagub,double posx,double posy,double rad, time_t end_time_ ){
const int tot=86400;
//demuplexer
/*cout<<"inp:"<<start_time_<<","<<
laglb<<","<<lagub<<","<<posx<<","<<posy<<","<<rad<<endl;*/
for(int i=laglb;i<=lagub;i++){

time_t   curtime= (start_time_+i*tot);

    if(curtime<=end_time_){
            InputTuples.push_back(
                          make_tuple(curtime,posx,posy,rad)
                          );
    }

}


return *this;
}

SpatioTimeBufferCalculator& SpatioTimeBufferCalculator::inputSpatioTimeBuffer(time_t cur_time_,double posx,double posy,double rad,time_t end_time_ ){
    if(cur_time_<=end_time_){
            InputTuples.push_back(
                          make_tuple(cur_time_,posx,posy,rad)
                          );
    }
}


void SpatioTimeBufferCalculator::setCoreNumber(int cn){
CoreNumber=cn;
}


double SpatioTimeBufferCalculator::calculateDissovleBufferArea(time_t ttday,const int pointPerCircle)const{
    vector<BufferCircleInformation> buffcirinfs;
    //select ttday
    //g_mutex.lock();
    //cout<<ttday<<",tup sz"<<InputTuples.size()<<endl;
    for(int i=0;i<InputTuples.size();i++){

        if(get<0>(InputTuples[i])==ttday)buffcirinfs.push_back(BufferCircleInformation(get<1>(InputTuples[i]),get<2>((InputTuples[i])),get<3>((InputTuples[i]))+0.01));
    }
    //g_mutex.unlock();
    //cout<<",sel sz"<<buffcirinfs.size();

    //cout<<buffcirinfs[0].positionX<<","<<buffcirinfs[0].positionY<<","<<buffcirinfs[0].radius<<endl;
return  calculateMultiBuffersArea(buffcirinfs,pointPerCircle  );
}

double SpatioTimeBufferCalculator::calculateNoDissovleBufferArea(time_t ttday)const{

 vector<BufferCircleInformation> buffcirinfs;
    //select ttday
    //g_mutex.lock();
    //cout<<ttday<<",tup sz"<<InputTuples.size()<<endl;
    for(int i=0;i<InputTuples.size();i++){

        if(get<0>(InputTuples[i])==ttday)buffcirinfs.push_back(BufferCircleInformation(get<1>(InputTuples[i]),get<2>((InputTuples[i])),get<3>((InputTuples[i]))+0.01));
    }
    //g_mutex.unlock();
    //cout<<",sel sz"<<buffcirinfs.size();

    //cout<<buffcirinfs[0].positionX<<","<<buffcirinfs[0].positionY<<","<<buffcirinfs[0].radius<<endl;
return calculateNoDissolveBuffersArea(buffcirinfs);

}





std::tuple<int,double> SpatioTimeBufferCalculator::calculateDissovleBufferSpatioTime(const int pointPerCircle)const{
    //time distinct
    set<time_t> timepoints;
    for(int i=0;i<InputTuples.size();i++){
       timepoints.insert( get<0>(InputTuples[i]));

    }
    //cout<<"test1"<<endl;
// parallel  buffercal
    boost::asio::thread_pool pool(CoreNumber);
    //cout<<"test2"<<endl;
    //boost::asio::post(pool, [] {});
    //pool.join();
    const int timepointcount=timepoints.size();
    double* commarea=new double [timepointcount];for(int q=0;q<timepointcount;q++)commarea[q]=0;
    int * isrun=new int[timepointcount];for(int q=0;q<timepointcount;q++)isrun[q]=0;
    //cout<<"test3"<<endl;
   // cout<<"tm point sz: "<<timepoints.size()<<endl;
        int schnm=0;
        //cout<<timepointcount<<endl;
        for(time_t ett: timepoints){

              // commarea[schnm]= calculateDissovleBufferArea(ett,pointPerCircle);



        //select by time
        vector<BufferCircleInformation> buffcirinfs;
    //select ttday


                for(int i=0;i<InputTuples.size();i++){

                    if(get<0>(InputTuples[i])==ett)buffcirinfs.push_back(BufferCircleInformation(get<1>(InputTuples[i]),get<2>((InputTuples[i])),get<3>((InputTuples[i]))+0.01));
                }


            boost::asio::post(pool,[=](){

                                        if(DissolveMode==0)

                                                commarea[schnm]=calculateMultiBuffersArea( simplifyCircleBuffer(buffcirinfs),pointPerCircle  );
                                        else
                                                commarea[schnm]=calculateRasterMultiBuffersArea( simplifyCircleBuffer(buffcirinfs),pointPerCircle  );
                                    isrun[schnm]=1;


                                    }



                              );//sync


        ++schnm;

        }

     //check is done
/*
     while(1){
        int culqq=0;
        for(int q=0;q<timepointcount;q++){
            culqq+=isrun[q];
        }
        if(culqq>=timepointcount) break;
     }
*/
pool.join();

//store buffer by time
const_cast<SpatioTimeBufferCalculator*>(this)-> StoreDissolveBufferAreaByTimeT.clear();
int jdfkdjfgkljfgl=0;
for(time_t ett: timepoints){
   const_cast<SpatioTimeBufferCalculator*>(this)->StoreDissolveBufferAreaByTimeT[ett]=commarea[jdfkdjfgkljfgl];
   jdfkdjfgkljfgl++;
}


long double rtar=0;
for(int i=0;i<timepoints.size();i++)rtar+=commarea[i];
delete commarea;
//delete isrun;
return make_tuple(timepoints.size(),rtar);

}


std::tuple<int,double> SpatioTimeBufferCalculator::calculateNODissovleBufferSpatioTime()const{
    //time distinct
    set<time_t> timepoints;
    for(int i=0;i<InputTuples.size();i++){
       timepoints.insert( get<0>(InputTuples[i]));

    }
    //cout<<"test1"<<endl;
// parallel  buffercal
    boost::asio::thread_pool pool(CoreNumber);
    //cout<<"test2"<<endl;
    //boost::asio::post(pool, [] {});
    //pool.join();
    const int timepointcount=timepoints.size();
    double* commarea=new double [timepointcount];for(int q=0;q<timepointcount;q++)commarea[q]=0;
    int * isrun=new int[timepointcount];for(int q=0;q<timepointcount;q++)isrun[q]=0;
    //cout<<"test3"<<endl;
   // cout<<"tm point sz: "<<timepoints.size()<<endl;
        int schnm=0;
        //cout<<timepointcount<<endl;
        for(time_t ett: timepoints){

              // commarea[schnm]= calculateDissovleBufferArea(ett,pointPerCircle);



        //select by time
        vector<BufferCircleInformation> buffcirinfs;
    //select ttday


                for(int i=0;i<InputTuples.size();i++){

                    if(get<0>(InputTuples[i])==ett)buffcirinfs.push_back(BufferCircleInformation(get<1>(InputTuples[i]),get<2>((InputTuples[i])),get<3>((InputTuples[i]))+0.01));
                }


            boost::asio::post(pool,[=](){


                                    commarea[schnm]=calculateNoDissolveBuffersArea(buffcirinfs);

                                    isrun[schnm]=1;


                                    }



                              );//sync


        ++schnm;

        }

     //check is done
/*
     while(1){
        int culqq=0;
        for(int q=0;q<timepointcount;q++){
            culqq+=isrun[q];
        }
        if(culqq>=timepointcount) break;
     }
*/
pool.join();

//store buffer by time
const_cast<SpatioTimeBufferCalculator*>(this)-> StoreNODissolveBufferAreaByTimeT.clear();
int jdfkdjfgkljfgl=0;
for(time_t ett: timepoints){
   const_cast<SpatioTimeBufferCalculator*>(this)->StoreNODissolveBufferAreaByTimeT[ett]=commarea[jdfkdjfgkljfgl];
   jdfkdjfgkljfgl++;
}


long double rtar=0;
for(int i=0;i<timepoints.size();i++)rtar+=commarea[i];
delete commarea;
//delete isrun;
return make_tuple(timepoints.size(),rtar);

}
