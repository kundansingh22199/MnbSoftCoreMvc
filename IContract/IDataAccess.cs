using BusinessEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IContract
{
    public interface IDataAccess
    {
        #region Home Pannel
        Task<bool> AddPartner(ModPartner obj);
        Task<bool> CheckExists(string name, string action);
        Task<bool> AddAffiliate(ModAffiliate obj);
        Task<bool> AddCarrier(ModCarrier obj);
        Task<bool> AddContact(ModContact obj);
        Task<DataTable> Login(string email, string password, string action);
        Task<string> SendRegistrationMail(ModPartner obj);
        Task<string> SendRegistrationApproveMail(ModPartner obj);
        Task<string> SendOtp(string email, string otp,string name);
        Task<string> SendPassword(string email, string pass, string name);
        Task<bool> UpdateForgotPassword(string email, string pass);
        #endregion

        #region Admin Pannel
        Task<DataTable> AdminReport(string action);
        Task<bool> UpdatePartner(int id, int status, int type);
        Task<DataTable> AdminDashboard();
        Task<bool> UpdateSalePurchase(int id, int status, string remarks);
        Task<bool> RankAdd(ModRank obj);
        Task<bool> AddRankComission(ModComissionMaster obj);
        Task<bool> SettlementUpdate(int id, int status, string ids);
        Task<bool> AddAdminBank(ModBankDetails obj);
        Task<string> UpdateAdminPass(string oldPass, string newPass, string confPass);
        Task<string> SendAffiliateApproveMail(ModAffiliate obj);
        #endregion

        #region User Pannel
        Task<DataTable> PartnerDashboard(string email);
        Task<bool> AddPartnerBank(ModBankDetails obj);
        Task<DataTable> PartnerReport(string action, string partnerCode);
        Task<string> UpdatePass(string partnerCode, string oldPass, string newPass, string confPass);
        Task<bool> UpdatePartnerProfile(ModPartner obj);
        Task<bool> AddSalePurchase(ModSalePurchase obj);
        Task<bool> AddSettlement(ModSettlement obj);
        #endregion

        #region Affiliate Pannel
        Task<DataTable> AffiliateDashboard(string email);
        Task<string> UpdateAffiliatePass(string partnerCode, string oldPass, string newPass, string confPass);
        #endregion
        string GetDomain();
    }
}
