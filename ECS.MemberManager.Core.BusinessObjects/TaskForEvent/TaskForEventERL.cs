//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 03/23/2021 09:57:48
//******************************************************************************    

using System;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess.Dal;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class TaskForEventERL : BusinessListBase<TaskForEventERL, TaskForEventEC>
    {
        #region Factory Methods

        public static async Task<TaskForEventERL> NewTaskForEventERL()
        {
            return await DataPortal.CreateAsync<TaskForEventERL>();
        }

        public static async Task<TaskForEventERL> GetTaskForEventERL()
        {
            return await DataPortal.FetchAsync<TaskForEventERL>();
        }

        #endregion

        #region Data Access

        [Fetch]
        private async Task Fetch([Inject] ITaskForEventDal dal)
        {
            var childData = await dal.Fetch();

            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await TaskForEventEC.GetTaskForEventEC(domainObjToAdd);
                    Add(objectToAdd);
                }
            }
        }

        [Update]
        private void Update()
        {
            Child_Update();
        }

        #endregion
    }
}