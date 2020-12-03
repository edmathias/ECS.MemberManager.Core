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

        [Required, MaxLength(35)]
        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(p => p.Name);
        public string Name
        {
            get => GetProperty(NameProperty);
            set => SetProperty(NameProperty, value);
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
                Name = data.Category;
                DisplayOrder = data.DisplayOrder;
            }
        }

        [Insert]
        private void Insert()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ICategoryOfOrganizationDal>();
            using (BypassPropertyChecks)
            {
                var categoryToInsert = new EF.Domain.CategoryOfOrganization()
                {
                    Id = this.Id,
                    DisplayOrder = this.DisplayOrder,
                    Category = this.Name
                };
                dal.Insert(categoryToInsert);
                Id = categoryToInsert.Id;
            }
        }
        
        [Update]
        private void Update()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ICategoryOfOrganizationDal>();
            using (BypassPropertyChecks)
            {
                var categoryToUpdate = new EF.Domain.CategoryOfOrganization()
                {
                    Id = this.Id,
                    Category = this.Name,
                    DisplayOrder = this.DisplayOrder
                };
                dal.Update(categoryToUpdate);
            }
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
            var dal = dalManager.GetProvider<CategoryOfOrganizationER>();
 
            dal.Delete(id);
        }

        #endregion

 
    }
}