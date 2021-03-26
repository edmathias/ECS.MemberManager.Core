


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
    public partial class EventMemberECL : BusinessListBase<EventMemberECL,EventMemberEC>
    {
        #region Factory Methods

        internal static async Task<EventMemberECL> NewEventMemberECL()
        {
            return await DataPortal.CreateChildAsync<EventMemberECL>();
        }

        internal static async Task<EventMemberECL> GetEventMemberECL(IList<EventMember> childData)
        {
            return await DataPortal.FetchChildAsync<EventMemberECL>(childData);
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
                    var objectToAdd = await EventMemberEC.GetEventMemberEC(domainObjToAdd);
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
