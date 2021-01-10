using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Dal
{
    public interface ICategoryOfOrganizationDal : IDisposable
    {
        CategoryOfOrganization Fetch(int id);
        Task<List<CategoryOfOrganization>> Fetch();
        CategoryOfOrganization Insert(CategoryOfOrganization categoryOfOrganization);
        CategoryOfOrganization Update(CategoryOfOrganization categoryOfOrganization );
        void Delete(int id);
    }
}