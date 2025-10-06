using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity
{
    public class ModPartnerDashboard
    {
        public string partnerCode { get; set; }
        public string sponsorCode { get; set; }
        public string company { get; set; }
        public string companyWebSite { get; set; }
        public string fullName { get; set; }
        public int totalSale { get; set; }
        public int monthlySale { get; set; }
        public decimal totalSaleAmount { get; set; }
        public decimal monthlySaleAmount { get; set; }
        public string referralLink { get; set; }
        public string rankNo { get; set; }
        public decimal settledAmount { get; set; }
        public decimal unSettledAmount { get; set; }
        public decimal comission { get; set; }
        public decimal balance { get; set; }
        public decimal pendingSettlement { get; set; }
        public decimal completeSettlement { get; set; }
        public List<ModSalePurchase> salePurchaseReport { get; set; }
        public List<ModSaleReport> totalReport { get; set; }
    }
    public class ModSaleReport
    {
        public string productName { get; set; }
        public string productImage { get; set; }
        public int thisMonthCount { get; set; }
        public decimal thisMonthAmount { get; set; }
        public int totalSale { get; set; }
        public decimal totalSaleAmount { get; set; }
    }
}
