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
    public class EMailROR : ReadOnlyBase<EMailROR>
    {
        #region Business Methods

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id);

        public int Id
        {
            get => GetProperty(IdProperty);
            private set => LoadProperty(IdProperty, value);
        }

        public static readonly PropertyInfo<EMailTypeEC> EMailTypeProperty =
            RegisterProperty<EMailTypeEC>(p => p.EMailType);

        public EMailTypeEC EMailType
        {
            get => GetProperty(EMailTypeProperty);
            private set => LoadProperty(EMailTypeProperty, value);
        }

        public static readonly PropertyInfo<string>
            EMailAddressProperty = RegisterProperty<string>(p => p.EMailAddress);

        [Required, MaxLength(255)]
        public string EMailAddress
        {
            get => GetProperty(EMailAddressProperty);
            private set => LoadProperty(EMailAddressProperty, value);
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

        public static async Task<EMailROR> GetEMailROR(int id)
        {
            return await DataPortal.FetchAsync<EMailROR>(id);
        }

        #endregion

        #region Data Access Methods

        [Fetch]
        private async Task FetchAsync(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IEMailDal>();
            var data = await dal.Fetch(id);

            await Fetch(data);
        }

        private async Task Fetch(EMail childData)
        {
            Id = childData.Id;
            EMailAddress = childData.EMailAddress;
            EMailType = await EMailTypeEC.GetEMailTypeEC(childData.EMailType);
            LastUpdatedBy = childData.LastUpdatedBy;
            LastUpdatedDate = childData.LastUpdatedDate;
            Notes = childData.Notes;
            RowVersion = childData.RowVersion;
        }

        #endregion
    }
}