using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class ContactForSponsorROC : ReadOnlyBase<ContactForSponsorROC>
    {
        #region Business Methods

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id);

        public int Id
        {
            get => GetProperty(IdProperty);
            private set => LoadProperty(IdProperty, value);
        }

        public static readonly PropertyInfo<int> SponsorIdProperty = RegisterProperty<int>(p => p.SponsorId);

        public int SponsorId
        {
            get => GetProperty(SponsorIdProperty);
            private set => LoadProperty(SponsorIdProperty, value);
        }

        public static readonly PropertyInfo<SmartDate> DateWhenContactedProperty =
            RegisterProperty<SmartDate>(p => p.DateWhenContacted);

        public SmartDate DateWhenContacted
        {
            get => GetProperty(DateWhenContactedProperty);
            private set => LoadProperty(DateWhenContactedProperty, value);
        }

        public static readonly PropertyInfo<string> PurposeProperty = RegisterProperty<string>(p => p.Purpose);

        [MaxLength(255)]
        public string Purpose
        {
            get => GetProperty(PurposeProperty);
            private set => LoadProperty(PurposeProperty, value);
        }

        public static readonly PropertyInfo<string> RecordOfDiscussionProperty =
            RegisterProperty<string>(p => p.RecordOfDiscussion);

        public string RecordOfDiscussion
        {
            get => GetProperty(RecordOfDiscussionProperty);
            private set => LoadProperty(RecordOfDiscussionProperty, value);
        }

        public static readonly PropertyInfo<int> PersonIdProperty = RegisterProperty<int>(p => p.PersonId);

        public int PersonId
        {
            get => GetProperty(PersonIdProperty);
            private set => LoadProperty(PersonIdProperty, value);
        }

        public static readonly PropertyInfo<int> ContactForSponsorTypeIdProperty =
            RegisterProperty<int>(p => p.ContactForSponsorTypeId);

        public int ContactForSponsorTypeId
        {
            get => GetProperty(ContactForSponsorTypeIdProperty);
            private set => LoadProperty(ContactForSponsorTypeIdProperty, value);
        }

        public static readonly PropertyInfo<string> LastUpdatedByProperty =
            RegisterProperty<string>(p => p.LastUpdatedBy);

        [Required, MaxLength(255)]
        public string LastUpdatedBy
        {
            get => GetProperty(LastUpdatedByProperty);
            private set => LoadProperty(LastUpdatedByProperty, value);
        }

        public static readonly PropertyInfo<SmartDate> LastUpdatedDateProperty =
            RegisterProperty<SmartDate>(p => p.LastUpdatedDate);

        [Required]
        public SmartDate LastUpdatedDate
        {
            get => GetProperty(LastUpdatedDateProperty);
            private set => LoadProperty(LastUpdatedDateProperty, value);
        }

        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(p => p.Notes);

        public string Notes
        {
            get => GetProperty(NotesProperty);
            private set => LoadProperty(NotesProperty, value);
        }

        public static readonly PropertyInfo<byte[]> RowVersionProperty = RegisterProperty<byte[]>(p => p.RowVersion);

        public byte[] RowVersion
        {
            get => GetProperty(RowVersionProperty);
            private set => LoadProperty(RowVersionProperty, value);
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

        public static async Task<ContactForSponsorROC> GetContactForSponsorROC(ContactForSponsor childData)
        {
            return await DataPortal.FetchChildAsync<ContactForSponsorROC>(childData);
        }

        #endregion

        #region Data Access Methods

        [FetchChild]
        private void Fetch(ContactForSponsor childData)
        {
            Id = childData.Id;
            SponsorId = childData.SponsorId;
            DateWhenContacted = childData.DateWhenContacted;
            Purpose = childData.Purpose;
            RecordOfDiscussion = childData.RecordOfDiscussion;
            PersonId = childData.PersonId;
            LastUpdatedBy = childData.LastUpdatedBy;
            LastUpdatedDate = childData.LastUpdatedDate;
            Notes = childData.Notes;
            RowVersion = childData.RowVersion;
        }

        #endregion
    }
}