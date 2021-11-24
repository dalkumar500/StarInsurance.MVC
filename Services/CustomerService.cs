using StarInsurance.MVC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
   public class CustomerService
    {
        
            private readonly Guid _userId;

            public CustomerService(Guid userId)
            {
                _userId = userId;
            }

            public bool CreateNote(CustomerCreate model)
            {
                var entity =
                    new ()
                    {
                        OwnerId = _userId,
                        Title = model.Title,
                        Content = model.Content,
                        CreatedUtc = DateTimeOffset.Now
                    };

                using (var ctx = new ApplicationDbContext())
                {
                    ctx.Notes.Add(entity);
                    return ctx.SaveChanges() == 1;
                }
            }

            public IEnumerable<CustomerListItem> GetNotes()
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var query =
                        ctx
                        .Notes
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                            new CustomerListItem
                            {
                                NoteId = e.NoteId,
                                Title = e.Title,
                                IsStarred = e.IsStarred,
                                CreatedUtc = e.CreatedUtc
                            }
                            );
                    return query.ToArray();
                }
            }

            public CustomerDetail GetNoteById(int id)
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var entity =
                        ctx
                        .Customers
                        .Single(e => e.NoteId == id && e.OwnerId == _userId);
                    return
                        new CustomerDetail
                        {
                            NoteId = entity.NoteId,
                            Title = entity.Title,
                            Content = entity.Content,
                            CreatedUtc = entity.CreatedUtc,
                            ModifiedUtc = entity.ModifiedUtc
                        };
                }
            }

            public bool UpdateNote(CustomerEdit model)
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var entity =
                        ctx
                        .Customers
                        .Single(e => e.CustomerId == model.CustomerId && e.OwnerId == _userId);

                    entity.Title = model.Title;
                    entity.Content = model.Content;
                    entity.ModifiedUtc = DateTimeOffset.UtcNow;
                    entity.IsStarred = model.IsStarred;


                    return ctx.SaveChanges() == 1;
                }
            }

            public bool DeleteNote(int noteId)
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var entity =
                        ctx
                        .Notes
                        .Single(e => e.NoteId == noteId && e.OwnerId == _userId);

                    ctx.Notes.Remove(entity);

                    return ctx.SaveChanges() == 1;
                }
            }
        }
    }
}
}
