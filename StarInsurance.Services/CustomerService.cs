using StarInsurance.Data;
using StarInsurance.Models;
using StarInsurance.MVC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarInsurance.Services
{
   public class CustomerService
    {
        
         private readonly Guid _userId;

            public CustomerService(Guid userId)
            {
                _userId = userId;
            }

            public bool CreateCustomer(CustomerCreate model)
            {
                var entity =
                    new Customer()
                    {
                        OwnerId = _userId,
                        Email =  model.Email,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        FullName = model.FullName,
                        Address = model.Address,
                        PhoneNumber = model.PhoneNumber,
                        CreatedUtc = DateTimeOffset.Now,
                        InsurancePolicyId=model.InsurancePolicyId

                    };

                using (var ctx = new ApplicationDbContext())
                {
                    ctx.Customers.Add(entity);
                return ctx.SaveChanges()==1;
                }
            }

            public IEnumerable<CustomerListItem> GetCustomers()
            {
                using (var ctx = new ApplicationDbContext())
                {
                var query =
                    ctx
                    .Customers
                    .Where(e => e.OwnerId == _userId)
                    .Select(
                        e =>
                        new CustomerListItem
                        {
                            CustomerId = e.CustomerId,
                            FirstName = e.FirstName,
                            LastName = e.LastName,
                            FullName = e.FullName,
                            Address = e.Address,
                            PhoneNumber = e.PhoneNumber,
                            Email = e.Email,
                            CreatedUtc = e.CreatedUtc,
                            InsurancePolicyId = e.InsurancePolicy.InsurancePolicyId

                        }
                        ) ;
                    return query.ToArray();
                }
            }

            public CustomerDetail GetCustomerById(int id)
            {
                using (var ctx = new ApplicationDbContext())
                {
                 var  entity=
                    ctx
                    .Customers
                    .Single(e => e.CustomerId == id && e.OwnerId == _userId);
                    return
                        new CustomerDetail
                        {   
                            CustomerId=entity.CustomerId,
                            FirstName = entity.FirstName,
                            LastName = entity.LastName,
                            FullName = entity.FullName,
                            Address = entity.Address,
                            PhoneNumber = entity.PhoneNumber,
                            Email = entity.Email,
                            ModifiedUtc = entity.ModifiedUtc,
                            InsurancePolicyId=entity.InsurancePolicyId,
                           

                        };
                }
            }

            public bool UpdateCustomer(CustomerEdit model)
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var entity =
                        ctx
                        .Customers
                        .Single(e => e.CustomerId == model.CustomerId && e.OwnerId == _userId);

                            entity.FirstName = model.FirstName;
                            entity.LastName = model.LastName;
                            entity.FullName = model.FullName;
                            entity.Address = model.Address;
                            entity.PhoneNumber = model.PhoneNumber;
                            entity.Email = model.Email;
                            entity.ModifiedUtc = DateTimeOffset.UtcNow;
                            entity.InsurancePolicyId = model.InsurancePolicyId;

                return ctx.SaveChanges() == 1;
                }
            }

            public bool DeleteCustomer(int customerId)
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var entity =
                        ctx
                        .Customers
                        .Single(e => e.CustomerId == customerId && e.OwnerId == _userId);

                    ctx.Customers.Remove(entity);

                    return ctx.SaveChanges() == 1;
                }
            }
        }
    }


