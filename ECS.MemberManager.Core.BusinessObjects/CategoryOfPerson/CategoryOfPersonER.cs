﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class CategoryOfPersonER : BusinessBase<CategoryOfPersonER>
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

        public static async Task<CategoryOfPersonER> NewCategoryOfPerson()
        {
            return await DataPortal.CreateAsync<CategoryOfPersonER>();
        }

        public static async Task<CategoryOfPersonER> GetCategoryOfPerson(int id)
        {
            return await DataPortal.FetchAsync<CategoryOfPersonER>(id);
        }

        public static async Task DeleteCategoryOfPerson(int id)
        {
            await DataPortal.DeleteAsync<CategoryOfPersonER>(id);
        }
 
        #endregion

        #region Data Access
        
        [Create]
        [RunLocal]
        private void Create()
        {
            BusinessRules.CheckRules();
        }
        
        [Fetch]
        private void Fetch(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ICategoryOfPersonDal>();
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
            var dal = dalManager.GetProvider<ICategoryOfPersonDal>();
            using (BypassPropertyChecks)
            {
                var categoryToInsert = new EF.Domain.CategoryOfPerson()
                {
                    Id = Id,
                    DisplayOrder = DisplayOrder,
                    Category = Category
                };
                Id = dal.Insert(categoryToInsert);
            }
        }
        
        [Update]
        private void Update()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ICategoryOfPersonDal>();
            using (BypassPropertyChecks)
            {
                var categoryToUpdate = dal.Fetch(Id);
                categoryToUpdate.Category = Category;
                categoryToUpdate.DisplayOrder = DisplayOrder;
                    
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
            var dal = dalManager.GetProvider<ICategoryOfPersonDal>();
 
            dal.Delete(id);
        }

        #endregion

 
    }
}