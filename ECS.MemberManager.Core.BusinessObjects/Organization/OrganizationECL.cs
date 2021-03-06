﻿


//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 04/07/2021 09:32:21
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
    public partial class OrganizationECL : BusinessListBase<OrganizationECL,OrganizationEC>
    {
        #region Factory Methods

        internal static async Task<OrganizationECL> NewOrganizationECL()
        {
            return await DataPortal.CreateChildAsync<OrganizationECL>();
        }

        internal static async Task<OrganizationECL> GetOrganizationECL(IList<Organization> childData)
        {
            return await DataPortal.FetchChildAsync<OrganizationECL>(childData);
        }

        #endregion

        #region Data Access
 
        [FetchChild]
        private async Task Fetch(IList<Organization> childData)
        {

            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await OrganizationEC.GetOrganizationEC(domainObjToAdd);
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
