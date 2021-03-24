//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 03/23/2021 09:57:02
//******************************************************************************    

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class EventROCL : ReadOnlyListBase<EventROCL, EventROC>
    {
        #region Factory Methods

        internal static async Task<EventROCL> GetEventROCL(IList<Event> childData)
        {
            return await DataPortal.FetchChildAsync<EventROCL>(childData);
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
                    var objectToAdd = await EventROC.GetEventROC(domainObjToAdd);
                    Add(objectToAdd);
                }
            }
        }

        #endregion
    }
}