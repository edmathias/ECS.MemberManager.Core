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
    public class CategoryOfPersonEC : BusinessBase<CategoryOfPersonEC>
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

        internal static async Task<CategoryOfPersonEC> NewCategoryOfPerson()
        {
            return await DataPortal.CreateAsync<CategoryOfPersonEC>();
        }

        internal static async Task<CategoryOfPersonEC> GetCategoryOfPerson(CategoryOfPerson categoryOfPerson)
        {
            return await DataPortal.FetchChildAsync<CategoryOfPersonEC>(categoryOfPerson);
        }

        internal static async Task DeleteCategoryOfPerson(int id)
        {
            await DataPortal.DeleteAsync<CategoryOfPersonEC>(id);
        }
 
        #endregion

        #region Data Access

        [FetchChild]
        private void FetchChild(CategoryOfPerson categoryOfPerson)
        {
            Id = categoryOfPerson.Id;
            Category = categoryOfPerson.Category;
            DisplayOrder = categoryOfPerson.DisplayOrder;

        }

        [InsertChild]
        private void InsertChild()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ICategoryOfPersonDal>();
            using (BypassPropertyChecks)
            {
                var categoryToInsert = new EF.Domain.CategoryOfPerson()
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
            var dal = dalManager.GetProvider<ICategoryOfPersonDal>();
            using (BypassPropertyChecks)
            {
                var categoryToUpdate = new EF.Domain.CategoryOfPerson()
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
            var dal = dalManager.GetProvider<ICategoryOfPersonDal>();
 
            dal.Delete(id);
        }

        #endregion

 
    }
}