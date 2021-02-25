


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
    public partial class OrganizationTypeROCL : ReadOnlyListBase<OrganizationTypeROCL,OrganizationTypeROC>
    {
        #region Factory Methods

        internal static async Task<OrganizationTypeROCL> NewOrganizationTypeROCL()
        {
            return await DataPortal.CreateChildAsync<OrganizationTypeROCL>();
        }

        internal static async Task<OrganizationTypeROCL> GetOrganizationTypeROCL(List<OrganizationType> childData)
        {
            return await DataPortal.FetchChildAsync<OrganizationTypeROCL>(childData);
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
                    var objectToAdd = await OrganizationTypeROC.GetOrganizationTypeROC(domainObjToAdd);
                    Add(objectToAdd);
                }
            }
        }

        #endregion

     }
}
