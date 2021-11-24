using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarInsurance.Data
{
  public  class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        [Required]
        public Guid OwnerId { get; set; }
        [Required]
        [Display(Name="FirstName") ]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName{ get; set; }
       
        [Required]
        [Display(Name = "FullName")]
        public string FullName { get; set; }
        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }
       
        [Required]
        [Display(Name = "PhoneNumber")]
        public int PhoneNumber { get; set; }
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [ForeignKey(nameof(InsurancePolicy))]
        public int? InsurancePolicyId { get; set; }
        public virtual InsurancePolicy InsurancePolicy { get; set; }
        [Required]
        [Display(Name = "Date Created")]

        public DateTimeOffset CreatedUtc { get; set; }

        [Display(Name = "Date Modified")]
        public DateTimeOffset? ModifiedUtc { get; set; }
        //one to many realtionship . One category can have many customers


    }
}
