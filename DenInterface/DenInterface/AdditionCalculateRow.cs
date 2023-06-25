using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DenInterface
{
    public abstract class AdditionCalculateRow
    {
        protected int ElementNumber;
        protected int RowNumber;
        protected List<List<double>> InnerDotMatrix;

        
        public AdditionCalculateRow(int elementnumber,int rownumber)
        {
            ElementNumber = elementnumber;
            RowNumber = rownumber;
            InnerDotMatrix = new List<List<double>>();
            //calculateWeight();
        }

        protected abstract void calculateWeight();
            /*
        {
            
            for(int i=0;i<ElementNumber; i++)//
            {
                List<double> mtlvec=new List<double>();
                for(int j=0;j<RowNumber; j++)
                {
                    mtlvec.Add(1);
                }//cal limited row number
                
                InnerDotMatrix.Add(mtlvec);

            }//cal elements number
            
        }*/
        public double getCalculate(double originalvalue_,int idx_,int rwnm_) 
        {
            return originalvalue_ * InnerDotMatrix[idx_][rwnm_];
        }

    }
    public class AdditionCalculateRowConst:  AdditionCalculateRow
    {
        private double constNumber;
        public AdditionCalculateRowConst(double cstNM,int elementnumber, int rownumber): base(elementnumber,rownumber)
        {
            constNumber = cstNM;
            calculateWeight();
        }
        protected override void calculateWeight()        
        {
            InnerDotMatrix.Clear();
            for (int i=0;i<ElementNumber; i++)//
            {
                List<double> mtlvec=new List<double>();
                for(int j=0;j<RowNumber; j++)
                {
                    mtlvec.Add(constNumber);
                }//cal limited row number

                InnerDotMatrix.Add(mtlvec);

            }//cal elements number

        }
    }//end constaddcalrw
    public class AdditionCalculateRowExponential : AdditionCalculateRow
    {
        private List<List<double>> ExponentialBound;
       
        
        public AdditionCalculateRowExponential(List<List<double>> expbd, int elementnumber, int rownumber) : base(elementnumber, rownumber)
        {
            ExponentialBound = expbd;
                         /*
                        string shtx="";
                        foreach(var a in ExponentialBound)
                        {
                            foreach(var b in a)
                            {
                                shtx += b.ToString()+" ";
                            }
                            shtx += "\n";
                        }
                        //MessageBox.Show(shtx);
                        */
            calculateWeight();
        }
        protected override void calculateWeight()
        {
            InnerDotMatrix.Clear();
            for (int i = 0; i < ElementNumber; i++)//
            {
                List<double> mtlvec = new List<double>();
                for (int j = 0; j < RowNumber; j++)
                {
                    double lb = ExponentialBound[i][0];
                    double ub = ExponentialBound[i][1];


                    mtlvec.Add( Math.Exp(     lb+((ub-lb)*((double)j/(double)RowNumber) )           )  );
                }//cal limited row number

                InnerDotMatrix.Add(mtlvec);

            }//cal elements number

        }
    }//end constaddcalrw



}
