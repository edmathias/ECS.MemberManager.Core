using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Data;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.EntityFrameworkCore;

namespace ECS.MemberManager.Core.DataAccess.EF
{
    public class EventDal : IDal<Event>
    {
        private MembershipManagerDataContext _context;

        public EventDal()
        {
            _context = new MembershipManagerDataContext();
        }

        public EventDal(MembershipManagerDataContext context)
        {
            _context = context;
        }

        public async Task<List<Event>> Fetch()
        {
            return await _context.Events
                .ToListAsync();
        }

        public async Task<Event> Fetch(int id)
        {
            return await _context.Events.Where(a => a.Id == id)
                .FirstAsync();
        }

        public async Task<Event> Insert(Event eventObjToInsert)
        {
            await _context.Events.AddAsync(eventObjToInsert);

            await _context.SaveChangesAsync();

            return eventObjToInsert;
        }

        public async Task<Event> Update(Event eventObjToUpdate)
        {
            _context.Update(eventObjToUpdate);

            await _context.SaveChangesAsync();

            return eventObjToUpdate;
        }

        public async Task Delete(int id)
        {
            _context.Remove(await _context.Events.SingleAsync(a => a.Id == id));
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
        }
    }
}