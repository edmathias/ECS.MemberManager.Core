using System.Collections.Generic;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Dal
{
    public interface IOfficeDal
    {
        Office Fetch(int id);
        List<Office> Fetch();
        int Insert(Office office);
        void Update(Office office);
        void Delete(int id);        
    }
}