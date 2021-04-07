

//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 04/07/2021 09:31:26
//******************************************************************************    

using System;
using System.Collections.Generic; 
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class CategoryOfOrganizationER : BusinessBase<CategoryOfOrganizationER>
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
        public static async Task<CategoryOfOrganizationER> NewCategoryOfOrganizationER()
        {
            return await DataPortal.CreateAsync<CategoryOfOrganizationER>();
        }

        public static async Task<CategoryOfOrganizationER> GetCategoryOfOrganizationER(int id)
        {
            return await DataPortal.FetchAsync<CategoryOfOrganizationER>(id);
        }  

        public static async Task DeleteCategoryOfOrganizationER(int id)
        {
            await DataPortal.DeleteAsync<CategoryOfOrganizationER>(id);
        } 


        #endregion

        #region Data Access Methods

        [Fetch]
        private async Task Fetch(int id, [Inject] IDal<CategoryOfOrganization> dal)
        {
            var data = await dal.Fetch(id);

            using(BypassPropertyChecks)
            {
            Id = data.Id;
            Category = data.Category;
            DisplayOrder = data.DisplayOrder;
            RowVersion = data.RowVersion;
            }            
        }
        [Insert]
        private async Task Insert([Inject] IDal<CategoryOfOrganization> dal)
        {
            FieldManager.UpdateChildren();

            var data = new CategoryOfOrganization()
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
        private async Task Update([Inject] IDal<CategoryOfOrganization> dal)
        {
            FieldManager.UpdateChildren();

            var data = new CategoryOfOrganization()
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
        private async Task DeleteSelf([Inject] IDal<CategoryOfOrganization> dal)
        {
            await Delete(Id,dal);
        }
       
        [Delete]
        private async Task Delete(int id, [Inject] IDal<CategoryOfOrganization> dal)
        {
            await dal.Delete(id);
        }

        #endregion
    }
}
