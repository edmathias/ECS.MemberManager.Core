using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Dal
{
    public interface ITaskForEventDal : IDisposable
    {
        Task<TaskForEvent> Fetch(int id);
        Task<List<TaskForEvent>> Fetch();
        Task<TaskForEvent> Insert(TaskForEvent title);
        Task<TaskForEvent> Update(TaskForEvent title);
        Task Delete(int id);
    }
}