using System.Collections.Generic;
using System.Threading.Tasks;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Dal
{
    public interface IMemberInfoDal
    {
        Task<MemberInfo> Fetch(int id);
        Task<List<MemberInfo>> Fetch();
        Task<MemberInfo> Insert(MemberInfo memberInfo);
        Task<MemberInfo> Update(MemberInfo memberInfo);
        Task Delete(int id);
    }
}