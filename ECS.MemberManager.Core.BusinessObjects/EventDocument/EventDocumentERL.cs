﻿


//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 04/07/2021 09:31:54
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
    public partial class EventDocumentERL : BusinessListBase<EventDocumentERL,EventDocumentEC>
    {
        #region Factory Methods

        public static async Task<EventDocumentERL> NewEventDocumentERL()
        {
            return await DataPortal.CreateAsync<EventDocumentERL>();
        }

        public static async Task<EventDocumentERL> GetEventDocumentERL( )
        {
            return await DataPortal.FetchAsync<EventDocumentERL>();
        }

        #endregion

        #region Data Access
 
        [Fetch]
        private async Task Fetch([Inject] IDal<EventDocument> dal)
        {
            var childData = await dal.Fetch();

            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await EventDocumentEC.GetEventDocumentEC(domainObjToAdd);
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
