


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
    public class TaskForEventROCL : ReadOnlyListBase<TaskForEventROCL, TaskForEventROC>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        internal static async Task<TaskForEventROCL> GetTaskForEventROCL(List<TaskForEvent> childData)
        {
            return await DataPortal.FetchAsync<TaskForEventROCL>(childData);
        }

        [Fetch]
        private async Task Fetch(List<TaskForEvent> childData)
        {
            using (LoadListMode)
            {
                foreach (var objectToFetch in childData)
                {
                    var TaskForEventToAdd = await TaskForEventROC.GetTaskForEventROC(objectToFetch);
                    Add(TaskForEventToAdd);
                }
            }
        }
    }
}

