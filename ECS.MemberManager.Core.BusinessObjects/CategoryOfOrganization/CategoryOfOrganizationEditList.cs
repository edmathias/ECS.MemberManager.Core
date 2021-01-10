using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class CategoryOfOrganizationEditList : BusinessListBase<CategoryOfOrganizationEditList, CategoryOfOrganizationEdit>
    {
        
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        public static async Task<CategoryOfOrganizationEditList> NewCategoryOfOrganizationEditList()
        {
            return await DataPortal.CreateAsync<CategoryOfOrganizationEditList>();
        }

        public static async Task<CategoryOfOrganizationEditList> GetCategoryOfOrganizationEditList(IList<CategoryOfOrganization> listOfChildren)
        {
            return await DataPortal.FetchAsync<CategoryOfOrganizationEditList>(listOfChildren);
        }
        
        public static async Task<CategoryOfOrganizationEditList> GetCategoryOfOrganizationEditList()
        {
            return await DataPortal.FetchAsync<CategoryOfOrganizationEditList>();
        }        
        
        [Fetch]
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ICategoryOfOrganizationDal>();
            var childData = await dal.Fetch();

            await Fetch(childData);
        }

        [Fetch]
        private async Task Fetch(IList<CategoryOfOrganization> listOfChildren)
        {
            RaiseListChangedEvents = false;
            
            foreach (var categoryOfOrganizationData in listOfChildren)
            {
                this.Add(await CategoryOfOrganizationEdit.GetCategoryOfOrganizationEdit(categoryOfOrganizationData));
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