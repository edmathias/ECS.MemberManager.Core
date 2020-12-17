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

        internal static async Task<CategoryOfOrganizationEC> NewCategoryOfOrganization()
        {
            return await DataPortal.CreateAsync<CategoryOfOrganizationEC>();
        }

        internal static async Task<CategoryOfOrganizationEC> GetCategoryOfOrganization(CategoryOfOrganization childData)
        {
            return await DataPortal.FetchAsync<CategoryOfOrganizationEC>(childData);
        }

        internal static async Task DeleteCategoryOfOrganization(int id)
        {
            await DataPortal.DeleteAsync<CategoryOfOrganizationEC>(id);
        }
 
        #endregion

        #region Data Access
       
        [CreateChild]
        private void Create()
        {
            MarkAsChild();
            
            BusinessRules.CheckRules();
        }
        
        [FetchChild]
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

        [InsertChild]
        private void Insert()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ICategoryOfOrganizationDal>();
            using (BypassPropertyChecks)
            {
                var categoryToInsert = new EF.Domain.CategoryOfOrganization()
                {
                    Id = Id,
                    DisplayOrder = DisplayOrder,
                    Category = Category
                };
                dal.Insert(categoryToInsert);
                Id = categoryToInsert.Id;
            }
        }
        
        [UpdateChild]
        private void Update()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ICategoryOfOrganizationDal>();
            using (BypassPropertyChecks)
            {
                var categoryToUpdate = new EF.Domain.CategoryOfOrganization()
                {
                    Id = Id,
                    Category = Category,
                    DisplayOrder = DisplayOrder
                };
                dal.Update(categoryToUpdate);
            }
        }

        [DeleteSelfChild]
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