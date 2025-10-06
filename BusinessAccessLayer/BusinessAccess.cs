using BusinessEntity;
using IContract;
using System.Data;

namespace BusinessAccessLayer
{
    public class BusinessAccess : IBusinessAccess
    {
        private readonly IDataAccess _dataAccess;
        public BusinessAccess(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        #region Admin Pannel
        public async Task<List<ModPartner>> getPartnerReport()
        {
            List<ModPartner> lst = new List<ModPartner>();
            DataTable dt = new DataTable();
            dt = await _dataAccess.AdminReport("partnerdetails");
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    lst.Add(new ModPartner
                    {
                        id = Convert.ToInt32(dr["id"]),
                        fullName = Convert.ToString(dr["fullName"]),
                        mobile = Convert.ToString(dr["mobile"]),
                        partnerCode = Convert.ToString(dr["partnerCode"]),
                        sponsorCode = Convert.ToString(dr["sponsorCode"]),
                        company = Convert.ToString(dr["company"]),
                        companyWebsite = Convert.ToString(dr["companyWebsite"]),
                        country = Convert.ToString(dr["country"]),
                        state = Convert.ToString(dr["state"]),
                        city = Convert.ToString(dr["city"]),
                        address = Convert.ToString(dr["address"]),
                        businessEmail = Convert.ToString(dr["businessEmail"]),
                        whyBecomeReseller = Convert.ToString(dr["whyBecomeReseller"]),
                        teamSize = Convert.ToString(dr["teamSize"]),
                        branchOfficeLocation = Convert.ToString(dr["branchOfficeLocation"]),
                        status = Convert.ToInt32(dr["status"]),
                        createDate = Convert.ToDateTime(dr["createDate"]),
                        password = Convert.ToString(dr["password"]),
                        gstNo = Convert.ToString(dr["gstNo"]),
                        profileImage = Convert.ToString(dr["profileImage"])
                    });
                }
            }
            return lst;
        }
        public async Task<List<ModAffiliate>> getAffiliateReport()
        {
            List<ModAffiliate> lst = new List<ModAffiliate>();
            DataTable dt = new DataTable();
            dt = await _dataAccess.AdminReport("affiliatedetails");
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    lst.Add(new ModAffiliate
                    {
                        id = Convert.ToInt32(dr["id"]),
                        partnerCode = Convert.ToString(dr["partnerCode"]),
                        firstName = Convert.ToString(dr["firstName"]),
                        lastName = Convert.ToString(dr["lastName"]),
                        mobile = Convert.ToString(dr["mobile"]),
                        email = Convert.ToString(dr["email"]),
                        subject = Convert.ToString(dr["subject"]),
                        country = Convert.ToString(dr["country"]),
                        password = Convert.ToString(dr["password"]),
                        status = Convert.ToInt32(dr["status"]),
                        createDate = Convert.ToDateTime(dr["createDate"])
                    });
                }
            }
            return lst;
        }
        public async Task<List<ModCarrier>> getcarrierReport()
        {
            List<ModCarrier> lst = new List<ModCarrier>();
            DataTable dt = new DataTable();
            dt = await _dataAccess.AdminReport("carrierdetails");
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    lst.Add(new ModCarrier
                    {
                        id = Convert.ToInt32(dr["id"]),
                        firstName = Convert.ToString(dr["firstName"]),
                        lastName = Convert.ToString(dr["lastName"]),
                        mobile = Convert.ToString(dr["mobile"]),
                        email = Convert.ToString(dr["email"]),
                        yearOfGraduation = Convert.ToString(dr["yearOfGraduation"]),
                        gender = Convert.ToString(dr["gender"]),
                        yearOfExperience = Convert.ToString(dr["yearOfExperience"]),
                        currentEmployer = Convert.ToString(dr["currentEmployer"]),
                        currentCtc = Convert.ToString(dr["currentCtc"]),
                        expectedCtc = Convert.ToString(dr["expectedCtc"]),
                        noticePeriod = Convert.ToString(dr["noticePeriod"]),
                        skillSet = Convert.ToString(dr["skillSet"]),
                        uploadResume = Convert.ToString(dr["uploadResume"]),
                        status = Convert.ToInt32(dr["status"]),
                        createDate = Convert.ToDateTime(dr["createDate"])
                    });
                }
            }
            return lst;
        }
        public async Task<List<ModContact>> getContactReport()
        {
            List<ModContact> lst = new List<ModContact>();
            DataTable dt = new DataTable();
            dt = await _dataAccess.AdminReport("contactdetails");
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    lst.Add(new ModContact
                    {
                        id = Convert.ToInt32(dr["id"]),
                        name = Convert.ToString(dr["name"]),
                        email = Convert.ToString(dr["email"]),
                        subject = Convert.ToString(dr["subject"]),
                        message = Convert.ToString(dr["message"]),
                        status = Convert.ToInt32(dr["status"]),
                        createDate = Convert.ToDateTime(dr["createDate"])
                    });
                }
            }
            return lst;
        }
        public async Task<ModAdminDashboard> getAdminDashboard()
        {
            ModAdminDashboard obj = new ModAdminDashboard();
            DataTable dt = new DataTable();
            dt = await _dataAccess.AdminDashboard();
            if (dt != null && dt.Rows.Count > 0)
            {
                obj.totalPartner = Convert.ToInt32(dt.Rows[0]["totalPartner"]);
                obj.pendingPartner = Convert.ToInt32(dt.Rows[0]["pendingPartner"]);
                obj.activePartner = Convert.ToInt32(dt.Rows[0]["activePartner"]);
                obj.rejectPartner = Convert.ToInt32(dt.Rows[0]["rejectPartner"]);
                obj.blockPartner = Convert.ToInt32(dt.Rows[0]["blockPartner"]);
                obj.totalAffiliate = Convert.ToInt32(dt.Rows[0]["totalAffiliate"]);
                obj.pendingAffiliate = Convert.ToInt32(dt.Rows[0]["pendingAffiliate"]);
                obj.activeAffiliate = Convert.ToInt32(dt.Rows[0]["activeAffiliate"]);
                obj.rejectAffiliate = Convert.ToInt32(dt.Rows[0]["rejectAffiliate"]);
                obj.blockAffiliate = Convert.ToInt32(dt.Rows[0]["blockAffiliate"]);
                obj.activeJob = Convert.ToInt32(dt.Rows[0]["activeJob"]);
                obj.totalApply = Convert.ToInt32(dt.Rows[0]["totalApply"]);
                obj.pendingApply = Convert.ToInt32(dt.Rows[0]["pendingApply"]);
                obj.activeApply = Convert.ToInt32(dt.Rows[0]["activeApply"]);
                obj.rejectApply = Convert.ToInt32(dt.Rows[0]["rejectApply"]);
                obj.totalEnquiry = Convert.ToInt32(dt.Rows[0]["totalEnquiry"]);
                obj.totalComission = Convert.ToDecimal(dt.Rows[0]["totalComission"]);
                obj.totalPayout = Convert.ToDecimal(dt.Rows[0]["totalPayout"]);
                obj.totalSale = Convert.ToDecimal(dt.Rows[0]["totalSale"]);
                obj.monthlyComission = Convert.ToDecimal(dt.Rows[0]["monthlyComission"]);
                obj.monthlyPayout = Convert.ToDecimal(dt.Rows[0]["monthlyPayout"]);
                obj.monthlySale = Convert.ToDecimal(dt.Rows[0]["monthlySale"]);
                obj.salePurchaseReport = (await getAdminSalePurchaseReport()).Where(x => x.status == 1).Take(5).ToList();
                obj.totalReport = await getAdminDashboardSaleReport();
            }
            return obj;
        }
        public async Task<List<ModSalePurchase>> getAdminSalePurchaseReport()
        {
            List<ModSalePurchase> lst = new List<ModSalePurchase>();
            DataTable dt = new DataTable();
            dt = await _dataAccess.AdminReport("salepurchase");
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string productName = Convert.ToString(dr["productName"]);
                    string productImage = productName switch
                    {
                        "Taskflier" => "/assets/images/icon/icontaskflier.png",
                        "Host Guardians" => "/assets/images/icon/iconhost.png",
                        "Mparchi" => "/assets/images/icon/mparchiicon.png",
                        "MNB Promo" => "/assets/images/icon/iconpromo.png",
                        _ => "/assets/images/icon/default.png"
                    };
                    lst.Add(new ModSalePurchase
                    {
                        id = Convert.ToInt32(dr["id"]),
                        partnerCode = Convert.ToString(dr["partnerCode"]),
                        productName = Convert.ToString(dr["productName"]),
                        planName = Convert.ToString(dr["planName"]),
                        planType = Convert.ToString(dr["planType"]),
                        serverName = Convert.ToString(dr["serverName"]),
                        config = Convert.ToString(dr["config"]),
                        noOfUser = Convert.ToInt32(dr["noOfUser"]),
                        company = Convert.ToString(dr["company"]),
                        mobile = Convert.ToString(dr["mobile"]),
                        email = Convert.ToString(dr["email"]),
                        amount = Convert.ToDecimal(dr["amount"]),
                        comission = Convert.ToDecimal(dr["comission"]),
                        saleType = Convert.ToString(dr["saleType"]),
                        remarks = Convert.ToString(dr["remarks"]),
                        adminRemarks = Convert.ToString(dr["adminRemarks"]),
                        status = Convert.ToInt32(dr["status"]),
                        createDate = Convert.ToDateTime(dr["createDate"]),
                        updateDate = Convert.ToDateTime(dr["updateDate"]),
                        productImage = productImage
                    });
                }
            }
            return lst;
        }
        public async Task<List<ModComission>> getComissionReport()
        {
            List<ModComission> lst = new List<ModComission>();
            DataTable dt = new DataTable();
            dt = await _dataAccess.AdminReport("comission");
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    lst.Add(new ModComission
                    {
                        id = Convert.ToInt32(dr["id"]),
                        fullName = Convert.ToString(dr["fullName"]),
                        businessEmail = Convert.ToString(dr["businessEmail"]),
                        partnerCode = Convert.ToString(dr["partnerCode"]),
                        totalAmount = Convert.ToDecimal(dr["totalAmount"]),
                        comissionPer = Convert.ToDecimal(dr["comissionPer"]),
                        comission = Convert.ToDecimal(dr["comission"]),
                        status = Convert.ToInt32(dr["status"]),
                        createDate = Convert.ToDateTime(dr["createDate"]),
                        updateDate = Convert.ToDateTime(dr["updateDate"])
                    });
                }
            }
            return lst;
        }
        public async Task<List<ModRank>> getrankNameReport()
        {
            List<ModRank> lst = new List<ModRank>();
            DataTable dt = new DataTable();
            dt = await _dataAccess.AdminReport("rank");
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    lst.Add(new ModRank
                    {
                        id = Convert.ToInt32(dr["id"]),
                        rankName = Convert.ToString(dr["rankName"]),
                        status = Convert.ToInt32(dr["status"]),
                        createDate = Convert.ToDateTime(dr["createDate"])
                    });
                }
            }
            return lst;
        }
        public async Task<List<ModComissionMaster>> getRankComissionReport()
        {
            List<ModComissionMaster> lst = new List<ModComissionMaster>();
            DataTable dt = new DataTable();
            dt = await _dataAccess.AdminReport("setcomission");
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    lst.Add(new ModComissionMaster
                    {
                        id = Convert.ToInt32(dr["id"]),
                        rankId = Convert.ToInt32(dr["rankId"]),
                        productName = Convert.ToString(dr["productName"]),
                        rankName = Convert.ToString(dr["rankName"]),
                        status = Convert.ToInt32(dr["status"]),
                        comissionPer = Convert.ToDecimal(dr["comissionPer"]),
                        createDate = Convert.ToDateTime(dr["createDate"])
                    });
                }
            }
            return lst;
        }
        public async Task<List<ModBankDetails>> getAdminBankInfo()
        {
            List<ModBankDetails> lst = new List<ModBankDetails>();
            DataTable dt = new DataTable();
            dt = await _dataAccess.AdminReport("adminBankInfo");
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach(DataRow dr in dt.Rows)
                {
                    lst.Add(new ModBankDetails
                    {
                        id = Convert.ToInt32(dr["id"]),
                        bankName = Convert.ToString(dr["bankName"]),
                        branch = Convert.ToString(dr["branch"]),
                        accountHolder = Convert.ToString(dr["accountHolder"]),
                        accountNo = Convert.ToString(dr["accountNo"]),
                        accountType = Convert.ToString(dr["accountType"]),
                        ifsc = Convert.ToString(dr["ifsc"]),
                        status = Convert.ToInt32(dr["status"]),
                        createDate = Convert.ToDateTime(dr["createDate"]),
                        updateDate = Convert.ToDateTime(dr["updateDate"])
                    });
                }
            }
            return lst;
        }
        public async Task<List<ModSettlement>> getSettlementReport()
        {
            List<ModSettlement> lst = new List<ModSettlement>();
            DataTable dt = new DataTable();
            dt = await _dataAccess.AdminReport("settlement");
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    lst.Add(new ModSettlement
                    {
                        id = Convert.ToInt32(dr["id"]),
                        partnerCode = Convert.ToString(dr["partnerCode"]),
                        selectedIds = Convert.ToString(dr["selectedIds"]),
                        totalAmount = Convert.ToDecimal(dr["totalAmount"]),
                        totalCommission = Convert.ToDecimal(dr["totalCommission"]),
                        payableAmount = Convert.ToDecimal(dr["payableAmount"]),
                        netPayableAmount = Convert.ToDecimal(dr["netPayableAmount"]),
                        gstPer = Convert.ToDecimal(dr["gstPer"]),
                        tdsPer = Convert.ToDecimal(dr["tdsPer"]),
                        gstAmount = Convert.ToDecimal(dr["gstAmount"]),
                        tdsAmount = Convert.ToDecimal(dr["tdsAmount"]),
                        utrNo = Convert.ToString(dr["utrNo"]),
                        status = Convert.ToInt32(dr["status"]),
                        walletUsed = Convert.ToDecimal(dr["walletUsed"]),
                        bankId = Convert.ToString(dr["bankId"]),
                        bankName = Convert.ToString(dr["bankName"]),
                        accountNo = Convert.ToString(dr["accountNo"]),
                        ifsc = Convert.ToString(dr["ifsc"]),
                        createdDate = Convert.ToDateTime(dr["createdDate"])
                    });
                }
            }
            return lst;
        }
        public async Task<List<ModCountry>> getCountryList()
        {
            List<ModCountry> lst = new List<ModCountry>();
            DataTable dt = new DataTable();
            dt = await _dataAccess.AdminReport("country");
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    lst.Add(new ModCountry
                    {
                        id = Convert.ToString(dr["id"]),
                        shortname = Convert.ToString(dr["shortname"]),
                        name = Convert.ToString(dr["name"]),
                        phonecode = Convert.ToString(dr["phonecode"])
                    });
                }
            }
            return lst;
        }
        public async Task<List<ModState>> getStateList()
        {
            List<ModState> lst = new List<ModState>();
            DataTable dt = new DataTable();
            dt = await _dataAccess.AdminReport("state");
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    lst.Add(new ModState
                    {
                        id = Convert.ToString(dr["id"]),
                        name = Convert.ToString(dr["name"]),
                        country_id = Convert.ToString(dr["country_id"])
                    });
                }
            }
            return lst;
        }
        public async Task<List<ModCity>> getCityList()
        {
            List<ModCity> lst = new List<ModCity>();
            DataTable dt = new DataTable();
            dt = await _dataAccess.AdminReport("city");
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    lst.Add(new ModCity
                    {
                        id = Convert.ToString(dr["id"]),
                        name = Convert.ToString(dr["name"]),
                        state_id = Convert.ToString(dr["state_id"])
                    });
                }
            }
            return lst;
        }
        public async Task<List<ModSaleReport>> getAdminDashboardSaleReport()
        {
            List<ModSaleReport> lst = new List<ModSaleReport>();
            DataTable dt = new DataTable();
            dt = await _dataAccess.AdminReport("salepurchasedashboard");
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string productName = Convert.ToString(dr["productName"]);
                    string productImage = productName switch
                    {
                        "Taskflier" => "/assets/images/icon/icontaskflier.png",
                        "Host Guardians" => "/assets/images/icon/iconhost.png",
                        "Mparchi" => "/assets/images/icon/mparchiicon.png",
                        "MNB Promo" => "/assets/images/icon/iconpromo.png",
                        _ => "/assets/images/icon/default.png"
                    };
                    lst.Add(new ModSaleReport
                    {
                        productName = Convert.ToString(dr["productName"]),
                        thisMonthAmount = Convert.ToDecimal(dr["thisMonthAmount"]),
                        totalSaleAmount = Convert.ToDecimal(dr["totalSaleAmount"]),
                        thisMonthCount = Convert.ToInt32(dr["thisMonthCount"]),
                        totalSale = Convert.ToInt32(dr["totalSale"]),
                        productImage = productImage
                    });
                }
            }
            return lst;
        }
        #endregion

        #region Partner Pannel
        public async Task<ModPartnerDashboard> getPartnerDashboard(string email)
        {
            ModPartnerDashboard obj = new ModPartnerDashboard();
            DataTable dt = new DataTable();
            dt = await _dataAccess.PartnerDashboard(email);
            if (dt != null && dt.Rows.Count > 0)
            {
                obj.partnerCode = Convert.ToString(dt.Rows[0]["partnerCode"]);
                obj.sponsorCode = Convert.ToString(dt.Rows[0]["sponsorCode"]);
                obj.company = Convert.ToString(dt.Rows[0]["company"]);
                obj.companyWebSite = Convert.ToString(dt.Rows[0]["companyWebSite"]);
                obj.fullName = Convert.ToString(dt.Rows[0]["fullName"]);
                obj.totalSale = Convert.ToInt32(dt.Rows[0]["totalSale"]);
                obj.monthlySale = Convert.ToInt32(dt.Rows[0]["monthlySale"]);
                obj.totalSaleAmount = Convert.ToDecimal(dt.Rows[0]["totalSaleAmount"]);
                obj.monthlySaleAmount = Convert.ToDecimal(dt.Rows[0]["monthlySaleAmount"]);
                obj.referralLink = _dataAccess.GetDomain() + "/partnerForm/" + obj.partnerCode;
                obj.rankNo = Convert.ToString(dt.Rows[0]["rankNo"]);
                obj.settledAmount = Convert.ToDecimal(dt.Rows[0]["settledAmount"]);
                obj.unSettledAmount = Convert.ToDecimal(dt.Rows[0]["unSettledAmount"]);
                obj.comission = Convert.ToDecimal(dt.Rows[0]["comission"]);
                obj.balance = Convert.ToDecimal(dt.Rows[0]["balance"]);
                obj.pendingSettlement = Convert.ToDecimal(dt.Rows[0]["pendingSettlement"]);
                obj.completeSettlement = Convert.ToDecimal(dt.Rows[0]["completeSettlement"]);
                obj.salePurchaseReport = (await getSalePurchaseReport(obj.partnerCode)).Where(x => x.status == 1).Take(5).ToList();
                obj.totalReport = await getDashboardSaleReport(obj.partnerCode);
            }
            return obj;
        }
        public async Task<ModBankDetails> getBankInfo(string partnerCode)
        {
            ModBankDetails obj = new ModBankDetails();
            DataTable dt = new DataTable();
            dt = await _dataAccess.PartnerReport("bankinfo", partnerCode);
            if (dt != null && dt.Rows.Count > 0)
            {
                obj.id = Convert.ToInt32(dt.Rows[0]["id"]);
                obj.partnerCode = Convert.ToString(dt.Rows[0]["partnerCode"]);
                obj.bankName = Convert.ToString(dt.Rows[0]["bankName"]);
                obj.branch = Convert.ToString(dt.Rows[0]["branch"]);
                obj.accountHolder = Convert.ToString(dt.Rows[0]["accountHolder"]);
                obj.accountNo = Convert.ToString(dt.Rows[0]["accountNo"]);
                obj.accountType = Convert.ToString(dt.Rows[0]["accountType"]);
                obj.ifsc = Convert.ToString(dt.Rows[0]["ifsc"]);
                obj.status = Convert.ToInt32(dt.Rows[0]["status"]);
                obj.createDate = Convert.ToDateTime(dt.Rows[0]["createDate"]);
                obj.updateDate = Convert.ToDateTime(dt.Rows[0]["updateDate"]);
                obj.fullName = Convert.ToString(dt.Rows[0]["fullName"]);
                obj.businessEmail = Convert.ToString(dt.Rows[0]["businessEmail"]);
                obj.mobile = Convert.ToString(dt.Rows[0]["mobile"]);
            }
            return obj;
        }
        public async Task<List<ModSaleReport>> getDashboardSaleReport(string partnerCode)
        {
            List<ModSaleReport> lst = new List<ModSaleReport>();
            DataTable dt = new DataTable();
            dt = await _dataAccess.PartnerReport("salepurchasedashboard", partnerCode);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string productName = Convert.ToString(dr["productName"]);
                    string productImage = productName switch
                    {
                        "Taskflier" => "/assets/images/icon/icontaskflier.png",
                        "Host Guardians" => "/assets/images/icon/iconhost.png",
                        "Mparchi" => "/assets/images/icon/mparchiicon.png",
                        "MNB Promo" => "/assets/images/icon/iconpromo.png",
                        _ => "/assets/images/icon/default.png"
                    };
                    lst.Add(new ModSaleReport
                    {
                        productName = Convert.ToString(dr["productName"]),
                        thisMonthAmount = Convert.ToDecimal(dr["thisMonthAmount"]),
                        totalSaleAmount = Convert.ToDecimal(dr["totalSaleAmount"]),
                        thisMonthCount = Convert.ToInt32(dr["thisMonthCount"]),
                        totalSale = Convert.ToInt32(dr["totalSale"]),
                        productImage= productImage
                    });
                }
            }
            return lst;
        }
        public async Task<List<ModSalePurchase>> getSalePurchaseReport(string partnerCode)
        {
            List<ModSalePurchase> lst = new List<ModSalePurchase>();
            DataTable dt = new DataTable();
            dt = await _dataAccess.PartnerReport("salepurchase", partnerCode);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string productName = Convert.ToString(dr["productName"]);
                    string productImage = productName switch
                    {
                        "Taskflier" => "/assets/images/icon/icontaskflier.png",
                        "Host Guardians" => "/assets/images/icon/iconhost.png",
                        "Mparchi" => "/assets/images/icon/mparchiicon.png",
                        "MNB Promo" => "/assets/images/icon/iconpromo.png",
                        _ => "/assets/images/icon/default.png" 
                    };
                    lst.Add(new ModSalePurchase
                    {
                        id = Convert.ToInt32(dr["id"]),
                        partnerCode = Convert.ToString(dr["partnerCode"]),
                        productName = Convert.ToString(dr["productName"]),
                        planName = Convert.ToString(dr["planName"]),
                        planType = Convert.ToString(dr["planType"]),
                        serverName = Convert.ToString(dr["serverName"]),
                        config = Convert.ToString(dr["config"]),
                        noOfUser = Convert.ToInt32(dr["noOfUser"]),
                        company = Convert.ToString(dr["company"]),
                        mobile = Convert.ToString(dr["mobile"]),
                        email = Convert.ToString(dr["email"]),
                        amount = Convert.ToDecimal(dr["amount"]),
                        comission = Convert.ToDecimal(dr["comission"]),
                        saleType = Convert.ToString(dr["saleType"]),
                        remarks = Convert.ToString(dr["remarks"]),
                        adminRemarks = Convert.ToString(dr["adminRemarks"]),
                        status = Convert.ToInt32(dr["status"]),
                        createDate = Convert.ToDateTime(dr["createDate"]),
                        updateDate = Convert.ToDateTime(dr["updateDate"]),
                        productImage= productImage
                    });
                }
            }
            return lst;
        }
        #endregion

        #region Affiliate Pannel
        public async Task<ModPartnerDashboard> getAffiliateDashboard(string partnercode)
        {
            ModPartnerDashboard obj = new ModPartnerDashboard();
            DataTable dt = new DataTable();
            dt = await _dataAccess.AffiliateDashboard(partnercode);
            if (dt != null && dt.Rows.Count > 0)
            {
                obj.partnerCode = Convert.ToString(dt.Rows[0]["partnerCode"]);
                obj.sponsorCode = Convert.ToString(dt.Rows[0]["sponsorCode"]);
                obj.company = Convert.ToString(dt.Rows[0]["company"]);
                obj.companyWebSite = Convert.ToString(dt.Rows[0]["companyWebSite"]);
                obj.fullName = Convert.ToString(dt.Rows[0]["fullName"]);
                obj.totalSale = Convert.ToInt32(dt.Rows[0]["totalSale"]);
                obj.monthlySale = Convert.ToInt32(dt.Rows[0]["monthlySale"]);
                obj.totalSaleAmount = Convert.ToDecimal(dt.Rows[0]["totalSaleAmount"]);
                obj.monthlySaleAmount = Convert.ToDecimal(dt.Rows[0]["monthlySaleAmount"]);
                obj.referralLink = _dataAccess.GetDomain() + "/partnerForm/" + obj.partnerCode;
                obj.rankNo = Convert.ToString(dt.Rows[0]["rankNo"]);
                obj.settledAmount = Convert.ToDecimal(dt.Rows[0]["settledAmount"]);
                obj.unSettledAmount = Convert.ToDecimal(dt.Rows[0]["unSettledAmount"]);
                obj.comission = Convert.ToDecimal(dt.Rows[0]["comission"]);
                obj.balance = Convert.ToDecimal(dt.Rows[0]["balance"]);
                obj.pendingSettlement = Convert.ToDecimal(dt.Rows[0]["pendingSettlement"]);
                obj.completeSettlement = Convert.ToDecimal(dt.Rows[0]["completeSettlement"]);
                obj.salePurchaseReport = (await getSalePurchaseReport(obj.partnerCode)).Where(x => x.status == 1).Take(5).ToList();
                obj.totalReport = await getDashboardSaleReport(obj.partnerCode);
            }
            return obj;
        }
        #endregion
    }
}
