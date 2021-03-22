﻿


//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 03/18/2021 16:28:28
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
    public partial class OrganizationTypeECL : BusinessListBase<OrganizationTypeECL,OrganizationTypeEC>
    {
        #region Factory Methods

        internal static async Task<OrganizationTypeECL> NewOrganizationTypeECL()
        {
            return await DataPortal.CreateChildAsync<OrganizationTypeECL>();
        }

        internal static async Task<OrganizationTypeECL> GetOrganizationTypeECL(List<OrganizationType> childData)
        {
            return await DataPortal.FetchChildAsync<OrganizationTypeECL>(childData);
        }

        #endregion

        #region Data Access
 
        [FetchChild]
        private async Task Fetch(List<OrganizationType> childData)
        {

            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await OrganizationTypeEC.GetOrganizationTypeEC(domainObjToAdd);
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
