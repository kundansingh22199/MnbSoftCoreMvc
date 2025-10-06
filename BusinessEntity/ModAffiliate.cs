using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity
{
    public class ModAffiliate
    {
        public int id { get; set; }
        public string partnerCode { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string country { get; set; }
        public string subject { get; set; }
        public string otp { get; set; }
        public string password { get; set; }
        public string profileImage { get; set; }
        public int status { get; set; }
        public DateTime createDate { get; set; }
    }
}
