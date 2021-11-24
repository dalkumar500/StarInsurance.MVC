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
  public  class ClaimService
    {
        private readonly Guid _userId;

            public ClaimService(Guid userId)
            {
                _userId = userId;
            }

            public bool CreateClaim(ClaimCreate model)
            {
                var entity =
                    new PolicyClaim()
                    {
                        OwnerId = _userId,
                        ClaimType = model.ClaimType,
                        Description = model.Description,
                        ClaimAmount = model.ClaimAmount,
                        DateOfIncident = model.DateOfIncident,
                        DateOfClaim = model.DateOfClaim,
                        IsValid = model.IsValid,
                        CreatedUtc = DateTimeOffset.Now,
                        InsurancePolicyId = model.InsurancePolicyId
                    };

                using (var ctx = new ApplicationDbContext())
                {
                    ctx.PolicyClaims.Add(entity);
                    return ctx.SaveChanges() == 1;
                }
            }

            public IEnumerable<ClaimListItem> Getclaims()
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var query =
                        ctx
                        .PolicyClaims
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                            new ClaimListItem
                            {   ClaimId =e.ClaimId,
                                 ClaimType = e.ClaimType,
                                Description = e.Description,
                                ClaimAmount = e.ClaimAmount,
                                DateOfIncident = e.DateOfIncident,
                                DateOfClaim = e.DateOfClaim,
                                IsValid = e.IsValid,
                                CreatedUtc = e.CreatedUtc,
                             InsurancePolicyId = e.InsurancePolicy.InsurancePolicyId

                            }
                            );
                    return query.ToArray();
                }
            }

            public ClaimDetail GetClaimById(int id)
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var entity =
                        ctx
                        .PolicyClaims
                        .Single(e => e.ClaimId == id && e.OwnerId == _userId);
                    return
                        new ClaimDetail
                        {   
                            ClaimId = entity.ClaimId,
                            ClaimType = entity.ClaimType,
                            Description = entity.Description,
                            ClaimAmount = entity.ClaimAmount,
                            DateOfIncident = entity.DateOfIncident,
                            DateOfClaim = entity.DateOfClaim,
                            IsValid = entity.IsValid,
                            ModifiedUtc = entity.ModifiedUtc,
                            InsurancePolicyId = entity.InsurancePolicyId


                        };
                }
            }

            public bool UpdateClaim(ClaimEdit model)
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var entity =
                        ctx
                        .PolicyClaims
                        .Single(e => e.ClaimId == model.ClaimId && e.OwnerId == _userId);

                entity.ClaimType = model.ClaimType;
                entity.Description = model.Description;
                entity.ClaimAmount = model.ClaimAmount;
                entity. DateOfIncident = model.DateOfIncident;
                entity.DateOfClaim = model.DateOfClaim;
                entity.IsValid = model.IsValid;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;
                entity.InsurancePolicyId = model.InsurancePolicyId;



                return ctx.SaveChanges() == 1;
                }
            }

            public bool DeleteClaim(int ClaimId)
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var entity =
                        ctx
                        .PolicyClaims
                        .Single(e => e.ClaimId == ClaimId && e.OwnerId == _userId);

                    ctx.PolicyClaims.Remove(entity);

                    return ctx.SaveChanges() == 1;
                }
            }
        }
    }


