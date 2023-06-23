#include"CoordinateTransmationTWD97.hpp"
#include"SpatialTimeProcessor.hpp"
#include<iostream>
#include<string>
#include<vector>
#include<cmath>
#include<sstream>
#include"assert.h"
using namespace std;
using namespace spatialtime;
 CoordinateTransform* CoordinateTransform::ctfptr=nullptr;
Coordinate CoordinateTransform::Cal_lonlat_To_twd97(long double lon ,long double lat){
    lon = (lon/180) * M_PI;
    lat = (lat/180) * M_PI;

        double e = pow((1 - pow(b,2) / pow(a,2)), 0.5);
        double e2 = pow(e,2)/(1-pow(e,2));
        double n = ( a - b ) / ( a + b );
        double nu = a / pow((1-(pow(e,2)) * (pow(sin(lat), 2) ) ) , 0.5);
        double p = lon - lon0;
        double A = a * (1 - n + (5/4) * (pow(n,2) - pow(n,3)) + (81/64) * (pow(n, 4)  - pow(n, 5)));
        double B = (3 * a * n/2.0) * (1 - n + (7/8.0)*(pow(n,2) - pow(n,3)) + (55/64.0)*(pow(n,4) - pow(n,5)));
        double C = (15 * a * (pow(n,2))/16.0)*(1 - n + (3/4.0)*(pow(n,2) - pow(n,3)));
        double D = (35 * a * (pow(n,3))/48.0)*(1 - n + (11/16.0)*(pow(n,2) - pow(n,3)));
        double E = (315 * a * (pow(n,4))/51.0)*(1 - n);

        double S = A * lat - B * sin(2 * lat) +C * sin(4 * lat) - D * sin(6 * lat) + E * sin(8 * lat);



         //calx
        double K1 = S*k0;
        double K2 = k0*nu*sin(2*lat)/4.0;
        double K3 = (k0*nu*sin(lat)*(pow(cos(lat),3))/24.0) * (5 - pow(tan(lat),2) + 9*e2*pow((cos(lat)),2) + 4*(pow(e2,2))*(pow(cos(lat),4)));
        double y = K1 + K2*(pow(p,2)) + K3*(pow(p,4));

        //caly
        double K4 = k0*nu*cos(lat);
        double K5 = (k0*nu*(pow(cos(lat),3))/6.0) * (1 - pow(tan(lat),2) + e2*(pow(cos(lat),2)));
        double x = K4 * p + K5 * (pow(p, 3)) + dx;
       /* stringstream ss;
        ss<<x;
        ss<<" ";
        ss<<y;
         string TWD97;
        getline(ss,TWD97);*/

        //TWD97 = x.ToString()+ "," + y.ToString();
        Coordinate tmpcrd;
        tmpcrd.CoordinateType="twd97";
        tmpcrd.Y=y;
        tmpcrd.X=x;

        return tmpcrd;

  }
long double distantTWD97(const Coordinate& c1,const Coordinate& c2){
if(c1.CoordinateType!="twd97" || c2.CoordinateType!="twd97"){
    cout<<"errordistant"<<endl;
    assert(1);
}
long double dist;
long double delx,dely;
dely=c1.Y-c2.Y;
delx=c1.X-c2.X;
dist=pow(delx*delx+dely*dely,0.5);



return dist;
}
spatialtime::Coordinate CoordinateTransform::Cal_twd97_To_lonlat(long double x,long double y){

        double dy = 0;
        double e = pow((1- pow(b,2)/pow(a,2)), 0.5);

        x -= dx;
        y -= dy;

        // Calculate the Meridional Arc
        double M = y/k0;

        // Calculate Footprint Latitude
        double mu = M/(a*(1.0 - pow(e, 2)/4.0 - 3*pow(e, 4)/64.0 - 5*pow(e, 6)/256.0));
        double e1 = (1.0 - pow((1.0 - pow(e, 2)), 0.5)) / (1.0 + pow((1.0 - pow(e, 2)), 0.5));

        double J1 = (3*e1/2 - 27*pow(e1, 3)/32.0);
        double J2 = (21*pow(e1, 2)/16 - 55*pow(e1, 4)/32.0);
        double J3 = (151*pow(e1, 3)/96.0);
        double J4 = (1097*pow(e1, 4)/512.0);

        double fp = mu + J1*sin(2*mu) + J2*sin(4*mu) + J3*sin(6*mu) + J4*sin(8*mu);

        // Calculate Latitude and Longitude

        double e2 = pow((e*a/b), 2);
        double C1 = pow(e2*cos(fp), 2);
        double T1 = pow(tan(fp), 2);
        double R1 = a*(1-pow(e, 2))/pow((1-pow(e, 2)*pow(sin(fp), 2)), (3.0/2.0));
        double N1 = a/pow((1-pow(e, 2)*pow(sin(fp), 2)), 0.5);

        double D = x/(N1*k0);

        //cal la
        double Q1 = N1*tan(fp)/R1;
        double Q2 = (pow(D, 2)/2.0);
        double Q3 = (5 + 3*T1 + 10*C1 - 4*pow(C1, 2) - 9*e2)*pow(D, 4)/24.0;
        double Q4 = (61 + 90*T1 + 298*C1 + 45*pow(T1, 2) - 3*pow(C1, 2) - 252*e2)*pow(D, 6)/720.0;
        double lat = fp - Q1*(Q2 - Q3 + Q4);

        //cal lo
        double Q5 = D;
        double Q6 = (1 + 2*T1 + C1)*pow(D, 3)/6;
        double Q7 = (5 - 2*C1 + 28*T1 - 3*pow(C1, 2) + 8*e2 + 24*pow(T1, 2))*pow(D, 5)/120.0;
        double lon = lon0 + (Q5 - Q6 + Q7)/cos(fp);

        lat = (lat * 180) / M_PI;
        lon = (lon * 180) / M_PI;





        Coordinate tmpcrd;
        tmpcrd.CoordinateType="wgs84";
        tmpcrd.Y=lat;
        tmpcrd.X=lon;

        return tmpcrd;


}

CoordinateTransform:: CoordinateTransform(){;}
CoordinateTransform*  CoordinateTransform::getInstance(){
  if(!ctfptr)ctfptr=new CoordinateTransform;
return ctfptr;

}

