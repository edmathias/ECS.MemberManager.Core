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
    public class AddressEdit : BusinessBase<AddressEdit>
    {
        #region Business Methods

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id);

        public int Id
        {
            get => GetProperty(IdProperty);
            set => SetProperty(IdProperty, value);
        }

        public static readonly PropertyInfo<string> Address1Property = RegisterProperty<string>(p => p.Address1);
        [Required, MaxLength(35)]
        public string Address1
        {
            get => GetProperty(Address1Property);
            set => SetProperty(Address1Property, value);
        }
        
        public static readonly PropertyInfo<string> Address2Property = RegisterProperty<string>(p => p.Address2);
        [Required, MaxLength(35)]
        public string Address2
        {
            get => GetProperty(Address2Property);
            set => SetProperty(Address2Property, value);
        }

        public static readonly PropertyInfo<string> CityProperty = RegisterProperty<string>(p => p.City);
        [Required, MaxLength(50)]
        public string City
        {
            get => GetProperty(CityProperty);
            set => SetProperty(CityProperty, value);
        }

        public static readonly PropertyInfo<string> StateProperty = RegisterProperty<string>(p => p.State);
        [Required, MaxLength(2)]
        public string State
        {
            get => GetProperty(StateProperty);
            set => SetProperty(StateProperty, value);
        }

        public static readonly PropertyInfo<string> PostCodeProperty = RegisterProperty<string>(p => p.PostCode);
        [Required, MaxLength(9)]
        public string PostCode
        {
            get => GetProperty(PostCodeProperty);
            set => SetProperty(PostCodeProperty, value);
        }
        
        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(p => p.Notes);
        public string Notes
        {
            get => GetProperty(NotesProperty);
            set => SetProperty(NotesProperty, value);
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

        public static async Task<AddressEdit> NewAddressEdit()
        {
            return await DataPortal.CreateAsync<AddressEdit>();
        }

        public static async Task<AddressEdit> GetAddressEdit(Address childData)
        {
            return await DataPortal.FetchChildAsync<AddressEdit>(childData);
        }

        public static async Task<AddressEdit> GetAddressEdit(int id)
        {
            return await DataPortal.FetchAsync<AddressEdit>(id);
        }

        public static async Task DeleteAddressEdit(int id)
        {
            await DataPortal.DeleteAsync<AddressEdit>(id);
        }

        #endregion

        #region Data Access Methods

        [FetchChild]
        private void Fetch(Address childData)
        {
            using (BypassPropertyChecks)
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
        }

        [Fetch]
        private async void Fetch(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IAddressDal>();
            var data = await dal.Fetch(id);

            Fetch(data);
        }

        [Insert]
        private void Insert()
        {
            InsertChild();
        }

        [InsertChild]
        private async void InsertChild()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IAddressDal>();
            var data = new Address()
            {
                Address1 = Address1,
                Address2 = Address2,
                City = City,
                State = State,
                PostCode = PostCode,
                LastUpdatedBy = LastUpdatedBy,
                LastUpdatedDate = LastUpdatedDate,
                Notes = Notes
            };

            var insertedAddress = await dal.Insert(data);
            Id = insertedAddress.Id;
            RowVersion = insertedAddress.RowVersion;
        }

        [Update]
        private void Update()
        {
            ChildUpdate();
        }

        [UpdateChild]
        private async void ChildUpdate()
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IAddressDal>();

            var emailTypeToUpdate = new Address()
            {
                Id = Id,
                Address1 = Address1,
                Address2 = Address2,
                City = City,
                State = State,
                PostCode = PostCode,
                LastUpdatedBy = LastUpdatedBy,
                LastUpdatedDate = LastUpdatedDate,
                Notes = Notes,
                RowVersion = RowVersion
            };

            var updatedEmail = await dal.Update(emailTypeToUpdate);
            RowVersion = updatedEmail.RowVersion;
        }
        
        [DeleteSelfChild]
        private void DeleteSelf()
        {
            Delete(Id);
        }

        [Delete]
        private async void Delete(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IAddressDal>();
           
            await dal.Delete(id);
        }

        #endregion
    }
}