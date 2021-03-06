﻿


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
    public partial class EventMemberERL : BusinessListBase<EventMemberERL,EventMemberEC>
    {
        #region Factory Methods

        public static async Task<EventMemberERL> NewEventMemberERL()
        {
            return await DataPortal.CreateAsync<EventMemberERL>();
        }

        public static async Task<EventMemberERL> GetEventMemberERL( )
        {
            return await DataPortal.FetchAsync<EventMemberERL>();
        }

        #endregion

        #region Data Access
 
        [Fetch]
        private async Task Fetch([Inject] IDal<EventMember> dal)
        {
            var childData = await dal.Fetch();

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
