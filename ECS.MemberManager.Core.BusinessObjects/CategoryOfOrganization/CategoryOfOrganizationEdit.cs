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
    public class CategoryOfOrganizationEdit : BusinessBase<CategoryOfOrganizationEdit>
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

        public static async Task<CategoryOfOrganizationEdit> NewCategoryOfOrganizationEdit()
        {
            return await DataPortal.CreateAsync<CategoryOfOrganizationEdit>();
        }

        public static async Task<CategoryOfOrganizationEdit> GetCategoryOfOrganizationEdit(int id)
        {
            return await DataPortal.FetchAsync<CategoryOfOrganizationEdit>(id);
        }
        
        public static async Task<CategoryOfOrganizationEdit> GetCategoryOfOrganizationEdit(CategoryOfOrganization childData)
        {
            return await DataPortal.FetchChildAsync<CategoryOfOrganizationEdit>(childData);
        }        

        public static async Task DeleteCategoryOfOrganizationEdit(int id)
        {
            await DataPortal.DeleteAsync<CategoryOfOrganizationEdit>(id);
        }

        #endregion

        #region Data Access

        [FetchChild]
        private void FetchChild(CategoryOfOrganization childData)
        {
            using (BypassPropertyChecks)
            {
                Id = childData.Id;
                Category = childData.Category;
                DisplayOrder = childData.DisplayOrder;
                RowVersion = childData.RowVersion;
            }
        }

        [Fetch]
        private void Fetch(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ICategoryOfOrganizationDal>();
            var data = dal.Fetch(id);
            
            FetchChild(data);
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
                DisplayOrder = DisplayOrder,
                RowVersion = RowVersion
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