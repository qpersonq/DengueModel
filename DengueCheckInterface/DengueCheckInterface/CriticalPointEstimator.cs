using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DengueCheckInterface
{
    public class CriticalPointEstimator
    {
        public double allPoint = 1000;
        public List<string> Tag=new List<string>();
        public List<double> Xset=new List<double>();
        public List<double> Yset=new List<double>();

        public void add(string tag, double xv, double yv)
        {
            Tag.Add(tag);
            Xset.Add(xv);
            Yset.Add(yv);
        }
        public List<Tuple<string,double,double>> getAccerator()
        {
            List < Tuple<string,double, double> >rtltup= new List<Tuple<string, double,double>>();


            for (int i=1;i<Xset.Count-1; i++)
            {
                rtltup.Add(Tuple.Create<string,double, double>(Tag[i], Xset[i], calculateAccerator(
                                                                                  Xset[i], Xset[i + 1], Xset[i - 1],
                                                                                  Yset[i], Yset[i + 1], Yset[i - 1]

                                                                                  )));
            }
            rtltup.Add(Tuple.Create<string, double, double>(Tag[Xset.Count - 1], Xset[Xset.Count - 1], calculateAccerator(
                                                                                  Xset[Xset.Count - 1], allPoint, Xset[Xset.Count - 1 - 1],
                                                                                  Yset[Xset.Count - 1],0, Yset[Xset.Count - 1 - 1]

                                                                                  )));
            return rtltup;
        }
        public List<Tuple<string, double,string>> getLocalMaxAndMin(List<Tuple<string,double, double>> ltp)
        {
            List<Tuple<string, double, string>> rtll = new List<Tuple<string, double, string>>();
            for (int i=1; i < ltp.Count - 1; i++)
            {
                string mm = "";
                if (ltp[i].Item3 >= ltp[i - 1].Item3 && ltp[i].Item3 >= ltp[i + 1].Item3) mm = "LMAX";
                else if (ltp[i].Item3 <= ltp[i - 1].Item3 && ltp[i].Item3 <= ltp[i + 1].Item3) mm = "LMIN";
                rtll.Add(Tuple.Create<string, double, string>(ltp[i].Item1,ltp[i].Item3,mm ));
            }
            rtll.Add(Tuple.Create<string, double, string>(ltp[ltp.Count - 1].Item1, ltp[ltp.Count - 1].Item3, ""));
            return rtll;
        }

        public double calculateAccerator(double xk,double xkf,double xkb,double yk,double ykf,double ykb,bool denominator_plus1=true) 
        {
            double up = 2*(((ykf-yk)*(xk-xkb))-((yk-ykb)*(xkf-xk)));
            double down = (xkf - xk) * (xk - xkb) * (xkf + xkb - (2 * xk));
            double rt = 0;
            if (denominator_plus1) rt = up / (down + 1);
            else rt = up / down;


            return rt;
        }

    }
    public class DensityThresholdEstimator: CriticalPointEstimator
    {
        public List<Tuple<string, double,double>> getAreaPerCase()
        {

            List<Tuple<string, double,double>> rtltup = new List<Tuple<string, double,double>>();
            for(int i=1; i<Xset.Count;i++){
                rtltup.Add(Tuple.Create<string,double,double>(Tag[i],Xset[i] ,(Yset[i]-Yset[i-1])/(Xset[i]-Xset[i-1]+0.0000001) ));
            }

            return rtltup;
        }
        public List<Tuple<string, double, string>> getMaxAreaChangeSlope (List<Tuple<string, double, double>> archg)
        {
            List<Tuple<string, double, string>> rtll = new List<Tuple<string, double, string>>();
            int mxidx = 0; double mxachg = -99999999999;
            //find max
            for (int i=0;i<archg.Count; i++)
            {
                if(archg[i].Item3 >=mxachg)
                {
                    mxachg=archg[i].Item3;
                    mxidx = i;
                }


            }

            //dfs
            List<int> isslope = new List<int>();
            int threshold = -1;

           // double fu = Math.Exp((Math.Log((Yset[mxidx] / Xset[mxidx]) / (Yset[mxidx - 1] / Xset[mxidx - 1]))) / (Xset[mxidx] - Xset[mxidx - 1]));
           // System.Windows.Forms.MessageBox.Show(fu.ToString());
            for(int j=mxidx-1;j>=1; j--)
            {
                double fd2y = 0.0;double bd2y = 0.0;
                for (int g = 1; g < j; g++)
                {
                   fd2y+= ( (archg[g].Item3 - archg[g - 1].Item3) / (archg[g].Item2 - archg[g - 1].Item2));
                }
                //fd2y /= (archg[j-1].Item2 - archg[0].Item2);
                fd2y /= j-1-0;
                for (int ph=mxidx; ph > j; ph--)
                {
                   bd2y +=( (archg[ph].Item3 - archg[ph - 1].Item3) / (archg[ph].Item2 - archg[ph - 1].Item2));
                }
                //bd2y /= (archg[mxidx].Item2 - archg[j].Item2);
                bd2y /= mxidx - j;
                double currentd2y = (archg[j].Item3 - archg[j - 1].Item3) / (archg[j].Item2 - archg[j - 1].Item2);
               
                if (  Math.Abs(currentd2y-fd2y) > Math.Abs(bd2y-currentd2y )  )
                {
                    //System.Windows.Forms.MessageBox.Show(Math.Pow(fu, archg[j].Item2).ToString()+" " + (Yset[j] / Xset[j]).ToString());
                    isslope.Add(j);
                }
                else
                {
                    //greedy
                    threshold = j;
                    break;
                }
            }

            //write
            for(int i = 0; i< archg.Count; i++)
            {
                string infstr = "";
                //isslope
                 
                if (isslope.Contains(i)  ) infstr += "Slope,";
                //isoutlier
                if (i > mxidx) infstr += "Outlier,";
                //maxchg
                if (i == mxidx) infstr += "Max Change,";
                //threshold
                if (i == threshold) infstr += "Threshold,";
                rtll.Add(Tuple.Create<string, double, string>(archg[i].Item1,archg[i].Item3,infstr));
            }


            return rtll;

        }
    }


}
