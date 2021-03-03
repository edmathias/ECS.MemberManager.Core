﻿


//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 03/02/2021 21:49:39
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
    public partial class CategoryOfOrganizationECL : BusinessListBase<CategoryOfOrganizationECL,CategoryOfOrganizationEC>
    {
        #region Factory Methods

        internal static async Task<CategoryOfOrganizationECL> NewCategoryOfOrganizationECL()
        {
            return await DataPortal.CreateChildAsync<CategoryOfOrganizationECL>();
        }

        internal static async Task<CategoryOfOrganizationECL> GetCategoryOfOrganizationECL(List<CategoryOfOrganization> childData)
        {
            return await DataPortal.FetchChildAsync<CategoryOfOrganizationECL>(childData);
        }

        #endregion

        #region Data Access
 
        [FetchChild]
        private async Task Fetch(List<CategoryOfOrganization> childData)
        {

            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await CategoryOfOrganizationEC.GetCategoryOfOrganizationEC(domainObjToAdd);
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
