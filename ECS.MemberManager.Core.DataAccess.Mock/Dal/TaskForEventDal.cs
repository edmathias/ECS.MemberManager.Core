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
        public async Task<TaskForEvent> Fetch(int id)
        {
            return MockDb.TaskForEvents.FirstOrDefault(dt => dt.Id == id);
        }

        public async Task<List<TaskForEvent>> Fetch()
        {
            return MockDb.TaskForEvents.ToList();
        }

        public async Task<TaskForEvent> Insert(TaskForEvent taskForEvent)
        {
            var lastTaskForEvent = MockDb.TaskForEvents.ToList().OrderByDescending(dt => dt.Id).First();
            taskForEvent.Id = 1 + lastTaskForEvent.Id;
            taskForEvent.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);

            MockDb.TaskForEvents.Add(taskForEvent);

            taskForEvent.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);
            return taskForEvent;
        }

        public async Task<TaskForEvent> Update(TaskForEvent taskForEvent)
        {
            var taskForEventToUpdate =
                MockDb.TaskForEvents.FirstOrDefault(em => em.Id == taskForEvent.Id &&
                                                          em.RowVersion.SequenceEqual(taskForEvent.RowVersion));

            if (taskForEventToUpdate == null)
                throw new Csla.DataPortalException(null);

            taskForEventToUpdate.RowVersion = BitConverter.GetBytes(DateTime.Now.Ticks);
            return taskForEventToUpdate;
        }

        public async Task Delete(int id)
        {
            var taskForEventsToDelete = MockDb.TaskForEvents.FirstOrDefault(dt => dt.Id == id);
            var listIndex = MockDb.TaskForEvents.IndexOf(taskForEventsToDelete);
            if (listIndex > -1)
                MockDb.TaskForEvents.RemoveAt(listIndex);
        }

        public void Dispose()
        {
        }
    }
}