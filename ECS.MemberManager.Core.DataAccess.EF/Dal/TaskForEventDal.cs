using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Data;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.EntityFrameworkCore;

namespace ECS.MemberManager.Core.DataAccess.EF
{
    public class TaskForEventDal : IDal<TaskForEvent>
    {
        public async Task<List<TaskForEvent>> Fetch()
        {
            List<TaskForEvent> list;

            using (var context = new MembershipManagerDataContext())
            {
                list = await context.TaskForEvents
                    .Include(e => e.Event)
                    .ToListAsync();
            }

            return list;
        }

        public async Task<TaskForEvent> Fetch(int id)
        {
            TaskForEvent payment = null;

            using (var context = new MembershipManagerDataContext())
            {
                payment = await context.TaskForEvents.Where(a => a.Id == id)
                    .Include(e => e.Event)
                    .FirstAsync();
            }

            return payment;
        }

        public async Task<TaskForEvent> Insert(TaskForEvent paymentToInsert)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Entry(paymentToInsert.Event).State = EntityState.Unchanged;

                await context.TaskForEvents.AddAsync(paymentToInsert);

                await context.SaveChangesAsync();
            }

            return paymentToInsert;
        }

        public async Task<TaskForEvent> Update(TaskForEvent paymentToUpdate)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Entry(paymentToUpdate.Event).State = EntityState.Unchanged;

                context.Update(paymentToUpdate);

                await context.SaveChangesAsync();
            }

            return paymentToUpdate;
        }

        public async Task Delete(int id)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Remove(await context.TaskForEvents.SingleAsync(a => a.Id == id));
                
                await context.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
        }
    }
}