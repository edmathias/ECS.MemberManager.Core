//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 03/23/2021 09:56:40
//******************************************************************************    

using System;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class CategoryOfOrganizationEC : BusinessBase<CategoryOfOrganizationEC>
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
            set => SetProperty(CategoryProperty, value);
        }

        public static readonly PropertyInfo<int> DisplayOrderProperty = RegisterProperty<int>(o => o.DisplayOrder);

        public virtual int DisplayOrder
        {
            get => GetProperty(DisplayOrderProperty);
            set => SetProperty(DisplayOrderProperty, value);
        }

        public static readonly PropertyInfo<byte[]> RowVersionProperty = RegisterProperty<byte[]>(o => o.RowVersion);

        public virtual byte[] RowVersion
        {
            get => GetProperty(RowVersionProperty);
            set => SetProperty(RowVersionProperty, value);
        }

        #endregion

        #region Factory Methods

        internal static async Task<CategoryOfOrganizationEC> NewCategoryOfOrganizationEC()
        {
            return await DataPortal.CreateChildAsync<CategoryOfOrganizationEC>();
        }

        internal static async Task<CategoryOfOrganizationEC> GetCategoryOfOrganizationEC(
            CategoryOfOrganization childData)
        {
            return await DataPortal.FetchChildAsync<CategoryOfOrganizationEC>(childData);
        }

        #endregion

        #region Data Access Methods

        [FetchChild]
        private async Task Fetch(CategoryOfOrganization data)
        {
            using (BypassPropertyChecks)
            {
                Id = data.Id;
                Category = data.Category;
                DisplayOrder = data.DisplayOrder;
                RowVersion = data.RowVersion;
            }
        }

        [InsertChild]
        private async Task Insert([Inject] ICategoryOfOrganizationDal dal)
        {
            var data = new CategoryOfOrganization()
            {
                Id = Id,
                Category = Category,
                DisplayOrder = DisplayOrder,
                RowVersion = RowVersion,
            };

            var insertedObj = await dal.Insert(data);
            Id = insertedObj.Id;
            RowVersion = insertedObj.RowVersion;
        }

        [UpdateChild]
        private async Task Update([Inject] ICategoryOfOrganizationDal dal)
        {
            var data = new CategoryOfOrganization()
            {
                Id = Id,
                Category = Category,
                DisplayOrder = DisplayOrder,
                RowVersion = RowVersion,
            };

            var insertedObj = await dal.Update(data);
            RowVersion = insertedObj.RowVersion;
        }


        [DeleteSelfChild]
        private async Task DeleteSelf([Inject] ICategoryOfOrganizationDal dal)
        {
            await Delete(Id, dal);
        }

        [Delete]
        private async Task Delete(int id, [Inject] ICategoryOfOrganizationDal dal)
        {
            await dal.Delete(id);
        }

        #endregion
    }
}