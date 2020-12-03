using System;
using System.Collections.Generic;
using System.Linq;
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
            using var ctx = DbContextManager<MembershipManagerDataContext>.GetManager();
            var emailTypeList = ctx.DbContext.EMailTypes.ToList();

            return emailTypeList;
        }

        public EMailType Fetch(int id)
        {
            using var ctx = DbContextManager<MembershipManagerDataContext>.GetManager();
            var emailType = ctx.DbContext.EMailTypes.Where( e => e.Id == id );

            return emailType.FirstOrDefault();
        }

        public int Insert(EMailType eMailTypeToInsert)
        {
            using var ctx = DbContextManager<MembershipManagerDataContext>.GetManager();
            ctx.DbContext.EMailTypes.Add(eMailTypeToInsert);
            var count = ctx.DbContext.SaveChanges();
            if (count == 0)
            {
                throw new InvalidOperationException("EMailTypeDal Insert");
            }

            return eMailTypeToInsert.Id;
        }

        public void Update(EMailType eMailTypeToUpdate)
        {
            using var ctx = DbContextManager<MembershipManagerDataContext>.GetManager();
            ctx.DbContext.EMailTypes.Update(eMailTypeToUpdate);

            var count = ctx.DbContext.SaveChanges();
                
            if (count == 0)
            {
                throw new InvalidOperationException("EMailTypeDal Insert");
            }
        }

        public void Delete(int id)
        {
            using var ctx = DbContextManager<MembershipManagerDataContext>.GetManager();
            var eMailTypeToDelete = ctx.DbContext.EMailTypes.Find(id);
            if (eMailTypeToDelete == null)
            {
                throw new InvalidOperationException("EMailTypeDal Find on Delete");
            }
                
            ctx.DbContext.EMailTypes.Remove(eMailTypeToDelete);
            var count = ctx.DbContext.SaveChanges();
            if (count == 0)
            {
                throw new InvalidOperationException("EMailTypeDal Insert");
            }
        }
    }
}