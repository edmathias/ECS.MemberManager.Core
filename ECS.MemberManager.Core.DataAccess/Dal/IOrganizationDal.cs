using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Dal
{
    public interface IOrganizationDal : IDisposable
    {
        Task<List<Organization>> Fetch();
        Task<Organization> Fetch(int id);
        Task<Organization> Insert(Organization organizationToInsert);
        Task<Organization> Update(Organization eMailToUpdate);
        Task Delete(int id);
    }
}