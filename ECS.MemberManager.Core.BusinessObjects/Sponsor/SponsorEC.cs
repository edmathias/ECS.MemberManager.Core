

//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 03/18/2021 16:28:38
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
    public partial class SponsorEC : BusinessBase<SponsorEC>
    {
        #region Business Methods
 
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(o => o.Id);
        public virtual int Id 
        {
            get => GetProperty(IdProperty); 
            private set => LoadProperty(IdProperty, value);    
        }


        public static readonly PropertyInfo<PersonEC> PersonProperty = RegisterProperty<PersonEC>(o => o.Person);
        public PersonEC Person  
        {
            get => GetProperty(PersonProperty); 
            set => SetProperty(PersonProperty, value); 
        }    
 

        public static readonly PropertyInfo<OrganizationEC> OrganizationProperty = RegisterProperty<OrganizationEC>(o => o.Organization);
        public OrganizationEC Organization  
        {
            get => GetProperty(OrganizationProperty); 
            set => SetProperty(OrganizationProperty, value); 
        }    
 
        public static readonly PropertyInfo<string> StatusProperty = RegisterProperty<string>(o => o.Status);
        public virtual string Status 
        {
            get => GetProperty(StatusProperty); 
            set => SetProperty(StatusProperty, value); 
   
        }

        public static readonly PropertyInfo<SmartDate> DateOfFirstContactProperty = RegisterProperty<SmartDate>(o => o.DateOfFirstContact);
        public virtual SmartDate DateOfFirstContact 
        {
            get => GetProperty(DateOfFirstContactProperty); 
            set => SetProperty(DateOfFirstContactProperty, value); 
   
        }

        public static readonly PropertyInfo<string> ReferredByProperty = RegisterProperty<string>(o => o.ReferredBy);
        public virtual string ReferredBy 
        {
            get => GetProperty(ReferredByProperty); 
            set => SetProperty(ReferredByProperty, value); 
   
        }

        public static readonly PropertyInfo<SmartDate> DateSponsorAcceptedProperty = RegisterProperty<SmartDate>(o => o.DateSponsorAccepted);
        public virtual SmartDate DateSponsorAccepted 
        {
            get => GetProperty(DateSponsorAcceptedProperty); 
            set => SetProperty(DateSponsorAcceptedProperty, value); 
   
        }

        public static readonly PropertyInfo<string> TypeNameProperty = RegisterProperty<string>(o => o.TypeName);
        public virtual string TypeName 
        {
            get => GetProperty(TypeNameProperty); 
            set => SetProperty(TypeNameProperty, value); 
   
        }

        public static readonly PropertyInfo<string> DetailsProperty = RegisterProperty<string>(o => o.Details);
        public virtual string Details 
        {
            get => GetProperty(DetailsProperty); 
            set => SetProperty(DetailsProperty, value); 
   
        }

        public static readonly PropertyInfo<SmartDate> SponsorUntilDateProperty = RegisterProperty<SmartDate>(o => o.SponsorUntilDate);
        public virtual SmartDate SponsorUntilDate 
        {
            get => GetProperty(SponsorUntilDateProperty); 
            set => SetProperty(SponsorUntilDateProperty, value); 
   
        }

        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(o => o.Notes);
        public virtual string Notes 
        {
            get => GetProperty(NotesProperty); 
            set => SetProperty(NotesProperty, value); 
   
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

        public static readonly PropertyInfo<byte[]> RowVersionProperty = RegisterProperty<byte[]>(o => o.RowVersion);
        public virtual byte[] RowVersion 
        {
            get => GetProperty(RowVersionProperty); 
            set => SetProperty(RowVersionProperty, value); 
   
        }

        #endregion 

        #region Factory Methods
        internal static async Task<SponsorEC> NewSponsorEC()
        {
            return await DataPortal.CreateChildAsync<SponsorEC>();
        }

        internal static async Task<SponsorEC> GetSponsorEC(Sponsor childData)
        {
            return await DataPortal.FetchChildAsync<SponsorEC>(childData);
        }  


        #endregion

        #region Data Access Methods

        [FetchChild]
        private async Task Fetch(Sponsor data)
        {
            using(BypassPropertyChecks)
            {
            Id = data.Id;
            Person = (data.Person != null ? await PersonEC.GetPersonEC(data.Person) : null);
            Organization = (data.Organization != null ? await OrganizationEC.GetOrganizationEC(data.Organization) : null);
            Status = data.Status;
            DateOfFirstContact = data.DateOfFirstContact;
            ReferredBy = data.ReferredBy;
            DateSponsorAccepted = data.DateSponsorAccepted;
            TypeName = data.TypeName;
            Details = data.Details;
            SponsorUntilDate = data.SponsorUntilDate;
            Notes = data.Notes;
            LastUpdatedBy = data.LastUpdatedBy;
            LastUpdatedDate = data.LastUpdatedDate;
            RowVersion = data.RowVersion;
            }            
        }
        [InsertChild]
        private async Task Insert([Inject] ISponsorDal dal)
        {
            var data = new Sponsor()
            {

                Id = Id,
                Person = (Person != null ? new Person() { Id = Person.Id } : null),
                Organization = (Organization != null ? new Organization() { Id = Organization.Id } : null),
                Status = Status,
                DateOfFirstContact = DateOfFirstContact,
                ReferredBy = ReferredBy,
                DateSponsorAccepted = DateSponsorAccepted,
                TypeName = TypeName,
                Details = Details,
                SponsorUntilDate = SponsorUntilDate,
                Notes = Notes,
                LastUpdatedBy = LastUpdatedBy,
                LastUpdatedDate = LastUpdatedDate,
                RowVersion = RowVersion,
            };

            var insertedObj = await dal.Insert(data);
            Id = insertedObj.Id;
            RowVersion = insertedObj.RowVersion;
        }

       [UpdateChild]
        private async Task Update([Inject] ISponsorDal dal)
        {
            var data = new Sponsor()
            {

                Id = Id,
                Person = (Person != null ? new Person() { Id = Person.Id } : null),
                Organization = (Organization != null ? new Organization() { Id = Organization.Id } : null),
                Status = Status,
                DateOfFirstContact = DateOfFirstContact,
                ReferredBy = ReferredBy,
                DateSponsorAccepted = DateSponsorAccepted,
                TypeName = TypeName,
                Details = Details,
                SponsorUntilDate = SponsorUntilDate,
                Notes = Notes,
                LastUpdatedBy = LastUpdatedBy,
                LastUpdatedDate = LastUpdatedDate,
                RowVersion = RowVersion,
            };

            var insertedObj = await dal.Update(data);
            RowVersion = insertedObj.RowVersion;
        }

       
        [DeleteSelfChild]
        private async Task DeleteSelf([Inject] ISponsorDal dal)
        {
            await Delete(Id,dal);
        }
       
        [Delete]
        private async Task Delete(int id, [Inject] ISponsorDal dal)
        {
            await dal.Delete(id);
        }

        #endregion
    }
}
