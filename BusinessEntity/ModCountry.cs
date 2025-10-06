using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity
{
    public class ModCountry
    {
        public string id { get; set; }
        public string shortname { get; set; }
        public string name { get; set; }
        public string phonecode { get; set; }
    }
    public class ModState
    {
        public string id { get; set; }
        public string name { get; set; }
        public string country_id { get; set; }
    }
    public class ModCity
    {
        public string id { get; set; }
        public string name { get; set; }
        public string state_id { get; set; }
    }
}
