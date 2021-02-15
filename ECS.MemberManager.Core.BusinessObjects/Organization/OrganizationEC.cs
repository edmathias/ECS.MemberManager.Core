﻿


using System;
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
            get => GetProperty(IdProperty); 
            private set => LoadProperty(IdProperty, value); 
   
        }        
        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(o => o.Name);
        public virtual string Name 
        {
            get => GetProperty(NameProperty); 
            set => SetProperty(NameProperty, value); 
   
        }        
        public static readonly PropertyInfo<OrganizationTypeEC> OrganizationTypeProperty = RegisterProperty<OrganizationTypeEC>(o => o.OrganizationType);
        public OrganizationTypeEC OrganizationType 
        {
            get => GetProperty(OrganizationTypeProperty); 
            set => SetProperty(OrganizationTypeProperty, value); 
        }        

        public static readonly PropertyInfo<SmartDate> DateOfFirstContactProperty = RegisterProperty<SmartDate>(o => o.DateOfFirstContact);
        public virtual SmartDate DateOfFirstContact 
        {
            get => GetProperty(DateOfFirstContactProperty); 
            set => SetProperty(DateOfFirstContactProperty, value); 
   
        }        
        public static readonly PropertyInfo<string> LastUpdatedByProperty = RegisterProperty<string>(o => o.LastUpdatedBy);
        public virtual string LastUpdatedBy 
        {
            get => GetProperty(LastUpdatedByProperty); 
            set => SetProperty(LastUpdatedByProperty, value); 
   
        }        
        public static readonly PropertyInfo<SmartDate> LastUpdatedDateProperty = RegisterProperty<SmartDate>(o => o.LastUpdatedDate);
        public virtual SmartDate LastUpdatedDate 
        {
            get => GetProperty(LastUpdatedDateProperty); 
            set => SetProperty(LastUpdatedDateProperty, value); 
   
        }        
        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(o => o.Notes);
        public virtual string Notes 
        {
            get => GetProperty(NotesProperty); 
            set => SetProperty(NotesProperty, value); 
   
        }        
        public static readonly PropertyInfo<byte[]> RowVersionProperty = RegisterProperty<byte[]>(o => o.RowVersion);
        public virtual byte[] RowVersion 
        {
            get => GetProperty(RowVersionProperty); 
            set => SetProperty(RowVersionProperty, value); 
   
        }        
        #endregion 

        #region Factory Methods

        public static async Task<OrganizationEC> NewOrganizationEC()
        {
            return await DataPortal.CreateChildAsync<OrganizationEC>();
        }

        public static async Task<OrganizationEC> GetOrganizationEC(Organization childData)
        {
            return await DataPortal.FetchChildAsync<OrganizationEC>(childData);
        }

        #endregion

        #region Data Access Methods

        [FetchChild]
        private async Task Fetch(Organization childData)
        {
            using (BypassPropertyChecks)
            {
                Id = childData.Id;
                Name = childData.Name;
                if(childData.OrganizationType != null )
                {
                    OrganizationType = await OrganizationTypeEC.GetOrganizationTypeEC(childData.OrganizationType);
                }
                DateOfFirstContact = childData.DateOfFirstContact;
                LastUpdatedBy = childData.LastUpdatedBy;
                LastUpdatedDate = childData.LastUpdatedDate;
                Notes = childData.Notes;
                RowVersion = childData.RowVersion;
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
                OrganizationType = (OrganizationType != null ? new OrganizationType() { Id = OrganizationType.Id } : null),
                DateOfFirstContact = DateOfFirstContact,
                LastUpdatedBy = LastUpdatedBy,
                LastUpdatedDate = LastUpdatedDate,
                Notes = Notes,
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

            var objToUpdate = new Organization()
            {
                Id = Id,
                Name = Name,
                OrganizationType = (OrganizationType != null ? new OrganizationType() { Id = OrganizationType.Id } : null),
                DateOfFirstContact = DateOfFirstContact,
                LastUpdatedBy = LastUpdatedBy,
                LastUpdatedDate = LastUpdatedDate,
                Notes = Notes,
                RowVersion = RowVersion,
        
            };

            var updatedObj = await dal.Update(objToUpdate);
            RowVersion = updatedObj.RowVersion;
        }

        [DeleteSelfChild]
        private async Task DeleteSelf()
        {
            await Delete(Id);
        }
        
        private async Task Delete(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IOrganizationDal>();
           
            await dal.Delete(id);
        }


        #endregion
    }
}
