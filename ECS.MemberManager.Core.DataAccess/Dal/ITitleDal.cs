using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Dal
{
    public interface ITitleDal : IDisposable
    {
        Task<Title> Fetch(int id);
        Task<List<Title>> Fetch();
        Task<Title> Insert(Title title);
        Task<Title> Update(Title title );
        Task Delete(int id);
    }
}