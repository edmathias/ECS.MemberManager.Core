using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Dal
{
    public interface ICategoryOfOrganizationDal : IDisposable
    {
        Task<CategoryOfOrganization> Fetch(int id);
        Task<List<CategoryOfOrganization>> Fetch();
        Task<CategoryOfOrganization> Insert(CategoryOfOrganization categoryOfOrganization);
        Task<CategoryOfOrganization> Update(CategoryOfOrganization categoryOfOrganization);
        Task Delete(int id);
    }
}