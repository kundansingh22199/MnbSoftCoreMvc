using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity
{
    public class ModComissionMaster
    {
        public int id { get; set; }
        public int rankId { get; set; }
        public string rankName { get; set; }
        public string productName { get; set; }
        public decimal comissionPer { get; set; }
        public int status { get; set; }
        public DateTime createDate { get; set; }
    }
}
