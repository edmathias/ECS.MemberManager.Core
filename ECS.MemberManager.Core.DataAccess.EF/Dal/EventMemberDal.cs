using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Data;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.EntityFrameworkCore;

namespace ECS.MemberManager.Core.DataAccess.EF
{
    public class EventMemberDal : IDal<EventMember>
    {
        private MembershipManagerDataContext _context;

        public EventMemberDal()
        {
            _context = new MembershipManagerDataContext();
        }

        public EventMemberDal(MembershipManagerDataContext context)
        {
            _context = context;
        }

        public async Task<List<EventMember>> Fetch()
        {
            return await _context.EventMembers
                .Include(e => e.Event)
                .Include(e => e.MemberInfo)
                .ToListAsync();
        }

        public async Task<EventMember> Fetch(int id)
        {
            return await _context.EventMembers.Where(a => a.Id == id)
                .Include(e => e.Event)
                .Include(e => e.MemberInfo)
                .FirstAsync();
        }

        public async Task<EventMember> Insert(EventMember eventMemberToInsert)
        {
            _context.Entry(eventMemberToInsert.Event).State = EntityState.Unchanged;
            _context.Entry(eventMemberToInsert.MemberInfo).State = EntityState.Unchanged;

            await _context.EventMembers.AddAsync(eventMemberToInsert);

            await _context.SaveChangesAsync();

            return eventMemberToInsert;
        }

        public async Task<EventMember> Update(EventMember eventMemberToUpdate)
        {
            _context.Entry(eventMemberToUpdate.Event).State = EntityState.Unchanged;
            _context.Entry(eventMemberToUpdate.MemberInfo).State = EntityState.Unchanged;

            _context.Update(eventMemberToUpdate);

            await _context.SaveChangesAsync();

            return eventMemberToUpdate;
        }

        public async Task Delete(int id)
        {
            _context.Remove(await _context.EventMembers.SingleAsync(a => a.Id == id));
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
        }
    }
}