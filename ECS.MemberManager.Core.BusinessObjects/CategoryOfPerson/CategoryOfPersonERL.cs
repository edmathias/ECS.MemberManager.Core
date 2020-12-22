using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class CategoryOfPersonERL : BusinessListBase<CategoryOfPersonERL, CategoryOfPersonEC>
    {
        
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        public static async Task<CategoryOfPersonERL> NewCategoryOfPersonList()
        {
            return await DataPortal.CreateAsync<CategoryOfPersonERL>();
        }

        public static async Task<CategoryOfPersonERL> GetCategoryOfPersonList(IList<CategoryOfPerson> listOfChildren)
        {
            return await DataPortal.FetchAsync<CategoryOfPersonERL>(listOfChildren);
        }

        [Fetch]
        private async void Fetch(IList<CategoryOfPerson> listOfChildren)
        {
            RaiseListChangedEvents = false;
            
            foreach (var addressData in listOfChildren)
            {
                this.Add(await CategoryOfPersonEC.GetCategoryOfPerson(addressData));
            }
            
            RaiseListChangedEvents = true;
        }

        [Update]
        private void Update()
        {
            base.Child_Update();
        }
            
    }
}