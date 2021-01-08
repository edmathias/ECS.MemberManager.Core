﻿using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class ContactForSponsorEC : BusinessBase<ContactForSponsorEC>
    {
        #region Business Methods

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id);
        public int Id
        {
            get => GetProperty(IdProperty);
            private set => LoadProperty(IdProperty, value);
        }

        public static readonly PropertyInfo<SmartDate> DateWhenContactedProperty = RegisterProperty<SmartDate>(p => p.DateWhenContacted);
        public SmartDate DateWhenContacted
        {
            get => GetProperty(DateWhenContactedProperty);
            set => SetProperty(DateWhenContactedProperty, value);
        }
        
        public static readonly PropertyInfo<string> PurposeProperty = RegisterProperty<string>(p => p.Purpose);
        public string Purpose
        {
            get => GetProperty(PurposeProperty);
            set => SetProperty(PurposeProperty, value);
        }

        public static readonly PropertyInfo<string> RecordOfDiscussionProperty = RegisterProperty<string>(p => p.RecordOfDiscussion);
        public string RecordOfDiscussion
        {
            get => GetProperty(RecordOfDiscussionProperty);
            set => SetProperty(RecordOfDiscussionProperty, value);
        }

        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(p => p.Notes);
        public string Notes
        {
            get => GetProperty(NotesProperty);
            set => SetProperty(NotesProperty, value);
        }

        public static readonly PropertyInfo<string> LastUpdatedByProperty = RegisterProperty<string>(p => p.LastUpdatedBy);
        public string LastUpdatedBy
        {
            get => GetProperty(LastUpdatedByProperty);
            set => SetProperty(LastUpdatedByProperty, value);
        }
        
        public static readonly PropertyInfo<SmartDate> LastUpdatedDateProperty = RegisterProperty<SmartDate>(p => p.LastUpdatedDate);
        public SmartDate LastUpdatedDate
        {
            get => GetProperty(LastUpdatedDateProperty);
            set => SetProperty(LastUpdatedDateProperty, value);
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

        public static async Task<ContactForSponsorEC> NewContactForSponsor()
        {
            return await DataPortal.CreateChildAsync<ContactForSponsorEC>();
        }

        public static async Task<ContactForSponsorEC> GetContactForSponsor(ContactForSponsor childData)
        {
            return await DataPortal.FetchChildAsync<ContactForSponsorEC>(childData);
        }

        public static async Task DeleteContactForSponsor(int id)
        {
            await DataPortal.DeleteAsync<ContactForSponsorEC>(id);
        }
        
        #endregion

        #region Data Access
        
        [CreateChild]
        private void Create()
        {
            MarkAsChild();
            
            BusinessRules.CheckRules();
        } 
        
        [FetchChild]
        private void Fetch(ContactForSponsor childData)
        {
            using (BypassPropertyChecks)
            {
                Id = childData.Id;
                Notes = childData.Notes;
                Purpose = childData.Purpose;
                DateWhenContacted = childData.DateWhenContacted;
                LastUpdatedBy = childData.LastUpdatedBy;
                RecordOfDiscussion = childData.RecordOfDiscussion;
            }
        }

        [InsertChild]
        private void Insert()
        {
            using IDalManager dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IContactForSponsorDal>();
            var contactToInsert = new ContactForSponsor()
            {
                Notes = Notes,
                DateWhenContacted = DateWhenContacted,
                LastUpdatedBy = LastUpdatedBy,
                LastUpdatedDate = LastUpdatedDate,
                Purpose = Purpose,
                RecordOfDiscussion = RecordOfDiscussion
                
                //TODO: sponsor & person
            };

            Id = dal.Insert(contactToInsert);

        }

        [UpdateChild]
        private void Update()
        {
            using IDalManager dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IContactForSponsorDal>();

            var contactToUpdate = dal.Fetch(Id);

            contactToUpdate.Notes = Notes;
            contactToUpdate.Purpose = Purpose;
            contactToUpdate.DateWhenContacted = DateWhenContacted;
            contactToUpdate.LastUpdatedBy = LastUpdatedDate;
            contactToUpdate.LastUpdatedBy = LastUpdatedBy;
            contactToUpdate.RecordOfDiscussion = RecordOfDiscussion;

            dal.Update(contactToUpdate);
        }

        [DeleteSelfChild]
        private void DeleteSelf()
        {
            Delete(this.Id);
        }

        [Delete]
        private void Delete(int id)
        {
            using IDalManager dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IContactForSponsorDal>();

            dal.Delete(id);
        }

        #endregion

 
    }
}