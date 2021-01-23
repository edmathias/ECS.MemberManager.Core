﻿using System;
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
    public class CategoryOfPersonEdit : BusinessBase<CategoryOfPersonEdit>
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

        public static async Task<CategoryOfPersonEdit> NewCategoryOfPersonEdit()
        {
            return await DataPortal.CreateAsync<CategoryOfPersonEdit>();
        }

        public static async Task<CategoryOfPersonEdit> GetCategoryOfPersonEdit(CategoryOfPerson childData)
        {
            return await DataPortal.FetchChildAsync<CategoryOfPersonEdit>(childData);
        }

        public static async Task<CategoryOfPersonEdit> GetCategoryOfPersonEdit(int id)
        {
            return await DataPortal.FetchAsync<CategoryOfPersonEdit>(id);
        }

        public static async Task DeleteCategoryOfPersonEdit(int id)
        {
            await DataPortal.DeleteAsync<CategoryOfPersonEdit>(id);
        }

        #endregion

        #region Data Access Methods

        [FetchChild]
        private void Fetch(CategoryOfPerson childData)
        {
            using (BypassPropertyChecks)
            {
                Id = childData.Id;
                Category = childData.Category;
                RowVersion = childData.RowVersion;
            }
        }

        [Fetch]
        private async Task FetchAsync(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ICategoryOfPersonDal>();
            var data = await dal.Fetch(id);

            Fetch(data);
        }

        [Insert]
        private async Task Insert()
        {
            await InsertChild();
        }

        [InsertChild]
        private async Task InsertChild()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ICategoryOfPersonDal>();
            var data = new CategoryOfPerson()
            {
                Category = Category,
                DisplayOrder = DisplayOrder
            };

            var insertedCategoryOfPerson = await dal.Insert(data);
            Id = insertedCategoryOfPerson.Id;
            RowVersion = insertedCategoryOfPerson.RowVersion;
        }

        [Update]
        private async Task Update()
        {
            await ChildUpdate();
        }

        [UpdateChild]
        private async Task ChildUpdate()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ICategoryOfPersonDal>();

            var categoryOfPersonTypeToUpdate = new CategoryOfPerson()
            {
                Id = Id,
                Category = Category,
                DisplayOrder = DisplayOrder,
                RowVersion = RowVersion
            };

            var updatedEmail = await dal.Update(categoryOfPersonTypeToUpdate);
            RowVersion = updatedEmail.RowVersion;
        }
        
        [DeleteSelfChild]
        private async Task DeleteSelf()
        {
            await Delete(Id);
        }

        [Delete]
        private async Task Delete(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ICategoryOfPersonDal>();
           
            await dal.Delete(id);
        }

        #endregion
    }
}