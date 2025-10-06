using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity
{
    public class ModComission
    {
        public int id { get; set; }
        public string partnerCode { get; set; }
        public string fullName { get; set; }
        public string businessEmail { get; set; }
        public decimal totalAmount { get; set; }
        public decimal comissionPer { get; set; }
        public decimal comission { get; set; }
        public int status { get; set; }
        public DateTime createDate { get; set; }
        public DateTime updateDate { get; set; }
    }
}
