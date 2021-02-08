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
    public class ContactForSponsorEC : BusinessBase<ContactForSponsorEC>
    {
        #region Business Methods

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id);

        public int Id
        {
            get => GetProperty(IdProperty);
            set => SetProperty(IdProperty, value);
        }
     
        public static readonly PropertyInfo<Sponsor> SponsorProperty = RegisterProperty<Sponsor>(p => p.Sponsor);
        public Sponsor Sponsor
        {
            get => GetProperty(SponsorProperty);
            set => SetProperty(SponsorProperty, value);
        }

        public static readonly PropertyInfo<SmartDate> DateWhenContactedProperty = RegisterProperty<SmartDate>(p => p.DateWhenContacted);
        public SmartDate DateWhenContacted
        {
            get => GetProperty(DateWhenContactedProperty);
            set => SetProperty(DateWhenContactedProperty, value);
        }

        public static readonly PropertyInfo<string> PurposeProperty = RegisterProperty<string>(p => p.Purpose);
        [MaxLength(255)]
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

        public static readonly PropertyInfo<Person> PersonProperty = RegisterProperty<Person>(p => p.Person);
        public Person Person
        {
            get => GetProperty(PersonProperty);
            set => SetProperty(PersonProperty, value);
        }

        public static readonly PropertyInfo<string> LastUpdatedByProperty = RegisterProperty<string>(p => p.LastUpdatedBy);
        [Required,MaxLength(255)]
        public string LastUpdatedBy
        {
            get => GetProperty(LastUpdatedByProperty);
            set => SetProperty(LastUpdatedByProperty, value);
        }

        public static readonly PropertyInfo<SmartDate> LastUpdatedDateProperty = RegisterProperty<SmartDate>(p => p.LastUpdatedDate);
        [Required]
        public SmartDate LastUpdatedDate
        {
            get => GetProperty(LastUpdatedDateProperty);
            set => SetProperty(LastUpdatedDateProperty, value);
        }

        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(p => p.Notes);

        public string Notes
        {
            get => GetProperty(NotesProperty);
            set => SetProperty(NotesProperty, value);
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
        
        internal static async Task<ContactForSponsorEC> NewContactForSponsorEC()
        {
            return await DataPortal.CreateChildAsync<ContactForSponsorEC>();
        }        

        public static async Task<ContactForSponsorEC> GetContactForSponsorEC(ContactForSponsor childData)
        {
            return await DataPortal.FetchChildAsync<ContactForSponsorEC>(childData);
        }

        #endregion

        #region Data Access Methods

        [FetchChild]
        private void Fetch(ContactForSponsor childData)
        {
            using (BypassPropertyChecks)
            {
                Id = childData.Id;
                Sponsor = childData.Sponsor;
                DateWhenContacted = childData.DateWhenContacted;
                Purpose = childData.Purpose;
                RecordOfDiscussion = childData.RecordOfDiscussion;
                Person = childData.Person;
                LastUpdatedBy = childData.LastUpdatedBy;
                LastUpdatedDate = childData.LastUpdatedDate;
                Notes = childData.Notes;
                RowVersion = childData.RowVersion;
            }
        }

        [InsertChild]
        private async Task InsertChild()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IContactForSponsorDal>();
            var data = new ContactForSponsor()
            {
                // TODO: provide sponsor & Person functionality
                Sponsor = Sponsor,
                DateWhenContacted = DateWhenContacted,
                Purpose = Purpose,
                RecordOfDiscussion = RecordOfDiscussion,
                Person = Person,
                LastUpdatedBy = LastUpdatedBy,
                LastUpdatedDate = LastUpdatedDate,
                Notes = Notes,
                RowVersion = RowVersion
            };

            var insertedContactForSponsor = await dal.Insert(data);
            Id = insertedContactForSponsor.Id;
            RowVersion = insertedContactForSponsor.RowVersion;
        }

        [UpdateChild]
        private async Task ChildUpdate()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IContactForSponsorDal>();

            var emailTypeToUpdate = new ContactForSponsor()
            {
                Id = Id,
                Sponsor = Sponsor,
                DateWhenContacted = DateWhenContacted,
                Purpose = Purpose,
                RecordOfDiscussion = RecordOfDiscussion,
                Person = Person,
                LastUpdatedBy = LastUpdatedBy,
                LastUpdatedDate = LastUpdatedDate,
                Notes = Notes,
                RowVersion = RowVersion
            };

            var updatedEmail = await dal.Update(emailTypeToUpdate);
            RowVersion = updatedEmail.RowVersion;
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
            var dal = dalManager.GetProvider<IContactForSponsorDal>();
           
            await dal.Delete(id);
        }

        #endregion
    }
}