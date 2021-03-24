using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Dal
{
    public interface IPersonDal : IDisposable
    {
        Task<Person> Fetch(int id);
        Task<List<Person>> Fetch();
        Task<Person> Insert(Person person);
        Task<Person> Update(Person personToUpdate);
        Task Delete(int id);
    }
}