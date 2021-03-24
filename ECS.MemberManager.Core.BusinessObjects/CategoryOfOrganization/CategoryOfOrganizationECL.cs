//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 03/23/2021 09:56:41
//******************************************************************************    

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class
        CategoryOfOrganizationECL : BusinessListBase<CategoryOfOrganizationECL, CategoryOfOrganizationEC>
    {
        #region Factory Methods

        internal static async Task<CategoryOfOrganizationECL> NewCategoryOfOrganizationECL()
        {
            return await DataPortal.CreateChildAsync<CategoryOfOrganizationECL>();
        }

        internal static async Task<CategoryOfOrganizationECL> GetCategoryOfOrganizationECL(
            IList<CategoryOfOrganization> childData)
        {
            return await DataPortal.FetchChildAsync<CategoryOfOrganizationECL>(childData);
        }

        #endregion

        #region Data Access

        [FetchChild]
        private async Task Fetch(IList<CategoryOfOrganization> childData)
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