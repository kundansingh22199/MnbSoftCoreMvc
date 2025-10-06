using BusinessEntity;
using Connection;
using IContract;
using Microsoft.AspNetCore.Http;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;

namespace DataAccessLayer
{
    public class DataAccess : IDataAccess
    {
        private readonly ClsConnection _con;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DataAccess(ClsConnection con, IHttpContextAccessor httpContextAccessor)
        {
            _con = con;
            _httpContextAccessor = httpContextAccessor;
        }

        #region Home Pannel
        public async Task<bool> AddPartner(ModPartner obj)
        {
            using (var connection = new SqlConnection(_con.GetConnectionString()))
            {
                try
                {
                    await connection.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("SP_AddPartner", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@partnerCode", obj.partnerCode);
                        //cmd.Parameters.AddWithValue("@sponsorCode", obj.sponsorCode);
                        cmd.Parameters.AddWithValue("@company", obj.company);
                        cmd.Parameters.AddWithValue("@companyWebsite", obj.companyWebsite);
                        cmd.Parameters.AddWithValue("@fullName", obj.fullName);
                        cmd.Parameters.AddWithValue("@mobile", obj.mobile);
                        cmd.Parameters.AddWithValue("@country", obj.country);
                        cmd.Parameters.AddWithValue("@state", obj.state);
                        cmd.Parameters.AddWithValue("@city", obj.city);
                        cmd.Parameters.AddWithValue("@address", obj.address);
                        cmd.Parameters.AddWithValue("@businessEmail", obj.businessEmail);
                        cmd.Parameters.AddWithValue("@whyBecomeReseller", obj.whyBecomeReseller);
                        cmd.Parameters.AddWithValue("@teamSize", obj.teamSize);
                        cmd.Parameters.AddWithValue("@branchOfficeLocation", obj.branchOfficeLocation);
                        cmd.Parameters.AddWithValue("@gstNo", obj.gstNo);
                        int i = await cmd.ExecuteNonQueryAsync();
                        if (i > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }
        }
        public async Task<bool> CheckExists(string name, string action)
        {
            using (var connection = new SqlConnection(_con.GetConnectionString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    using (SqlCommand cmd = new SqlCommand("SP_CheckExists", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@action", action);
                        SqlDataAdapter dr = new SqlDataAdapter(cmd);
                        dr.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
        public async Task<bool> AddAffiliate(ModAffiliate obj)
        {
            using (var connection = new SqlConnection(_con.GetConnectionString()))
            {
                try
                {
                    await connection.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("SP_AddAffiliate", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@firstName", obj.firstName);
                        cmd.Parameters.AddWithValue("@lastName", obj.lastName);
                        cmd.Parameters.AddWithValue("@mobile", obj.mobile);
                        cmd.Parameters.AddWithValue("@email", obj.email);
                        cmd.Parameters.AddWithValue("@country", obj.country);
                        cmd.Parameters.AddWithValue("@subject", obj.subject);
                        int i = await cmd.ExecuteNonQueryAsync();
                        if (i > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }
        }
        public async Task<bool> AddCarrier(ModCarrier obj)
        {
            using (var connection = new SqlConnection(_con.GetConnectionString()))
            {
                try
                {
                    await connection.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("SP_AddCarrier", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@firstName", obj.firstName);
                        cmd.Parameters.AddWithValue("@lastName", obj.lastName);
                        cmd.Parameters.AddWithValue("@email", obj.email);
                        cmd.Parameters.AddWithValue("@mobile", obj.mobile);
                        cmd.Parameters.AddWithValue("@yearOfGraduation", obj.yearOfGraduation);
                        cmd.Parameters.AddWithValue("@gender", obj.gender);
                        cmd.Parameters.AddWithValue("@yearOfExperience", obj.yearOfExperience);
                        cmd.Parameters.AddWithValue("@currentEmployer", obj.currentEmployer);
                        cmd.Parameters.AddWithValue("@currentCtc", obj.currentCtc);
                        cmd.Parameters.AddWithValue("@expectedCtc", obj.expectedCtc);
                        cmd.Parameters.AddWithValue("@noticePeriod", obj.noticePeriod);
                        cmd.Parameters.AddWithValue("@skillSet", obj.skillSet);
                        cmd.Parameters.AddWithValue("@uploadResume", obj.uploadResume);
                        int i = await cmd.ExecuteNonQueryAsync();
                        if (i > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }
        }
        public async Task<bool> AddContact(ModContact obj)
        {
            using (var connection = new SqlConnection(_con.GetConnectionString()))
            {
                try
                {
                    await connection.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("SP_AddContact", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@name", obj.name);
                        cmd.Parameters.AddWithValue("@email", obj.email);
                        cmd.Parameters.AddWithValue("@subject", obj.subject);
                        cmd.Parameters.AddWithValue("@message", obj.message);
                        int i = await cmd.ExecuteNonQueryAsync();
                        if (i > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }
        }
        public async Task<DataTable> Login(string email, string password, string action)
        {
            using (var connection = new SqlConnection(_con.GetConnectionString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    using (SqlCommand cmd = new SqlCommand("SP_AdminLogin", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@password", password);
                        cmd.Parameters.AddWithValue("@action", action);
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        sda.Fill(dt);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            return dt;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }
        }
        public async Task<string> SendRegistrationMail(ModPartner obj)
        {
            using (var connection = new SqlConnection(_con.GetConnectionString()))
            {
                try
                {
                    MailMessage message = new MailMessage();
                    SmtpClient smtpClient = new SmtpClient();
                    string msg = string.Empty;
                    try
                    {
                        MailAddress fromAddress = new MailAddress("mnbsoftsolution@gmail.com");
                        message.From = fromAddress;
                        message.To.Add(obj.businessEmail.ToString());
                        message.Subject = "Mnb Soft Solution Credential";
                        message.IsBodyHtml = true;

                        string _mailBodyHTML = @"<div bgcolor='#f0f0f0' style='background-color:#f0f0f0;margin:10px 0;padding:10px 0;width:100%'>
                                    <span ><font color='#888888'></font></span><span class='HOEnZb'><font color='#888888'></font></span>
                                    <table style='border-spacing:0' width='100%' height='100%' cellspacing='0' cellpadding='0' border='0' bgcolor='#f0f0f0'>
                                    <tbody><tr>
                                    <td style='border-collapse:collapse' valign='top' bgcolor='#f0f0f0' align='center'>
                                    <span ><font color='#888888'></font></span>
                                    <span><font color='#888888'> </font></span>
                                    <table style='border-bottom:1px solid #e0e0e0;border-spacing:0;color:#434343;font-family:Helvetica,Verdana,sans-serif;max-width:600px;overflow:hidden' cellspacing='0' cellpadding='0' border='0' bgcolor='#ffffff'>
                                    <tbody><tr>
                                    <td style='border-collapse:collapse;border-left:1px solid #e0e0e0'>
                                    <div style='background-color:#0e4595;border:1px solid #042f6d;border-radius:2px;height:5px'></div>
                                    </td>
                                    </tr>
                                    <tr>
                                    <td  style='border-bottom:1px solid #efefef;border-collapse:collapse;border-left:1px solid #e0e0e0;max-width:600px;padding:20px 0;text-align:center' align='center'>
                                    <a href='{compDomain}' style='color:#0e4595;text-decoration:underline;word-break:break-word;word-wrap:break-word' target='_blank' data-saferedirecturl=''>
                                    <img src='{logoSrc}' alt='{compName}' style='max-width:80%'>
                                    </a>
                                    </td>
                                    </tr>
                                    <tr>
                                    <td  style='background:#ffffff;border-collapse:collapse;border-left:1px solid #e0e0e0;padding-left:30px;padding-right:30px' bgcolor='#ffffff'>
                                    <br>
                                    <p style='color:#434343;font-size:14px;line-height:1.5;margin:0 0 25px 0'>
                                    Hi {userName},
                                    </p>
                                    <p style='color:#434343;font-size:14px;line-height:1.5;margin:0 0 25px 0'>
                                    Registration is Successfull !!.<br/>
                                    Welcome to the MNB Become a Partner Network<br>
                                    Thank you for registering with <strong>MNB Become a Partner</strong>. Your application has been received successfully, and you are now one step closer to becoming a valued partner in our growth journey.<br>
                                    <p style='color:#434343;font-size:14px;line-height:1.5;margin:0 0 25px 0'>
                                    {compName}.<br/>
                                    Company Name : {companyName}. <br>
                                    Website : {website}<br>
                                    Partner Code : {partnercode}<br>
                                    Business Email : {businessemail}<br>
                                    </p>
                                    
                                    <p style='color:#434343;font-size:14px;line-height:1.5;margin:0 0 25px 0'>
                                    Best Regards, <br>
                                    <a href='{compDomain}' style='color:#0e4595;text-decoration:underline;word-break:break-word;word-wrap:break-word' target='_blank' data-saferedirecturl=''>{compName}</a>
                                    </p>
                                    </td>
                                    </tr>
                                    <tr>
                                    <td style='border-collapse:collapse;border-left:1px solid #e0e0e0'>
                                    <span ><font color='#888888'>
                                    </font></span><span ><font color='#888888'>
                                    </font></span><table class='m_2264787707347477873m_-8276338735438771956m_-8110430706966875067footer' style='border-spacing:0;border-top:1px solid #efefef;width:100%' width='100%' height='100%' cellspacing='0' cellpadding='0' border='0' bgcolor='#fbfbfb'>
                                    <tbody><tr>
                                    <td 
                                    style='border-collapse:collapse;border-left:none;color:#434343;font-size:12px;line-height:20px;padding:5px;text-align:center;vertical-align:middle;width:60%' valign='middle'>
                                    Follow us:
                                    <a href='' class='' style='color:#00aced;font-weight:bold;margin:0 10px;text-decoration:none;word-break:break-word;word-wrap:break-word' target='_blank' data-saferedirecturl=''>
                                    Twitter
                                    </a>
                                    <a href='' class='' style='color:#6d83bc;font-weight:bold;margin:0 10px;text-decoration:none;word-break:break-word;word-wrap:break-word' target='_blank' data-saferedirecturl=''>
                                    Facebook
                                    </a>
                                    </td>
                                    <td class='m_2264787707347477873m_-8276338735438771956m_-8110430706966875067help-me' style='border-collapse:collapse;color:#434343;font-size:12px;padding:5px;text-align:center;vertical-align:middle;width:40%' valign='middle'>
                                    This is an auto generated mail, Do not revert over this mail.<br><br>
                                    <a href='' style='color:#0e4595;text-decoration:underline;word-break:break-word;word-wrap:break-word' target='_blank' data-saferedirecturl=''></a><span ><font color='#888888'>
                                    </font></span></td></tr></tbody></table><span class='HOEnZb'><font color='#888888'>
                                    </font></span></td></tr></tbody></table><span class='HOEnZb'><font color='#888888'>
                                    </font></span></td></tr></tbody></table><div><div >
                                    </div>";
                        _mailBodyHTML = _mailBodyHTML.Replace("{userName}", obj.fullName);
                        _mailBodyHTML = _mailBodyHTML.Replace("{userLoginID}", obj.partnerCode);
                        _mailBodyHTML = _mailBodyHTML.Replace("{logoSrc}", "https://mnbsoftsolution.com/assets/images/logo.png");
                        _mailBodyHTML = _mailBodyHTML.Replace("{compName}", "Mnb Soft Solution");
                        _mailBodyHTML = _mailBodyHTML.Replace("{companyName}", obj.company);
                        _mailBodyHTML = _mailBodyHTML.Replace("{website}", obj.companyWebsite);
                        _mailBodyHTML = _mailBodyHTML.Replace("{partnercode}", obj.partnerCode);
                        _mailBodyHTML = _mailBodyHTML.Replace("{businessemail}", obj.businessEmail);
                        _mailBodyHTML = _mailBodyHTML.Replace("{compDomain}", "https://mnbsoftsolution.com");
                        message.Body = _mailBodyHTML;
                        smtpClient.Host = "smtp.gmail.com"; //-- Donot change. "dedrelay.secureserver.net";
                        smtpClient.Port = 587; //--- Donot change
                        smtpClient.EnableSsl = true;//--- Donot change
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.Credentials = new System.Net.NetworkCredential("mnbsoftsolution@gmail.com", "lftqidmojhkbaiip");
                        smtpClient.Send(message);
                        return "Ok";
                    }
                    catch (Exception ex)
                    {
                        msg = ex.Message;
                        return msg;
                    }
                }
                catch (Exception)
                {
                    return "Failed !";
                }
            }
        }
        public async Task<string> SendOtp(string email, string otp, string name)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtpClient = new SmtpClient();
                string msg = string.Empty;
                try
                {
                    MailAddress fromAddress = new MailAddress("mnbsoftsolution@gmail.com");
                    message.From = fromAddress;
                    message.To.Add(email);
                    message.Subject = "Mnb Soft Solution Credential";
                    message.IsBodyHtml = true;

                    string _mailBodyHTML = @"<div marginwidth='0' marginheight='0'
                    style='margin-top:0;margin-bottom:0;padding-top:0;padding-bottom:0;width:100%!important;background-color:#f4f5ff'>
                    <table width='100%' cellpadding='0' cellspacing='0' align='center' bgcolor='#f4f5ff'>
                       <tbody>
                          <tr>
                             <td align='center' valign='top'>
                                <table width='640' cellpadding='0' cellspacing='0' style='width:640px ; margin-top:20px'>
                                   <tbody>
                                      <tr>
                                         <td align='center' valign='top'>
                                            <table width='100%' cellpadding='0' cellspacing='0'>
                                               <tbody>
                                                  <tr>
                                                     <td width='20'>&nbsp;</td>
                                                     <td align='center' valign='top'>
                                                        <table width='100%' cellpadding='0' cellspacing='0'
                                                           bgcolor='#fafbff'>
                                                           <tbody>
                                                              <tr>
                                                                 <td height='20' style='line-height:1px;font-size:1px'>
                                                                    &nbsp;
                                                                 </td>
                                                              </tr>
                                                              <tr>
                                                                 <td align='center' valign='top'>
                                                                    <table width='100%' cellpadding='0' cellspacing='0'>
                                                                       <tbody>
                                                                          <tr>
                                                                             <td width='40'>&nbsp;</td>
                                                                             <td align='center' valign='top'>
                                                                                <table width='100%' cellpadding='0'
                                                                                   cellspacing='0'>
                                                                                   <tbody>
                                                                                      <tr>
                                                                                         <td align='left'
                                                                                            valign='top'><a
                                                                                            href='{compDomain}'
                                                                                            target='_blank'><img
                                                                                            src='{logoSrc}'
                                                                                            width='120'
                                                                                            border='0'
                                                                                            style='width:120px;max-width:140px;height:auto;display:block'
                                                                                            alt='MNB Soft Solution'></a>
                                                                                         </td>
                                                                                         <td align='right'
                                                                                            valign='top'
                                                                                            style='Margin:0;font-family:roboto,helvetica,arial,sans-serif;line-height:21px;color:#673de6;font-size:14px;font-weight:bold'>
                                                                                         </td>
                                                                                      </tr>
                                                                                   </tbody>
                                                                                </table>
                                                                             </td>
                                                                             <td width='40'>&nbsp;</td>
                                                                          </tr>
                                                                       </tbody>
                                                                    </table>
                                                                 </td>
                                                              </tr>
                                                              <tr>
                                                                 <td height='20' style='line-height:1px;font-size:1px'>
                                                                    &nbsp;
                                                                 </td>
                                                              </tr>
                                                           </tbody>
                                                        </table>
                                                     </td>
                                                     <td width='20'>&nbsp;</td>
                                                  </tr>
                                               </tbody>
                                            </table>
                                         </td>
                                      </tr>
                                   </tbody>
                                </table>
                             </td>
                          </tr>
                       </tbody>
                    </table>
                    <div>
                       <div>
                          <table width='100%' cellpadding='0' cellspacing='0' align='center' style='background-color:#f4f5ff; font-family:Google Sans,Roboto,Helvetica Neue,Helvetica,Arial,sans-serif; font-size:14px'>
                             <tbody>
                                <tr>
                                   <td align='center' valign='top'>
                                      <table width='640' cellpadding='0' cellspacing='0' style='width:640px'>
                                         <tbody>
                                            <tr>
                                               <td align='center' valign='top'>
                                                  <table width='100%' cellpadding='0' cellspacing='0'>
                                                     <tbody>
                                                        <tr>
                                                           <td width='20' style='line-height:1px;font-size:1px'>&nbsp;</td>
                                                           <td align='center' valign='top'>
                                                              <table width='100%' cellpadding='0' cellspacing='0'
                                                                 style='background-color:#fafbff'>
                                                                 <tbody>
                                                                    <tr>
                                                                       <td height='20'
                                                                          style='line-height:1px;font-size:1px'>&nbsp;
                                                                       </td>
                                                                    </tr>
                                                                    <tr>
                                                                       <td align='center' valign='top'>
                                                                          <table width='100%' cellpadding='0'
                                                                             cellspacing='0'>
                                                                             <tbody>
                                                                                <tr>
                                                                                   <td width='40'>&nbsp;</td>
                                                                                   <td align='center' valign='top'>
                                                                                      <table width='100%'
                                                                                         cellpadding='0'
                                                                                         cellspacing='0'>
                                                                                         <tbody>
                                                                                            <tr>
                                                                                               <td align='left'
                                                                                                  valign='top'
                                                                                                  style='font-family:'DM Sans',sans-serif,-apple-system,BlinkMacSystemFont,'Segoe UI',Roboto,Helvetica,Arial,sans-serif,'Apple Color Emoji','Segoe UI Emoji','Segoe UI Symbol';font-size:14px;line-height:20px;font-weight:400;font-style:normal;letter-spacing:normal;color:#2f1c6a'
                                                                                                  class='m_1741340122979844724txt_12'>
                                                                                                  Dear
                                                                                                  <strong>{name}</strong>,<br>
                                                                                                  Thank you for reaching out to <strong>{compName}.</strong>
                                                                                                  To verify your email address, please use the One-Time Password (OTP) below:<br/><br/>
                                                                                                  To complete your authentication, please use the following One-Time Password (OTP):
                                                                                                  <br><br> Your OTP :<b> {otp}</b><br><br>
                                                                                                  This This OTP is valid for <strong>5 minutes.</strong><br>
                                                                                                  Please enter it on the verification page to complete your request.<br> <br>
                                                                                                  If you didn’t request this verification, you can safely ignore this email.
                                                                                                  <br><br>
                                                                                                  <b>Best Regards,</b><br>The
                                                                                                  {compName} <br>www.mnbsoft.com | ask@mnbsoft.com | +91 9311 221776
                                                                                               </td>
                                                                                            </tr>
                                                                                         </tbody>
                                                                                      </table>
                                                                                   </td>
                                                                                   <td width='40'>&nbsp;</td>
                                                                                </tr>
                                                                             </tbody>
                                                                          </table>
                                                                       </td>
                                                                    </tr>
                                                                    <tr>
                                                                       <td height='20'
                                                                          style='line-height:1px;font-size:1px'>&nbsp;
                                                                       </td>
                                                                    </tr>
                                                                 </tbody>
                                                              </table>
                                                           </td>
                                                           <td width='20' style='line-height:1px;font-size:1px'>&nbsp;</td>
                                                        </tr>
                                                     </tbody>
                                                  </table>
                                               </td>
                                            </tr>
                                         </tbody>
                                      </table>
                                   </td>
                                </tr>
                             </tbody>
                          </table>
                       </div>
                    </div>
                    <table width='100%' cellpadding='0' cellspacing='0' align='center' bgcolor='#f4f5ff'>
                       <tbody>
                          <tr>
                             <td align='center' valign='top'>
                                <table width='640' cellpadding='0' cellspacing='0' style='width:640px'>
                                   <tbody>
                                      <tr>
                                         <td width='20'>&nbsp;</td>
                                         <td align='center' valign='top'>
                                            <table width='100%' cellpadding='0' cellspacing='0' bgcolor='#f4f5ff'>
                                               <tbody>
                                                  <tr>
                                                     <td width='30'>&nbsp;</td>
                                                     <td align='center' valign='top'>
                                                        <table width='100%' cellpadding='0' cellspacing='0'>
                                                           <tbody>
                                                              <tr>
                                                                 <td height='20' style='line-height:1px;font-size:1px'>
                                                                    &nbsp;
                                                                 </td>
                                                              </tr>
                                                           </tbody>
                                                        </table>
                                                        <table width='100%' cellpadding='0' cellspacing='0'>
                                                           <tbody>
                                                              <tr>
                                                                 <td align='left' valign='top'><a
                                                                    href='{compDomain}'
                                                                    target='_blank'><img
                                                                    src='{logoSrc}'
                                                                    border='0'
                                                                    style='width:60px;max-width:147px;height:auto;display:block'
                                                                    alt='{compDomain}'></a></td>
                                                              </tr>
                                                           </tbody>
                                                        </table>
                                                         <table width='100%' cellpadding='0' cellspacing='0'>
                                                           <tbody>
                                                              <tr>
                                                                 <td height='10' style='line-height:1px;font-size:1px'>
                                                                    &nbsp;
                                                                 </td>
                                                              </tr>
                                                           </tbody>
                                                        </table>
                                                        <table width='100%' cellpadding='0' cellspacing='0' style='font-family:Google Sans,Roboto,Helvetica Neue,Helvetica,Arial,sans-serif;'>
                                                           <tbody>
                                                              <tr></tr>
                                                              <tr>
                                                                 <td
                                                                    style='font-family:'DM Sans',sans-serif,-apple-system,BlinkMacSystemFont,'Segoe UI',Roboto,Helvetica,Arial,sans-serif,'Apple Color Emoji','Segoe UI Emoji','Segoe UI Symbol';font-size:12px;line-height:14px;font-weight:400;color:#555555'>
                                                                    You have received this email because you are
                                                                    registered at  Mnb Soft Solution, to ensure the
                                                                    implementation of our Terms of Service and (or) for
                                                                    other legitimate matters.<br><br>
                                                                 </td>
                                                              </tr>
                                                              <tr>
                                                                 <td align='left' valign='top'
                                                                    style='font-family:'DM Sans',sans-serif,-apple-system,BlinkMacSystemFont,'Segoe UI',Roboto,Helvetica,Arial,sans-serif,'Apple Color Emoji','Segoe UI Emoji','Segoe UI Symbol';font-size:12px;line-height:14px;font-weight:400;color:#555555'
                                                                    class='m_1741340122979844724ftr'><a href='https://mnbsoft.com/Privacy'
                                                                    target='_blank'><span
                                                                    style='color:#555555;text-decoration:underline;display:inline-block'>Privacy Policy</span></a><br><br>2024-2025
                                                                    MNB Soft Solution
                                                                 </td>
                                                              </tr>
                                                           </tbody>
                                                        </table>
                                                        <table width='100%' cellpadding='0' cellspacing='0'>
                                                           <tbody>
                                                              <tr>
                                                                 <td height='40' style='line-height:1px;font-size:1px'>
                                                                    &nbsp;
                                                                 </td>
                                                              </tr>
                                                           </tbody>
                                                        </table>
                                                     </td>
                                                     <td width='30'>&nbsp;</td>
                                                  </tr>
                                               </tbody>
                                            </table>
                                         </td>
                                         <td width='20'>&nbsp;</td>
                                      </tr>
                                   </tbody>
                                </table>
                             </td>
                          </tr>
                       </tbody>
                    </table>
                    </div>";
                    _mailBodyHTML = _mailBodyHTML.Replace("{logoSrc}", "https://mnbsoft.com/images/logo/black-logo.png");
                    _mailBodyHTML = _mailBodyHTML.Replace("{compName}", "Mnb Soft Solution");
                    _mailBodyHTML = _mailBodyHTML.Replace("{otp}", otp);
                    _mailBodyHTML = _mailBodyHTML.Replace("{name}", name);
                    _mailBodyHTML = _mailBodyHTML.Replace("{compDomain}", "https://mnbsoft.com");
                    message.Body = _mailBodyHTML;
                    smtpClient.Host = "smtp.gmail.com"; //-- Donot change. "dedrelay.secureserver.net";
                    smtpClient.Port = 587; //--- Donot change
                    smtpClient.EnableSsl = true;//--- Donot change
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new System.Net.NetworkCredential("mnbsoftsolution@gmail.com", "lftqidmojhkbaiip");
                    smtpClient.Send(message);
                    return "Ok";
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                    return msg;
                }
            }
            catch (Exception)
            {
                return "Failed !";
            }
        }
        public async Task<string> SendPassword(string email, string pass, string name)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtpClient = new SmtpClient();
                string msg = string.Empty;
                try
                {
                    MailAddress fromAddress = new MailAddress("mnbsoftsolution@gmail.com");
                    message.From = fromAddress;
                    message.To.Add(email);
                    message.Subject = "Mnb Soft Solution Credential";
                    message.IsBodyHtml = true;

                    string _mailBodyHTML = @"<div marginwidth='0' marginheight='0'
                    style='margin-top:0;margin-bottom:0;padding-top:0;padding-bottom:0;width:100%!important;background-color:#f4f5ff'>
                    <table width='100%' cellpadding='0' cellspacing='0' align='center' bgcolor='#f4f5ff'>
                       <tbody>
                          <tr>
                             <td align='center' valign='top'>
                                <table width='640' cellpadding='0' cellspacing='0' style='width:640px ; margin-top:20px'>
                                   <tbody>
                                      <tr>
                                         <td align='center' valign='top'>
                                            <table width='100%' cellpadding='0' cellspacing='0'>
                                               <tbody>
                                                  <tr>
                                                     <td width='20'>&nbsp;</td>
                                                     <td align='center' valign='top'>
                                                        <table width='100%' cellpadding='0' cellspacing='0'
                                                           bgcolor='#fafbff'>
                                                           <tbody>
                                                              <tr>
                                                                 <td height='20' style='line-height:1px;font-size:1px'>
                                                                    &nbsp;
                                                                 </td>
                                                              </tr>
                                                              <tr>
                                                                 <td align='center' valign='top'>
                                                                    <table width='100%' cellpadding='0' cellspacing='0'>
                                                                       <tbody>
                                                                          <tr>
                                                                             <td width='40'>&nbsp;</td>
                                                                             <td align='center' valign='top'>
                                                                                <table width='100%' cellpadding='0'
                                                                                   cellspacing='0'>
                                                                                   <tbody>
                                                                                      <tr>
                                                                                         <td align='left'
                                                                                            valign='top'><a
                                                                                            href='{compDomain}'
                                                                                            target='_blank'><img
                                                                                            src='{logoSrc}'
                                                                                            width='120'
                                                                                            border='0'
                                                                                            style='width:120px;max-width:140px;height:auto;display:block'
                                                                                            alt='MNB Soft Solution'></a>
                                                                                         </td>
                                                                                         <td align='right'
                                                                                            valign='top'
                                                                                            style='Margin:0;font-family:roboto,helvetica,arial,sans-serif;line-height:21px;color:#673de6;font-size:14px;font-weight:bold'>
                                                                                         </td>
                                                                                      </tr>
                                                                                   </tbody>
                                                                                </table>
                                                                             </td>
                                                                             <td width='40'>&nbsp;</td>
                                                                          </tr>
                                                                       </tbody>
                                                                    </table>
                                                                 </td>
                                                              </tr>
                                                              <tr>
                                                                 <td height='20' style='line-height:1px;font-size:1px'>
                                                                    &nbsp;
                                                                 </td>
                                                              </tr>
                                                           </tbody>
                                                        </table>
                                                     </td>
                                                     <td width='20'>&nbsp;</td>
                                                  </tr>
                                               </tbody>
                                            </table>
                                         </td>
                                      </tr>
                                   </tbody>
                                </table>
                             </td>
                          </tr>
                       </tbody>
                    </table>
                    <div>
                       <div>
                          <table width='100%' cellpadding='0' cellspacing='0' align='center' style='background-color:#f4f5ff; font-family:Google Sans,Roboto,Helvetica Neue,Helvetica,Arial,sans-serif; font-size:14px'>
                             <tbody>
                                <tr>
                                   <td align='center' valign='top'>
                                      <table width='640' cellpadding='0' cellspacing='0' style='width:640px'>
                                         <tbody>
                                            <tr>
                                               <td align='center' valign='top'>
                                                  <table width='100%' cellpadding='0' cellspacing='0'>
                                                     <tbody>
                                                        <tr>
                                                           <td width='20' style='line-height:1px;font-size:1px'>&nbsp;</td>
                                                           <td align='center' valign='top'>
                                                              <table width='100%' cellpadding='0' cellspacing='0'
                                                                 style='background-color:#fafbff'>
                                                                 <tbody>
                                                                    <tr>
                                                                       <td height='20'
                                                                          style='line-height:1px;font-size:1px'>&nbsp;
                                                                       </td>
                                                                    </tr>
                                                                    <tr>
                                                                       <td align='center' valign='top'>
                                                                          <table width='100%' cellpadding='0'
                                                                             cellspacing='0'>
                                                                             <tbody>
                                                                                <tr>
                                                                                   <td width='40'>&nbsp;</td>
                                                                                   <td align='center' valign='top'>
                                                                                      <table width='100%'
                                                                                         cellpadding='0'
                                                                                         cellspacing='0'>
                                                                                         <tbody>
                                                                                            <tr>
                                                                                               <td align='left'
                                                                                                  valign='top'
                                                                                                  style='font-family:'DM Sans',sans-serif,-apple-system,BlinkMacSystemFont,'Segoe UI',Roboto,Helvetica,Arial,sans-serif,'Apple Color Emoji','Segoe UI Emoji','Segoe UI Symbol';font-size:14px;line-height:20px;font-weight:400;font-style:normal;letter-spacing:normal;color:#2f1c6a'
                                                                                                  class='m_1741340122979844724txt_12'>
                                                                                                  Dear
                                                                                                  <strong>{name}</strong>,<br>
                                                                                                  Thank you for reaching out to <strong>{compName}.</strong>
                                                                                                  To Forgot your Password, please use the password below to Login Your Account:<br/><br/>
                                                                                                   Your Password :<b> {pass}</b><br><br>
                                                                                                  This Password is use to login your Account<br>
                                                                                                  <br><br>
                                                                                                  <b>Best Regards,</b><br>The
                                                                                                  {compName} <br>www.mnbsoft.com | ask@mnbsoft.com | +91 9311 221776
                                                                                               </td>
                                                                                            </tr>
                                                                                         </tbody>
                                                                                      </table>
                                                                                   </td>
                                                                                   <td width='40'>&nbsp;</td>
                                                                                </tr>
                                                                             </tbody>
                                                                          </table>
                                                                       </td>
                                                                    </tr>
                                                                    <tr>
                                                                       <td height='20'
                                                                          style='line-height:1px;font-size:1px'>&nbsp;
                                                                       </td>
                                                                    </tr>
                                                                 </tbody>
                                                              </table>
                                                           </td>
                                                           <td width='20' style='line-height:1px;font-size:1px'>&nbsp;</td>
                                                        </tr>
                                                     </tbody>
                                                  </table>
                                               </td>
                                            </tr>
                                         </tbody>
                                      </table>
                                   </td>
                                </tr>
                             </tbody>
                          </table>
                       </div>
                    </div>
                    <table width='100%' cellpadding='0' cellspacing='0' align='center' bgcolor='#f4f5ff'>
                       <tbody>
                          <tr>
                             <td align='center' valign='top'>
                                <table width='640' cellpadding='0' cellspacing='0' style='width:640px'>
                                   <tbody>
                                      <tr>
                                         <td width='20'>&nbsp;</td>
                                         <td align='center' valign='top'>
                                            <table width='100%' cellpadding='0' cellspacing='0' bgcolor='#f4f5ff'>
                                               <tbody>
                                                  <tr>
                                                     <td width='30'>&nbsp;</td>
                                                     <td align='center' valign='top'>
                                                        <table width='100%' cellpadding='0' cellspacing='0'>
                                                           <tbody>
                                                              <tr>
                                                                 <td height='20' style='line-height:1px;font-size:1px'>
                                                                    &nbsp;
                                                                 </td>
                                                              </tr>
                                                           </tbody>
                                                        </table>
                                                        <table width='100%' cellpadding='0' cellspacing='0'>
                                                           <tbody>
                                                              <tr>
                                                                 <td align='left' valign='top'><a
                                                                    href='{compDomain}'
                                                                    target='_blank'><img
                                                                    src='{logoSrc}'
                                                                    border='0'
                                                                    style='width:60px;max-width:147px;height:auto;display:block'
                                                                    alt='{compDomain}'></a></td>
                                                              </tr>
                                                           </tbody>
                                                        </table>
                                                         <table width='100%' cellpadding='0' cellspacing='0'>
                                                           <tbody>
                                                              <tr>
                                                                 <td height='10' style='line-height:1px;font-size:1px'>
                                                                    &nbsp;
                                                                 </td>
                                                              </tr>
                                                           </tbody>
                                                        </table>
                                                        <table width='100%' cellpadding='0' cellspacing='0' style='font-family:Google Sans,Roboto,Helvetica Neue,Helvetica,Arial,sans-serif;'>
                                                           <tbody>
                                                              <tr></tr>
                                                              <tr>
                                                                 <td
                                                                    style='font-family:'DM Sans',sans-serif,-apple-system,BlinkMacSystemFont,'Segoe UI',Roboto,Helvetica,Arial,sans-serif,'Apple Color Emoji','Segoe UI Emoji','Segoe UI Symbol';font-size:12px;line-height:14px;font-weight:400;color:#555555'>
                                                                    You have received this email because you are
                                                                    registered at  Mnb Soft Solution, to ensure the
                                                                    implementation of our Terms of Service and (or) for
                                                                    other legitimate matters.<br><br>
                                                                 </td>
                                                              </tr>
                                                              <tr>
                                                                 <td align='left' valign='top'
                                                                    style='font-family:'DM Sans',sans-serif,-apple-system,BlinkMacSystemFont,'Segoe UI',Roboto,Helvetica,Arial,sans-serif,'Apple Color Emoji','Segoe UI Emoji','Segoe UI Symbol';font-size:12px;line-height:14px;font-weight:400;color:#555555'
                                                                    class='m_1741340122979844724ftr'><a href='https://mnbsoft.com/Privacy'
                                                                    target='_blank'><span
                                                                    style='color:#555555;text-decoration:underline;display:inline-block'>Privacy Policy</span></a><br><br>2024-2025
                                                                    MNB Soft Solution
                                                                 </td>
                                                              </tr>
                                                           </tbody>
                                                        </table>
                                                        <table width='100%' cellpadding='0' cellspacing='0'>
                                                           <tbody>
                                                              <tr>
                                                                 <td height='40' style='line-height:1px;font-size:1px'>
                                                                    &nbsp;
                                                                 </td>
                                                              </tr>
                                                           </tbody>
                                                        </table>
                                                     </td>
                                                     <td width='30'>&nbsp;</td>
                                                  </tr>
                                               </tbody>
                                            </table>
                                         </td>
                                         <td width='20'>&nbsp;</td>
                                      </tr>
                                   </tbody>
                                </table>
                             </td>
                          </tr>
                       </tbody>
                    </table>
                    </div>";
                    _mailBodyHTML = _mailBodyHTML.Replace("{logoSrc}", "https://mnbsoft.com/images/logo/black-logo.png");
                    _mailBodyHTML = _mailBodyHTML.Replace("{compName}", "Mnb Soft Solution");
                    _mailBodyHTML = _mailBodyHTML.Replace("{pass}", pass);
                    _mailBodyHTML = _mailBodyHTML.Replace("{name}", name);
                    _mailBodyHTML = _mailBodyHTML.Replace("{compDomain}", "https://mnbsoft.com");
                    message.Body = _mailBodyHTML;
                    smtpClient.Host = "smtp.gmail.com"; //-- Donot change. "dedrelay.secureserver.net";
                    smtpClient.Port = 587; //--- Donot change
                    smtpClient.EnableSsl = true;//--- Donot change
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new System.Net.NetworkCredential("mnbsoftsolution@gmail.com", "lftqidmojhkbaiip");
                    smtpClient.Send(message);
                    return "Ok";
                }
                catch (Exception ex)
                {
                    msg = ex.Message;
                    return msg;
                }
            }
            catch (Exception)
            {
                return "Failed !";
            }
        }
        public async Task<bool> UpdateForgotPassword(string email, string pass)
        {
            using (var connection = new SqlConnection(_con.GetConnectionString()))
            {
                try
                {
                    await connection.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("SP_ForgotPassword", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@pass", pass);
                        int i = await cmd.ExecuteNonQueryAsync();
                        if (i > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }
        }
        #endregion

        #region Admin Pannel
        public async Task<DataTable> AdminReport(string action)
        {
            using (var connection = new SqlConnection(_con.GetConnectionString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    using (SqlCommand cmd = new SqlCommand("SP_GetAdminReport", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@action", action);
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        sda.Fill(dt);
                        return dt;
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }
        }
        public async Task<bool> UpdatePartner(int id, int status,int type)
        {
            using (var connection = new SqlConnection(_con.GetConnectionString()))
            {
                try
                {
                    await connection.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("SP_UpdatePartner", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@status", status);
                        cmd.Parameters.AddWithValue("@type", type);
                        int i = await cmd.ExecuteNonQueryAsync();
                        if (i > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }
        }
        public async Task<bool> UpdateSalePurchase(int id, int status, string remarks)
        {
            using (var connection = new SqlConnection(_con.GetConnectionString()))
            {
                try
                {
                    await connection.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("SP_UpdateSalePurchase", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@status", status);
                        cmd.Parameters.AddWithValue("@remarks", remarks);
                        int i = await cmd.ExecuteNonQueryAsync();
                        if (i > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }
        }
        public async Task<string> SendRegistrationApproveMail(ModPartner obj)
        {
            using (var connection = new SqlConnection(_con.GetConnectionString()))
            {
                try
                {
                    MailMessage message = new MailMessage();
                    SmtpClient smtpClient = new SmtpClient();
                    string msg = string.Empty;
                    try
                    {

                        MailAddress fromAddress = new MailAddress("mnbsoftsolution@gmail.com");
                        message.From = fromAddress;
                        message.To.Add(obj.businessEmail.ToString());
                        message.Subject = "Mnb Soft Solution Credential";
                        message.IsBodyHtml = true;

                        string _mailBodyHTML = @"<div marginwidth='0' marginheight='0'
                        style='margin-top:0;margin-bottom:0;padding-top:0;padding-bottom:0;width:100%!important;background-color:#f4f5ff'>
                        <table width='100%' cellpadding='0' cellspacing='0' align='center' bgcolor='#f4f5ff'>
                           <tbody>
                              <tr>
                                 <td align='center' valign='top'>
                                    <table width='640' cellpadding='0' cellspacing='0' style='width:640px ; margin-top:20px'>
                                       <tbody>
                                          <tr>
                                             <td align='center' valign='top'>
                                                <table width='100%' cellpadding='0' cellspacing='0'>
                                                   <tbody>
                                                      <tr>
                                                         <td width='20'>&nbsp;</td>
                                                         <td align='center' valign='top'>
                                                            <table width='100%' cellpadding='0' cellspacing='0'
                                                               bgcolor='#fafbff'>
                                                               <tbody>
                                                                  <tr>
                                                                     <td height='20' style='line-height:1px;font-size:1px'>
                                                                        &nbsp;
                                                                     </td>
                                                                  </tr>
                                                                  <tr>
                                                                     <td align='center' valign='top'>
                                                                        <table width='100%' cellpadding='0' cellspacing='0'>
                                                                           <tbody>
                                                                              <tr>
                                                                                 <td width='40'>&nbsp;</td>
                                                                                 <td align='center' valign='top'>
                                                                                    <table width='100%' cellpadding='0'
                                                                                       cellspacing='0'>
                                                                                       <tbody>
                                                                                          <tr>
                                                                                             <td align='left'
                                                                                                valign='top'><a
                                                                                                href='{compDomain}'
                                                                                                target='_blank'><img
                                                                                                src='{logoSrc}'
                                                                                                width='120'
                                                                                                border='0'
                                                                                                style='width:120px;max-width:140px;height:auto;display:block'
                                                                                                alt='MNB Soft Solution'></a>
                                                                                             </td>
                                                                                             <td align='right'
                                                                                                valign='top'
                                                                                                style='Margin:0;font-family:roboto,helvetica,arial,sans-serif;line-height:21px;color:#673de6;font-size:14px;font-weight:bold'>
                                                                                             </td>
                                                                                          </tr>
                                                                                       </tbody>
                                                                                    </table>
                                                                                 </td>
                                                                                 <td width='40'>&nbsp;</td>
                                                                              </tr>
                                                                           </tbody>
                                                                        </table>
                                                                     </td>
                                                                  </tr>
                                                                  <tr>
                                                                     <td height='20' style='line-height:1px;font-size:1px'>
                                                                        &nbsp;
                                                                     </td>
                                                                  </tr>
                                                               </tbody>
                                                            </table>
                                                         </td>
                                                         <td width='20'>&nbsp;</td>
                                                      </tr>
                                                   </tbody>
                                                </table>
                                             </td>
                                          </tr>
                                       </tbody>
                                    </table>
                                 </td>
                              </tr>
                           </tbody>
                        </table>
                        <div>
                           <div>
                              <table width='100%' cellpadding='0' cellspacing='0' align='center' style='background-color:#f4f5ff; font-family:Google Sans,Roboto,Helvetica Neue,Helvetica,Arial,sans-serif; font-size:14px'>
                                 <tbody>
                                    <tr>
                                       <td align='center' valign='top'>
                                          <table width='640' cellpadding='0' cellspacing='0' style='width:640px'>
                                             <tbody>
                                                <tr>
                                                   <td align='center' valign='top'>
                                                      <table width='100%' cellpadding='0' cellspacing='0'>
                                                         <tbody>
                                                            <tr>
                                                               <td width='20' style='line-height:1px;font-size:1px'>&nbsp;</td>
                                                               <td align='center' valign='top'>
                                                                  <table width='100%' cellpadding='0' cellspacing='0'
                                                                     style='background-color:#fafbff'>
                                                                     <tbody>
                                                                        <tr>
                                                                           <td height='20'
                                                                              style='line-height:1px;font-size:1px'>&nbsp;
                                                                           </td>
                                                                        </tr>
                                                                        <tr>
                                                                           <td align='center' valign='top'>
                                                                              <table width='100%' cellpadding='0'
                                                                                 cellspacing='0'>
                                                                                 <tbody>
                                                                                    <tr>
                                                                                       <td width='40'>&nbsp;</td>
                                                                                       <td align='center' valign='top'>
                                                                                          <table width='100%'
                                                                                             cellpadding='0'
                                                                                             cellspacing='0'>
                                                                                             <tbody>
                                                                                                <tr>
                                                                                                   <td align='left'
                                                                                                      valign='top'
                                                                                                      style='font-family:'DM Sans',sans-serif,-apple-system,BlinkMacSystemFont,'Segoe UI',Roboto,Helvetica,Arial,sans-serif,'Apple Color Emoji','Segoe UI Emoji','Segoe UI Symbol';font-size:14px;line-height:20px;font-weight:400;font-style:normal;letter-spacing:normal;color:#2f1c6a'
                                                                                                      class='m_1741340122979844724txt_12'>
                                                                                                      Dear
                                                                                                      <strong>{userName}</strong>,<br><br>
                                                                                                      Registration is Successfull !!.<br>
                                                                                                     Welcome to the MNB Become a Partner Network.<br>
                                                                                                     Thank you for registering with MNB Become a Partner. Your application has been received successfully, and you are now one step closer to becoming a valued partner in our growth journey.
                                                                                                     <br>
                                                                                                     <br>
                                                                                                     {compName}.<br>
                                                                                                     <strong>Company Name</strong> : {companyName}.<br>
                                                                                                     <strong>Website</strong> : {website}<br>
                                                                                                     <strong>Partner Code : {partnercode}<br>
                                                                                                     <strong>Business Email</strong> : {businessemail}<br>
                                                                                                     <strong>Password</strong> : {password}<br><br><br>
                                                                                                      <b>Best Regards,</b><br>The
                                                                                                      {compName} <br>www.mnbsoft.com | ask@mnbsoft.com | +91 9311 221776
                                                                                                   </td>
                                                                                                </tr>
                                                                                             </tbody>
                                                                                          </table>
                                                                                       </td>
                                                                                       <td width='40'>&nbsp;</td>
                                                                                    </tr>
                                                                                 </tbody>
                                                                              </table>
                                                                           </td>
                                                                        </tr>
                                                                        <tr>
                                                                           <td height='20'
                                                                              style='line-height:1px;font-size:1px'>&nbsp;
                                                                           </td>
                                                                        </tr>
                                                                     </tbody>
                                                                  </table>
                                                               </td>
                                                               <td width='20' style='line-height:1px;font-size:1px'>&nbsp;</td>
                                                            </tr>
                                                         </tbody>
                                                      </table>
                                                   </td>
                                                </tr>
                                             </tbody>
                                          </table>
                                       </td>
                                    </tr>
                                 </tbody>
                              </table>
                           </div>
                        </div>
                        <table width='100%' cellpadding='0' cellspacing='0' align='center' bgcolor='#f4f5ff'>
                           <tbody>
                              <tr>
                                 <td align='center' valign='top'>
                                    <table width='640' cellpadding='0' cellspacing='0' style='width:640px'>
                                       <tbody>
                                          <tr>
                                             <td width='20'>&nbsp;</td>
                                             <td align='center' valign='top'>
                                                <table width='100%' cellpadding='0' cellspacing='0' bgcolor='#f4f5ff'>
                                                   <tbody>
                                                      <tr>
                                                         <td width='30'>&nbsp;</td>
                                                         <td align='center' valign='top'>
                                                            <table width='100%' cellpadding='0' cellspacing='0'>
                                                               <tbody>
                                                                  <tr>
                                                                     <td height='20' style='line-height:1px;font-size:1px'>
                                                                        &nbsp;
                                                                     </td>
                                                                  </tr>
                                                               </tbody>
                                                            </table>
                                                            <table width='100%' cellpadding='0' cellspacing='0'>
                                                               <tbody>
                                                                  <tr>
                                                                     <td align='left' valign='top'><a
                                                                        href='{compDomain}'
                                                                        target='_blank'><img
                                                                        src='{logoSrc}'
                                                                        border='0'
                                                                        style='width:60px;max-width:147px;height:auto;display:block'
                                                                        alt='{compDomain}'></a></td>
                                                                  </tr>
                                                               </tbody>
                                                            </table>
                                                             <table width='100%' cellpadding='0' cellspacing='0'>
                                                               <tbody>
                                                                  <tr>
                                                                     <td height='10' style='line-height:1px;font-size:1px'>
                                                                        &nbsp;
                                                                     </td>
                                                                  </tr>
                                                               </tbody>
                                                            </table>
                                                            <table width='100%' cellpadding='0' cellspacing='0' style='font-family:Google Sans,Roboto,Helvetica Neue,Helvetica,Arial,sans-serif;'>
                                                               <tbody>
                                                                  <tr></tr>
                                                                  <tr>
                                                                     <td
                                                                        style='font-family:'DM Sans',sans-serif,-apple-system,BlinkMacSystemFont,'Segoe UI',Roboto,Helvetica,Arial,sans-serif,'Apple Color Emoji','Segoe UI Emoji','Segoe UI Symbol';font-size:12px;line-height:14px;font-weight:400;color:#555555'>
                                                                        You have received this email because you are
                                                                        registered at  Mnb Soft Solution, to ensure the
                                                                        implementation of our Terms of Service and (or) for
                                                                        other legitimate matters.<br><br>
                                                                     </td>
                                                                  </tr>
                                                                  <tr>
                                                                     <td align='left' valign='top'
                                                                        style='font-family:'DM Sans',sans-serif,-apple-system,BlinkMacSystemFont,'Segoe UI',Roboto,Helvetica,Arial,sans-serif,'Apple Color Emoji','Segoe UI Emoji','Segoe UI Symbol';font-size:12px;line-height:14px;font-weight:400;color:#555555'
                                                                        class='m_1741340122979844724ftr'><a href='https://mnbsoft.com/privacy-policy'
                                                                        target='_blank'><span
                                                                        style='color:#555555;text-decoration:underline;display:inline-block'>Privacy Policy</span></a><br><br>2024-2025
                                                                        MNB Soft Solution
                                                                     </td>
                                                                  </tr>
                                                               </tbody>
                                                            </table>
                                                            <table width='100%' cellpadding='0' cellspacing='0'>
                                                               <tbody>
                                                                  <tr>
                                                                     <td height='40' style='line-height:1px;font-size:1px'>
                                                                        &nbsp;
                                                                     </td>
                                                                  </tr>
                                                               </tbody>
                                                            </table>
                                                         </td>
                                                         <td width='30'>&nbsp;</td>
                                                      </tr>
                                                   </tbody>
                                                </table>
                                             </td>
                                             <td width='20'>&nbsp;</td>
                                          </tr>
                                       </tbody>
                                    </table>
                                 </td>
                              </tr>
                           </tbody>
                        </table>
                        </div>";
                        _mailBodyHTML = _mailBodyHTML.Replace("{userName}", obj.fullName);
                        _mailBodyHTML = _mailBodyHTML.Replace("{userLoginID}", obj.partnerCode);
                        _mailBodyHTML = _mailBodyHTML.Replace("{logoSrc}", "https://mnbsoft.com/images/logo/black-logo.png");
                        _mailBodyHTML = _mailBodyHTML.Replace("{compName}", "Mnb Soft Solution");
                        _mailBodyHTML = _mailBodyHTML.Replace("{companyName}", obj.company);
                        _mailBodyHTML = _mailBodyHTML.Replace("{website}", obj.companyWebsite);
                        _mailBodyHTML = _mailBodyHTML.Replace("{partnercode}", obj.partnerCode);
                        _mailBodyHTML = _mailBodyHTML.Replace("{businessemail}", obj.businessEmail);
                        _mailBodyHTML = _mailBodyHTML.Replace("{password}", obj.password);
                        _mailBodyHTML = _mailBodyHTML.Replace("{compDomain}", "https://mnbsoft.com");
                        message.Body = _mailBodyHTML;
                        smtpClient.Host = "smtp.gmail.com"; //-- Donot change. "dedrelay.secureserver.net";
                        smtpClient.Port = 587; //--- Donot change
                        smtpClient.EnableSsl = true;//--- Donot change
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.Credentials = new System.Net.NetworkCredential("mnbsoftsolution@gmail.com", "lftqidmojhkbaiip");
                        smtpClient.Send(message);
                        return "Ok";
                    }
                    catch (Exception ex)
                    {
                        msg = ex.Message;
                        return msg;
                    }
                }
                catch (Exception)
                {
                    return "Failed !";
                }
            }
        }
        public async Task<string> SendAffiliateApproveMail(ModAffiliate obj)
        {
            using (var connection = new SqlConnection(_con.GetConnectionString()))
            {
                try
                {
                    MailMessage message = new MailMessage();
                    SmtpClient smtpClient = new SmtpClient();
                    string msg = string.Empty;
                    try
                    {

                        MailAddress fromAddress = new MailAddress("mnbsoftsolution@gmail.com");
                        message.From = fromAddress;
                        message.To.Add(obj.email.ToString());
                        message.Subject = "Mnb Soft Solution Credential";
                        message.IsBodyHtml = true;

                        string _mailBodyHTML = @"<div marginwidth='0' marginheight='0'
                        style='margin-top:0;margin-bottom:0;padding-top:0;padding-bottom:0;width:100%!important;background-color:#f4f5ff'>
                        <table width='100%' cellpadding='0' cellspacing='0' align='center' bgcolor='#f4f5ff'>
                           <tbody>
                              <tr>
                                 <td align='center' valign='top'>
                                    <table width='640' cellpadding='0' cellspacing='0' style='width:640px ; margin-top:20px'>
                                       <tbody>
                                          <tr>
                                             <td align='center' valign='top'>
                                                <table width='100%' cellpadding='0' cellspacing='0'>
                                                   <tbody>
                                                      <tr>
                                                         <td width='20'>&nbsp;</td>
                                                         <td align='center' valign='top'>
                                                            <table width='100%' cellpadding='0' cellspacing='0'
                                                               bgcolor='#fafbff'>
                                                               <tbody>
                                                                  <tr>
                                                                     <td height='20' style='line-height:1px;font-size:1px'>
                                                                        &nbsp;
                                                                     </td>
                                                                  </tr>
                                                                  <tr>
                                                                     <td align='center' valign='top'>
                                                                        <table width='100%' cellpadding='0' cellspacing='0'>
                                                                           <tbody>
                                                                              <tr>
                                                                                 <td width='40'>&nbsp;</td>
                                                                                 <td align='center' valign='top'>
                                                                                    <table width='100%' cellpadding='0'
                                                                                       cellspacing='0'>
                                                                                       <tbody>
                                                                                          <tr>
                                                                                             <td align='left'
                                                                                                valign='top'><a
                                                                                                href='{compDomain}'
                                                                                                target='_blank'><img
                                                                                                src='{logoSrc}'
                                                                                                width='120'
                                                                                                border='0'
                                                                                                style='width:120px;max-width:140px;height:auto;display:block'
                                                                                                alt='MNB Soft Solution'></a>
                                                                                             </td>
                                                                                             <td align='right'
                                                                                                valign='top'
                                                                                                style='Margin:0;font-family:roboto,helvetica,arial,sans-serif;line-height:21px;color:#673de6;font-size:14px;font-weight:bold'>
                                                                                             </td>
                                                                                          </tr>
                                                                                       </tbody>
                                                                                    </table>
                                                                                 </td>
                                                                                 <td width='40'>&nbsp;</td>
                                                                              </tr>
                                                                           </tbody>
                                                                        </table>
                                                                     </td>
                                                                  </tr>
                                                                  <tr>
                                                                     <td height='20' style='line-height:1px;font-size:1px'>
                                                                        &nbsp;
                                                                     </td>
                                                                  </tr>
                                                               </tbody>
                                                            </table>
                                                         </td>
                                                         <td width='20'>&nbsp;</td>
                                                      </tr>
                                                   </tbody>
                                                </table>
                                             </td>
                                          </tr>
                                       </tbody>
                                    </table>
                                 </td>
                              </tr>
                           </tbody>
                        </table>
                        <div>
                           <div>
                              <table width='100%' cellpadding='0' cellspacing='0' align='center' style='background-color:#f4f5ff; font-family:Google Sans,Roboto,Helvetica Neue,Helvetica,Arial,sans-serif; font-size:14px'>
                                 <tbody>
                                    <tr>
                                       <td align='center' valign='top'>
                                          <table width='640' cellpadding='0' cellspacing='0' style='width:640px'>
                                             <tbody>
                                                <tr>
                                                   <td align='center' valign='top'>
                                                      <table width='100%' cellpadding='0' cellspacing='0'>
                                                         <tbody>
                                                            <tr>
                                                               <td width='20' style='line-height:1px;font-size:1px'>&nbsp;</td>
                                                               <td align='center' valign='top'>
                                                                  <table width='100%' cellpadding='0' cellspacing='0'
                                                                     style='background-color:#fafbff'>
                                                                     <tbody>
                                                                        <tr>
                                                                           <td height='20'
                                                                              style='line-height:1px;font-size:1px'>&nbsp;
                                                                           </td>
                                                                        </tr>
                                                                        <tr>
                                                                           <td align='center' valign='top'>
                                                                              <table width='100%' cellpadding='0'
                                                                                 cellspacing='0'>
                                                                                 <tbody>
                                                                                    <tr>
                                                                                       <td width='40'>&nbsp;</td>
                                                                                       <td align='center' valign='top'>
                                                                                          <table width='100%'
                                                                                             cellpadding='0'
                                                                                             cellspacing='0'>
                                                                                             <tbody>
                                                                                                <tr>
                                                                                                   <td align='left'
                                                                                                      valign='top'
                                                                                                      style='font-family:'DM Sans',sans-serif,-apple-system,BlinkMacSystemFont,'Segoe UI',Roboto,Helvetica,Arial,sans-serif,'Apple Color Emoji','Segoe UI Emoji','Segoe UI Symbol';font-size:14px;line-height:20px;font-weight:400;font-style:normal;letter-spacing:normal;color:#2f1c6a'
                                                                                                      class='m_1741340122979844724txt_12'>
                                                                                                      Dear
                                                                                                      <strong>{userName}</strong>,<br><br>
                                                                                                      Registration is Successfull !!.<br>
                                                                                                     Welcome to the MNB Become a Affiliate Partner Network.<br>
                                                                                                     Thank you for registering with MNB Become a Affiliate Partner. Your application has been approved successfully, and you are now one step closer to becoming a valued affiliate partner in our growth journey.
                                                                                                     <br>
                                                                                                     <strong>Email</strong> : {email}<br>
                                                                                                     <strong>Password</strong> : {password}<br>
                                                                                                     <strong>Login Url</strong> : https://mnbsoft.com/affiliate-login <br><br><br>
                                                                                                      <b>Best Regards,</b><br>The
                                                                                                      {compName} <br>www.mnbsoft.com | ask@mnbsoft.com | +91 9311 221776
                                                                                                   </td>
                                                                                                </tr>
                                                                                             </tbody>
                                                                                          </table>
                                                                                       </td>
                                                                                       <td width='40'>&nbsp;</td>
                                                                                    </tr>
                                                                                 </tbody>
                                                                              </table>
                                                                           </td>
                                                                        </tr>
                                                                        <tr>
                                                                           <td height='20'
                                                                              style='line-height:1px;font-size:1px'>&nbsp;
                                                                           </td>
                                                                        </tr>
                                                                     </tbody>
                                                                  </table>
                                                               </td>
                                                               <td width='20' style='line-height:1px;font-size:1px'>&nbsp;</td>
                                                            </tr>
                                                         </tbody>
                                                      </table>
                                                   </td>
                                                </tr>
                                             </tbody>
                                          </table>
                                       </td>
                                    </tr>
                                 </tbody>
                              </table>
                           </div>
                        </div>
                        <table width='100%' cellpadding='0' cellspacing='0' align='center' bgcolor='#f4f5ff'>
                           <tbody>
                              <tr>
                                 <td align='center' valign='top'>
                                    <table width='640' cellpadding='0' cellspacing='0' style='width:640px'>
                                       <tbody>
                                          <tr>
                                             <td width='20'>&nbsp;</td>
                                             <td align='center' valign='top'>
                                                <table width='100%' cellpadding='0' cellspacing='0' bgcolor='#f4f5ff'>
                                                   <tbody>
                                                      <tr>
                                                         <td width='30'>&nbsp;</td>
                                                         <td align='center' valign='top'>
                                                            <table width='100%' cellpadding='0' cellspacing='0'>
                                                               <tbody>
                                                                  <tr>
                                                                     <td height='20' style='line-height:1px;font-size:1px'>
                                                                        &nbsp;
                                                                     </td>
                                                                  </tr>
                                                               </tbody>
                                                            </table>
                                                            <table width='100%' cellpadding='0' cellspacing='0'>
                                                               <tbody>
                                                                  <tr>
                                                                     <td align='left' valign='top'><a
                                                                        href='{compDomain}'
                                                                        target='_blank'><img
                                                                        src='{logoSrc}'
                                                                        border='0'
                                                                        style='width:60px;max-width:147px;height:auto;display:block'
                                                                        alt='{compDomain}'></a></td>
                                                                  </tr>
                                                               </tbody>
                                                            </table>
                                                             <table width='100%' cellpadding='0' cellspacing='0'>
                                                               <tbody>
                                                                  <tr>
                                                                     <td height='10' style='line-height:1px;font-size:1px'>
                                                                        &nbsp;
                                                                     </td>
                                                                  </tr>
                                                               </tbody>
                                                            </table>
                                                            <table width='100%' cellpadding='0' cellspacing='0' style='font-family:Google Sans,Roboto,Helvetica Neue,Helvetica,Arial,sans-serif;'>
                                                               <tbody>
                                                                  <tr></tr>
                                                                  <tr>
                                                                     <td
                                                                        style='font-family:'DM Sans',sans-serif,-apple-system,BlinkMacSystemFont,'Segoe UI',Roboto,Helvetica,Arial,sans-serif,'Apple Color Emoji','Segoe UI Emoji','Segoe UI Symbol';font-size:12px;line-height:14px;font-weight:400;color:#555555'>
                                                                        You have received this email because you are
                                                                        registered at  Mnb Soft Solution, to ensure the
                                                                        implementation of our Terms of Service and (or) for
                                                                        other legitimate matters.<br><br>
                                                                     </td>
                                                                  </tr>
                                                                  <tr>
                                                                     <td align='left' valign='top'
                                                                        style='font-family:'DM Sans',sans-serif,-apple-system,BlinkMacSystemFont,'Segoe UI',Roboto,Helvetica,Arial,sans-serif,'Apple Color Emoji','Segoe UI Emoji','Segoe UI Symbol';font-size:12px;line-height:14px;font-weight:400;color:#555555'
                                                                        class='m_1741340122979844724ftr'><a href='https://mnbsoft.com/privacy-policy'
                                                                        target='_blank'><span
                                                                        style='color:#555555;text-decoration:underline;display:inline-block'>Privacy Policy</span></a><br><br>2024-2025
                                                                        MNB Soft Solution
                                                                     </td>
                                                                  </tr>
                                                               </tbody>
                                                            </table>
                                                            <table width='100%' cellpadding='0' cellspacing='0'>
                                                               <tbody>
                                                                  <tr>
                                                                     <td height='40' style='line-height:1px;font-size:1px'>
                                                                        &nbsp;
                                                                     </td>
                                                                  </tr>
                                                               </tbody>
                                                            </table>
                                                         </td>
                                                         <td width='30'>&nbsp;</td>
                                                      </tr>
                                                   </tbody>
                                                </table>
                                             </td>
                                             <td width='20'>&nbsp;</td>
                                          </tr>
                                       </tbody>
                                    </table>
                                 </td>
                              </tr>
                           </tbody>
                        </table>
                        </div>";
                        _mailBodyHTML = _mailBodyHTML.Replace("{userName}", obj.firstName + obj.lastName);
                        _mailBodyHTML = _mailBodyHTML.Replace("{logoSrc}", "https://mnbsoft.com/images/logo/black-logo.png");
                        _mailBodyHTML = _mailBodyHTML.Replace("{compName}", "Mnb Soft Solution");
                        _mailBodyHTML = _mailBodyHTML.Replace("{email}", obj.email);
                        _mailBodyHTML = _mailBodyHTML.Replace("{password}", obj.password);
                        _mailBodyHTML = _mailBodyHTML.Replace("{compDomain}", "https://mnbsoft.com");
                        message.Body = _mailBodyHTML;
                        smtpClient.Host = "smtp.gmail.com";
                        smtpClient.Port = 587;
                        smtpClient.EnableSsl = true;
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.Credentials = new System.Net.NetworkCredential("mnbsoftsolution@gmail.com", "lftqidmojhkbaiip");
                        smtpClient.Send(message);
                        return "Ok";
                    }
                    catch (Exception ex)
                    {
                        msg = ex.Message;
                        return msg;
                    }
                }
                catch (Exception)
                {
                    return "Failed !";
                }
            }
        }
        public async Task<DataTable> AdminDashboard()
        {
            using (var connection = new SqlConnection(_con.GetConnectionString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    using (SqlCommand cmd = new SqlCommand("SP_AdminDashboard", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        sda.Fill(dt);
                        return dt;
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }
        }
        public async Task<bool> RankAdd(ModRank obj)
        {
            using (var connection = new SqlConnection(_con.GetConnectionString()))
            {
                try
                {
                    await connection.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("SP_AddRank", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@rankName", obj.rankName);
                        int i = await cmd.ExecuteNonQueryAsync();
                        if (i > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }
        }
        public async Task<bool> AddRankComission(ModComissionMaster obj)
        {
            using (var connection = new SqlConnection(_con.GetConnectionString()))
            {
                try
                {
                    await connection.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("SP_SetComission", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@productName", obj.productName);
                        cmd.Parameters.AddWithValue("@rankId", obj.rankId);
                        cmd.Parameters.AddWithValue("@comissionPer", obj.comissionPer);
                        int i = await cmd.ExecuteNonQueryAsync();
                        if (i > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }
        }
        public async Task<bool> SettlementUpdate(int id, int status, string ids)
        {
            using (var connection = new SqlConnection(_con.GetConnectionString()))
            {
                try
                {
                    await connection.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("SP_UpdateSattlement", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@status", status);
                        cmd.Parameters.AddWithValue("@selectedIds", ids);
                        int i = await cmd.ExecuteNonQueryAsync();
                        if (i > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }
        }
        public async Task<bool> AddAdminBank(ModBankDetails obj)
        {
            using (var connection = new SqlConnection(_con.GetConnectionString()))
            {
                try
                {
                    await connection.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("SP_AddAdminBank", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@bankName", obj.bankName);
                        cmd.Parameters.AddWithValue("@branch", obj.branch);
                        cmd.Parameters.AddWithValue("@accountHolder", obj.accountHolder);
                        cmd.Parameters.AddWithValue("@accountNo", obj.accountNo);
                        cmd.Parameters.AddWithValue("@accountType", obj.accountType);
                        cmd.Parameters.AddWithValue("@ifsc", obj.ifsc);
                        int i = await cmd.ExecuteNonQueryAsync();
                        if (i > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }
        }
        public async Task<string> UpdateAdminPass(string oldPass, string newPass, string confPass)
        {
            using (var connection = new SqlConnection(_con.GetConnectionString()))
            {
                string msg = "";
                DataTable dt = new DataTable();
                try
                {
                    using (SqlCommand cmd = new SqlCommand("SP_UpdateAdminPassword", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@oldPass", oldPass);
                        cmd.Parameters.AddWithValue("@newPass", newPass);
                        cmd.Parameters.AddWithValue("@confPass", confPass);
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        sda.Fill(dt);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            msg = Convert.ToString(dt.Rows[0]["mess"]);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    await connection.CloseAsync();
                }
                return msg;
            }
        }
        #endregion

        #region Partner Pannel
        public async Task<DataTable> PartnerDashboard(string email)
        {
            using (var connection = new SqlConnection(_con.GetConnectionString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    using (SqlCommand cmd = new SqlCommand("SP_PartnerDashboard", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@email", email);
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        sda.Fill(dt);
                        return dt;
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }
        }
        public async Task<bool> AddPartnerBank(ModBankDetails obj)
        {
            using (var connection = new SqlConnection(_con.GetConnectionString()))
            {
                try
                {
                    await connection.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("SP_AddUpdatePartnerBank", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@partnerCode", obj.partnerCode);
                        cmd.Parameters.AddWithValue("@bankName", obj.bankName);
                        cmd.Parameters.AddWithValue("@branch", obj.branch);
                        cmd.Parameters.AddWithValue("@accountHolder", obj.accountHolder);
                        cmd.Parameters.AddWithValue("@accountNo", obj.accountNo);
                        cmd.Parameters.AddWithValue("@accountType", obj.accountType);
                        cmd.Parameters.AddWithValue("@ifsc", obj.ifsc);
                        int i = await cmd.ExecuteNonQueryAsync();
                        if (i > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }
        }
        public async Task<DataTable> PartnerReport(string action, string partnerCode)
        {
            using (var connection = new SqlConnection(_con.GetConnectionString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    using (SqlCommand cmd = new SqlCommand("SP_GetPartnerReport", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@action", action);
                        cmd.Parameters.AddWithValue("@partnerCode", partnerCode);
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        sda.Fill(dt);
                        return dt;
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }
        }
        public async Task<string> UpdatePass(string partnerCode, string oldPass, string newPass, string confPass)
        {
            using (var connection = new SqlConnection(_con.GetConnectionString()))
            {
                string msg = "";
                DataTable dt = new DataTable();
                try
                {
                    using (SqlCommand cmd = new SqlCommand("SP_UpdatePassword", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@partnerCode", partnerCode);
                        cmd.Parameters.AddWithValue("@oldPass", oldPass);
                        cmd.Parameters.AddWithValue("@newPass", newPass);
                        cmd.Parameters.AddWithValue("@confPass", confPass);
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        sda.Fill(dt);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            msg = Convert.ToString(dt.Rows[0]["mess"]);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    await connection.CloseAsync();
                }
                return msg;
            }
        }
        public async Task<bool> UpdatePartnerProfile(ModPartner obj)
        {
            using (var connection = new SqlConnection(_con.GetConnectionString()))
            {
                try
                {
                    await connection.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("SP_UpdatePartnerProfile", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@partnerCode", obj.partnerCode);
                        cmd.Parameters.AddWithValue("@fullName", obj.fullName);
                        cmd.Parameters.AddWithValue("@country", obj.country);
                        cmd.Parameters.AddWithValue("@state", obj.state);
                        cmd.Parameters.AddWithValue("@city", obj.city);
                        cmd.Parameters.AddWithValue("@address", obj.address);
                        cmd.Parameters.AddWithValue("@whyBecomeReseller", obj.whyBecomeReseller);
                        cmd.Parameters.AddWithValue("@teamSize", obj.teamSize);
                        cmd.Parameters.AddWithValue("@branchOfficeLocation", obj.branchOfficeLocation);
                        cmd.Parameters.AddWithValue("@profileImage", obj.profileImage);
                        cmd.Parameters.AddWithValue("@action", obj.action);
                        int i = await cmd.ExecuteNonQueryAsync();
                        if (i > 0)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }
        }
        public async Task<bool> AddSalePurchase(ModSalePurchase obj)
        {
            using (var connection = new SqlConnection(_con.GetConnectionString()))
            {
                try
                {
                    await connection.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("SP_AddSalePurchase", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@partnerCode", obj.partnerCode);
                        cmd.Parameters.AddWithValue("@productName", obj.productName);
                        cmd.Parameters.AddWithValue("@planName", (object?)obj.planName ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@planType", (object?)obj.planType ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@serverName", (object?)obj.serverName ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@config", (object?)obj.config ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@noOfUser", obj.noOfUser);
                        cmd.Parameters.AddWithValue("@company", obj.company);
                        cmd.Parameters.AddWithValue("@mobile", obj.mobile);
                        cmd.Parameters.AddWithValue("@email", obj.email);
                        cmd.Parameters.AddWithValue("@amount", obj.amount);
                        //cmd.Parameters.AddWithValue("@commissionPer", obj.commissionPer);
                        cmd.Parameters.AddWithValue("@saleType", obj.saleType);
                        cmd.Parameters.AddWithValue("@remarks", (object?)obj.remarks ?? DBNull.Value);
                        int i = await cmd.ExecuteNonQueryAsync();
                        return i > 0;
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }
        }
        public async Task<bool> AddSettlement(ModSettlement obj)
        {
            using (var connection = new SqlConnection(_con.GetConnectionString()))
            {
                try
                {
                    await connection.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("SP_AddSettlement", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@partnerCode", obj.partnerCode);
                        cmd.Parameters.AddWithValue("@selectedIds", obj.selectedIds);
                        cmd.Parameters.AddWithValue("@totalAmount", obj.totalAmount);
                        cmd.Parameters.AddWithValue("@totalCommission", obj.totalCommission);
                        cmd.Parameters.AddWithValue("@payableAmount", obj.payableAmount);
                        cmd.Parameters.AddWithValue("@netPayableAmount", obj.netPayableAmount);
                        cmd.Parameters.AddWithValue("@gstPer", obj.gstPer);
                        cmd.Parameters.AddWithValue("@tdsPer", obj.tdsPer);
                        cmd.Parameters.AddWithValue("@utrNo", (object?)obj.utrNo);
                        cmd.Parameters.AddWithValue("@gstAmount", obj.gstAmount);
                        cmd.Parameters.AddWithValue("@tdsAmount", obj.tdsAmount);
                        cmd.Parameters.AddWithValue("@bankId", obj.bankId);
                        cmd.Parameters.AddWithValue("@walletUsed", obj.walletUsed);
                        int i = await cmd.ExecuteNonQueryAsync();
                        return i > 0;
                    }
                }
                catch (Exception ex)
                {
                    // log exception if needed
                    return false;
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }
        }
        #endregion

        #region Affiliate Pannel
        public async Task<DataTable> AffiliateDashboard(string partnerCode)
        {
            using (var connection = new SqlConnection(_con.GetConnectionString()))
            {
                DataTable dt = new DataTable();
                try
                {
                    using (SqlCommand cmd = new SqlCommand("SP_AffiliateDashboard", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@partnerCode", partnerCode);
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        sda.Fill(dt);
                        return dt;
                    }
                }
                catch (Exception ex)
                {
                    return null;
                }
                finally
                {
                    await connection.CloseAsync();
                }
            }
        }
        public async Task<string> UpdateAffiliatePass(string partnerCode, string oldPass, string newPass, string confPass)
        {
            using (var connection = new SqlConnection(_con.GetConnectionString()))
            {
                string msg = "";
                DataTable dt = new DataTable();
                try
                {
                    using (SqlCommand cmd = new SqlCommand("SP_UpdateAffiliatePassword", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@partnerCode", partnerCode);
                        cmd.Parameters.AddWithValue("@oldPass", oldPass);
                        cmd.Parameters.AddWithValue("@newPass", newPass);
                        cmd.Parameters.AddWithValue("@confPass", confPass);
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        sda.Fill(dt);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            msg = Convert.ToString(dt.Rows[0]["mess"]);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    await connection.CloseAsync();
                }
                return msg;
            }
        }
        #endregion
        public string GetDomain()
        {
            var request = _httpContextAccessor.HttpContext?.Request;
            return request != null ? $"{request.Scheme}://{request.Host}" : string.Empty;
        }
    }
}
