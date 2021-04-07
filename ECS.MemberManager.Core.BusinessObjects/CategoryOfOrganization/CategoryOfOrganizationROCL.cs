


//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 04/07/2021 09:31:28
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
    public partial class CategoryOfOrganizationROCL : ReadOnlyListBase<CategoryOfOrganizationROCL,CategoryOfOrganizationROC>
    {
        #region Factory Methods


        internal static async Task<CategoryOfOrganizationROCL> GetCategoryOfOrganizationROCL(IList<CategoryOfOrganization> childData)
        {
            return await DataPortal.FetchChildAsync<CategoryOfOrganizationROCL>(childData);
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
                    var objectToAdd = await CategoryOfOrganizationROC.GetCategoryOfOrganizationROC(domainObjToAdd);
                    Add(objectToAdd);
                }
            }
        }

        #endregion

     }
}
