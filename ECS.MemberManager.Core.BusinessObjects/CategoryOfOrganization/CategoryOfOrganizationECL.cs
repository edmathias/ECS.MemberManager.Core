using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class CategoryOfOrganizationECL : BusinessListBase<CategoryOfOrganizationECL,CategoryOfOrganizationEC>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        internal static async Task<CategoryOfOrganizationECL> NewCategoryOfOrganizationList()
        {
            return await DataPortal.CreateChildAsync<CategoryOfOrganizationECL>();
        }

        internal static async Task<CategoryOfOrganizationECL> GetCategoryOfOrganizationList(IList<CategoryOfOrganization> listOfChildren)
        {
            return await DataPortal.FetchChildAsync<CategoryOfOrganizationECL>(listOfChildren);
        }

        [FetchChild]
        private async void FetchChild(IList<CategoryOfOrganization> listOfChildren)
        {
            RaiseListChangedEvents = false;
            
            foreach (var childData in listOfChildren)
            {
                this.Add( await CategoryOfOrganizationEC.GetCategoryOfOrganization(childData)  );
            }

            RaiseListChangedEvents = true;
        }
  
    }
}