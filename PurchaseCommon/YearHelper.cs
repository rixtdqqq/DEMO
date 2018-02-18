using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseCommon
{
    public class YearHelper
    {
        //年份能够被400整除
        //年份能够被4整除且不能被100整除
        //满足以上两个条件其中之一，则该年份为闰年
        public static bool JudgeRun(string year)
        {
            int yearNew = int.Parse(year);
            bool b = true;
            if (yearNew % 400 == 0 || (yearNew % 4 == 0 && yearNew % 100 != 0))
            {
                
            }
            else
            {
                b = false;
            }
            return b;
        }
    }
}
