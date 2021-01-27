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
    public class CategoryOfOrganizationEC : BusinessBase<CategoryOfOrganizationEC>
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
            set => SetProperty(CategoryProperty, value);
        }

        public static readonly PropertyInfo<int> DisplayOrderProperty = RegisterProperty<int>(p => p.DisplayOrder);

        public int DisplayOrder
        {
            get => GetProperty(DisplayOrderProperty);
            set => SetProperty(DisplayOrderProperty, value);
        }

        public static readonly PropertyInfo<byte[]> RowVersionProperty = RegisterProperty<byte[]>(p => p.RowVersion);

        public byte[] RowVersion
        {
            get => GetProperty(RowVersionProperty);
            set => SetProperty(RowVersionProperty, value);
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

        internal static async Task<CategoryOfOrganizationEC> NewCategoryOfOrganizationEC()
        {
            return await DataPortal.CreateChildAsync<CategoryOfOrganizationEC>();
        }

        internal static async Task<CategoryOfOrganizationEC> GetCategoryOfOrganizationEC(CategoryOfOrganization childData)
        {
            return await DataPortal.FetchChildAsync<CategoryOfOrganizationEC>(childData);
        }

        #endregion

        #region Data Access Methods


        [FetchChild]
        private void Fetch(CategoryOfOrganization childData)
        {
            using (BypassPropertyChecks)
            {
                Id = childData.Id;
                Category = childData.Category;
                RowVersion = childData.RowVersion;
            }
        }

        [InsertChild]
        private async Task InsertChild()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ICategoryOfOrganizationDal>();
            var data = new CategoryOfOrganization()
            {
                Category = Category,
                DisplayOrder = DisplayOrder
            };

            var insertedCategoryOfOrganization = await dal.Insert(data);
            Id = insertedCategoryOfOrganization.Id;
            RowVersion = insertedCategoryOfOrganization.RowVersion;
        }

        [UpdateChild]
        private async Task ChildUpdate()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ICategoryOfOrganizationDal>();

            var categoryOfOrganizationTypeToUpdate = new CategoryOfOrganization()
            {
                Id = Id,
                Category = Category,
                DisplayOrder = DisplayOrder,
                RowVersion = RowVersion
            };

            var updatedEmail = await dal.Update(categoryOfOrganizationTypeToUpdate);
            RowVersion = updatedEmail.RowVersion;
        }
        
        [DeleteSelfChild]
        private async Task DeleteSelf()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ICategoryOfOrganizationDal>();
           
            await dal.Delete(Id);
        }

        #endregion
    }
}