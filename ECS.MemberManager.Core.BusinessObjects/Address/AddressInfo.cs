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
    public class AddressInfo : ReadOnlyBase<AddressInfo>
    {
        #region Business Methods

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id);

        public int Id
        {
            get => GetProperty(IdProperty);
            private set => LoadProperty(IdProperty, value);
        }

        public static readonly PropertyInfo<string> Address1Property = RegisterProperty<string>(p => p.Address1);

        [Required, MaxLength(35)]
        public string Address1
        {
            get => GetProperty(Address1Property);
            private set => LoadProperty(Address1Property, value);
        }

        public static readonly PropertyInfo<string> Address2Property = RegisterProperty<string>(p => p.Address2);

        [Required, MaxLength(35)]
        public string Address2
        {
            get => GetProperty(Address2Property);
            private set => LoadProperty(Address2Property, value);
        }

        public static readonly PropertyInfo<string> CityProperty = RegisterProperty<string>(p => p.City);

        [Required, MaxLength(50)]
        public string City
        {
            get => GetProperty(CityProperty);
            private set => LoadProperty(CityProperty, value);
        }

        public static readonly PropertyInfo<string> StateProperty = RegisterProperty<string>(p => p.State);

        [Required, MaxLength(9)]
        public string State
        {
            get => GetProperty(StateProperty);
            private set => LoadProperty(StateProperty, value);
        }

        public static readonly PropertyInfo<string> PostCodeProperty = RegisterProperty<string>(p => p.PostCode);

        public string PostCode
        {
            get => GetProperty(PostCodeProperty);
            private set => LoadProperty(PostCodeProperty, value);
        }

        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(p => p.Notes);

        public string Notes
        {
            get => GetProperty(NotesProperty);
            private set => LoadProperty(NotesProperty, value);
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

        public static async Task<AddressInfo> GetAddressInfo(Address childData)
        {
            return await DataPortal.FetchChildAsync<AddressInfo>(childData);
        }

        public static async Task<AddressInfo> GetAddressInfo(int id)
        {
            return await DataPortal.FetchAsync<AddressInfo>(id);
        }

        #endregion

        #region Data Access Methods

        [FetchChild]
        private void Fetch(Address childData)
        {
            Id = childData.Id;
            Address1 = childData.Address1;
            Address2 = childData.Address2;
            City = childData.City;
            State = childData.State;
            PostCode = childData.PostCode;
            LastUpdatedBy = childData.LastUpdatedBy;
            LastUpdatedDate = childData.LastUpdatedDate;
            Notes = childData.Notes;
            RowVersion = childData.RowVersion;
        }

        [Fetch]
        private async void Fetch(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IAddressDal>();
            var data = await dal.Fetch(id);

            Fetch(data);
        }

        #endregion
    }
}