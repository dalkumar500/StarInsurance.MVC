using StarInsurance.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarInsurance.Models
{
    
   public class InsurancePolicyDetail
    {
        public int InsurancePolicyId { get; set; }
        public int PolicyNumber { get; set; }

        public PolicyType TypeOfPolicy { get; set; }

        public DateTime DateOfPolicy { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }
        public DateTimeOffset? ModifiedUtc { get; set; }
        [Display(Name = "Created")]
        public DateTimeOffset CreatedUtc { get; set; }
        public  List<PolicyClaim> PolicyClaims { get; set; }
        public int? CustomerId { get; set; }
    }
}
