using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseModel
{
    public class GouMaiJiLu
    {
        public int PId { get; set; }
        public decimal PMoney { get; set; }
        public DateTime PDate { get; set; }
        public short PDelFlag { get; set; }
        public string Payner { get; set; }
    }
}
