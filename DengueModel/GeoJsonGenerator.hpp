#ifndef GeoJsonGenerator_hpp
#define GeoJsonGenerator_hpp
#include"SpatialTimeProcessor.hpp"
#include<string>
#include<map>

std::string jarray(const std::vector<std::string>& prop_ );
std::string jitem(const std::string& ll,const std::string& rr);
std::string jset(const std::vector<std::string>& prop_);
std::string jnotation(std::string str);
std::string geojPoint(double lon,double lat ,const std::map<std::string,std::string>& notation);
std::string geojlinestring(const std::vector<spatialtime::Coordinate>& ftset,const std::map<std::string,std::string>& notation);


#endif // GeoJsonGenerator_hpp
