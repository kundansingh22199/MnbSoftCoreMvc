using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity
{
    public class ModSalePurchase
    {
        public int id { get; set; }
        public string partnerCode { get; set; }
        public string productName { get; set; }
        public string planName { get; set; }
        public string planTypeTaskflier { get; set; }
        public string planType { get; set; }
        public string serverName { get; set; }
        public string config { get; set; }
        public int noOfUser { get; set; }
        public string company { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public decimal amount { get; set; }
        public decimal comission { get; set; }
        public decimal commissionPer { get; set; }
        public string saleType { get; set; }
        public string remarks { get; set; }
        public int status { get; set; }
        public DateTime createDate { get; set; }
        public DateTime updateDate { get; set; }
        public string productImage { get; set; }
        public string adminRemarks { get; set; }
    }
}
