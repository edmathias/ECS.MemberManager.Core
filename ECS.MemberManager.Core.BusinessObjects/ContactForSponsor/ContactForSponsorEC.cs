﻿

//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 04/07/2021 09:31:33
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
    public partial class ContactForSponsorEC : BusinessBase<ContactForSponsorEC>
    {
        #region Business Methods
 
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(o => o.Id);
        public virtual int Id 
        {
            get => GetProperty(IdProperty); 
            private set => LoadProperty(IdProperty, value);    
        }

        public static readonly PropertyInfo<SmartDate> DateWhenContactedProperty = RegisterProperty<SmartDate>(o => o.DateWhenContacted);
        public virtual SmartDate DateWhenContacted 
        {
            get => GetProperty(DateWhenContactedProperty); 
            set => SetProperty(DateWhenContactedProperty, value); 
   
        }

        public static readonly PropertyInfo<string> PurposeProperty = RegisterProperty<string>(o => o.Purpose);
        public virtual string Purpose 
        {
            get => GetProperty(PurposeProperty); 
            set => SetProperty(PurposeProperty, value); 
   
        }

        public static readonly PropertyInfo<string> RecordOfDiscussionProperty = RegisterProperty<string>(o => o.RecordOfDiscussion);
        public virtual string RecordOfDiscussion 
        {
            get => GetProperty(RecordOfDiscussionProperty); 
            set => SetProperty(RecordOfDiscussionProperty, value); 
   
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


        public static readonly PropertyInfo<SponsorEC> SponsorProperty = RegisterProperty<SponsorEC>(o => o.Sponsor);
        public SponsorEC Sponsor  
        {
            get => GetProperty(SponsorProperty); 
            set => SetProperty(SponsorProperty, value); 
        }    
 

        public static readonly PropertyInfo<PersonEC> PersonProperty = RegisterProperty<PersonEC>(o => o.Person);
        public PersonEC Person  
        {
            get => GetProperty(PersonProperty); 
            set => SetProperty(PersonProperty, value); 
        }    
 
        public static readonly PropertyInfo<byte[]> RowVersionProperty = RegisterProperty<byte[]>(o => o.RowVersion);
        public virtual byte[] RowVersion 
        {
            get => GetProperty(RowVersionProperty); 
            set => SetProperty(RowVersionProperty, value); 
   
        }

        #endregion 

        #region Factory Methods
        internal static async Task<ContactForSponsorEC> NewContactForSponsorEC()
        {
            return await DataPortal.CreateChildAsync<ContactForSponsorEC>();
        }

        internal static async Task<ContactForSponsorEC> GetContactForSponsorEC(ContactForSponsor childData)
        {
            return await DataPortal.FetchChildAsync<ContactForSponsorEC>(childData);
        }  


        #endregion

        #region Data Access Methods

        [FetchChild]
        private async Task Fetch(ContactForSponsor data)
        {
            using(BypassPropertyChecks)
            {
            Id = data.Id;
            DateWhenContacted = data.DateWhenContacted;
            Purpose = data.Purpose;
            RecordOfDiscussion = data.RecordOfDiscussion;
            Notes = data.Notes;
            LastUpdatedBy = data.LastUpdatedBy;
            LastUpdatedDate = data.LastUpdatedDate;
            Sponsor = (data.Sponsor != null ? await SponsorEC.GetSponsorEC(data.Sponsor) : null);
            Person = (data.Person != null ? await PersonEC.GetPersonEC(data.Person) : null);
            RowVersion = data.RowVersion;
            }            
        }
        [InsertChild]
        private async Task Insert([Inject] IDal<ContactForSponsor> dal)
        {
            FieldManager.UpdateChildren();

            var data = new ContactForSponsor()
            {

                Id = Id,
                DateWhenContacted = DateWhenContacted,
                Purpose = Purpose,
                RecordOfDiscussion = RecordOfDiscussion,
                Notes = Notes,
                LastUpdatedBy = LastUpdatedBy,
                LastUpdatedDate = LastUpdatedDate,
                Sponsor = (Sponsor != null ? new Sponsor() { Id = Sponsor.Id } : null),
                Person = (Person != null ? new Person() { Id = Person.Id } : null),
                RowVersion = RowVersion,
            };

            var insertedObj = await dal.Insert(data);
            Id = insertedObj.Id;
            RowVersion = insertedObj.RowVersion;
        }

       [UpdateChild]
        private async Task Update([Inject] IDal<ContactForSponsor> dal)
        {
            FieldManager.UpdateChildren();

            var data = new ContactForSponsor()
            {

                Id = Id,
                DateWhenContacted = DateWhenContacted,
                Purpose = Purpose,
                RecordOfDiscussion = RecordOfDiscussion,
                Notes = Notes,
                LastUpdatedBy = LastUpdatedBy,
                LastUpdatedDate = LastUpdatedDate,
                Sponsor = (Sponsor != null ? new Sponsor() { Id = Sponsor.Id } : null),
                Person = (Person != null ? new Person() { Id = Person.Id } : null),
                RowVersion = RowVersion,
            };

            var insertedObj = await dal.Update(data);
            RowVersion = insertedObj.RowVersion;
        }

       
        [DeleteSelfChild]
        private async Task DeleteSelf([Inject] IDal<ContactForSponsor> dal)
        {
            await Delete(Id,dal);
        }
       
        [Delete]
        private async Task Delete(int id, [Inject] IDal<ContactForSponsor> dal)
        {
            await dal.Delete(id);
        }

        #endregion
    }
}
