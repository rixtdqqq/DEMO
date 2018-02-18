using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using PurchaseModel;

namespace PurchaseDal
{
    public partial class PurchaseRecordsDal
    {
        public List<GouMaiJiLu> Getlist(Dictionary<string, string> dic)
        {
            List<GouMaiJiLu> list = new List<GouMaiJiLu>();
            string sql = "select R.*,M.MName as Payner from Records as R inner join MyPay as M on R.Pid=M.Mid WHERE pDELFLAG=1";
            foreach (var pair in dic)
            {
                sql += " and " + pair.Key + " like '" + pair.Value + "%'";
            }
            DataTable dt = new DataTable();
            dt = SqliteHelper.GetDataTable(sql);
            foreach (DataRow row in dt.Rows)
            {
                list.Add(new GouMaiJiLu()
                {
                    PId = Convert.ToInt32(row["pid"]),
                    PMoney = Convert.ToDecimal(row["pmoney"]),
                    PDate = Convert.ToDateTime(row["pdate"]),
                    Payner = row["payner"].ToString()
                });
            }
            return list;
        }

        //添加购买记录
        public int insert(GouMaiJiLu gmjl)
        {
            string sql = "insert into Records(pid,pmoney,pdate,pdelflag) values(@pid,@pmoney,@pdate,1)";
            SQLiteParameter[] paras ={
                                        new SQLiteParameter("@pid",gmjl.PId),
                                        new SQLiteParameter("@pmoney",gmjl.PMoney),
                                        new SQLiteParameter("@pdate",gmjl.PDate)
                                    };
            return SqliteHelper.ExecuteNonQuery(sql, paras);
        }

        //查询支付月度年度统计
        public Dictionary<int, string> selectSum(string date)
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            string sql = "select pid,sum(pmoney) from Records  where pdate like " + "'" + date + "%'" + " and pdelflag=1 group by pid";
            //string sql = "select pid,sum(pmoney) from Records  where pdate like '2017-09%' and pdelflag=1 group by pid";
            //select pid,sum(pmoney) from Records  where pdate like '2017-09%' and pdelflag=1 group by pid
            //string dateNew = "'" + date + "%'";
            //SQLiteParameter para = new SQLiteParameter("@date", dateNew);
            DataTable dt = SqliteHelper.GetDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    dic.Add(int.Parse(row["pid"].ToString()), row["sum(pmoney)"].ToString());
                }
            }
            return dic;

        }

    }
}
