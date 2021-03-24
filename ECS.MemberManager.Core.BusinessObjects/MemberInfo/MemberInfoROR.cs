//******************************************************************************
// This file has been generated via text template.
// Do not make changes as they will be automatically overwritten.
//
// Generated on 03/23/2021 09:57:14
//******************************************************************************    

using System;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess.Dal;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public partial class MemberInfoROR : BusinessBase<MemberInfoROR>
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

        public static readonly PropertyInfo<string>
            MemberNumberProperty = RegisterProperty<string>(o => o.MemberNumber);

        public virtual string MemberNumber
        {
            get => GetProperty(MemberNumberProperty);
            private set => LoadProperty(MemberNumberProperty, value);
        }

        public static readonly PropertyInfo<SmartDate> DateFirstJoinedProperty =
            RegisterProperty<SmartDate>(o => o.DateFirstJoined);

        public virtual SmartDate DateFirstJoined
        {
            get => GetProperty(DateFirstJoinedProperty);
            private set => LoadProperty(DateFirstJoinedProperty, value);
        }


        public static readonly PropertyInfo<PrivacyLevelROC> PrivacyLevelProperty =
            RegisterProperty<PrivacyLevelROC>(o => o.PrivacyLevel);

        public PrivacyLevelROC PrivacyLevel
        {
            get => GetProperty(PrivacyLevelProperty);

            private set => LoadProperty(PrivacyLevelProperty, value);
        }


        public static readonly PropertyInfo<MemberStatusROC> MemberStatusProperty =
            RegisterProperty<MemberStatusROC>(o => o.MemberStatus);

        public MemberStatusROC MemberStatus
        {
            get => GetProperty(MemberStatusProperty);

            private set => LoadProperty(MemberStatusProperty, value);
        }


        public static readonly PropertyInfo<MembershipTypeROC> MembershipTypeProperty =
            RegisterProperty<MembershipTypeROC>(o => o.MembershipType);

        public MembershipTypeROC MembershipType
        {
            get => GetProperty(MembershipTypeProperty);

            private set => LoadProperty(MembershipTypeProperty, value);
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

        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(o => o.Notes);

        public virtual string Notes
        {
            get => GetProperty(NotesProperty);
            private set => LoadProperty(NotesProperty, value);
        }

        public static readonly PropertyInfo<byte[]> RowVersionProperty = RegisterProperty<byte[]>(o => o.RowVersion);

        public virtual byte[] RowVersion
        {
            get => GetProperty(RowVersionProperty);
            private set => LoadProperty(RowVersionProperty, value);
        }

        #endregion

        #region Factory Methods

        public static async Task<MemberInfoROR> GetMemberInfoROR(int id)
        {
            return await DataPortal.FetchAsync<MemberInfoROR>(id);
        }

        #endregion

        #region Data Access Methods

        [Fetch]
        private async Task Fetch(int id, [Inject] IMemberInfoDal dal)
        {
            var data = await dal.Fetch(id);

            Id = data.Id;
            Person = (data.Person != null ? await PersonROC.GetPersonROC(data.Person) : null);
            MemberNumber = data.MemberNumber;
            DateFirstJoined = data.DateFirstJoined;
            PrivacyLevel = (data.PrivacyLevel != null
                ? await PrivacyLevelROC.GetPrivacyLevelROC(data.PrivacyLevel)
                : null);
            MemberStatus = (data.MemberStatus != null
                ? await MemberStatusROC.GetMemberStatusROC(data.MemberStatus)
                : null);
            MembershipType = (data.MembershipType != null
                ? await MembershipTypeROC.GetMembershipTypeROC(data.MembershipType)
                : null);
            LastUpdatedBy = data.LastUpdatedBy;
            LastUpdatedDate = data.LastUpdatedDate;
            Notes = data.Notes;
            RowVersion = data.RowVersion;
        }

        #endregion
    }
}