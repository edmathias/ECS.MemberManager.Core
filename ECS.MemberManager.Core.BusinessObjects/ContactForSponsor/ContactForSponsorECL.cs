﻿


//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 03/02/2021 21:49:45
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
    public partial class ContactForSponsorECL : BusinessListBase<ContactForSponsorECL,ContactForSponsorEC>
    {
        #region Factory Methods

        internal static async Task<ContactForSponsorECL> NewContactForSponsorECL()
        {
            return await DataPortal.CreateChildAsync<ContactForSponsorECL>();
        }

        internal static async Task<ContactForSponsorECL> GetContactForSponsorECL(List<ContactForSponsor> childData)
        {
            return await DataPortal.FetchChildAsync<ContactForSponsorECL>(childData);
        }

        #endregion

        #region Data Access
 
        [FetchChild]
        private async Task Fetch(List<ContactForSponsor> childData)
        {

            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await ContactForSponsorEC.GetContactForSponsorEC(domainObjToAdd);
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
