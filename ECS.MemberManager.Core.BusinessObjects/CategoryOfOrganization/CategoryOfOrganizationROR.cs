//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 03/23/2021 09:56:43
//******************************************************************************    

using System;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess.Dal;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class CategoryOfOrganizationROR : BusinessBase<CategoryOfOrganizationROR>
    {
        #region Business Methods

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(o => o.Id);

        public virtual int Id
        {
            get => GetProperty(IdProperty);
            private set => LoadProperty(IdProperty, value);
        }

        public static readonly PropertyInfo<string> CategoryProperty = RegisterProperty<string>(o => o.Category);

        public virtual string Category
        {
            get => GetProperty(CategoryProperty);
            private set => LoadProperty(CategoryProperty, value);
        }

        public static readonly PropertyInfo<int> DisplayOrderProperty = RegisterProperty<int>(o => o.DisplayOrder);

        public virtual int DisplayOrder
        {
            get => GetProperty(DisplayOrderProperty);
            private set => LoadProperty(DisplayOrderProperty, value);
        }

        public static readonly PropertyInfo<byte[]> RowVersionProperty = RegisterProperty<byte[]>(o => o.RowVersion);

        public virtual byte[] RowVersion
        {
            get => GetProperty(RowVersionProperty);
            private set => LoadProperty(RowVersionProperty, value);
        }

        #endregion

        #region Factory Methods

        public static async Task<CategoryOfOrganizationROR> GetCategoryOfOrganizationROR(int id)
        {
            return await DataPortal.FetchAsync<CategoryOfOrganizationROR>(id);
        }

        #endregion

        #region Data Access Methods

        [Fetch]
        private async Task Fetch(int id, [Inject] ICategoryOfOrganizationDal dal)
        {
            var data = await dal.Fetch(id);

            Id = data.Id;
            Category = data.Category;
            DisplayOrder = data.DisplayOrder;
            RowVersion = data.RowVersion;
        }

        #endregion
    }
}