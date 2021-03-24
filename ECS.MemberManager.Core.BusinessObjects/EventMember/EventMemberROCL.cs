using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class EventMemberROCL : ReadOnlyListBase<EventMemberROCL, EventMemberROC>
    {
        #region Factory Methods

        internal static async Task<EventMemberROCL> GetEventMemberROCL(IList<EventMember> childData)
        {
            return await DataPortal.FetchChildAsync<EventMemberROCL>(childData);
        }

        #endregion

        #region Data Access

        [FetchChild]
        private async Task Fetch(IList<EventMember> childData)
        {
            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await EventMemberROC.GetEventMemberROC(domainObjToAdd);
                    Add(objectToAdd);
                }
            }
        }

        #endregion
    }
}