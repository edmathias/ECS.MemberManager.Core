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
    public class CategoryOfPersonEditList : BusinessListBase<CategoryOfPersonEditList,CategoryOfPersonEdit>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        public static async Task<CategoryOfPersonEditList> NewCategoryOfPersonEditList()
        {
            return await DataPortal.CreateAsync<CategoryOfPersonEditList>();
        }

        public static async Task<CategoryOfPersonEditList> GetCategoryOfPersonEditList()
        {
            return await DataPortal.FetchAsync<CategoryOfPersonEditList>();
        }

        public static async Task<CategoryOfPersonEditList> GetCategoryOfPersonEditList(List<CategoryOfPerson> childData)
        {
            return await DataPortal.FetchAsync<CategoryOfPersonEditList>(childData);
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
            var dal = dalManager.GetProvider<ICategoryOfPersonDal>();
            var childData = await dal.Fetch();

            await Fetch(childData);

        }

        [Fetch]
        private async Task Fetch(List<CategoryOfPerson> childData)
        {
            using (LoadListMode)
            {
                foreach (var categoryOfPerson in childData)
                {
                    var categoryOfPersonToAdd = 
                        await CategoryOfPersonEdit.GetCategoryOfPersonEdit(categoryOfPerson);
                    Add(categoryOfPersonToAdd);
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