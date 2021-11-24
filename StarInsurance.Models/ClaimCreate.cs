using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarInsurance.Models
{
   public class ClaimCreate
    {
        public int InsurancePolicyId { get; set; }
        public string ClaimType { get; set; }
        public string Description { get; set; }
        public int ClaimAmount { get; set; }
        public DateTime DateOfIncident { get; set; }
        public DateTime DateOfClaim { get; set; }
        public bool IsValid { get; set; }
    }
}
