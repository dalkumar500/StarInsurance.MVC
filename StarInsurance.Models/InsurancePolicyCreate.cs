using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarInsurance.Models
{
    public enum PolicyType
    {
        Home = 1,
        Car,
        Heathinsurance,
        petinsurance

    }
    public class InsurancePolicyCreate
    {
        
        public PolicyType TypeOfPolicy { get; set; }
        public DateTime DateOfPolicy { get; set; }
        public int PolicyNumber { get; set; }

        public string Name { get; set; }
       
        public string Address { get; set; }
        
    }
}
