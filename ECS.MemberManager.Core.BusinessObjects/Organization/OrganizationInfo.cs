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
    public class OrganizationInfo : ReadOnlyBase<OrganizationInfo>
    {
        #region Business Methods

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id);

        public int Id
        {
            get => GetProperty(IdProperty);
            private set => LoadProperty(IdProperty, value);
        }

        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(p => p.Name);

        [Required, MaxLength(50)]
        public string Name
        {
            get => GetProperty(NameProperty);
            private set => LoadProperty(NameProperty, value);
        }

        public static readonly PropertyInfo<int> OrganizationTypeIdProperty =
            RegisterProperty<int>(p => p.OrganizationTypeId);

        public int OrganizationTypeId
        {
            get => GetProperty(OrganizationTypeIdProperty);
            private set => LoadProperty(OrganizationTypeIdProperty, value);
        }

        public static readonly PropertyInfo<SmartDate> DateOfFirstContactProperty =
            RegisterProperty<SmartDate>(p => p.DateOfFirstContact);

        public SmartDate DateOfFirstContact
        {
            get => GetProperty(DateOfFirstContactProperty);
            private set => LoadProperty(DateOfFirstContactProperty, value);
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

        public static async Task<OrganizationInfo> GetOrganizationInfo(Organization childData)
        {
            return await DataPortal.FetchChildAsync<OrganizationInfo>(childData);
        }

        public static async Task<OrganizationInfo> GetOrganizationInfo(int id)
        {
            return await DataPortal.FetchAsync<OrganizationInfo>(id);
        }

        #endregion

        #region Data Access Methods

        [FetchChild]
        private void FetchChild(Organization childData)
        {
            Id = childData.Id;
            Name = childData.Name;
            OrganizationTypeId = childData.OrganizationTypeId;
            DateOfFirstContact = childData.DateOfFirstContact;
            LastUpdatedBy = childData.LastUpdatedBy;
            LastUpdatedDate = childData.LastUpdatedDate;
            Notes = childData.Notes;
            RowVersion = childData.RowVersion;
        }

        [Fetch]
        private async Task Fetch(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IOrganizationDal>();
            var data = await dal.Fetch(id);

            FetchChild(data);
        }

        #endregion
    }
}