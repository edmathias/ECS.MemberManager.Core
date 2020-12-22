using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;

namespace ECS.MemberManager.Core.BusinessObjects.Sponsor
{
    [Serializable]
    public class SponsorER : BusinessBase<SponsorER>
    {
        #region Business Methods

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id);
        public int Id
        {
            get => GetProperty(IdProperty);
            private set => LoadProperty(IdProperty, value);
        }

        public static readonly PropertyInfo<string> StatusProperty = RegisterProperty<string>(p => p.Status);
        [MaxLength(50)]
        public string Status
        {
            get => GetProperty(StatusProperty);
            set => SetProperty(StatusProperty, value);
        }

        public static readonly PropertyInfo<SmartDate> DateOfFirstContactProperty = RegisterProperty<SmartDate>(p => p.DateOfFirstContact);
        public SmartDate DateOfFirstContact
        {
            get => GetProperty(DateOfFirstContactProperty);
            set => SetProperty(DateOfFirstContactProperty, value);
        }

        public static readonly PropertyInfo<string> ReferredByProperty = RegisterProperty<string>(p => p.ReferredBy);
        [MaxLength(255)]
        public string ReferredBy
        {
            get => GetProperty(ReferredByProperty);
            set => SetProperty(ReferredByProperty, value);
        }
        
        public static readonly PropertyInfo<SmartDate> DateSponsorAcceptedProperty = RegisterProperty<SmartDate>(p => p.DateSponsorAccepted);
        public SmartDate DateSponsorAccepted
        {
            get => GetProperty(DateSponsorAcceptedProperty);
            set => SetProperty(DateSponsorAcceptedProperty, value);
        }

        public static readonly PropertyInfo<string> TypeNameProperty = RegisterProperty<string>(p => p.TypeName);
        public string TypeName
        {
            get => GetProperty(TypeNameProperty);
            set => SetProperty(TypeNameProperty, value);
        }

        public static readonly PropertyInfo<string> DetailsProperty = RegisterProperty<string>(p => p.Details);
        public string Details
        {
            get => GetProperty(DetailsProperty);
            set => SetProperty(DetailsProperty, value);
        }

        public static readonly PropertyInfo<SmartDate> SponsorUntilDateProperty = RegisterProperty<SmartDate>(p => p.SponsorUntilDate);
        public SmartDate SponsorUntilDate
        {
            get => GetProperty(SponsorUntilDateProperty);
            set => SetProperty(SponsorUntilDateProperty, value);
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

        public static readonly PropertyInfo<PersonEC> PersonProperty = RegisterProperty<PersonEC>(p => p.Person);
        public PersonEC Person
        {
            get => GetProperty(PersonProperty);
            set => SetProperty(PersonProperty, value);
        }

        public static readonly PropertyInfo<OrganizationEC> OrganizationProperty = RegisterProperty<OrganizationEC>(p => p.Organization);
        public OrganizationEC Organization
        {
            get => GetProperty(OrganizationProperty);
            set => SetProperty(OrganizationProperty, value);
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

        public static async Task<SponsorER> NewSponsorER()
        {
            return await DataPortal.CreateAsync<SponsorER>();
        }

        public static async Task<SponsorER> GetSponsorER(int id)
        {
            return await DataPortal.FetchAsync<SponsorER>(id);
        }

        public static async Task DeleteSponsorER(int id)
        {
            await DataPortal.DeleteAsync<SponsorER>(id);
        }

        #endregion

        #region Data Access

        [Fetch]
        private void Fetch(int id)
        {
            using IDalManager dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<ISponsorDal>();
            var data = dal.Fetch(id);
            using (BypassPropertyChecks)
            {
                Id = data.Id;

            }
        }

        [Insert]
        private void Insert()
        {
            using IDalManager dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<SponsorER>();
            using (BypassPropertyChecks)
            {
                // format and store dto 

            }
        }

        [Update]
        private void Update()
        {
            using IDalManager dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<SponsorER>();
            using (BypassPropertyChecks)
            {
                // format dto and update dal 
            }
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
            var dal = dalManager.GetProvider<SponsorER>();

            dal.Delete(id);
        }



        #endregion

 
    }
}