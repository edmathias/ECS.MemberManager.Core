using System;
using System.Collections.Generic;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Dal
{
    public interface ICategoryOfOrganizationDal : IDisposable
    {
        CategoryOfOrganization Fetch(int id);
        List<CategoryOfOrganization> Fetch();
        int Insert(CategoryOfOrganization categoryOfOrganization);
        void Update(CategoryOfOrganization categoryOfOrganization );
        void Delete(int id);
    }
}