


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
    public class TaskForEventRORL : ReadOnlyListBase<TaskForEventRORL, TaskForEventROC>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        internal static async Task<TaskForEventRORL> GetTaskForEventRORL()
        {
            return await DataPortal.FetchAsync<TaskForEventRORL>();
        }

        [Fetch]
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ITaskForEventDal>();
            var childData = await dal.Fetch();

            using (LoadListMode)
            {
                foreach (var objectToFetch in childData)
                {
                    var objectToAdd = await TaskForEventROC.GetTaskForEventROC(objectToFetch);
                    Add(objectToAdd);
                }
            }
        }
    }
}

