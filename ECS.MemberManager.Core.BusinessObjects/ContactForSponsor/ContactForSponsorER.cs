using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.BusinessObjects.Sponsor;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class ContactForSponsorER : BusinessBase<ContactForSponsorER>
    {
        #region Business Methods

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id);

        public int Id
        {
            get => GetProperty(IdProperty);
            private set => LoadProperty(IdProperty, value);
        }

        public static readonly PropertyInfo<SmartDate> DateWhenContactedProperty =
            RegisterProperty<SmartDate>(p => p.DateWhenContacted);

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

        public static readonly PropertyInfo<string> RecordOfDiscussionProperty =
            RegisterProperty<string>(p => p.RecordOfDiscussion);

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

        public static readonly PropertyInfo<string> LastUpdatedByProperty =
            RegisterProperty<string>(p => p.LastUpdatedBy);

        public string LastUpdatedBy
        {
            get => GetProperty(LastUpdatedByProperty);
            set => SetProperty(LastUpdatedByProperty, value);
        }

        public static readonly PropertyInfo<SmartDate> LastUpdatedDateProperty =
            RegisterProperty<SmartDate>(p => p.LastUpdatedDate);

        public SmartDate LastUpdatedDate
        {
            get => GetProperty(LastUpdatedDateProperty);
            set => SetProperty(LastUpdatedDateProperty, value);
        }
        
        public static readonly PropertyInfo<SponsorEC> SponsorProperty = RegisterProperty<SponsorEC>(p => p.Sponsor);
        public SponsorEC Sponsor
        {
            get => GetProperty(SponsorProperty);
            set => SetProperty(SponsorProperty, value);
        }

        // TODO: add public properties and methods

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

        public static async Task<ContactForSponsorER> NewContactForSponsor()
        {
            return await DataPortal.CreateAsync<ContactForSponsorER>();
        }

        public static async Task<ContactForSponsorER> GetContactForSponsor(int id)
        {
            return await DataPortal.FetchAsync<ContactForSponsorER>(id);
        }

        public static async Task DeleteContactForSponsorER(int id)
        {
            await DataPortal.DeleteAsync<ContactForSponsorER>(id);
        }

        #endregion

        #region Data Access

        [Fetch]
        private async void Fetch(int id)
        {
            using IDalManager dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IContactForSponsorDal>();
            var data = dal.Fetch(id);
            using (BypassPropertyChecks)
            {
                Id = data.Id;
                Notes = data.Notes;
                Purpose = data.Purpose;
                DateWhenContacted = data.DateWhenContacted;
                LastUpdatedBy = data.LastUpdatedBy;
                LastUpdatedDate = data.LastUpdatedDate;
                RecordOfDiscussion = data.RecordOfDiscussion;
                Sponsor = await SponsorEC.GetSponsor(data.Sponsor);
            }
        }

        [Insert]
        private void Insert()
        {
            using IDalManager dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IContactForSponsorDal>();
            var contactToInsert = new ContactForSponsor()
            {
                Id = Id,
                Notes = Notes,
                Purpose = Purpose,
                DateWhenContacted = DateWhenContacted,
                LastUpdatedBy = LastUpdatedBy,
                LastUpdatedDate = LastUpdatedDate,
                RecordOfDiscussion = RecordOfDiscussion,
                Sponsor = Sponsor,
                
            };
            dal.Insert(contactToInsert);
        }

        [Update]
        private void Update()
        {
            using IDalManager dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IContactForSponsorDal>();
            var contactToUpdate = dal.Fetch(Id);

            contactToUpdate.Id = Id;
            contactToUpdate.Notes = Notes;
            contactToUpdate.Purpose = Purpose;
            contactToUpdate.DateWhenContacted = DateWhenContacted;
            contactToUpdate.LastUpdatedBy = LastUpdatedBy;
            contactToUpdate.LastUpdatedDate = LastUpdatedDate;
            contactToUpdate.RecordOfDiscussion = RecordOfDiscussion;

            dal.Update(contactToUpdate);
        }

        [DeleteSelf]
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