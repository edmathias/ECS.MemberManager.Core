using System;
using System.Collections.Generic;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Dal
{
    public interface IOrganizationDal : IDisposable
    {
        List<Organization> Fetch();
        Organization Fetch(int id);
        int Insert(Organization organizationToInsert);
        int Update(Organization eMailToUpdate);
        void Delete(int id);
    }
}