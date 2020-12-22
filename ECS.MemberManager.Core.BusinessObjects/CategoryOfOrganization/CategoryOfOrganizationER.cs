using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class CategoryOfOrganizationER : BusinessBase<CategoryOfOrganizationER>
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

        [Create]
        [RunLocal]
        private void Create()
        {
            BusinessRules.CheckRules();
        }

        public static async Task<CategoryOfOrganizationER> NewCategoryOfOrganization()
        {
            return await DataPortal.CreateAsync<CategoryOfOrganizationER>();
        }

        public static async Task<CategoryOfOrganizationER> GetCategoryOfOrganization(int id)
        {
            return await DataPortal.FetchAsync<CategoryOfOrganizationER>(id);
        }

        public static async Task DeleteCategoryOfOrganization(int id)
        {
            await DataPortal.DeleteAsync<CategoryOfOrganizationER>(id);
        }

        #endregion

        #region Data Access

        [Fetch]
        private void Fetch(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ICategoryOfOrganizationDal>();
            var data = dal.Fetch(id);
            using (BypassPropertyChecks)
            {
                Id = data.Id;
                Category = data.Category;
                DisplayOrder = data.DisplayOrder;
            }
        }

        [Insert]
        private void Insert()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ICategoryOfOrganizationDal>();
            var categoryToInsert = new EF.Domain.CategoryOfOrganization()
            {
                Id = Id,
                DisplayOrder = DisplayOrder,
                Category = Category
            };
            dal.Insert(categoryToInsert);
            Id = categoryToInsert.Id;
        }

        [Update]
        private void Update()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ICategoryOfOrganizationDal>();
            var categoryToUpdate = new EF.Domain.CategoryOfOrganization()
            {
                Id = Id,
                Category = Category,
                DisplayOrder = DisplayOrder
            };
            dal.Update(categoryToUpdate);
        }

        [DeleteSelf]
        private void DeleteSelf()
        {
            Delete(this.Id);
        }

        [Delete]
        private void Delete(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ICategoryOfOrganizationDal>();

            dal.Delete(id);
        }

        #endregion
    }
}