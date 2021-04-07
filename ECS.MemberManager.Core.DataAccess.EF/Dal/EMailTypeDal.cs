using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Data;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.EntityFrameworkCore;

namespace ECS.MemberManager.Core.DataAccess.EF
{
    public class EMailTypeDal : IDal<EMailType>
    {
        public async Task<List<EMailType>> Fetch()
        {
            List<EMailType> list;

            using (var context = new MembershipManagerDataContext())
            {
                list = await context.EMailTypes
                    .ToListAsync();
            }

            return list;
        }

        public async Task<EMailType> Fetch(int id)
        {
            List<EMailType> list = null;

            EMailType emailType = null;

            using (var context = new MembershipManagerDataContext())
            {
                emailType = await context.EMailTypes.Where(a => a.Id == id)
                    .FirstAsync();
            }

            return emailType;
        }

        public async Task<EMailType> Insert(EMailType emailTypeToInsert)
        {
            using (var context = new MembershipManagerDataContext())
            {
                await context.EMailTypes.AddAsync(emailTypeToInsert);

                await context.SaveChangesAsync();
            }

            ;

            return emailTypeToInsert;
        }

        public async Task<EMailType> Update(EMailType emailTypeToUpdate)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Update(emailTypeToUpdate);

                await context.SaveChangesAsync();
            }

            return emailTypeToUpdate;
        }

        public async Task Delete(int id)
        {
            using (var context = new MembershipManagerDataContext())
            {
                context.Remove(await context.EMailTypes.SingleAsync(a => a.Id == id));
                await context.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
        }
    }
}