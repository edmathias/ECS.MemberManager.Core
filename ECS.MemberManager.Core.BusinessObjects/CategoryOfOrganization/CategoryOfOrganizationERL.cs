﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class CategoryOfOrganizationERL : BusinessListBase<CategoryOfOrganizationERL,CategoryOfOrganizationEC>
    {
        #region Authorization Rules
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        #endregion
       
        #region Factory Methods
        
        public static async Task<CategoryOfOrganizationERL> NewCategoryOfOrganizationERL()
        {
            return await DataPortal.CreateAsync<CategoryOfOrganizationERL>();
        }

        public static async Task<CategoryOfOrganizationERL> GetCategoryOfOrganizationERL()
        {
            return await DataPortal.FetchAsync<CategoryOfOrganizationERL>();
        }
       
        #endregion
        
        #region Data Access
        
        [Fetch]
        private async Task Fetch()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ICategoryOfOrganizationDal>();
            var childData = await dal.Fetch();

            using (LoadListMode)
            {
                foreach (var categoryOfOrganization in childData)
                {
                    var categoryOfOrganizationToAdd = 
                        await CategoryOfOrganizationEC.GetCategoryOfOrganizationEC(categoryOfOrganization);
                    Add(categoryOfOrganizationToAdd);
                }
            }
        }
        
        [Update]
        private void Update()
        {
            Child_Update();
        }
        
        #endregion
    }
}