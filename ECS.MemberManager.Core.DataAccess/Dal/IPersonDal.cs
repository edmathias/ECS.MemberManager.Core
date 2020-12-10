using System;
using System.Collections.Generic;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Dal
{
    public interface IPersonDal : IDisposable
    {
        Person Fetch(int id);
        List<Person> Fetch();
        int Insert(Person person);
        void Update(Person person );
        void Delete(int id);
    }
}