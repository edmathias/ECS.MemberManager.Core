//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 03/23/2021 09:57:03
//******************************************************************************    

using System;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess.Dal;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class EventRORL : ReadOnlyListBase<EventRORL, EventROC>
    {
        #region Factory Methods

        public static async Task<EventRORL> GetEventRORL()
        {
            return await DataPortal.FetchAsync<EventRORL>();
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
                    var objectToAdd = await EventROC.GetEventROC(domainObjToAdd);
                    Add(objectToAdd);
                }
            }
        }

        #endregion
    }
}