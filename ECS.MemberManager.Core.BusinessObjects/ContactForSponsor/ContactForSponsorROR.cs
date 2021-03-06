﻿


//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 04/07/2021 09:31:37
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
    public partial class ContactForSponsorROR : BusinessBase<ContactForSponsorROR>
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
            private set => LoadProperty(DateWhenContactedProperty, value);    
        }

        public static readonly PropertyInfo<string> PurposeProperty = RegisterProperty<string>(o => o.Purpose);
        public virtual string Purpose 
        {
            get => GetProperty(PurposeProperty); 
            private set => LoadProperty(PurposeProperty, value);    
        }

        public static readonly PropertyInfo<string> RecordOfDiscussionProperty = RegisterProperty<string>(o => o.RecordOfDiscussion);
        public virtual string RecordOfDiscussion 
        {
            get => GetProperty(RecordOfDiscussionProperty); 
            private set => LoadProperty(RecordOfDiscussionProperty, value);    
        }

        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(o => o.Notes);
        public virtual string Notes 
        {
            get => GetProperty(NotesProperty); 
            private set => LoadProperty(NotesProperty, value);    
        }

        public static readonly PropertyInfo<string> LastUpdatedByProperty = RegisterProperty<string>(o => o.LastUpdatedBy);
        public virtual string LastUpdatedBy 
        {
            get => GetProperty(LastUpdatedByProperty); 
            private set => LoadProperty(LastUpdatedByProperty, value);    
        }

        public static readonly PropertyInfo<SmartDate> LastUpdatedDateProperty = RegisterProperty<SmartDate>(o => o.LastUpdatedDate);
        public virtual SmartDate LastUpdatedDate 
        {
            get => GetProperty(LastUpdatedDateProperty); 
            private set => LoadProperty(LastUpdatedDateProperty, value);    
        }


        public static readonly PropertyInfo<SponsorROC> SponsorProperty = RegisterProperty<SponsorROC>(o => o.Sponsor);
        public SponsorROC Sponsor  
        {
            get => GetProperty(SponsorProperty); 
        
            private set => LoadProperty(SponsorProperty, value); 
        }    
 

        public static readonly PropertyInfo<PersonROC> PersonProperty = RegisterProperty<PersonROC>(o => o.Person);
        public PersonROC Person  
        {
            get => GetProperty(PersonProperty); 
        
            private set => LoadProperty(PersonProperty, value); 
        }    
 
        public static readonly PropertyInfo<byte[]> RowVersionProperty = RegisterProperty<byte[]>(o => o.RowVersion);
        public virtual byte[] RowVersion 
        {
            get => GetProperty(RowVersionProperty); 
            private set => LoadProperty(RowVersionProperty, value);    
        }

        #endregion 

        #region Factory Methods
        public static async Task<ContactForSponsorROR> GetContactForSponsorROR(int id)
        {
            return await DataPortal.FetchAsync<ContactForSponsorROR>(id);
        }  


        #endregion

        #region Data Access Methods

        [Fetch]
        private async Task Fetch(int id, [Inject] IDal<ContactForSponsor> dal)
        {
            var data = await dal.Fetch(id);

            Id = data.Id;
            DateWhenContacted = data.DateWhenContacted;
            Purpose = data.Purpose;
            RecordOfDiscussion = data.RecordOfDiscussion;
            Notes = data.Notes;
            LastUpdatedBy = data.LastUpdatedBy;
            LastUpdatedDate = data.LastUpdatedDate;
            Sponsor = (data.Sponsor != null ? await SponsorROC.GetSponsorROC(data.Sponsor) : null);
            Person = (data.Person != null ? await PersonROC.GetPersonROC(data.Person) : null);
            RowVersion = data.RowVersion;
        }

        #endregion
    }
}
