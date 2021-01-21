using System.Collections.Generic;
using System.Threading.Tasks;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Dal
{
    public interface IOfficeDal
    {
        Task<Office> Fetch(int id);
        Task<List<Office>> Fetch();
        Task<Office> Insert(Office office);
        Task<Office> Update(Office office);
        Task Delete(int id);        
    }
}