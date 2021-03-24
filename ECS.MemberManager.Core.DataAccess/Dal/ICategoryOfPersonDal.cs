using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Dal
{
    public interface ICategoryOfPersonDal : IDisposable
    {
        Task<CategoryOfPerson> Fetch(int id);
        Task<List<CategoryOfPerson>> Fetch();
        Task<CategoryOfPerson> Insert(CategoryOfPerson categoryOfPerson);
        Task<CategoryOfPerson> Update(CategoryOfPerson categoryOfPerson);
        Task Delete(int id);
    }
}