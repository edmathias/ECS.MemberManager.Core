using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Csla.Data.EntityFrameworkCore;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Data;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.SqlEF
{
    public class EMailTypeDal : IEMailTypeDal
    {

 
        public void Dispose()
        {
        }

        public List<EMailType> Fetch()
        {
            var ctx = DbContextManager<MembershipManagerDataContext>.GetManager().DbContext;
            var emailTypeList = ctx.EMailTypes.ToList();

            return emailTypeList;
        }

        public EMailType Fetch(int id)
        {
            using var ctx = DbContextManager<MembershipManagerDataContext>.GetManager().DbContext;
            var emailType = ctx.EMailTypes.Where( e => e.Id == id );

            return emailType.FirstOrDefault();
        }

        public int Insert(EMailType eMailTypeToInsert)
        {
            using var ctx = DbContextManager<MembershipManagerDataContext>.GetManager().DbContext;
            ctx.EMailTypes.Add(eMailTypeToInsert);
            var count = ctx.SaveChanges();
            if (count == 0)
            {
                throw new InvalidOperationException("EMailTypeDal Insert");
            }

            return eMailTypeToInsert.Id;
        }

        public int Update(EMailType eMailTypeToUpdate)
        {
            var ctx = DbContextManager<MembershipManagerDataContext>.GetManager().DbContext;
            var emailTypeUpdated = ctx.EMailTypes.Find(eMailTypeToUpdate.Id);
            emailTypeUpdated.Description = eMailTypeToUpdate.Description;
            emailTypeUpdated.Notes = eMailTypeToUpdate.Notes;
            ctx.EMailTypes.Update(emailTypeUpdated);
            
            var count = ctx.SaveChanges();
                
            if (count == 0)
            {
                throw new InvalidOperationException("EMailTypeDal Insert");
            }

            return count;
        }

        public void Delete(int id)
        {
            var ctx = DbContextManager<MembershipManagerDataContext>.GetManager().DbContext;
            var eMailTypeToDelete = ctx.EMailTypes.Find(id);
            if (eMailTypeToDelete == null)
            {
                throw new InvalidOperationException("EMailTypeDal Find on Delete");
            }
                
            ctx.EMailTypes.Remove(eMailTypeToDelete);
            var count = ctx.SaveChanges();
            if (count == 0)
            {
                throw new InvalidOperationException("EMailTypeDal Insert");
            }
        }
    }
}