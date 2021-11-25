using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarInsurance.Data
{
    public enum PolicyType
    {
        Home = 1,
        Car,
        Heathinsurance,
        petinsurance
        
    }
    public class InsurancePolicy
    {
        [Key]
        public int InsurancePolicyId{ get; set; }
        [Required]
        public Guid OwnerId { get; set; }
        [Required]
        public PolicyType TypeOfPolicy { get; set; }
        [Required]
        public DateTime DateOfPolicy { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public int PolicyNumber { get; set; }
        public virtual List<PolicyClaim>PolicyClaims { get; set; }
        public virtual List<Customer> Customers { get; set; }
        [Required]
        
        [Display(Name = "Date Created")]
        public DateTimeOffset CreatedUtc { get; set; }

        [Display(Name = "Date Modified")]
        public DateTimeOffset? ModifiedUtc { get; set; }
        //one to many realtionship . One category can have many InsurancePolicy
        

    }
}
