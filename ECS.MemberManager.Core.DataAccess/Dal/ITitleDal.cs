using System;
using System.Collections.Generic;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Dal
{
    public interface ITitleDal : IDisposable
    {
        Title Fetch(int id);
        List<Title> Fetch();
        int Insert(Title title);
        void Update(Title title );
        void Delete(int id);
    }
}