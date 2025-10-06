using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity
{
    public class ModContact
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string subject { get; set; }
        public string message { get; set; }
        public string otp { get; set; }
        public int status { get; set; }
        public DateTime createDate { get; set; }
    }
}
