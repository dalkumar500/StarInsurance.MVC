using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarInsurance.Data
{
  public class PolicyClaim
    {  
        [Key]
        public int ClaimId { get; set; }
        [Required]
        public Guid OwnerId { get; set; }
        [Required]
        public string ClaimType { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int ClaimAmount { get; set; }
        [Required]
        public DateTime DateOfIncident { get; set; }
        [Required]
        public DateTime DateOfClaim { get; set; }
        [Required]
        public bool IsValid { get; set; }
       
        [ForeignKey(nameof(InsurancePolicy))]
        public int? InsurancePolicyId { get; set; }
        public virtual InsurancePolicy InsurancePolicy { get; set; }
        [Required]
        [Display(Name = "Date Created")]

        public DateTimeOffset CreatedUtc { get; set; }

        [Display(Name = "Date Modified")]
        public DateTimeOffset? ModifiedUtc { get; set; }
        //one to many realtionship . One category can have many claims

    }
}
