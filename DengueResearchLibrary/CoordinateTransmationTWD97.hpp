#ifndef coordinatetransmation_twd97_h
#define coordinatetransmation_twd97_h

#include<iostream>
#include<string>
#include<cmath>
#include"SpatialTimeProcessor.hpp"

constexpr long double  M_PI_ = 3.14159265358979323846264338327950288419716939937510582097494459230781640628620899863;



class CoordinateTransform{
private:
   constexpr static double a = 6378137.0;
   constexpr static double b = 6356752.314245;
   constexpr static double lon0 = 121 * M_PI_ / 180;
   constexpr static double k0 = 0.9999;
   constexpr static int dx = 250000;

static CoordinateTransform *ctfptr;

protected:

public:
    CoordinateTransform();
     spatialtime::Coordinate Cal_lonlat_To_twd97(long double lon ,long double lat);
     spatialtime::Coordinate Cal_twd97_To_lonlat(long double x,long double y);


static CoordinateTransform *getInstance();
};



long double distantTWD97(const spatialtime::Coordinate& c1,const spatialtime::Coordinate& c2);

#endif // coordinatetransmation_twd97_h
