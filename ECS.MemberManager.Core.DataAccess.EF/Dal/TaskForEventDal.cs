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
        private MembershipManagerDataContext _context;

        public TaskForEventDal()
        {
            _context = new MembershipManagerDataContext();
        }

        public TaskForEventDal(MembershipManagerDataContext context)
        {
            _context = context;
        }

        public async Task<List<TaskForEvent>> Fetch()
        {
            return await _context.TaskForEvents
                .Include(e => e.Event)
                .ToListAsync();
        }

        public async Task<TaskForEvent> Fetch(int id)
        {
            return await _context.TaskForEvents.Where(a => a.Id == id)
                .Include(e => e.Event)
                .FirstAsync();
        }

        public async Task<TaskForEvent> Insert(TaskForEvent paymentToInsert)
        {
            _context.Entry(paymentToInsert.Event).State = EntityState.Unchanged;

            await _context.TaskForEvents.AddAsync(paymentToInsert);

            await _context.SaveChangesAsync();

            return paymentToInsert;
        }

        public async Task<TaskForEvent> Update(TaskForEvent paymentToUpdate)
        {
            _context.Entry(paymentToUpdate.Event).State = EntityState.Unchanged;

            _context.Update(paymentToUpdate);

            await _context.SaveChangesAsync();

            return paymentToUpdate;
        }

        public async Task Delete(int id)
        {
            _context.Remove(await _context.TaskForEvents.SingleAsync(a => a.Id == id));

            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
        }
    }
}