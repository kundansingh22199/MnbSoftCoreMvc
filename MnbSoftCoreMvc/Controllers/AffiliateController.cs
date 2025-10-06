using BusinessEntity;
using IContract;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace MnbSoftCoreMvc.Controllers
{
    public class AffiliateController : Controller
    {
        private readonly IDataAccess _dataAccess;
        private readonly IBusinessAccess _businessAccess;
        public AffiliateController(IDataAccess dataAccess, IBusinessAccess businessAccess)
        {
            _dataAccess = dataAccess;
            _businessAccess = businessAccess;
        }
        public async Task<IActionResult> Dashboard()
        {
            string partnercode = HttpContext.Session.GetString("pCode");
            if (string.IsNullOrEmpty(partnercode))
            {
                return Redirect("../../affiliate-login");
            }
            ModPartnerDashboard obj = new ModPartnerDashboard();
            obj = await _businessAccess.getAffiliateDashboard(partnercode);
            return View(obj);
        }
        public async Task<IActionResult> AffiliateDetails()
        {
            ModAffiliate obj = new ModAffiliate();
            try
            {
                string email = HttpContext.Session.GetString("email");
                if (string.IsNullOrEmpty(email))
                {
                    return Redirect("../../affiliate-login");
                }
                obj = (await _businessAccess.getAffiliateReport()).FirstOrDefault(x => x.email == email);
            }
            catch (Exception)
            {

            }
            return View(obj);
        }
        public async Task<IActionResult> BankInformation()
        {
            ModBankDetails objdetails = new ModBankDetails();
            string partnerCode = HttpContext.Session.GetString("pCode");
            if (string.IsNullOrEmpty(partnerCode))
            {
                return Redirect("../../affiliate-login");
            }
            objdetails = await _businessAccess.getBankInfo(partnerCode);
            return View(objdetails);
        }
        [HttpPost]
        public async Task<IActionResult> BankInformation(ModBankDetails obj)
        {
            ModBankDetails objdetails = new ModBankDetails();
            string partnerCode = HttpContext.Session.GetString("pCode");
            if (string.IsNullOrEmpty(partnerCode))
            {
                return Redirect("../../affiliate-login");
            }
            obj.partnerCode = partnerCode;
            if (await _dataAccess.AddPartnerBank(obj))
            {
                TempData["Success"] = "Bank Details Save Successfully";
            }
            else
            {
                TempData["Err"] = "Failed to Update Bank Information";
            }
            objdetails = await _businessAccess.getBankInfo(partnerCode);
            return View(objdetails);
        }
        public IActionResult UpdatePassword()
        {
            string partnerCode = HttpContext.Session.GetString("pCode");
            if (string.IsNullOrEmpty(partnerCode))
            {
                return Redirect("../../affiliate-login");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdatePassword(string oldPass, string newPass, string confPass)
        {
            string msg = "";
            string partnerCode = HttpContext.Session.GetString("pCode");
            if (string.IsNullOrEmpty(partnerCode))
            {
                return Redirect("../../affiliate-login");
            }
            if (!string.IsNullOrEmpty(oldPass) && !string.IsNullOrEmpty(newPass) && !string.IsNullOrEmpty(confPass))
            {
                msg = await _dataAccess.UpdateAffiliatePass(partnerCode, oldPass, newPass, confPass);
            }
            if(msg== "Password Update Successfully")
            {
                HttpContext.Session.Clear();
                HttpContext.SignOutAsync();
                return Redirect("../../affiliate-login");
            }
            TempData["Success"] = msg;
            return View();
        }
        public async Task<IActionResult> SaleRequest()
        {
            string partnercode = HttpContext.Session.GetString("pCode");
            if (string.IsNullOrEmpty(partnercode))
            {
                return Redirect("../../affiliate-login");
            }
            ModPartnerDashboard obj = new ModPartnerDashboard();
            obj = await _businessAccess.getPartnerDashboard(partnercode);
            ViewBag.rank = obj.rankNo;
            return View();
        }
        [HttpGet("/Affiliate/Conversions/{st?}")]
        public async Task<IActionResult> Conversions(int? st)
        {
            List<ModSalePurchase> lst = new List<ModSalePurchase>();
            string partnerCode = HttpContext.Session.GetString("pCode");

            if (string.IsNullOrEmpty(partnerCode))
            {
                return Redirect("../../affiliate-login");
            }

            lst = await _businessAccess.getSalePurchaseReport(partnerCode);
            if (st.HasValue)
            {
                if (st.Value == 1)
                {
                    ViewBag.titlename = "Total Sale Data Report";
                    lst = lst.Where(x => x.status == 1).ToList();
                }
                else if (st.Value == 2)
                {
                    ViewBag.titlename = "Sale Amount Monthly Report";
                    lst = lst.Where(x => x.createDate.Month == DateTime.Now.Month && x.createDate.Year == DateTime.Now.Year && x.status == 1).ToList();
                }
            }
            else
            {
                ViewBag.titlename = "Product wise Sale Report";
            }
            return View(lst);
        }
        public async Task<IActionResult> Commissions()
        {
            string partnerCode = HttpContext.Session.GetString("pCode");
            if (string.IsNullOrEmpty(partnerCode))
            {
                return Redirect("../../affiliate-login");
            }
            List<ModComission> lst = new List<ModComission>();
            lst = (await _businessAccess.getComissionReport()).Where(x => x.partnerCode == partnerCode).ToList();
            return View(lst);
        }
        public async Task<IActionResult> BalanceMaster()
        {
            string partnerCode = HttpContext.Session.GetString("pCode");
            if (string.IsNullOrEmpty(partnerCode))
            {
                return Redirect("../../affiliate-login");
            }

            DataTable dt = await _dataAccess.AdminReport("balancemaster");

            if (dt != null && dt.Rows.Count > 0)
            {
                var filtered = dt.AsEnumerable().Where(r => r.Field<string>("partnerCode") == partnerCode);
                if (filtered.Any())
                    dt = filtered.CopyToDataTable();
                else
                    dt = dt.Clone();
            }

            ViewBag.BalanceData = dt;
            return View();
        }
        public async Task<IActionResult> Profile()
        {
            ModAffiliate obj = new ModAffiliate();
            try
            {
                string email = HttpContext.Session.GetString("email");
                string password = HttpContext.Session.GetString("password");
                if (string.IsNullOrEmpty(email))
                {
                    return Redirect("../../affiliate-login");
                }
                obj = (await _businessAccess.getAffiliateReport()).FirstOrDefault(x => x.email == email);
            }
            catch (Exception)
            {

            }
            return View(obj);
        }
        public async Task<IActionResult> UnSettledAmount()
        {
            string partnerCode = HttpContext.Session.GetString("pCode");
            string email = HttpContext.Session.GetString("email");
            if (string.IsNullOrEmpty(partnerCode))
            {
                return Redirect("../../affiliate-login");
            }
            ModPartnerDashboard obj1 = new ModPartnerDashboard();
            obj1 = await _businessAccess.getPartnerDashboard(email);
            ViewBag.balance = obj1.balance;
            List<ModBankDetails> lstbnk = await _businessAccess.getAdminBankInfo();
            ViewBag.partnerCode = partnerCode;
            ViewBag.adminbank = lstbnk;

            List<ModComission> lst = new List<ModComission>();
            lst = (await _businessAccess.getComissionReport()).Where(x => x.partnerCode == partnerCode && (x.status == 0 || x.status == 2)).ToList();
            return View(lst);
        }
        public async Task<IActionResult> SettledAmount()
        {
            string partnerCode = HttpContext.Session.GetString("pCode");
            if (string.IsNullOrEmpty(partnerCode))
            {
                return Redirect("../../affiliate-login");
            }
            List<ModComission> lst = new List<ModComission>();
            lst = (await _businessAccess.getComissionReport()).Where(x => x.partnerCode == partnerCode && x.status == 1).ToList();
            return View(lst);
        }
        public async Task<IActionResult> PendingSettlement()
        {
            string partnerCode = HttpContext.Session.GetString("pCode");
            if (string.IsNullOrEmpty(partnerCode))
            {
                return Redirect("../../affiliate-login");
            }
            List<ModSettlement> lst = new List<ModSettlement>();
            lst = (await _businessAccess.getSettlementReport()).Where(x => x.partnerCode == partnerCode && x.status == 0).ToList();
            return View(lst);
        }
        public async Task<IActionResult> CompleteSettlement()
        {
            string partnerCode = HttpContext.Session.GetString("pCode");
            if (string.IsNullOrEmpty(partnerCode))
            {
                return Redirect("../../affiliate-login");
            }
            List<ModSettlement> lst = new List<ModSettlement>();
            lst = (await _businessAccess.getSettlementReport()).Where(x => x.partnerCode == partnerCode && x.status == 1).ToList();
            return View(lst);
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            HttpContext.SignOutAsync();
            return Redirect("../../affiliate-login");
        }
    }
}
