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
    public class CategoryOfPersonECL : BusinessListBase<CategoryOfPersonECL,CategoryOfPersonEC>
    {
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        public static async Task<CategoryOfPersonECL> NewCategoryOfPersonECL()
        {
            return await DataPortal.CreateAsync<CategoryOfPersonECL>();
        }

        public static async Task<CategoryOfPersonECL> GetCategoryOfPersonECL()
        {
            return await DataPortal.FetchAsync<CategoryOfPersonECL>();
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

            using (LoadListMode)
            {
                foreach (var categoryOfPerson in childData)
                {
                    var categoryOfPersonToAdd = 
                        await CategoryOfPersonEC.GetCategoryOfPersonEC(categoryOfPerson);
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