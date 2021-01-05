using System;
using System.Collections.Generic;
using System.Linq;
using Csla.Data.EntityFrameworkCore;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Data;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.EntityFrameworkCore;

namespace ECS.MemberManager.Core.DataAccess.SqlEF
{
    public class EMailDal : IEMailDal
    {
        public void Dispose()
        {
        }

        public List<EMail> Fetch()
        {
            var ctx = DbContextManager<MembershipManagerDataContext>.GetManager().DbContext;
            return ctx.EMails.ToList();
        }

        public EMail Fetch(int id)
        {
            var ctx = DbContextManager<MembershipManagerDataContext>.GetManager().DbContext;
            var emailDto = ctx.EMails.FirstOrDefault(e => e.Id == id);
            
            if(emailDto == null) throw new ArgumentException($"EMail id = {id} is not found");

            return emailDto;
        }

        public EMail Insert(EMail eMailToInsert)
        {
            var ctx = DbContextManager<MembershipManagerDataContext>.GetManager().DbContext;
            ctx.EMails.Add(eMailToInsert);
            var recordsAffected = ctx.SaveChanges();
            
            return eMailToInsert;
        }

        public EMail Update(EMail eMailToUpdate)
        {
            var ctx = DbContextManager<MembershipManagerDataContext>.GetManager().DbContext;
            ctx.EMails.Update(eMailToUpdate);

            int recordsAffected = ctx.SaveChanges();

            return eMailToUpdate;

        }

        public void Delete(int id)
        {
            var ctx = DbContextManager<MembershipManagerDataContext>.GetManager().DbContext;
            var emailToDelete = ctx.EMails.FirstOrDefault(e => e.Id == id);
            
            if(emailToDelete == null) throw new ArgumentException($"Email with id of {id} to delete was not found");

            ctx.EMails.Remove(emailToDelete);
            ctx.SaveChanges();
        }
    }
}