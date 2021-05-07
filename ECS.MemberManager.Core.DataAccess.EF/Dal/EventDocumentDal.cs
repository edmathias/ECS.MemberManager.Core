using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Data;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.EntityFrameworkCore;

namespace ECS.MemberManager.Core.DataAccess.EF
{
    public class EventDocumentDal : IDal<EventDocument>
    {
        private MembershipManagerDataContext _context;

        public EventDocumentDal()
        {
            _context = new MembershipManagerDataContext();
        }

        public EventDocumentDal(MembershipManagerDataContext context)
        {
            _context = context;
        }

        public async Task<List<EventDocument>> Fetch()
        {
            return await _context.EventDocuments
                .Include(e => e.Event)
                .Include(e => e.DocumentType)
                .ToListAsync();
        }

        public async Task<EventDocument> Fetch(int id)
        {
            return await _context.EventDocuments.Where(a => a.Id == id)
                .Include(e => e.Event)
                .Include(e => e.DocumentType)
                .FirstAsync();
        }

        public async Task<EventDocument> Insert(EventDocument eventDocumentToInsert)
        {
            _context.Entry(eventDocumentToInsert.Event).State = EntityState.Unchanged;
            _context.Entry(eventDocumentToInsert.DocumentType).State = EntityState.Unchanged;

            await _context.EventDocuments.AddAsync(eventDocumentToInsert);

            await _context.SaveChangesAsync();

            return eventDocumentToInsert;
        }

        public async Task<EventDocument> Update(EventDocument eventDocumentToUpdate)
        {
            _context.Entry(eventDocumentToUpdate.Event).State = EntityState.Unchanged;
            _context.Entry(eventDocumentToUpdate.DocumentType).State = EntityState.Unchanged;

            _context.Update(eventDocumentToUpdate);

            await _context.SaveChangesAsync();

            return eventDocumentToUpdate;
        }

        public async Task Delete(int id)
        {
            _context.Remove(await _context.EventDocuments.SingleAsync(a => a.Id == id));
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
        }
    }
}