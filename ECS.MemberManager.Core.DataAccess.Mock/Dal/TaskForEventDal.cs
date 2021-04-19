using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Mock
{
    public class TaskForEventDal : IDal<TaskForEvent>
    {
        public Task<TaskForEvent> Fetch(int id)
        {
            return Task.FromResult(MockDb.TaskForEvents.FirstOrDefault(dt => dt.Id == id));
        }

        public Task<List<TaskForEvent>> Fetch()
        {
            return Task.FromResult(MockDb.TaskForEvents.ToList());
        }

        public Task<TaskForEvent> Insert(TaskForEvent taskForEvent)
        {
            var lastTaskForEvent = MockDb.TaskForEvents.ToList().OrderByDescending(dt => dt.Id).First();
            taskForEvent.Id = 1 + lastTaskForEvent.Id;
            taskForEvent.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);

            MockDb.TaskForEvents.Add(taskForEvent);

            taskForEvent.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);
            return Task.FromResult(taskForEvent);
        }

        public Task<TaskForEvent> Update(TaskForEvent taskForEvent)
        {
            var taskForEventToUpdate =
                MockDb.TaskForEvents.FirstOrDefault(em => em.Id == taskForEvent.Id &&
                                                          em.RowVersion.SequenceEqual(taskForEvent.RowVersion));

            if (taskForEventToUpdate == null)
                throw new Csla.DataPortalException(null);

            taskForEventToUpdate.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);
            return Task.FromResult(taskForEventToUpdate);
        }

        public Task Delete(int id)
        {
            var taskForEventsToDelete = MockDb.TaskForEvents.FirstOrDefault(dt => dt.Id == id);
            var listIndex = MockDb.TaskForEvents.IndexOf(taskForEventsToDelete);
            if (listIndex > -1)
                MockDb.TaskForEvents.RemoveAt(listIndex);
            
            return Task.CompletedTask;
        }

        public void Dispose()
        {
        }
    }
}