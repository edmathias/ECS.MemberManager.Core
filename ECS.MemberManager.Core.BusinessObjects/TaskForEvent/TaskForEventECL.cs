


//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 03/09/2021 14:34:52
//******************************************************************************    

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
    public partial class TaskForEventECL : BusinessListBase<TaskForEventECL,TaskForEventEC>
    {
        #region Factory Methods

        internal static async Task<TaskForEventECL> NewTaskForEventECL()
        {
            return await DataPortal.CreateChildAsync<TaskForEventECL>();
        }

        internal static async Task<TaskForEventECL> GetTaskForEventECL(List<TaskForEvent> childData)
        {
            return await DataPortal.FetchChildAsync<TaskForEventECL>(childData);
        }

        #endregion

        #region Data Access
 
        [FetchChild]
        private async Task Fetch(List<TaskForEvent> childData)
        {

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
