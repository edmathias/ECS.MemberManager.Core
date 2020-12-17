using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class CategoryOfOrganizationERL : BusinessListBase<CategoryOfOrganizationERL, CategoryOfOrganizationEC>
    {
        
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        public static async Task<CategoryOfOrganizationERL> NewCategoryOfOrganizationList()
        {
            return await DataPortal.CreateAsync<CategoryOfOrganizationERL>();
        }

        public static async Task<CategoryOfOrganizationERL> GetCategoryOfOrganizationList(IList<CategoryOfOrganization> listOfChildren)
        {
            return await DataPortal.FetchAsync<CategoryOfOrganizationERL>(listOfChildren);
        }

        [Fetch]
        private async void Fetch(IList<CategoryOfOrganization> listOfChildren)
        {
            RaiseListChangedEvents = false;
            
            foreach (var addressData in listOfChildren)
            {
                this.Add(await CategoryOfOrganizationEC.GetCategoryOfOrganization(addressData));
            }
            
            RaiseListChangedEvents = true;
        }
            
    }
}