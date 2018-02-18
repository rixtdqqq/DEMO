using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PurchaseModel;
using PurchaseDal;

namespace PurchaseBll
{
    public partial class PurchaseRecordsBll
    {
        PurchaseRecordsDal prd = new PurchaseRecordsDal();
        public List<GouMaiJiLu> GetList(Dictionary<string, string> dic)
        {
            return prd.Getlist(dic);
        }

        //添加购买记录
        public bool Add(GouMaiJiLu gmjl)
        {
            return prd.insert(gmjl) > 0;
        }

        //查询月度年度统计
        public Dictionary<int, string> selectSum(string date)
        {
            return prd.selectSum(date);
        }
    }
}
