using System;
using System.Collections.Generic;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Dal
{
    public interface ICategoryOfPersonDal : IDisposable
    {
        CategoryOfPerson Fetch(int id);
        List<CategoryOfPerson> Fetch();
        int Insert(CategoryOfPerson CategoryOfPerson);
        void Update(CategoryOfPerson CategoryOfPerson );
        void Delete(int id);
    }
}