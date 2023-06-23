#include"BufferDissolver.hpp"
#include<iostream>
#include<cmath>
using namespace std;

boost::geometry::model::polygon<boost::geometry::model::d2::point_xy<double>> makeCirclePolygon(double posx,double posy,double radius,int PointsPerCircle){

    boost::geometry::strategy::buffer::distance_symmetric<double> distance_strategy(radius);
    boost::geometry::strategy::buffer::join_round join_strategy(PointsPerCircle);
    boost::geometry::strategy::buffer::end_round end_strategy(PointsPerCircle);
    boost::geometry::strategy::buffer::point_circle circle_strategy(PointsPerCircle);
    boost::geometry::strategy::buffer::side_straight side_strategy;


     boost::geometry::model::multi_polygon<boost::geometry::model::polygon<boost::geometry::model::d2::point_xy<double>> > result;
    //create circle
    boost::geometry::model::multi_point<boost::geometry::model::d2::point_xy<double>> sp;
    sp.push_back(boost::geometry::model::d2::point_xy<double> (posx,posy));

    boost::geometry::buffer(sp, result,
                distance_strategy, side_strategy,
                join_strategy, end_strategy, circle_strategy);


    return result[0];
}

boost::geometry::model::polygon<boost::geometry::model::d2::point_xy<double>> makeCirclePolygon(const BufferCircleInformation& bcinf,int PointsPerCircle){
 return   makeCirclePolygon(bcinf.positionX,bcinf.positionY,bcinf.radius,PointsPerCircle);
}

boost::geometry::model::multi_polygon<boost::geometry::model::polygon<boost::geometry::model::d2::point_xy<double>> > dissolvePolygons( const std::vector<boost::geometry::model::polygon<boost::geometry::model::d2::point_xy<double>>> Polygons_,int PointsPerCircle){

boost::geometry::model::multi_polygon<boost::geometry::model::polygon<boost::geometry::model::d2::point_xy<double>>> disvl;



// start here
double pseudoradius_=0.001;
//int PointsPerCircle=100;
    boost::geometry::strategy::buffer::distance_symmetric<double> distance_strategy(pseudoradius_);
    boost::geometry::strategy::buffer::join_round join_strategy(PointsPerCircle);
    boost::geometry::strategy::buffer::end_round end_strategy(PointsPerCircle);
    boost::geometry::strategy::buffer::point_circle circle_strategy(PointsPerCircle);
    boost::geometry::strategy::buffer::side_straight side_strategy;


    boost::geometry::model::multi_polygon<boost::geometry::model::polygon<boost::geometry::model::d2::point_xy<double>>> mpol;

    for(int i=0;i<Polygons_.size();i++){
            mpol.push_back(Polygons_[i]);

    }

        boost::geometry::buffer(mpol, disvl,
                distance_strategy, side_strategy,
                join_strategy, end_strategy, circle_strategy);










return disvl;
}
double calculateNoDissolveBuffersArea(std::vector<BufferCircleInformation> bcinfs_){
double cul=0;

        for(const BufferCircleInformation& bcinf:bcinfs_){

            cul+=(bcinf.radius*bcinf.radius*acos(-1));

        }


return cul;
}






double calculateMultiBuffersArea(std::vector<BufferCircleInformation> bcinfs_,int PointsPerCircle){

vector<boost::geometry::model::polygon<boost::geometry::model::d2::point_xy<double>>> vec_cirpolygon;

 //cout<<"test1"<<endl;
        for(const BufferCircleInformation& bcinf:bcinfs_){

            vec_cirpolygon.push_back(makeCirclePolygon(bcinf,PointsPerCircle));
        }
        //cout<<"load"<<endl;
        // cout<<"test2"<<endl;
        boost::geometry::model::multi_polygon
            mp= dissolvePolygons(vec_cirpolygon,PointsPerCircle);
//cout<<"test3"<<endl;
        return boost::geometry::area(mp);


}



