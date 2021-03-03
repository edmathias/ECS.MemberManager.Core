


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


        internal static async Task<CategoryOfOrganizationROCL> GetCategoryOfOrganizationROCL(List<CategoryOfOrganization> childData)
        {
            return await DataPortal.FetchChildAsync<CategoryOfOrganizationROCL>(childData);
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
                    var objectToAdd = await CategoryOfOrganizationROC.GetCategoryOfOrganizationROC(domainObjToAdd);
                    Add(objectToAdd);
                }
            }
        }

        #endregion

     }
}
