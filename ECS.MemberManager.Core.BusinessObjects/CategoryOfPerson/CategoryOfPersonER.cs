//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 03/23/2021 09:56:45
//******************************************************************************    

using System;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class CategoryOfPersonER : BusinessBase<CategoryOfPersonER>
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

        public static async Task<CategoryOfPersonER> NewCategoryOfPersonER()
        {
            return await DataPortal.CreateAsync<CategoryOfPersonER>();
        }

        public static async Task<CategoryOfPersonER> GetCategoryOfPersonER(int id)
        {
            return await DataPortal.FetchAsync<CategoryOfPersonER>(id);
        }

        public static async Task DeleteCategoryOfPersonER(int id)
        {
            await DataPortal.DeleteAsync<CategoryOfPersonER>(id);
        }

        #endregion

        #region Data Access Methods

        [Fetch]
        private async Task Fetch(int id, [Inject] ICategoryOfPersonDal dal)
        {
            var data = await dal.Fetch(id);

            using (BypassPropertyChecks)
            {
                Id = data.Id;
                Category = data.Category;
                DisplayOrder = data.DisplayOrder;
                RowVersion = data.RowVersion;
            }
        }

        [Insert]
        private async Task Insert([Inject] ICategoryOfPersonDal dal)
        {
            var data = new CategoryOfPerson()
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

        [Update]
        private async Task Update([Inject] ICategoryOfPersonDal dal)
        {
            var data = new CategoryOfPerson()
            {
                Id = Id,
                Category = Category,
                DisplayOrder = DisplayOrder,
                RowVersion = RowVersion,
            };

            var insertedObj = await dal.Update(data);
            RowVersion = insertedObj.RowVersion;
        }

        [DeleteSelf]
        private async Task DeleteSelf([Inject] ICategoryOfPersonDal dal)
        {
            await Delete(Id, dal);
        }

        [Delete]
        private async Task Delete(int id, [Inject] ICategoryOfPersonDal dal)
        {
            await dal.Delete(id);
        }

        #endregion
    }
}