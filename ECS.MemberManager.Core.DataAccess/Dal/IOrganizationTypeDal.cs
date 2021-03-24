using System.Collections.Generic;
using System.Threading.Tasks;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Dal
{
    public interface IOrganizationTypeDal
    {
        Task<OrganizationType> Fetch(int id);
        Task<List<OrganizationType>> Fetch();
        Task<OrganizationType> Insert(OrganizationType organizationType);
        Task<OrganizationType> Update(OrganizationType organizationType);
        Task Delete(int id);
    }
}