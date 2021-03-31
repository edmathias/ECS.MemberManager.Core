using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Data;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.EntityFrameworkCore;

namespace ECS.MemberManager.Core.DataAccess.EF
{
    public class EventDocumentDal : IEventDocumentDal
    {
        public async Task<List<EventDocument>> Fetch()
        {
            List<EventDocument> list;

            using (var context = new MembershipManagerDataContext())
            {
                list = await context.EventDocuments
                    .Include(e => e.Event)
                    .Include(e => e.DocumentType)
                    .ToListAsync();
            }

            return list;
        }

        public async Task<EventDocument> Fetch(int id)
        {
            List<EventDocument> list = null;

            EventDocument eventDocument = null;

            using (var context = new MembershipManagerDataContext())
            {
                eventDocument = await context.EventDocuments.Where(a => a.Id == id)
                    .Include(e => e.Event)
                    .Include(e => e.DocumentType)
                    .FirstAsync();
            }

            return eventDocument;
        }

        public async Task<EventDocument> Insert(EventDocument eventDocumentToInsert)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Entry(eventDocumentToInsert.Event).State = EntityState.Unchanged;
                context.Entry(eventDocumentToInsert.DocumentType).State = EntityState.Unchanged;

                await context.EventDocuments.AddAsync(eventDocumentToInsert);

                await context.SaveChangesAsync();
            }

            return eventDocumentToInsert;
        }

        public async Task<EventDocument> Update(EventDocument eventDocumentToUpdate)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Entry(eventDocumentToUpdate.Event).State = EntityState.Unchanged;
                context.Entry(eventDocumentToUpdate.DocumentType).State = EntityState.Unchanged;

                context.Update(eventDocumentToUpdate);

                await context.SaveChangesAsync();
            }

            return eventDocumentToUpdate;
        }

        public async Task Delete(int id)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Remove(await context.EventDocuments.SingleAsync(a => a.Id == id));
                await context.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
        }
    }
}