using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PurchaseCommon;
using PurchaseModel;
using PurchaseBll;


namespace PurchaseRecord
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        PurchaseRecordsBll prb = new PurchaseRecordsBll();

        private void FormMain_Load(object sender, EventArgs e)
        {

            //MessageBox.Show(DateTime.Now.ToString());
            cbbYear.Items.AddRange(new string[] { "2017", "2018" });
            cbbMonth.Items.AddRange(new string[] { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12" });
            string date = DateTime.Now.ToString();
            string[] str = date.Split(new char[] { '/', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            cbbYear.SelectedIndex = 0;
            this.cbbMonth.Text = int.Parse(str[1]) >= 10 ? str[1] : "0" + str[1];
            //LoadDay(str[0], str[1]);
            this.cbbDay.Text = int.Parse(str[2]) >= 10 ? str[2] : "0" + str[2];
            LoadList();
            LoadTongJiMonth();
            LoadTongJiYear();

        }

        private void LoadList()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            //string time = DateTime.Now.ToString();
            //string[] str = time.Split(new char[] { '/', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string timeNew = cbbYear.Text + "-" + cbbMonth.Text;// +"-" + cbbDay.Text;// +" " + str[3];
            dic.Add("PDate", timeNew);
            //MessageBox.Show(timeNew);
            List<GouMaiJiLu> list = prb.GetList(dic);
            dgvList.AutoGenerateColumns = false;
            dgvList.DataSource = list;
        }

        private void LoadTongJiMonth()
        {
            lblBen.Text = "0";
            lblZei.Text = "0";
            lblMonth.Text = cbbYear.Text + "年" + cbbMonth.Text + "月";
            string dateMonth = lblMonth.Text.Replace("年", "-").Substring(0, 7);
            Dictionary<int, string> dic = prb.selectSum(dateMonth);
            foreach (var pair in dic)
            {
                switch (pair.Key)
                {
                    case 0: lblBen.Text = pair.Value; break;
                    case 1: lblZei.Text = pair.Value; break;
                }
            }
        }

        private void LoadTongJiYear()
        {
            lblYearBen.Text = "0";
            lblYearZei.Text = "0";
            lblYear.Text = cbbYear.Text;
            Dictionary<int, string> dic = prb.selectSum(lblYear.Text);
            foreach (var pair in dic)
            {
                switch (pair.Key)
                {
                    case 0: lblYearBen.Text = pair.Value; break;
                    case 1: lblYearZei.Text = pair.Value; break;
                }
            }
        }

        private void LoadDay(string year, string month)
        {
            int yearNew = int.Parse(year);
            int monthNew = int.Parse(month);
            bool b = YearHelper.JudgeRun(year);
            switch (monthNew)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    for (int i = 1; i < 32; i++)
                    {
                        if (i < 10)
                        {
                            cbbDay.Items.Add("0" + i.ToString());
                        }
                        else
                        {
                            cbbDay.Items.Add(i.ToString());
                        }

                    }
                    break;
                case 2:
                    if (b)
                    {
                        for (int i = 1; i < 30; i++)
                        {
                            if (i < 10)
                            {
                                cbbDay.Items.Add("0" + i.ToString());
                            }
                            else
                            {
                                cbbDay.Items.Add(i.ToString());
                            }
                        }
                    }
                    else
                    {
                        for (int i = 1; i < 29; i++)
                        {
                            if (i < 10)
                            {
                                cbbDay.Items.Add("0" + i.ToString());
                            }
                            else
                            {
                                cbbDay.Items.Add(i.ToString());
                            }
                        }

                    }
                    break;
                default:
                    for (int i = 1; i < 31; i++)
                    {
                        if (i < 10)
                        {
                            cbbDay.Items.Add("0" + i.ToString());
                        }
                        else
                        {
                            cbbDay.Items.Add(i.ToString());
                        }
                    }
                    break;
            }

        }

        private void cbbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbYear.Text == "" || cbbMonth.Text == "")
            {
                return;
            }
            cbbDay.Items.Clear();
            LoadDay(cbbYear.Text, cbbMonth.Text);
            LoadList();
            LoadTongJiMonth();
            LoadTongJiYear();
        }

        private void cbbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbYear.Text == "" || cbbMonth.Text == "")
            {
                return;
            }
            cbbDay.Items.Clear();
            LoadDay(cbbYear.Text, cbbMonth.Text);
            LoadList();
            LoadTongJiMonth();
        }


        //添加购买记录
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string time = DateTime.Now.ToString();
            string[] str = time.Split(new char[] { '/', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string timeNew = cbbYear.Text + "-" + cbbMonth.Text + "-" + cbbDay.Text + " " + str[3];
            GouMaiJiLu gmjl = new GouMaiJiLu();
            if (rdoBen.Checked == false && rdoZei.Checked == false)
            {
                MessageBox.Show("请选择大款!");
                return;
            }
            gmjl.PId = rdoZei.Checked ? 1 : 0;
            gmjl.PDate = DateTime.Parse(timeNew);
            decimal money;
            if (decimal.TryParse(txtMoney.Text, out money))
            {
                gmjl.PMoney = money;
            }
            else
            {
                MessageBox.Show("请输入正确的金额,silly b!");
                return;
            }
            if (prb.Add(gmjl))
            {
                LoadList();
                LoadTongJiMonth();
                LoadTongJiYear();
                txtMoney.Text = "";
            }

        }







    }
}
