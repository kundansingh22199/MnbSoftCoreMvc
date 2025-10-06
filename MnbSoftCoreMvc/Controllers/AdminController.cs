using BusinessEntity;
using IContract;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;

namespace MnbSoftCoreMvc.Controllers
{
    public class AdminController : Controller
    {
        private readonly IDataAccess _dataAccess;
        private readonly IBusinessAccess _businessAccess;
        public AdminController(IDataAccess dataAccess, IBusinessAccess businessAccess)
        {
            _dataAccess = dataAccess;
            _businessAccess = businessAccess;
        }
        public async Task<IActionResult> Dashboard()
        {
            string userid = HttpContext.Session.GetString("userid");
            if (string.IsNullOrEmpty(userid))
            {
                return Redirect("../../partner-login");
            }
            ModAdminDashboard obj = new ModAdminDashboard();
            obj = await _businessAccess.getAdminDashboard();
            return View(obj);
        }
        [HttpGet("/Admin/PartnerDetails/{st?}")]
        public async Task<IActionResult> PartnerDetails(int? st)
        {
            string userid = HttpContext.Session.GetString("userid");
            if (string.IsNullOrEmpty(userid))
            {
                return Redirect("../../partner-login");
            }
            List<ModPartner> lst = new List<ModPartner>();
            lst = await _businessAccess.getPartnerReport();
            if (st.HasValue)
            {
                if (st.Value == 0)
                {
                    ViewBag.titlename = "Total Pending Partner Report";
                    lst = lst.Where(x => x.status == 0).ToList();
                }
                else if (st.Value == 1)
                {
                    ViewBag.titlename = "Total Active Partner Report";
                    lst = lst.Where(x => x.status == 1).ToList();
                }
                else if (st.Value == 2)
                {
                    ViewBag.titlename = "Total Rejected Partner Report";
                    lst = lst.Where(x => x.status == 2).ToList();
                }
                else if (st.Value == 3)
                {
                    ViewBag.titlename = "Total Block Partner Report";
                    lst = lst.Where(x => x.status == 3).ToList();
                }
            }
            else
            {
                ViewBag.titlename = "All Partner Report";
            }
            return View(lst);
        }
        [HttpPost("/Admin/PartnerDetails")]
        public async Task<IActionResult> PartnerDetails(int id, int status)
        {
            string userid = HttpContext.Session.GetString("userid");
            if (string.IsNullOrEmpty(userid))
            {
                return Redirect("../../partner-login");
            }
            if (await _dataAccess.UpdatePartner(id, status,1))
            {
                //TempData["Success"] = "Partner Update Successfully";
                TempData["id"] = id.ToString();
                if (status == 1)
                {
                    ModPartner obj = new ModPartner();
                    obj = (await _businessAccess.getPartnerReport()).FirstOrDefault(x => x.id == id);
                    await _dataAccess.SendRegistrationApproveMail(obj);
                    return RedirectToAction("PartnerSuccess");
                    //TempData["Success"] = "Partner Approved Successfully";
                }
                else
                {
                    TempData["Success"] = "Partner Reject Successfully";
                }
            }
            else
            {
                TempData["Err"] = "Failed to Update Partner Details";
            }
            List<ModPartner> lst = new List<ModPartner>();
            lst = (await _businessAccess.getPartnerReport()).Where(x => x.status == 0).ToList();
            return View(lst);
        }
        public async Task<IActionResult> PartnerSuccess()
        {
            if (TempData["id"] == null)
            {
                TempData["Err"] = "Invalid Partner ID";
                return RedirectToAction("PartnerDetails");
            }
            TempData.Keep();
            int id = Convert.ToInt32(TempData["id"]);
            var partner = (await _businessAccess.getPartnerReport()).FirstOrDefault(x => x.id == id);
            if (partner == null)
            {
                TempData["Err"] = "Partner not found";
                return RedirectToAction("PartnerDetails");
            }

            return View(partner);
        }
        [HttpGet("/Admin/AffiliateReport/{st?}")]
        public async Task<IActionResult> AffiliateReport(int? st)
        {
            string userid = HttpContext.Session.GetString("userid");
            if (string.IsNullOrEmpty(userid))
            {
                return Redirect("../../partner-login");
            }
            List<ModAffiliate> lst = new List<ModAffiliate>();
            lst = await _businessAccess.getAffiliateReport();
            if (st.HasValue)
            {
                if (st.Value == 0)
                {
                    ViewBag.titlename = "Total Pending Affiliate Report";
                    lst = lst.Where(x => x.status == 0).ToList();
                }
                else if (st.Value == 1)
                {
                    ViewBag.titlename = "Total Active Affiliate Report";
                    lst = lst.Where(x => x.status == 1).ToList();
                }
                else if (st.Value == 2)
                {
                    ViewBag.titlename = "Total Rejected Affiliate Report";
                    lst = lst.Where(x => x.status == 2).ToList();
                }
                else if (st.Value == 3)
                {
                    ViewBag.titlename = "Total Block Affiliate Report";
                    lst = lst.Where(x => x.status == 3).ToList();
                }
            }
            else
            {
                ViewBag.titlename = "All Affiliate Report";
            }
            return View(lst);
        }
        [HttpPost("/Admin/AffiliateReport")]
        public async Task<IActionResult> AffiliateReport(int id, int status)
        {
            string userid = HttpContext.Session.GetString("userid");
            if (string.IsNullOrEmpty(userid))
            {
                return Redirect("../../partner-login");
            }
            if (await _dataAccess.UpdatePartner(id, status , 2))
            {
                TempData["id"] = id.ToString();
                if (status == 1)
                {
                    ModAffiliate obj = new ModAffiliate();
                    obj = (await _businessAccess.getAffiliateReport()).FirstOrDefault(x => x.id == id);
                    await _dataAccess.SendAffiliateApproveMail(obj);
                    return RedirectToAction("AffiliateSuccess");
                    //TempData["Success"] = "Affiliate Approved Successfully";
                }
                else
                {
                    TempData["Success"] = "Affiliate Reject Successfully";
                }
            }
            else
            {
                TempData["Err"] = "Failed to Update Affiliate Details";
            }
            List<ModAffiliate> lst = new List<ModAffiliate>();
            lst = (await _businessAccess.getAffiliateReport()).Where(x=>x.status==0).ToList();
            return View(lst);
        }
        public async Task<IActionResult> AffiliateSuccess()
        {
            if (TempData["id"] == null)
            {
                TempData["Err"] = "Invalid Partner ID";
                return RedirectToAction("AffiliateReport");
            }
            TempData.Keep();
            int id = Convert.ToInt32(TempData["id"]);
            var partner = (await _businessAccess.getAffiliateReport()).FirstOrDefault(x => x.id == id);
            if (partner == null)
            {
                TempData["Err"] = "Affiliate not found";
                return RedirectToAction("AffiliateReport");
            }

            return View(partner);
        }
        [HttpGet("/Admin/JobApplyReport/{st?}")]
        public async Task<IActionResult> JobApplyReport(int? st)
        {
            string userid = HttpContext.Session.GetString("userid");
            if (string.IsNullOrEmpty(userid))
            {
                return Redirect("../../partner-login");
            }
            List<ModCarrier> lst = new List<ModCarrier>();
            lst = await _businessAccess.getcarrierReport();
            if (st.HasValue)
            {
                if (st.Value == 0)
                {
                    ViewBag.titlename = "Total Pending Job Apply Report";
                    lst = lst.Where(x => x.status == 0).ToList();
                }
                else if (st.Value == 1)
                {
                    ViewBag.titlename = "Total Active Job Apply Report";
                    lst = lst.Where(x => x.status == 1).ToList();
                }
                else if (st.Value == 2)
                {
                    ViewBag.titlename = "Total Rejected Job Apply Report";
                    lst = lst.Where(x => x.status == 2).ToList();
                }
            }
            else
            {
                ViewBag.titlename = "All Job Apply Report";
            }
            return View(lst);
        }
        public async Task<IActionResult> ContactReport()
        {
            string userid = HttpContext.Session.GetString("userid");
            if (string.IsNullOrEmpty(userid))
            {
                return Redirect("../../partner-login");
            }
            List<ModContact> lst = new List<ModContact>();
            lst = await _businessAccess.getContactReport();
            return View(lst);
        }
        public async Task<IActionResult> Profile()
        {
            string userid = HttpContext.Session.GetString("userid");
            if (string.IsNullOrEmpty(userid))
            {
                return Redirect("../../partner-login");
            }
            DataTable dt=new DataTable();
            dt = await _dataAccess.AdminReport("profile");
            if (dt != null && dt.Rows.Count > 0)
            {
                ViewBag.ProfileRow = dt.Rows[0]; // pass only first row
            } 
            return View();
        }
        public IActionResult Earning()
        {
            string userid = HttpContext.Session.GetString("userid");
            if (string.IsNullOrEmpty(userid))
            {
                return Redirect("../../partner-login");
            }
            return View();
        }
        public IActionResult AccountSetting()
        {
            string userid = HttpContext.Session.GetString("userid");
            if (string.IsNullOrEmpty(userid))
            {
                return Redirect("../../partner-login");
            }
            return View();
        }
        public IActionResult HelpCenter()
        {
            string userid = HttpContext.Session.GetString("userid");
            if (string.IsNullOrEmpty(userid))
            {
                return Redirect("../../partner-login");
            }
            return View();
        }
        public async Task<IActionResult> AddRank()
        {
            string userid = HttpContext.Session.GetString("userid");
            if (string.IsNullOrEmpty(userid))
            {
                return Redirect("../../partner-login");
            }
            List<ModRank> lst = new List<ModRank>();
            lst = await _businessAccess.getrankNameReport();
            return View(lst);
        }
        [HttpPost]
        public async Task<IActionResult> AddRank(ModRank obj)
        {
            string userid = HttpContext.Session.GetString("userid");
            if (string.IsNullOrEmpty(userid))
            {
                return Redirect("../../partner-login");
            }
            List<ModRank> lst = new List<ModRank>();
            lst = await _businessAccess.getrankNameReport();
            if (!string.IsNullOrEmpty(obj.rankName))
            {
                bool exists = lst.Any(r => r.rankName.Equals(obj.rankName, StringComparison.OrdinalIgnoreCase));
                if (!exists)
                {
                    if (await _dataAccess.RankAdd(obj))
                    {
                        TempData["Success"] = "RankName Add Successfully";
                    }
                    else
                    {
                        TempData["Err"] = "Failed To Add Rank";
                    }
                }
                else
                {
                    TempData["Err"] = "Rank Name Already Exists";
                }
            }
            else
            {
                TempData["Err"] = "Rank Name Required";
            }
            lst = await _businessAccess.getrankNameReport();
            return View(lst);
        }
        public async Task<IActionResult> AddComission()
        {
            string userid = HttpContext.Session.GetString("userid");
            if (string.IsNullOrEmpty(userid))
            {
                return Redirect("../../partner-login");
            }
            List<ModRank> lst = new List<ModRank>();
            lst = await _businessAccess.getrankNameReport();
            ViewBag.rankname = lst;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddComission(ModComissionMaster obj)
        {
            string userid = HttpContext.Session.GetString("userid");
            if (string.IsNullOrEmpty(userid))
            {
                return Redirect("../../partner-login");
            }
            List<ModComissionMaster> lstcom = new List<ModComissionMaster>();
            lstcom = await _businessAccess.getRankComissionReport();
            if (!string.IsNullOrEmpty(obj.productName) && obj.rankId > 0 && obj.comissionPer > 0)
            {
                bool exists = lstcom.Any(r => r.productName.Equals(obj.productName, StringComparison.OrdinalIgnoreCase) && r.rankId==obj.rankId);
                if (!exists)
                {
                    if (await _dataAccess.AddRankComission(obj))
                    {
                        TempData["Success"] = "Rank Comission Add Successfully";
                    }
                    else
                    {
                        TempData["Err"] = "Failed To Add Rank Comission";
                    }
                }
                else
                {
                    TempData["Err"] = "Already Set Rank Comission";
                }
            }
            else
            {
                TempData["Err"] = "All Field Required";
            }
            List<ModRank> lst = new List<ModRank>();
            lst = await _businessAccess.getrankNameReport();
            ViewBag.rankname = lst;
            return View();
        }
        public async Task<IActionResult> ViewComission()
        {
            string userid = HttpContext.Session.GetString("userid");
            if (string.IsNullOrEmpty(userid))
            {
                return Redirect("../../partner-login");
            }
            List<ModComissionMaster> lst = new List<ModComissionMaster>();
            lst = await _businessAccess.getRankComissionReport();
            return View(lst);
        }
        [HttpGet("/Admin/Conversions/{st?}")]
        public async Task<IActionResult> Conversions(int? st)
        {
            string userid = HttpContext.Session.GetString("userid");
            if (string.IsNullOrEmpty(userid))
            {
                return Redirect("../../partner-login");
            }
            List<ModSalePurchase> lst = new List<ModSalePurchase>();
            
            lst = await _businessAccess.getAdminSalePurchaseReport();
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
        [HttpPost]
        public async Task<IActionResult> Conversions(int id, int status, string remarks)
        {
            string userid = HttpContext.Session.GetString("userid");
            if (string.IsNullOrEmpty(userid))
            {
                return Redirect("../../partner-login");
            }
            if (await _dataAccess.UpdateSalePurchase(id, status, remarks))
            {
                TempData["id"] = id.ToString();
                if (status == 1)
                {
                    TempData["Success"] = "Sale Approved Successfully";
                }
                else
                {
                    TempData["Success"] = "Sale Reject Successfully";
                }
            }
            else
            {
                TempData["Err"] = "Failed to Update Sale Details";
            }
            List<ModSalePurchase> lst = new List<ModSalePurchase>();
            lst = await _businessAccess.getAdminSalePurchaseReport();
            return View(lst);
        }
        [HttpGet("/Admin/Commissions/{st?}")]
        public async Task<IActionResult> Commissions(int? st)
        {
            string userid = HttpContext.Session.GetString("userid");
            if (string.IsNullOrEmpty(userid))
            {
                return Redirect("../../partner-login");
            }
            List<ModComission> lst = new List<ModComission>();
            lst = await _businessAccess.getComissionReport();
            if (st.HasValue)
            {
                if (st.Value == 1)
                {
                    ViewBag.titlename = "Total Comission Report";
                    lst = lst.Where(x => x.status == 1).ToList();
                }
                else if (st.Value == 2)
                {
                    ViewBag.titlename = "Monthly Comission Report";
                    lst = lst.Where(x => x.createDate.Month == DateTime.Now.Month && x.createDate.Year == DateTime.Now.Year && x.status == 1).ToList();
                }
            }
            else
            {
                ViewBag.titlename = "All Comission Report";
            }
            return View(lst);
        }
        public IActionResult WithdrawRequest()
        {
            string userid = HttpContext.Session.GetString("userid");
            if (string.IsNullOrEmpty(userid))
            {
                return Redirect("../../partner-login");
            }
            return View();
        }
        public IActionResult PayoutHistory()
        {
            string userid = HttpContext.Session.GetString("userid");
            if (string.IsNullOrEmpty(userid))
            {
                return Redirect("../../partner-login");
            }
            return View();
        }
        public async Task<IActionResult> PendingSettlement()
        {
            string userid = HttpContext.Session.GetString("userid");
            if (string.IsNullOrEmpty(userid))
            {
                return Redirect("../../partner-login");
            }
            List<ModSettlement> lst = new List<ModSettlement>();
            lst = (await _businessAccess.getSettlementReport()).Where(x=>x.status==0).ToList();
            return View(lst);
        }
        public async Task<IActionResult> SettlementDetails(string selectedIds)
        {
            string userid = HttpContext.Session.GetString("userid");
            if (string.IsNullOrEmpty(userid))
            {
                return Redirect("../../partner-login");
            }
            var ids = selectedIds.Split(',').Select(id => int.Parse(id)).ToList();
            List<ModComission> lst = new List<ModComission>();
            lst = (await _businessAccess.getComissionReport()).Where(c => ids.Contains(c.id)).ToList();
            return View(lst);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateSattlement(int id, string selectedIds, int status)
        {
            string userid = HttpContext.Session.GetString("userid");
            if (string.IsNullOrEmpty(userid))
            {
                return Redirect("../../partner-login");
            }
            await _dataAccess.SettlementUpdate(id, status, selectedIds );

            return RedirectToAction("PendingSettlement", "Admin");
        }
        public async Task<IActionResult> AdminBankInformation()
        {
            string userid = HttpContext.Session.GetString("userid");
            if (string.IsNullOrEmpty(userid))
            {
                return Redirect("../../partner-login");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AdminBankInformation(ModBankDetails obj)
        {
            string userid = HttpContext.Session.GetString("userid");
            if (string.IsNullOrEmpty(userid))
            {
                return Redirect("../../partner-login");
            }
            if (await _dataAccess.AddAdminBank(obj))
            {
                TempData["Success"] = "Bank Details Save Successfully";
            }
            else
            {
                TempData["Err"] = "Failed to Update Bank Information";
            }
            return View();
        }
        public async Task<IActionResult> bankList()
        {
            string userid = HttpContext.Session.GetString("userid");
            if (string.IsNullOrEmpty(userid))
            {
                return Redirect("../../partner-login");
            }
            List<ModBankDetails> lst = new List<ModBankDetails>();
            lst = (await _businessAccess.getAdminBankInfo());
            return View(lst);
        }
        [HttpGet("/Admin/BalanceMaster/{st?}")]
        public async Task<IActionResult> BalanceMaster(int? st)
        {
            string userid = HttpContext.Session.GetString("userid");
            if (string.IsNullOrEmpty(userid))
            {
                return Redirect("../../partner-login");
            }
            DataTable dt = await _dataAccess.AdminReport("balancemaster");

            if (dt != null && dt.Rows.Count > 0)
            {
                
                var filtered = dt.AsEnumerable();
                if (st.HasValue)
                {
                    if (st.Value == 1)
                    {
                        ViewBag.titlename = "Total payout Report";
                        //filtered = filtered.Where(x => x.Field<string>("status") == "DR");
                        filtered = filtered;
                    }
                    else if (st.Value == 2)
                    {
                        ViewBag.titlename = "Monthly Payout Report";
                        filtered = filtered.Where(x =>
                        x.Field<DateTime>("createDate").Month == DateTime.Now.Month &&
                        x.Field<DateTime>("createDate").Year == DateTime.Now.Year);
                    }
                }
                else
                {
                    ViewBag.titlename = "All payout Report";
                }
                if (filtered.Any())
                    dt = filtered.CopyToDataTable();
                else
                    dt = dt.Clone();
            }

            ViewBag.BalanceData = dt;
            return View();
        }
        public async Task<IActionResult> changePassword()
        {
            string userid = HttpContext.Session.GetString("userid");
            if (string.IsNullOrEmpty(userid))
            {
                return Redirect("../../partner-login");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> changePassword(string oldPass, string newPass, string confPass)
        {
            string msg = "";
            string userid = HttpContext.Session.GetString("userid");
            if (string.IsNullOrEmpty(userid))
            {
                return Redirect("../../partner-login");
            }
            if (!string.IsNullOrEmpty(oldPass) && !string.IsNullOrEmpty(newPass) && !string.IsNullOrEmpty(confPass))
            {
                msg = await _dataAccess.UpdateAdminPass(oldPass, newPass, confPass);
            }
            TempData["Success"] = msg;
            return View();
        }
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            HttpContext.SignOutAsync();
            return Redirect("../../partner-login");
        }
    }
}
