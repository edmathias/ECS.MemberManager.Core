using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Mock
{
    public class EventDocumentDal : IEventDocumentDal
    {
        public void Dispose()
        {
        }

        public async Task<EventDocument> Fetch(int id)
        {
            return MockDb.EventDocuments.FirstOrDefault(ms => ms.Id == id);
        }

        public async Task<List<EventDocument>> Fetch()
        {
            return MockDb.EventDocuments.ToList();
        }

        public async Task<EventDocument> Insert(EventDocument eventToInsert)
        {
            var lastEventDocument = MockDb.EventDocuments.ToList().OrderByDescending(e =>e.Id).First();
            eventToInsert.Id = 1+lastEventDocument.Id;
            
            MockDb.EventDocuments.Add(eventToInsert);
            return eventToInsert;        
        }

        public async Task<EventDocument> Update(EventDocument eventUpdate)
        {
            var eventToUpdate = MockDb.EventDocuments.FirstOrDefault(e => e.Id == eventUpdate.Id);
            eventToUpdate.Id = eventUpdate.Id;
            eventToUpdate.Notes = eventUpdate.Notes;
            eventToUpdate.DocumentName = eventUpdate.DocumentName;
            eventToUpdate.EventId = eventUpdate.EventId;
            eventToUpdate.DocumentTypeId = eventUpdate.DocumentTypeId;
            eventToUpdate.LastUpdatedBy = eventUpdate.LastUpdatedBy;
            eventToUpdate.LastUpdatedDate = eventUpdate.LastUpdatedDate;
            eventToUpdate.PathAndFileName = eventUpdate.PathAndFileName;
            
            return eventUpdate;
        }

        public async Task Delete(int id)
        {
            var eventToDelete = MockDb.EventDocuments.FirstOrDefault(e => e.Id == id);
            var listIndex = MockDb.EventDocuments.IndexOf(eventToDelete);
            if(listIndex > -1)
                MockDb.EventDocuments.RemoveAt(listIndex);
        }
    }
}