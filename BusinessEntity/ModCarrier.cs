using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntity
{
    public class ModCarrier
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }
        public string yearOfGraduation { get; set; }
        public string gender { get; set; }
        public string yearOfExperience { get; set; }
        public string currentEmployer { get; set; }
        public string currentCtc { get; set; }
        public string expectedCtc { get; set; }
        public string noticePeriod { get; set; }
        public string skillSet { get; set; }
        public string uploadResume { get; set; }
        public string otp { get; set; }
        public int status { get; set; }
        public DateTime createDate { get; set; }
    }
}
