using StarInsurance.Data;
using StarInsurance.Models;
using StarInsurance.MVC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
   public class InsurancePolicyService
    {
         private readonly Guid _userId;

            public InsurancePolicyService(Guid userId)
            {
                _userId = userId;
            }

            public bool CreateInsurancePolicy(InsurancePolicyCreate model)
            {
                var entity =
                    new InsurancePolicy()
                    {
                        OwnerId = _userId,
                        PolicyNumber= model.PolicyNumber,
                        DateOfPolicy = model.DateOfPolicy,
                        Name         = model.Name,
                        Address      = model.Address,
                        TypeOfPolicy = (StarInsurance.Data.PolicyType)model.TypeOfPolicy,
                        CreatedUtc = DateTimeOffset.Now

                    };

                using (var ctx = new ApplicationDbContext())
                {
                    ctx.InsurancePolicies.Add(entity);
                    return ctx.SaveChanges() == 1;
                }
            }

            public IEnumerable<InsurancePolicyListItem> GetInsurancePolicy()
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var query =
                        ctx
                        .InsurancePolicies
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                            new InsurancePolicyListItem
                            {
                                InsurancePolicyId=e.InsurancePolicyId,
                                PolicyNumber =e.PolicyNumber,
                                DateOfPolicy = e.DateOfPolicy,
                                Name = e.Name,
                                Address = e.Address,
                                CreatedUtc = e.CreatedUtc

                            }
                            );
                    return query.ToArray();
                }
            }

            public InsurancePolicyDetail GetInsurancePolicyById(int id)
            {
                using (var ctx = new ApplicationDbContext())
                {
                var InsuranceClaims =
                    ctx
                    .InsurancePolicies
                    .Include("PolicyClaims");
                     var entity=InsuranceClaims
                     .Single(e => e.InsurancePolicyId == id && e.OwnerId == _userId);
                return
                    new InsurancePolicyDetail
                    {
                        InsurancePolicyId = entity.InsurancePolicyId,
                        DateOfPolicy = entity.DateOfPolicy,
                        Name = entity.Name,
                        Address = entity.Address,
                        ModifiedUtc = entity.ModifiedUtc,
                        PolicyClaims = ctx.PolicyClaims.Where(i => i.InsurancePolicyId == id).ToList()

                        };
                }
            }

            public bool UpdateNote(InsurancePolicyEdit model)
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var entity =
                        ctx
                        .InsurancePolicies
                        .Single(e => e.InsurancePolicyId == model.InsurancePolicyId && e.OwnerId == _userId);
                    
                    entity.InsurancePolicyId = model.InsurancePolicyId;
                    entity.Name = model.Name;
                    entity.Address = model.Address;
                    entity.PolicyNumber = model.PolicyNumber;
                    entity.ModifiedUtc = DateTimeOffset.UtcNow;
                  
                return ctx.SaveChanges() == 1;
                }
            }

            public bool DeleteInsurancePolicy(int InsurancePolicyId)
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var entity =
                        ctx
                        .InsurancePolicies
                        .Single(e => e.InsurancePolicyId == InsurancePolicyId && e.OwnerId == _userId);

                    ctx.InsurancePolicies.Remove(entity);

                    return ctx.SaveChanges() == 1;
                }
            }
        }
    }

