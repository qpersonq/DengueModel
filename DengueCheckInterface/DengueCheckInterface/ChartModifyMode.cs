using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DengueCheckInterface
{
    class ChartModifyMode
    {
        public static bool IsChinese = true;
        public static void  RMSEMD(TextBox xtx,TextBox ytx)
        {
            xtx.Text = "Epochs";
            ytx.Text = "RMSE";
        }
        public static void CasedensityMD(TextBox xtx, TextBox ytx)
        {
            if (IsChinese)
            {
                xtx.Text = "病例數 (case)";
                ytx.Text = "密度 (case/(km^2 day))";
            }
            else
            {
                xtx.Text = "Number of cases (case)";
                ytx.Text = "Density (case/(km^2 day))";
            }
            
            
        }
        public static void CaseareaMD(TextBox xtx, TextBox ytx)
        {
            if (IsChinese)
            {
                xtx.Text = "病例數 (case)";
                ytx.Text = "總和傳染面積乘發病間距 (km^2 day)";
            }
            else
            {
                xtx.Text = "Number of cases (case)";
                ytx.Text = "Sum of the areas * days (km^2 day)";
            }
           
        }
        public static void PercentagedensityMD(TextBox xtx, TextBox ytx)
        {
            xtx.Text = "Predicted propotion (predicted number of cases/number of all cases)";
            ytx.Text = "Density (case/(km^2 day))";
        }
        public static void PercentageareaMD(TextBox xtx, TextBox ytx)
        {
            xtx.Text = "Predicted propotion (predicted number of cases/number of all cases)";
            ytx.Text = "Sum of the areas * days (KM^2 days)";
        }
        public static void DcaseMD(TextBox xtx, TextBox ytx,TextBox y2tx)
        {
            if (IsChinese)
            {
                xtx.Text = "病例數 (case)";
                y2tx.Text ="總和傳染面積乘發病間距 (km^2 day)";
            }
            else
            {
                xtx.Text = "Number of cases (case)";
                y2tx.Text = "Sum of the areas * days (km^2 day)";
            }
        }
        public static void SeedCoverageCaseMD(TextBox xtx,TextBox ytx,TextBox yvalueFormat)
        {
            yvalueFormat.Text = "0%";
            if (IsChinese)
            {
                xtx.Text = "預估涵蓋率 (%)";
                ytx.Text = "病例數 (case)";
            }
            else
            {
                xtx.Text = "Estimation of coverage (%)";
                ytx.Text = "Number of cases (case)";
            }
        }

    }


    }

