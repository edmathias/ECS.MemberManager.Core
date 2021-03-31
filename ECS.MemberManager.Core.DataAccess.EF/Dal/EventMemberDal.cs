using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Data;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.EntityFrameworkCore;

namespace ECS.MemberManager.Core.DataAccess.EF
{
    public class EventMemberDal : IEventMemberDal
    {
        public async Task<List<EventMember>> Fetch()
        {
            List<EventMember> list;

            using (var context = new MembershipManagerDataContext())
            {
                list = await context.EventMembers
                    .Include(e => e.Event)
                    .Include(e => e.MemberInfo)
                    .ToListAsync();
            }

            return list;
        }

        public async Task<EventMember> Fetch(int id)
        {
            List<EventMember> list = null;

            EventMember eventMember = null;

            using (var context = new MembershipManagerDataContext())
            {
                eventMember = await context.EventMembers.Where(a => a.Id == id)
                    .Include(e => e.Event)
                    .Include(e => e.MemberInfo)
                    .FirstAsync();
            }

            return eventMember;
        }

        public async Task<EventMember> Insert(EventMember eventMemberToInsert)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Entry(eventMemberToInsert.Event).State = EntityState.Unchanged;
                context.Entry(eventMemberToInsert.MemberInfo).State = EntityState.Unchanged;

                await context.EventMembers.AddAsync(eventMemberToInsert);

                await context.SaveChangesAsync();
            }

            return eventMemberToInsert;
        }

        public async Task<EventMember> Update(EventMember eventMemberToUpdate)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Entry(eventMemberToUpdate.Event).State = EntityState.Unchanged;
                context.Entry(eventMemberToUpdate.MemberInfo).State = EntityState.Unchanged;

                context.Update(eventMemberToUpdate);

                await context.SaveChangesAsync();
            }

            return eventMemberToUpdate;
        }

        public async Task Delete(int id)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Remove(await context.EventMembers.SingleAsync(a => a.Id == id));
                await context.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
        }
    }
}