﻿

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
    public partial class OrganizationEC : BusinessBase<OrganizationEC>
    {
        #region Business Methods
 
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(o => o.Id);
        public virtual int Id 
        {
            get => GetProperty(IdProperty); //1-2
            private set => LoadProperty(IdProperty, value); //2-3   
        }

        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(o => o.Name);
        public virtual string Name 
        {
            get => GetProperty(NameProperty); //1-2
            set => SetProperty(NameProperty, value); //2-4
   
        }

        public static readonly PropertyInfo<SmartDate> DateOfFirstContactProperty = RegisterProperty<SmartDate>(o => o.DateOfFirstContact);
        public virtual SmartDate DateOfFirstContact 
        {
            get => GetProperty(DateOfFirstContactProperty); //1-2
            set => SetProperty(DateOfFirstContactProperty, value); //2-4
   
        }

        public static readonly PropertyInfo<string> LastUpdatedByProperty = RegisterProperty<string>(o => o.LastUpdatedBy);
        public virtual string LastUpdatedBy 
        {
            get => GetProperty(LastUpdatedByProperty); //1-2
            set => SetProperty(LastUpdatedByProperty, value); //2-4
   
        }

        public static readonly PropertyInfo<SmartDate> LastUpdatedDateProperty = RegisterProperty<SmartDate>(o => o.LastUpdatedDate);
        public virtual SmartDate LastUpdatedDate 
        {
            get => GetProperty(LastUpdatedDateProperty); //1-2
            set => SetProperty(LastUpdatedDateProperty, value); //2-4
   
        }

        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(o => o.Notes);
        public virtual string Notes 
        {
            get => GetProperty(NotesProperty); //1-2
            set => SetProperty(NotesProperty, value); //2-4
   
        }


        public static readonly PropertyInfo<OrganizationTypeEC> OrganizationTypeProperty = RegisterProperty<OrganizationTypeEC>(o => o.OrganizationType);
        public OrganizationTypeEC OrganizationType  
        {
            get => GetProperty(OrganizationTypeProperty); //1-1
            set => SetProperty(OrganizationTypeProperty, value); //2-2
        }    
 

        public static readonly PropertyInfo<CategoryOfOrganizationEC> CategoryOfOrganizationProperty = RegisterProperty<CategoryOfOrganizationEC>(o => o.CategoryOfOrganization);
        public CategoryOfOrganizationEC CategoryOfOrganization  
        {
            get => GetProperty(CategoryOfOrganizationProperty); //1-1
            set => SetProperty(CategoryOfOrganizationProperty, value); //2-2
        }    
 
        public static readonly PropertyInfo<byte[]> RowVersionProperty = RegisterProperty<byte[]>(o => o.RowVersion);
        public virtual byte[] RowVersion 
        {
            get => GetProperty(RowVersionProperty); //1-2
            set => SetProperty(RowVersionProperty, value); //2-4
   
        }

        #endregion 

        #region Factory Methods
        internal static async Task<OrganizationEC> NewOrganizationEC()
        {
            return await DataPortal.CreateChildAsync<OrganizationEC>();
        }

        internal static async Task<OrganizationEC> GetOrganizationEC(Organization childData)
        {
            return await DataPortal.FetchChildAsync<OrganizationEC>(childData);
        }  


        #endregion

        #region Data Access Methods

        [FetchChild]
        private async Task Fetch(Organization data)
        {
            using(BypassPropertyChecks)
            {
                Id = data.Id;
                Name = data.Name;
                DateOfFirstContact = data.DateOfFirstContact;
                LastUpdatedBy = data.LastUpdatedBy;
                LastUpdatedDate = data.LastUpdatedDate;
                Notes = data.Notes;
                OrganizationType = (data.OrganizationType != null ? await OrganizationTypeEC.GetOrganizationTypeEC(data.OrganizationType) : null);
                CategoryOfOrganization = (data.CategoryOfOrganization != null ? await CategoryOfOrganizationEC.GetCategoryOfOrganizationEC(data.CategoryOfOrganization) : null);
                RowVersion = data.RowVersion;
            }            
        }
        [InsertChild]
        private async Task Insert()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IOrganizationDal>();
            var data = new Organization()
            {

                Id = Id,
                Name = Name,
                DateOfFirstContact = DateOfFirstContact,
                LastUpdatedBy = LastUpdatedBy,
                LastUpdatedDate = LastUpdatedDate,
                Notes = Notes,
                OrganizationType = (OrganizationType != null ? new OrganizationType() { Id = OrganizationType.Id } : null),
                CategoryOfOrganization = (CategoryOfOrganization != null ? new CategoryOfOrganization() { Id = CategoryOfOrganization.Id } : null),
                RowVersion = RowVersion,
            };

            var insertedObj = await dal.Insert(data);
            Id = insertedObj.Id;
            RowVersion = insertedObj.RowVersion;
        }

       [UpdateChild]
        private async Task Update()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IOrganizationDal>();
            var data = new Organization()
            {

                Id = Id,
                Name = Name,
                DateOfFirstContact = DateOfFirstContact,
                LastUpdatedBy = LastUpdatedBy,
                LastUpdatedDate = LastUpdatedDate,
                Notes = Notes,
                OrganizationType = (OrganizationType != null ? new OrganizationType() { Id = OrganizationType.Id } : null),
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
            var dal = dalManager.GetProvider<IOrganizationDal>();
           
            await dal.Delete(id);
        }

        #endregion
    }
}
