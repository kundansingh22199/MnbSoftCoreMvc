using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity
{
    public class ModBankDetails
    {
        public int id { get; set; }
        public string partnerCode { get; set; }
        public string bankName { get; set; }
        public string branch { get; set; }
        public string accountHolder { get; set; }
        public string accountNo { get; set; }
        public string accountType { get; set; }
        public string ifsc { get; set; }
        public int status { get; set; }
        public DateTime createDate { get; set; }
        public DateTime updateDate { get; set; }
        public string fullName { get; set; }
        public string businessEmail { get; set; }
        public string mobile { get; set; }
        public decimal amount { get; set; }
    }
}
