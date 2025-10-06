using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity
{
    public class ModAdminDashboard
    {
        public int totalPartner { get; set; }
        public int pendingPartner { get; set; }
        public int activePartner { get; set; }
        public int rejectPartner { get; set; }
        public int blockPartner { get; set; }
        public int totalAffiliate { get; set; }
        public int pendingAffiliate { get; set; }
        public int activeAffiliate { get; set; }
        public int rejectAffiliate { get; set; }
        public int blockAffiliate { get; set; }
        public int activeJob { get; set; }
        public int totalApply { get; set; }
        public int pendingApply { get; set; }
        public int activeApply { get; set; }
        public int rejectApply { get; set; }
        public int totalEnquiry { get; set; }
        public decimal totalComission { get; set; }
        public decimal totalPayout { get; set; }
        public decimal totalSale { get; set; }
        public decimal monthlyComission { get; set; }
        public decimal monthlyPayout { get; set; }
        public decimal monthlySale { get; set; }
        public List<ModSalePurchase> salePurchaseReport { get; set; }
        public List<ModSaleReport> totalReport { get; set; }
    }
}