double calculateRasterMultiBuffersArea(std::vector<BufferCircleInformation> bcinfs_,const int ColumnSize){


    set<vector<int>>bigset;
    //help
    set<vector<int>>lubigset,llbigset,rubigset,rlbigset;


        for(const BufferCircleInformation& bcinf:bcinfs_){

            //get all cirraster
            if(bcinf.radius>ColumnSize){
                    int rlbound=(int)((bcinf.positionX-bcinf.radius)/ColumnSize)-1;
                    int rubound=(int)((bcinf.positionX+bcinf.radius)/ColumnSize)+1;
                    int clbound=(int)((bcinf.positionY-bcinf.radius)/ColumnSize)-1;
                    int cubound=(int)((bcinf.positionY+bcinf.radius)/ColumnSize)+1;

                        for(int pseudoR=rlbound;pseudoR<=rubound;pseudoR++ ){
                            //cout<<"pseudoR:" <<pseudoR<<endl;

                            for(int pseudoC=clbound;pseudoC<=cubound;pseudoC++ ){
                                         //cout<<"pseudoC:" <<pseudoC<<endl;
                                        //central
                                        const double r2=double(bcinf.radius*bcinf.radius);

                                        double vecx=((double)(pseudoR*ColumnSize+(ColumnSize/2))-bcinf.positionX);
                                        double vecy=((double)(pseudoC*ColumnSize-(ColumnSize/2))-bcinf.positionY);
                                        double dis2=vecx*vecx+vecy*vecy;
                                        if(dis2<= r2){
                                            bigset.insert(vector<int>{pseudoR,pseudoC});
                                        }

                                        //LU
                                        vecx=((double)(pseudoR*ColumnSize+(0))-bcinf.positionX);
                                        vecy=((double)(pseudoC*ColumnSize-(0))-bcinf.positionY);
                                        dis2=vecx*vecx+vecy*vecy;
                                        if(dis2<= r2){
                                            lubigset.insert(vector<int>{pseudoR,pseudoC});
                                        }


                                        //LL

                                        vecx=((double)(pseudoR*ColumnSize+(0))-bcinf.positionX);
                                        vecy=((double)(pseudoC*ColumnSize-(ColumnSize))-bcinf.positionY);
                                        dis2=vecx*vecx+vecy*vecy;
                                        if(dis2<= r2){
                                            llbigset.insert(vector<int>{pseudoR,pseudoC});
                                        }

                                        //RU
                                        vecx=((double)(pseudoR*ColumnSize+(ColumnSize))-bcinf.positionX);
                                        vecy=((double)(pseudoC*ColumnSize-(0))-bcinf.positionY);
                                        dis2=vecx*vecx+vecy*vecy;
                                        if(dis2<= r2){
                                            rubigset.insert(vector<int>{pseudoR,pseudoC});
                                        }

                                        //RL
                                        vecx=((double)(pseudoR*ColumnSize+(ColumnSize))-bcinf.positionX);
                                        vecy=((double)(pseudoC*ColumnSize-(ColumnSize))-bcinf.positionY);
                                        dis2=vecx*vecx+vecy*vecy;
                                        if(dis2<= r2){
                                            rlbigset.insert(vector<int>{pseudoR,pseudoC});
                                        }

                            }//pseudoC

                        }//pseudoR
            }//radius> colsz
            else{

                bigset.insert(vector<int>{(int)(bcinf.positionX/ColumnSize),(int)(bcinf.positionY/ColumnSize )});

            }//radius<=colsz


        }//every point

return ((1*bigset.size()+2*lubigset.size()+2*llbigset.size()+2*rubigset.size()+2*rlbigset.size())/9)*ColumnSize*ColumnSize;
}// end raster


std::vector<BufferCircleInformation> simplifyCircleBuffer(const std::vector<BufferCircleInformation>& bcinfs_){
const int orisz= bcinfs_.size();
bool keep[orisz];
for(int i=0;i<orisz;i++){
    keep[i]=true;
}

    for(int i=0;i<orisz;i++){

        for(int j=i+1;j<orisz;j++){

                   double u=(bcinfs_[i].positionX-bcinfs_[j].positionX);
                   double v=(bcinfs_[i].positionY-bcinfs_[j].positionY);
                   double d=sqrt((u*u)+(v*v));
                   //tell big R and small r
                   double R;int idxR;
                   double r;int idxr;
                   if(bcinfs_[i].radius>bcinfs_[j].radius){
                       R=bcinfs_[i].radius;idxR=i;
                       r=bcinfs_[j].radius;idxr=j;
                   }
                   else{
                       R=bcinfs_[j].radius;idxR=j;
                       r=bcinfs_[i].radius;idxr=i;

                   }

                   if(d<=(R-r)){
                        keep[idxr]=false;

                   }

            }
    }// tell elim

    //decide keep
       vector<BufferCircleInformation> rt;
    for(int i=0;i<orisz;i++){
        if(keep[i]) rt.push_back(bcinfs_[i]);
    }



return rt;

}//simplifyCirclesBuffers
