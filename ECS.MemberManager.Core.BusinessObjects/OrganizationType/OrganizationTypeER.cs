

//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 03/02/2021 21:50:23
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
    public partial class OrganizationTypeER : BusinessBase<OrganizationTypeER>
    {
        #region Business Methods
 
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(o => o.Id);
        public virtual int Id 
        {
            get => GetProperty(IdProperty); 
            private set => LoadProperty(IdProperty, value);    
        }

        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(o => o.Name);
        public virtual string Name 
        {
            get => GetProperty(NameProperty); 
            set => SetProperty(NameProperty, value); 
   
        }

        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(o => o.Notes);
        public virtual string Notes 
        {
            get => GetProperty(NotesProperty); 
            set => SetProperty(NotesProperty, value); 
   
        }


        public static readonly PropertyInfo<CategoryOfOrganizationEC> CategoryOfOrganizationProperty = RegisterProperty<CategoryOfOrganizationEC>(o => o.CategoryOfOrganization);
        public CategoryOfOrganizationEC CategoryOfOrganization  
        {
            get => GetProperty(CategoryOfOrganizationProperty); 
            set => SetProperty(CategoryOfOrganizationProperty, value); 
        }    
 
        public static readonly PropertyInfo<byte[]> RowVersionProperty = RegisterProperty<byte[]>(o => o.RowVersion);
        public virtual byte[] RowVersion 
        {
            get => GetProperty(RowVersionProperty); 
            set => SetProperty(RowVersionProperty, value); 
   
        }

        #endregion 

        #region Factory Methods
        public static async Task<OrganizationTypeER> NewOrganizationTypeER()
        {
            return await DataPortal.CreateAsync<OrganizationTypeER>();
        }

        public static async Task<OrganizationTypeER> GetOrganizationTypeER(int id)
        {
            return await DataPortal.FetchAsync<OrganizationTypeER>(id);
        }  

        public static async Task DeleteOrganizationTypeER(int id)
        {
            await DataPortal.DeleteAsync<OrganizationTypeER>(id);
        } 


        #endregion

        #region Data Access Methods

        [Fetch]
        private async Task Fetch(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IOrganizationTypeDal>();
            var data = await dal.Fetch(id);
            using(BypassPropertyChecks)
            {
                Id = data.Id;
                Name = data.Name;
                Notes = data.Notes;
                CategoryOfOrganization = (data.CategoryOfOrganization != null ? await CategoryOfOrganizationEC.GetCategoryOfOrganizationEC(data.CategoryOfOrganization) : null);
                RowVersion = data.RowVersion;
            }            
        }
        [Insert]
        private async Task Insert()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IOrganizationTypeDal>();
            var data = new OrganizationType()
            {

                Id = Id,
                Name = Name,
                Notes = Notes,
                CategoryOfOrganization = (CategoryOfOrganization != null ? new CategoryOfOrganization() { Id = CategoryOfOrganization.Id } : null),
                RowVersion = RowVersion,
            };

            var insertedObj = await dal.Insert(data);
            Id = insertedObj.Id;
            RowVersion = insertedObj.RowVersion;
        }

       [Update]
        private async Task Update()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IOrganizationTypeDal>();
            var data = new OrganizationType()
            {

                Id = Id,
                Name = Name,
                Notes = Notes,
                CategoryOfOrganization = (CategoryOfOrganization != null ? new CategoryOfOrganization() { Id = CategoryOfOrganization.Id } : null),
                RowVersion = RowVersion,
            };

            var insertedObj = await dal.Update(data);
            Id = insertedObj.Id;
            RowVersion = insertedObj.RowVersion;
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
            var dal = dalManager.GetProvider<IOrganizationTypeDal>();
           
            await dal.Delete(id);
        }

        #endregion
    }
}
