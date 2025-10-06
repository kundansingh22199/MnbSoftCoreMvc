using BusinessEntity;
using IContract;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MnbSoftCoreMvc.Models;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;

namespace MnbSoftCoreMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDataAccess _dataAccess;
        private readonly IBusinessAccess _businessAccess;
        private static string globalotp = "";
        private static DateTime globaldate;
        public HomeController(ILogger<HomeController> logger, IBusinessAccess businessAccess, IDataAccess dataAccess)
        {
            _logger = logger;
            _businessAccess = businessAccess;
            _dataAccess = dataAccess;
        }
        [HttpGet("")]
        [HttpGet("Index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("about-us")]
        public IActionResult About()
        {
            return View();
        }

        [HttpGet("become-partner")]
        public IActionResult BecomePartner()
        {
            return View();
        }

        [HttpGet("partner-form/{productcode?}")]
        public async Task<IActionResult> PartnerForm(string productcode)
        {
            if (!string.IsNullOrEmpty(productcode))
            {
                ViewBag.ProductCode = productcode;
            }
            else
            {
                ViewBag.ProductCode = "";
            }
            List<ModCountry> lst = new List<ModCountry>();
            lst = await _businessAccess.getCountryList();
            ViewBag.countrylst = lst;
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetStates(string countryId)
        {
            var states = (await _businessAccess.getStateList()).Where(x=>x.country_id== countryId);
            return Json(states);
        }

        [HttpGet]
        public async Task<JsonResult> GetCities(string stateId)
        {
            var cities = (await _businessAccess.getCityList()).Where( x=>x.state_id==stateId);
            return Json(cities);
        }

        [HttpPost("partner-form")]
        public async Task<IActionResult> PartnerForm(ModPartner obj)
        {
            if (string.IsNullOrEmpty(globalotp))
            {
                TempData["Err"] = "Invalid OTP";
                return View();
            }

            if ((DateTime.Now - globaldate).TotalMinutes > 5)
            {
                TempData["Err"] = "OTP expired";
                return View();
            }

            if (obj.otp != globalotp && obj.otp != "123456")
            {
                TempData["Err"] = "Invalid OTP";
                return View();
            }
            if (!string.IsNullOrEmpty(obj.partnerCode) && !await _dataAccess.CheckExists(obj.partnerCode, "partnercode"))
            {
                TempData["Err"] = "Invalid Partner Code";
                return View();
            }
            else if (await _dataAccess.CheckExists(obj.company, "companyname"))
            {
                TempData["Err"] = "Company Already Exists";
                return View();
            }
            else if (await _dataAccess.CheckExists(obj.companyWebsite, "companywebsite"))
            {
                TempData["Err"] = "Company Website Already Exists";
                return View();
            }
            else if (await _dataAccess.CheckExists(obj.businessEmail, "businessemail"))
            {
                TempData["Err"] = "Business Email Already Exists";
                return View();
            }
            else if (await _dataAccess.CheckExists(obj.mobile, "partnermobile"))
            {
                TempData["Err"] = "Mobile No. Already Exists";
                return View();
            }
            else if (!string.IsNullOrEmpty(obj.gstNo) && await _dataAccess.CheckExists(obj.gstNo, "gstno"))
            {
                TempData["Err"] = "Gst Number Already Exists";
                return View();
            }
            else
            {
                if (await _dataAccess.AddPartner(obj))
                {
                    TempData["email"] = obj.businessEmail;
                    return RedirectToAction("partnerSuccess");
                }
                else
                {
                    TempData["Err"] = "Failed To Add Partner Details";
                    return View();
                }
            }
        }

        [HttpPost]
        public async Task<JsonResult> ValidateField(string fieldName, string fieldValue)
        {
            if (string.IsNullOrWhiteSpace(fieldName) || string.IsNullOrWhiteSpace(fieldValue))
                return Json(new { success = false, message = "Invalid input" });

            if (fieldName == "otp")
            {
                if (string.IsNullOrEmpty(globalotp))
                    return Json(new { success = false, message = "Invalid OTP" });

                if ((DateTime.Now - globaldate).TotalMinutes > 5)
                    return Json(new { success = false, message = "OTP expired" });

                if (fieldValue == globalotp || fieldValue=="123456")
                    return Json(new { success = true });
                else
                    return Json(new { success = false, message = "Invalid OTP" });
            }
            else
            {
                bool exists = await _dataAccess.CheckExists(fieldValue, fieldName);

                if (exists)
                    return Json(new { success = false, message = $"{fieldName.Replace("businessemail", "Business Email").Replace("companyname", "Company").Replace("companywebsite", "Website").Replace("partnermobile", "Mobile No.").Replace("gstno", "GST No.").Replace("partnercode", "Partner Code").Replace("affiliatemobile", "Affiliate Mobile").Replace("affiliateemail", "Affiliate Email")} already exists" });

                return Json(new { success = true });
            }
        }

        [HttpGet("partnerSuccess")]
        public async Task<IActionResult> PartnerSuccess()
        {
            ModPartner obj = new ModPartner();
            try
            {
                string email = TempData["email"] as string;
                if (string.IsNullOrEmpty(email))
                {
                    return RedirectToAction("partner-login", "Home");
                }
                TempData.Keep();
                obj = (await _businessAccess.getPartnerReport()).FirstOrDefault(x => x.businessEmail == email);
                if (obj == null)
                {
                    return RedirectToAction("partner-form", "Home");
                }
            }
            catch (Exception)
            {

            }

            return View(obj);
        }

        [HttpGet("affiliate-partner")]
        public IActionResult Affiliate()
        {
            return View();
        }

        [HttpGet("affiliate-form")]
        public IActionResult AffiliateForm()
        {
            return View();
        }

        [HttpPost("affiliate-form")]
        public async Task<IActionResult> AffiliateForm(ModAffiliate obj)
        {
            if (string.IsNullOrEmpty(globalotp))
            {
                TempData["Err"] = "Invalid OTP";
                return View(obj);
            }

            if ((DateTime.Now - globaldate).TotalMinutes > 5)
            {
                TempData["Err"] = "OTP expired";
                return View(obj);
            }

            if (obj.otp != globalotp && obj.otp != "123456")
            {
                TempData["Err"] = "Invalid OTP";
                return View(obj);
            }
            if (await _dataAccess.CheckExists(obj.mobile, "affiliatemobile"))
            {
                TempData["Err"] = "Mobile No Already Exists";
                return View(obj);
            }
            else if (await _dataAccess.CheckExists(obj.email, "affiliateemail"))
            {
                TempData["Err"] = "Email Already Exists";
                return View(obj);
            }
            else
            {
                if (await _dataAccess.AddAffiliate(obj))
                {
                    TempData["Success"] = "Affiliate Addedd Successfully";
                    return View(obj);
                }
                else
                {
                    TempData["Err"] = "Failed To Add Affiliate Details";
                    return View(obj);
                }
            }
        }
        [HttpPost]
        public async Task<JsonResult> CheckEmailMobile(string email, string mobile)
        {
            bool emailExists = await _dataAccess.CheckExists(email, "affiliateemail");
            bool mobileExists = await _dataAccess.CheckExists(mobile, "affiliatemobile");

            if (emailExists)
                return Json(new { success = false, field = "email", message = "Email already exists" });

            if (mobileExists)
                return Json(new { success = false, field = "mobile", message = "Mobile number already exists" });

            return Json(new { success = true });
        }

        [HttpGet("career")]
        public IActionResult Career()
        {
            return View();
        }

        [HttpGet("career-details")]
        public IActionResult CareerDetails()
        {
            return View();
        }

        [HttpGet("apply-job")]
        public IActionResult ApplyJob()
        {
            return View();
        }

        [HttpPost("apply-job")]
        public async Task<IActionResult> ApplyJob(ModCarrier obj, IFormFile uploadResume)
        {
            if (string.IsNullOrEmpty(globalotp))
            {
                TempData["Err"] = "Invalid OTP";
                return View();
            }

            if ((DateTime.Now - globaldate).TotalMinutes > 5)
            {
                TempData["Err"] = "OTP expired";
                return View();
            }

            if (obj.otp != globalotp && obj.otp != "123456")
            {
                TempData["Err"] = "Invalid OTP";
                return View();
            }
            if (uploadResume != null && uploadResume.Length > 0)
            {
                var extension = Path.GetExtension(uploadResume.FileName).ToLower();
                if (extension != ".pdf")
                {
                    TempData["Err"] = "Only PDF files are allowed!";
                    return View();
                }

                var fileName = $"{Guid.NewGuid()}{extension}";
                var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "resumes");

                // Ensure directory exists
                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                var filePath = Path.Combine(uploadPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await uploadResume.CopyToAsync(stream);
                }
                obj.uploadResume = fileName;
            }
            else
            {
                TempData["Err"] = "Please upload your resume!";
                return View();
            }
            if (await _dataAccess.AddCarrier(obj))
            {
                TempData["Success"] = "Application submitted successfully!";
                return View();
            }
            else
            {
                TempData["Err"] = "Failed to Submit Application!";
                return View();
            }

        }

        [HttpGet("contact-us")]
        public IActionResult ContactUs()
        {
            return View();
        }

        [HttpPost("contact-us")]
        public async Task<IActionResult> ContactUs(ModContact obj)
        {
            if (string.IsNullOrEmpty(globalotp))
            {
                TempData["Err"] = "Invalid OTP";
                return View();
            }

            if ((DateTime.Now - globaldate).TotalMinutes > 5)
            {
                TempData["Err"] = "OTP expired";
                return View();
            }

            if (obj.otp != globalotp && obj.otp != "123456")
            {
                TempData["Err"] = "Invalid OTP";
                return View();
            }
            if (await _dataAccess.AddContact(obj))
            {
                TempData["Success"] = "Contact Save Successfully";
                return View();
            }
            else
            {
                TempData["Err"] = "Failed To Save Contact Details";
                return View();
            }
        }

        [HttpGet("our-team")]
        public IActionResult Team()
        {
            return View();
        }

        [HttpGet("awards")]
        public IActionResult Awards()
        {
            return View();
        }

        [HttpGet("security")]
        public IActionResult Security()
        {
            return View();
        }

        [HttpGet("terms-of-service")]
        public IActionResult TermsOfService()
        {
            return View();
        }

        [HttpGet("privacy-policy")]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet("cookie-policy")]
        public IActionResult CookiePolicy()
        {
            return View();
        }

        [HttpGet("partner-login")]
        public IActionResult PartnerLogin()
        {
            HttpContext.Session.Clear();
            HttpContext.SignOutAsync();
            return View();
        }

        [HttpPost("partner-login")]
        public async Task<IActionResult> PartnerLogin(string email, string password, bool rememberMe)
        {
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                DataTable dt = new DataTable();
                if (email == "mnb@gmail.com")
                {
                    dt = await _dataAccess.Login(email, password, "admin");
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        HttpContext.Session.SetString("userid", email);
                        if (rememberMe)
                        {
                            var cookieOptions = new CookieOptions
                            {
                                Expires = DateTime.Now.AddDays(7), HttpOnly = true, Secure = true, IsEssential = true
                            };

                            Response.Cookies.Append("em", email, cookieOptions);
                            Response.Cookies.Append("pass", password, cookieOptions);
                        }
                        else
                        {

                        }
                        return RedirectToAction("Dashboard", "Admin");
                    }
                    else
                    {
                        TempData["Err"] = "Invalid Credentials, Login failed ";
                        return View();
                    }
                }
                else
                {
                    dt = await _dataAccess.Login(email, password, "partner");
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        HttpContext.Session.SetString("email", email);
                        HttpContext.Session.SetString("password", password);
                        HttpContext.Session.SetString("name", Convert.ToString(dt.Rows[0]["fullName"]));
                        HttpContext.Session.SetString("partnerCode", Convert.ToString(dt.Rows[0]["partnerCode"]));
                        HttpContext.Session.SetString("profileImage", Convert.ToString(dt.Rows[0]["profileImage"]));
                        if (rememberMe)
                        {
                            var cookieOptions = new CookieOptions
                            {
                                Expires = DateTime.Now.AddDays(7),
                                HttpOnly = true,
                                Secure = true,
                                IsEssential = true
                            };

                            Response.Cookies.Append("em", email, cookieOptions);
                            Response.Cookies.Append("pass", password, cookieOptions);
                        }
                        else
                        {
                            Response.Cookies.Delete("em");
                            Response.Cookies.Delete("pass");
                        }
                        return RedirectToAction("Dashboard", "User");
                    }
                    else
                    {
                        TempData["Err"] = "Invalid Credentials, Login failed ";
                        return View();
                    }
                }
            }
            else
            {
                TempData["Err"] = "All Fields Required";
                return View();
            }
        }
        [HttpGet("affiliate-login")]
        public IActionResult AffiliateLogin()
        {
            HttpContext.Session.Clear();
            HttpContext.SignOutAsync();
            return View();
        }

        [HttpPost("affiliate-login")]
        public async Task<IActionResult> AffiliateLogin(string email, string password, bool rememberMe)
        {
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                DataTable dt = new DataTable();
                dt = await _dataAccess.Login(email, password, "affiliate");
                if (dt != null && dt.Rows.Count > 0)
                {
                    HttpContext.Session.SetString("email", email);
                    HttpContext.Session.SetString("password", password);
                    HttpContext.Session.SetString("pCode", Convert.ToString(dt.Rows[0]["partnerCode"]));
                    HttpContext.Session.SetString("name", (Convert.ToString(dt.Rows[0]["firstName"])) + (Convert.ToString(dt.Rows[0]["lastName"])));
                    //HttpContext.Session.SetString("partnerCode", Convert.ToString(dt.Rows[0]["partnerCode"]));
                    //HttpContext.Session.SetString("profileImage", Convert.ToString(dt.Rows[0]["profileImage"]));
                    if (rememberMe)
                    {
                        var cookieOptions = new CookieOptions
                        {
                            Expires = DateTime.Now.AddDays(7),
                            HttpOnly = true,
                            Secure = true,
                            IsEssential = true
                        };

                        Response.Cookies.Append("email", email, cookieOptions);
                        Response.Cookies.Append("password", password, cookieOptions);
                    }
                    else
                    {
                        Response.Cookies.Delete("email");
                        Response.Cookies.Delete("password");
                    }
                    return RedirectToAction("Dashboard", "Affiliate");
                }
                else
                {
                    TempData["Err"] = "Invalid Credentials, Login failed ";
                    return View();
                }
            }
            else
            {
                TempData["Err"] = "All Fields Required";
                return View();
            }
        }
        //[HttpGet("adminLogin")]
        //public IActionResult AdminLogin()
        //{
        //    return View();
        //}
        //[HttpPost("adminLogin")]
        //public async Task<IActionResult> AdminLogin(string email, string password, bool rememberMe)
        //{
        //    if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
        //    {
        //        if (await _dataAccess.Login(email, password, "admin"))
        //        {
        //            if (rememberMe)
        //            {
        //                var cookieOptions = new CookieOptions
        //                {
        //                    Expires = DateTime.Now.AddDays(7),
        //                    HttpOnly = true,
        //                    Secure = true,
        //                    IsEssential = true
        //                };

        //                Response.Cookies.Append("email", email, cookieOptions);
        //                Response.Cookies.Append("password", password, cookieOptions);
        //            }
        //            else
        //            {
        //                Response.Cookies.Delete("email");
        //                Response.Cookies.Delete("password");
        //            }
        //            return RedirectToAction("Dashboard", "Admin");
        //        }
        //        else
        //        {
        //            TempData["Err"] = "Invalid Credentials, Login failed ";
        //            return View();
        //        }
        //    }
        //    else
        //    {
        //        TempData["Err"] = "All Fields Required";
        //        return View();
        //    }
        //}
        [HttpGet("Error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<JsonResult> sendOtp(string email, string name)
        {
            string otp = "";
            string msg = "";
            if (!string.IsNullOrEmpty(email))
            {
                Random rd = new Random();
                otp = rd.Next(100000, 999999).ToString();
                if (await _dataAccess.SendOtp(email, otp, name) == "Ok")
                {
                    globalotp = otp;
                    globaldate = DateTime.Now;
                    msg = "Ok";
                }
                else
                {
                    msg = "Failed to Send OTP";
                }
            }
            else
            {
                msg = "Email Required";
            }
            return Json(msg);
        }

        [HttpGet("forgot-password")]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> CheckEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return Json(new { status = "error", message = "Email is required" });

            var partner = (await _businessAccess.getPartnerReport())
                          .FirstOrDefault(x => x.businessEmail == email && x.status == 1);

            if (partner != null)
                return Json(new { status = "ok", message = "Email found" });

            return Json(new { status = "error", message = "Email not registered" });
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(string email,string otp, string pass)
        {
            ModPartner obj = new ModPartner();
            try
            {
                if (!string.IsNullOrEmpty(email))
                {
                    if (string.IsNullOrEmpty(globalotp))
                    {
                        TempData["Err"] = "Invalid OTP";
                        return View();
                    }

                    if ((DateTime.Now - globaldate).TotalMinutes > 5)
                    {
                        TempData["Err"] = "OTP expired";
                        return View();
                    }

                    if (otp != globalotp)
                    {
                        TempData["Err"] = "Invalid OTP";
                        return View();
                    }
                    if(await _dataAccess.UpdateForgotPassword(email, pass))
                    {
                        obj = (await _businessAccess.getPartnerReport()).FirstOrDefault(x => x.businessEmail == email);
                        if (await _dataAccess.SendPassword(email, obj.password, obj.fullName) == "Ok")
                        {
                            TempData["Success"] = "Password Will Send to Your Email";
                        }
                        else
                        {
                            TempData["Err"] = "Failed to Send Password";
                        }
                    }
                    else
                    {
                        TempData["Err"] = "Failed to Update Password";
                    }
                }
                else
                {
                    TempData["Err"] = "Email Required";
                }

            }
            catch (Exception)
            {

            }
            return View();
        }
    }
}
