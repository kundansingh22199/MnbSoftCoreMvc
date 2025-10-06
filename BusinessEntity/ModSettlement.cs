using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity
{
    public class ModSettlement
    {
        public int id { get; set; }

        public string partnerCode { get; set; }
        public string selectedIds { get; set; }
        public decimal totalAmount { get; set; }
        public decimal totalCommission { get; set; }
        public decimal payableAmount { get; set; }
        public decimal netPayableAmount { get; set; }
        public decimal gstPer { get; set; } = 0;
        public decimal tdsPer { get; set; } = 0;
        public string utrNo { get; set; }
        public decimal gstAmount { get; set; } = 0;
        public decimal tdsAmount { get; set; } = 0;
        public decimal walletUsed { get; set; } = 0;
        public string bankId { get; set; }
        public string bankName { get; set; }
        public string accountNo { get; set; }
        public string ifsc { get; set; }
        public int status { get; set; }
        public DateTime createdDate { get; set; }
    }
}
