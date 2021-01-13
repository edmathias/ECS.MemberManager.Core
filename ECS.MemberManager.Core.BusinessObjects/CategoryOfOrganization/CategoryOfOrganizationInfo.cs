using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class CategoryOfOrganizationInfo : ReadOnlyBase<CategoryOfOrganizationInfo>
    {
        #region Business Methods

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id);

        public int Id
        {
            get => GetProperty(IdProperty);
            private set => LoadProperty(IdProperty, value);
        }

        public static readonly PropertyInfo<string> CategoryProperty = RegisterProperty<string>(p => p.Category);

        [Required, MaxLength(35)]
        public string Category
        {
            get => GetProperty(CategoryProperty);
            private set => LoadProperty(CategoryProperty, value);
        }

        public static readonly PropertyInfo<int> DisplayOrderProperty = RegisterProperty<int>(p => p.DisplayOrder);

        public int DisplayOrder
        {
            get => GetProperty(DisplayOrderProperty);
            private set => LoadProperty(DisplayOrderProperty, value);
        }

        public static readonly PropertyInfo<byte[]> RowVersionProperty = RegisterProperty<byte[]>(p => p.RowVersion);

        public byte[] RowVersion
        {
            get => GetProperty(RowVersionProperty);
            private set => LoadProperty(RowVersionProperty, value);
        }


        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();

            // TODO: add business rules
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        #endregion

        #region Factory Methods

        public static async Task<CategoryOfOrganizationInfo> NewCategoryOfOrganizationInfo()
        {
            return await DataPortal.CreateAsync<CategoryOfOrganizationInfo>();
        }

        public static async Task<CategoryOfOrganizationInfo> GetCategoryOfOrganizationInfo(int id)
        {
            return await DataPortal.FetchAsync<CategoryOfOrganizationInfo>(id);
        }

        public static async Task<CategoryOfOrganizationInfo> GetCategoryOfOrganizationInfo(
            CategoryOfOrganization childData)
        {
            return await DataPortal.FetchChildAsync<CategoryOfOrganizationInfo>(childData);
        }

        #endregion

        #region Data Access

        [FetchChild]
        private void FetchChild(CategoryOfOrganization childData)
        {
            Id = childData.Id;
            Category = childData.Category;
            DisplayOrder = childData.DisplayOrder;
            RowVersion = childData.RowVersion;
        }

        [Fetch]
        private async Task Fetch(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ICategoryOfOrganizationDal>();
            var data = await dal.Fetch(id);

            FetchChild(data);
        }

        #endregion
    }
}