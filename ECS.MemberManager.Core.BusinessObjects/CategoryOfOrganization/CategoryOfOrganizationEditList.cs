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
    public class CategoryOfOrganizationEditList : BusinessListBase<CategoryOfOrganizationEditList,CategoryOfOrganizationEdit>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        public static async Task<CategoryOfOrganizationEditList> NewCategoryOfOrganizationEditList()
        {
            return await DataPortal.CreateAsync<CategoryOfOrganizationEditList>();
        }

        public static async Task<CategoryOfOrganizationEditList> GetCategoryOfOrganizationEditList()
        {
            return await DataPortal.FetchAsync<CategoryOfOrganizationEditList>();
        }

        public static async Task<CategoryOfOrganizationEditList> GetCategoryOfOrganizationEditList(List<CategoryOfOrganization> childData)
        {
            return await DataPortal.FetchAsync<CategoryOfOrganizationEditList>(childData);
        }
        
        
        [RunLocal]
        [Create]
        private void Create()
        {
            base.DataPortal_Create();
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
        private async Task Fetch(List<CategoryOfOrganization> childData)
        {
            using (LoadListMode)
            {
                foreach (var categoryOfOrganization in childData)
                {
                    var categoryOfOrganizationToAdd = 
                        await CategoryOfOrganizationEdit.GetCategoryOfOrganizationEdit(categoryOfOrganization);
                    Add(categoryOfOrganizationToAdd);
                }
            }
        }
        
        [Update]
        private void Update()
        {
            Child_Update();
        }
    }
}