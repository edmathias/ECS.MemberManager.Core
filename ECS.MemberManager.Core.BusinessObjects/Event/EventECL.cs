//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 03/23/2021 09:57:01
//******************************************************************************    

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class EventECL : BusinessListBase<EventECL, EventEC>
    {
        #region Factory Methods

        internal static async Task<EventECL> NewEventECL()
        {
            return await DataPortal.CreateChildAsync<EventECL>();
        }

        internal static async Task<EventECL> GetEventECL(IList<Event> childData)
        {
            return await DataPortal.FetchChildAsync<EventECL>(childData);
        }

        #endregion

        #region Data Access

        [FetchChild]
        private async Task Fetch(IList<Event> childData)
        {
            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await EventEC.GetEventEC(domainObjToAdd);
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