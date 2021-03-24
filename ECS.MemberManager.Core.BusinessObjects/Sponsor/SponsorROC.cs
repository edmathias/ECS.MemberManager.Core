//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 03/23/2021 09:57:46
//******************************************************************************    

using System;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class SponsorROC : ReadOnlyBase<SponsorROC>
    {
        #region Business Methods

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(o => o.Id);

        public virtual int Id
        {
            get => GetProperty(IdProperty);
            private set => LoadProperty(IdProperty, value);
        }


        public static readonly PropertyInfo<PersonROC> PersonProperty = RegisterProperty<PersonROC>(o => o.Person);

        public PersonROC Person
        {
            get => GetProperty(PersonProperty);

            private set => LoadProperty(PersonProperty, value);
        }


        public static readonly PropertyInfo<OrganizationROC> OrganizationProperty =
            RegisterProperty<OrganizationROC>(o => o.Organization);

        public OrganizationROC Organization
        {
            get => GetProperty(OrganizationProperty);

            private set => LoadProperty(OrganizationProperty, value);
        }

        public static readonly PropertyInfo<string> StatusProperty = RegisterProperty<string>(o => o.Status);

        public virtual string Status
        {
            get => GetProperty(StatusProperty);
            private set => LoadProperty(StatusProperty, value);
        }

        public static readonly PropertyInfo<SmartDate> DateOfFirstContactProperty =
            RegisterProperty<SmartDate>(o => o.DateOfFirstContact);

        public virtual SmartDate DateOfFirstContact
        {
            get => GetProperty(DateOfFirstContactProperty);
            private set => LoadProperty(DateOfFirstContactProperty, value);
        }

        public static readonly PropertyInfo<string> ReferredByProperty = RegisterProperty<string>(o => o.ReferredBy);

        public virtual string ReferredBy
        {
            get => GetProperty(ReferredByProperty);
            private set => LoadProperty(ReferredByProperty, value);
        }

        public static readonly PropertyInfo<SmartDate> DateSponsorAcceptedProperty =
            RegisterProperty<SmartDate>(o => o.DateSponsorAccepted);

        public virtual SmartDate DateSponsorAccepted
        {
            get => GetProperty(DateSponsorAcceptedProperty);
            private set => LoadProperty(DateSponsorAcceptedProperty, value);
        }

        public static readonly PropertyInfo<string> TypeNameProperty = RegisterProperty<string>(o => o.TypeName);

        public virtual string TypeName
        {
            get => GetProperty(TypeNameProperty);
            private set => LoadProperty(TypeNameProperty, value);
        }

        public static readonly PropertyInfo<string> DetailsProperty = RegisterProperty<string>(o => o.Details);

        public virtual string Details
        {
            get => GetProperty(DetailsProperty);
            private set => LoadProperty(DetailsProperty, value);
        }

        public static readonly PropertyInfo<SmartDate> SponsorUntilDateProperty =
            RegisterProperty<SmartDate>(o => o.SponsorUntilDate);

        public virtual SmartDate SponsorUntilDate
        {
            get => GetProperty(SponsorUntilDateProperty);
            private set => LoadProperty(SponsorUntilDateProperty, value);
        }

        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(o => o.Notes);

        public virtual string Notes
        {
            get => GetProperty(NotesProperty);
            private set => LoadProperty(NotesProperty, value);
        }

        public static readonly PropertyInfo<string> LastUpdatedByProperty =
            RegisterProperty<string>(o => o.LastUpdatedBy);

        public virtual string LastUpdatedBy
        {
            get => GetProperty(LastUpdatedByProperty);
            private set => LoadProperty(LastUpdatedByProperty, value);
        }

        public static readonly PropertyInfo<SmartDate> LastUpdatedDateProperty =
            RegisterProperty<SmartDate>(o => o.LastUpdatedDate);

        public virtual SmartDate LastUpdatedDate
        {
            get => GetProperty(LastUpdatedDateProperty);
            private set => LoadProperty(LastUpdatedDateProperty, value);
        }

        public static readonly PropertyInfo<byte[]> RowVersionProperty = RegisterProperty<byte[]>(o => o.RowVersion);

        public virtual byte[] RowVersion
        {
            get => GetProperty(RowVersionProperty);
            private set => LoadProperty(RowVersionProperty, value);
        }

        #endregion

        #region Factory Methods

        internal static async Task<SponsorROC> GetSponsorROC(Sponsor childData)
        {
            return await DataPortal.FetchChildAsync<SponsorROC>(childData);
        }

        #endregion

        #region Data Access Methods

        [FetchChild]
        private async Task Fetch(Sponsor data)
        {
            Id = data.Id;
            Person = (data.Person != null ? await PersonROC.GetPersonROC(data.Person) : null);
            Organization = (data.Organization != null
                ? await OrganizationROC.GetOrganizationROC(data.Organization)
                : null);
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

        #endregion
    }
}