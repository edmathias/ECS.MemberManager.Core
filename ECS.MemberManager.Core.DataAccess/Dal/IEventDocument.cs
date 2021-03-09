using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Dal
{
    public interface IEventDocumentDal : IDisposable
    {
        Task<List<EventDocument>> Fetch();
        Task<EventDocument> Fetch(int id);
        Task<EventDocument> Insert(EventDocument eMailTypeToInsert);
        Task<EventDocument> Update(EventDocument eventDocument);
        Task Delete(int id);
    }
}