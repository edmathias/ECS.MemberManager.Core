using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Dal
{
    public interface IEventMemberDal : IDisposable
    {
        Task<List<EventMember>> Fetch();
        Task<EventMember> Fetch(int id);
        Task<EventMember> Insert(EventMember eMailTypeToInsert);
        Task<EventMember> Update(EventMember eventMember);
        Task Delete(int id);
    }
}