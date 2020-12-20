using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class CategoryOfPersonECL : BusinessListBase<CategoryOfPersonECL,CategoryOfPersonEC>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        internal static async Task<CategoryOfPersonECL> NewCategoryOfPersonList()
        {
            return await DataPortal.CreateChildAsync<CategoryOfPersonECL>();
        }

        internal static async Task<CategoryOfPersonECL> GetCategoryOfPersonList(IList<CategoryOfPerson> listOfChildren)
        {
            return await DataPortal.FetchChildAsync<CategoryOfPersonECL>(listOfChildren);
        }

        [FetchChild]
        private async void FetchChild(IList<CategoryOfPerson> listOfChildren)
        {
            RaiseListChangedEvents = false;
            
            foreach (var childData in listOfChildren)
            {
                this.Add( await CategoryOfPersonEC.GetCategoryOfPerson(childData)  );
            }

            RaiseListChangedEvents = true;
        }
  
    }
}