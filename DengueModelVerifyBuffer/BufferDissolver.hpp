#ifndef BUFFER_DISSOLVER
#define BUFFER_DISSOLVER



#include <boost/geometry.hpp>
#include <boost/geometry/geometries/point_xy.hpp>
#include <boost/geometry/geometries/polygon.hpp>

#include <boost/geometry.hpp>
#include <boost/geometry/geometries/point_xy.hpp>
#include <boost/geometry/geometries/geometries.hpp>
#include<vector>
struct BufferCircleInformation{
double positionX;
double positionY;
double radius;
BufferCircleInformation(double px,double py,double rad){    positionX=px;positionY=py;radius=rad;}


};
boost::geometry::model::polygon<boost::geometry::model::d2::point_xy<double>> makeCirclePolygon(const BufferCircleInformation& bcinf,int PointsPerCircle=180);

boost::geometry::model::polygon<boost::geometry::model::d2::point_xy<double>> makeCirclePolygon(double posx,double posy,double radius, int PointsPerCircle=180);

boost::geometry::model::multi_polygon<boost::geometry::model::polygon<boost::geometry::model::d2::point_xy<double>> > dissolvePolygons(const std::vector<boost::geometry::model::polygon<boost::geometry::model::d2::point_xy<double>>> Polygons_, int PointsPerCircle=180);

double calculateNoDissolveBuffersArea(std::vector<BufferCircleInformation> bcinfs_);
double calculateMultiBuffersArea(std::vector<BufferCircleInformation> bcinfs_,int PointsPerCircle=36);
double calculateRasterMultiBuffersArea(std::vector<BufferCircleInformation> bcinfs_,const int ColumnSize=10);
std::vector<BufferCircleInformation> simplifyCircleBuffer(const std::vector<BufferCircleInformation>& bcinfs_);
#endif // BUFFER_DISSOLVER
