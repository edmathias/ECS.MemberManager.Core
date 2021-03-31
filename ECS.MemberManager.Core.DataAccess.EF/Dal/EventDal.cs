using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Data;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.EntityFrameworkCore;

namespace ECS.MemberManager.Core.DataAccess.EF
{
    public class EventDal : IEventDal
    {
        public async Task<List<Event>> Fetch()
        {
            List<Event> list;

            using (var context = new MembershipManagerDataContext())
            {
                list = await context.Events
                    .ToListAsync();
            }

            return list;
        }

        public async Task<Event> Fetch(int id)
        {
            List<Event> list = null;

            Event eventObj = null;

            using (var context = new MembershipManagerDataContext())
            {
                eventObj = await context.Events.Where(a => a.Id == id)
                    .FirstAsync();
            }

            return eventObj;
        }

        public async Task<Event> Insert(Event eventObjToInsert)
        {
            using (var context = new MembershipManagerDataContext())
            {
                await context.Events.AddAsync(eventObjToInsert);

                await context.SaveChangesAsync();
            }

            ;

            return eventObjToInsert;
        }

        public async Task<Event> Update(Event eventObjToUpdate)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Update(eventObjToUpdate);

                await context.SaveChangesAsync();
            }

            return eventObjToUpdate;
        }

        public async Task Delete(int id)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Remove(await context.Events.SingleAsync(a => a.Id == id));
                await context.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
        }
    }
}