using System.Collections.Generic;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Dal
{
    public interface IOrganizationTypeDal
    {
        OrganizationType Fetch(int id);
        List<OrganizationType> Fetch();
        int Insert(OrganizationType organizationType);
        void Update(OrganizationType organizationType );
        void Delete(int id);        
    }
}