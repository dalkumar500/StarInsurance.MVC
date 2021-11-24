using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarInsurance.Models
{
   public class InsurancePolicyEdit
    {
        public int InsurancePolicyId { get; set; }
        public  int PolicyNumber{ get; set; }

        public PolicyType TypeOfPolicy { get; set; }

        public DateTime DateOfPolicy { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }
    }
}
