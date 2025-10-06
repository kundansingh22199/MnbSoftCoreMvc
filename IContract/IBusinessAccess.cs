using BusinessEntity;

namespace IContract
{
    public interface IBusinessAccess
    {
        #region Admin Pannel
        Task<List<ModPartner>> getPartnerReport();
        Task<List<ModAffiliate>> getAffiliateReport();
        Task<List<ModCarrier>> getcarrierReport();
        Task<List<ModContact>> getContactReport();
        Task<ModAdminDashboard> getAdminDashboard();
        Task<List<ModSalePurchase>> getAdminSalePurchaseReport();
        Task<List<ModComission>> getComissionReport();
        Task<List<ModRank>> getrankNameReport();
        Task<List<ModComissionMaster>> getRankComissionReport();
        Task<List<ModBankDetails>> getAdminBankInfo();
        Task<List<ModSettlement>> getSettlementReport();
        Task<List<ModCountry>> getCountryList();
        Task<List<ModState>> getStateList();
        Task<List<ModCity>> getCityList();
        Task<List<ModSaleReport>> getAdminDashboardSaleReport();
        #endregion

        #region Partner Pannel
        Task<ModPartnerDashboard> getPartnerDashboard(string email);
        Task<ModBankDetails> getBankInfo(string partnerCode);
        Task<List<ModSalePurchase>> getSalePurchaseReport(string partnerCode);
        #endregion

        #region
        Task<ModPartnerDashboard> getAffiliateDashboard(string email);
        #endregion
    }
}
