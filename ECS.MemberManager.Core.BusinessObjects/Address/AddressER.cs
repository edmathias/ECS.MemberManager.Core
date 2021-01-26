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
    public class AddressER : BusinessBase<AddressER>
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

        public static async Task<AddressER> NewAddressER()
        {
            return await DataPortal.CreateAsync<AddressER>();
        }

        public static async Task<AddressER> GetAddressER(int id)
        {
            return await DataPortal.FetchAsync<AddressER>(id);
        }

        public static async Task DeleteAddressER(int id)
        {
            await DataPortal.DeleteAsync<AddressER>(id);
        }

        #endregion

        #region Data Access Methods

        [Fetch]
        private async Task Fetch(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IAddressDal>();
            var data = await dal.Fetch(id);

            using (BypassPropertyChecks)
            {
                Id = data.Id;
                Address1 = data.Address1;
                Address2 = data.Address2;
                City = data.City;
                State = data.State;
                PostCode = data.PostCode;
                LastUpdatedBy = data.LastUpdatedBy;
                LastUpdatedDate = data.LastUpdatedDate;
                Notes = data.Notes;
                RowVersion = data.RowVersion;
            }
        }

        [Insert]
        private async Task Insert()
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
        private async Task Update()
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
        
        [DeleteSelf]
        private async Task DeleteSelf()
        {
            await Delete(Id);
        }

        [Delete]
        private async Task Delete(int id)
        {
            using var dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IAddressDal>();
           
            await dal.Delete(id);
        }

        #endregion
    }
}