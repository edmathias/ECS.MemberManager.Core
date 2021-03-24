//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 03/23/2021 09:57:01
//******************************************************************************    

using System;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess.Dal;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class EventERL : BusinessListBase<EventERL, EventEC>
    {
        #region Factory Methods

        public static async Task<EventERL> NewEventERL()
        {
            return await DataPortal.CreateAsync<EventERL>();
        }

        public static async Task<EventERL> GetEventERL()
        {
            return await DataPortal.FetchAsync<EventERL>();
        }

        #endregion

        #region Data Access

        [Fetch]
        private async Task Fetch([Inject] IEventDal dal)
        {
            var childData = await dal.Fetch();

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