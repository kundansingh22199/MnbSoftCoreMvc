using BusinessEntity;
using IContract;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Threading.Tasks;

namespace MnbSoftCoreMvc.Controllers
{
    public class UserController : Controller
    {
        private readonly IDataAccess _dataAccess;
        private readonly IBusinessAccess _businessAccess;
        public UserController(IDataAccess dataAccess, IBusinessAccess businessAccess)
        {
            _dataAccess = dataAccess;
            _businessAccess = businessAccess;
        }
        public async Task<IActionResult> Dashboard()
        {
            string email = HttpContext.Session.GetString("email");
            if (string.IsNullOrEmpty(email))
            {
                return Redirect("../../partner-login");
            }
            ModPartnerDashboard obj = new ModPartnerDashboard();
            obj = await _businessAccess.getPartnerDashboard(email);
            return View(obj);
        }
        public async Task<IActionResult> PartnerDetails()
        {
            ModPartner obj = new ModPartner();
            try
            {
                string email = HttpContext.Session.GetString("email");
                string password = HttpContext.Session.GetString("password");
                if (string.IsNullOrEmpty(email))
                {
                    return Redirect("../../partner-login");
                }
                obj = (await _businessAccess.getPartnerReport()).FirstOrDefault(x => x.businessEmail == email && x.password == password);
            }
            catch (Exception)
            {

            }
            return View(obj);
        }
        public async Task<IActionResult> BankInformation()
        {
            ModBankDetails objdetails = new ModBankDetails();
            string partnerCode = HttpContext.Session.GetString("partnerCode");
            if (string.IsNullOrEmpty(partnerCode))
            {
                return Redirect("../../partner-login");
            }
            objdetails = await _businessAccess.getBankInfo(partnerCode);
            return View(objdetails);
        }
        [HttpPost]
        public async Task<IActionResult> BankInformation(ModBankDetails obj)
        {
            ModBankDetails objdetails = new ModBankDetails();
            string partnerCode = HttpContext.Session.GetString("partnerCode");
            if (string.IsNullOrEmpty(partnerCode))
            {
                return Redirect("../../partner-login");
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
            string partnerCode = HttpContext.Session.GetString("partnerCode");
            if (string.IsNullOrEmpty(partnerCode))
            {
                return Redirect("../../partner-login");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdatePassword(string oldPass, string newPass, string confPass)
        {
            string msg = "";
            string partnerCode = HttpContext.Session.GetString("partnerCode");
            if (string.IsNullOrEmpty(partnerCode))
            {
                return Redirect("../../partner-login");
            }
            if (!string.IsNullOrEmpty(oldPass) && !string.IsNullOrEmpty(newPass) && !string.IsNullOrEmpty(confPass))
            {
                msg = await _dataAccess.UpdatePass(partnerCode, oldPass, newPass, confPass);
            }
            TempData["Success"] = msg;
            return View();
        }
        public async Task<IActionResult> MyReferral()
        {
            List<ModPartner> lst = new List<ModPartner>();
            try
            {
                string email = HttpContext.Session.GetString("email");
                string partnerCode = HttpContext.Session.GetString("partnerCode");
                if (string.IsNullOrEmpty(email))
                {
                    return Redirect("../../partner-login");
                }
                lst = (await _businessAccess.getPartnerReport()).Where(x => x.sponsorCode == partnerCode).ToList();
            }
            catch (Exception)
            {

            }
            return View(lst);
        }
        public async Task<IActionResult> SaleRequest()
        {
            string email = HttpContext.Session.GetString("email");
            if (string.IsNullOrEmpty(email))
            {
                return Redirect("../../partner-login");
            }
            ModPartnerDashboard obj = new ModPartnerDashboard();
            obj = await _businessAccess.getPartnerDashboard(email);
            ViewBag.rank = obj.rankNo;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SaleRequest(ModSalePurchase obj)
        {
            string partnerCode = HttpContext.Session.GetString("partnerCode");
            string email = HttpContext.Session.GetString("email");
            if (string.IsNullOrEmpty(partnerCode))
            {
                return Redirect("../../partner-login");
            }
            ModPartnerDashboard objdash = new ModPartnerDashboard();
            objdash = await _businessAccess.getPartnerDashboard(email);
            ViewBag.rank = objdash.rankNo;
            if (string.IsNullOrEmpty(obj.company) || string.IsNullOrEmpty(obj.mobile) || string.IsNullOrEmpty(obj.email) || string.IsNullOrEmpty(obj.saleType) || obj.amount <= 0)
            {
                TempData["Err"] = "All Field Required";
                return View();
            }
            obj.partnerCode = partnerCode;
            if (obj.productName == "Taskflier")
            {
                obj.planType = obj.planTypeTaskflier;
                if (string.IsNullOrEmpty(obj.planName) || string.IsNullOrEmpty(obj.planTypeTaskflier) || obj.noOfUser <= 0)
                {
                    TempData["Err"] = "All Field Required";
                    return View();
                }
            }
            if (obj.productName == "Host Guardians")
            {
                if (string.IsNullOrEmpty(obj.planType) || string.IsNullOrEmpty(obj.serverName))
                {
                    TempData["Err"] = "All Field Required";
                    return View();
                }
                if (obj.serverName == "Linux" || obj.serverName == "Windows")
                {
                    if (string.IsNullOrEmpty(obj.config))
                    {
                        TempData["Err"] = "Configuration Required";
                        return View();
                    }
                }
                else
                {
                    if (obj.noOfUser <= 0)
                    {
                        TempData["Err"] = "Please Enter No. of User";
                        return View();
                    }
                }
            }

            if (await _dataAccess.AddSalePurchase(obj))
            {
                TempData["Success"] = "Successfull Submited";
            }
            else
            {
                TempData["Err"] = "Failed to Submit";
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetCommission(string productName, int rankNo)
        {
            // fetch from DB or from business layer
            var data = await _businessAccess.getRankComissionReport();

            var commission = data
                .FirstOrDefault(x => x.productName == productName && x.rankId == rankNo);

            if (commission != null)
            {
                return Json(new { commissionPer = commission.comissionPer });
            }
            return Json(new { commissionPer = "" });
        }
        [HttpGet("/User/Conversions/{st?}")]
        public async Task<IActionResult> Conversions(int? st)
        {
            List<ModSalePurchase> lst = new List<ModSalePurchase>();
            string partnerCode = HttpContext.Session.GetString("partnerCode");

            if (string.IsNullOrEmpty(partnerCode))
            {
                return Redirect("../../partner-login");
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
            string partnerCode = HttpContext.Session.GetString("partnerCode");
            if (string.IsNullOrEmpty(partnerCode))
            {
                return Redirect("../../partner-login");
            }
            List<ModComission> lst = new List<ModComission>();
            lst = (await _businessAccess.getComissionReport()).Where(x => x.partnerCode == partnerCode).ToList();
            return View(lst);
        }
        public async Task<IActionResult> UnSettledAmount()
        {
            string partnerCode = HttpContext.Session.GetString("partnerCode");
            string email = HttpContext.Session.GetString("email");
            if (string.IsNullOrEmpty(partnerCode))
            {
                return Redirect("../../partner-login");
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
        [HttpPost]
        public async Task<IActionResult> UnSettledAmount(ModSettlement obj)
        {
            string partnerCode = HttpContext.Session.GetString("partnerCode");
            string email = HttpContext.Session.GetString("email");
            if (string.IsNullOrEmpty(partnerCode))
            {
                return Redirect("../../partner-login");
            }
            ModPartnerDashboard obj1 = new ModPartnerDashboard();
            obj1 = await _businessAccess.getPartnerDashboard(email);
            //ViewBag.balance = obj1.balance;
            List<ModBankDetails> lstbnk = await _businessAccess.getAdminBankInfo();
            ViewBag.partnerCode = partnerCode;
            ViewBag.adminbank = lstbnk;
            if (!string.IsNullOrEmpty(obj.utrNo) && !string.IsNullOrEmpty(obj.bankId) && !string.IsNullOrEmpty(obj.selectedIds) && obj.tdsAmount > 0 && obj.totalCommission > 0 && obj.payableAmount > 0 && obj.netPayableAmount > 0)
            {
                if(!await _dataAccess.CheckExists(obj.utrNo, "checkutrno"))
                {
                    if (await _dataAccess.AddSettlement(obj))
                    {
                        TempData["Success"] = "Successfull Submited";
                    }
                    else
                    {
                        TempData["Err"] = "Failed to Submit";
                    }
                }
                else
                {
                    TempData["Err"] = "Utr No Already Exists, Please Enter Valid Utr No";
                }
            }
            else
            {
                TempData["Err"] = "All Field Required";
            }
            obj1 = await _businessAccess.getPartnerDashboard(email);
            ViewBag.balance = obj1.balance;
            List<ModComission> lst = new List<ModComission>();
            lst = (await _businessAccess.getComissionReport()).Where(x => x.partnerCode == partnerCode && (x.status == 0 || x.status == 2)).ToList();
            return View(lst);
        }
        public async Task<IActionResult> SettledAmount()
        {
            string partnerCode = HttpContext.Session.GetString("partnerCode");
            if (string.IsNullOrEmpty(partnerCode))
            {
                return Redirect("../../partner-login");
            }
            List<ModComission> lst = new List<ModComission>();
            lst = (await _businessAccess.getComissionReport()).Where(x => x.partnerCode == partnerCode && x.status == 1).ToList();
            return View(lst);
        }
        public async Task<IActionResult> PendingSettlement()
        {
            string partnerCode = HttpContext.Session.GetString("partnerCode");
            if (string.IsNullOrEmpty(partnerCode))
            {
                return Redirect("../../partner-login");
            }
            List<ModSettlement> lst = new List<ModSettlement>();
            lst = (await _businessAccess.getSettlementReport()).Where(x=>x.partnerCode==partnerCode && x.status==0).ToList();
            return View(lst);
        }
        public async Task<IActionResult> CompleteSettlement()
        {
            string partnerCode = HttpContext.Session.GetString("partnerCode");
            if (string.IsNullOrEmpty(partnerCode))
            {
                return Redirect("../../partner-login");
            }
            List<ModSettlement> lst = new List<ModSettlement>();
            lst = (await _businessAccess.getSettlementReport()).Where(x => x.partnerCode == partnerCode && x.status == 1).ToList();
            return View(lst);
        }
        public async Task<IActionResult> BalanceMaster()
        {
            string partnerCode = HttpContext.Session.GetString("partnerCode");
            if (string.IsNullOrEmpty(partnerCode))
            {
                return Redirect("../../partner-login");
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
        public async Task<IActionResult> SettlementDetails(string selectedIds)
        {
            string partnerCode = HttpContext.Session.GetString("partnerCode");
            if (string.IsNullOrEmpty(partnerCode))
            {
                return Redirect("../../partner-login");
            }
            var ids = selectedIds.Split(',').Select(id => int.Parse(id)).ToList();
            List<ModComission> lst = new List<ModComission>();
            lst = (await _businessAccess.getComissionReport()).Where(c => ids.Contains(c.id)).ToList();
            return View(lst);
        }
        public IActionResult CurrentBalance()
        {
            string email = HttpContext.Session.GetString("email");
            if (string.IsNullOrEmpty(email))
            {
                return Redirect("../../partner-login");
            }
            return View();
        }
        public async Task<IActionResult> Withdrawl()
        {
            ModBankDetails objdetails = new ModBankDetails();
            string partnerCode = HttpContext.Session.GetString("partnerCode");
            if (string.IsNullOrEmpty(partnerCode))
            {
                return Redirect("../../partner-login");
            }
            objdetails = await _businessAccess.getBankInfo(partnerCode);
            return View(objdetails);
        }
        public IActionResult PayoutHistory()
        {
            string email = HttpContext.Session.GetString("email");
            if (string.IsNullOrEmpty(email))
            {
                return Redirect("../../partner-login");
            }
            return View();
        }
        public IActionResult SocialMediaPost()
        {
            string email = HttpContext.Session.GetString("email");
            if (string.IsNullOrEmpty(email))
            {
                return Redirect("../../partner-login");
            }
            return View();
        }
        public IActionResult EmailTemplate()
        {
            string email = HttpContext.Session.GetString("email");
            if (string.IsNullOrEmpty(email))
            {
                return Redirect("../../partner-login");
            }
            return View();
        }
        public IActionResult BannerLogo()
        {
            string email = HttpContext.Session.GetString("email");
            if (string.IsNullOrEmpty(email))
            {
                return Redirect("../../partner-login");
            }
            return View();
        }
        public IActionResult KnoledgeBase()
        {
            string email = HttpContext.Session.GetString("email");
            if (string.IsNullOrEmpty(email))
            {
                return Redirect("../../partner-login");
            }
            return View();
        }
        public IActionResult VideoTutorial()
        {
            string email = HttpContext.Session.GetString("email");
            if (string.IsNullOrEmpty(email))
            {
                return Redirect("../../partner-login");
            }
            return View();
        }
        public IActionResult ContactSupport()
        {
            string email = HttpContext.Session.GetString("email");
            if (string.IsNullOrEmpty(email))
            {
                return Redirect("../../partner-login");
            }
            return View();
        }
        public IActionResult CompanyUpdates()
        {
            string email = HttpContext.Session.GetString("email");
            if (string.IsNullOrEmpty(email))
            {
                return Redirect("../../partner-login");
            }
            return View();
        }
        public IActionResult CampaignOffer()
        {
            string email = HttpContext.Session.GetString("email");
            if (string.IsNullOrEmpty(email))
            {
                return Redirect("../../partner-login");
            }
            return View();
        }
        public async Task<IActionResult> Profile()
        {
            ModPartner obj = new ModPartner();
            try
            {
                string partnerCode = HttpContext.Session.GetString("partnerCode");
                if (string.IsNullOrEmpty(partnerCode))
                {
                    return Redirect("../../partner-login");
                }

                obj = (await _businessAccess.getPartnerReport()).FirstOrDefault(x => x.partnerCode == partnerCode);
            }
            catch (Exception)
            {

            }
            return View(obj);
        }
        [HttpPost]
        public async Task<IActionResult> Profile(ModPartner obj)
        {
            ModPartner objdetails = new ModPartner();
            try
            {
                string partnerCode = HttpContext.Session.GetString("partnerCode");
                if (string.IsNullOrEmpty(partnerCode))
                {
                    return Redirect("../../partner-login");
                }
                obj.partnerCode = partnerCode;
                obj.action = "profile";
                if (await _dataAccess.UpdatePartnerProfile(obj))
                {
                    HttpContext.Session.SetString("name", obj.fullName);
                    TempData["Success"] = "Profile Update Successfully";
                }
                else
                {
                    TempData["Err"] = "Failed to Update Profile";
                }
                objdetails = (await _businessAccess.getPartnerReport()).FirstOrDefault(x => x.partnerCode == partnerCode);
            }
            catch (Exception)
            {

            }
            return View(objdetails);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProfilePhoto(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return Json(new { success = false, message = "No file selected" });

            var partnerCode = HttpContext.Session.GetString("partnerCode");
            if (string.IsNullOrEmpty(partnerCode))
                return Json(new { success = false, message = "Partner Not Exists" });

            // Folder: wwwroot/upload/profile
            var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/profile");
            if (!Directory.Exists(uploads))
                Directory.CreateDirectory(uploads);

            var fileName = $"{partnerCode}{Path.GetExtension(file.FileName)}";
            var filePath = Path.Combine(uploads, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var imagePath = "/uploads/profile/" + fileName;
            ModPartner obj = new ModPartner();
            obj.action = "profileimage";
            obj.profileImage = imagePath;
            obj.partnerCode = partnerCode;
            if (await _dataAccess.UpdatePartnerProfile(obj))
            {
                HttpContext.Session.SetString("profileImage", imagePath);
                return Json(new { success = true, newPath = imagePath });
            }
            else
            {
                return Json(new { success = false, newPath = "" });
            }
        }
        public IActionResult Earning()
        {
            string email = HttpContext.Session.GetString("email");
            if (string.IsNullOrEmpty(email))
            {
                return Redirect("../../partner-login");
            }
            return View();
        }
        public IActionResult AccountSetting()
        {
            string email = HttpContext.Session.GetString("email");
            if (string.IsNullOrEmpty(email))
            {
                return Redirect("../../partner-login");
            }
            return View();
        }
        public IActionResult Security()
        {
            string email = HttpContext.Session.GetString("email");
            if (string.IsNullOrEmpty(email))
            {
                return Redirect("../../partner-login");
            }
            return View();
        }
        public IActionResult HelpCenter()
        {
            string email = HttpContext.Session.GetString("email");
            if (string.IsNullOrEmpty(email))
            {
                return Redirect("../../partner-login");
            }
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
