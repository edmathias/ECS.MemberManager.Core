


using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class TaskForEventECL : BusinessListBase<TaskForEventECL, TaskForEventEC>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        internal static async Task<TaskForEventECL> NewTaskForEventECL()
        {
            return await DataPortal.CreateAsync<TaskForEventECL>();
        }

        internal static async Task<TaskForEventECL> GetTaskForEventECL(List<TaskForEvent> childData)
        {
            return await DataPortal.FetchAsync<TaskForEventECL>(childData);
        }

        [Fetch]
        private async Task Fetch(List<TaskForEvent> childData)
        {
            using (LoadListMode)
            {
                foreach (var TaskForEvent in childData)
                {
                    var TaskForEventToAdd = await TaskForEventEC.GetTaskForEventEC(TaskForEvent);
                    Add(TaskForEventToAdd);
                }
            }
        }

        [Update]
        private void Update()
        {
            Child_Update();
        }
    }
}

