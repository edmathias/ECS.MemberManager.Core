//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 03/23/2021 09:56:41
//******************************************************************************    

using System;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess.Dal;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class
        CategoryOfOrganizationERL : BusinessListBase<CategoryOfOrganizationERL, CategoryOfOrganizationEC>
    {
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
        private async Task Fetch([Inject] ICategoryOfOrganizationDal dal)
        {
            var childData = await dal.Fetch();

            using (LoadListMode)
            {
                foreach (var domainObjToAdd in childData)
                {
                    var objectToAdd = await CategoryOfOrganizationEC.GetCategoryOfOrganizationEC(domainObjToAdd);
                    Add(objectToAdd);
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