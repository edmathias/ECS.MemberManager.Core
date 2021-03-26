

//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 03/25/2021 11:08:18
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
    public partial class OrganizationER : BusinessBase<OrganizationER>
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


        public static readonly PropertyInfo<OrganizationTypeEC> OrganizationTypeProperty = RegisterProperty<OrganizationTypeEC>(o => o.OrganizationType);
        public OrganizationTypeEC OrganizationType  
        {
            get => GetProperty(OrganizationTypeProperty); 
            set => SetProperty(OrganizationTypeProperty, value); 
        }    
 
        public static readonly PropertyInfo<byte[]> RowVersionProperty = RegisterProperty<byte[]>(o => o.RowVersion);
        public virtual byte[] RowVersion 
        {
            get => GetProperty(RowVersionProperty); 
            set => SetProperty(RowVersionProperty, value); 
   
        }

        #endregion 

        #region Factory Methods
        public static async Task<OrganizationER> NewOrganizationER()
        {
            return await DataPortal.CreateAsync<OrganizationER>();
        }

        public static async Task<OrganizationER> GetOrganizationER(int id)
        {
            return await DataPortal.FetchAsync<OrganizationER>(id);
        }  

        public static async Task DeleteOrganizationER(int id)
        {
            await DataPortal.DeleteAsync<OrganizationER>(id);
        } 


        #endregion

        #region Data Access Methods

        [Fetch]
        private async Task Fetch(int id, [Inject] IOrganizationDal dal)
        {
            var data = await dal.Fetch(id);

            using(BypassPropertyChecks)
            {
            Id = data.Id;
            Name = data.Name;
            DateOfFirstContact = data.DateOfFirstContact;
            LastUpdatedBy = data.LastUpdatedBy;
            LastUpdatedDate = data.LastUpdatedDate;
            Notes = data.Notes;
            OrganizationType = (data.OrganizationType != null ? await OrganizationTypeEC.GetOrganizationTypeEC(data.OrganizationType) : null);
            RowVersion = data.RowVersion;
            }            
        }
        [Insert]
        private async Task Insert([Inject] IOrganizationDal dal)
        {
            FieldManager.UpdateChildren();

            var data = new Organization()
            {

                Id = Id,
                Name = Name,
                DateOfFirstContact = DateOfFirstContact,
                LastUpdatedBy = LastUpdatedBy,
                LastUpdatedDate = LastUpdatedDate,
                Notes = Notes,
                OrganizationType = (OrganizationType != null ? new OrganizationType() { Id = OrganizationType.Id } : null),
                RowVersion = RowVersion,
            };

            var insertedObj = await dal.Insert(data);
            Id = insertedObj.Id;
            RowVersion = insertedObj.RowVersion;
        }

       [Update]
        private async Task Update([Inject] IOrganizationDal dal)
        {
            FieldManager.UpdateChildren();

            var data = new Organization()
            {

                Id = Id,
                Name = Name,
                DateOfFirstContact = DateOfFirstContact,
                LastUpdatedBy = LastUpdatedBy,
                LastUpdatedDate = LastUpdatedDate,
                Notes = Notes,
                OrganizationType = (OrganizationType != null ? new OrganizationType() { Id = OrganizationType.Id } : null),
                RowVersion = RowVersion,
            };

            var insertedObj = await dal.Update(data);
            RowVersion = insertedObj.RowVersion;
        }

        [DeleteSelf]
        private async Task DeleteSelf([Inject] IOrganizationDal dal)
        {
            await Delete(Id,dal);
        }
       
        [Delete]
        private async Task Delete(int id, [Inject] IOrganizationDal dal)
        {
            await dal.Delete(id);
        }

        #endregion
    }
}
